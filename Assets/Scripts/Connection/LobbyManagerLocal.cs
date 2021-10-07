using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
using SFB;
using UnityEditor;

public class LobbyManagerLocal : NetworkManager
{
    [SerializeField] private bool isServer = default;
    private MenuBehavior _menuBehavior;
    private ClientController _client;
    private NetworkDiscovery networkDiscovery;
    private NetworkDiscoveryHUD _Hud;
    Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    
    public void OfflineStart()
    {
        Application.targetFrameRate = 60;
        _menuBehavior = MenuBehavior.Instance;
        _client = ClientController.Instance;
        networkDiscovery = GetComponent<NetworkDiscovery>();
        _Hud = GetComponent<NetworkDiscoveryHUD>();
        if (isServer)
        {
            _menuBehavior.Init(new MirrorTransport());
            _menuBehavior.ShowControlMenu();
            StartServer();
            networkDiscovery.AdvertiseServer();
        }
        else
        {
            _menuBehavior.UnShowControlMenu();
            _client.Init(new MirrorTransport());
            StartCoroutine(Connect());
        }
    }
    public void GetAllConnection()
    {
        /*List<string> connection = new List<string>();
        for(int i = 0; i < NetworkServer.connections.Count; i++)
        {
            connection.Add(NetworkServer.connections[i+1].address);
        }
        MenuBehavior.Instance.UpdateListDevise(connection);*/
    }
    IEnumerator Connect()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        yield return new WaitForSeconds(1f);
        _Hud.Search();
        yield return new WaitForSeconds(1f);
        _client.OnConnected();
    }
   
}
