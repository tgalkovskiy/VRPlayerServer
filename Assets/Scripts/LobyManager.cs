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
    [SerializeField] private InputField _inputNameRoom = default;
    [SerializeField] private Text _nameText = default;
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
        _nameText.text = _inputName.text;
        PhotonNetwork.NickName = _namePlayer;
        PhotonNetwork.GameVersion = _gameVersion;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }
     
    public override void OnCreatedRoom()
    {
        Debug.Log("Room Create");
        _playerNetBehavior.ShowControlMenu();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log($"Conect {_namePlayer} in {_inputNameRoom.text}");
        if (!PhotonNetwork.IsMasterClient)
        {
            _playerNetBehavior.UnshowControllMenu();
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to MasterServer");
        _playerNetBehavior.UnshowInputField();
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(_inputNameRoom.text, new Photon.Realtime.RoomOptions{ MaxPlayers = 10});
       
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_inputNameRoom.text);
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

    public void CommandVideo(int index)
    {
        SendMessage(index.ToString());
    }

}
