using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoffinAnimationManager : MonoBehaviour
{
    public Animator coffin;
    [SerializeField] private GameObject tunnelLight;
    [SerializeField] private AudioSource myAudioSource;
    private GameObject animTrigger;

    private void OnTriggerEnter(Collider other)
    {
        myAudioSource.Play();
        coffin.SetTrigger("PlayerCoffinProximity");
        tunnelLight.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        coffin.SetTrigger("PlayerCoffinProximity");
        animTrigger = GameObject.Find("CoffinTrigger");
        animTrigger.SetActive(false);
    }
}
