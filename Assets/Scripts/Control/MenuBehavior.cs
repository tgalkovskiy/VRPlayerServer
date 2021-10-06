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
    [SerializeField] private Text _listDevise = default;
    [SerializeField] private MediaPlayer _mediaPlayer = default;
    private bool mute = false;
    public List<string> path = new List<string>();
    private List<string> devises = new List<string>();
    public static MenuBehavior Instance;
    public LobbyManagerLocal _Lobby;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
    }

    public void ControlVideo(string command)
    {
        switch (command)
        {
            case "Play":
                _mediaPlayer.Play();
                break;
            case "Stop":
                _mediaPlayer.Stop();
                break;
            case "Mute":
                mute = !mute;
                _mediaPlayer.AudioMuted = mute;
                break;
            case "Reboot":
                _mediaPlayer.Rewind(true);
                break;
        }
    }
    public void ChangeVolumePower(float power)
    {
        _mediaPlayer.AudioVolume = power;
    }
    public void ChooseVideo(string nameVideo)
    {
        _mediaPlayer.OpenMedia(MediaPathType.AbsolutePathOrURL, Path.Combine(Application.persistentDataPath, nameVideo),
            false);
    }

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void UpdateListDevise(List<string> devises)
    {
        string list = null;
        for (int i = 0; i < devises.Count; i++)
        {
            list += "\n" + devises[i];
        }
        _listDevise.text = list;
    }

    public void UpdateList(string name, int batteryLevel, string connection)
    {
        devises.Add($"{name} {batteryLevel} {connection}");
        UpdateListDevise(devises);
    }
    public void ShowControlMenu()
    {
        _canvasControl.SetActive(true);
        _mediaPlayer.gameObject.SetActive(true);
    }

    public void UnShowControlMenu()
    {
        //_pico.SetActive(true);
        _mediaPlayer.gameObject.SetActive(true);
    }
    
}
