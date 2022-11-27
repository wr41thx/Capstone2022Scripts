using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneLoader : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float delayTime;
    [SerializeField] private GameObject fadeScreen;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private PersistentDeathRoomManager deathManager;

    private void Awake()
    {
        deathManager = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<PersistentDeathRoomManager>();

        if (gameObject.GetComponent<AudioSource>() != null)
        {
            delayTime = gameObject.GetComponent<AudioSource>().clip.length;
        }
    }

    private IEnumerator LoadDelay(float delay) 
    {
        yield return new WaitForSeconds(delay);
        LoadScene();
    }

    private void DisableFirstPersonController()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<StarterAssets.FirstPersonController>().ToggleController(false);
    }

    private void DisableCrossHair() 
    {
        crossHair = GameObject.FindGameObjectWithTag("CrossHair");
        crossHair.SetActive(false);
    }

    private void LoadScene() 
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Interact() 
    {
        DisableCrossHair();
        DisableFirstPersonController();
        fadeScreen.SetActive(true);

        if (gameObject.GetComponent<AudioSource>() != null)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }

        if (SceneManager.GetActiveScene().name == "WarRoom")
        {
            deathManager.SetWarComplete();
            deathManager.UpdateSolveCount();
        }

        if (SceneManager.GetActiveScene().name == "FamineRoom")
        {
            deathManager.SetFamineComplete();
            deathManager.UpdateSolveCount();
        }

        if (SceneManager.GetActiveScene().name == "PestilenceRoom")
        {
            deathManager.SetPestComplete();
            deathManager.UpdateSolveCount();
        }

        if (this.gameObject.tag == "EndGameDoor") 
        {
            Application.ExternalEval("document.location.reload(true)");
        }

        StartCoroutine(LoadDelay(delayTime));
    }
}
