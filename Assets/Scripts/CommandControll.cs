using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandControll : MonoBehaviour
{
    private LobyManagerLocal _managerLocal;
    private LobyManagerNetWork _lobyManagerNet;
    private void Awake()
    {
        _managerLocal = GetComponent<LobyManagerLocal>();
        _lobyManagerNet = GetComponent<LobyManagerNetWork>();
    }
    public void Play()
    {
        if (ChooseTypeConnection.OnLine)
        {
            _lobyManagerNet.CommandPlay();
        }
        else
        {
            _managerLocal.CommandPlay();
        }
    }
    public void Stop()
    {
        if (ChooseTypeConnection.OnLine)
        {
            _lobyManagerNet.CommandStop();
        }
        else
        {
            _managerLocal.CommandStop();
        }
    }
    public void Mute()
    {
        if (ChooseTypeConnection.OnLine)
        {
            _lobyManagerNet.CommandMuteAudio();
        }
        else
        {
            _managerLocal.CommandMuteAudio();
        }
    }
    public void RebootVideo()
    {
        if (ChooseTypeConnection.OnLine)
        {
            _lobyManagerNet.CommandRebootVideo();
        }
        else
        {
            _managerLocal.CommandRebootVideo();
        }
    }
    
    public void CommandVideo(int index)
    {
        if (ChooseTypeConnection.OnLine)
        {
            _lobyManagerNet.CommandVideo(index);
        }
        else
        {
            _managerLocal.CommandVideo(index);
        }
    }
}
