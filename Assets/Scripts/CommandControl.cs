using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandControl : MonoBehaviour
{
    [SerializeField] private LobyManagerLocal _managerLocal;
    private void Awake()
    {
        
    }

    public void Play()
    {
        if (ChooseTypeConnection.OnLine)
        {
            
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
            
        }
        else
        {
            _managerLocal.CommandVideo(index);
        } 
    }

    public void OpenScene(int index)
    {
        if (ChooseTypeConnection.OnLine)
        {
            
        }
        else
        {
            _managerLocal.OpenScene(index);
        } 
    }
}
