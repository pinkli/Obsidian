﻿using Obsidian.Util.Extensions;

namespace Obsidian.Items
{
    public class Enchantment
    {
        internal string Id { get; set; }

        public EnchantmentType Type => this.Id.ToEnchantType();

        public int Level { get; set; }
    }

    public enum EnchantmentType : int
    {
        Protection,
        FireProtection,
        FeatherFalling,
        BlastProtection,
        ProjectileProtection,
        Respiration,
        AquaAffinity,
        Thorns,
        DepthStrider,
        FrostWalker,
        BindingCurse,
        SoulSpeed,
        Sharpness,
        Smite,
        BaneOfArthropods,
        Knockback,
        FireAspect,
        Looting,
        Sweeping,
        Efficiency,
        SilkTouch,
        Unbreaking,
        Fortune,
        Power,
        Punch,
        Flame,
        Infinity,
        LuckOfTheSea,
        Lure,
        Loyalty,
        Impaling,
        Riptide,
        Channeling,
        Multishot,
        QuickCharge,
        Piercing,
        Mending,
        VanishingCurse,
    }
}
