using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the state of and color of terminal LEDs
public class TermLights : MonoBehaviour
{
    [SerializeField] private Material[] lightMats;
    [SerializeField] private bool[] colorChoiceArr;
    private int chosenIndex;

    public bool lightOn;

    // Sets appropriate materials based on color choice array.
    // Color Choice array indices match order of desired color materials in mats array.
    // This just helped make the setup process a bit easier.

    private void Awake()
    {
        for (int i = 0; i < colorChoiceArr.Length; i++) 
        {
            if (colorChoiceArr[i]) 
            {
                chosenIndex = i;
            }    
        }

        this.GetComponent<MeshRenderer>().material = lightMats[0];
        lightOn = false;
    }

    public void LightOn() 
    {
        this.GetComponent<MeshRenderer>().material = lightMats[chosenIndex];
        lightOn = true;
    }

    public void LightOff() 
    {
        this.GetComponent<MeshRenderer>().material = lightMats[0];
        lightOn = false;
    }
}
