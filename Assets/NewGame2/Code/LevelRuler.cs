using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRuler : MonoBehaviour
{
    public bool ShowTheGameRules;

    private void Update()
    {
        showDebugValues();
    }

    public void showDebugValues()
    {
        if (ShowTheGameRules)
        {
            Debug.Log("The Age is - " + LevelsSettingsManager.Age);
            Debug.Log(LevelsSettingsManager.TimeToEnd + " - Minutes to end level");
            Debug.Log(LevelsSettingsManager.ToComplete + " - Achivments to end level");
            Debug.Log("Is it by time? - " + LevelsSettingsManager.byTime);
            ShowTheGameRules = false;
        }
    }

    public void SetAge(NumberToMod numToSet)
    {
        LevelsSettingsManager.Age = numToSet.ValueToSet;   
    }

    public void SetTimeToEnd(NumberToMod numToSet)
    {
        LevelsSettingsManager.TimeToEnd = numToSet.ValueToSet;
    }

    public void SetAmountToEnd(NumberToMod numToSet)
    {
        LevelsSettingsManager.ToComplete = numToSet.ValueToSet;
    }

    public void ToggleEndRule(bool isTime)
    {
        if (isTime)
        {
            LevelsSettingsManager.byTime = true;
        }
        else
        {
            LevelsSettingsManager.byTime = false;
        }
    }
}
