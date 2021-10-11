using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public bool isByTime;

    public TMP_Text score;
    public TMP_Text timeTitle;
    public TMP_Text time;


    public float timeInSeconds = 0.0f;
    public int points;
    public int scoreMultiplyer;
    public Timer roundTimer;

    public void AddPoints()
    {
        if (isByTime)
        {
            if (roundTimer.elapsedTime == 3)
            {
                points += 3;
            }
            else if (roundTimer.elapsedTime == 2)
            {
                points += 2;
            }
            else if (roundTimer.elapsedTime == 1)
            {
                points += 1;
            }
        }
        else
        {
            points++;
        }


        timeInSeconds += (float) roundTimer.elapsedTime;

        time.text = timeInSeconds.ToString();       
        score.text = (points * scoreMultiplyer).ToString();
    }

    public void DecPoints()
    {
        #region(decrease ponts(test))
        /*
        if (roundTimer.elapsedTime == 3)
        {
            if (points > 3)
            {
                points -= 3;
            }
            else
            {
                points = 0;
            }
            
        }
        else if (roundTimer.elapsedTime == 2)
        {
            if (points > 2)
            {
                points -= 2;
            }
            else
            {
                points = 0;
            }
        }
        else if (roundTimer.elapsedTime == 1)
        {
            if (points > 1)
            {
                points -= 1;
            }
            else
            {
                points = 0;
            }
        }
        */
        #endregion

        timeInSeconds += (float)roundTimer.elapsedTime;

        time.text = timeInSeconds.ToString();
        //score.text = points.ToString();
    }
}
