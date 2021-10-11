using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CountDownTime : MonoBehaviour
{
    [Header("Insert the UI element")]
    public TMP_Text timerText;

    [Header("Insert the UI element")]
    [SerializeField]
    private Slider slider;

    private float startTime = LevelsSettingsManager.TimeToEnd * 60;

    //[HideInInspector]
    public float timer;

    private IEnumerator Timer()
    {
        timer = startTime;

        do
        {
            timer -= Time.deltaTime;
            slider.value = timer / startTime;

            FormatText();

            yield return null;
        } 
        while (timer > 0);
    }

    private void FormatText()
    {

        int days = (int)(timer / 86400) % 365;
        int hours = (int)(timer / 3600) % 24;
        int minutes = (int) (timer / 60) % 60;
        int seconds = (int)(timer % 60);


        timerText.text = " ";
        if (days > 0) { timerText.text += days + " - םימי" + "\n"; }
        if (hours > 0) { timerText.text += hours + " - תועש" + "\n"; }
        if (minutes > 0) { timerText.text += minutes + " - תוקד" + "\n"; }
        if (seconds > 0) { timerText.text += seconds + " - תוינש" + "\n"; }

        //timerText.text = " ";
        //if (days > 0) { timerText.text += "םימי - " + days; }
        //if (hours > 0) { timerText.text += "תועש - " + hours; }
        //if (minutes > 0) { timerText.text += "תוקד - " +minutes; }
        //if (seconds > 0) { timerText.text += "תוינש - " + seconds; }
    }

    private void Start()
    {
        startTime = LevelsSettingsManager.TimeToEnd * 60;
    }

    public void StartTimer()
    {
        Debug.Log("The timer is set to - " + LevelsSettingsManager.TimeToEnd);
        Debug.Log("The timer Calculate is set to - " + LevelsSettingsManager.TimeToEnd * 60);
        StartCoroutine(Timer());
    }

}
