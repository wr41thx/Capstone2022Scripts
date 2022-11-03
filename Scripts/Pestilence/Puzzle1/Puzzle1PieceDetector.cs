using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle1PieceDetector : MonoBehaviour, IInteractable
{
    private bool isFound;


    private void Awake()
    {
        isFound = false;
        Debug.Log("awake");
    }

    public void InspectPiece()
    {
        if (Input.GetKey(KeyCode.G))
        {
            Debug.Log("pressed E");
            if (!isFound)
            {
                Debug.Log("found something");
            }

        }
    }
    
    public void Interact()
    {
        InspectPiece();
    }

}
