using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Animates and Allows Player to interact with Terminal buttons.

public class TerminalBtnLogic : MonoBehaviour, IInteractable
{
    [SerializeField] public bool isOn;
    [SerializeField] private UnityEvent lightOn;
    [SerializeField] private UnityEvent lightOff;
    [SerializeField] private GameObject btn;
    [SerializeField] private Material[] lightMats;
    [SerializeField] private AudioSource btnAudio;
    private Animator btnAnim;

    private void Awake()
    {
        isOn = true;
        btnAnim = btn.GetComponent<Animator>();
        btn.GetComponent<MeshRenderer>().material = lightMats[0];
        btnAudio = gameObject.GetComponent<AudioSource>();
        DisableButton();
    }

    public void DisableButton() 
    {
        gameObject.layer = 0;
        btn.GetComponent<MeshRenderer>().material = lightMats[0];
    }

    public void EnableButton() 
    {
        btn.GetComponent<MeshRenderer>().material = lightMats[1];
        gameObject.layer = 10;
    }

    // Event Invokes LED on and off functions, allowing each button to control a set number of lights
    // independently.  Basically add each LED to the event and call Light On or Light Off.
    private void ToggleLights() 
    {
        btnAnim.Play("TermBtnPress", 0, 0f);
        btnAudio.Play();

        if (!isOn)
        {
            lightOn.Invoke();
            isOn = true;
        }
        else 
        {
            lightOff.Invoke();
            isOn = false;
        }        
    }

    public void Interact() 
    {
        ToggleLights();
    }
}
