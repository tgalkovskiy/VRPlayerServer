using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public struct NetWorKDataPlay : NetworkMessage
{
    public string Data;
}

public struct NetWorKDataVideo : NetworkMessage
{
    public int Data;
}

public class NetManager : NetworkManager
{
    [SerializeField] private MediaPlayer _mediaPlayer =default;
    [SerializeField] private PlayerNetBehavior _playerNetBehavior = default;
    [SerializeField] private GameObject _canvas = default;
    public override void OnStartServer()
    {
        Debug.Log($"Start Server");
    }

    public override void OnStartClient()
    {
        Debug.Log($"Start Client");
        NetworkClient.RegisterHandler<NetWorKDataPlay>(GetDataPlay);
        NetworkClient.RegisterHandler<NetWorKDataVideo>(GetDataVideo);
        UnShowCanvasForClient();
    }

    private void SendDataPlay(string data)
    {
        var dataPlay = new NetWorKDataPlay()
        {
            Data = data
        };
        NetworkServer.SendToAll(dataPlay);
    }

    private void SendDataVideo(int data)
    {
        var dataVideo = new NetWorKDataVideo() {Data = data};
        NetworkServer.SendToAll(dataVideo);
    }

    private void GetDataPlay(NetWorKDataPlay dataPlay)
    {
        if (dataPlay.Data == "Play")
        {
            _mediaPlayer.Play();
        }
        if (dataPlay.Data == "Stop")
        {
            _mediaPlayer.Stop();
        }
    }

    private void GetDataVideo(NetWorKDataVideo dataVideo)
    {
        var _path = new MediaPath {_path = _playerNetBehavior.path[dataVideo.Data]};
        _mediaPlayer.OpenMedia(_path, false);
    }
    public void PlayVideo()
    {
        SendDataPlay("Play");
    }

    public void StopVideo()
    {
        SendDataPlay("Stop");
    }
    public void СhoiceVideo(int index)
    {
        SendDataVideo(index);
    }

    private void UnShowCanvasForClient()
    {
        _playerNetBehavior.UnShowObj(_canvas);
    }
    
}
