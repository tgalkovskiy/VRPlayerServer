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
            public int number;
        }
        private void Start()
        {
            if(!NetworkClient.active) return;
            NetworkClient.RegisterHandler<MessageCommand>(OnGetMessage);
            NetworkClient.RegisterHandler<NumberVideo>(OnGetNumberVideo);
        }

        private void OnGetMessage(NetworkConnection connection, MessageCommand messageCommand)
        {
            MenuBehavior.Instance.ControllVideo(messageCommand.message);
        }

        private void OnGetNumberVideo(NetworkConnection connection, NumberVideo numberVideo)
        {
            MenuBehavior.Instance.ChooseVideo(numberVideo.number);
        }
    }
