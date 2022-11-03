using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle1Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] puzzlePieces;
    [SerializeField] private GameObject nextPuzzlePiece;
    private GameObject selectedPiece;
    private bool puzzle1Solved;


    private void Start()
    {
        puzzle1Solved = false;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "PuzzlePiece 1_1":
                puzzlePieces[0].SetActive(true);
                selectedPiece = GameObject.Find("PuzzlePiece 1_1");
                selectedPiece.SetActive(false);
                CheckPuzzle();
                break;
            case "PuzzlePiece 1_2":
                puzzlePieces[1].SetActive(true);
                selectedPiece = GameObject.Find("PuzzlePiece 1_2");
                selectedPiece.SetActive(false);
                CheckPuzzle();
                break;
            case "PuzzlePiece 2_1":
                puzzlePieces[2].SetActive(true);
                selectedPiece = GameObject.Find("PuzzlePiece 2_1");
                selectedPiece.SetActive(false);
                CheckPuzzle();
                break;
            case "PuzzlePiece 2_2":
                puzzlePieces[3].SetActive(true);
                selectedPiece = GameObject.Find("PuzzlePiece 2_2");
                selectedPiece.SetActive(false);
                CheckPuzzle();
                break;


        }
    }

    private void CheckPuzzle()
    {

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (puzzlePieces[i].activeSelf == false)
            {
                return;
            }
            puzzle1Solved = true;
        }
        if (puzzle1Solved == true)
        {
            Debug.Log("puzzle solved");
            nextPuzzlePiece.SetActive(true);
        }
    }

}
