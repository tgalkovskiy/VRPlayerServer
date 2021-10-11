using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneMenu
{
    [MenuItem("Scenes/MainMenu")]
    public static void OpenMenu()
    {
        OpenScene("MainMenu");
    }


    [MenuItem("Scenes/FirstLevel")]
    public static void OpenFirstLevel()
    {
        OpenScene("FirstLevel");
    }

    [MenuItem("Scenes/SecondLevel")]
    public static void OpenSecondLevel()
    {
        OpenScene("SecondLevel");
    }

    [MenuItem("Scenes/ThirdLevel")]
    public static void OpenThirdLevel()
    {
        OpenScene("ThirdLevel");
    }

    private static void OpenScene(string sceneName)
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Loading/Persistent.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity", OpenSceneMode.Additive);
    }
}