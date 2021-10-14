using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZergRush;
using ZergRush.ReactiveCore;

public class ClientController : MonoBehaviour
{
    [SerializeField] private MediaPlayer _mediaPlayer = default;

    public static ClientController Instance;
    public ClientState state = new ClientState();
    public INetwork network;

    bool syncInProcess => syncFiles.Count > 0;
    List<string> syncFiles = new List<string>();
    Coroutine syncCoro;

    void Awake()
    {
        Instance = this;
    }

    public void OnConnected(INetwork net)
    {
        Debug.Log("Init client controller");
        _mediaPlayer.SetActiveSafe(true);
        network = net;
        network.commandReceived.Subscribe(c =>
        {
            Debug.Log("Client command received");
            switch (c)
            {
                case ClientState st : state.UpdateFrom(st); OpenVideo(); break;
                case SendDataFile data : SaveDataFile(data.data, data.format, data.name); break;
                case NumberSceneOpen n : OpenScene(n.numberScene); break;
                case VideoSyncList videoSync : 
                    if (syncCoro != null) StopCoroutine(syncCoro);
                    syncCoro = StartCoroutine(SyncVideos(videoSync));
                    break;
            }
        });
        state.BindToPlayer(_mediaPlayer);
        Debug.Log($"DeviceInfo sent battery level: {SystemInfo.batteryLevel}");
        StartCoroutine(UpdateDevice());
    }
    
    public void SaveDataFile(byte[] data, string format, string name)
    {
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, $"{name}.{format}"), data);
    }

    IEnumerator UpdateDevice()
    {
        while (true)
        {
            SendDeviceInfo();
            yield return new WaitForSeconds(3);
        } 
    }

    void SendDeviceInfo()
    {
        network.SendCommand(new DeviceInfo { name = SystemInfo.deviceName,
            battery = (int)SystemInfo.batteryLevel,
            connection = "good",
            syncInProcess = syncInProcess
        });
    }

    public IEnumerable<string> RequiredFiles(VideoItem item)
    {
        yield return item.fileName;
        if (item.soundFilename.IsNullOrEmpty() == false) yield return item.soundFilename;
        if (item.subtitlesFileName.IsNullOrEmpty() == false) yield return item.subtitlesFileName;
    }
    
    public IEnumerator SyncVideos(VideoSyncList videoSync)
    {
        syncFiles = videoSync.items.SelectMany(RequiredFiles).Where(path => File.Exists(LoaderVideo.GetFillVideoPath(path))).ToList();
        if (syncFiles.Count > 0)
        {
            SendDeviceInfo();
        }

        for (var i = syncFiles.Count - 1; i >= 0; i--)
        {
            var item = syncFiles[i];
            network.SendCommand(new NeedFile { fileName = item });
            yield return new WaitForEvent(network.commandReceived.Filter(c => c is SendDataFile));
            syncFiles.RemoveLast();
        }
        SendDeviceInfo();
        syncCoro = null;
    }
    
    public void OpenVideo()
    {
        state.BindToPlayer(_mediaPlayer);
    }
    public void OpenScene(int index)
    {
        _mediaPlayer.SetActiveSafe(index == 0);
        if (index == 0)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(index);
        }
    }
}