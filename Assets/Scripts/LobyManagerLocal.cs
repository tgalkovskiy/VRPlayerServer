
using UnityEngine;
using Mirror;

public class LobyManagerLocal : NetworkManager
{
    [SerializeField] private MenuBehavior _menuBehavior = default;
    [SerializeField] private bool isServer = default;
    private void Awake()
    {
        
    }
    public void OfflineStart()
    {
        if (isServer)
        {
            _menuBehavior.ShowControlMenu();
        }
        else
        {
            _menuBehavior.UnShowControllMenu();
        }
    }
    public void SendMessageToAll(string command)
    {
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "sdvsdv"});
    }
    public void CommandPlay()
    {
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Play"});
    }
    public void CommandStop()
    {
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Stop"});
    }
    public void CommandMuteAudio()
    {
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Mute"});
    }
    public void CommandRebootVideo()
    {
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Reboot"});
    }
    public void CommandVideo(int index)
    {
       NetworkServer.SendToAll(new MirrorTransport.NumberVideo() {number = index});
    }
    
    
}
