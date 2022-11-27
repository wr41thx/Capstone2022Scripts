using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingAnimManager : MonoBehaviour
{
    [SerializeField] private Animator puzzle3Statue;
    [SerializeField] private AudioSource myAudioSource;
    private GameObject animTrigger;


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            myAudioSource.Play();
            puzzle3Statue.SetTrigger("Puzzle3Proximity");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        puzzle3Statue.SetTrigger("Puzzle3Proximity");
        animTrigger = GameObject.Find("PointTrigger");
        animTrigger.SetActive(false);
    }
}
