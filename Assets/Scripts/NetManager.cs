using System.Collections;
using System.Collections.Generic;
using Mirror;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public struct NetWorKData : NetworkMessage
{
    public string Data;
}

public class NetManager : NetworkManager
{
    [SerializeField] private MediaPlayer _mediaPlayer =default;
    //[SerializeField] private 
    public override void OnStartServer()
    {
        Debug.Log($"Start Server");
    }

    public override void OnStartClient()
    {
        Debug.Log($"Start Client");
        NetworkClient.RegisterHandler<NetWorKData>(GetData);
    }

    private void SendData(string data)
    {
        NetWorKData _data = new NetWorKData()
        {
            Data = data
        };
        NetworkServer.SendToAll(_data);
    }

    private void GetData(NetWorKData data)
    {
        if (data.Data == "Play")
        {
            _mediaPlayer.Play();
        }

        if (data.Data == "Stop")
        {
            _mediaPlayer.Stop();
        }
    }

    public void PlayVideo()
    {
        SendData("Play");
    }

    public void StopVideo()
    {
        SendData("Stop");
    }
    

    
}
