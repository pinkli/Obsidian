﻿using Obsidian.Blocks;
using Obsidian.Util.DataTypes;
using Obsidian.Util.Registry;

namespace Obsidian.World
{
    public class Chunk
    {
        public int X { get; }
        public int Z { get; }

        public bool Loaded { get; }

        public Block[,,] Blocks { get; }

        public Chunk(int x, int z)
        {
            this.X = x;
            this.Z = z;

            this.Blocks = new Block[16, 16, 16];
            for (int chunkX = 0; chunkX < 16; chunkX++)
            {
                for (int chunkY = 0; chunkY < 16; chunkY++)
                {
                    for (int chunkZ = 0; chunkZ < 16; chunkZ++)
                    {
                        this.Blocks[chunkX, chunkY, chunkZ] = BlockRegistry.GetBlock(Materials.Air);
                    }
                }
            }
        }

        public Block GetBlock(Position position) => this.GetBlock((int)position.X, (int)position.Y, (int)position.Z);

        public Block GetBlock(int x, int y, int z) => this.Blocks[x, y, z];

        public void SetBlock(Position position, Block block) => this.SetBlock((int)position.X, (int)position.Y, (int)position.Z, block);

        public void SetBlock(int x, int y, int z, Block block) => this.Blocks[x, y, z] = block;
    }
}