using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Activates DEFCON poster spotlight
public class DefConLight : MonoBehaviour
{

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void TurnOn() 
    {
        gameObject.SetActive(true);
    }
}
