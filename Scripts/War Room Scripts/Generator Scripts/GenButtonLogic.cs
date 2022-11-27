using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GenButtonLogic : MonoBehaviour, IInteractable
{
    // Basic controller for the buttons on the Generator.

    public bool isSelected;
    [SerializeField] private Light btnLight;
    [SerializeField] private GameObject genAudio;

    private void Awake()
    {
        isSelected = false;
        btnLight.color = Color.yellow;
    }

    public void ToggleButton() 
    {
        genAudio.GetComponent<AudioSource>().Play();

        if (isSelected) 
        {
            btnLight.color = Color.yellow;
            isSelected = false;
        }
        else
        {
            btnLight.color = Color.green;
            isSelected = true;
        }
        
    }

    public void Interact() 
    {
        ToggleButton();
    }
}
