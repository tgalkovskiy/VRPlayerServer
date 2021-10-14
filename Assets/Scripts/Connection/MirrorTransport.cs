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
    public bool disconnected;
    public bool syncInProcess;
    
    public EventStream updated;
}

public partial class NeedFile : NetworkCommand
{
    public string fileName;
}

public partial class VideoSyncList : NetworkCommand
{
    public List<VideoItem> items;
}

public partial class VideoPath : NetworkCommand
{
    public string path;
}
public struct MirrorCommand : NetworkMessage
{
    public byte[] data;
}

public class MirrorTransport : INetwork, INetworkServer
{
    EventStream<NetworkCommand> _stream = new EventStream<NetworkCommand>();
    EventStream<int> _disconnectedStream = new EventStream<int>();

    public void InitClient()
    {
        if (!NetworkClient.active) return;
        NetworkClient.RegisterHandler<MirrorCommand>(CommandReceived);
    }
    
    public void InitServer()
    {
        if (!NetworkServer.active) return;
        NetworkServer.RegisterHandler<MirrorCommand>(CommandReceived);
        NetworkServer.OnDisconnectedEvent += connection => _disconnectedStream.Send(connection.connectionId);
    }

    private void CommandReceived(NetworkConnection connection, MirrorCommand command)
    {
        Debug.Log($"Mirror received bytes:{command.data.Length}");
        try
        {
            var networkCommand = command.data.LoadFromBinary<NetworkCommandWrapper>().command;
            networkCommand.connectionId = connection.connectionId;
            _stream.Send(networkCommand);
        }
        catch (Exception e)
        {
            Debug.LogError($"error in command received {e.ToError()}");
        }
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

    public void SendCommand(int connectionId, NetworkCommand command)
    {
        NetworkServer.SendToReady(NetworkServer.connections[connectionId].identity,
            new MirrorCommand { data = new NetworkCommandWrapper{command = command}.SaveToBinary() });
    }

    public IEventStream<NetworkCommand> commandReceived => _stream;
    public IEventStream<int> clientDisconnected => _disconnectedStream;
}