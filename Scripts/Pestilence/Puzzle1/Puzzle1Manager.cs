using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle1Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] puzzlePieces;
    [SerializeField] private GameObject nextPuzzlePiece;
    [SerializeField] private GameObject []lightsToTurnOff;
    [SerializeField] private GameObject []lightsToTurnOn;
    [SerializeField] private AudioSource myAudioSource;
    private GameObject selectedPiece;
    private bool puzzle1Solved;
    private GameObject player; 

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // grab reference to player in room
        puzzle1Solved = false;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "PuzzlePiece 1_1":
                player.GetComponent<PlayerInteract>().GrabDrop();  // call the grab drop function to signal player has "dropped" tile
                puzzlePieces[0].SetActive(true);
                myAudioSource.Play();
                selectedPiece = GameObject.Find("PuzzlePiece 1_1");
                selectedPiece.SetActive(false);
                CheckPuzzle();
                break;
            case "PuzzlePiece 1_2":
                player.GetComponent<PlayerInteract>().GrabDrop();
                puzzlePieces[1].SetActive(true);
                myAudioSource.Play();
                selectedPiece = GameObject.Find("PuzzlePiece 1_2");
                selectedPiece.SetActive(false);
                CheckPuzzle();
                break;
            case "PuzzlePiece 2_1":
                player.GetComponent<PlayerInteract>().GrabDrop();
                puzzlePieces[2].SetActive(true);
                myAudioSource.Play();
                selectedPiece = GameObject.Find("PuzzlePiece 2_1");
                selectedPiece.SetActive(false);
                CheckPuzzle();
                break;
            case "PuzzlePiece 2_2":
                player.GetComponent<PlayerInteract>().GrabDrop();
                puzzlePieces[3].SetActive(true);
                myAudioSource.Play();
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
            nextPuzzlePiece.SetActive(true);
            foreach (GameObject light in lightsToTurnOff)
            {
                light.SetActive(false);
            }
            foreach (GameObject light in lightsToTurnOn)
            {
                light.SetActive(true);
            }
        }
    }

}
