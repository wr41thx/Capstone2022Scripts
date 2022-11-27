using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls all of the lighting effects when the generator is switched on
public class GR_LightController : MonoBehaviour
{
    [SerializeField] private GameObject[] lightsArray;
    public void ActivateGeneratorLights() 
    {
        foreach (GameObject light in lightsArray) 
        {
            light.GetComponent<GenRoomLight>().ColorToggle();
        }
    }
}
