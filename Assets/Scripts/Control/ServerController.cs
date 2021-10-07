using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEditor;

public class ServerController : ConnectableMonoBehaviour
{
    [SerializeField] private GameObject _canvasControl = default;
    [SerializeField] private Text _listDevise = default;
    [SerializeField] private MediaPlayer _mediaPlayer = default;
    private bool mute = false;
    public List<string> path = new List<string>();
    private List<string> devises = new List<string>();
    public static ServerController Instance;
    public LobbyManagerLocal _Lobby;

    public ClientState state = new ClientState();
    public INetworkServer network;

    public LoaderVideo videoLoader;

    bool stateDirty;

    void Awake()
    {
        Instance = this;
        videoLoader = GetComponent<LoaderVideo>();
    }

    public void Init(INetworkServer net)
    {
        Application.targetFrameRate = 60;
        Instance = this;

        this.network = net;
        
        WindowControll.Instance.volume.onValueChanged.AddListener(val => {
            state.volume.value = val;
        });
        WindowControll.Instance.play.Subscribe(() => state.playing.value = !state.playing.value);
        WindowControll.Instance.pause.Subscribe(() => state.playing.value = false);
        WindowControll.Instance.stop.Subscribe(() =>
        {
            state.playing.value = false;
            state.time.value = 0;
        });
        WindowControll.Instance.back.Subscribe(() =>
        {
            state.playingItem.value = null;
            state.time.value = 0;
            state.playing.value = false;
        });
        state.BindToPlayer(_mediaPlayer);
        state.updated.Subscribe(() => { stateDirty = true; });
        network.commandReceived.Subscribe(c =>
        {
            switch (c)
            {
                case DeviceInfo info: UpdateList(info.name, info.battery, info.connection); break;
            }
        });
    }

    void Update()
    {
        if (stateDirty)
        {
            stateDirty = false;
            network.SendCommandAll(state);
        }
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
        network.SendCommandAll(new SendDataFile { data = _data, format = _format, name = _name });
    }

    public void OpenScene(int index)
    {
        network.SendCommandAll(new NumberSceneOpen { numberScene = index });
    }
}