using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;

public class LobyManagerLocal : NetworkManager
{
    [SerializeField] private bool isServer = default;
    private MenuBehavior _menuBehavior;
    private NetworkDiscovery networkDiscovery;
    private NetworkDiscoveryHUD _Hud;
    Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    
    public void OfflineStart()
    {
        _menuBehavior = MenuBehavior.Instance;
        networkDiscovery = GetComponent<NetworkDiscovery>();
        _Hud = GetComponent<NetworkDiscoveryHUD>();
        if (isServer)
        {
            _menuBehavior.ShowControlMenu();
            StartServer();
            networkDiscovery.AdvertiseServer();
            //StartCoroutine(Connect());
        }
        else
        {
            _menuBehavior.UnShowControllMenu();
            StartCoroutine(Connect());
            //StartClient();
        }
    }
    public void SendMessageToAll(string command)
    {
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "sdvsdv"});
    }
    public void CommandPlay()
    {
        MenuBehavior.Instance.ControlVideo("Play");
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Play"});
    }
    public void CommandStop()
    {
        MenuBehavior.Instance.ControlVideo("Stop");
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Stop"});
    }
    public void CommandMuteAudio()
    {
        MenuBehavior.Instance.ControlVideo("Mute");
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Mute"});
    }
    public void CommandRebootVideo()
    {
        MenuBehavior.Instance.ControlVideo("Reboot");
        NetworkServer.SendToAll(new MirrorTransport.MessageCommand() {message = "Reboot"});
    }
    public void CommandVideo(int index)
    {
        MenuBehavior.Instance.ChooseVideo(index);
        NetworkServer.SendToAll(new MirrorTransport.NumberVideo() {numberVideo = index});
    }
    
    public void OpenScene(int index)
    {
        NetworkServer.SendToAll(new MirrorTransport.NumberSceneOpen() {numberScene = index});
    }

    IEnumerator Connect()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        yield return new WaitForSeconds(1f);
        _Hud.Search();
    }
   
}
