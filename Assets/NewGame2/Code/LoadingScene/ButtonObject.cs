using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    public StartButton StartButtonRef;
    public void LoadGame(string sceneName)
    {
        SceneLoader.Instance.LoadNewScene(sceneName);
    }

    public void SetToButton(string sceneName)
    {
        StartButtonRef.sceneName = sceneName;
    }
}
