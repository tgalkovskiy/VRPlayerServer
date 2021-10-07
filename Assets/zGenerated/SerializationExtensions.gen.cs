using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ZergRush.Alive;
#if !INCLUDE_ONLY_CODE_GENERATION

public static partial class SerializationExtensions
{
    public static void Serialize(this ZergRush.ReactiveCore.ReactiveCollection<VideoFolder> self, BinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            self[i].Serialize(writer);
        }
    }
    public static void Serialize(this ZergRush.ReactiveCore.ReactiveCollection<VideoItem> self, BinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            self[i].Serialize(writer);
        }
    }
    public static void Deserialize(this ZergRush.ReactiveCore.ReactiveCollection<VideoFolder> self, BinaryReader reader) 
    {
        var size = reader.ReadInt32();
        if(size > 1000) throw new ZergRushCorruptedOrInvalidDataLayout();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            VideoFolder val = default;
            val = new VideoFolder();
            val.Deserialize(reader);
            self.Add(val);
        }
    }
    public static void Deserialize(this ZergRush.ReactiveCore.ReactiveCollection<VideoItem> self, BinaryReader reader) 
    {
        var size = reader.ReadInt32();
        if(size > 1000) throw new ZergRushCorruptedOrInvalidDataLayout();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            VideoItem val = default;
            val = new VideoItem();
            val.Deserialize(reader);
            self.Add(val);
        }
    }
    public static void UpdateFrom(this ZergRush.ReactiveCore.ReactiveCollection<VideoFolder> self, ZergRush.ReactiveCore.ReactiveCollection<VideoFolder> other) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            self[i].UpdateFrom(other[i]);
        }
        for (; i < other.Count; ++i)
        {
            VideoFolder inst = default;
            inst = new VideoFolder();
            inst.UpdateFrom(other[i]);
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void UpdateFrom(this ZergRush.ReactiveCore.ReactiveCollection<VideoItem> self, ZergRush.ReactiveCore.ReactiveCollection<VideoItem> other) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            self[i].UpdateFrom(other[i]);
        }
        for (; i < other.Count; ++i)
        {
            VideoItem inst = default;
            inst = new VideoItem();
            inst.UpdateFrom(other[i]);
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
    public static void Serialize(this System.Collections.Generic.List<VideoItem> self, BinaryWriter writer) 
    {
        writer.Write(self.Count);
        for (int i = 0; i < self.Count; i++)
        {
            self[i].Serialize(writer);
        }
    }
    public static void Deserialize(this System.Collections.Generic.List<VideoItem> self, BinaryReader reader) 
    {
        var size = reader.ReadInt32();
        if(size > 1000) throw new ZergRushCorruptedOrInvalidDataLayout();
        self.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            VideoItem val = default;
            val = new VideoItem();
            val.Deserialize(reader);
            self.Add(val);
        }
    }
    public static void UpdateFrom(this System.Collections.Generic.List<VideoItem> self, System.Collections.Generic.List<VideoItem> other) 
    {
        int i = 0;
        int oldCount = self.Count;
        int crossCount = Math.Min(oldCount, other.Count);
        for (; i < crossCount; ++i)
        {
            self[i].UpdateFrom(other[i]);
        }
        for (; i < other.Count; ++i)
        {
            VideoItem inst = default;
            inst = new VideoItem();
            inst.UpdateFrom(other[i]);
            self.Add(inst);
        }
        for (; i < oldCount; ++i)
        {
            self.RemoveAt(self.Count - 1);
        }
    }
}
#endif
