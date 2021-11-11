using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class SendDataFile : IUpdatableFrom<SendDataFile>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (SendDataFile)other;
        percent = otherConcrete.percent;
        data = otherConcrete.data;
        format = otherConcrete.format;
        name = otherConcrete.name;
    }
    public void UpdateFrom(SendDataFile other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        percent = reader.ReadInt32();
        data = reader.ReadByteArray();
        format = reader.ReadString();
        name = reader.ReadString();
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        writer.Write(percent);
        writer.WriteByteArray(data);
        writer.Write(format);
        writer.Write(name);
    }
    public  SendDataFile() 
    {
        data = Array.Empty<System.Byte>();
        percent = default;
        format = string.Empty;
        name = string.Empty;
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.SendDataFile;
    }
    public override ProtocolItem NewInst() 
    {
    return new SendDataFile();
    }
}
#endif
