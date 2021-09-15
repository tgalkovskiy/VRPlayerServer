using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.UI;
using System;

public class PlayerNetBehavior : MonoBehaviour
{
   [SerializeField] private GameObject _inputName = default;
   [SerializeField] private GameObject _MenuPanel = default;
   [SerializeField] private GameObject _LobyConector = default;
   [SerializeField] private GameObject _BackPanel = default;
   [SerializeField] private Text _listDevise = default;
   [SerializeField] private MediaPlayer _mediaPlayer = default;
   public List<string> path = new List<string>();
   private bool mute = false;
   public void ControllVideo(string command)
   {
        switch (command)
        {
            case "Play": _mediaPlayer.Play(); break;
            case "Stop": _mediaPlayer.Stop(); break;
            case "Mute":  mute = !mute; _mediaPlayer.AudioMuted = mute; break;
            case "Reboot": _mediaPlayer.Rewind(true); break;
        }
    }
   public void ChooseVideo(int index)
   {
        _mediaPlayer.OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, path[index], false);
        int a = 7;
        TimeSpan timeSpan = TimeSpan.FromSeconds(7);
        Debug.Log(timeSpan);
   }
   public void UpdateListDevise(List<string> device)
   {
        string list =null;
        for(int i =0;i<device.Count; i++)
        {
            list += "\n"+device[i];
        }
        _listDevise.text = list;
   }
    //for master
   public void UnshowInputField()
    {
        _inputName.SetActive(false);
        _LobyConector.SetActive(true);
    }
   public void ShowControlMenu()
    {
        _LobyConector.SetActive(false);
        _MenuPanel.SetActive(true);
    }
    //for client
   public void UnshowControllMenu()
    {
        _LobyConector.SetActive(false);
        _MenuPanel.SetActive(false);
        _BackPanel.SetActive(false);
    }
    
}
