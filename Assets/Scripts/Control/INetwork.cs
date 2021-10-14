using ZergRush.ReactiveCore;

public interface INetwork
{
    void SendCommand(NetworkCommand command);
    IEventStream<NetworkCommand> commandReceived { get; }
}

public interface INetworkServer
{
    void SendCommandAll(NetworkCommand command);
    void SendCommand(int connectionId, NetworkCommand command);
    IEventStream<NetworkCommand> commandReceived { get; }
    IEventStream<int> clientDisconnected { get; }
}
