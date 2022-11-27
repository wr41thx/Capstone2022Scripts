using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject finalDoor;
    [SerializeField] private AudioSource myAudioSource;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Crown")
        {
            myAudioSource.Play();
            finalDoor.SetActive(true);
            player.GetComponent<PlayerInteract>().GrabDrop();
        }
    }
}
