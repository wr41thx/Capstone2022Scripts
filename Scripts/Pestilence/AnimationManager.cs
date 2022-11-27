using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator statue;
    [SerializeField] private AudioSource myAudioSource;
    private GameObject animTrigger;

    private void OnTriggerEnter(Collider other)
    {
        myAudioSource.Play();
        statue.SetTrigger("PlayerProximity");
    }

    private void OnTriggerExit(Collider other)
    {
        statue.SetTrigger("PlayerProximity");
        animTrigger = GameObject.Find("PointingStatueTrigger");
        animTrigger.SetActive(false);
    }

}
