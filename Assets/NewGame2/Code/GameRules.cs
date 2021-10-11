using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : LevelRuler
{
    public NumberToMod Age;
    public NumberToMod timeToEndLevel;
    public NumberToMod amountToEndLevel;


    public bool setRulesOnStart;
    // Start is called before the first frame update
    void Start()
    {
        if (setRulesOnStart)
        {
            SetRules();
        }
    }

    public void SetRules()
    {
        SetAge(Age);
        if (timeToEndLevel != null)
        {
            SetTimeToEnd(timeToEndLevel);
        }
        SetAmountToEnd(amountToEndLevel);
    }
}
