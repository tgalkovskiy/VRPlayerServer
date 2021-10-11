using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PressNumber : MonoBehaviour
{
    public WordsLevelManager manager;
    public int theAnswer;

    private void Start()
    {
        //Debug.Log("the button is: " + gameObject.transform.parent.name);
    }

    [ContextMenu("DoIt")]
    public void AnswerPress()
    {
        //Debug.Log("the button is: " + gameObject.transform.parent.name);

        manager.Answer(theAnswer);
    }
}
