using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string pathToFileString;
    private void Start()
    {
        System.Diagnostics.Process.Start(pathToFileString);
    }
}
