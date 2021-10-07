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
    [SerializeField] private GameObject _pico = default;
    [SerializeField] private Text _listDevise = default;
    [SerializeField] private MediaPlayer _mediaPlayer = default;
    public List<string> path = new List<string>();
    private bool mute = false;
    private LoaderVideo _loaderVideo;
    public static MenuBehavior Instance;

    public ClientState state = new ClientState();
    public INetworkServer network;

    void Awake()
    {
         Instance = this;
    }

    public void Init(INetworkServer net)
    {
        this.network = net;
         _loaderVideo = GetComponent<LoaderVideo>();
         
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
   
    public void SendData(byte[] _data, string _format, string _name)
    {
        network.SendCommandAll(new SendDataFile{data = _data, format = _format, name = _name});
    }
    
    public void OpenScene(int index)
    {
        network.SendCommandAll(new NumberSceneOpen{numberScene = index});
    }
}