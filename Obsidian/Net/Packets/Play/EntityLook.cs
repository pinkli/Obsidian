﻿using Obsidian.Serializer.Attributes;
using Obsidian.Util.DataTypes;

namespace Obsidian.Net.Packets.Play
{
    public class EntityLook : Packet
    {
        [Field(0, Type = Serializer.Enums.DataType.VarInt)]
        public int EntityId { get; set; }

        [Field(1)]
        public Angle Yaw { get; set; }

        [Field(2)]
        public Angle Pitch { get; set; }

        [Field(3)]
        public bool OnGround { get; set; }

        public EntityLook() : base(0x2A) { }
    }
}
