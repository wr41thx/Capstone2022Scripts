using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicturePuzzleData : MonoBehaviour
{
    // Event signal scriptable object
    [SerializeField] private RotationEventChannelSO eventChannelSO;
   // List of panel coordinates in the puzzle
    [SerializeField] private List<Transform> panelTransforms = new();
    
    // Position to compare puzzle transforms against to tell if puzzle is solved
    private Vector3 solvedPos = new (0.0f, 0.0f, 90.0f);

    void Start()
    {
        // Subscribes to event in the scriptable object to be notified when panels have been rotated
        eventChannelSO.updatePosition += CheckTransforms;
    }
     
    private void CheckTransforms ()
    {
        /*
         * Checks if the picture puzzle has been solved.
         * When solved, method is called to signal final puzzle reveal
         * and change the layer on the puzzle buttons from interactable (or disable script)
         */

        int solvedPanels = 0;

        foreach (Transform transform in panelTransforms)
        {
            if (transform.eulerAngles == solvedPos)
            {
                solvedPanels++;
            }
        }

        if (solvedPanels == 25)
        {
            //Reveals the final puzzle hidden behind a painting
            RevealSafe();
        }
    }

    private void RevealSafe()
    {
        //testing - Not fully implemented yet
        Debug.Log("success!");
        //
    }
}
