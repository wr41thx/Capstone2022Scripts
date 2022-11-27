using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3Manager : MonoBehaviour
{
    [SerializeField] private GameObject puzzle3Piece;
    [SerializeField] private GameObject finalStairs;
    [SerializeField] private GameObject coffinLid;
    [SerializeField] private GameObject[] lightsToTurnOff;
    [SerializeField] private GameObject[] lightsToTurnOn;
    [SerializeField] private AudioSource myAudioSource;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "StatueBaseHandle")
        {
            myAudioSource.Play();
            player.GetComponent<PlayerInteract>().GrabDrop();
            ActivateStairs();
        }
    }

    private void ActivateStairs()
    {
        finalStairs.SetActive(true);
        coffinLid.SetActive(true);

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
