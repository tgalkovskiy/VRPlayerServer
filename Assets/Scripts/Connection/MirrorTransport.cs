using System;
using UnityEngine;
using Mirror;

    public class MirrorTransport : MonoBehaviour
    {
        public struct MessageCommand : NetworkMessage
        {
            public string message;
        }
        public struct NameVideo : NetworkMessage
        {
            public string nameVideo;
        }
        public struct NumberSceneOpen: NetworkMessage
        {
            public int numberScene;
        }
        public struct SendDataFile: NetworkMessage
        {
            public byte[] data;
            public string format;
            public string name;
        }
        private void Start()
        {
            if(!NetworkClient.active) return;
            NetworkClient.RegisterHandler<MessageCommand>(OnGetMessage);
            NetworkClient.RegisterHandler<NameVideo>(OnGetNumberVideo);
            NetworkClient.RegisterHandler<NumberSceneOpen>(OnGetNumberScene);
            NetworkClient.RegisterHandler<SendDataFile>(OnSendData);
        }

        private void OnGetMessage(NetworkConnection connection, MessageCommand messageCommand)
        {
            MenuBehavior.Instance.ControlVideo(messageCommand.message);
        }
        private void OnGetNumberVideo(NetworkConnection connection, NameVideo nameVideo)
        {
            MenuBehavior.Instance.ChooseVideo(nameVideo.nameVideo);
        }
        private void OnGetNumberScene(NetworkConnection connection, NumberSceneOpen numberSceneOpen)
        {
            MenuBehavior.Instance.OpenScene(numberSceneOpen.numberScene);
        }
        private void OnSendData(NetworkConnection connection, SendDataFile sendDataFile)
        {
            MenuBehavior.Instance.GetData(sendDataFile.data, sendDataFile.format, sendDataFile.name);
        }
    }
