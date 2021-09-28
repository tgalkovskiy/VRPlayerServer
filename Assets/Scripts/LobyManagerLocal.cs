using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;

public class LobyManagerLocal : NetworkManager
{
    [SerializeField] private MenuBehavior _menuBehavior = default;
    [SerializeField] private bool isServer = default;
    public NetworkDiscovery networkDiscovery;
    public NetworkDiscoveryHUD _Hud;
    Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    
    public void OfflineStart()
    {
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

    IEnumerator Connect()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        yield return new WaitForSeconds(1f);
        _Hud.Search();
    }
   
}
