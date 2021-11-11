using System.Collections.Generic;
using System.Linq;
using Mirror;
using ZergRush;
using ZergRush.CodeGen;
using ZergRush.ReactiveCore;

[GenTask(GenTaskFlags.Serialization | GenTaskFlags.PolymorphicConstruction | GenTaskFlags.UpdateFrom | GenTaskFlags.DefaultConstructor)]
public partial class ProtocolItem : ISerializable {}
public partial class VideoLibItem : ProtocolItem {}

public partial class LibraryItem : ProtocolItem
{ 
}

public partial class VideoItem : LibraryItem
{
    public string id;
    public bool is2DVideo;
    public string description;
    public string fileName;
    public string soundFilename;
    public string subtitlesFileName;
    public string extImage;
    public string filePath => LoaderVideo.GetFillVideoPath(fileName);
    public string subFilePath => LoaderVideo.GetFillVideoPath(subtitlesFileName);
}

public partial class VideoCategory : LibraryItem
{
    public string name;
    public string description;
    public string extImage;
    public ReactiveCollection<LibraryItem> items;
}

public partial class ServerLibrary : ProtocolItem
{
    public ReactiveCollection<LibraryItem> library;
}