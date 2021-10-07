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
    public string description;
    public string fileName;
    public string soundFilename;
    public string subtitlesFileName;
    public string filePath => LoaderVideo.GetFillVideoPath(fileName);
    public string subFilePath => LoaderVideo.GetFillVideoPath(subtitlesFileName);
}

public partial class VideoCategory : LibraryItem
{
    public string name;
    public ReactiveCollection<LibraryItem> items;
}

public partial class ServerLibrary : ProtocolItem
{
    public ReactiveCollection<LibraryItem> library;

    // public IEnumerable<string> RequiredFiles(string videoId)
    // {
    //     var item = library.Find(v => v.id == videoId);
    //     yield return item.fileName;
    //     if (item.soundFilename.IsNullOrEmpty() == false) yield return item.soundFilename;
    //     if (item.subtitlesFileName.IsNullOrEmpty() == false) yield return item.subtitlesFileName;
    // }
    //
    // public IEnumerable<string> files => library
    //     .SelectMany(l => new[] { l.fileName, l.soundFilename, l.subtitlesFileName })
    //     .Where(name => name != null);
}