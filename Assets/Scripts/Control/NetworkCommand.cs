using ZergRush.CodeGen;

public partial class NetworkCommand : ProtocolItem
{
    [GenIgnore] public int connectionId;
}

public partial class NetworkCommandWrapper : ProtocolItem
{
    public NetworkCommand command;
}