using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] puzzle3Pieces;
    //private GameObject puzzle2TextPiece;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("here it is");
        ActivateNextPuzzle();
    }

    private void ActivateNextPuzzle()
    {
        for (int i = 0; i < puzzle3Pieces.Length; i++)
        {
            puzzle3Pieces[i].SetActive(true);
        }
    }
}
