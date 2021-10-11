using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberToMod : MonoBehaviour
{
    [Header("Deafault value")]
    public int ValueToSet;

    [Header("Set Jumps")]
    public int jumpAmount;

    private TextMeshProUGUI _valueText;
    public bool isAge;

    [Header("Set range")]
    public int maxNum;
    public int minNum;

    private void Start()
    {
        //Debug.Log("is first run is - " + LevelsSettingsManager.isRunedOnce);
        _valueText = GetComponent<TextMeshProUGUI>();

        if (isAge)
        {
            if (LevelsSettingsManager.isRunedOnce == false)
            {
                _valueText.text = ValueToSet.ToString();
                LevelsSettingsManager.isRunedOnce = true;
            }
            else
            {
                _valueText.text = LevelsSettingsManager.Age.ToString();
            }
        }
        else
        {
            _valueText.text = ValueToSet.ToString();
        }
    }

    [ContextMenu("AddValue")]
    public void AddValue()
    {

        if (ValueToSet <= maxNum)
        {
            ValueToSet += jumpAmount;
            _valueText.text = ValueToSet.ToString();
        }
    }

    [ContextMenu("SubtractValue")]
    public void SubtractValue()
    {

        if (ValueToSet > minNum)
        {
            ValueToSet -= jumpAmount;
            _valueText.text = ValueToSet.ToString();
        }
    }

    public void DebugerYair()
    {
        Debug.Log("Wtf1");
    }
}
