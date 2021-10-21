using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Mirror;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.UI;

public class ServerController : ConnectableMonoBehaviour
{
    [SerializeField] private GameObject _canvasControl = default;
    [SerializeField] private MediaPlayer _mediaPlayer = default;
    private bool mute = false;
    public List<string> path = new List<string>();
    private List<string> devises = new List<string>();
    public static ServerController Instance;
    public LobbyManagerLocal _Lobby;
    public DeviceListController deviceList;

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
        WindowControll.Instance.mute.Subscribe(() => state.mute.value = !state.mute.value);
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

        network.clientDisconnected.Subscribe(deviceList.DeviceDisconnected);
        network.commandReceived.Subscribe(c =>
        {
            Debug.Log($"client command received {c}");
            switch (c)
            {
                case DeviceInfo info: deviceList.DeviceInfoReceived(info); break;
                case NeedFile need: SendFile(need.connectionId, need.fileName); break;
            }
        });
    }

    void Update()
    {
        if (stateDirty)
        {
            stateDirty = false;
            videoLoader.selectedItems.Clear();
            Debug.Log($"client state sent {state.playingItem.value?.fileName}");
            videoLoader.selectedItems.Add(state.playingItem.value);
            Debug.Log(videoLoader.selectedItems.Count);
            SendCommandSelectedDevices(state);
        }
    }

    public void ShowControlMenu()
    {
        _canvasControl.SetActive(true);
        _mediaPlayer.gameObject.SetActive(true);
    }

    public void UnShowControlMenu()
    {
        //_pico.SetActive(true);
        _canvasControl.SetActive(false);
        _mediaPlayer.gameObject.SetActive(true);
    }

    public void DeleteVideoCategory()
    {
        videoLoader.DeleteCell();
    }

    public async void SendFile(int connectionId, string file)
    {
        /*var bytes = await Task.Run(() =>
        { 
        }); */
        Debug.Log("Send");
        byte[] massByteToFile =File.ReadAllBytes(LoaderVideo.GetFillVideoPath(file));
        //return massByteToFile;
        network.SendCommand(connectionId, new SendDataFile { data = massByteToFile, name = file });
    }
    
    public void SyncCall()
    {
        if (state.playingItem.value == null)
        {
            Debug.Log("No video selected");
            return;
        }
        SendCommandSelectedDevices(new VideoSyncList{items = {state.playingItem.value}});
    }

    void SendCommandSelectedDevices(NetworkCommand command)
    {
        foreach (var device in deviceList.selectedCategory.value.devices)
        {
            if (device.disconnected) continue;
            network.SendCommand(device.connectionId, command);
        }
    }

    public void OpenScene(int index)
    {
        state.playing.value = false;
        state.playingItem.value = null;
        SendCommandSelectedDevices(new NumberSceneOpen { numberScene = index });
    }
}