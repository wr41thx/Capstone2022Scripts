using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle2Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] puzzle3Pieces;
    [SerializeField] private GameObject trigger;
    [SerializeField] GameObject[] lightsToTurnOff;
    [SerializeField] GameObject[] lightsToTurnOn;
    [SerializeField] private AudioSource myAudioSource;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "LeftCube")
        {
            myAudioSource.Play();
            player.GetComponent<PlayerInteract>().GrabDrop();
            ActivateNextPuzzle();
        }
    }

    private void ActivateNextPuzzle()
    {
        trigger.SetActive(true);

        foreach (GameObject puzzlepiece in puzzle3Pieces)
        {
            puzzlepiece.SetActive(true);
        }

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
