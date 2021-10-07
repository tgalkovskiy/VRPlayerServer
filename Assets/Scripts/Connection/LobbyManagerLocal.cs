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
    private ServerController serverController;
    private ClientController _client;
    private NetworkDiscovery networkDiscovery;
    private NetworkDiscoveryHUD _Hud;
    Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    
    public void OfflineStart()
    {
        Application.targetFrameRate = 60;
        serverController = ServerController.Instance;
        _client = ClientController.Instance;
        networkDiscovery = GetComponent<NetworkDiscovery>();
        _Hud = GetComponent<NetworkDiscoveryHUD>();
        if (isServer)
        {
            var mirrorTransport = new MirrorTransport();
            serverController.Init(mirrorTransport);
            serverController.ShowControlMenu();
            StartServer();
            networkDiscovery.AdvertiseServer();
            mirrorTransport.InitServer();
        }
        else
        {
            serverController.UnShowControlMenu();
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
        var mirrorTransport = new MirrorTransport();
        mirrorTransport.InitClient();
        _client.OnConnected(mirrorTransport);
    }
   
}
