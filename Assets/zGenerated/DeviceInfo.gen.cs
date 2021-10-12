using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class DeviceInfo : IUpdatableFrom<DeviceInfo>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (DeviceInfo)other;
        name = otherConcrete.name;
        battery = otherConcrete.battery;
        connection = otherConcrete.connection;
        updated.UpdateFrom(otherConcrete.updated);
    }
    public void UpdateFrom(DeviceInfo other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        name = reader.ReadString();
        battery = reader.ReadInt32();
        connection = reader.ReadString();
        updated.Deserialize(reader);
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        writer.Write(name);
        writer.Write(battery);
        writer.Write(connection);
        updated.Serialize(writer);
    }
    public  DeviceInfo() 
    {
        name = string.Empty;
        connection = string.Empty;
        updated = new ZergRush.ReactiveCore.EventStream();
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.DeviceInfo;
    }
    public override ProtocolItem NewInst() 
    {
    return new DeviceInfo();
    }
}
#endif
