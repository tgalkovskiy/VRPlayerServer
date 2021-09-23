using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

[RequireComponent(typeof(PhotonView))]
public class LobyManager : MonoBehaviourPunCallbacks
{
    #region Field
    [SerializeField] private PlayerNetBehavior _playerNetBehavior = default;
    //[SerializeField] private InputField _inputNameRoom = default;
    [SerializeField] private Text _nameText = default;
    [SerializeField] private string _gameVersion = default;
    private PhotonView View;
    public static LobyManager Instance;
    private string _namePlayer;
    private string _nameRoom;
    List<string> playersInRoom = new List<string>();
    #endregion
    #region LifeCicle
    private void Awake()
    {
        View = GetComponent<PhotonView>();
        Instance = this;
    }
    private void Start()
    {
       _namePlayer = SystemInfo.deviceName;
       _nameText.text = _namePlayer;
       _nameRoom = "VR360";
       PhotonNetwork.NickName = _namePlayer;
       PhotonNetwork.GameVersion = _gameVersion;
       PhotonNetwork.AutomaticallySyncScene = true;
       PhotonNetwork.NetworkingClient.EnableLobbyStatistics = true;
       PhotonNetwork.ConnectUsingSettings();
    }
    #endregion
    #region ConectionOfLoby
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to MasterServer");
        //CreateRoom();
        //JoinRoom();
    #if UNITY_EDITOR || UNITY_STANDALONE
            CreateRoom();
    #endif
    #if UNITY_ANDROID || ANDROID_DEVICE
            JoinRoom();
    #endif
        }
    public override void OnCreatedRoom()
    {
        Debug.Log("Room Create");
        _playerNetBehavior.ShowControlMenu();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log($"Conect {_namePlayer} in {_nameRoom}");
        if (!PhotonNetwork.IsMasterClient)
        {
            _playerNetBehavior.UnShowControllMenu();
        }
        //update stats in room
        View.RPC("RoomState", RpcTarget.MasterClient);
    }
    public override void OnLeftRoom()
    {
        Debug.Log($"Left {_namePlayer} in {_nameRoom}");
        View.RPC("RoomState", RpcTarget.MasterClient);
    }
    [PunRPC]
    public void RoomState()
    {
        playersInRoom.Clear();
        int countPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        for(int i=0; i<countPlayers; i++)
        {
            playersInRoom.Add(PhotonNetwork.CurrentRoom.GetPlayer(i+1).NickName);
            Debug.Log(playersInRoom[i]);
        }
        _playerNetBehavior.UpdateListDevise(playersInRoom);
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(_nameRoom, new Photon.Realtime.RoomOptions{ MaxPlayers = 10});
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_nameRoom);
    }
    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom(true);
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
