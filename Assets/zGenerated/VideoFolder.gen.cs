using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class VideoFolder : IUpdatableFrom<VideoFolder>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (VideoFolder)other;
        videoIds.UpdateFrom(otherConcrete.videoIds);
    }
    public void UpdateFrom(VideoFolder other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        videoIds.Deserialize(reader);
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        videoIds.Serialize(writer);
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.VideoFolder;
    }
    public override ProtocolItem NewInst() 
    {
    return new VideoFolder();
    }
}
#endif
