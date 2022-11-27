using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{

    // Controls the mini game timer for the win condition.
    // Updates every frame using the mini game controller time.
    // Converts that time into seconds and displays the count down.

    [SerializeField] private GameObject timer;
    private int timerTime;

    private void Awake()
    {
        timer.GetComponent<TextMeshPro>().text = "";
    }

    private void Update()
    {
        DisplayTime();
    }

    public void SetTime(float gameTime) 
    {
        timerTime = (int)gameTime % 60;
    }

    private void DisplayTime() 
    {
        timer.GetComponent<TextMeshPro>().text = timerTime.ToString();
    }
}
