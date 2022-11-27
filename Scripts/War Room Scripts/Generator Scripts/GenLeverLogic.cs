using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Logic triggered when player interacts with the Generator Lever.
// Compares state of all buttons on generator with the solutions array.
// Comparisons are left to right, top to bottom.  
public class GenLeverLogic : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] btnArray;
    [SerializeField] private bool[] solutionArray;
    [SerializeField] private bool isSolved;
    [SerializeField] private UnityEvent powerOn;

    private Animator leverAnim;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject warRoomAmbient;
    [SerializeField] private AudioClip incorrectSound;
    [SerializeField] private AudioClip correctSound;


    private void Awake()
    {
        isSolved = false;
        leverAnim = gameObject.GetComponent<Animator>();
    }

    // Compares the solutions array to the state of the buttons in button array.
    // Performs desired logic if solution is correct.
    public void ActivateGenerator() 
    {
        for (int i = 0; i < solutionArray.Length; i++) 
        {
            if (btnArray[i].GetComponent<GenButtonLogic>().isSelected != solutionArray[i])
            {
                isSolved = false;
                break;
            }
            else 
            {
                isSolved = true;
            }
           
        }

        if (isSolved)
        {
            audioSource.PlayOneShot(correctSound);
            warRoomAmbient.SetActive(true);
            leverAnim.Play("LeverCorrect", 0, 0f);
            gameObject.layer = 0;  // Disable player interactino with lever if puzzle is "solved"
            powerOn.Invoke();  // Event Trigger
        }
        else 
        {
            audioSource.PlayOneShot(incorrectSound);
            leverAnim.Play("LeverWrong", 0, 0f);         
        }

    }
   
    public void Interact() 
    {
        ActivateGenerator();
    }
}
