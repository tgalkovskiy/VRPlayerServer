using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.UI;
using System;

public class MenuBehavior : MonoBehaviour
{ 
    [SerializeField] private GameObject _canvasControl = default; 
    [SerializeField] private GameObject _pico = default;
    [SerializeField] private Text _listDevise = default;
    [SerializeField] private MediaPlayer _mediaPlayer = default;
    public List<string> path = new List<string>();
    private bool mute = false;
    public static MenuBehavior Instance;

    private void Awake()
    {
         Instance = this;
    }

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
        Debug.Log(index);
        _mediaPlayer.OpenMedia(MediaPathType.RelativeToStreamingAssetsFolder, path[index], false);
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
    public void ShowControlMenu()
   {
        _canvasControl.SetActive(true);
        _mediaPlayer.gameObject.SetActive(true);
   }
    //for client
   public void UnShowControllMenu()
   {
        //_pico.SetActive(true);
        _mediaPlayer.gameObject.SetActive(true);
   }
    
}
