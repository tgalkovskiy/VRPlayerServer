
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
        public struct DataClient: NetworkMessage
        {
            public string name;
            public int battery;
            public string connection;
        }
        public struct VolumePower: NetworkMessage
        {
            public float volumePower;
        }
        private void Start()
        {
            if (NetworkClient.active)
            {
                NetworkClient.RegisterHandler<MessageCommand>(OnGetMessage);
                NetworkClient.RegisterHandler<NameVideo>(OnGetNumberVideo);
                NetworkClient.RegisterHandler<NumberSceneOpen>(OnGetNumberScene);
                NetworkClient.RegisterHandler<SendDataFile>(OnGetDataFile);
                NetworkClient.RegisterHandler<VolumePower>(OngetVolumePower);
            }
            if (NetworkServer.active)
            {
                NetworkServer.RegisterHandler<DataClient>(OnGetClientData);
            }
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
        private void OnGetDataFile(NetworkConnection connection, SendDataFile sendDataFile)
        {
            DataManager.Instance.GetDataFile(sendDataFile.data, sendDataFile.format, sendDataFile.name);
        }
        private void OngetVolumePower(NetworkConnection connection, VolumePower volumePower)
        {
            MenuBehavior.Instance.ChangeVolumePower(volumePower.volumePower);
        }
        private void OnGetClientData(NetworkConnection connection, DataClient dataClient)
        {
            MenuBehavior.Instance.UpdateList(dataClient.name, dataClient.battery, dataClient.connection);
        }
    }
