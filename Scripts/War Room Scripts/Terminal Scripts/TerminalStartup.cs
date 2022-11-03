using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Turns on all terminal effects when Generator Event is invoked
// and allows the Terminals to be interacted with.

public class TerminalStartup : MonoBehaviour
{
    [SerializeField] private GameObject[] btnArray;
    [SerializeField] private GameObject[] ledArray;
    [SerializeField] private GameObject keyboard;

    public void PowerOnTerminal() 
    {
        keyboard.GetComponent<KeyboardLogic>().TurnMonitorOn();

        foreach (GameObject btn in btnArray)
        {
            btn.GetComponent<TerminalBtnLogic>().EnableButton();
        }
        foreach(GameObject led in ledArray)
        {
            led.GetComponent<TermLights>().LightOn();
        }
    }

}
