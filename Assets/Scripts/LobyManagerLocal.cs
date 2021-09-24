
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
            StartServer();
        }
        else
        {
            _menuBehavior.UnShowControllMenu();
            StartClient();
        }
    }
    public void SendMessageToAll(string command)
    {
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "sdvsdv"});
    }
    public void CommandPlay()
    {
        MenuBehavior.Instance.ControllVideo("Play");
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Play"});
    }
    public void CommandStop()
    {
        MenuBehavior.Instance.ControllVideo("Stop");
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Stop"});
    }
    public void CommandMuteAudio()
    {
        MenuBehavior.Instance.ControllVideo("Mute");
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Mute"});
    }
    public void CommandRebootVideo()
    {
        MenuBehavior.Instance.ControllVideo("Reboot");
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Reboot"});
    }
    public void CommandVideo(int index)
    {
        MenuBehavior.Instance.ChooseVideo(index);
        NetworkServer.SendToAll(new MirrorTransport.NumberVideo() {number = index});
    }
    
}
