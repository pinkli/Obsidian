﻿using System;

namespace Obsidian.API.Containers
{
    public sealed class Stonecutter : AbstractResultContainer
    {
        public Stonecutter() : base(2, InventoryType.Stonecutter)
        {
            this.Title = "Stonecutter";
        }

        public override ItemStack? GetResult() => throw new NotImplementedException();
        public override void SetResult(ItemStack? result) => throw new NotImplementedException();
    }
}
