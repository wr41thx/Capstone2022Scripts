using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Swap Materials and lighting for all lights that are triggered by
// solving the generator puzzle.

public class GenRoomLight : MonoBehaviour
{
    [SerializeField] private Material[] lightMatArr;
    [SerializeField] private GameObject pointLight;
    [SerializeField] private Color[] lightColorArr;

    private void Awake()
    {
        // index 0 should always be default light color
        this.GetComponent<MeshRenderer>().material = lightMatArr[0];
        pointLight.GetComponent<Light>().color = lightColorArr[0];
    }

    public void ColorToggle() 
    {
        this.GetComponent<MeshRenderer>().material = lightMatArr[1];
        pointLight.GetComponent<Light>().color = lightColorArr[1];
    }
}
