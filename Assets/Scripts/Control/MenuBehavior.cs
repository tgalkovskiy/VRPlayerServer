using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEditor;

public class MenuBehavior : ConnectableMonoBehaviour
{ 
    [SerializeField] private GameObject _canvasControl = default;
    [SerializeField] private Text _listDevise = default;
    [SerializeField] private MediaPlayer _mediaPlayer = default;
    private bool mute = false;
    public List<string> path = new List<string>();
    private List<string> devises = new List<string>();
    public static MenuBehavior Instance;
    public LobbyManagerLocal _Lobby;

    public ClientState state = new ClientState();
    public INetworkServer network;

    void Awake()
    {
         Instance = this;
    }

    public void Init(INetworkServer net)
    {
        Application.targetFrameRate = 60;
        Instance = this;
        
        this.network = net;
         
         state.BindToPlayer(_mediaPlayer);
         state.updated.Subscribe(() => {
             network.SendCommandAll(state);
         });
         network.commandReceived.Subscribe(c =>
         {
             switch (c)
             {
             }
         });
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
   
    public void SendData(byte[] _data, string _format, string _name)
    {
        network.SendCommandAll(new SendDataFile{data = _data, format = _format, name = _name});
    }
    
    public void OpenScene(int index)
    {
        network.SendCommandAll(new NumberSceneOpen{numberScene = index});
    }
}
