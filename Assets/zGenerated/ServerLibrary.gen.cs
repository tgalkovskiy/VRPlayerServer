using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class ServerLibrary : IUpdatableFrom<ServerLibrary>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (ServerLibrary)other;
        library.UpdateFrom(otherConcrete.library);
    }
    public void UpdateFrom(ServerLibrary other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        library.Deserialize(reader);
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        library.Serialize(writer);
    }
    public  ServerLibrary() 
    {
        library = new ZergRush.ReactiveCore.ReactiveCollection<LibraryItem>();
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.ServerLibrary;
    }
    public override ProtocolItem NewInst() 
    {
    return new ServerLibrary();
    }
}
#endif
