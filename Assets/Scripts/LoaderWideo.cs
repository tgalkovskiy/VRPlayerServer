using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoaderWideo : MonoBehaviour
{
    [SerializeField] private Sprite[] _envelope = default;
    [SerializeField] private GameObject _content = default;
    [SerializeField] private GameObject _prefabVideoCell = default;
    public List<VideoCell> _cell = new List<VideoCell>();
    PlayerNetBehavior _playerNetBehavior;
    private void Awake()
    {
        BetterStreamingAssets.Initialize();
        _playerNetBehavior = GetComponent<PlayerNetBehavior>();
        string[] paths = BetterStreamingAssets.GetFiles("/", "*.mp4", SearchOption.AllDirectories);
        bool a = BetterStreamingAssets.FileExists("/BigBuckBunny-360p30-H264.mp4");
        //DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        //FileInfo[] info = dir.GetFiles("*.*");
        for(int i=0; i<paths.Length; i++)
        {
            _playerNetBehavior.path.Add(paths[i]);
        }
        for(int i=0; i< _playerNetBehavior.path.Count; i++)
        {
            var cell = Instantiate(_prefabVideoCell, _content.transform);
            //_cell.Add(_content.transform.GetChild(i).GetComponent<VideoCell>());
            //_cell[i].SetParamertsCell(_envelope[Random.Range(0, _envelope.Length)], i, _playerNetBehavior.path[i]);
        }
        
    }
    
}
