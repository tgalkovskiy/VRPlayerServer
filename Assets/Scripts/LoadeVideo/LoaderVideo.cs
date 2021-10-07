
using System;
using System.Collections.Generic;
using System.IO;
using SFB;
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
    
    static string libPath = "lib";
    private void Awake()
    {
        var filename = libPath;
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
            cell.SetParametersCell(_envelope.RandomElement(ZergRandom.global), item.fileName, item.description);
            cell.connections += cell.selected.Subscribe(() => MenuBehavior.Instance.state.playingItem.value = item);
        });
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) OnApplicationQuit();
    }

    void OnApplicationQuit()
    {
        library.SaveToFile("lib", true);
    }

    public static string GetFillVideoPath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, $"{fileName}.mp4");
    }

    public void OpenFile()
    {
        var extensions = new [] {  //какие файлы вообще можно открыть
            new ExtensionFilter("Move Files", "mp4"),
            new ExtensionFilter("All Files", "*" ),
        };
        
        foreach(string path in StandaloneFileBrowser.OpenFilePanel("Add File", "", extensions, true))
        {
            var name = Path.GetFileNameWithoutExtension(path);
            File.Copy(path, GetFillVideoPath(name));
            library.library.Add(new VideoItem {
                id = new GUI().ToString(),
                fileName = name
            });
        }
    }
    
    public void SendData()
    {
        //GetComponent<DataManager>().SendDataFile(_path, _Name);
    }
}
