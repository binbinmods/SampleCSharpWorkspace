using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
// using static Obeliskial_Essentials.Essentials;
using System;
// using static SeedSearcher.CustomFunctions;
using static SeedSearcher.Plugin;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Analytics;
using System.Text;
using System.Text.RegularExpressions;

// Make sure your namespace is the same everywhere
namespace SeedSearcher
{

    [HarmonyPatch] //DO NOT REMOVE/CHANGE

    public class SeedSearcherFunctions
    {
        // To create a patch, you need to declare either a prefix or a postfix. 
        // Prefixes are executed before the original code, postfixes are executed after
        // Then you need to tell Harmony which method to patch.

        // public static 

#pragma warning disable Harmony003 // Harmony non-ref patch parameters modified
        public static Dictionary<string, string> allLootLocations = new() {
            // {"towntier3", "voidlow_1"}, {"towntier3_a", "voidlow_1"}, {"towntier3_b", "voidlow_1"}, {"ruinedplaza", "voidlow_5"}, {"ruinedplaza_crit", "voidlow_5"}, {"voidasmody", "voidhigh_6"}, {"voidshop", "voidlow_8"}, {"voidtreasure", "voidlow_16"}, {"voidtreasurejade", "voidlow_16"}, {"voidtsnemo", "voidlow_24"}, {"voidtwins", "voidlow_25"}, {"wareacc1", "voidlow_4"}, {"wareacc2", "voidlow_4"}, {"warearm1", "voidlow_4"}, {"warearm2", "voidlow_4"}, {"warejew1", "voidlow_4"}, {"warejew2", "voidlow_4"}, {"warenavalea", "voidlow_4"}, {"wareweap1", "voidlow_4"}, {"wareweap2", "voidlow_4"}, {"aquarvendor","aqua_26"}, {"elvenemporium","faen_23"}, {"felineshop","ulmin_29"}, {"firebazaar","velka_20"}, {"heronshop","ulmin_27"}, {"Merchantwares","velka_7"}, {"voidshop","voidlow_8"}, {"voidtsnemo","voidlow_24"}, {"voodooshop", "aqua_23"}, {"Jeweler","sen_35"}, {"gobletsmerchant","sewers_4"}, {"werewolfstall_a","sen_30"}, {"werewolfstall_b","sen_30"}, {"FrozenSewersElves", "sewers_11"}, {"FrozenSewersMix", "sewers_11"}, {"FrozenSewersRatmen", "sewers_11"}, {"Jewelerrings", "sen_35"}, {"Mansionleft", "faen_35"}, {"Mansionleftplus", "faen_35"}, {"Sailor", "faen_4"}, {"Stolenitems", "faen_10"}, 
            // {"apprentince", "sen_24"}, {"balanceblack", "ulmin_52"}, {"balanceboth", "ulmin_52"}, {"balancewhite", "ulmin_52"}, {"battlefield", "aqua_6"}, {"bridge", "sen_17"}, {"chappel", "sen_21"}, {"crane", "velka_21"}, {"crocoloot", "aqua_7"}, {"crocosmugglers", "aqua_48"}, {"desertreliquary", "ulmin_58"}, {"dreadcosme", "dread_2"}, {"dreadcuthbert", "dread_2"}, {"dreadfrancis", "dread_2"}, {"dreadhoratio", "dread_2"}, {"dreadjack", "dread_2"}, {"dreadmimic", "dread_10"},                                
            // {"dreadrhodogor", "dread_2"}, {"eeriechest_a", "sen_6"}, {"eeriechest_b", "sen_6"}, {"lavacascade", "velka_22"}, {"lootedarmory", "pyr_10"}, {"obsidianall", "forge_3"}, {"obsidiananvil", "forge_3"}, {"obsidianboots", "forge_3"}, {"obsidianrings", "forge_3"}, {"obsidianrods", "forge_3"}, {"rift", "velka_40"}, {"riftsen", "sen_48"}, {"sahtibernardstash", "sahti_52"}, {"sahtidomedesert", "sahti_49"}, {"sahtidomeice", "sahti_49"}, {"sahtidomemain", "sahti_49"}, {"sahtidomemountain", "sahti_49"}, {"sahtidomeswamp", "sahti_49"}, {"sahtipiratearsenal", "sahti_25"}, {"sahtipiratewarehouse", "sahti_25"}, {"sahtiplaguecot", "sahti_11"}, {"sahtisurgeonarsenal", "sahti_13"}, {"sahtisurgeonstash", "sahti_13"}, {"sahtitreasurechamber", "sahti_25"}, {"sahtivalkyriestash", "sahti_19"}, {"thorimsrod", "forge_8"}, {"treasureaquarfall", "aqua_28"}, {"uprienergy", "uprising_5"}, {"uprimagma", "uprising_4"}, {"voidcraftemerald", "voidlow_14"}, {"voidcraftgolden", "voidlow_14"}, {"voidcraftobsidian", "voidlow_14"}, {"voidcraftpearl", "voidlow_14"}, {"voidcraftring", "voidlow_14"}, {"voidcraftruby", "voidlow_14"}, {"voidcraftsapphire", "voidlow_14"}, {"voidcrafttopaz", "voidlow_14"}, {"voidtreasurejade", "voidlow_16"}, {"watermill", "sen_15"},
            // {"Jelly", "aqua_41"}, {"betty", "sen_39"}, {"blobbleed", "sen_29"}, {"blobcold", "sewers_13"}, {"blobdire", "velka_37"}, {"blobholy", "pyr_12"}, {"bloblightning", "sahti_30"}, {"blobmetal", "velka_38"}, {"blobmind", "dread_7"}, {"blobpoison", "aqua_44"}, {"blobselem", "voidlow_28"}, {"blobshadow", "ulmin_59"}, {"blobsmyst", "voidlow_28"}, {"blobsphys", "voidlow_28"}, {"blobwater", "aqua_45"}, {"cuby", "pyr_9"}, {"cubyd", "pyr_9"}, {"daley", "faen_7"}, {"fenny", "ulmin_6"}, {"fishlava", "forge_8"}, {"inky", "aqua_41"}, {"liante", "spider_5"}, {"matey", "sahti_20"}, {"mimy", "dread_10"}, {"oculy", "aqua_41"}, {"sharpy", "aqua_18"}, {"slimy", "spider_7"}, {"stormy", "aqua_24"}, {"wolfy", "sen_46"},
            {"voidtsnemo", "voidlow_24"},{"voidshop", "voidlow_8"},{"rubychest","aqua_20"},{"rubyrefuges","aqua_20"},{"caravanshop", "sen_44"},{"Basthet", "pyr_7"}, {"Dryad_a", "sen_31"}, {"Dryad_b", "sen_32"}, {"Faeborg", "faen_38"}, {"Hydra", "aqua_35"}, {"Ignidoh", "velka_32"}, {"Mansionright", "faen_35"}, {"Mortis", "ulmin_56"}, {"Tulah", "spider_8"}, {"Ylmer", "sen_33"}, {"belphyor", "secta_5"}, {"belphyorquest", "velka_8"}, {"burninghand", "velka_25"}, {"dreadhalfman", "dread_11"}, {"elvenarmory", "faen_29"}, {"elvenarmoryplus", "faen_29"}, {"goblintown", "velka_6"}, {"harpychest", "velka_27"}, {"harpyfight", "velka_27"}, {"khazakdhum", "velka_29"}, {"kingrat", "sewers_8"}, {"minotaurcave", "velka_9"}, {"sahtikraken", "sahti_65"}, {"sahtikrakenmjolmir", "sahti_65"}, {"sahtirustkingtreasure", "sahti_62"}, {"spiderqueen", "sen_1"}, {"tyrant", "faen_25"}, {"tyrantbeavers", "faen_25"}, {"tyrantchampy", "faen_25"}, {"tyrantchompy", "faen_25"}, {"tyrantchumpy", "faen_25"}, {"upripreboss", "uprising_13"}, {"yogger", "sen_27"},{"voidtwins", "voidlow_25"},{"voidtreasurejade", "voidlow_16"}
        };

        public static Dictionary<string, string> guaranteedDropNodeMap = new()
            {
                {"rubychest","aqua_20"},
                {"rubyrefuges","aqua_20"},
                {"FrozenSewersElves", "sewers_11"},
                {"FrozenSewersMix", "sewers_11"},
                {"FrozenSewersRatmen", "sewers_11"},
                {"Jewelerrings", "sen_35"},
                {"Mansionleft", "faen_35"},
                {"Mansionleftplus", "faen_35"},
                {"Sailor", "faen_4"},
                // {"Shady", "sen_1"},
                {"Stolenitems", "faen_10"},
                {"apprentince", "sen_24"},
                {"balanceblack", "ulmin_52"},
                {"balanceboth", "ulmin_52"},
                {"balancewhite", "ulmin_52"},
                {"battlefield", "aqua_6"},
                {"bridge", "sen_17"},
                {"chappel", "sen_21"},
                {"crane", "velka_21"},
                {"crocoloot", "aqua_7"},
                {"crocosmugglers", "aqua_48"},
                {"desertreliquary", "ulmin_58"},
                {"dreadcosme", "dread_2"},
                {"dreadcuthbert", "dread_2"},
                {"dreadfrancis", "dread_2"},
                {"dreadhoratio", "dread_2"},
                {"dreadjack", "dread_2"},
                {"dreadmimic", "dread_10"},
                // {"dreadpurser", "sen_1"},
                // {"dreadpurserstash", "sen_1"},
                {"dreadrhodogor", "dread_2"},
                {"eeriechest_a", "sen_6"},
                {"eeriechest_b", "sen_6"},
                {"lavacascade", "velka_22"},
                {"lootedarmory", "pyr_10"},
                // {"lootsepulchralrare", "voidlow_18"},
                {"obsidianall", "forge_3"},
                {"obsidiananvil", "forge_3"},
                {"obsidianboots", "forge_3"},
                {"obsidianrings", "forge_3"},
                {"obsidianrods", "forge_3"},
                {"rift", "velka_40"},
                {"riftsen", "sen_48"},
                {"sahtibernardstash", "sahti_52"},
                {"sahtidomedesert", "sahti_49"},
                {"sahtidomeice", "sahti_49"},
                {"sahtidomemain", "sahti_49"},
                {"sahtidomemountain", "sahti_49"},
                {"sahtidomeswamp", "sahti_49"},
                {"sahtipiratearsenal", "sahti_25"},
                {"sahtipiratewarehouse", "sahti_25"},
                {"sahtiplaguecot", "sahti_11"},
                {"sahtisurgeonarsenal", "sahti_13"},
                {"sahtisurgeonstash", "sahti_13"},
                {"sahtitreasurechamber", "sahti_25"},
                {"sahtivalkyriestash", "sahti_19"},
                {"thorimsrod", "forge_8"},
                {"treasureaquarfall", "aqua_28"},
                {"uprienergy", "uprising_5"},
                {"uprimagma", "uprising_4"},
                {"voidcraftemerald", "voidlow_14"},
                {"voidcraftgolden", "voidlow_14"},
                {"voidcraftobsidian", "voidlow_14"},
                {"voidcraftpearl", "voidlow_14"},
                {"voidcraftring", "voidlow_14"},
                // {"voidcraftrodsofblasting", "sen_1"},
                {"voidcraftruby", "voidlow_14"},
                {"voidcraftsapphire", "voidlow_14"},
                {"voidcrafttopaz", "voidlow_14"},
                {"voidtreasurejade", "voidlow_16"},
                {"watermill", "sen_15"},
            };

        public static Dictionary<string, string> MythicLocations = new()
            {
                {"towntier3", "voidlow_1"},
                {"towntier3_a", "voidlow_1"},
                {"towntier3_b", "voidlow_1"},
                {"ruinedplaza", "voidlow_5"},
                {"ruinedplaza_crit", "voidlow_5"},
                {"voidasmody", "voidhigh_6"},
                {"voidshop", "voidlow_8"},
                {"voidtreasure", "voidlow_16"},
                {"voidtreasurejade", "voidlow_16"},
                {"voidtsnemo", "voidlow_24"},
                {"voidtwins", "voidlow_25"},
                {"wareacc1", "voidlow_4"},
                {"wareacc2", "voidlow_4"},
                {"warearm1", "voidlow_4"},
                {"warearm2", "voidlow_4"},
                {"warejew1", "voidlow_4"},
                {"warejew2", "voidlow_4"},
                {"warenavalea", "voidlow_4"},
                {"wareweap1", "voidlow_4"},
                {"wareweap2", "voidlow_4"},

            };

        public static Dictionary<string, string> allItemDropLocations = new()
            {
                {"towntier3", "voidlow_1"},
                {"towntier3_a", "voidlow_1"},
                {"towntier3_b", "voidlow_1"},
                {"ruinedplaza", "voidlow_5"},
                {"ruinedplaza_crit", "voidlow_5"},
                {"voidasmody", "voidhigh_6"},
                {"voidshop", "voidlow_8"},
                {"voidtreasure", "voidlow_16"},
                {"voidtreasurejade", "voidlow_16"},
                {"voidtsnemo", "voidlow_24"},
                {"voidtwins", "voidlow_25"},
                {"wareacc1", "voidlow_4"},
                {"wareacc2", "voidlow_4"},
                {"warearm1", "voidlow_4"},
                {"warearm2", "voidlow_4"},
                {"warejew1", "voidlow_4"},
                {"warejew2", "voidlow_4"},
                {"warenavalea", "voidlow_4"},
                {"wareweap1", "voidlow_4"},
                {"wareweap2", "voidlow_4"},

                {"aquarvendor","aqua_26"},
                {"elvenemporium","faen_23"},
                {"felineshop","ulmin_29"},
                {"firebazaar","velka_20"},
                {"heronshop","ulmin_27"},
                {"Merchantwares","velka_7"},
                {"voodooshop", "aqua_23"},
                {"Jeweler","sen_35"},
                {"gobletsmerchant","sewers_4"},
                {"werewolfstall_a","sen_30"},
                {"werewolfstall_b","sen_30"},

                {"FrozenSewersElves", "sewers_11"},
                {"FrozenSewersMix", "sewers_11"},
                {"FrozenSewersRatmen", "sewers_11"},
                {"Jewelerrings", "sen_35"},
                {"Mansionleft", "faen_35"},
                {"Mansionleftplus", "faen_35"},
                {"Sailor", "faen_4"},
                // {"Shady", "sen_1"},
                {"Stolenitems", "faen_10"},
                {"apprentince", "sen_24"},
                {"balanceblack", "ulmin_52"},
                {"balanceboth", "ulmin_52"},
                {"balancewhite", "ulmin_52"},
                {"battlefield", "aqua_6"},
                {"bridge", "sen_17"},
                {"chappel", "sen_21"},
                {"crane", "velka_21"},
                {"crocoloot", "aqua_7"},
                {"crocosmugglers", "aqua_48"},
                {"desertreliquary", "ulmin_58"},
                {"dreadcosme", "dread_2"},
                {"dreadcuthbert", "dread_2"},
                {"dreadfrancis", "dread_2"},
                {"dreadhoratio", "dread_2"},
                {"dreadjack", "dread_2"},
                {"dreadmimic", "dread_10"},
                // {"dreadpurser", "sen_1"},
                // {"dreadpurserstash", "sen_1"},
                {"dreadrhodogor", "dread_2"},
                {"eeriechest_a", "sen_6"},
                {"eeriechest_b", "sen_6"},
                {"lavacascade", "velka_22"},
                {"lootedarmory", "pyr_10"},
                // {"lootsepulchralrare", "voidlow_18"},
                {"obsidianall", "forge_3"},
                {"obsidiananvil", "forge_3"},
                {"obsidianboots", "forge_3"},
                {"obsidianrings", "forge_3"},
                {"obsidianrods", "forge_3"},
                {"rift", "velka_40"},
                {"riftsen", "sen_48"},
                {"sahtibernardstash", "sahti_52"},
                {"sahtidomedesert", "sahti_49"},
                {"sahtidomeice", "sahti_49"},
                {"sahtidomemain", "sahti_49"},
                {"sahtidomemountain", "sahti_49"},
                {"sahtidomeswamp", "sahti_49"},
                {"sahtipiratearsenal", "sahti_25"},
                {"sahtipiratewarehouse", "sahti_25"},
                {"sahtiplaguecot", "sahti_11"},
                {"sahtisurgeonarsenal", "sahti_13"},
                {"sahtisurgeonstash", "sahti_13"},
                {"sahtitreasurechamber", "sahti_25"},
                {"sahtivalkyriestash", "sahti_19"},
                {"thorimsrod", "forge_8"},
                {"treasureaquarfall", "aqua_28"},
                {"uprienergy", "uprising_5"},
                {"uprimagma", "uprising_4"},
                {"voidcraftemerald", "voidlow_14"},
                {"voidcraftgolden", "voidlow_14"},
                {"voidcraftobsidian", "voidlow_14"},
                {"voidcraftpearl", "voidlow_14"},
                {"voidcraftring", "voidlow_14"},
                // {"voidcraftrodsofblasting", "sen_1"},
                {"voidcraftruby", "voidlow_14"},
                {"voidcraftsapphire", "voidlow_14"},
                {"voidcrafttopaz", "voidlow_14"},
                {"watermill", "sen_15"},

                {"Jelly", "aqua_41"},
                {"betty", "sen_39"},
                {"blobbleed", "sen_29"},
                {"blobcold", "sewers_13"},
                {"blobdire", "velka_37"},
                {"blobholy", "pyr_12"},
                {"bloblightning", "sahti_30"},
                {"blobmetal", "velka_38"},
                {"blobmind", "dread_7"},
                {"blobpoison", "aqua_44"},
                {"blobselem", "voidlow_28"},
                {"blobshadow", "ulmin_59"},
                {"blobsmyst", "voidlow_28"},
                {"blobsphys", "voidlow_28"},
                {"blobwater", "aqua_45"},
                {"cuby", "pyr_9"},
                {"cubyd", "pyr_9"},
                {"daley", "faen_7"},
                {"fenny", "ulmin_6"},
                {"fishlava", "forge_8"},
                {"inky", "aqua_41"},
                {"liante", "spider_5"},
                {"matey", "sahti_20"},
                {"mimy", "dread_10"},
                {"oculy", "aqua_41"},
                {"sharpy", "aqua_18"},
                {"slimy", "spider_7"},
                {"stormy", "aqua_24"},
                {"wolfy", "sen_46"},

                {"Basthet", "pyr_7"},
                {"Dryad_a", "sen_31"},
                {"Dryad_b", "sen_32"},
                {"Faeborg", "faen_38"},
                {"Hydra", "aqua_35"},
                {"Ignidoh", "velka_32"},
                {"Mansionright", "faen_35"},
                {"Mortis", "ulmin_56"},
                {"Tulah", "spider_8"},
                {"Ylmer", "sen_33"},
                {"belphyor", "secta_5"},
                {"belphyorquest", "velka_8"},
                {"burninghand", "velka_25"},
                {"dreadhalfman", "dread_11"},
                {"elvenarmory", "faen_29"},
                {"elvenarmoryplus", "faen_29"},
                {"goblintown", "velka_6"},
                {"harpychest", "velka_27"},
                {"harpyfight", "velka_27"},
                {"khazakdhum", "velka_29"},
                {"kingrat", "sewers_8"},
                {"minotaurcave", "velka_9"},
                {"sahtikraken", "sahti_65"},
                {"sahtikrakenmjolmir", "sahti_65"},
                {"sahtirustkingtreasure", "sahti_62"},
                {"spiderqueen", "sen_1"},
                {"tyrant", "faen_25"},
                {"tyrantbeavers", "faen_25"},
                {"tyrantchampy", "faen_25"},
                {"tyrantchompy", "faen_25"},
                {"tyrantchumpy", "faen_25"},
                {"upripreboss", "uprising_13"},
                {"yogger", "sen_27"},
            };

        public static Dictionary<string, string> petDropNodeMap = new()
            {
                {"Jelly", "aqua_41"},
                {"betty", "sen_39"},
                {"blobbleed", "sen_29"},
                {"blobcold", "sewers_13"},
                {"blobdire", "velka_37"},
                {"blobholy", "pyr_12"},
                {"bloblightning", "sahti_30"},
                {"blobmetal", "velka_38"},
                {"blobmind", "dread_7"},
                {"blobpoison", "aqua_44"},
                {"blobselem", "voidlow_28"},
                {"blobshadow", "ulmin_59"},
                {"blobsmyst", "voidlow_28"},
                {"blobsphys", "voidlow_28"},
                {"blobwater", "aqua_45"},
                {"cuby", "pyr_9"},
                {"cubyd", "pyr_9"},
                {"daley", "faen_7"},
                {"fenny", "ulmin_6"},
                {"fishlava", "forge_8"},
                {"inky", "aqua_41"},
                {"liante", "spider_5"},
                {"matey", "sahti_20"},
                {"mimy", "dread_10"},
                {"oculy", "aqua_41"},
                {"sharpy", "aqua_18"},
                {"slimy", "spider_7"},
                {"stormy", "aqua_24"},
                {"wolfy", "sen_46"},

            };
        public static Dictionary<string, string> dropNodeMap = new()
            {
                {"Basthet", "pyr_7"},
                {"Dryad_a", "sen_31"},
                {"Dryad_b", "sen_32"},
                {"Faeborg", "faen_38"},
                {"Hydra", "aqua_35"},
                {"Ignidoh", "velka_32"},
                {"Mansionright", "faen_35"},
                {"Mortis", "ulmin_56"},
                {"Tulah", "spider_8"},
                {"Ylmer", "sen_33"},
                {"belphyor", "secta_5"},
                {"belphyorquest", "velka_8"},
                {"burninghand", "velka_25"},
                {"dreadhalfman", "dread_11"},
                {"elvenarmory", "faen_29"},
                {"elvenarmoryplus", "faen_29"},
                {"goblintown", "velka_6"},
                {"harpychest", "velka_27"},
                {"harpyfight", "velka_27"},
                {"khazakdhum", "velka_29"},
                {"kingrat", "sewers_8"},
                {"minotaurcave", "velka_9"},
                {"sahtikraken", "sahti_65"},
                {"sahtikrakenmjolmir", "sahti_65"},
                {"sahtirustkingtreasure", "sahti_62"},
                {"spiderqueen", "sen_1"},
                {"tyrant", "faen_25"},
                {"tyrantbeavers", "faen_25"},
                {"tyrantchampy", "faen_25"},
                {"tyrantchompy", "faen_25"},
                {"tyrantchumpy", "faen_25"},
                {"upripreboss", "uprising_13"},
                {"yogger", "sen_27"},
            };

        internal static void LogListInfoFromSeed(string seed, string shop, string node)
        {
            List<string> itemList = GetItemsFromSeed(_seed: seed, _shop: shop, _node: node, _madness: 1, _corruptorCount: 0);
            LogDebug(seed);
            LogDebug(string.Join(", ", itemList));

        }

        private static System.Random random = new();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        internal static string DoubleCaravanEpics(List<(string, string)> listOfPairs, int nSeeds)
        {
            // int nSeeds = 10_000_000;
            StringBuilder foundItems = new();
            for (int i = 0; i < nSeeds; i++)
            {
                string randomSeed = RandomString(8).ToUpper();
                if (i % 100_000 == 0) { LogDebug($"On Seed {i}: {randomSeed}"); }

                List<string> itemList = GetItemsFromSeed(_seed: randomSeed, _shop: "caravanshop", _node: "sen_44", _madness: 1, _corruptorCount: 0);


                if (ItemListContains(itemList, "darkhood") && ItemListContains(itemList, "assassintools") && (ItemListContains(itemList, "longbow") || ItemListContains(itemList, "twinblades")) && ItemListContains(itemList, "brassamulet"))
                {
                    LogDebug($"FOUND THE PERFECT DARKHOOD/ASSASSIN TOOL SEED + {randomSeed}");
                }

                foreach ((string, string) pair in listOfPairs)
                {
                    if ((itemList.Contains(pair.Item1) || itemList.Contains(pair.Item1 + "rare")) && (itemList.Contains(pair.Item2) || itemList.Contains(pair.Item2 + "rare")))
                    {
                        foundItems.Append($"{randomSeed} - {pair.Item1} and {pair.Item2} \n");
                        LogDebug($"{randomSeed} - {pair.Item1} and {pair.Item2}");
                    }
                }


            }
            return foundItems.ToString();

        }


        public static bool ItemListContains(List<string> itemList, string item)
        {
            return itemList.Contains(item) || itemList.Contains(item + "rare");
        }

        internal static bool CheckSingleSeed(string seed, Dictionary<string, string> itemsAndShops, int madness = 1, int corruptorCount = 0)
        {
            // LogDebug("CheckSingleSeed");

            // string shop = "sahtikrakenmjolmir";   
            // string lootLocation = "sahti_65";
            // LogDebug("CheckSingleSeed - Pre getItems");

            foreach (KeyValuePair<string, string> itemShopPair in itemsAndShops)
            {
                string item = itemShopPair.Key;
                item = char.IsDigit(item[item.Length - 1]) ? item.Remove(item.Length - 1) : item;
                string shop = itemShopPair.Value;

                if (!CheckSingleShop(item, shop, seed, madness, corruptorCount))
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool CheckSingleShop(string item, string shop, string seed, int madness, int corruptorCount, string node = null)
        {
            string lootLocation = allLootLocations.ContainsKey(shop) ? allLootLocations[shop] : node;
            if (lootLocation == null) { LogDebug($"improper LootLocation - {shop}"); return false; }
            if (!NodeExists(node, seed))
            {
                return false;
            }
            List<string> itemList = GetItemsFromSeed(_seed: seed, _shop: shop, _node: lootLocation, _madness: madness, _corruptorCount: corruptorCount);
            // if(itemList.Contains(""))            
            return itemList.Contains(item);
        }

        internal static bool CheckSingleNodeAndShop(string item, string shop, string node, string seed, int madness = 1, int corruptorCount = 0, string eventId = null)
        {
            if (eventId == null)
            {
                if (!NodeExists(node, seed)) { return false; }
            }
            else if (!EventExists(seed, node, eventId))
            {
                return false;
            }
            string lootLocation = node;
            if (lootLocation == "") { LogDebug($"improper LootLocation - {shop}"); return false; }
            List<string> itemList = GetItemsFromSeed(_seed: seed, _shop: shop, _node: lootLocation, _madness: madness, _corruptorCount: corruptorCount);
            // if(itemList.Contains(""))            
            return itemList.Contains(item);
        }

        internal static List<string> CheckSeeds(Dictionary<string, string> itemsAndShops, int nSeeds, int madness = 1, int corruptorCount = 0)
        {
            LogDebug("CheckSeeds - Begin");
            // string correctSeed = "";
            List<string> foundSeeds = [];
            for (int i = 0; i < nSeeds; i++)
            {

                string randomSeed = RandomString(8).ToUpper();
                if (i % 50_000 == 0) { LogDebug($"On Seed {i}: {randomSeed}"); }
                if (CheckSingleSeed(randomSeed, itemsAndShops, madness: madness, corruptorCount: corruptorCount))
                {
                    foundSeeds.Append(randomSeed);
                    List<string> items = itemsAndShops.Keys.ToList();
                    string foundStuff = string.Join(", ", items);
                    LogInfo($"Found Seed with {foundStuff}: {randomSeed}");
                    // correctSeed = randomSeed;
                    // break;
                }
            }
            return foundSeeds;
        }

        internal static void LogSeedsNodeSpecified(List<(string, string, string)> thingsToSearch, int nSeeds = 20_000, int madness = 1, int corruptorCount = 0)
        {
            List<string> foundSeeds = [];

            for (int i = 0; i < nSeeds; i++)
            {
                if (i % 10000 == 0) { LogDebug($"On Seed {i}"); }
                string seed = RandomString(8).ToUpper();
                // item, loot/shop, node
                bool flag = true;
                foreach ((string, string, string) thing in thingsToSearch)
                {
                    string item = thing.Item1;
                    string shop = thing.Item2;
                    string node = thing.Item3;
                    string hasEvent = node == "sen_29" ? "e_sen29_a" : null;
                    if (!CheckSingleNodeAndShop(item, shop, node, seed, madness, corruptorCount, eventId: hasEvent))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    foundSeeds.Add(seed);
                if (foundSeeds.Count() >= 10)
                {
                    break;
                }
            }
            LogDebug("Found Seeds with appropriate items: " + string.Join(", ", foundSeeds));
            return;
        }



        internal static void FindSeedWithItems()
        {

            List<string> itemsToFind = [];

            int nSeeds = 5000;

            for (int i = 0; i < nSeeds; i++)
            {
                string randomSeed = RandomString(8).ToUpper();
                List<string> itemsFound = [];
                foreach (string lootDrop in allItemDropLocations.Keys)
                {
                    string lootLocation = allItemDropLocations[lootDrop];
                    List<string> itemList = GetItemsFromSeed(_seed: randomSeed, _shop: lootDrop, _node: lootLocation, _madness: 1, _corruptorCount: 0);

                }

            }
        }

        public static void LogAllItems()
        {
            LogInfo("LogAllItems - Start");
            LogCaravanItems();
            LogGuaranteedDropItems();
            LogBossDropItems();
            LogPetItems();
            LogMythicItems();

        }

        internal static void LogMythicItems()
        {

            int nSeeds = 2000;
            HandleMythics(MythicLocations, nSeeds, rareOnly: false);
            LogInfo("Completed Mythic Items");
        }

        internal static void HandleMythics(Dictionary<string, string> lootNodeMap, int nSeeds = 300, bool rareOnly = false)
        {
            Dictionary<string, List<string>> outputItemMap = [];

            foreach (string location in lootNodeMap.Keys)
            {
                LogDebug($"Searching Location {location}");
                for (int i = 0; i < nSeeds; i++)
                {
                    string randomSeed = RandomString(7).ToUpper();
                    string node = lootNodeMap[location];
                    List<string> itemList = GetItemsFromSeed(_seed: randomSeed, _shop: location, _node: node, _madness: 1, _corruptorCount: 0);
                    // LogDebug($"Shop - {shop} itemList Items - {string.Join(", ",itemList)}");
                    UpdateItemDictForMythics(ref outputItemMap, itemList, randomSeed, location, count: 10);
                }
            }

            LogDebug($"Shop Items - {DictToString(outputItemMap)}");
        }

        internal static bool EventExists(string seed, string nodeId, string eventId, bool assumePlayerHasReqs = false)
        {
            // LogDebug("CheckIfNodeHasEvent");
            NodeData nodeData = Globals.Instance?.GetNodeData(nodeId);
            if (nodeData == null)
            {
                LogDebug("Null nodedata/Globals");
                return false;
            }
            UnityEngine.Random.InitState((nodeData.NodeId + seed + "AssignSingleGameNode").GetDeterministicHashCode());
            if (UnityEngine.Random.Range(0, 100) >= nodeData.ExistsPercent)
            {
                return false;
            }
            else
            {
                bool flag1 = true;
                bool flag2 = true;
                if (nodeData.NodeEvent != null && nodeData.NodeEvent.Length != 0 && nodeData.NodeCombat != null && nodeData.NodeCombat.Length != 0)
                {
                    if (UnityEngine.Random.Range(0, 100) < nodeData.CombatPercent)
                        flag1 = false;
                    else
                        flag2 = false;
                }
                if (flag1 && nodeData.NodeEvent != null && nodeData.NodeEvent.Length != 0)
                {
                    string str = "";
                    Dictionary<string, int> source = new Dictionary<string, int>();
                    for (int index = 0; index < nodeData.NodeEvent.Length; ++index)
                    {
                        if ((UnityEngine.Object)nodeData.NodeEvent[index] != (UnityEngine.Object)null)
                        {
                            bool flag3 = true;
                            // if ((UnityEngine.Object)nodeData.NodeEvent[index].Requirement != (UnityEngine.Object)null && !this.PlayerHasRequirement(nodeData.NodeEvent[index].Requirement))
                            if ((UnityEngine.Object)nodeData.NodeEvent[index].Requirement != (UnityEngine.Object)null && assumePlayerHasReqs)
                                flag3 = false;
                            // if ((UnityEngine.Object)nodeData.NodeEvent[index].RequiredClass != (UnityEngine.Object)null && !this.PlayerHasRequirementClass(nodeData.NodeEvent[index].RequiredClass.Id))
                            if ((UnityEngine.Object)nodeData.NodeEvent[index].RequiredClass != (UnityEngine.Object)null && assumePlayerHasReqs)
                                flag3 = false;
                            if (flag3)
                            {
                                int num = 10000;
                                if (index < nodeData.NodeEventPriority.Length)
                                    num = nodeData.NodeEventPriority[index];
                                source.Add(nodeData.NodeEvent[index].EventId, num);
                            }
                        }
                    }
                    if (source.Count == 0)
                        return false;
                    Dictionary<string, int> dictionary1 = source.OrderBy<KeyValuePair<string, int>, int>((Func<KeyValuePair<string, int>, int>)(x => x.Value)).ToDictionary<KeyValuePair<string, int>, string, int>((Func<KeyValuePair<string, int>, string>)(x => x.Key), (Func<KeyValuePair<string, int>, int>)(x => x.Value));
                    int num1 = 1;
                    int num2 = dictionary1.ElementAt<KeyValuePair<string, int>>(0).Value;
                    while (num1 < dictionary1.Count && dictionary1.ElementAt<KeyValuePair<string, int>>(num1).Value == num2)
                        ++num1;
                    if (num1 == 1)
                    {
                        str = dictionary1.ElementAt<KeyValuePair<string, int>>(0).Key;
                    }
                    else
                    {
                        if (nodeData.NodeEventPercent != null && nodeData.NodeEvent.Length == nodeData.NodeEventPercent.Length)
                        {
                            Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
                            int index1 = 0;
                            for (int index2 = 0; index2 < num1; ++index2)
                            {
                                int index3 = 0;
                                while (index2 < nodeData.NodeEvent.Length)
                                {
                                    if (nodeData.NodeEvent[index3].EventId == dictionary1.ElementAt<KeyValuePair<string, int>>(index1).Key)
                                    {
                                        dictionary2.Add(nodeData.NodeEvent[index3].EventId, nodeData.NodeEventPercent[index3]);
                                        ++index1;
                                        break;
                                    }
                                    ++index3;
                                }
                            }
                            int num3 = UnityEngine.Random.Range(0, 100);
                            int num4 = 0;
                            foreach (KeyValuePair<string, int> keyValuePair in dictionary2)
                            {
                                num4 += keyValuePair.Value;
                                if (num3 < num4)
                                {
                                    str = keyValuePair.Key;
                                    break;
                                }
                            }
                        }
                        if (str == "")
                        {
                            int index = UnityEngine.Random.Range(0, num1);
                            str = dictionary1.ElementAt<KeyValuePair<string, int>>(index).Key;
                        }
                    }
                    // if (this.gameNodeAssigned.ContainsKey(nodeData.NodeId))
                    //     this.gameNodeAssigned[nodeData.NodeId] = "event:" + str;
                    // else
                    //     this.gameNodeAssigned.Add(nodeData.NodeId, "event:" + str);
                    // LogDebug($"comparing {str} to {eventId}" );
                    if (str == eventId) { return true; }
                }
                else if (flag2 && nodeData.NodeCombat != null && nodeData.NodeCombat.Length != 0)
                {
                    string str = "";
                    // if (GameManager.Instance.IsWeeklyChallenge() && (nodeData.NodeId == "of1_10" || nodeData.NodeId == "of2_10"))
                    // {
                    //     ChallengeData weeklyData = Globals.Instance.GetWeeklyData(this.weekly);
                    //     if ((UnityEngine.Object)weeklyData != (UnityEngine.Object)null && (UnityEngine.Object)weeklyData.BossCombat != (UnityEngine.Object)null)
                    //         str = weeklyData.BossCombat.CombatId;
                    // }
                    if (str == "")
                    {
                        int index = 0;
                        if (nodeData.NodeId == "of1_10" || nodeData.NodeId == "of2_10")
                        {
                            UnityEngine.Random.State state = UnityEngine.Random.state;
                            UnityEngine.Random.InitState((nodeData.NodeId + seed + "finalBoss").GetDeterministicHashCode());
                            index = UnityEngine.Random.Range(0, nodeData.NodeCombat.Length);
                            UnityEngine.Random.state = state;
                        }
                        str = nodeData.NodeCombat[index].CombatId;
                        // LogDebug($"comparing {str} to {eventId}" );
                        if (str == eventId) { return true; }
                    }
                    //     if (this.gameNodeAssigned.ContainsKey(nodeData.NodeId))
                    //         this.gameNodeAssigned[nodeData.NodeId] = "combat:" + str;
                    //     else
                    //         this.gameNodeAssigned.Add(nodeData.NodeId, "combat:" + str);
                    // }
                    // else if (this.gameNodeAssigned.ContainsKey(nodeData.NodeId))
                    //     this.gameNodeAssigned[nodeData.NodeId] = "";
                    // else
                    //     this.gameNodeAssigned.Add(nodeData.NodeId, "");
                }
            }

            return false;
        }


        internal static bool EventExists(string seed, NodeData nodeData, string eventId)
        {
            string nodeId = nodeData.NodeId;
            return EventExists(seed, nodeId, eventId);
        }


        internal static void FindAndLogSeedsWithEvent(List<string> eventsToFind, int nSeeds = 20_000)
        {
            List<string> foundSeeds = [];

            for (int i = 0; i < nSeeds; i++)
            {
                if (i % 10000 == 0) { LogDebug($"On Seed {i}"); }
                string seed = RandomString(8).ToUpper();
                // item, loot/shop, node
                bool flag = true;
                foreach (string eventId in eventsToFind)
                {
                    string node = ConvertEventToNode(eventId);
                    if (!EventExists(seed, node, eventId))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    foundSeeds.Add(seed);
                if (foundSeeds.Count() >= 10)
                {
                    break;
                }
            }
            LogDebug("Found Seeds with events: " + string.Join(", ", foundSeeds));
            return;

        }

        internal static bool NodeExists(string nodeId, string seed, string hasEvent = null)
        {
            if (Globals.Instance == null)
            {
                return false;
            }
            NodeData node = Globals.Instance.GetNodeData(nodeId);
            if (node == null)
            {
                LogDebug($"null node: {nodeId} in seed {seed}");
                return false;
            }


            UnityEngine.Random.InitState((nodeId + seed + "AssignSingleGameNode").GetDeterministicHashCode());
            if (UnityEngine.Random.Range(0, 100) >= node.ExistsPercent)
            {
                return false;
            }

            if (hasEvent != null && AtOManager.Instance != null)
            {
                Dictionary<string, string> gameNodeAssigned = Traverse.Create(AtOManager.Instance).Field("gameNodeAssigned").GetValue<Dictionary<string, string>>();
                if (gameNodeAssigned == null) { return false; }
                if (gameNodeAssigned.ContainsKey(nodeId))
                {
                    string action = gameNodeAssigned[nodeId];
                    LogDebug(action);
                    return action.Split(":")[1] == hasEvent;


                }
                return false;
            }

            return true;
        }



        internal static void LogPetItems()
        {
            int nSeeds = 500;
            HandleDropItems(petDropNodeMap, nSeeds, rareOnly: true);
            LogInfo("Completed Pet Items");
        }


        internal static void LogBossDropItems()
        {

            int nSeeds = 500;
            HandleDropItems(dropNodeMap, nSeeds, rareOnly: true);
            LogInfo("Completed Boss Items");
        }

        internal static void LogGuaranteedDropItems()
        {

            int nSeeds = 500;
            HandleDropItems(guaranteedDropNodeMap, nSeeds, rareOnly: true);
            LogInfo("Completed Guaranteed Items");

        }

        internal static void LogSingleShop(string shop, string seed, string node = null, int madness = 1, int corruptorCount = 0)
        {
            if (node == null)
            {

            }
            List<string> itemList = GetItemsFromSeed(_seed: seed, _shop: shop, _node: node, _madness: 1, _corruptorCount: 0);
        }

        internal static void LogShopItems()
        {
            Dictionary<string, string> shopNodeMap = new()
            {
                {"aquarvendor","aqua_26"},
                {"elvenemporium","faen_23"},
                {"felineshop","ulmin_29"},
                {"firebazaar","velka_20"},
                // {"heronshop","ulmin_27"},
                // {"Merchantwares","velka_7"},
                // {"voidshop","voidlow_8"},
                // {"voidtsnemo","voidlow_24"},
                // {"voodooshop", "aqua_23"},
                // {"Jeweler","sen_35"},
                // {"gobletsmerchant","sewers_4"},
                // {"werewolfstall_a","sen_30"},
                // {"werewolfstall_b","sen_30"}
            };

            int nSeeds = 300;
            // LootItem item = lootData.Loot    ItemTable[0];
            HandleDropItems(shopNodeMap, nSeeds);


        }

        internal static void HandleDropItems(Dictionary<string, string> lootNodeMap, int nSeeds = 300, bool rareOnly = false)
        {
            Dictionary<string, List<string>> outputItemMap = [];

            foreach (string shop in lootNodeMap.Keys)
            {
                LootData lootData = Globals.Instance.GetLootData(shop);
                List<string> validItems = [];
                foreach (LootItem item in lootData.LootItemTable)
                {
                    if (item.LootCard == null)
                    {
                        continue;
                    }
                    if (item.LootCard.CardName != "")
                    {
                        if (rareOnly)
                        {
                            validItems.Add(item.LootCard.Id + "rare");
                        }
                        else
                        {
                            validItems.Add(item.LootCard.Id);
                        }

                    }
                }
                LogDebug($"Shop - {shop} Valid Items - {string.Join(", ", validItems)}");

                for (int i = 0; i < nSeeds; i++)
                {
                    string randomSeed = RandomString(7).ToUpper();
                    string node = lootNodeMap[shop];
                    List<string> itemList = GetItemsFromSeed(_seed: randomSeed, _shop: shop, _node: node, _madness: 1, _corruptorCount: 0);
                    // LogDebug($"Shop - {shop} itemList Items - {string.Join(", ",itemList)}");
                    UpdateItemDictForShops(ref outputItemMap, itemList, validItems, randomSeed, shop, count: 10);
                }
            }

            LogDebug($"Shop Items - {DictToString(outputItemMap)}");
        }

        internal static void UpdateItemDictForMythics(ref Dictionary<string, List<string>> itemDict, List<string> itemList, string seed, string shopName, int count = 5)
        {
            foreach (string item in itemList)
            {

                CardData cardData = Globals.Instance.GetCardData(item, false);
                // if(cardData==null){continue;}
                if (cardData == null || cardData.CardRarity != Enums.CardRarity.Mythic) { continue; }
                string key = $"{shopName} - {cardData.CardName}";
                if (!itemDict.ContainsKey(key))
                {
                    itemDict.Add(key, []);
                }
                if (itemDict[key].Count() < count)
                {
                    itemDict[key].Add(seed);
                }
            }
        }

        internal static void LogCaravanItems()
        {
            Dictionary<string, List<string>> itemDict = [];
            // Dictionary<string,string> itemDict = [];
            // LogCaravanItems()
            int nSeeds = 1000;
            for (int i = 0; i < nSeeds; i++)
            {
                // string testSeed = testSeeds[i];
                string randomSeed = RandomString(7).ToUpper();

                string shop = "caravanshop";
                string node = "sen_44";
                List<string> itemList = GetItemsFromSeed(_seed: randomSeed, _shop: shop, _node: node, _madness: 1);
                UpdateItemDict(ref itemDict, itemList, randomSeed);

            }
            LogDebug($"Dictionary - {DictToString(itemDict)}");
        }


        internal static void UpdateItemDictForShops(ref Dictionary<string, List<string>> itemDict, List<string> itemList, List<string> validItems, string seed, string shopName, int count = 5)
        {
            foreach (string item in itemList)
            {
                if (!validItems.Contains(item))
                {
                    continue;
                }

                CardData cardData = Globals.Instance.GetCardData(item, false);
                // if(cardData==null){continue;}
                if (cardData == null || cardData.CardUpgraded != Enums.CardUpgraded.Rare) { continue; }
                string key = $"{shopName} - {cardData.CardName}";
                if (!itemDict.ContainsKey(key))
                {
                    itemDict.Add(key, []);
                }
                if (itemDict[key].Count() < count)
                {
                    itemDict[key].Add(seed);
                }
            }
        }

        internal static void UpdateItemDict(ref Dictionary<string, List<string>> itemDict, List<string> itemList, string seed, int count = 5)
        {
            foreach (string item in itemList)
            {
                CardData cardData = Globals.Instance.GetCardData(item, false);
                string key = cardData.CardUpgraded == Enums.CardUpgraded.Rare ? $"{cardData.CardName} (Corrupted)" : cardData.CardName;
                if (!itemDict.ContainsKey(key))
                {
                    itemDict.Add(key, []);
                }
                if (itemDict[key].Count() < count)
                {
                    itemDict[key].Add(seed);
                }
            }
        }
        internal static List<string> GetItemsFromSeed(string _seed = "", string _shop = "caravanshop", string _node = "", int _townReroll = 0, bool _obeliskChallenge = false, int _madness = 0, int _corruptorCount = 0, bool _poverty = false)
        {
            // LogInfo("GetItemsFromSeed - Start");

            string reroll = "";
            string node = _node; // have to do this because can't use ref keyword w/ default values
            if (_shop == "caravanshop" && node == "")
                node = "sen_44";
            // #TODO: other shops/nodes
            if (_townReroll > 0)
            {
                // #TODO: I have not looked into how the reroll string is generated
                // reroll = ??
            }

            // AtOManager.Instance.Gettown
            // LogInfo("GetItemsFromSeed - Start 1");

            // if (AtOManager.Instance != null) // a game is in progress
            // {
            //     // replace with current game values if defaults are used
            //     if (_seed == "")
            //     {
            //         string s = AtOManager.Instance.GetGameId();
            //         _seed = s;
            //     }
            //     LogInfo("GetItemsFromSeed - Start 2");

            //     if (node == "")
            //         node = AtOManager.Instance.currentMapNode;
            //     if (_townReroll == 0)
            //         reroll = AtOManager.Instance.shopItemReroll;
            //     if (_obeliskChallenge == false && GameManager.Instance != null && GameManager.Instance.IsObeliskChallenge())
            //         _obeliskChallenge = true;
            //     _madness = _obeliskChallenge ? AtOManager.Instance.GetObeliskMadness() : AtOManager.Instance.GetNgPlus();
            //     _corruptorCount = AtOManager.Instance.GetMadnessDifficulty() - _madness;
            //     _poverty = AtOManager.Instance.IsChallengeTraitActive("poverty") || MadnessManager.Instance != null && MadnessManager.Instance.IsMadnessTraitActive("poverty");
            // }
#pragma warning restore Harmony003 // Harmony non-ref patch parameters modified

            LootData lootData = Globals.Instance.GetLootData(_shop);
            if (lootData == null)
            {
                LogError("Unable to get shop items for " + _shop + " (shop does not exist!)");
                return null;
            }

            // LogInfo("GetItemsFromSeed - Mid 1");

            List<string> ts1 = new List<string>();
            List<string> ts2 = new List<string>();
            for (int index = 0; index < Globals.Instance.CardListByClass[Enums.CardClass.Item].Count; ++index)
                ts2.Add(Globals.Instance.CardListByClass[Enums.CardClass.Item][index]);
            int deterministicHashCode = (node + _seed + reroll).GetDeterministicHashCode();
            UnityEngine.Random.InitState(deterministicHashCode);
            ts2.Shuffle<string>(deterministicHashCode);
            int index1 = 0;
            int num1 = !_obeliskChallenge ? lootData.NumItems : lootData.LootItemTable.Length;
            for (int index2 = 0; index2 < num1 && ts1.Count < lootData.NumItems; ++index2)
            {
                if (index2 < lootData.LootItemTable.Length)
                {
                    LootItem lootItem = lootData.LootItemTable[index2];
                    if ((double)UnityEngine.Random.Range(0, 100) < (double)lootItem.LootPercent)
                    {
                        if (lootItem.LootCard != null)
                        {
                            ts1.Add(lootItem.LootCard.Id);
                        }
                        else
                        {
                            bool flag = false;
                            int num2 = 0;
                            CardData cardData = (CardData)null;
                            for (; !flag && num2 < 10000; ++num2)
                            {
                                if (index1 >= ts2.Count)
                                    index1 = 0;
                                string str = ts2[index1];
                                // if (!ts1.Contains(str) && (!AtOManager.Instance.ItemBoughtOnThisShop(_itemListId, str) && AtOManager.Instance.HowManyTownRerolls() > 0 || AtOManager.Instance.HowManyTownRerolls() == 0))
                                // replaced with a line that DOESN'T check if item has already been bought
                                // usually, if an item has been bought it will not show up in town again
                                if (!ts1.Contains(str))
                                {
                                    cardData = Globals.Instance.GetCardData(str, false);
                                    if (cardData.Item != null && !cardData.Item.DropOnly)
                                    {
                                        if (cardData.CardUpgraded == Enums.CardUpgraded.Rare)
                                            flag = false;
                                        else if (cardData.Item.PercentRetentionEndGame > 0 && (_madness > 2 || _obeliskChallenge))
                                            flag = false;
                                        else if (cardData.Item.PercentDiscountShop > 0 && _poverty)
                                            flag = false;
                                        else if (lootItem.LootType == Enums.CardType.None && cardData.CardRarity == lootItem.LootRarity)
                                            flag = true;
                                        else if (cardData.CardType == lootItem.LootType && cardData.CardRarity == lootItem.LootRarity)
                                            flag = true;
                                    }
                                }
                                ++index1;
                            }
                            if (flag && cardData != null)
                                ts1.Add(cardData.Id);
                        }
                    }
                }
            }

            // LogInfo("GetItemsFromSeed - Mid 2");
            for (int count = ts1.Count; count < lootData.NumItems; ++count)
            {
                bool flag = false;
                int num3 = 0;
                CardData cardData = (CardData)null;
                int num4 = UnityEngine.Random.Range(0, 100);
                while (!flag && num3 < 10000)
                {
                    if (index1 >= ts2.Count)
                        index1 = 0;
                    string str = ts2[index1];
                    // if (!ts1.Contains(str) && (!AtOManager.Instance.ItemBoughtOnThisShop(_itemListId, str) && AtOManager.Instance.HowManyTownRerolls() > 0 || AtOManager.Instance.HowManyTownRerolls() == 0))
                    // replaced with a line that DOESN'T check if item has already been bought
                    // usually, if an item has been bought it will not show up in town again
                    if (!ts1.Contains(str))
                    {
                        cardData = Globals.Instance.GetCardData(str, false);
                        if (cardData.Item != null && !cardData.Item.DropOnly)
                        {
                            if (cardData.CardUpgraded == Enums.CardUpgraded.Rare)
                                flag = false;
                            else if (cardData.Item.PercentRetentionEndGame > 0 && (_madness > 2 || _obeliskChallenge))
                                flag = false;
                            else if (cardData.Item.PercentDiscountShop > 0 && _poverty)
                                flag = false;
                            else if ((double)num4 < (double)lootData.DefaultPercentMythic)
                            {
                                if (cardData.CardRarity == Enums.CardRarity.Mythic)
                                    flag = true;
                            }
                            else if ((double)num4 < (double)lootData.DefaultPercentEpic + (double)lootData.DefaultPercentMythic)
                            {
                                if (cardData.CardRarity == Enums.CardRarity.Epic)
                                    flag = true;
                            }
                            else if ((double)num4 < (double)lootData.DefaultPercentRare + (double)lootData.DefaultPercentEpic + (double)lootData.DefaultPercentMythic)
                            {
                                if (cardData.CardRarity == Enums.CardRarity.Rare)
                                    flag = true;
                            }
                            else if ((double)num4 < (double)lootData.DefaultPercentUncommon + (double)lootData.DefaultPercentRare + (double)lootData.DefaultPercentEpic + (double)lootData.DefaultPercentMythic)
                            {
                                if (cardData.CardRarity == Enums.CardRarity.Uncommon)
                                    flag = true;
                            }
                            else if (cardData.CardRarity == Enums.CardRarity.Common)
                                flag = true;
                        }
                    }
                    ++index1;
                    ++num3;
                    if (!flag && num3 % 100 == 0)
                        num4 += 10;
                }
                if (flag && cardData != null)
                    ts1.Add(cardData.Id);
                else
                    break;
            }
            // LogInfo("GetItemsFromSeed - Mid 3");
            ts1.Shuffle<string>(deterministicHashCode);
            if (!_shop.Contains("towntier") && (!_obeliskChallenge && _madness > 0 || _obeliskChallenge))
            {
                int num5 = 0;
                if (_shop.Contains("exoticshop"))
                    num5 += 8;
                else if (_shop.Contains("rareshop"))
                    num5 += 4;
                if (_obeliskChallenge)
                {
                    if (_madness > 8)
                        num5 += 4;
                    else if (_madness > 4)
                        num5 += 2;
                }
                else
                    num5 += Functions.FuncRoundToInt(0.2f * (float)(_madness + _corruptorCount));
                for (int index3 = 0; index3 < ts1.Count; ++index3)
                {
                    int num6 = UnityEngine.Random.Range(0, 100);
                    CardData cardData = Globals.Instance.GetCardData(ts1[index3], false);
                    if (!(cardData == null))
                    {
                        bool flag = false;
                        if ((cardData.CardRarity == Enums.CardRarity.Mythic || cardData.CardRarity == Enums.CardRarity.Epic) && num6 < 3 + num5)
                            flag = true;
                        else if (cardData.CardRarity == Enums.CardRarity.Rare && num6 < 5 + num5)
                            flag = true;
                        else if (cardData.CardRarity == Enums.CardRarity.Uncommon && num6 < 7 + num5)
                            flag = true;
                        else if (cardData.CardRarity == Enums.CardRarity.Common && num6 < 9 + num5)
                            flag = true;
                        if (flag && cardData.UpgradesToRare != null)
                            ts1[index3] = cardData.UpgradesToRare.Id;
                    }
                }
            }
            // LogInfo("GetItemsFromSeed - End 1");
#pragma warning disable Harmony003 // Harmony non-ref patch parameters modified
            // LogInfo($"SHOP CONTENTS for {_shop} at node {node} in seed {_seed} (reroll: {(reroll == "" ? _townReroll.ToString() : reroll)}, {(_obeliskChallenge ? "OC " : "")}madness {_madness.ToString()}|{_corruptorCount.ToString() + (_poverty ? " with poverty" : "")})");

            return ts1;
            // foreach (string cardID in ts1)
            // {
            //     CardData card = Globals.Instance.GetCardData(cardID);
            //     if (card != null)
            //     {
            //         LogInfo(card.CardName + (card.CardUpgraded == Enums.CardUpgraded.Rare ? " (Corrupted)" : (card.CardUpgraded == Enums.CardUpgraded.A ? " (Blue)" : (card.CardUpgraded == Enums.CardUpgraded.B ? " (Yellow)" : ""))) + " [" + card.Id + "]");

            //     }
            // }
        }

        public static string ConvertEventToNode(string eventId)
        {
            var pattern = @"[^_]*_([^_\d]*)(\d+).*";

            var match = Regex.Match(eventId, pattern);
            if (match.Success)
            {
                string zone = match.Groups[1].Value;
                string nodeNumber = match.Groups[2].Value;
                return $"{zone}_{nodeNumber}";
            }
            return eventId;

        }
    }
}