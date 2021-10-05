using System;
using UnityEngine;
using Mirror;

    public class MirrorTransport : MonoBehaviour
    {
        public struct MessageCommand : NetworkMessage
        {
            public string message;
        }
        public struct NumberVideo : NetworkMessage
        {
            public int numberVideo;
        }
        public struct NumberSceneOpen: NetworkMessage
        {
            public int numberScene;
        }
        public struct SendDataFile: NetworkMessage
        {
            public byte[] data;
        }
        private void Start()
        {
            if(!NetworkClient.active) return;
            NetworkClient.RegisterHandler<MessageCommand>(OnGetMessage);
            NetworkClient.RegisterHandler<NumberVideo>(OnGetNumberVideo);
            NetworkClient.RegisterHandler<NumberSceneOpen>(OnGetNumberScene);
            NetworkClient.RegisterHandler<SendDataFile>(OnSendData);
        }

        private void OnGetMessage(NetworkConnection connection, MessageCommand messageCommand)
        {
            MenuBehavior.Instance.ControlVideo(messageCommand.message);
        }
        private void OnGetNumberVideo(NetworkConnection connection, NumberVideo numberVideo)
        {
            MenuBehavior.Instance.ChooseVideo(numberVideo.numberVideo);
        }
        private void OnGetNumberScene(NetworkConnection connection, NumberSceneOpen numberSceneOpen)
        {
            MenuBehavior.Instance.OpenScene(numberSceneOpen.numberScene);
        }
        private void OnSendData(NetworkConnection connection, SendDataFile sendDataFile)
        {
            MenuBehavior.Instance.GetData(sendDataFile.data);
        }
    }
