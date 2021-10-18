using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
using SFB;
using UnityEditor;
using UnityEngine.UI;

public class LobbyManagerLocal : NetworkManager
{
    [SerializeField] private bool isServer = default;
    [SerializeField] private Text _waitText = default;
    [SerializeField] private GameObject _logo = default;
    private ServerController serverController;
    private ClientController _client;
    private NetworkDiscovery networkDiscovery;
    private NetworkDiscoveryHUD _Hud;
    Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();

    public async void OfflineStart()
    {
        Debug.Log($"Offline start isServer:{isServer}");
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
            _waitText.text = $"{SystemInfo.deviceName} \n wait for connection by the host...";
            _logo.SetActive(true);
            serverController.UnShowControlMenu();
            gameObject.SetActive(true);
            await Task.Delay(200);
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
        Debug.Log("Start connect");
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        while (_Hud.Search() == false)
            yield return new WaitForSeconds(0.3f);
        yield return new WaitForSeconds(1);
        Debug.Log("server found");
        var mirrorTransport = new MirrorTransport();
        mirrorTransport.InitClient();
        Debug.Log("client initialized");
        _client.OnConnected(mirrorTransport);
        _logo.SetActive(false);
    }
   
}
