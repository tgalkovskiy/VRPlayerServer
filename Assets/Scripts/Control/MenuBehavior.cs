using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{ 
    [SerializeField] private GameObject _canvasControl = default; 
    [SerializeField] private GameObject _pico = default;
    [SerializeField] private Text _listDevise = default;
    [SerializeField] private MediaPlayer _mediaPlayer = default;
    public List<string> path = new List<string>();
    private bool mute = false;
    private LoaderVideo _loaderVideo;
    public static MenuBehavior Instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
         Instance = this;
         _loaderVideo = GetComponent<LoaderVideo>();
    }
    public void ControlVideo(string command)
   {
        switch (command)
        {
            case "Play": _mediaPlayer.Play(); break;
            case "Stop": _mediaPlayer.Stop(); break;
            case "Mute":  mute = !mute; _mediaPlayer.AudioMuted = mute; break;
            case "Reboot": _mediaPlayer.Rewind(true); break;
        }
   } 
    public void ChooseVideo(string nameVideo)
   {
       _mediaPlayer.OpenMedia(MediaPathType.AbsolutePathOrURL, Path.Combine(Application.persistentDataPath, nameVideo), false);
   }
    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
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
   public void UnShowControlMenu()
   {
        //_pico.SetActive(true);
        _mediaPlayer.gameObject.SetActive(true);
   }

   public void GetData(byte[] data, string format, string name)
   {
       Debug.Log("Get file" + data.Length);
       File.WriteAllBytes(Path.Combine(Application.persistentDataPath, $"{name}.mp4"), data);
       Debug.Log("save file");
       File.WriteAllText(Path.Combine(Application.persistentDataPath, "ListVideo.Json"), name);
       Debug.Log("save Json");
       Debug.Log("Update Video");
       _loaderVideo.LoadVideo();
       //AssetDatabase.Refresh();
   }
}
