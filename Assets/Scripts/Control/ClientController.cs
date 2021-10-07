using System;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientController : MonoBehaviour
{
    [SerializeField] private MediaPlayer _mediaPlayer = default;

    public static ClientController Instance;
    public ClientState state = new ClientState();
    public INetwork network;

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
                case SendDataFile data : DataManager.Instance.SaveDataFile(data.data, data.format, data.name); break;
                case NumberSceneOpen n : OpenScene(n.numberScene); break;
                
            }
        });
        state.BindToPlayer(_mediaPlayer);
        network.SendCommand(new DeviceInfo
            { name = SystemInfo.deviceName, battery = (int)SystemInfo.batteryLevel, connection = "good" });
        Debug.Log("DeviceInfo sent");
    }
    
    public void OpenVideo()
    {
        state.BindToPlayer(_mediaPlayer);
    }
    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}