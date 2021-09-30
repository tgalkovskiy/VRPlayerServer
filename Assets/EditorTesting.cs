using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorTesting : MonoBehaviour
{

    public GameObject[] editorActive;
    public GameObject[] editorPassive;

    private void Awake()
    {
        foreach (GameObject g in editorActive)
            g.SetActive(Application.isEditor);

        foreach (GameObject g in editorPassive)
            g.SetActive(!Application.isEditor);
    }

}
