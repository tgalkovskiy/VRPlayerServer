using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoaderWideo : MonoBehaviour
{
    [SerializeField] private Sprite[] _envelope = default;
    [SerializeField] private GameObject _content = default;
    public List<VideoCell> _cell = new List<VideoCell>();
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
        for(int i=0; i< _playerNetBehavior.path.Count; i++)
        {
            _cell.Add(_content.transform.GetChild(i).GetComponent<VideoCell>());
            _cell[i].SetParamertsCell(_envelope[Random.Range(0, _envelope.Length)], i, _playerNetBehavior.path[i]);
        }
        
    }
}
