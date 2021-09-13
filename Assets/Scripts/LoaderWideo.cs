using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoaderWideo : MonoBehaviour
{
    PlayerNetBehavior _playerNetBehavior;

    private void Awake()
    {
        _playerNetBehavior = GetComponent<PlayerNetBehavior>();
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] info = dir.GetFiles("*.*");
        for(int i=0; i<info.Length; i++)
        {
            if (!info[i].Name.EndsWith(".meta"))
            {
                _playerNetBehavior.path.Add(info[i].Name);
            } 
        }
    }
}
