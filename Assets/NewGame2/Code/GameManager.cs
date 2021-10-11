using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Scene control")]
    //public KeyCode quit;
    public static bool maleGender = true;

    [Header("Debug values DON'T TOUCH")]
    public bool isMale;

    // Update is called once per frame
    void Update()
    {
        isMale = maleGender;
        //if (Input.GetKeyDown(quit))
        //{
        //    EndGame();
        //}
    }

    public void EndGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
