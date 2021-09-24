using System;
using UnityEngine;
using Mirror;

    public class MirrorTransport : MonoBehaviour
    {
        public struct MessageCommand : NetworkMessage
        {
            public string message;
        }

        private void Start()
        {
            if(!NetworkClient.active) return;
            NetworkClient.RegisterHandler<MessageCommand>(OnGetMessage);
        }

        private void OnGetMessage(NetworkConnection connection, MessageCommand messageCommand)
        {
           Debug.Log(messageCommand.message); 
        }
    }
