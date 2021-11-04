using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZergRush;
using ZergRush.ReactiveCore;
using DG.Tweening;

public class ClientController : MonoBehaviour
{
    [SerializeField] private MediaPlayer _mediaPlayer = default;
    [SerializeField] private Slider _LoadBar = default;
    [SerializeField] private Text _debugText = default;
    public static ClientController Instance;
    public ClientState state = new ClientState();
    public INetwork network;
    private List<byte> Data = new List<byte>();
    bool syncInProcess => syncFiles.Count > 0;
    List<string> syncFiles = new List<string>();
    Coroutine syncCoro;
    
    public string filePrefix = "";
    public static string persistentPathFilePrefix => Instance != null ? Instance.filePrefix : "";

    void Awake()
    {
        Instance = this;
    }

    /*private void Start()
    {
        byte[] mass = Encoding.ASCII.GetBytes("sdfvsdfs");
        WriteTextAsync(Path.Combine(Application.persistentDataPath, "TestWriter.mp4"), mass);
    }*/

    public void OnConnected(INetwork net)
    {
        _debugText.text += $"\nInit client controller";
        Debug.Log("Init client controller");
        _mediaPlayer.SetActiveSafe(true);
        network = net;
        network.commandReceived.Subscribe(c =>
        {
            Debug.Log($"Client command received {c}");
            _debugText.text += $"\nClient command received {c}";
            switch (c)
            {
                case ClientState st : state.UpdateFrom(st); OpenVideo(); break;
                case SendDataFile data : SaveDataFile(data.length ,data.data, data.name); break;
                case NumberSceneOpen n : OpenScene(n.numberScene); break;
                case VideoSyncList videoSync : 
                    if (syncCoro != null) StopCoroutine(syncCoro);
                    syncCoro = StartCoroutine(SyncVideos(videoSync));
                    break;
            }
        });
        state.BindToPlayer(_mediaPlayer);
        Debug.Log($"DeviceInfo sent battery level: {SystemInfo.batteryLevel}");
        _debugText.text += $"\nDeviceInfo sent battery level: {SystemInfo.batteryLevel}";
        StartCoroutine(UpdateDevice());
        SendDeviceInfo();
    }
    
    public void SaveDataFile(string length, byte[] data, string name)
    {
       // Sequence sequence = DOTween.Sequence().OnStart(()=>_LoadBar.gameObject.SetActive(true)).Append(
            //DOTween.To(() => _LoadBar.value, x => _LoadBar.value = x, 100, 2)).Play().OnComplete((() => _LoadBar.gameObject.SetActive(false)));
        Debug.Log($"get data {length}");
        _debugText.text += $"\nget data {length}";
        try
        {
            Debug.Log($"datacount {Data.Count}");
            _debugText.text += $"\ndatacount {Data.Count}";
            WriteTextAsync(LoaderVideo.GetFillVideoPath(name), data);
        }
        catch (Exception e)
        {
            Debug.Log($"{e}");
            _debugText.text += $"\n {e}";
            throw;
        }
        //File.WriteAllBytes(LoaderVideo.GetFillVideoPath(name), data);
            //AsyncWriter(data, name);
            /*else
            {
                Debug.Log($"Create massByte");
                Byte[] finalData = new Byte[Data.Count];
                for (int i = 0; i < finalData.Length; i++)
                {
                    Debug.Log($"Writer byte");
                    finalData[i] = Data[i];
                }
                Debug.Log($"finalize");
                File.WriteAllBytes(LoaderVideo.GetFillVideoPath(name), finalData); 
                Data.Clear();
            }*/
        /*if (File.Exists(name))
        {
            byte[] contentOld = File.ReadAllBytes(name);
            if (contentOld.Length < Convert.ToInt32(length))
            {
                //byte[] contentNew = new [contentOld.Length+]
            }
        }
        else
        {
           File.WriteAllBytes(LoaderVideo.GetFillVideoPath(name), data); 
        }*/
    }
 
    static async void WriteTextAsync(string name, byte[] data)
    {
        await Task.Run((() =>
        {
            using (FileStream stream = new FileStream(name, FileMode.Append))
            {
                stream.Write(data, 0, data.Length);
            }
        }));
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
            battery = (int)(SystemInfo.batteryLevel * 100),
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
        syncFiles = videoSync.items.SelectMany(RequiredFiles).Where(path => File.Exists(LoaderVideo.GetFillVideoPath(path)) == false).ToList();
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