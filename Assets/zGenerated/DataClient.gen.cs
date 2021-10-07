using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class DataClient : IUpdatableFrom<DataClient>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (DataClient)other;
        name = otherConcrete.name;
        battery = otherConcrete.battery;
        connection = otherConcrete.connection;
    }
    public void UpdateFrom(DataClient other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        name = reader.ReadString();
        battery = reader.ReadInt32();
        connection = reader.ReadString();
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        writer.Write(name);
        writer.Write(battery);
        writer.Write(connection);
    }
    public  DataClient() 
    {
        name = string.Empty;
        connection = string.Empty;
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.DataClient;
    }
    public override ProtocolItem NewInst() 
    {
    return new DataClient();
    }
}
#endif
