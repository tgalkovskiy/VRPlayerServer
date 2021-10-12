using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class VideoPath : IUpdatableFrom<VideoPath>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (VideoPath)other;
        path = otherConcrete.path;
    }
    public void UpdateFrom(VideoPath other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        path = reader.ReadString();
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        writer.Write(path);
    }
    public  VideoPath() 
    {
        path = string.Empty;
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.VideoPath;
    }
    public override ProtocolItem NewInst() 
    {
    return new VideoPath();
    }
}
#endif
