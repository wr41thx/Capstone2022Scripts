using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Master Controller for the mini game running outside of main environment.
public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;
    private Vector3 titleStartPoint;

    [SerializeField] private GameObject titleEndPoint;
    [SerializeField] private float titleSpeed = 1.0f;
    private float titleStep;

    [SerializeField] private GameObject gameoverScreen;
    [SerializeField] private GameObject winScreen;


    [SerializeField] private bool isBeingControlled;

    [SerializeField] private GameObject foreground;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject planeSpawner;

    [SerializeField] private GameObject turret;

    private bool delayTimerStarted;
    private bool isWaiting;

    [SerializeField] private float gameTimer;
    [SerializeField] private float timeLimit;
    [SerializeField] private GameObject timerDisplay;

    [SerializeField] private GameObject sword;

    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip gameWonMusic;



    // State Machine States
    private enum GameState
    {
        GameMenu,
        StartGame,
        GameRunning,
        ResetGame,
        GameOver,
        GameWon,
        GameComplete
    }

    private GameState state;

    // Initial Game State settings when screen is set to active
    private void Awake()
    {
        gameTimer = timeLimit;
        delayTimerStarted = false;
        isWaiting = false;
        isBeingControlled = false;
        state = GameState.GameMenu;
        ToggleTurretController(false);
        titleStartPoint = titleScreen.transform.position;
    }

    private void Update()
    {
        // State Machine for Mini Game
        switch (state)
        {
            case GameState.GameMenu:
                GameMenu();
                break;

            case GameState.StartGame:
                StartGame();
                break;

            case GameState.ResetGame:
                ResetGame();
                break;

            case GameState.GameRunning:
                GameLoop();
                break;

            case GameState.GameOver:
                GameOver();
                break;

            case GameState.GameWon:
                PlayerWon();
                break;

            case GameState.GameComplete:
                GameComplete();
                break;
        }
    }

    // Fired by Display Object Event when player interacts with the screen.
    // Toggles between playing the game and stepping away from monitor.
    public void InteractToggle() 
    {
        if (state == GameState.GameMenu && isBeingControlled == false)
        {
            Debug.Log("Changing GameState to StartGame");
            state = GameState.StartGame;
            isBeingControlled = true;
        }
        else 
        {
            isBeingControlled = !isBeingControlled;
        }
       
    }

    // Enable / Disable Turret Controller
    public void ToggleTurretController(bool canControl) 
    {
        
        turret.GetComponent<TurretController>().toggleTurret(canControl);
    }

    // Change Game State - Easier to read.
    private void ChangeState(GameState gameState) 
    {
        state = gameState;
    }

    // Logic for Game Menu State
    private void GameMenu() 
    {
        ToggleTurretController(false);
        ToggleTimer(false);
        gameAudio.volume = .08f;
        gameAudio.clip = menuMusic;
        gameAudio.loop = true;
        if (!gameAudio.isPlaying) 
        {
            gameAudio.Play();
        }
        
    }

    // Displays or Hides the Timer
    private void ToggleTimer(bool toggle) 
    {
        timerDisplay.GetComponent<MeshRenderer>().enabled = toggle;
    }

    // Triggered by Display Event and starts core game loop
    private void StartGame()
    {
        gameAudio.loop = false;
        gameAudio.Stop();
        // Slide Title down
        titleStep = titleSpeed * Time.deltaTime;
        titleScreen.transform.position = Vector3.MoveTowards(titleScreen.transform.position, titleEndPoint.transform.position, titleStep);

        // Run Game when title screen is gone
        if (titleScreen.transform.position == titleEndPoint.transform.position) 
        {
            Debug.Log("Changing Game State to GameRunning");
            ToggleTimer(true);
            SpawnPlanes(true);
            ChangeState(GameState.GameRunning);
        }        
    }

    // Game Running State Logic
    private void GameLoop() 
    {
        gameAudio.volume = .05f;
        // Activate Turret Controls
        ToggleTurretController(isBeingControlled);

        // Update Game Timer
        gameTimer -= Time.deltaTime;
        timerDisplay.GetComponent<GameTimer>().SetTime(gameTimer);

        // Check Win Condition
        if (gameTimer <= 0) 
        {
            gameTimer = 0;
            Debug.Log("You Won! Changing State to GameWon");
            ChangeState(GameState.GameWon);
        }

        // Check Lose Condition
        if (foreground.GetComponent<CheckLoss>().CheckIfHit()) 
        {
            Debug.Log("IMPACT!  Changing State to GameOver");
            ChangeState(GameState.GameOver);
        }
    }

    // Enables spawning of planes
    private void SpawnPlanes(bool canSpawn) 
    {
        planeSpawner.SetActive(canSpawn);
    }

    // Finds and destroys all planes on screen
    private void WipePlanes() 
    {
        GameObject[] leftPlanes = GameObject.FindGameObjectsWithTag("LeftBomber");
        GameObject[] rightPlanes = GameObject.FindGameObjectsWithTag("RightBomber");
        foreach (GameObject plane in leftPlanes) 
        {
            Destroy(plane);
        }
        foreach (GameObject plane in rightPlanes)
        {
            Destroy(plane);
        }
    }

    // Find and Destroys all Bombs on screen
    private void WipeBombs() 
    {
        GameObject[] allBombs = GameObject.FindGameObjectsWithTag("Missile");
        foreach (GameObject bomb in allBombs)
        {
            Destroy(bomb);
        }
    }


    // Shows Game Over Screen, Triggers Game Reset
    private void GameOver()
    {
        // Reset Game Timer
        gameTimer = timeLimit;
        
        // Turn off plane Spawning and disable turret controls
        SpawnPlanes(false);
        ToggleTurretController(false);

        // Destroy all Planes and Bombs on Screen
        WipeBombs();
        WipePlanes();

        // Show Game Over Screen and play music
        gameoverScreen.SetActive(true);
        gameAudio.volume = .04f;

        if (!gameAudio.isPlaying) 
        {
            gameAudio.PlayOneShot(gameOverMusic);
        }

        // Wait a bit
        if (!delayTimerStarted) 
        {
            isWaiting = true;
            StartCoroutine(DelayTimer(3f));
            delayTimerStarted = true;
        }

        // Done Waiting, swap states
        if (!isWaiting) 
        {
            delayTimerStarted = false;
            Debug.Log("Changing GameState to ResetGame");
            ChangeState(GameState.ResetGame);
        }       
    }

    // Resets the game back to original state
    private void ResetGame()
    {
        gameAudio.Stop();
        // Hide Game Over Screen
        gameoverScreen.SetActive(false);

        // Reset Pile Bunker Screen
        titleScreen.transform.position = titleStartPoint;

        // Reset Foreground detection
        foreground.GetComponent<CheckLoss>().ResetIfHit();
        
        // Set State to Menu
        Debug.Log("Reset Complete: Changing State to GameMenu");
        ChangeState(GameState.GameMenu);
    }

    // Used by timer to get current game time.
    public float GetGameTime() 
    {
        return gameTimer;
    }

    // Player Win State
    private void PlayerWon()
    {
        // Disable most game functions and clearn board
        gameAudio.loop = false;
        ToggleTimer(false);
        ToggleTurretController(false);
        SpawnPlanes(false);
        WipeBombs();
        WipePlanes();

        // Show Win Screen and play song
        winScreen.SetActive(true);
        gameAudio.volume = .08f;
        gameAudio.PlayOneShot(gameWonMusic);

        // Spawn Sword behind player
        sword.SetActive(true);
        ChangeState(GameState.GameComplete);
    }

    // Disables the game once the player has won. 
    private void GameComplete() 
    {
        if (!gameAudio.isPlaying) 
        {
            this.gameObject.SetActive(false);
        }
    }

    // For adding delay between state changes or other effects
    IEnumerator DelayTimer(float coolDownTime) 
    {
        yield return new WaitForSeconds(coolDownTime);
        isWaiting = false;
    }

}

