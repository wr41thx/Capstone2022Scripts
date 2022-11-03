using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Controls how the player interacts with doors
public class DoorInteraction : MonoBehaviour, IInteractable
{
    private Animator doorAnim;
    [SerializeField] private bool doorOpen = false;
    [SerializeField] private AudioSource doorAudio;
    [SerializeField] private AudioClip openSnd;
    [SerializeField] private AudioClip closeSnd;

    // Scene Management for Exit Door Tagged Doors
    [SerializeField] private string sceneToLoad;
    [SerializeField] private PersistentDeathRoomManager deathManager;

    private void Awake()
    {
        doorAudio = gameObject.GetComponent<AudioSource>();
        doorAnim = gameObject.GetComponent<Animator>();
        deathManager = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<PersistentDeathRoomManager>();
    }

    // Animates door or triggers a scene change and death room status update if door is tagged "ExitDoor"
    public void ControlDoor() 
    {
        if (gameObject.tag == "ExitDoor")
        {
            doorAudio.PlayOneShot(openSnd);

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

            SceneManager.LoadScene(sceneToLoad);
        }
        else 
        {
            AnimateDoor();
        }

        
    }

    // Performs Animations and Sound.
    // Checking the normalized time prevents user from interrupting animation in progress.
    private void AnimateDoor() 
    {
        if (!doorOpen && doorAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            doorAnim.Play("DoorOpen", 0, 0f);
            doorAudio.PlayOneShot(openSnd);
            doorOpen = true;
        }
        else if (doorAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            doorAnim.Play("DoorClose", 0, 0f);
            doorAudio.PlayOneShot(closeSnd);
            doorOpen = false;
        }
    }

    public void Interact() 
    {
        ControlDoor();
    }


}
