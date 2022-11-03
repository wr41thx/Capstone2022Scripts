using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class CommandCenterController : MonoBehaviour
{
    // Controller for the middle terminal in the war room.
    // String array updates as each terminal is completed to show progress.

    [SerializeField] private Text monitorText;
    [SerializeField] private string[] terminalNames;
    private string offlineTxt = "<color=red>Offline</color>\n";
    private string onlineTxt = "<color=#4AF626>Online</color>\n";
    private string lowPowerModeStr = "Low Power Mode\n\nTerminals:\nOffline\n\nDefense Systems: Offline";
    private string statusText;
    private string statusTitle = "<color=#4AF626>Power Restored</color>\n";
    private int solveCount;
    [SerializeField] private UnityEvent allTermsSolved;

    private void Awake()
    {
        solveCount = 0;
        monitorText.text = lowPowerModeStr;       
    }

    // Concatenates offline text for each named terminal
    public void ShowPowerOnText() 
    {
        statusText += statusTitle;
        foreach (string terminalStr in terminalNames) 
        {
            statusText += terminalStr + offlineTxt;
        }

        monitorText.text = statusText;
    }

    // Terminals always report online status in order, regardless of which temrinal is solved.
    // The terminals are not numbered, so this should be fine.  
    private void UpdateText() 
    {  
        statusText = "";
        statusText += statusTitle;

        for (int i = 0; i < terminalNames.Length; i++)
        {
            if (i <= solveCount)
            {
                statusText += terminalNames[i] + onlineTxt;
            }
            else
            {
                statusText += terminalNames[i] + offlineTxt;
            }

        }
        monitorText.text = statusText;     
    }

    // When a terminal is solved, calls this function to update the main terminal
    // and the total number of solved terminals.  Invokes an event when all terminals are solved.
    // This event will enable the mini game display on central terminal.
    public void StatusUpdate() 
    {
        if (solveCount < 5) 
        {
            UpdateText();
            solveCount += 1;
        }

        if (solveCount == 5) 
        {
            allTermsSolved.Invoke();
        }
    }

}

