using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SFB;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using ZergRush;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;
using Random = UnityEngine.Random;

public class LoaderVideo : ConnectableMonoBehaviour
{
    [SerializeField] private Sprite[] _envelope = default;
    [SerializeField] private GameObject _contentVideo = default;
    [SerializeField] private GameObject _contentCat;

    ServerLibrary library;
    static string libPath = "lib";

    public IReactiveCollection<VideoCategory> allCategories => library.library
        .Filter(i => i is VideoCategory)
        .Map(i => (VideoCategory)i);

    public ReactiveCollection<VideoItem> selectedItems = new ReactiveCollection<VideoItem>(); 
    Cell<VideoCategory> selectedCat = new Cell<VideoCategory>();
    ICell<ReactiveCollection<LibraryItem>> currentCollection =>
        selectedCat.MapWithDefaultIfNull(c => c.items, library.library);
    IReactiveCollection<VideoItem> itemsToShow => currentCollection.Join().Filter(i => i is VideoItem)
        .Map(i => (VideoItem)i);
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
        connections += itemsToShow.Present(_contentVideo.transform, PrefabRef<VideoCell>.Auto(),
            (vi, cell) =>
            {
                
                    var view = (VideoCell)cell;
                    view.SetParametersCell(_envelope.RandomElement(ZergRandom.global), vi.fileName, vi.description);
                    cell.connections += view.selected.Subscribe(() =>
                    {
                        ServerController.Instance.state.playingItem.value = vi;
                    });
                    cell.SetSelection(false);
            }, options: PresentOptions.UseChildWithSameTypeAsView | PresentOptions.PreserveSiblingOrder
        );
        connections += allCategories.Present(_contentCat.transform, PrefabRef<CategoryCell>.Auto(),
            (cat, cell) =>
            {
                var view = (CategoryCell) cell;
                view.SetName(cat.name);
                cell.connections += view.selected.Subscribe(() => { selectedCat.value = cat; });
            }, options: PresentOptions.UseChildWithSameTypeAsView | PresentOptions.PreserveSiblingOrder);
    }

    void Start()
    {
        WindowControll.Instance.categoryBackButton.SetActive(canGoBack);
        WindowControll.Instance.categoryBackButton.Subscribe(GoBack);
        WindowControll.Instance.addPlayListButton.SetActive(selectedCat.Is(null));
        WindowControll.Instance.delete.onClick.AddListener(DeleteCell);
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
        return Path.Combine(Application.persistentDataPath, $"{fileName}");
    }

    public void GoBack()
    {
        selectedCat.value = null;
    }

    public void AddCategory(string catName, string description)
    {
        currentCollection.value.Insert(0, new VideoCategory{name = catName, description = description});
    }
    public void  DeleteCell()
    {
        foreach (var selectedItem in selectedItems)
        {
            if (currentCollection.value.Contains(selectedItem) == false)
            {
                Debug.LogError($"{selectedItem} is not in selected cat {currentCollection.value}");
            }
            currentCollection.value.Remove(selectedItem);
            DeleteLibraryItem(selectedItem);
        }
    }

    private void DeleteLibraryItem(LibraryItem item)
    {
        if (item is VideoItem i)
        {
            ServerController.Instance.state.playing.value = false;
            ServerController.Instance.state.playingItem.value = null;
            File.Delete(i.filePath);
        }
        else if (item is VideoCategory cat)
        {
            foreach (var libraryItem in cat.items)
            {
                DeleteLibraryItem(libraryItem);
            }
        }
    }
    
    
    public void OpenFile(string name, string description)
    {
        var extensions = new[]
        {
            //какие файлы вообще можно открыть
            new ExtensionFilter("Move Files", "mp4"),
            new ExtensionFilter("All Files", "*"),
        };

        foreach (string path in StandaloneFileBrowser.OpenFilePanel("Add File", "", extensions, true))
        {
            var ext = Path.GetExtension(path);
            var fileName = $"{name}{ext}";
            var fillVideoPath = GetFillVideoPath(fileName);
            if (File.Exists(fillVideoPath))
            {
                File.Delete(fillVideoPath);
            }
            File.Copy(path, fillVideoPath);
            currentCollection.value.Add(new VideoItem
            {
                id = new GUI().ToString(),
                fileName = fileName,
                description = description
            });
        }
    }

    public void SendData()
    {
        ServerController.Instance.SyncCall();
    }
}