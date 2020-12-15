﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Obsidian.Blocks;
using Obsidian.Crafting;
using Obsidian.Entities;
using Obsidian.Items;
using Obsidian.Net.Packets.Play.Clientbound;
using Obsidian.Util.Converters;
using Obsidian.Util.Extensions;
using Obsidian.Util.Registry;
using Obsidian.Util.Registry.Codecs;
using Obsidian.Util.Registry.Codecs.Biomes;
using Obsidian.Util.Registry.Codecs.Dimensions;
using Obsidian.Util.Registry.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Obsidian
{
    public class Registry
    {
        internal static ILogger Logger { get; set; }

        public static Dictionary<Materials, Item> Items = new Dictionary<Materials, Item>();
        public static Dictionary<Materials, Block> Blocks = new Dictionary<Materials, Block>();

        public static Dictionary<string, List<Tag>> Tags = new Dictionary<string, List<Tag>>();

        public static Dictionary<string, IRecipe> Recipes = new Dictionary<string, IRecipe>();

        internal static CodecCollection<int, DimensionCodec> DefaultDimensions { get; } = new CodecCollection<int, DimensionCodec>("minecraft:dimension_type");

        internal static CodecCollection<string, BiomeCodec> DefaultBiomes { get; } = new CodecCollection<string, BiomeCodec>("minecraft:worldgen/biome");

        private readonly static string mainDomain = "Obsidian.Assets";

        private readonly static JsonSerializer recipeSerializer = new JsonSerializer();

        static Registry()
        {
            recipeSerializer.Converters.Add(new IngredientConverter());
            recipeSerializer.Converters.Add(new IngredientsConverter());
            recipeSerializer.Converters.Add(new CraftingKeyConverter());
        }

        public static async Task RegisterBlocksAsync()
        {
            var fs = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{mainDomain}.blocks.json");

            using var read = new StreamReader(fs, new UTF8Encoding(false));

            string json = await read.ReadToEndAsync();

            int registered = 0;

            var type = JObject.Parse(json);

            using (var enumerator = type.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var (blockName, token) = enumerator.Current;

                    var name = blockName.Split(":")[1];

                    var states = JsonConvert.DeserializeObject<BlockJson>(token.ToString(), Globals.JsonSettings);

                    if (!Enum.TryParse(name.Replace("_", ""), true, out Materials material))
                        continue;

                    if (states.States.Length <= 0)
                        continue;

                    int id = 0;
                    foreach (var state in states.States)
                        id = state.Default ? state.Id : states.States.First().Id;

                    Logger.LogDebug($"Registered block: {material} with id: {id}");

                    switch (material)
                    {
                        case Materials.GrassBlock:
                            Blocks.Add(material, new BlockGrass(blockName, id, material));
                            break;
                        case Materials.Podzol:
                            Blocks.Add(material, new BlockDirtSnow(blockName, id, material));
                            break;
                        case Materials.OakSapling:
                            Blocks.Add(material, new BlockSapling(blockName, id, material));
                            break;
                        case Materials.SpruceSapling:
                            Blocks.Add(material, new BlockSapling(blockName, id, material));
                            break;
                        case Materials.BirchSapling:
                            Blocks.Add(material, new BlockSapling(blockName, id, material));
                            break;
                        case Materials.JungleSapling:
                            Blocks.Add(material, new BlockSapling(blockName, id, material));
                            break;
                        case Materials.AcaciaSapling:
                            Blocks.Add(material, new BlockSapling(blockName, id, material));
                            break;
                        case Materials.DarkOakSapling:
                            Blocks.Add(material, new BlockSapling(blockName, id, material));
                            break;
                        case Materials.Water:
                            Blocks.Add(material, new BlockFluid(blockName, id, material));
                            break;
                        case Materials.Lava:
                            Blocks.Add(material, new BlockFluid(blockName, id, material));
                            break;
                        case Materials.OakLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.SpruceLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.BirchLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.JungleLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.AcaciaLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.DarkOakLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.StrippedOakLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.StrippedSpruceLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.StrippedBirchLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.StrippedJungleLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.StrippedAcaciaLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.StrippedDarkOakLog:
                            Blocks.Add(material, new BlockLog(blockName, id, material));
                            break;
                        case Materials.OakWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.SpruceWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.BirchWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.JungleWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.AcaciaWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.DarkOakWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.StrippedOakWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.StrippedSpruceWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.StrippedBirchWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.StrippedJungleWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.StrippedAcaciaWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.StrippedDarkOakWood:
                            Blocks.Add(material, new BlockRotatable(blockName, id, material));
                            break;
                        case Materials.OakLeaves:
                            Blocks.Add(material, new BlockLeaves(blockName, id, material));
                            break;
                        case Materials.SpruceLeaves:
                            Blocks.Add(material, new BlockLeaves(blockName, id, material));
                            break;
                        case Materials.BirchLeaves:
                            Blocks.Add(material, new BlockLeaves(blockName, id, material));
                            break;
                        case Materials.JungleLeaves:
                            Blocks.Add(material, new BlockLeaves(blockName, id, material));
                            break;
                        case Materials.AcaciaLeaves:
                            Blocks.Add(material, new BlockLeaves(blockName, id, material));
                            break;
                        case Materials.DarkOakLeaves:
                            Blocks.Add(material, new BlockLeaves(blockName, id, material));
                            break;
                        case Materials.Sponge:
                            Blocks.Add(material, new BlockSponge(blockName, id, material));
                            break;
                        case Materials.WetSponge:
                            Blocks.Add(material, new BlockSponge(blockName, id, material));
                            break;
                        case Materials.Dispenser:
                            Blocks.Add(material, new BlockDispenser(blockName, id, material));
                            break;
                        case Materials.NoteBlock:
                            Blocks.Add(material, new BlockNote(blockName, id, material));
                            break;
                        case Materials.WhiteBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.OrangeBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.MagentaBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.LightBlueBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.YellowBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.LimeBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.PinkBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.GrayBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.LightGrayBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.CyanBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.PurpleBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.BlueBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.BrownBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.GreenBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.RedBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.BlackBed:
                            Blocks.Add(material, new BlockBed(blockName, id, material));
                            break;
                        case Materials.PoweredRail:
                            Blocks.Add(material, new BlockPoweredRail(blockName, id, material));
                            break;
                        case Materials.DetectorRail:
                            Blocks.Add(material, new BlockMinecartDetector(blockName, id, material));
                            break;
                        case Materials.StickyPiston:
                            Blocks.Add(material, new BlockPiston(blockName, id, material));
                            break;
                        case Materials.Cobweb:
                            Blocks.Add(material, new BlockWeb(blockName, id, material));
                            break;
                        case Materials.Grass:
                            Blocks.Add(material, new BlockLongGrass(blockName, id, material));
                            break;
                        case Materials.Fern:
                            Blocks.Add(material, new BlockLongGrass(blockName, id, material));
                            break;
                        case Materials.DeadBush:
                            Blocks.Add(material, new BlockDeadBush(blockName, id, material));
                            break;
                        case Materials.Seagrass:
                            Blocks.Add(material, new BlockSeaGrass(blockName, id, material));
                            break;
                        case Materials.TallSeagrass:
                            Blocks.Add(material, new BlockTallSeaGrass(blockName, id, material));
                            break;
                        case Materials.Piston:
                            Blocks.Add(material, new BlockPiston(blockName, id, material));
                            break;
                        case Materials.PistonHead:
                            Blocks.Add(material, new BlockPistonExtension(blockName, id, material));
                            break;
                        case Materials.MovingPiston:
                            Blocks.Add(material, new BlockPiston(blockName, id, material));
                            break;
                        case Materials.Dandelion:
                            Blocks.Add(material, new BlockFlower(blockName, id, material));
                            break;
                        case Materials.Poppy:
                            Blocks.Add(material, new BlockFlower(blockName, id, material));
                            break;
                        case Materials.BlueOrchid:
                            Blocks.Add(material, new BlockFlower(blockName, id, material));
                            break;
                        case Materials.Allium:
                            Blocks.Add(material, new BlockFlower(blockName, id, material));
                            break;
                        case Materials.AzureBluet:
                            Blocks.Add(material, new BlockFlower(blockName, id, material));
                            break;
                        case Materials.RedTulip:
                            Blocks.Add(material, new BlockFlower(blockName, id, material));
                            break;
                        case Materials.OrangeTulip:
                            Blocks.Add(material, new BlockFlower(blockName, id, material));
                            break;
                        case Materials.WhiteTulip:
                            Blocks.Add(material, new BlockFlower(blockName, id, material));
                            break;
                        case Materials.PinkTulip:
                            Blocks.Add(material, new BlockFlower(blockName, id, material));
                            break;
                        case Materials.OxeyeDaisy:
                            Blocks.Add(material, new BlockFlower(blockName, id, material));
                            break;
                        case Materials.BrownMushroom:
                            Blocks.Add(material, new BlockMushroom(blockName, id, material));
                            break;
                        case Materials.RedMushroom:
                            Blocks.Add(material, new BlockMushroom(blockName, id, material));
                            break;
                        case Materials.Tnt:
                            Blocks.Add(material, new BlockTnt(blockName, id));
                            break;
                        case Materials.Torch:
                            Blocks.Add(material, new BlockTorch(blockName, id, material));
                            break;
                        case Materials.WallTorch:
                            Blocks.Add(material, new BlockWallTorch(blockName, id));
                            break;
                        case Materials.Fire:
                            Blocks.Add(material, new BlockFire(blockName, id));
                            break;
                        case Materials.Spawner:
                            Blocks.Add(material, new BlockMobSpawner(blockName, id));
                            break;
                        case Materials.OakStairs:
                            Blocks.Add(material, new BlockStairs(blockName, id, material));
                            break;
                        case Materials.Chest:
                            Blocks.Add(material, new Chest(blockName, id, material));
                            break;
                        case Materials.RedstoneWire:
                            Blocks.Add(material, new BlockRedstoneWire(blockName, id));
                            break;
                        case Materials.CraftingTable:
                            Blocks.Add(material, new BlockWorkbench(blockName, id));
                            break;
                        case Materials.Wheat:
                            Blocks.Add(material, new BlockCrops(blockName, id, material));
                            break;
                        case Materials.Farmland:
                            Blocks.Add(material, new BlockSoil(blockName, id));
                            break;
                        case Materials.Furnace:
                            Blocks.Add(material, new BlockFurnace(blockName, id));
                            break;
                        case Materials.AcaciaSign://TODO signs
                            Blocks.Add(material, new BlockFloorSign(blockName, id, material));
                            break;
                        case Materials.BirchSign:
                            Blocks.Add(material, new BlockFloorSign(blockName, id, material));
                            break;
                        case Materials.DarkOakSign:
                            Blocks.Add(material, new BlockFloorSign(blockName, id, material));
                            break;
                        case Materials.JungleSign:
                            Blocks.Add(material, new BlockFloorSign(blockName, id, material));
                            break;
                        case Materials.OakSign:
                            Blocks.Add(material, new BlockFloorSign(blockName, id, material));
                            break;
                        case Materials.SpruceSign:
                            Blocks.Add(material, new BlockFloorSign(blockName, id, material));
                            break;
                        case Materials.OakDoor:
                            Blocks.Add(material, new BlockDoor(blockName, id, material));
                            break;
                        case Materials.Ladder:
                            Blocks.Add(material, new BlockLadder(blockName, id));
                            break;
                        case Materials.Rail:
                            Blocks.Add(material, new BlockMinecartTrack(blockName, id));
                            break;
                        case Materials.CobblestoneStairs:
                            Blocks.Add(material, new BlockStairs(blockName, id, material));
                            break;
                        case Materials.AcaciaWallSign://TODO different wall signs
                            Blocks.Add(material, new BlockWallSign(blockName, id, material));
                            break;
                        case Materials.BirchWallSign:
                            Blocks.Add(material, new BlockWallSign(blockName, id, material));
                            break;
                        case Materials.DarkOakWallSign:
                            Blocks.Add(material, new BlockWallSign(blockName, id, material));
                            break;
                        case Materials.JungleWallSign:
                            Blocks.Add(material, new BlockWallSign(blockName, id, material));
                            break;
                        case Materials.OakWallSign:
                            Blocks.Add(material, new BlockWallSign(blockName, id, material));
                            break;
                        case Materials.SpruceWallSign:
                            Blocks.Add(material, new BlockWallSign(blockName, id, material));
                            break;
                        case Materials.Lever:
                            Blocks.Add(material, new BlockLever(blockName, id));
                            break;
                        case Materials.StonePressurePlate:
                            Blocks.Add(material, new BlockPressurePlate(blockName, id, material));
                            break;
                        case Materials.IronDoor:
                            Blocks.Add(material, new BlockDoor(blockName, id, material));
                            break;
                        case Materials.OakPressurePlate:
                            Blocks.Add(material, new BlockPressurePlate(blockName, id, material));
                            break;
                        case Materials.SprucePressurePlate:
                            Blocks.Add(material, new BlockPressurePlate(blockName, id, material));
                            break;
                        case Materials.BirchPressurePlate:
                            Blocks.Add(material, new BlockPressurePlate(blockName, id, material));
                            break;
                        case Materials.JunglePressurePlate:
                            Blocks.Add(material, new BlockPressurePlate(blockName, id, material));
                            break;
                        case Materials.AcaciaPressurePlate:
                            Blocks.Add(material, new BlockPressurePlate(blockName, id, material));
                            break;
                        case Materials.DarkOakPressurePlate:
                            Blocks.Add(material, new BlockPressurePlate(blockName, id, material));
                            break;
                        case Materials.RedstoneTorch:
                            Blocks.Add(material, new BlockRedstoneTorch(blockName, id, material));
                            break;
                        case Materials.RedstoneWallTorch:
                            Blocks.Add(material, new BlockRedstoneWallTorch(blockName, id));
                            break;
                        case Materials.StoneButton:
                            Blocks.Add(material, new BlockButton(blockName, id, material));
                            break;
                        case Materials.Snow:
                            Blocks.Add(material, new BlockSnow(blockName, id));
                            break;
                        case Materials.Ice:
                            Blocks.Add(material, new BlockIce(blockName, id, material));
                            break;
                        case Materials.Cactus:
                            Blocks.Add(material, new BlockCactus(blockName, id));
                            break;
                        case Materials.Clay:
                            Blocks.Add(material, new Block(blockName, id, material));
                            break;
                        case Materials.SugarCane:
                            Blocks.Add(material, new BlockReed(blockName, id, material));
                            break;
                        case Materials.Jukebox:
                            Blocks.Add(material, new BlockJukebox(blockName, id));
                            break;
                        case Materials.OakFence:
                            Blocks.Add(material, new BlockFence(blockName, id, material));
                            break;
                        case Materials.Pumpkin:
                            Blocks.Add(material, new BlockPumpkin(blockName, id));
                            break;
                        case Materials.SoulSand:
                            Blocks.Add(material, new BlockSlowSand(blockName, id));
                            break;
                        case Materials.NetherPortal:
                            Blocks.Add(material, new BlockPortal(blockName, id));
                            break;
                        case Materials.CarvedPumpkin:
                            Blocks.Add(material, new BlockPumpkinCarved(blockName, id, material));
                            break;
                        case Materials.JackOLantern:
                            Blocks.Add(material, new BlockPumpkinCarved(blockName, id, material));
                            break;
                        case Materials.Cake:
                            Blocks.Add(material, new BlockCake(blockName, id));
                            break;
                        case Materials.Repeater:
                            Blocks.Add(material, new BlockRepeater(blockName, id));
                            break;
                        case Materials.WhiteStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.OrangeStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.MagentaStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.LightBlueStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.YellowStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.LimeStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.PinkStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.GrayStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.LightGrayStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.CyanStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.PurpleStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.BlueStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.BrownStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.GreenStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.RedStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.BlackStainedGlass:
                            Blocks.Add(material, new BlockStainedGlass(blockName, id, material));
                            break;
                        case Materials.OakTrapdoor:
                            Blocks.Add(material, new BlockTrapdoor(blockName, id, material));
                            break;
                        case Materials.SpruceTrapdoor:
                            Blocks.Add(material, new BlockTrapdoor(blockName, id, material));
                            break;
                        case Materials.BirchTrapdoor:
                            Blocks.Add(material, new BlockTrapdoor(blockName, id, material));
                            break;
                        case Materials.JungleTrapdoor:
                            Blocks.Add(material, new BlockTrapdoor(blockName, id, material));
                            break;
                        case Materials.AcaciaTrapdoor:
                            Blocks.Add(material, new BlockTrapdoor(blockName, id, material));
                            break;
                        case Materials.DarkOakTrapdoor:
                            Blocks.Add(material, new BlockTrapdoor(blockName, id, material));
                            break;
                        case Materials.InfestedStone:
                            Blocks.Add(material, new BlockMonsterEggs(blockName, id, material));
                            break;
                        case Materials.InfestedCobblestone:
                            Blocks.Add(material, new BlockMonsterEggs(blockName, id, material));
                            break;
                        case Materials.InfestedStoneBricks:
                            Blocks.Add(material, new BlockMonsterEggs(blockName, id, material));
                            break;
                        case Materials.InfestedMossyStoneBricks:
                            Blocks.Add(material, new BlockMonsterEggs(blockName, id, material));
                            break;
                        case Materials.InfestedCrackedStoneBricks:
                            Blocks.Add(material, new BlockMonsterEggs(blockName, id, material));
                            break;
                        case Materials.InfestedChiseledStoneBricks:
                            Blocks.Add(material, new BlockMonsterEggs(blockName, id, material));
                            break;
                        case Materials.BrownMushroomBlock:
                            Blocks.Add(material, new BlockHugeMushroom(blockName, id, material));
                            break;
                        case Materials.RedMushroomBlock:
                            Blocks.Add(material, new BlockHugeMushroom(blockName, id, material));
                            break;
                        case Materials.MushroomStem:
                            Blocks.Add(material, new BlockHugeMushroom(blockName, id, material));
                            break;
                        case Materials.IronBars:
                            Blocks.Add(material, new BlockIronBars(blockName, id));
                            break;
                        case Materials.GlassPane:
                            Blocks.Add(material, new Block(blockName, id, material));
                            break;
                        case Materials.Melon:
                            Blocks.Add(material, new BlockMelon(blockName, id));
                            break;
                        case Materials.AttachedPumpkinStem:
                            Blocks.Add(material, new BlockStemAttached(blockName, id, material));
                            break;
                        case Materials.AttachedMelonStem:
                            Blocks.Add(material, new BlockStemAttached(blockName, id, material));
                            break;
                        case Materials.PumpkinStem:
                            Blocks.Add(material, new BlockStem(blockName, id, material));
                            break;
                        case Materials.MelonStem:
                            Blocks.Add(material, new BlockStem(blockName, id, material));
                            break;
                        case Materials.Vine:
                            Blocks.Add(material, new BlockVine(blockName, id));
                            break;
                        case Materials.OakFenceGate:
                            Blocks.Add(material, new BlockFenceGate(blockName, id, material));
                            break;
                        case Materials.BrickStairs:
                            Blocks.Add(material, new BlockStairs(blockName, id, material));
                            break;
                        case Materials.StoneBrickStairs:
                            Blocks.Add(material, new BlockStairs(blockName, id, material));
                            break;
                        case Materials.Mycelium:
                            Blocks.Add(material, new BlockMycel(blockName, id));
                            break;
                        case Materials.LilyPad:
                            Blocks.Add(material, new BlockWaterLily(blockName, id));
                            break;
                        case Materials.NetherBrickFence:
                            Blocks.Add(material, new BlockFence(blockName, id, material));
                            break;
                        case Materials.NetherBrickStairs:
                            Blocks.Add(material, new BlockStairs(blockName, id, material));
                            break;
                        case Materials.NetherWart:
                            Blocks.Add(material, new BlockNetherWart(blockName, id));
                            break;
                        case Materials.EnchantingTable:
                            Blocks.Add(material, new BlockEnchantingTable(blockName, id));
                            break;
                        case Materials.BrewingStand:
                            Blocks.Add(material, new BlockBrewingStand(blockName, id));
                            break;
                        case Materials.Cauldron:
                            Blocks.Add(material, new BlockCauldron(blockName, id));
                            break;
                        default:
                            Blocks.Add(material, new Block(blockName, id, material));
                            break;
                    }

                    foreach (var state in states.States)
                    {
                        if (id == state.Id)
                            continue;

                        Blocks[material].States.Add(new BlockState(state.Id));
                    }
                    registered++;
                }
            }

            Logger.LogDebug($"Successfully registered {registered} blocks..");
        }

        public static async Task RegisterItemsAsync()
        {
            var fs = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{mainDomain}.items.json");

            using var read = new StreamReader(fs, new UTF8Encoding(false));

            var json = await read.ReadToEndAsync();

            var type = JObject.Parse(json);

            using var enumerator = type.GetEnumerator();
            int registered = 0;

            while (enumerator.MoveNext())
            {
                var (name, token) = enumerator.Current;

                var itemName = name.Split(":")[1];

                var item = JsonConvert.DeserializeObject<BaseRegistryJson>(token.ToString());

                if (!Enum.TryParse(itemName.Replace("_", ""), true, out Materials material))
                    continue;

                Logger.LogDebug($"Registered item: {material} with id: {item.ProtocolId}");

                Items.Add(material, new Item((short)item.ProtocolId, name, material));
                registered++;
            }

            Logger.LogDebug($"Successfully registered {registered} items..");
        }

        public static async Task RegisterBiomesAsync()
        {
            using var cfs = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{mainDomain}.biome_dimension_codec.json");

            using var cread = new StreamReader(cfs, new UTF8Encoding(false));

            var json = await cread.ReadToEndAsync();

            var type = JObject.Parse(json);

            using var cenumerator = type.GetEnumerator();

            var registered = 0;
            while (cenumerator.MoveNext())
            {
                var (name, token) = cenumerator.Current;

                foreach (var obj in token)
                {
                    var val = obj.ToString();
                    var codec = JsonConvert.DeserializeObject<BiomeCodec>(val, Globals.JsonSettings);

                    DefaultBiomes.TryAdd(codec.Name, codec);

                    Logger.LogDebug($"Added codec: {codec.Name}:{codec.Id}");
                    registered++;
                }
            }
            Logger.LogDebug($"Successfully registered {registered} codec biomes");
        }

        public static async Task RegisterDimensionsAsync()
        {
            using var cfs = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{mainDomain}.default_dimensions.json");

            using var cread = new StreamReader(cfs, new UTF8Encoding(false));

            var json = await cread.ReadToEndAsync();

            var type = JObject.Parse(json);

            using var cenumerator = type.GetEnumerator();

            var registered = 0;
            while (cenumerator.MoveNext())
            {
                var (name, token) = cenumerator.Current;

                foreach (var obj in token)
                {
                    var val = obj.ToString();
                    var codec = JsonConvert.DeserializeObject<DimensionCodec>(val, Globals.JsonSettings);

                    DefaultDimensions.TryAdd(codec.Id, codec);

                    Logger.LogDebug($"Added codec: {codec.Name}:{codec.Id}");
                    registered++;
                }
            }
            Logger.LogDebug($"Successfully registered {registered} codec dimensions");
        }

        public static async Task RegisterTagsAsync()
        {
            var registered = 0;

            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{mainDomain}.tags.json");

            using var reader = new StreamReader(stream, new UTF8Encoding(false));

            var element = JObject.Parse(await reader.ReadToEndAsync());

            using var enu = element.GetEnumerator();

            static void addValues(string tagBase, Tag tag, List<string> values)
            {
                foreach (var value in values)
                {
                    switch (tagBase)
                    {
                        case "items":
                            var item = GetItem(value);

                            tag.Entries.Add(item.Id);
                            break;
                        case "blocks":
                            var block = GetBlock(value);

                            tag.Entries.Add(block.Id);
                            break;
                        case "entity_types":
                            Enum.TryParse<EntityType>(value.Replace("minecraft:", "").ToCamelCase().ToLower(), true, out var type);
                            tag.Entries.Add((int)type);
                            break;
                        case "fluids":
                            Enum.TryParse<Fluids>(value.Replace("minecraft:", "").ToCamelCase().ToLower(), true, out var fluid);
                            tag.Entries.Add((int)fluid);
                            break;
                        default:
                            break;
                    }
                }
            }

            while (enu.MoveNext())
            {
                var (name, token) = enu.Current;

                var split = name.Split('/');

                var tagBase = split[0];
                var tagName = split[1];

                if (Tags.ContainsKey(tagBase))
                {
                    var tag = new Tag
                    {
                        Type = tagBase,
                        Name = tagName
                    };

                    var array = token.Value<JArray>("values");
                    var values = array.ToObject<List<string>>();

                    addValues(tagBase, tag, values);

                    Logger.LogDebug($"Registered tag: {name} with {tag.Count} entries");

                    Tags[tagBase].Add(tag);
                }
                else
                {
                    var tag = new Tag
                    {
                        Type = tagBase,
                        Name = tagName
                    };

                    var array = token.Value<JArray>("values");
                    var values = array.ToObject<List<string>>();

                    addValues(tagBase, tag, values);

                    Logger.LogDebug($"Registered tag: {name} with {tag.Count} entries");

                    Tags[tagBase] = new List<Tag> { tag };
                }

                registered++;
            }

            Logger.LogDebug($"Registered {registered} tags");
        }

        public static async Task RegisterRecipesAsync()
        {
            using var fs = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{mainDomain}.recipes.json");

            using var sw = new StreamReader(fs, new UTF8Encoding(false));

            var json = await sw.ReadToEndAsync();

            var jObject = JObject.Parse(json);

            var enu = jObject.GetEnumerator();

            while (enu.MoveNext())
            {
                var (name, element) = enu.Current;

                var type = element.Value<string>("type").Replace("minecraft:", "").ToCamelCase().ToLower();

                if (Enum.TryParse<CraftingType>(type, true, out var result))
                {
                    switch (result)
                    {
                        case CraftingType.CraftingShaped:
                            Recipes.Add(name, element.ToObject<ShapedRecipe>(recipeSerializer));
                            break;
                        case CraftingType.CraftingShapeless:
                            Recipes.Add(name, element.ToObject<ShapelessRecipe>(recipeSerializer));
                            break;
                        case CraftingType.CraftingSpecialArmordye:
                        case CraftingType.CraftingSpecialBookcloning:
                        case CraftingType.CraftingSpecialMapcloning:
                        case CraftingType.CraftingSpecialMapextending:
                        case CraftingType.CraftingSpecialFireworkRocket:
                        case CraftingType.CraftingSpecialFireworkStar:
                        case CraftingType.CraftingSpecialFireworkStarFade:
                        case CraftingType.CraftingSpecialTippedarrow:
                        case CraftingType.CraftingSpecialBannerduplicate:
                        case CraftingType.CraftingSpecialShielddecoration:
                        case CraftingType.CraftingSpecialShulkerboxcoloring:
                        case CraftingType.CraftingSpecialSuspiciousstew:
                        case CraftingType.CraftingSpecialRepairitem:
                            break;
                        case CraftingType.Smelting:
                        case CraftingType.Blasting:
                        case CraftingType.Smoking:
                        case CraftingType.CampfireCooking:
                            Recipes.Add(name, element.ToObject<SmeltingRecipe>(recipeSerializer));
                            break;
                        case CraftingType.Stonecutting:
                            Recipes.Add(name, element.ToObject<CuttingRecipe>(recipeSerializer));
                            break;
                        case CraftingType.Smithing:
                            Recipes.Add(name, element.ToObject<SmithingRecipe>(recipeSerializer));
                            break;
                        default:
                            break;
                    }
                }
            }

            Logger.LogDebug($"Registered {Recipes.Count} recipes");
        }

        public static Block GetBlock(Materials mat) => Blocks.GetValueOrDefault(mat);

        public static Block GetBlock(int id) => Blocks.Values.SingleOrDefault(x => x.Id == id);

        public static Block GetBlock(string unlocalizedName) =>
            Blocks.Values.SingleOrDefault(x => x.UnlocalizedName.EqualsIgnoreCase(unlocalizedName));

        public static Item GetItem(int id) => Items.Values.SingleOrDefault(x => x.Id == id);
        public static Item GetItem(Materials mat) => Items.GetValueOrDefault(mat);
        public static Item GetItem(string unlocalizedName) =>
            Items.Values.SingleOrDefault(x => x.UnlocalizedName.EqualsIgnoreCase(unlocalizedName));

        public static ItemStack GetSingleItem(Materials mat, ItemMeta? meta = null) => new ItemStack(GetItem(mat).Id, 1, meta);

        public static ItemStack GetSingleItem(string unlocalizedName, ItemMeta? meta = null) => new ItemStack(GetItem(unlocalizedName).Id, 1, meta);

        class BaseRegistryJson
        {
            [JsonProperty("protocol_id")]
            public int ProtocolId { get; set; }
        }

    }

    public class DomainTag
    {
        public string TagName { get; set; }

        public string BaseTagName { get; set; }
    }

}
