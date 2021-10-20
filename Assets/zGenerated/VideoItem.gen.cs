using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class VideoItem : IUpdatableFrom<VideoItem>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (VideoItem)other;
        id = otherConcrete.id;
        description = otherConcrete.description;
        fileName = otherConcrete.fileName;
        extImage = otherConcrete.extImage;
        soundFilename = otherConcrete.soundFilename;
        subtitlesFileName = otherConcrete.subtitlesFileName;
    }
    public void UpdateFrom(VideoItem other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        id = reader.ReadString();
        description = reader.ReadString();
        fileName = reader.ReadString();
        extImage = reader.ReadString();
        soundFilename = reader.ReadString();
        subtitlesFileName = reader.ReadString();
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        writer.Write(id);
        writer.Write(description);
        writer.Write(fileName);
        writer.Write(extImage);
        writer.Write(soundFilename);
        writer.Write(subtitlesFileName);
    }
    public  VideoItem() 
    {
        id = string.Empty;
        description = string.Empty;
        fileName = string.Empty;
        extImage = string.Empty;
        soundFilename = string.Empty;
        subtitlesFileName = string.Empty;
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.VideoItem;
    }
    public override ProtocolItem NewInst() 
    {
    return new VideoItem();
    }
}
#endif
