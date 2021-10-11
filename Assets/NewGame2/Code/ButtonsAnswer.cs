using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonsAnswer : MonoBehaviour
{
    public TargetStrings targets;
    public List <TMP_Text> answers;
    public List<PressNumber> buttons;
    private int chosen;
    private bool isMinused;

    [HideInInspector]
    public int minusNum;
    [HideInInspector]
    public int plusNum;

    public void RandomAnswer(float waitTime)
    {
        StartCoroutine(DelayAnswer(waitTime));
    }

    public IEnumerator DelayAnswer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        isMinused = false;
        chosen = Random.Range(0, answers.Count);
        answers[chosen].text = targets.CharachterNum.ToString();
        buttons[chosen].theAnswer = targets.CharachterNum;
        for (int i = 0; i < answers.Count; i++)
        {
            if (i != chosen)
            {
                minusNum = targets.CharachterNum - 1;
                plusNum = targets.CharachterNum + 1;
                if (isMinused)
                {
                    buttons[i].theAnswer = plusNum;
                    answers[i].text = plusNum.ToString();
                }
                else
                {
                    isMinused = true;
                    buttons[i].theAnswer = minusNum;
                    answers[i].text = minusNum.ToString();
                }
            }
        }
    }
}
