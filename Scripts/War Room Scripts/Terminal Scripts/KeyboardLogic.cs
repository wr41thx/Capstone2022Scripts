using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Performs check of Terminal Status when triggered.  Updates Terminal Status if puzzle is solved.
// Boolean solution array corresponds to indicies of LED array from left to right, top row to bottom row.
public class KeyboardLogic : MonoBehaviour, IInteractable
{
    [SerializeField] private bool[] solutionArr;
    [SerializeField] private GameObject[] lightsArr;
    [SerializeField] private GameObject defcodeText;
    [SerializeField] private GameObject solvedText;
    [SerializeField] private GameObject monitorLed;
    [SerializeField] private UnityEvent TerminalSolved;

    [SerializeField] private bool isSolved;

    [SerializeField] private AudioSource kbAudio;
    [SerializeField] private AudioClip wrongSnd;
    [SerializeField] private AudioClip correctSnd;

    [SerializeField] private Material emissiveMaterial;

    // All lighting effects disabled, initiallizes audio source and status of puzzle.
    private void Awake()
    {
        emissiveMaterial = gameObject.GetComponent<Renderer>().material;
        emissiveMaterial.DisableKeyword("_EMISSION");
        kbAudio = gameObject.GetComponent<AudioSource>();
        kbAudio.volume = .1f;
        kbAudio.loop = false;
        isSolved = false;
        defcodeText.SetActive(false);
        gameObject.layer = 0;
        monitorLed.SetActive(false);
    }

    // Enables Monitor, Becomes Interactable, Illuminates
    public void TurnMonitorOn() 
    {
        monitorLed.SetActive(true);
        defcodeText.SetActive(true);
        gameObject.layer = 10;
        emissiveMaterial.EnableKeyword("_EMISSION");
    }

    // Checks the array of LED lights to see if they match the appropriate solution
    private void CheckSolution() 
    {
        
        if (!isSolved)
        {
            for (int i = 0; i < solutionArr.Length; i++)
            {
                if (lightsArr[i].GetComponent<TermLights>().lightOn != solutionArr[i])
                {
                    isSolved = false;
                    kbAudio.PlayOneShot(wrongSnd);
                    break;
                }
                isSolved = true;
            }

            if (isSolved) 
            {
                SolveTerminal();
            }        
        }
    }

    // Invokes event if terminal is Solved and disables further interactions
    private void SolveTerminal() 
    {
        kbAudio.PlayOneShot(correctSnd);
        defcodeText.SetActive(false);
        solvedText.SetActive(true);
        emissiveMaterial.DisableKeyword("_EMISSION");
        gameObject.layer = 0;
        TerminalSolved.Invoke(); // Disable buttons, tell war room manager this terminal is solved
    }

    // Public function used to update central screen for Status of all terminals.
    public bool IsSolved() 
    {
        return isSolved;
    }


    public void Interact() 
    {
        CheckSolution();
    }
}
