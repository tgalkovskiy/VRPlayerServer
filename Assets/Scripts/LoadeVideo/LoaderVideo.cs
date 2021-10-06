
using System;
using System.Collections.Generic;
using System.IO;
using SFB;
using UnityEngine;
using Random = UnityEngine.Random;

public class LoaderVideo : MonoBehaviour
{
    public languagesEnum _LanguagesEnum;
    [SerializeField] private Sprite[] _envelope = default;
    [SerializeField] private GameObject _content = default;
    [SerializeField] private GameObject _cellVideo = default;
    private MenuBehavior _menuBehavior;
    private List<GameObject> _allVideo = new List<GameObject>();
    private List<GameObject> _engVideoPath = new List<GameObject>();
    private List<GameObject> _herbVideoPath = new List<GameObject>();
    private string _videoList;
    public string _path;
    public string _Name;
    private void Awake()
    {
        _menuBehavior = GetComponent<MenuBehavior>();
        LoadVideo();
    }
    public void LoadVideo()
    {
        ClearCellVideo();
        if (File.Exists(Path.Combine(Application.persistentDataPath, "ListVideo.Json")))
        {
            WWW listVideo = new WWW(Path.Combine(Application.persistentDataPath, "ListVideo.Json"));
            _videoList = listVideo.text;
            string[] _dataList = _videoList.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < _dataList.Length; i++)
            {
                _menuBehavior.path.Add(Path.Combine(Application.persistentDataPath, _dataList[i]));
                var cell = Instantiate(_cellVideo, _content.transform);
                _allVideo.Add(cell);
                cell.GetComponent<VideoCell>()
                    .SetParametrsCell(_envelope[Random.Range(0, _envelope.Length)], i, _dataList[i]);
                if (_dataList[i].Contains("ENG"))
                {
                    _engVideoPath.Add(cell);
                }
                if (_dataList[i].Contains("HERB"))
                {
                    _herbVideoPath.Add(cell);
                }
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
        {
            _path = path;
            int index = path.LastIndexOf("\\");
            string _name = path.Substring(index+1);
            _Name = _name;
            File.Copy(path, Path.Combine(Application.persistentDataPath, _name));
            _videoList += $"{_name} ";
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "ListVideo.Json"), _videoList);
            LoadVideo();
        }
    }

    public void SendData()
    {
        GetComponent<DataManager>().SendDataFile(_path, _Name);
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
