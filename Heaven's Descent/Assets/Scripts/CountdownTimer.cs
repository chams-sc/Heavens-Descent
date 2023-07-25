using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    private float duration = 120f;
    private float timeRemaining;
    private bool timeIsRunning = true;
    [SerializeField]
    TMP_Text countdownText;

    void Start()
    {
        timeRemaining = duration;
        timeIsRunning = true;
    }

    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
        }
        else
        {
            timeRemaining = 0;
            timeIsRunning = false;
        }
    }

    void DisplayTime (float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        countdownText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
