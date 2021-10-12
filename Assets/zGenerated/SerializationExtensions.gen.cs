using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ZergRush.Alive;
#if !INCLUDE_ONLY_CODE_GENERATION

public static partial class SerializationExtensions
{
    public static void Serialize(this ZergRush.ReactiveCore.ReactiveCollection<LibraryItem> self, BinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            writer.Write(self[i].GetClassId());
            self[i].Serialize(writer);
        }
    }
    public static void Deserialize(this ZergRush.ReactiveCore.ReactiveCollection<LibraryItem> self, BinaryReader reader) 
    {
        var size = reader.ReadInt32();
        if(size > 1000) throw new ZergRushCorruptedOrInvalidDataLayout();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            LibraryItem val = default;
            val = (LibraryItem)ProtocolItem.CreatePolymorphic(reader.ReadUInt16());
            val.Deserialize(reader);
            self.Add(val);
        }
    }
    public static void UpdateFrom(this ZergRush.ReactiveCore.ReactiveCollection<LibraryItem> self, ZergRush.ReactiveCore.ReactiveCollection<LibraryItem> other) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            var self_i_ClassId = other[i].GetClassId();
            if (self[i] == null || self[i].GetClassId() != self_i_ClassId) {
                self[i] = (LibraryItem)other[i].NewInst();
            }
            self[i].UpdateFrom(other[i]);
        }
        for (; i < other.Count; ++i)
        {
            LibraryItem inst = default;
            inst = (LibraryItem)other[i].NewInst();
            inst.UpdateFrom(other[i]);
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void Serialize(this ZergRush.ReactiveCore.EventStream self, BinaryWriter writer) 
    {

    }
    public static void Deserialize(this ZergRush.ReactiveCore.EventStream self, BinaryReader reader) 
    {

    }
    public static void UpdateFrom(this ZergRush.ReactiveCore.EventStream self, ZergRush.ReactiveCore.EventStream other) 
    {

    }
}
#endif
