using System;
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
    private byte[] massByteToFile;
    public ClientState state = new ClientState();
    public INetworkServer network;

    public LoaderVideo videoLoader;
    bool stateDirty;
    private Slider _progress;
    private Text _progressPercent;
    void Awake()
    {
        Instance = this;
        videoLoader = GetComponent<LoaderVideo>();
    }
    private void Start()
    {
        _progress = WindowControll.Instance.progress;
        _progressPercent = WindowControll.Instance._progressPercent;
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

    public void SendFile(int connectionId, string name)
    {
        Debug.Log("Send");
        string file = LoaderVideo.GetFillVideoPath(name);
        ReadAsync(connectionId, file, name);
        //network.SendCommand(connectionId, new SendDataFile {data = ReadAsync(name).Result, name = file });
        /*using (FileStream SourceStream =new FileStream(name, FileMode.Open))
        {
            massByteToFile = new byte[SourceStream.Length];
            await SourceStream.ReadAsync(massByteToFile, 0, (int)SourceStream.Length);
        }
        Debug.Log($"{massByteToFile.Length}");*/
        //byte[] massByteToFile =File.ReadAllBytes(LoaderVideo.GetFillVideoPath(file));
        /*if (massByteToFile.Length > _Lobby._maxSizeFile)
        {
            var countPackage = (int) Math.Ceiling((decimal) massByteToFile.Length / (decimal) _Lobby._maxSizeFile);
            var bufferArray = new byte[countPackage][];
            for (var i = 0; i < countPackage; i++)
            {
                bufferArray[i] = new byte[Math.Min(_Lobby._maxSizeFile,massByteToFile.Length - i*_Lobby._maxSizeFile)];
                Debug.Log(bufferArray[i].Length);
                for (var j = 0; j < bufferArray[i].Length; j++)
                {
                    bufferArray[i][j] = massByteToFile[i * countPackage + j];
                }
                network.SendCommand(connectionId, new SendDataFile {length =massByteToFile.Length.ToString(), data = bufferArray[i], name = file });
            }
        }
        else
        {
           
        }#1#*/
    }
    private async Task ReadAsync(int ID, string file, string name)
    {
        Debug.Log("Start Async");
        using(FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            massByteToFile = new byte[stream.Length];
            await stream.ReadAsync(massByteToFile, 0, (int) stream.Length);
            int countPackage = (int)Math.Ceiling((decimal)massByteToFile.Length /1000000);
            int percent;
            _progress.gameObject.SetActive(true);
            _progress.maxValue = countPackage;
            for (int i = 0; i < countPackage; i++)
            {
                byte[] mass = new Byte[Math.Min(1000000, stream.Length - i * 1000000)];
                Debug.Log($"Send {i} length{mass.Length}");
                Array.Copy(massByteToFile, i * 1000000, mass, 0, mass.Length);
                _progress.value += 1;
                percent = (int) ((double) i / countPackage * 100);
                _progressPercent.text =$"{percent}%";
                network.SendCommand(ID, new SendDataFile {percent = percent, data = mass, name = name});
                await Task.Delay(500);
            }
            _progress.value = 0;
            _progress.gameObject.SetActive(false);
        }
        Debug.Log("Send Data");
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