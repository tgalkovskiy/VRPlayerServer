using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
public class LobyManager : MonoBehaviourPunCallbacks
{
    #region Field
    [SerializeField] private PlayerNetBehavior _playerNetBehavior = default;
    [SerializeField] private InputField _inputName = default;
    [SerializeField] private InputField _inputNameRoom = default;
    [SerializeField] private Text _nameText = default;
    [SerializeField] private string _gameVersion = default;
    private PhotonView View;
    public static LobyManager Instance;
    private string _namePlayer;
    #endregion
    #region LifeCicle
    private void Awake()
    {
        View = GetComponent<PhotonView>();
        Instance = this;
    }
    #endregion
    #region ConectionOfLoby
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
    #endregion
    #region SendMessage
    private void SendMessageControllVideo(string command)
    {
        View.RPC("GetCommandVideo", RpcTarget.All, command);
    }
    private void SendMessageChooseVideo(int index)
    {
        View.RPC("GetChooseVideo", RpcTarget.All, index);
    }
    #endregion
    #region GetCommand
    [PunRPC]
    private void GetCommandVideo(string command)
    {
        _playerNetBehavior.ControllVideo(command);
    }
    [PunRPC]
    private void GetChooseVideo(int index)
    {
        _playerNetBehavior.ChooseVideo(index);
    }
    #endregion
    #region Command
    public void CommandPlay()
    {
        SendMessageControllVideo("Play");
    }
    public void CommandStop()
    {
        SendMessageControllVideo("Stop");
    }
    public void CommandMuteAudio()
    {
        SendMessageControllVideo("Mute");
    }
    public void CommandRebootVideo()
    {
        SendMessageControllVideo("Reboot");
    }
    public void CommandVideo(int index)
    {
        SendMessageChooseVideo(index);
    }
    #endregion

}
