
using System;
using System.Collections.Generic;
using System.IO;
using SFB;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using ZergRush;
using ZergRush.ReactiveUI;
using Random = UnityEngine.Random;

public class LoaderVideo : ConnectableMonoBehaviour
{
    [SerializeField] private Sprite[] _envelope = default;
    [SerializeField] private GameObject _content = default;

    ServerLibrary library;
    
    private void Awake()
    {
        var filename = "lib";
        if (FileWrapper.Exists(filename))
        {
            SerializationTools.LoadFromBinaryFile(filename, out library);
        }
        else
        {
            library = new ServerLibrary();
        }
        //LoadVideo();
        connections += library.library.Present(_content.transform, PrefabRef<VideoCell>.Auto(), (item, cell) =>
        {
            cell.SetParamertsCell(_envelope.RandomElement(ZergRandom.global), item.fileName, item.description);
        });
    }
    public void OpenFile()
    {
        var extensions = new [] {  //какие файлы вообще можно открыть
            new ExtensionFilter("Move Files", "mp4"),
            new ExtensionFilter("All Files", "*" ),
        };
        
        foreach(string path in StandaloneFileBrowser.OpenFilePanel("Add File", "", extensions, true))
        {
            library.library.Add(new VideoItem {
                id = new GUI().ToString(),
                fileName = Path.GetFileName(path)
            });
        }
    }
    public void SendVideo()
    {
        //ClientController.Instance.
        // byte[] massByteToFile = File.ReadAllBytes(_path);
        // _Lobby.SendData(massByteToFile, ".mp4", _name.ToString());
        //File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath,"SavedVideo.mp4"), obj);
        //AssetDatabase.Refresh();
    }
}
