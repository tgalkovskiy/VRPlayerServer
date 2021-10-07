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

    public void InitClient()
    {
        if (!NetworkClient.active) return;
        NetworkClient.RegisterHandler<MirrorCommand>(CommandReceived);
    }
    
    public void InitServer()
    {
        if (!NetworkServer.active) return;
        NetworkServer.RegisterHandler<MirrorCommand>(CommandReceived);
    }

    private void CommandReceived(NetworkConnection connection, MirrorCommand command)
    {
        Debug.Log($"Mirror received bytes:{command.data.Length}");
        _stream.Send(command.data.LoadFromBinary<NetworkCommandWrapper>().command);
    }

    public void SendCommand(NetworkCommand command)
    {
        var saveToBinary = new NetworkCommandWrapper{command = command}.SaveToBinary();
        Debug.Log($"Mirror command {command} len:{saveToBinary.Length}");
        NetworkClient.Send(new MirrorCommand { data = saveToBinary });
    }

    public void SendCommandAll(NetworkCommand command)
    {
        NetworkServer.SendToAll(new MirrorCommand { data = new NetworkCommandWrapper{command = command}.SaveToBinary() });
    }

    public IEventStream<NetworkCommand> commandReceived => _stream;
}