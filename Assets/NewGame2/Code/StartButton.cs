using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public string sceneName;
    public void LoadGame()
    {
        SceneLoader.Instance.LoadNewScene(sceneName);
    }
}
