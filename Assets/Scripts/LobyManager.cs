using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
public class LobyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerNetBehavior _playerNetBehavior = default;
    [SerializeField] private InputField _inputName = default;
    [SerializeField] private string _NameRoom = default;
    [SerializeField] private string _gameVersion = default;
    private PhotonView View;
    private string _namePlayer;
    private void Awake()
    {
        View = GetComponent<PhotonView>();
    }
    public void SetNamePlayerAndConect()
    {
        _namePlayer = _inputName.text;
        PhotonNetwork.NickName = _namePlayer;
        PhotonNetwork.GameVersion = _gameVersion;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Room Create");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log($"Conect {_namePlayer} in {_NameRoom}");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to MasterServer");
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(_NameRoom, new Photon.Realtime.RoomOptions{ MaxPlayers = 10});
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_NameRoom);
        if (!PhotonNetwork.IsMasterClient)
        {
            GetCommand("UnShow");
        }
    }

    private void SendMessage(string command)
    {
        View.RPC("GetCommand", RpcTarget.All, command);
    }
    [PunRPC]
    private void GetCommand(string command)
    {
        _playerNetBehavior.GetCommand(command);
    }

    public void CommanPlay()
    {
        SendMessage("Play");
    }
    public void CommandStop()
    {
        SendMessage("Stop");
    }

}
