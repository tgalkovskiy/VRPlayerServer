using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class NeedFile : IUpdatableFrom<NeedFile>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (NeedFile)other;
        fileName = otherConcrete.fileName;
    }
    public void UpdateFrom(NeedFile other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        fileName = reader.ReadString();
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        writer.Write(fileName);
    }
    public  NeedFile() 
    {
        fileName = string.Empty;
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.NeedFile;
    }
    public override ProtocolItem NewInst() 
    {
    return new NeedFile();
    }
}
#endif
