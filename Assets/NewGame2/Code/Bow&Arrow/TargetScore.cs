using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetScore : MonoBehaviour
{
    public TMP_Text scoreText;
    //public TMP_Text toComplete;

    public int Multiplyer;
    public static int hitCount;
    // Start is called before the first frame update
    void Start()
    {
        hitCount = 0;
        //toComplete.text = (LevelsSettingsManager.ToComplete * multiplyer).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = (hitCount * Multiplyer).ToString();

        if (hitCount * Multiplyer >= LevelsSettingsManager.ToComplete * Multiplyer)
        {
            SceneLoader.Instance.LoadNewScene("MainMenu");
        }
    }
}
