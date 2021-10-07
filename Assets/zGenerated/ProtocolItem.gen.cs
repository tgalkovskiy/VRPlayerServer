using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class ProtocolItem : IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public enum Types : ushort
    {
        ProtocolItem = 1,
        ClientState = 2,
        NumberSceneOpen = 3,
        SendDataFile = 4,
        VideoLibItem = 5,
        VideoItem = 6,
        VideoFolder = 7,
        ServerLibrary = 8,
        NetworkCommand = 9,
    }
    static Func<ProtocolItem> [] polymorphConstructors = new Func<ProtocolItem> [] {
        () => null, // 0
        () => new ProtocolItem(), // 1
        () => new ClientState(), // 2
        () => new NumberSceneOpen(), // 3
        () => new SendDataFile(), // 4
        () => new VideoLibItem(), // 5
        () => new VideoItem(), // 6
        () => new VideoFolder(), // 7
        () => new ServerLibrary(), // 8
        () => new NetworkCommand(), // 9
    };
    public static ProtocolItem CreatePolymorphic(System.UInt16 typeId) {
        return polymorphConstructors[typeId]();
    }
    public virtual void UpdateFrom(ProtocolItem other) 
    {

    }
    public virtual void Deserialize(BinaryReader reader) 
    {

    }
    public virtual void Serialize(BinaryWriter writer) 
    {

    }
    public virtual ushort GetClassId() 
    {
    return (System.UInt16)Types.ProtocolItem;
    }
    public virtual ProtocolItem NewInst() 
    {
    return new ProtocolItem();
    }
}
#endif
