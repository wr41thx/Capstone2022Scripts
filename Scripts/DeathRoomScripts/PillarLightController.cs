using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarLightController : MonoBehaviour
{
    [SerializeField] private Light groundLight;
    [SerializeField] private Light lampLight;

    public void ChangeColor(Color newColor) 
    {
        groundLight.color = newColor;
        lampLight.color = newColor;
    }

}
