using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class VideoCategory : IUpdatableFrom<VideoCategory>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (VideoCategory)other;
        name = otherConcrete.name;
        items.UpdateFrom(otherConcrete.items);
    }
    public void UpdateFrom(VideoCategory other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        name = reader.ReadString();
        items.Deserialize(reader);
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        writer.Write(name);
        items.Serialize(writer);
    }
    public  VideoCategory() 
    {
        name = string.Empty;
        items = new ZergRush.ReactiveCore.ReactiveCollection<LibraryItem>();
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.VideoCategory;
    }
    public override ProtocolItem NewInst() 
    {
    return new VideoCategory();
    }
}
#endif
