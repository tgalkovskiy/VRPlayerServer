using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class ClientState : IUpdatableFrom<ClientState>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (ClientState)other;
        var __playingItem = playingItem.value;
        __playingItem.UpdateFrom(otherConcrete.playingItem.value);
        playingItem.value = __playingItem;
        playing.value = otherConcrete.playing.value;
        time.value = otherConcrete.time.value;
    }
    public void UpdateFrom(ClientState other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        playingItem.value.Deserialize(reader);
        playing.value = reader.ReadBoolean();
        time.value = reader.ReadSingle();
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        playingItem.value.Serialize(writer);
        writer.Write(playing.value);
        writer.Write(time.value);
    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.ClientState;
    }
    public override ProtocolItem NewInst() 
    {
    return new ClientState();
    }
}
#endif
