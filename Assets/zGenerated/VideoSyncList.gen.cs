using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class VideoSyncList : IUpdatableFrom<VideoSyncList>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (VideoSyncList)other;
        items.UpdateFrom(otherConcrete.items);
    }
    public void UpdateFrom(VideoSyncList other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        items.Deserialize(reader);
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        items.Serialize(writer);
    }
    public  VideoSyncList() 
    {
        items = new System.Collections.Generic.List<VideoItem>();
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.VideoSyncList;
    }
    public override ProtocolItem NewInst() 
    {
    return new VideoSyncList();
    }
}
#endif
