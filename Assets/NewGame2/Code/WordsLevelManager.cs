using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordsLevelManager : MonoBehaviour
{
    public CountDownTime timerToEndLevel;
    public Timer levelTimer;
    public Timer roundTimer;
    public TargetStrings targets;
    public ScoreSystem score;
    public ButtonObject SceneMenu;
    [Header("Insert the UI element")]
    public GameObject amountCounter;
    public TMP_Text amountText;

    [Header("Insert the buttons father")]
    public ButtonsAnswer buttons;

    [Header("Insert the number of the rounds")]
    public int rounds;

    [Header("Debug values")]
    public int roundCounter = 0;

    public bool isStart;
    public bool isEnded;

    public void PlayLevel()
    {
        if (!roundTimer.isActive)
        {
            if (LevelsSettingsManager.byTime)
            {
                amountCounter.SetActive(false);
                score.time.gameObject.SetActive(false);
                timerToEndLevel.gameObject.SetActive(true);
                timerToEndLevel.StartTimer();
                isStart = true;
            }
            if (!LevelsSettingsManager.byTime)
            {
                score.timeTitle.gameObject.SetActive(false);
                amountCounter.SetActive(false);
                //amountText.text = LevelsSettingsManager.ToComplete.ToString();
            }
            levelTimer.gameObject.SetActive(true);
            levelTimer.StartTimer(1.0f);
            roundTimer.gameObject.SetActive(true);
            roundTimer.isActive = true;
            RoundReset(4.0f);
        }
        else
        {
            return;
        }
    }

    public void RoundReset(float waitTime)
    {
        StartCoroutine(RestartRound(waitTime));
    }

    public IEnumerator RestartRound(float waitTime)
    {
        roundCounter++;
        Debug.Log("The round is: " + roundCounter);
        yield return new WaitForSeconds(waitTime);
        if (roundCounter == 1)                      // If it's the first round
        {
            targets.RandValue();                    // Random the value of targets

            //targets.CleanWords(0.2f);
            targets.ImageSet(0.2f);                 // Set the new mood images

            targets.SetPosition(1.0f);              // Set the new position of the targets
            targets.ShuffleWords(1.0f);             // Set the new words 

            targets.RotateImages(2.0f);             // Reveale the targets

            targets.Flip(3.0f);                     // Open the targets lid                 
            buttons.RandomAnswer(3.0f);             // Set the answer buttons numbers
            roundTimer.StartTimer(3.0f);            // Start timer of the round
        }
        else                                        // If it's not the first round
        {
            targets.RandValue();                    // Random the value of targets
            targets.RotateImages(0.0f);             // Hide the targets
            targets.Flip(0.0f);                     // Close the targets lid

            targets.ImageSet(0.5f);                 // Set the new mood images

            targets.SetPosition(1.0f);              // Set the new position of the targets
            targets.ShuffleWords(1.0f);             // Set the new words

            targets.RotateImages(2.0f);             // Reveale the targets

            targets.Flip(3.0f);                     // Open the targets lid                 
            buttons.RandomAnswer(3.0f);             // Set the answer buttons numbers
            roundTimer.StartTimer(3.0f);            // Start timer of the round
        }
    }

    public void Answer(int objectNum)               // Get the player's answer and set the next round
    {
        if (roundTimer.elapsedTime > 0)
        {
            //Debug.Log("the player answer is: " + objectNum);
            //Debug.Log("the answer is: " + targets.CharachterNum);
            if (objectNum == targets.CharachterNum)
            {
                targets.winAudio.Play();
                score.AddPoints();
                roundTimer.timerText.fontSize = 0.4f;
                roundTimer.timerText.text = "כל הכבוד!";
            }
            else if (objectNum == 4)
            {
                Debug.Log("Oh you didnt make it");
            }
            else
            {
                targets.loseAudio.Play();
                score.DecPoints();
                roundTimer.timerText.fontSize = 0.4f;
                roundTimer.timerText.text = "לא נורא נסה שוב";
            }
            roundTimer.StopTimer();
            RoundReset(0.3f);
        }
        else
        {
            return;
        }
    }
    public void NextLevel()
    {
        SceneMenu.LoadGame("MainMenu");
    }

    private void Update()
    {
        if (LevelsSettingsManager.byTime)
        {
            if (isStart)
            {
                if ((timerToEndLevel.timer < 1) && (!isEnded))
                {
                    isEnded = true;
                    Invoke("NextLevel", 2.0f);
                }
            }
        }
        else
        {
            if (score.points >= LevelsSettingsManager.ToComplete)
            {
                Invoke("NextLevel", 2.0f);
            }
        }
    }
}
