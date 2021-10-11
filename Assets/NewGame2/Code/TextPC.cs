using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPC : MonoBehaviour
{
    public string maleText;
    public string femaleText;

    private void Start()
    {
        Toggle();
    }

    public void PcString()
    {
        Toggle();
    }

    public void Toggle()
    {
        if (GameManager.maleGender)
        {
            gameObject.GetComponent<TMP_Text>().text = maleText;
        }
        else
        {
            gameObject.GetComponent<TMP_Text>().text = femaleText;
        }
    }
}
