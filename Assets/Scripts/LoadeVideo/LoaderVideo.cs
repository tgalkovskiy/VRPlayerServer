using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoaderVideo : MonoBehaviour
{
    public languagesEnum _LanguagesEnum;
    [SerializeField] private Sprite[] _envelope = default;
    [SerializeField] private GameObject _content = default;
    [SerializeField] private GameObject _cellVideo = default;
    MenuBehavior _menuBehavior;
    private List<GameObject> _engVideoPath = new List<GameObject>();
    private List<GameObject> _herbVideoPath = new List<GameObject>();
    private void Awake()
    {
        BetterStreamingAssets.Initialize();
        _menuBehavior = GetComponent<MenuBehavior>();
        string[] paths = BetterStreamingAssets.GetFiles("/", "*.mp4", SearchOption.AllDirectories);
        for(int i=0; i<paths.Length; i++)
        {
            _menuBehavior.path.Add(paths[i]);
            var cell = Instantiate(_cellVideo, _content.transform);
            cell.GetComponent<VideoCell>()
                .SetParamertsCell(_envelope[Random.Range(0, _envelope.Length)], i, _menuBehavior.path[i]);
            if (paths[i].Contains("ENG"))
            {
               _engVideoPath.Add(cell);
            }
            if (paths[i].Contains("HERB"))
            {
                _herbVideoPath.Add(cell);
            }
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
