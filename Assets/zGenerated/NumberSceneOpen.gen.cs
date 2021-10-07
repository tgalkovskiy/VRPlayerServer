using System;
using System.Collections.Generic;
using System.Text;
using ZergRush.Alive;
using System.IO;
#if !INCLUDE_ONLY_CODE_GENERATION

public partial class NumberSceneOpen : IUpdatableFrom<NumberSceneOpen>, IUpdatableFrom<ProtocolItem>, IPolymorphable
{
    public override void UpdateFrom(ProtocolItem other) 
    {
        base.UpdateFrom(other);
        var otherConcrete = (NumberSceneOpen)other;
        numberScene = otherConcrete.numberScene;
    }
    public void UpdateFrom(NumberSceneOpen other) 
    {
        this.UpdateFrom((ProtocolItem)other);
    }
    public override void Deserialize(BinaryReader reader) 
    {
        base.Deserialize(reader);
        numberScene = reader.ReadInt32();
    }
    public override void Serialize(BinaryWriter writer) 
    {
        base.Serialize(writer);
        writer.Write(numberScene);
    }
    public  NumberSceneOpen() 
    {

    }
    public override ushort GetClassId() 
    {
    return (System.UInt16)Types.NumberSceneOpen;
    }
    public override ProtocolItem NewInst() 
    {
    return new NumberSceneOpen();
    }
}
#endif
