using ZergRush.ReactiveCore;

public interface INetwork
{
    void SendCommand(NetworkCommand command);
    IEventStream<NetworkCommand> commandReceived { get; }
}

public interface INetworkServer
{
    void SendCommandAll(NetworkCommand command);
    IEventStream<NetworkCommand> commandReceived { get; }
}
