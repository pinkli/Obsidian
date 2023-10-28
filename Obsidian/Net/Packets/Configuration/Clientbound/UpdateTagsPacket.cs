﻿using Obsidian.Serialization.Attributes;

namespace Obsidian.Net.Packets.Configuration.Clientbound;
public sealed partial class UpdateTagsPacket : IClientboundPacket
{
    [Field(0)]
    public IDictionary<string, Tag[]> Tags { get; }

    public int Id => 0x08;

    public static UpdateTagsPacket FromRegistry { get; } = new(TagsRegistry.Categories);

    public UpdateTagsPacket(IDictionary<string, Tag[]> tags)
    {
        this.Tags = tags;
    }
}

public class Tag
{
    public string Name { get; init; }
    public string Type { get; init; }
    public bool Replace { get; init; }
    public int[] Entries { get; init; }
    public int Count => Entries.Length;
}
