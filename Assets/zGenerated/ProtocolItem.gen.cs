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
        DataClient = 10,
        VideoLibItem = 5,
        LibraryItem = 11,
        VideoItem = 6,
        VideoCategory = 12,
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
        () => null, // 7
        () => new ServerLibrary(), // 8
        () => new NetworkCommand(), // 9
        () => new DataClient(), // 10
        () => new LibraryItem(), // 11
        () => new VideoCategory(), // 12
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
    public  ProtocolItem() 
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
