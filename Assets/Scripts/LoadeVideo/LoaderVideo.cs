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
    public IReactiveCollection<LibraryItem> itemsToShow => currentCollection.Join();
    public ReactiveCollection<LibraryItem> selectedItems = new ReactiveCollection<LibraryItem>(); 
    public Cell<VideoCategory> selectedCat = new Cell<VideoCategory>();
    public ICell<ReactiveCollection<LibraryItem>> currentCollection =>
        selectedCat.MapWithDefaultIfNull(c => c.items, library.library);
    /*IReactiveCollection<VideoItem> itemsToShow => currentCollection.Join().Filter(i => i is VideoItem)
        .Map(i => (VideoItem)i);*/
    public ICell<bool> canGoBack => selectedCat.IsNot(null);
    public List<VideoCell> _videoCells = new List<VideoCell>();
    public List<CategoryCell> _categoryCells = new List<CategoryCell>();

    //public LibraryItem RigthSelect;


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
        connections += itemsToShow.Present(_contentVideo.transform, PrefabRef<ReusableView>.Auto(),
            (item, cell) =>
            {
                if (item is VideoItem vi)
                {
                    var view = (VideoCell)cell;
                    _videoCells.Add(view);
                    view.SetParametersCell(vi.fileName, vi.description, vi.extImage);
                    cell.connections += view.selectedLeftMouse.Subscribe(() => ServerController.Instance.state.playingItem.value = vi);
                    cell.connections += view.selectedRigthMouse.Subscribe(() => selectedItems.Add(vi));
                }
                else if (item is VideoCategory cat)
                {
                    var view = (CategoryCell)cell;
                    _categoryCells.Add(view);
                    view.SetParameters(cat.name , cat.description, cat.extImage);
                    cell.connections += view.selectedLeftMouse.Subscribe(() => { selectedCat.value = cat;});
                    cell.connections += view.selectedRigthMouse.Subscribe(() => selectedItems.Add(cat));
                    
                    
                }
            }, prefabSelector: item =>
            {
                Debug.Log("update");
                if (item is VideoItem) return PrefabRef<ReusableView>.ByType(typeof(VideoCell));
                else if (item is VideoCategory) return PrefabRef<ReusableView>.ByType(typeof(CategoryCell));
                else throw new NotImplementedException();
            }, options: PresentOptions.UseChildWithSameTypeAsView | PresentOptions.PreserveSiblingOrder
        );
        /*connections += itemsToShow.Present(_contentVideo.transform, PrefabRef<VideoCell>.Auto(),
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
        );*/
        connections += allCategories.Present(_contentCat.transform, PrefabRef<CategoryCell>.Auto(),
            (cat, cell) =>
            {
                var view = (CategoryCell) cell;
                view.SetParameters(cat.name);
                cell.connections += view.selectedLeftMouse.Subscribe(() => { selectedCat.value = cat; });

            }, options: PresentOptions.UseChildWithSameTypeAsView | PresentOptions.PreserveSiblingOrder);
    }

    void Start()
    {
        WindowControll.Instance.categoryBackButton.SetActive(canGoBack);
        WindowControll.Instance.categoryBackButton.Subscribe(GoBack);
        WindowControll.Instance.addPlayListButton.SetActive(selectedCat.Is(null));
        //WindowControll.Instance.delete.onClick.AddListener(DeleteCell);
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
        return Path.Combine(Application.persistentDataPath, $"{ClientController.persistentPathFilePrefix}{fileName}");
    }

    public void GoBack()
    {
        selectedCat.value = null;
    }

    public void AddCategory(string catName, string description, string extImage)
    {
        string image = String.Empty;
        if (!extImage.IsNullOrEmpty())
        {
            image = $"{catName}{extImage}";
        }
        currentCollection.value.Insert(0,
            new VideoCategory {name = catName, description = description, extImage = image});
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
    
    
    public void OpenFile(string name, string description, string extImage, bool is2DVideo)
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
            var imageName = string.Empty;
            if (!extImage.IsNullOrEmpty())
            {
                imageName= $"{name}{extImage}"; 
            }
            var fillVideoPath = GetFillVideoPath(fileName);
            if (File.Exists(fillVideoPath))
            {
                File.Delete(fillVideoPath);
            }
            File.Copy(path, fillVideoPath);
            currentCollection.value.Add(new VideoItem
            {
                id = new GUI().ToString(),
                is2DVideo = is2DVideo,
                fileName = fileName,
                description = description,
                extImage = imageName
            });
        }
    }

    public void SendData()
    {
        ServerController.Instance.SyncCall();
    }
}