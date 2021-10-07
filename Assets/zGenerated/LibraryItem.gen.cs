using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class LibraryItem : IUpdatableFrom<LibraryItem>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (LibraryItem)other;
    }
    public void UpdateFrom(LibraryItem other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);

    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);

    }
    public  LibraryItem() 
    {

    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.LibraryItem;
    }
    public override ProtocolItem NewInst() 
    {
    return new LibraryItem();
    }
}
#endif
