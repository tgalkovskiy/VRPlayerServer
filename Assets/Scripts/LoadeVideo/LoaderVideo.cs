using System;
using System.Collections.Generic;
using System.IO;
using SFB;
using UnityEngine;
using UnityEngine.UI;
using ZergRush;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;
using Random = UnityEngine.Random;

public class LoaderVideo : ConnectableMonoBehaviour
{
    [SerializeField] private Sprite[] _envelope = default;
    [SerializeField] private GameObject _content = default;

    ServerLibrary library;
    static string libPath = "lib";

    Cell<VideoCategory> selectedCat = new Cell<VideoCategory>();
    IReactiveCollection<LibraryItem> itemsToShow =>
        selectedCat.MapWithDefaultIfNull(c => c.items, library.library).Join();
    public ICell<bool> canGoBack => selectedCat.IsNot(null);

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

        connections += itemsToShow.Present(_content.transform, PrefabRef<VideoCell>.Auto(),
            (item, cell) =>
            {
                if (item is VideoItem vi)
                {
                    cell.SetParamertsCell(_envelope.RandomElement(ZergRandom.global), vi.fileName, vi.description);
                    cell.connections += cell.selected.Subscribe(() => MenuBehavior.Instance.state.playingItem.value = vi);
                }
                else if (item is VideoCategory cat)
                {
                    cell.SetParamertsCell(_envelope.RandomElement(ZergRandom.global), cat.name, "");
                    cell.connections += cell.selected.Subscribe(() =>
                    {
                        selectedCat.value = cat;
                    });
                }
            }, prefabSelector: item =>
            {
                if (item is VideoItem) return PrefabRef<VideoCell>.Auto();
                else if (item is VideoCategory) return PrefabRef<VideoCell>.Auto();
                else throw new NotImplementedException();
            }
        );
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

    public void GoBack()
    {
        selectedCat.value = null;
    }
    

    public void OpenFile()
    {
        var extensions = new[]
        {
            //какие файлы вообще можно открыть
            new ExtensionFilter("Move Files", "mp4"),
            new ExtensionFilter("All Files", "*"),
        };

        foreach (string path in StandaloneFileBrowser.OpenFilePanel("Add File", "", extensions, true))
        {
            var name = Path.GetFileNameWithoutExtension(path);

            var fillVideoPath = GetFillVideoPath(name);
            if (File.Exists(fillVideoPath))
            {
                File.Delete(fillVideoPath);
            }

            File.Copy(path, fillVideoPath);

            library.library.Add(new VideoItem
            {
                id = new GUI().ToString(),
                fileName = name
            });
        }
    }

    public void SendData()
    {
        MenuBehavior.Instance.SendData();
    }
}