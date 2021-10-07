using System;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientController : MonoBehaviour
{
    [SerializeField] private MediaPlayer _mediaPlayer = default;

    public static ClientController Instance;
    public ClientState state = new ClientState();
    public INetwork network;

    void Awake()
    {
        Instance = this;
    }

    public void Init(INetwork net)
    {
        _mediaPlayer.SetActiveSafe(true);
        network = net;
        network.commandReceived.Subscribe(c =>
        {
            switch (c)
            {
                case ClientState state : Debug.Log("asdf"); break;
                case SendDataFile data : Debug.Log("asdf"); break;
                case NumberSceneOpen n : OpenScene(n.numberScene);
                    break;
            }
        });
        state.BindToPlayer(_mediaPlayer);
    }
    
    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}