using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class NetworkCommandWrapper : IUpdatableFrom<NetworkCommandWrapper>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (NetworkCommandWrapper)other;
        var commandClassId = otherConcrete.command.GetClassId();
        if (command == null || command.GetClassId() != commandClassId) {
            command = (NetworkCommand)otherConcrete.command.NewInst();
        }
        command.UpdateFrom(otherConcrete.command);
    }
    public void UpdateFrom(NetworkCommandWrapper other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        var commandClassId = reader.ReadUInt16();
        if (command == null || command.GetClassId() != commandClassId) {
            command = (NetworkCommand)ProtocolItem.CreatePolymorphic(commandClassId);
        }
        command.Deserialize(reader);
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        writer.Write(command.GetClassId());
        command.Serialize(writer);
    }
    public  NetworkCommandWrapper() 
    {
        command = (NetworkCommand)new NetworkCommand();
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.NetworkCommandWrapper;
    }
    public override ProtocolItem NewInst() 
    {
    return new NetworkCommandWrapper();
    }
}
#endif
