using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

public class PlayerNetBehavior : MonoBehaviour
{
   [SerializeField] private GameObject _controlCanvasPanel = default;
   [SerializeField] private GameObject _selectorVideo = default;
   [SerializeField] private GameObject _BackPanel = default;
   [SerializeField] private GameObject _inputName = default;
   [SerializeField] private GameObject _LobyConector = default;
   [SerializeField] private MediaPlayer _mediaPlayer = default;
   public string[] path;
   private bool selector = true;
   public void GetCommand(string command)
   {
        if(command == "Play")
        {
            _mediaPlayer.Play();
        }
        if(command == "Stop")
        {
            _mediaPlayer.Stop();
        }
        if(command == "UnShow")
        {
            _controlCanvasPanel.SetActive(false);
        }
        if(command == "0")
        {
            _mediaPlayer.OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, path[0], false);
        }
        if (command == "1")
        {
            _mediaPlayer.OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, path[1], false);
        }
        if (command == "2")
        {
            _mediaPlayer.OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, path[2], false);
        }
        if (command == "3")
        {
            _mediaPlayer.OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, path[3], false);
        }
    }
    public void UnshowInputField()
    {
        _inputName.SetActive(false);
        _LobyConector.SetActive(true);
    }
    public void ShowControlVideo()
    {
        _controlCanvasPanel.SetActive(true);
        _LobyConector.SetActive(false);
        _BackPanel.SetActive(false);
    }
    public void ShowSelectWideo()
    {
        _selectorVideo.SetActive(selector);
        selector = !selector;
    }
}
