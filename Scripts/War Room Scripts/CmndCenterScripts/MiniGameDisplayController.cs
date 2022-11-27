using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MiniGameDisplayController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject miniGameCam;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private bool isPlaying;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private UnityEvent toggleSnap;

    [SerializeField] private GameObject miniGameManager;


    private void Awake()
    {
        isPlaying = false;
        miniGameCam.SetActive(false);
    }

    // Activate the Pile Bunker Mini Game Screen Object
    public void SetGameScreen() 
    {
        gameObject.SetActive(true);
        miniGameManager.SetActive(true);
    }

    // Swap Camera from player FPS to fixed Monitor Cam
    private void FocusGame()
    {
        crossHair.SetActive(false);
        playerCam.SetActive(false);
        miniGameCam.SetActive(true);
    }

    private void FocusPlayer() 
    {
        crossHair.SetActive(true);
        playerCam.SetActive(true);
        miniGameCam.SetActive(false);
    }

    // Disable First Person Character Controller
    private void DisableFirstPersonController() 
    {
        player.GetComponent<StarterAssets.FirstPersonController>().ToggleController(false);
    }

    // Enable First Person Character Controller
    private void EnableFirstPersonController() 
    {
        player.GetComponent<StarterAssets.FirstPersonController>().ToggleController(true);
    }

    public void Interact() 
    {
        // Snap to monitor and start game if player interacts with monitor
        if (!isPlaying)
        {
            FocusGame();
            DisableFirstPersonController();
            isPlaying = true;

            // Start Game Loop if this is the first time we've interacted with the game.
            // Game Loop will handle all logic after this, but game will "play" on screen
            // even if the player steps away.

            toggleSnap.Invoke();
        }
        else 
        {
            // Return State back to normal FPS controller and Player Camera
            FocusPlayer();
            EnableFirstPersonController();
            isPlaying = false;

            toggleSnap.Invoke();
        }

    }
}
