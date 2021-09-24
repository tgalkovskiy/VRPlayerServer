
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using Mirror;

public class LobyManagerLocal : NetworkManager
{
    [SerializeField] private MenuBehavior _menuBehavior = default;
    [SerializeField] private bool isServer;
    public static LobyManagerLocal Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OfflineStart()
    {
       
    }
    public void SendMessageToAll(string command)
    {
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "sdvsdv"});
    }
    private void ReceiveMessage()
    {
        
    }

    public void CommandPlay()
    {
        _menuBehavior.ControllVideo("Play");
        SendMessage("Play");
        
    }
    public void CommandStop()
    {
        _menuBehavior.ControllVideo("Stop");
        SendMessage("Stop");
    }
    public void CommandMuteAudio()
    {
        _menuBehavior.ControllVideo("Mute");
        SendMessage("Mute");
    }
    public void CommandRebootVideo()
    {
        _menuBehavior.ControllVideo("Reboot");
        SendMessage("Reboot");
    }
    public void CommandVideo(int index)
    {
        Debug.Log(1);
        _menuBehavior.ChooseVideo(index);
        SendMessage(index.ToString());
    }
    
    
}
