﻿using System;

namespace Obsidian.API.Containers
{
    //TODO override add item and match fuel to fuel slot
    public sealed class BrewingStand : AbstractResultContainer, ITileEntity
    {
        public string Id => "brewing_stand";

        public Vector? BlockPosition { get; set; }

        public BrewingStand() : base(5, InventoryType.BrewingStand)
        {
            this.Title = "Brewing Stand";
        }

        public void ToNbt() => throw new NotImplementedException();
        public void FromNbt() => throw new NotImplementedException();

        public override void SetResult(ItemStack? result) => throw new NotImplementedException();
        public override ItemStack? GetResult() => throw new NotImplementedException();
    }
}
