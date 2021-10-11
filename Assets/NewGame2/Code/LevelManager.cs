using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public  class LevelManager : MonoBehaviour
{
    public CountDownTime timerToEndLevel;
    public Timer levelTimer;
    public Timer roundTimer;
    public TargetManager targets;
    public ScoreSystem score;
    public ButtonObject SceneMenu;
    [Header("Insert the UI element")]
    public GameObject amountCounter;
    public TMP_Text amountText;

    [Header("Debug values")]
    public int roundCounter = 0;
    public bool isEnded;
    public bool isStart = false;

    public IEnumerator RestartRound(float waitTime)
    {
        roundCounter++;
        yield return new WaitForSeconds(waitTime);
        if (roundCounter == 1)                      // If it's the first round
        {
            targets.Clean(0.2f);                    // Reset the cubes images to blank
            targets.Shuffle(0.3f);                  // Shuffle the mood images and the object images
            targets.Flip(3.0f);                     // Rotate the cubes to object image
            roundTimer.StartTimer(3.0f);            // Start the round timer
        }
        else                                        // If it's not the first round
        {
            targets.Flip(0.0f);                     // Rotate the cubes to mood images
            targets.Clean(0.2f);                    // Reset the cubes images to blank
            targets.Shuffle(0.3f);                  // Shuffle the mood images and the object images
            targets.Flip(3.0f);                     // Rotate the cubes to object image
            roundTimer.StartTimer(3.0f);            // Start the round timer
        }
    }


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

    public void Answer(int objectNum)           // Get the player's answer and set the next round
    {
        if (roundTimer.elapsedTime > 0)
        {
            if (objectNum == targets.objectRandom)
            {
                targets.winAudio.Play();
                score.AddPoints();
                roundTimer.timerText.fontSize = 0.4f;
                roundTimer.timerText.text = "כל הכבוד!";
            }
            else if(objectNum == 4)
            {
                Debug.Log("Oh you didnt make it");
            }
            else
            {
                targets.loseAudio.Play();
                //score.DecPoints();
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
                //roundTimer.gameObject.SetActive(false);
                //targets.gameObject.SetActive(false);
                Invoke("NextLevel", 2.0f);
            }
        }
    }
}
