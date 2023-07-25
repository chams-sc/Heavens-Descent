using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    private int noOfKills;
    [SerializeField]
    private TMP_Text killsText;

    void Start()
    {
        noOfKills = 0;
    }
    void Update()
    {
        Debug.Log(noOfKills);
        DisplayKill();
    }

    public void AddKill()
    {
        noOfKills += 1;
    }

    private void DisplayKill()
    {
        killsText.text = "Score: " + noOfKills.ToString();
    }

    public int getScore()
    {
        return noOfKills;
    }
}
