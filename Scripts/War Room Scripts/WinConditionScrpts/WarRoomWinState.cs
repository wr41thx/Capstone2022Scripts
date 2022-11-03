using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic Trigger for War Room Win State
public class WarRoomWinState : MonoBehaviour
{
    [SerializeField] private GameObject exitDoor;
    [SerializeField] private GameObject spotlight;

    public void ActivateExitDoor() 
    {
        exitDoor.layer = 10;
    }

    public void ActivateExitDoorLight() 
    {
        spotlight.SetActive(true);
    }

}
