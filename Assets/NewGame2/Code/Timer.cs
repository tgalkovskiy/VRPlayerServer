using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Is that the level timer?")]
    public bool isLevelTimer;

    [Header("Insert the timer values")]
    public int timerTime = 3;

    [Header("Insert the UI element")]
    public TMP_Text timerText;

    [Header("Debug variables")]
    public bool isActive;
    public bool isAnswered;

    [Header("Choose the level type")]
    [SerializeField]
    private LevelType typeOfLevel;

    [Header("Insert the manager if needed")]
    public GameObject manager;

    [HideInInspector]
    public float elapsedTime;

    public enum LevelType
    {
        None,
        First,
        Second,
        Third
    }

    private void Start()
    {
        if (manager == null)
        {
            manager = GameObject.Find("SceneManager");
        }
    }

    public void StartTimer(float waitTime)
    {
        StartCoroutine(TimerCoroutine(waitTime));
    }

    public void StopTimer()
    {
        isAnswered = true;
        elapsedTime = 0;
    }

    IEnumerator TimerCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        elapsedTime = timerTime;

        while (elapsedTime > 0)
        {
            timerText.fontSize = 2.0f;
            timerText.text = elapsedTime.ToString();

            yield return new WaitForSeconds(1.0f);
            elapsedTime--;
        }
        // When timer is end
        if (isLevelTimer)
        {
            yield return new WaitForSeconds(0.5f);

            timerText.gameObject.SetActive(false);
        }
        else
        {
            if (!isAnswered)
            {
                timerText.fontSize = 0.4f;
                timerText.text = "לא נורא נסה שוב";

                switch (typeOfLevel)
                {
                    case LevelType.None:
                        break;
                    case LevelType.First:
                        manager.GetComponent<LevelManager>().RoundReset(0.0f);
                        break;
                    case LevelType.Second:
                        manager.GetComponent<WordsLevelManager>().RoundReset(0.0f);
                        break;
                    case LevelType.Third:
                        break;
                    default:
                        break;
                }

                
            }
            else
            {
                isAnswered = false;
            }
        }
    }
}
