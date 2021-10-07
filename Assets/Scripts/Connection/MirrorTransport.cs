using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using WebSocketSharp;
using ZergRush.ReactiveCore;

public partial class NumberSceneOpen : NetworkCommand
{
    public int numberScene;
}

public partial class SendDataFile : NetworkCommand
{
    public byte[] data;
    public string format;
    public string name;
}

public partial class DeviceInfo : NetworkCommand
{
    public string name;
    public int battery;
    public string connection;
}

public struct MirrorCommand : NetworkMessage
{
    public byte[] data;
}

public class MirrorTransport : INetwork, INetworkServer
{
    EventStream<NetworkCommand> _stream = new EventStream<NetworkCommand>();

    public void Init()
    {
        if (!NetworkClient.active) return;
        NetworkClient.RegisterHandler<MirrorCommand>(CommandReceived);
    }

    private void CommandReceived(NetworkConnection connection, MirrorCommand command)
    {
        _stream.Send(command.data.LoadFromBinary<NetworkCommand>());
    }

    public void SendCommand(NetworkCommand command)
    {
        NetworkClient.Send(new MirrorCommand { data = command.SaveToBinary() });
    }

    public void SendCommandAll(NetworkCommand command)
    {
        NetworkServer.SendToAll(new MirrorCommand { data = command.SaveToBinary() });
    }

    public IEventStream<NetworkCommand> commandReceived => _stream;
}