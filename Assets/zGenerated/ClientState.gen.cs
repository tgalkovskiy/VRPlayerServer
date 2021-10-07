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
        if (otherConcrete.playingItem.value == null) {
            __playingItem = null;
        }
        else { 
            if (__playingItem == null) {
                __playingItem = new VideoItem();
            }
            __playingItem.UpdateFrom(otherConcrete.playingItem.value);
        }
        playingItem.value = __playingItem;
        playing.value = otherConcrete.playing.value;
        time.value = otherConcrete.time.value;
        volume.value = otherConcrete.volume.value;
    }
    public void UpdateFrom(ClientState other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        if (!reader.ReadBoolean()) {
            playingItem.value = null;
        }
        else { 
            if (playingItem.value == null) {
                playingItem.value = new VideoItem();
            }
            playingItem.value.Deserialize(reader);
        }
        playing.value = reader.ReadBoolean();
        time.value = reader.ReadSingle();
        volume.value = reader.ReadSingle();
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        if (playingItem.value == null) writer.Write(false);
        else {
            writer.Write(true);
            playingItem.value.Serialize(writer);
        }
        writer.Write(playing.value);
        writer.Write(time.value);
        writer.Write(volume.value);
    }
    public  ClientState() 
    {
        playingItem = new ZergRush.ReactiveCore.Cell<VideoItem>();
        playing = new ZergRush.ReactiveCore.Cell<System.Boolean>();
        time = new ZergRush.ReactiveCore.Cell<System.Single>();
        volume = new ZergRush.ReactiveCore.Cell<System.Single>();
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
