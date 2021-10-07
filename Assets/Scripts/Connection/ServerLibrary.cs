using System.Collections.Generic;
using System.Linq;
using Mirror;
using ZergRush;
using ZergRush.CodeGen;
using ZergRush.ReactiveCore;

[GenTask(GenTaskFlags.Serialization | GenTaskFlags.PolymorphicConstruction | GenTaskFlags.UpdateFrom)]
public partial class ProtocolItem : ISerializable {}

public partial class VideoLibItem : ProtocolItem {}

public partial class VideoItem : VideoLibItem
{
    public string id;
    public string description;
    public string fileName;
    public string soundFilename;
    public string subtitlesFileName;
    public string filePath => fileName;
}

public partial class VideoFolder : VideoLibItem
{
    public List<VideoItem> videoIds;
}

public partial class ServerLibrary : ProtocolItem
{
    public ReactiveCollection<VideoItem> library;
    public ReactiveCollection<VideoFolder> playlists;

    public IEnumerable<string> RequiredFiles(string videoId)
    {
        var item = library.Find(v => v.id == videoId);
        yield return item.fileName;
        if (item.soundFilename.IsNullOrEmpty() == false) yield return item.soundFilename;
        if (item.subtitlesFileName.IsNullOrEmpty() == false) yield return item.subtitlesFileName;
    }

    public IEnumerable<string> files => library
        .SelectMany(l => new[] { l.fileName, l.soundFilename, l.subtitlesFileName })
        .Where(name => name != null);
}