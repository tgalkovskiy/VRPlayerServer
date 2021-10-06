
using System.Collections.Generic;
using System.IO;
using SFB;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoaderVideo : MonoBehaviour
{
    public languagesEnum _LanguagesEnum;
    public LobbyManagerLocal _Lobby;
    [SerializeField] private Sprite[] _envelope = default;
    [SerializeField] private GameObject _content = default;
    [SerializeField] private GameObject _cellVideo = default;
    MenuBehavior _menuBehavior;
    private List<GameObject> _allVideo = new List<GameObject>();
    private List<GameObject> _engVideoPath = new List<GameObject>();
    private List<GameObject> _herbVideoPath = new List<GameObject>();
    private string _videoList;
    private string _path;
    private int _name;
    private void Awake()
    {
        _menuBehavior = GetComponent<MenuBehavior>();
        LoadVideo();
    }
    public void LoadVideo()
    {
        BetterStreamingAssets.Initialize();
        ClearCellVideo();
        if (File.Exists(Path.Combine(Application.persistentDataPath, "ListVideo.Json")))
        {
            WWW listVideo = new WWW(Path.Combine(Application.persistentDataPath, "ListVideo.Json"));
            _videoList = listVideo.text;
            //_videoList.
            Debug.Log(_path);
        }
        string[] allfiles = Directory.GetFiles(Application.persistentDataPath);
        for (int i = 0; i < allfiles.Length; i++)
        {
            _menuBehavior.path.Add(allfiles[i]);
            var cell = Instantiate(_cellVideo, _content.transform);
            _allVideo.Add(cell);
            cell.GetComponent<VideoCell>().SetParamertsCell(_envelope[Random.Range(0, _envelope.Length)], i, _menuBehavior.path[i]);
            if (allfiles[i].Contains("ENG"))
            {
                _engVideoPath.Add(cell);
            }
            if (allfiles[i].Contains("HERB"))
            {
                _herbVideoPath.Add(cell);
            }
        }
    }
    public void OpenFile()
    {
        var extensions = new [] {  //какие файлы вообще можно открыть
            new ExtensionFilter("Move Files", "mp4"),
            new ExtensionFilter("All Files", "*" ),
        };
        foreach(string path in StandaloneFileBrowser.OpenFilePanel("Add File", "", extensions, true))
        { //открытие формы для загрузки файла
            _path = path;
            _name = Random.Range(1, 1000);
            File.Copy(path, Path.Combine(Application.persistentDataPath, $"{_name}.mp4"));
            _videoList += $",{_name}";
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "ListVideo.Json"), _videoList);
            LoadVideo();
            //AssetDatabase.Refresh();
        }
    }
    public void SendVideo()
    {
        byte[] massByteToFile = File.ReadAllBytes(_path);
        _Lobby.SendData(massByteToFile, ".mp4", _name.ToString());
        //File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath,"SavedVideo.mp4"), obj);
        //AssetDatabase.Refresh();
    }
    private void ClearCellVideo()
    {
        for(int i = 0; i < _allVideo.Count; i++)
        {
            Destroy(_allVideo[i]);
        }
        _allVideo.Clear();
        _engVideoPath.Clear();
        _herbVideoPath.Clear();
        _menuBehavior.path.Clear();
        
    }
    public void ShowAll()
    {
        for(int i = 0; i < _allVideo.Count; i++)
        {
            _allVideo[i].SetActive(true);
        }
    }
    public void ShowEng()
    {
        for(int i = 0; i < _engVideoPath.Count; i++)
        {
            _engVideoPath[i].SetActive(true);
        }
        for(int i = 0; i < _herbVideoPath.Count; i++)
        {
            _herbVideoPath[i].SetActive(false);
        }
    }
    public void ShowHerb()
    {
        for(int i = 0; i < _engVideoPath.Count; i++)
        {
            _engVideoPath[i].SetActive(false);
        }
        for(int i = 0; i < _herbVideoPath.Count; i++)
        {
            _herbVideoPath[i].SetActive(true);
        } 
    }
}
