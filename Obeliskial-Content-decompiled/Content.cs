using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static Obeliskial_Content.DataTextConvert;
using System.IO;
using Unity.Collections;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.U2D;
using Obeliskial_Essentials;
using static Obeliskial_Essentials.Essentials;
using static Obeliskial_Essentials.DataTextConvert;


/*
AtOManager
    AddPlayerRequirement
    GlobalAuraCurseModificationByTraitsAndItems
    RemovePlayerRequirement
CardCraftManager
    CheckForCorruptableCards
    ShowListCardsForCraft
Character
    GetAuraCurseQuantityModification
    GetTraitAuraCurseModifiers
CreateGameContent
    Start
HeroSelection
    SetSprite
    SetSpriteSilueta
IntroNewGameManager
    Start
Item
    DoItemData
MainMenuManager
    Start
MapManager
    Awake
    IncludeMapPrefab
MatchManager
    CreateNPC
    FinishCombat
    KillNPC
    ResignCombatActionExecute
PlayerManager
    IsCardUnlocked
RewardsManager
    Start
Trait
    DoTrait
*/
namespace Obeliskial_Content
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.stiffmeds.obeliskialessentials")]
    [BepInProcess("AcrossTheObelisk.exe")]
    public class Content : BaseUnityPlugin
    {
        internal const int ModDate = 20240311;
        private readonly Harmony harmony = new(PluginInfo.PLUGIN_GUID);
        internal static ManualLogSource Log;
        public static Dictionary<string, CardData> medsCardsSource = new();
        public static Dictionary<string, SubClassData> medsSubClassesSource = new();
        public static Dictionary<string, CardbackData> medsCardbacksSource = new();
        public static Dictionary<string, SkinData> medsSkinsSource = new();
        public static Dictionary<string, TraitData> medsTraitsSource = new();
        public static List<string> medsCustomTraitsSource = new();
        public static Dictionary<string, NPCData> medsNPCsSource = new();
        public static Dictionary<string, AuraCurseData> medsAurasCursesSource = new();
        public static Dictionary<string, NodeData> medsNodeDataSource = new();
        public static Dictionary<string, LootData> medsLootDataSource = new();
        public static Dictionary<string, PerkData> medsPerksSource = new();
        public static Dictionary<string, PerkNodeData> medsPerksNodesSource = new();
        public static Dictionary<string, ChallengeTrait> medsChallengeTraitsSource = new();
        public static Dictionary<string, CombatData> medsCombatDataSource = new();
        public static Dictionary<string, EventData> medsEventDataSource = new();
        public static Dictionary<string, EventRequirementData> medsEventRequirementDataSource = new();
        public static Dictionary<string, ZoneData> medsZoneDataSource = new();
        public static SortedDictionary<string, KeyNotesData> medsKeyNotesDataSource = new();
        public static Dictionary<string, ChallengeData> medsChallengeDataSource = new();
        public static Dictionary<string, PackData> medsPackDataSource = new();
        public static Dictionary<string, ItemData> medsItemDataSource = new();
        public static Dictionary<string, CardPlayerPackData> medsCardPlayerPackDataSource = new();
        public static Dictionary<string, CorruptionPackData> medsCorruptionPackDataSource = new();
        public static Dictionary<string, CinematicData> medsCinematicDataSource = new();
        public static Dictionary<string, Sprite> medsSprites = new();
        public static Dictionary<int, TierRewardData> medsTierRewardDataSource = new();
        public static Dictionary<string, Node> medsNodeSource = new();
        public static Dictionary<string, string[]> medsSecondRunImport = new();
        public static Dictionary<string, string> medsSecondRunImport2 = new();
        public static Dictionary<string[], string> medsSecondRunImport3 = new();
        public static Dictionary<string, string> medsCardsNeedingItems = new();
        public static Dictionary<string, string> medsCardsNeedingItemEnchants = new();
        public static List<string> medsCustomUnlocks = new();
        public static TMP_SpriteAsset medsFallbackSpriteAsset = ScriptableObject.CreateInstance<TMP_SpriteAsset>();
        public static Dictionary<string, AudioClip> medsAudioClips = new();
        public static Dictionary<string, Sprite> medsVanillaSprites = new();
        public static Dictionary<string, GameObject> medsGOs = new();
        public static Dictionary<string, string> medsNodeCombatEventRelation = new();
        public static Dictionary<string, CardPlayerPairsPackData> medsCardPlayerPairsPackDataSource = new();
        public static Dictionary<string, string> medsNodeEvent = new();
        public static Dictionary<string, int> medsNodeEventPercent = new();
        public static Dictionary<string, int> medsNodeEventPriority = new();
        public static Dictionary<string, ThermometerTierData> medsThermometerTierData = new();
        public static Dictionary<string, string> medsSecondRunCombatEvent = new();
        public static Dictionary<string, string> medsSecondRunCinematicCombat = new();
        public static Dictionary<string, string> medsSecondRunCinematicEvent = new();
        public static Dictionary<string, string[]> medsSecondRunNodesConnected = new();
        public static Dictionary<string, EventReplyDataText> medsEventReplyDataText = new();
        public static Dictionary<string, string> medsCardsNeedingSummonUnits = new();
        public static int medsMaxHeroesInClass = 10;
        public static Dictionary<string, ZoneDataText> medsCustomZones = new();
        public static Dictionary<string, GameObject> medsCustomZoneGOs = new();
        public static Dictionary<string, List<NodeDataText>> medsNodesByZone = new();
        public static bool medsLoadedCustomNodes = false;
        public static Dictionary<string, Vector3> medsNodePositions = new();
        public static GameObject medsBaseRoadGO = (GameObject)null;
        public static Dictionary<string, List<Vector3>> medsCustomRoads = new();
        public static GameObject medsInvisibleGOHolder = new();
        public static List<string> medsVanillaIntroNodes = new List<string>() { "sen_0", "tutorial_0", "secta_0", "spider_0", "forge_0", "sewers_0", "sewers_1", "wolf_0", "pyr_0", "velka_0", "aqua_0", "voidlow_0", "faen_0", "ulmin_0", "voidhigh_0" };
        public static GameObject medsZoneTransitionGO = (GameObject)null;
        public static Dictionary<string, PrestigeDeck> medsPrestigeDecks = new();
        public static bool medsDoNotLetCombatFinish = false;
        public static Dictionary<string, CardData> medsExtendedEnchantments = new();
        public static string medsAwaitingKill = "";
        public static List<string> medsAutoUnlockHeroes = new();
        internal static List<string> medsACIndex = new();
        internal static Dictionary<string, NPCData> medsNPCs = new();
        internal static Dictionary<string, NPCData> medsNPCsNamed = new();
        internal static SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
        internal static Dictionary<string, TraitData> medsTraitsCopy = new();
        internal static Dictionary<string, SubClassData> medsSubClassCopy = new();
        public static ConfigEntry<bool> medsVanillaContentLog { get; private set; }
        private void Awake()
        {
            Log = Logger;
            medsVanillaContentLog = Config.Bind(new ConfigDefinition("Debug", "Vanilla Content Logging"), false, new ConfigDescription("Logs the loading of each individual piece of vanilla content."));
            LogInfo($"{PluginInfo.PLUGIN_GUID} {PluginInfo.PLUGIN_VERSION} has loaded!");
            RegisterMod(
                _name: PluginInfo.PLUGIN_NAME,
                _author: "stiffmeds",
                _description: "Enables custom content in Across the Obelisk.",
                _version: PluginInfo.PLUGIN_VERSION,
                _date: ModDate,
                _link: @"https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Content/",
                _priority: 2000000000,
                _type: new string[2] { "Core", "Content" }
            );
            harmony.PatchAll();
        }
        internal static void LogDebug(string msg)
        {
            Log.LogDebug(msg);
        }
        internal static void LogInfo(string msg)
        {
            Log.LogInfo(msg);
        }
        internal static void LogWarning(string msg)
        {
            Log.LogWarning(msg);
        }
        internal static void LogError(string msg)
        {
            Log.LogError(msg);
        }
        internal static void LoadVanillaContent()
        {
            LogInfo("Loading vanilla content...");
            KeyNotesData[] keyNotesDataArray = Resources.LoadAll<KeyNotesData>("KeyNotes");
            AuraCurseData[] auraCurseDataArray1 = Resources.LoadAll<AuraCurseData>("Auras");
            AuraCurseData[] auraCurseDataArray2 = Resources.LoadAll<AuraCurseData>("Curses");
            CardData[] cardDataArray = Resources.LoadAll<CardData>("Cards");
            TraitData[] traitDataArray = Resources.LoadAll<TraitData>("Traits");
            HeroData[] heroDataArray = Resources.LoadAll<HeroData>("Heroes");
            PerkData[] perkDataArray = Resources.LoadAll<PerkData>("Perks");
            PackData[] packDataArray = Resources.LoadAll<PackData>("Packs");
            SubClassData[] subClassDataArray = Resources.LoadAll<SubClassData>("SubClass");
            NPCData[] npcDataArray = Resources.LoadAll<NPCData>("NPCs");
            PerkNodeData[] perkNodeDataArray = Resources.LoadAll<PerkNodeData>("PerkNode");
            EventData[] eventDataArray = Resources.LoadAll<EventData>("World/Events");
            EventRequirementData[] eventRequirementDataArray = Resources.LoadAll<EventRequirementData>("World/Events/Requirements");
            CombatData[] combatDataArray = Resources.LoadAll<CombatData>("World/Combats");
            NodeData[] nodeDataArray = Resources.LoadAll<NodeData>("World/MapNodes");
            TierRewardData[] tierRewardDataArray = Resources.LoadAll<TierRewardData>("Rewards");
            ItemData[] itemDataArray = Resources.LoadAll<ItemData>("Items");
            LootData[] lootDataArray = Resources.LoadAll<LootData>("Loot");
            SkinData[] skinDataArray = Resources.LoadAll<SkinData>("Skins");
            CardbackData[] cardbackDataArray = Resources.LoadAll<CardbackData>("Cardbacks");
            CorruptionPackData[] corruptionPackDataArray = Resources.LoadAll<CorruptionPackData>("CorruptionRewards");
            CardPlayerPackData[] cardPlayerPackDataArray = Resources.LoadAll<CardPlayerPackData>("CardPlayer");
            CinematicData[] cinematicDataArray = Resources.LoadAll<CinematicData>("Cinematics");
            ZoneData[] zoneDataArray = Resources.LoadAll<ZoneData>("World/Zones");
            ChallengeTrait[] challengeTraitArray = Resources.LoadAll<ChallengeTrait>("Challenge/Traits");
            ChallengeData[] challengeDataArray = Resources.LoadAll<ChallengeData>("Challenge/Weeks");
            CardPlayerPairsPackData[] playerPairsPackDataArray = Resources.LoadAll<CardPlayerPairsPackData>("CardPlayerPairs");

            Traverse.Create(Globals.Instance).Field("_CardsListSearch").SetValue(new Dictionary<string, List<string>>());

            // initialise vanilla vars? (why is this not in the declaration, do you think?)
            Globals.Instance.CardItemByType = new();
            Globals.Instance.CardEnergyCost = new();

            // vanilla AudioClips, noting that there are two walk_stone entries and this will just use the latter
            LogInfo("Loading vanilla AudioClips...");
            try
            {
                AudioClip[] foundAudioClips = Resources.FindObjectsOfTypeAll<AudioClip>();
                foreach (AudioClip ac in foundAudioClips)
                {
                    if (medsVanillaContentLog.Value) { LogInfo("Loading vanilla AudioClip: " + ac.name); };
                    medsAudioClips[ac.name] = ac;
                }
                LogInfo(medsAudioClips.Count + " vanilla AudioClips loaded");
            }
            catch (Exception e) { LogError("Error loading AudioClips: " + e.Message); }

            // vanilla sprites
            LogInfo("Loading vanilla Sprites...");
            try
            {
                Sprite[] foundSprites = Resources.FindObjectsOfTypeAll<UnityEngine.Sprite>();
                foreach (Sprite spr in foundSprites)
                {
                    if (medsVanillaContentLog.Value) { LogInfo("Loading vanilla Sprite: " + spr.name); };
                    medsSprites[spr.name.Trim().ToLower()] = spr;
                }
                LogInfo(medsSprites.Count + " vanilla Sprites loaded");
            }
            catch (Exception e) { LogError("Error loading Sprites: " + e.Message); }

            // vanilla gameobjects
            LogInfo("Loading vanilla gameObjects...");
            try
            {
                GameObject[] foundGOs = Resources.FindObjectsOfTypeAll<GameObject>();
                foreach (GameObject gObj in foundGOs)
                {
                    if (medsVanillaContentLog.Value) { LogInfo("Loading vanilla gameObject: " + gObj.name); };
                    if (medsGOs.ContainsKey(gObj.name))
                    {
                        int c1 = medsGOs[gObj.name].GetComponents(typeof(Component)).Count();
                        int c2 = gObj.GetComponents(typeof(Component)).Count();
                        if (c1 > c2)
                            continue;
                    }
                    medsGOs[gObj.name] = gObj;
                }
                LogInfo(medsGOs.Count + " vanilla GameObjects loaded");
            }
            catch (Exception e) { LogError("Error loading GameObjects: " + e.Message); }

            // vanilla thermometer tier data
            LogInfo("Loading vanilla thermometerTierData...");
            try
            {
                ThermometerTierData[] thermoTD = Resources.FindObjectsOfTypeAll<ThermometerTierData>();
                foreach (ThermometerTierData td in thermoTD)
                {
                    if (medsVanillaContentLog.Value) { LogInfo("Loading vanilla thermometerTierData: " + td.name); };
                    medsThermometerTierData[td.name] = td;
                }
                LogInfo("Loaded " + medsThermometerTierData.Count + " vanilla thermometerTierData");
            }
            catch (Exception e) { LogError("Error loading vanilla thermometerTierData: " + e.Message); }

            // vanilla keynotes
            LogInfo("Loading vanilla keynotes...");
            for (int index = 0; index < keyNotesDataArray.Length; ++index)
            {
                try
                {
                    string lower = keyNotesDataArray[index].KeynoteName.Replace(" ", "").ToLower();
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla keynote: " + lower); };
                    keyNotesDataArray[index].Id = lower;
                    medsKeyNotesDataSource[lower] = UnityEngine.Object.Instantiate<KeyNotesData>(keyNotesDataArray[index]);
                    medsKeyNotesDataSource[lower].KeynoteName = Texts.Instance.GetText(medsKeyNotesDataSource[lower].KeynoteName);
                    string text1 = Texts.Instance.GetText(lower + "_description", "keynotes");
                    if (text1 != "")
                        medsKeyNotesDataSource[lower].Description = text1;
                    string text2 = Texts.Instance.GetText(lower + "_descriptionExtended", "keynotes");
                    if (text2 != "")
                        medsKeyNotesDataSource[lower].DescriptionExtended = text2;
                }
                catch (Exception e) { LogError("Error loading vanilla keynotes: " + e.Message); }
            }
            LogInfo(medsKeyNotesDataSource.Count + " vanilla keynotes loaded");
            Globals.Instance.KeyNotes = medsKeyNotesDataSource;

            LogInfo("Loading auras & curses...");
            // vanilla auras & curses; this is basically CreateAuraCurses, but occurring earlier?
            for (int index = 0; index < auraCurseDataArray1.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla auraCurse: " + auraCurseDataArray1[index].ACName); };
                    auraCurseDataArray1[index].Init();
                    medsAurasCursesSource.Add(auraCurseDataArray1[index].Id, UnityEngine.Object.Instantiate<AuraCurseData>(auraCurseDataArray1[index]));
                    medsAurasCursesSource[auraCurseDataArray1[index].Id].Init();
                    medsAurasCursesSource[auraCurseDataArray1[index].Id].ACName = Texts.Instance.GetText(medsAurasCursesSource[auraCurseDataArray1[index].Id].Id);
                    string text = Texts.Instance.GetText(auraCurseDataArray1[index].Id + "_description", "auracurse");
                    if (text != "")
                        medsAurasCursesSource[auraCurseDataArray1[index].Id].Description = text;
                    medsACIndex.Add(auraCurseDataArray1[index].Id.ToLower());
                }
                catch (Exception e) { LogError("Error loading vanilla auraCurse: " + e.Message); }
            }
            for (int index = 0; index < auraCurseDataArray2.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla auraCurse: " + auraCurseDataArray2[index].ACName); };
                    auraCurseDataArray2[index].Init();
                    medsAurasCursesSource[auraCurseDataArray2[index].Id] = UnityEngine.Object.Instantiate<AuraCurseData>(auraCurseDataArray2[index]);
                    medsAurasCursesSource[auraCurseDataArray2[index].Id].Init();
                    medsAurasCursesSource[auraCurseDataArray2[index].Id].ACName = Texts.Instance.GetText(medsAurasCursesSource[auraCurseDataArray2[index].Id].Id);
                    string text = Texts.Instance.GetText(auraCurseDataArray2[index].Id + "_description", "auracurse");
                    if (text != "")
                        medsAurasCursesSource[auraCurseDataArray2[index].Id].Description = text;
                    medsACIndex.Add(auraCurseDataArray2[index].Id.ToLower());
                }
                catch (Exception e) { LogError("Error loading vanilla auraCurse: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_AurasCursesSource").SetValue(medsAurasCursesSource);
            Traverse.Create(Globals.Instance).Field("_AurasCursesIndex").SetValue(medsACIndex);
            Traverse.Create(Globals.Instance).Field("_AurasCurses").SetValue(medsAurasCursesSource);

            LogInfo("Loading cards...");
            // vanilla cards
            for (int index = 0; index < cardDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla card: " + cardDataArray[index].name); };
                    cardDataArray[index].Id = cardDataArray[index].name.ToLower();
                    medsCardsSource[cardDataArray[index].Id] = UnityEngine.Object.Instantiate<CardData>(cardDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla card: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_CardsSource").SetValue(medsCardsSource);

            LogInfo("Loading tier reward data...");
            // vanilla tier reward data
            for (int index = 0; index < tierRewardDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla tier reward data: " + tierRewardDataArray[index].name); };
                    medsTierRewardDataSource[tierRewardDataArray[index].TierNum] = UnityEngine.Object.Instantiate<TierRewardData>(tierRewardDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla tier reward data: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_TierRewardDataSource").SetValue(medsTierRewardDataSource);
            LogInfo("Tier reward data loaded!");

            LogInfo("Loading NPCs...");
            medsSecondRunImport = new();
            // vanilla NPCs
            for (int index = 0; index < npcDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla NPC: " + npcDataArray[index].Id); };
                    medsNPCsSource[npcDataArray[index].Id] = UnityEngine.Object.Instantiate<NPCData>(npcDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla NPC: " + e.Message); }
            }
            LogInfo("Creating vanilla NPC clones...");
            foreach (string key in medsNPCsSource.Keys)
            {
                try
                {
                    //LogDebug("NPC clone: " + key);
                    if (!sortedDictionary.ContainsKey(key))
                        sortedDictionary.Add(key, medsNPCsSource[key].NPCName);
                    if (medsSecondRunImport.ContainsKey(key))
                    {
                        medsNPCsSource[key].BaseMonster = !medsSecondRunImport[key][0].IsNullOrWhiteSpace() && medsNPCsSource.ContainsKey(medsSecondRunImport[key][0]) ? medsNPCsSource[medsSecondRunImport[key][0]] : (NPCData)null;
                        medsNPCsSource[key].HellModeMob = !medsSecondRunImport[key][1].IsNullOrWhiteSpace() && medsNPCsSource.ContainsKey(medsSecondRunImport[key][1]) ? medsNPCsSource[medsSecondRunImport[key][1]] : (NPCData)null;
                        medsNPCsSource[key].NgPlusMob = !medsSecondRunImport[key][2].IsNullOrWhiteSpace() && medsNPCsSource.ContainsKey(medsSecondRunImport[key][2]) ? medsNPCsSource[medsSecondRunImport[key][2]] : (NPCData)null;
                        medsNPCsSource[key].UpgradedMob = !medsSecondRunImport[key][3].IsNullOrWhiteSpace() && medsNPCsSource.ContainsKey(medsSecondRunImport[key][3]) ? medsNPCsSource[medsSecondRunImport[key][3]] : (NPCData)null;
                    }
                    medsNPCs.Add(key, UnityEngine.Object.Instantiate<NPCData>(medsNPCsSource[key]));
                    string text1 = Texts.Instance.GetText(key + "_name", "monsters");
                    if (text1 != "")
                        medsNPCs[key].NPCName = text1;
                    if (medsNPCsSource[key].IsNamed && medsNPCsSource[key].Difficulty > -1)
                    {
                        medsNPCsNamed.Add(key, UnityEngine.Object.Instantiate<NPCData>(medsNPCsSource[key]));
                        string text2 = Texts.Instance.GetText(key + "_name", "monsters");
                        if (text2 != "")
                            medsNPCsNamed[key].NPCName = text2;
                    }
                }
                catch (Exception e) { LogError("Error creating NPC clones: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_NPCsSource").SetValue(medsNPCsSource);
            Traverse.Create(Globals.Instance).Field("_NPCs").SetValue(medsNPCs);
            Traverse.Create(Globals.Instance).Field("_NPCsNamed").SetValue(medsNPCsNamed);
            LogInfo("NPCs loaded!");

            LogInfo("Loading item data...");
            // vanilla items
            for (int index = 0; index < itemDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla item data: " + itemDataArray[index].name); };
                    itemDataArray[index].Id = itemDataArray[index].name.ToLower();
                    medsItemDataSource[itemDataArray[index].Id] = UnityEngine.Object.Instantiate<ItemData>(itemDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla item data: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_ItemDataSource").SetValue(medsItemDataSource);

            LogInfo("Loading traits...");
            // vanilla traits
            for (int index = 0; index < traitDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla trait: " + traitDataArray[index].TraitName); };
                    traitDataArray[index].Init();
                    medsTraitsSource[traitDataArray[index].Id] = UnityEngine.Object.Instantiate<TraitData>(traitDataArray[index]);
                    medsTraitsSource[traitDataArray[index].Id].SetNameAndDescription();
                    medsTraitsCopy[traitDataArray[index].Id] = UnityEngine.Object.Instantiate<TraitData>(medsTraitsSource[traitDataArray[index].Id]);
                }
                catch (Exception e) { LogError("Error loading vanilla trait: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_TraitsSource").SetValue(medsTraitsSource);
            Traverse.Create(Globals.Instance).Field("_Traits").SetValue(medsTraitsCopy);

            LogDebug("Loading vanilla heroes...");
            Dictionary<string, HeroData> medsHeroes = new();
            for (int index = 0; index < heroDataArray.Length; ++index)
            {
                try
                {
                    medsHeroes.Add(heroDataArray[index].Id, UnityEngine.Object.Instantiate<HeroData>(heroDataArray[index]));
                }
                catch (Exception e) { LogError("Error loading vanilla heroes: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_Heroes").SetValue(medsHeroes);
            LogInfo("Heroes loaded!");

            // vanilla perks
            for (int index = 0; index < perkDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla perk: " + perkDataArray[index].Id); };
                    perkDataArray[index].Init();
                    medsPerksSource[perkDataArray[index].Id] = UnityEngine.Object.Instantiate<PerkData>(perkDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla perk: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_PerksSource").SetValue(medsPerksSource);

            // vanilla packdata
            LogInfo("Loading PackData...");
            for (int index = 0; index < packDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla PackData: " + packDataArray[index].PackId); };
                    medsPackDataSource[packDataArray[index].PackId.ToLower()] = UnityEngine.Object.Instantiate<PackData>(packDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla PackData: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_PackDataSource").SetValue(medsPackDataSource);

            LogInfo("Loading subclasses...");
            // vanilla subclasses
            for (int index = 0; index < subClassDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla subclass: " + subClassDataArray[index].SubClassName); };
                    medsSubClassesSource[subClassDataArray[index].SubClassName.Replace(" ", "").ToLower()] = UnityEngine.Object.Instantiate<SubClassData>(subClassDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla subclass: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_SubClassSource").SetValue(medsSubClassesSource);

            LogInfo("Creating subclass text...");
            foreach (string key in medsSubClassesSource.Keys)
            {
                try
                {
                    medsSubClassCopy[key] = UnityEngine.Object.Instantiate<SubClassData>(medsSubClassesSource[key]);
                    if (medsSubClassCopy[key].CharacterName.Length < 2)
                        medsSubClassCopy[key].CharacterName = Texts.Instance.GetText(key + "_name", "class");
                    if (medsSubClassCopy[key].CharacterDescription.Length < 2)
                        medsSubClassCopy[key].CharacterDescription = Texts.Instance.GetText(key + "_description", "class");
                    if (medsSubClassCopy[key].CharacterDescriptionStrength.Length < 2)
                        medsSubClassCopy[key].CharacterDescriptionStrength = Texts.Instance.GetText(key + "_strength", "class");
                }
                catch (Exception e) { LogError("Error creating subclass text: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_SubClass").SetValue(medsSubClassCopy);
            LogInfo("Subclass text created!");

            LogInfo("Loading SkinData...");
            // vanilla skins
            for (int index = 0; index < skinDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla SkinData: " + skinDataArray[index].SkinId); };
                    medsSkinsSource[skinDataArray[index].SkinId.ToLower()] = UnityEngine.Object.Instantiate<SkinData>(skinDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla SkinData: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_SkinDataSource").SetValue(medsSkinsSource);

            LogInfo("Loading CardbackData...");
            // vanilla cardbacks
            for (int index = 0; index < cardbackDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla CardbackData: " + cardbackDataArray[index].CardbackId); };
                    medsCardbacksSource[cardbackDataArray[index].CardbackId.ToLower()] = UnityEngine.Object.Instantiate<CardbackData>(cardbackDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla CardbackData: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_CardbackDataSource").SetValue(medsCardbacksSource);

            LogInfo("Loading perknodes...");
            // vanilla perknodes
            for (int index = 0; index < perkNodeDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla perknode: " + perkNodeDataArray[index].Id); };
                    medsPerksNodesSource[perkNodeDataArray[index].Id] = UnityEngine.Object.Instantiate<PerkNodeData>(perkNodeDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla perknode: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_PerksNodesSource").SetValue(medsPerksNodesSource);

            LogInfo("Loading event requirements...");
            // vanilla event requirements
            for (int index = 0; index < eventRequirementDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla event requirement: " + eventRequirementDataArray[index].RequirementId); };
                    string lower = eventRequirementDataArray[index].RequirementId.ToLower();
                    medsEventRequirementDataSource[lower] = UnityEngine.Object.Instantiate<EventRequirementData>(eventRequirementDataArray[index]);
                    if (medsEventRequirementDataSource[lower].ItemTrack || medsEventRequirementDataSource[lower].RequirementTrack)
                    {
                        string text3 = Texts.Instance.GetText(lower + "_name", "requirements");
                        if (text3 != "")
                            medsEventRequirementDataSource[lower].RequirementName = text3;
                        string text4 = Texts.Instance.GetText(lower + "_description", "requirements");
                        if (text4 != "")
                            medsEventRequirementDataSource[lower].Description = text4;
                    }
                }
                catch (Exception e) { LogError("Error loading vanilla event requirement: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_Requirements").SetValue(medsEventRequirementDataSource);

            LogInfo("Loading pairs packs...");
            // vanilla pairs packs
            for (int index = 0; index < playerPairsPackDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla pairs pack data: " + playerPairsPackDataArray[index].PackId); };
                    medsCardPlayerPairsPackDataSource[playerPairsPackDataArray[index].PackId.ToLower()] = UnityEngine.Object.Instantiate<CardPlayerPairsPackData>(playerPairsPackDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla pairs pack data: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_CardPlayerPairsPackDataSource").SetValue(medsCardPlayerPairsPackDataSource);

            LogInfo("Loading CorruptionPackData...");
            // vanilla corruption packs
            for (int index = 0; index < corruptionPackDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla CorruptionPackData: " + corruptionPackDataArray[index].name); };
                    medsCorruptionPackDataSource[corruptionPackDataArray[index].name] = UnityEngine.Object.Instantiate<CorruptionPackData>(corruptionPackDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla CorruptionPackData: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_CorruptionPackDataSource").SetValue(medsCorruptionPackDataSource);

            LogInfo("Loading CardPlayerPackData...");
            // vanilla player packs
            for (int index = 0; index < cardPlayerPackDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla CardPlayerPackData: " + cardPlayerPackDataArray[index].PackId); };
                    medsCardPlayerPackDataSource[cardPlayerPackDataArray[index].PackId.ToLower()] = UnityEngine.Object.Instantiate<CardPlayerPackData>(cardPlayerPackDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla CardPlayerPackData: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_CardPlayerPackDataSource").SetValue(medsCardPlayerPackDataSource);

            LogInfo("Loading cinematic data...");
            // vanilla cinematics
            for (int index = 0; index < cinematicDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla cinematic data: " + cinematicDataArray[index].CinematicId); };
                    medsCinematicDataSource[cinematicDataArray[index].CinematicId.Replace(" ", "").ToLower()] = UnityEngine.Object.Instantiate<CinematicData>(cinematicDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla cinematic: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_Cinematics").SetValue(medsCinematicDataSource);

            LogInfo("Loading combat data...");
            // vanilla combats
            for (int index = 0; index < combatDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla combatData: " + combatDataArray[index].CombatId); };
                    medsCombatDataSource[combatDataArray[index].CombatId.Replace(" ", "").ToLower()] = UnityEngine.Object.Instantiate<CombatData>(combatDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla combatData: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_CombatDataSource").SetValue(medsCombatDataSource);

            LogInfo("Loading LootData...");
            // vanilla lootData
            for (int index = 0; index < lootDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla LootData: " + lootDataArray[index].Id); };
                    medsLootDataSource[lootDataArray[index].Id.ToLower()] = UnityEngine.Object.Instantiate<LootData>(lootDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla LootData: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_LootDataSource").SetValue(medsLootDataSource);

            LogInfo("Loading zone data...");
            // vanilla zone data
            for (int index = 0; index < zoneDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla zone data: " + zoneDataArray[index].ZoneId); };
                    medsZoneDataSource[zoneDataArray[index].ZoneId.ToLower()] = UnityEngine.Object.Instantiate<ZoneData>(zoneDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla zone data: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_ZoneDataSource").SetValue(medsZoneDataSource);

            LogInfo("Loading node data...");
            // vanilla node data
            for (int index = 0; index < nodeDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla node data: " + nodeDataArray[index].NodeId); };
                    string lower = nodeDataArray[index].NodeId.ToLower();
                    if (!medsNodesByZone.ContainsKey(nodeDataArray[index].NodeZone.ZoneId.ToLower()))
                        medsNodesByZone[nodeDataArray[index].NodeZone.ZoneId.ToLower()] = new List<NodeDataText>();
                    if (!medsNodesByZone[nodeDataArray[index].NodeZone.ZoneId.ToLower()].Contains(ToText(nodeDataArray[index])))
                        medsNodesByZone[nodeDataArray[index].NodeZone.ZoneId.ToLower()].Add(ToText(nodeDataArray[index]));
                    medsNodeDataSource[lower] = nodeDataArray[index];
                    medsNodeDataSource[lower].name = lower;
                }
                catch (Exception e) { LogError("Error loading vanilla node data: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_NodeDataSource").SetValue(medsNodeDataSource);
            Traverse.Create(Globals.Instance).Field("_NodeCombatEventRelation").SetValue(medsNodeCombatEventRelation);

            LogInfo("Loading events...");
            // vanilla events
            for (int index = 0; index < eventDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla event: " + eventDataArray[index].EventId); };
                    EventData eventData = UnityEngine.Object.Instantiate<EventData>(eventDataArray[index]);
                    eventData.Init();
                    medsEventDataSource[eventData.EventId.ToLower()] = eventData;
                }
                catch (Exception e) { LogError("Error loading vanilla events: " + e.Message); }
            }
            // save vanilla+custom events
            Traverse.Create(Globals.Instance).Field("_Events").SetValue(medsEventDataSource);

            LogInfo("Loading challenge traits...");
            // vanilla challenge traits
            for (int index = 0; index < challengeTraitArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla challengeTrait: " + challengeTraitArray[index].Id); };
                    medsChallengeTraitsSource[challengeTraitArray[index].Id.ToLower()] = UnityEngine.Object.Instantiate<ChallengeTrait>(challengeTraitArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla challengeTrait: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_ChallengeTraitsSource").SetValue(medsChallengeTraitsSource);

            LogInfo("Loading challenge data...");
            // vanilla challenge data
            for (int index = 0; index < challengeDataArray.Length; ++index)
            {
                try
                {
                    if (medsVanillaContentLog.Value) { LogDebug("Loading vanilla challengeData: " + challengeDataArray[index].Id); };
                    medsChallengeDataSource[challengeDataArray[index].Id.ToLower()] = UnityEngine.Object.Instantiate<ChallengeData>(challengeDataArray[index]);
                }
                catch (Exception e) { LogError("Error loading vanilla challengeData: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_WeeklyDataSource").SetValue(medsChallengeDataSource);
        }
        internal static bool LoadCustomContent(string sFolderName)
        {
            Texture2D spriteTexture;
            int customCount;
            FileInfo[] medsFI;
            if (!Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName)))
            {
                LogError("Cannot find content pack folder: " + sFolderName);
                return false;
            }
            else
            {
                LogInfo("Loading content pack: " + sFolderName);
            }

            // custom audioClips
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "audio")))
            {
                customCount = 0;
                LogInfo("Loading AudioClips from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "audio"))).GetFiles("*.wav");
                foreach (FileInfo f in medsFI)
                {
                    LogInfo("Loading " + sFolderName + " AudioClip: " + f.Name);
                    string path = "file:///" + f.ToString().Replace("\\", "/");
                    try
                    {
                        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
                        {
                            www.SendWebRequest();
                            while (!www.isDone) { }
                            if (www.result == UnityWebRequest.Result.ConnectionError)
                            {
                                LogError(www.error);
                            }
                            else
                            {
                                AudioClip ac = DownloadHandlerAudioClip.GetContent(www);
                                ac.name = Path.GetFileNameWithoutExtension(f.Name);
                                medsAudioClips[Path.GetFileNameWithoutExtension(f.Name)] = ac;
                                customCount++;
                            }
                        }
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " AudioClip " + f.Name + ": " + e.Message); }
                }
                if (customCount > 0) { LogInfo("Loaded " + customCount + " AudioClips from " + sFolderName); };
            }

            // custom sprites
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "sprite")))
            {
                customCount = 0;
                LogInfo("Loading Sprites from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "sprite"))).GetFiles("*.png");
                foreach (FileInfo f in medsFI)
                {
                    LogInfo("Loading " + sFolderName + " Sprite: " + f.Name);
                    try
                    {
                        spriteTexture = new Texture2D(0, 0);
                        spriteTexture.LoadImage(File.ReadAllBytes(f.ToString()));
                        Sprite medsSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect);
                        medsSprite.name = Path.GetFileNameWithoutExtension(f.Name).Trim().ToLower();
                        medsSprites[Path.GetFileNameWithoutExtension(f.Name).Trim().ToLower()] = medsSprite;
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " Sprite " + f.Name + ": " + e.Message); }
                }
                if (customCount > 0) { LogInfo("Loaded " + customCount + " Sprites from " + sFolderName); };
            }

            // custom gameObjects
            // #TODO: _actual_ custom gameObjects
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "gameObject")))
            {
                customCount = 0;
                LogInfo("Loading gameObjects from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "gameObject"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    LogInfo("Loading " + sFolderName + " gameObject retexture: " + f.Name);
                    try
                    {
                        GameObjectRetexture medsGOR = JsonUtility.FromJson<GameObjectRetexture>(File.ReadAllText(f.ToString()));
                        GameObject newGO = GetGO(medsGOR.BaseGameObjectID);
                        if (newGO != null)
                        {
                            newGO.name = medsGOR.NewGameObjectID;
                            Sprite newTexture = medsGOR.SpriteToUse.IsNullOrWhiteSpace() ? (Sprite)null : GetSprite(medsGOR.SpriteToUse);
                            LogInfo("localScale: " + newGO.transform.localScale.ToString());
                            newGO.transform.localScale = new Vector3(newGO.transform.localScale.x * medsGOR.ScaleX * (medsGOR.Flip ? -1 : 1), newGO.transform.localScale.y * medsGOR.ScaleY, newGO.transform.localScale.z);
                            LogInfo("Flip: " + medsGOR.Flip.ToString());
                            LogInfo("Scale: " + medsGOR.ScaleX.ToString() + "," + medsGOR.ScaleY.ToString());
                            LogInfo("NEW localScale: " + newGO.transform.localScale.ToString());
                            if (newTexture != null)
                            {
                                try
                                {
                                    SpriteRenderer[] GOSRs = newGO.GetComponentsInChildren<SpriteRenderer>(true);
                                    foreach (SpriteRenderer SR in GOSRs)
                                    {
                                        if (SR.sprite != (Sprite)null && SR.gameObject.GetComponent<UnityEngine.U2D.Animation.SpriteSkin>() != null)// && SR.sprite.texture != (Texture2D)null)
                                        {

                                            LogDebug("trying to change sprite for spriterenderer: " + SR.name);
                                            // attempt 3: make new sprites, set bones, set bind poses
                                            Sprite actualNewSprite = Sprite.Create(newTexture.texture, new Rect(SR.sprite.rect.x, SR.sprite.rect.y, SR.sprite.rect.width, SR.sprite.rect.height), new Vector2(SR.sprite.pivot.x / SR.sprite.rect.width, SR.sprite.pivot.y / SR.sprite.rect.height));
                                            int vertexCount = SR.sprite.GetVertexCount();

                                            NativeArray<Vector2> uvArr = new(vertexCount, Allocator.Temp);
                                            NativeArray<Vector3> vertexArr = new(vertexCount, Allocator.Temp);
                                            NativeArray<Vector4> tangentArr = new(vertexCount, Allocator.Temp);
                                            NativeArray<BoneWeight> blendWeightArr = new(vertexCount, Allocator.Temp);

                                            NativeSlice<Vector2> uvRef = SR.sprite.GetVertexAttribute<Vector2>(VertexAttribute.TexCoord0);
                                            NativeSlice<Vector3> vertexRef = SR.sprite.GetVertexAttribute<Vector3>(VertexAttribute.Position);
                                            NativeSlice<Vector4> tangentRef = SR.sprite.GetVertexAttribute<Vector4>(VertexAttribute.Tangent);
                                            NativeSlice<BoneWeight> blendWeightRef = SR.sprite.GetVertexAttribute<BoneWeight>(VertexAttribute.BlendWeight);

                                            for (var i = 0; i < vertexCount; ++i)
                                            {
                                                uvArr[i] = uvRef[i];
                                                vertexArr[i] = vertexRef[i];
                                                tangentArr[i] = tangentRef[i];
                                                blendWeightArr[i] = blendWeightRef[i];
                                            }
                                            actualNewSprite.SetIndices(SR.sprite.GetIndices());
                                            actualNewSprite.SetVertexCount(SR.sprite.GetVertexCount());
                                            actualNewSprite.SetVertexAttribute(VertexAttribute.TexCoord0, uvArr);
                                            actualNewSprite.SetVertexAttribute(VertexAttribute.Position, vertexArr);
                                            actualNewSprite.SetVertexAttribute(VertexAttribute.Tangent, tangentArr);
                                            actualNewSprite.SetVertexAttribute(VertexAttribute.BlendWeight, blendWeightArr);
                                            uvArr.Dispose();
                                            vertexArr.Dispose();
                                            tangentArr.Dispose();
                                            blendWeightArr.Dispose();
                                            actualNewSprite.SetBones(SR.sprite.GetBones());
                                            actualNewSprite.SetBindPoses(SR.sprite.GetBindPoses());
                                            SR.sprite = actualNewSprite;
                                        }
                                    }
                                }
                                catch
                                {
                                    LogError("Unable to retexture gameObject " + medsGOR.NewGameObjectID + " for unknown reason! Please let Meds know.");
                                }
                            }
                            else if (!medsGOR.SpriteToUse.IsNullOrWhiteSpace())
                            {
                                LogError("Could not find gameObject retexture sprite: " + medsGOR.SpriteToUse);
                            }
                            medsGOs[medsGOR.NewGameObjectID] = newGO;
                            customCount++;
                        }
                        else
                        {
                            LogError("Could not find base gameObject with ID: " + medsGOR.BaseGameObjectID);
                        }
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " GameObject " + f.Name + ": " + e.Message); }
                }
                if (customCount > 0) { LogInfo("Loaded " + customCount + " gameObject retextures from " + sFolderName); };
            }

            // custom keynotes
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "keyNote")))
            {
                customCount = 0;
                LogInfo("Loading keynotes from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "keyNote"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    LogInfo("Loading " + sFolderName + " keynote: " + f.Name);
                    try
                    {
                        KeyNotesData medsKeyNote = ToData(JsonUtility.FromJson<KeyNotesDataText>(File.ReadAllText(f.ToString())));
                        medsKeyNotesDataSource[medsKeyNote.Id] = UnityEngine.Object.Instantiate<KeyNotesData>(medsKeyNote);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " keynote " + f.Name + ": " + e.Message); }
                }
                Globals.Instance.KeyNotes = medsKeyNotesDataSource;
                if (customCount > 0) { LogInfo("Loaded " + customCount + " keynotes from " + sFolderName); };
            }

            // custom auras & curses
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "auraCurse")))
            {
                customCount = 0;
                LogInfo("Loading auraCurses from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "auraCurse"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    LogInfo("Loading " + sFolderName + " auraCurse: " + f.Name);
                    try
                    {
                        AuraCurseData medsACSingle = ToData(JsonUtility.FromJson<AuraCurseDataText>(File.ReadAllText(f.ToString())));
                        medsACSingle.Init();
                        medsAurasCursesSource[medsACSingle.Id] = UnityEngine.Object.Instantiate<AuraCurseData>(medsACSingle);
                        if (!medsACIndex.Contains(medsACSingle.Id.ToLower()))
                            medsACIndex.Add(medsACSingle.Id.ToLower());
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " auraCurse " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_AurasCursesSource").SetValue(medsAurasCursesSource);
                Traverse.Create(Globals.Instance).Field("_AurasCursesIndex").SetValue(medsACIndex);
                Traverse.Create(Globals.Instance).Field("_AurasCurses").SetValue(medsAurasCursesSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " auraCurses from " + sFolderName); };
            }

            // custom cards
            medsCardsNeedingItems = new();
            medsCardsNeedingItemEnchants = new();
            medsSecondRunImport = new();
            medsSecondRunImport2 = new();
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "card")))
            {
                customCount = 0;
                LogInfo("Loading cards from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "card"))).GetFiles("*.json");
                Dictionary<string, CardData> medsCardsCustom = new();
                foreach (FileInfo f in medsFI)
                {
                    LogInfo("Loading " + sFolderName + " card: " + f.Name);
                    try
                    {
                        CardData medsCard = ToData(JsonUtility.FromJson<CardDataText>(File.ReadAllText(f.ToString())));
                        // #TODO: UNLOCKS?
                        if (!medsCustomUnlocks.Contains(medsCard.Id))
                            medsCustomUnlocks.Add(medsCard.Id);
                        medsCardsSource[medsCard.Id] = UnityEngine.Object.Instantiate<CardData>(medsCard);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " card " + f.Name + ": " + e.Message); }
                }
                SortedDictionary<string, CardData> medsCardsTemp = new SortedDictionary<string, CardData>(medsCardsSource);
                medsCardsSource = new Dictionary<string, CardData>(medsCardsTemp);
                // do a second run to link AddCardList
                LogInfo("Loading custom card component for " + sFolderName + ": AddCardList...");
                foreach (string key in medsSecondRunImport.Keys)
                {
                    try
                    {
                        medsCardsSource[key].AddCardList = new CardData[medsSecondRunImport[key].Length];
                        for (int a = 0; a < medsSecondRunImport[key].Length; a++)
                        {
                            if (medsCardsSource.ContainsKey(medsSecondRunImport[key][a]))
                                medsCardsSource[key].AddCardList[a] = medsCardsSource[medsSecondRunImport[key][a]];
                            else
                                medsCardsSource[key].AddCardList[a] = (CardData)null;
                        }
                    }
                    catch (Exception e) { LogError("Error loading AddCardList: " + e.Message); }
                }
                // do a second run to link UpgradesToRare
                LogInfo("Loading custom card component for " + sFolderName + ": UpgradesToRare...");
                foreach (string key in medsSecondRunImport2.Keys)
                {
                    try
                    {
                        if (medsCardsSource.ContainsKey(medsSecondRunImport2[key]))
                            medsCardsSource[key].UpgradesToRare = medsCardsSource[medsSecondRunImport2[key]];
                    }
                    catch (Exception e) { LogError("Error loading UpgradesToRare: " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_CardsSource").SetValue(medsCardsSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " cards from " + sFolderName); };
            }

            // custom tierRewards
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "tierReward")))
            {
                customCount = 0;
                LogInfo("Loading tierRewards from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "tierReward"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    LogInfo("Loading " + sFolderName + " tierReward: " + f.Name);
                    try
                    {
                        TierRewardData medsTRD = ToData(JsonUtility.FromJson<TierRewardDataText>(File.ReadAllText(f.ToString())));
                        medsTierRewardDataSource[medsTRD.TierNum] = UnityEngine.Object.Instantiate<TierRewardData>(medsTRD);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " tierReward " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_TierRewardDataSource").SetValue(medsTierRewardDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " tierRewards from " + sFolderName); };
            }

            // custom NPCs
            medsSecondRunImport = new();
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "NPC")))
            {
                customCount = 0;
                LogInfo("Loading NPCs from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "NPC"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " NPC: " + f.Name);
                        NPCData medsNPC = ToData(JsonUtility.FromJson<NPCDataText>(File.ReadAllText(f.ToString())));
                        medsNPCsSource[medsNPC.Id] = UnityEngine.Object.Instantiate<NPCData>(medsNPC);
                        //LogDebug("NPC clone: " + key);
                        sortedDictionary[medsNPC.Id] = medsNPCsSource[medsNPC.Id].NPCName;
                        if (medsSecondRunImport.ContainsKey(medsNPC.Id))
                        {
                            medsNPCsSource[medsNPC.Id].BaseMonster = !medsSecondRunImport[medsNPC.Id][0].IsNullOrWhiteSpace() && medsNPCsSource.ContainsKey(medsSecondRunImport[medsNPC.Id][0]) ? medsNPCsSource[medsSecondRunImport[medsNPC.Id][0]] : (NPCData)null;
                            medsNPCsSource[medsNPC.Id].HellModeMob = !medsSecondRunImport[medsNPC.Id][1].IsNullOrWhiteSpace() && medsNPCsSource.ContainsKey(medsSecondRunImport[medsNPC.Id][1]) ? medsNPCsSource[medsSecondRunImport[medsNPC.Id][1]] : (NPCData)null;
                            medsNPCsSource[medsNPC.Id].NgPlusMob = !medsSecondRunImport[medsNPC.Id][2].IsNullOrWhiteSpace() && medsNPCsSource.ContainsKey(medsSecondRunImport[medsNPC.Id][2]) ? medsNPCsSource[medsSecondRunImport[medsNPC.Id][2]] : (NPCData)null;
                            medsNPCsSource[medsNPC.Id].UpgradedMob = !medsSecondRunImport[medsNPC.Id][3].IsNullOrWhiteSpace() && medsNPCsSource.ContainsKey(medsSecondRunImport[medsNPC.Id][3]) ? medsNPCsSource[medsSecondRunImport[medsNPC.Id][3]] : (NPCData)null;
                        }
                        medsNPCs[medsNPC.Id] = UnityEngine.Object.Instantiate<NPCData>(medsNPCsSource[medsNPC.Id]);
                        string text1 = Texts.Instance.GetText(medsNPC.Id + "_name", "monsters");
                        if (text1 != "")
                            medsNPCs[medsNPC.Id].NPCName = text1;
                        if (medsNPCsSource[medsNPC.Id].IsNamed && medsNPCsSource[medsNPC.Id].Difficulty > -1)
                        {
                            medsNPCsNamed[medsNPC.Id] = UnityEngine.Object.Instantiate<NPCData>(medsNPCsSource[medsNPC.Id]);
                            string text2 = Texts.Instance.GetText(medsNPC.Id + "_name", "monsters");
                            if (text2 != "")
                                medsNPCsNamed[medsNPC.Id].NPCName = text2;
                        }
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " NPC " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_NPCsSource").SetValue(medsNPCsSource);
                Traverse.Create(Globals.Instance).Field("_NPCs").SetValue(medsNPCs);
                Traverse.Create(Globals.Instance).Field("_NPCsNamed").SetValue(medsNPCsNamed);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " NPCs from " + sFolderName); };
            }

            // second run through cards to connect items...
            LogInfo("Loading custom card component for " + sFolderName + ": Item...");
            foreach (string key in medsCardsNeedingItems.Keys)
            {
                try
                {
                    ItemData newItem = ToData(JsonUtility.FromJson<ItemDataText>(medsCardsNeedingItems[key]));
                    LogDebug("Loading custom item: " + newItem.Id);
                    medsItemDataSource[newItem.Id] = UnityEngine.Object.Instantiate<ItemData>(newItem);
                    medsCardsSource[key].Item = medsItemDataSource[newItem.Id];
                }
                catch (Exception e) { LogError("Error loading custom " + sFolderName + " card item " + key + ": " + e.Message); }
            }
            LogInfo("Loading custom card component for " + sFolderName + ": ItemEnchantment...");
            foreach (string key in medsCardsNeedingItemEnchants.Keys)
            {
                try
                {
                    ItemData newItem = ToData(JsonUtility.FromJson<ItemDataText>(medsCardsNeedingItemEnchants[key]));
                    LogDebug("Loading custom enchantment: " + newItem.Id);
                    medsItemDataSource[newItem.Id] = UnityEngine.Object.Instantiate<ItemData>(newItem);
                    medsCardsSource[key].ItemEnchantment = medsItemDataSource[newItem.Id];
                }
                catch (Exception e) { LogError("Error loading custom " + sFolderName + " card enchantment " + key + ": " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_ItemDataSource").SetValue(medsItemDataSource);

            // custom traits
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "trait")))
            {
                customCount = 0;
                LogInfo("Loading traits from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "trait"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " trait: " + f.Name);
                        TraitData medsTrait = ToData(JsonUtility.FromJson<TraitDataText>(File.ReadAllText(f.ToString())));
                        medsTraitsSource[medsTrait.Id] = UnityEngine.Object.Instantiate<TraitData>(medsTrait);
                        medsTraitsCopy[medsTrait.Id] = UnityEngine.Object.Instantiate<TraitData>(medsTraitsSource[medsTrait.Id]);
                        if (!(medsCustomTraitsSource.Contains(medsTrait.Id)))
                            medsCustomTraitsSource.Add(medsTrait.Id);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " trait " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_TraitsSource").SetValue(medsTraitsSource);
                Traverse.Create(Globals.Instance).Field("_Traits").SetValue(medsTraitsCopy);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " traits from " + sFolderName); };
            }

            // custom perks
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "perk")))
            {
                customCount = 0;
                LogInfo("Loading perks from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "perk"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " perk: " + f.Name);
                        PerkData medsPerk = ToData(JsonUtility.FromJson<PerkDataText>(File.ReadAllText(f.ToString())));
                        medsPerksSource[medsPerk.Id] = UnityEngine.Object.Instantiate<PerkData>(medsPerk);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " perk " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_PerksSource").SetValue(medsPerksSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " perks from " + sFolderName); };
            }

            // custom packs
            medsSecondRunImport2 = new();
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "pack")))
            {
                customCount = 0;
                LogInfo("Loading packs from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "pack"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " pack: " + f.Name);
                        PackData medsPack = ToData(JsonUtility.FromJson<PackDataText>(File.ReadAllText(f.ToString())));
                        medsPackDataSource[medsPack.PackId] = UnityEngine.Object.Instantiate<PackData>(medsPack);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " pack " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_PackDataSource").SetValue(medsPackDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " packs from " + sFolderName); };
            }

            // custom subclasses
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "subclass")))
            {
                customCount = 0;
                LogInfo("Loading subclasses from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "subclass"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " subclass: " + f.Name);
                        SubClassData medsSubClass = ToData(JsonUtility.FromJson<SubClassDataText>(File.ReadAllText(f.ToString())));
                        medsSubClassesSource[medsSubClass.Id] = UnityEngine.Object.Instantiate<SubClassData>(medsSubClass);
                        medsSubClassCopy[medsSubClass.Id] = UnityEngine.Object.Instantiate<SubClassData>(medsSubClassesSource[medsSubClass.Id]);
                        if (medsSubClassCopy[medsSubClass.Id].CharacterName.Length < 2)
                            medsSubClassCopy[medsSubClass.Id].CharacterName = Texts.Instance.GetText(medsSubClass.Id + "_name", "class");
                        if (medsSubClassCopy[medsSubClass.Id].CharacterDescription.Length < 2)
                            medsSubClassCopy[medsSubClass.Id].CharacterDescription = Texts.Instance.GetText(medsSubClass.Id + "_description", "class");
                        if (medsSubClassCopy[medsSubClass.Id].CharacterDescriptionStrength.Length < 2)
                            medsSubClassCopy[medsSubClass.Id].CharacterDescriptionStrength = Texts.Instance.GetText(medsSubClass.Id + "_strength", "class");
                        customCount++;
                        
                        if (medsSubClassCopy[medsSubClass.Id].InitialUnlock){
                            LogInfo("Actually Unlocking " + medsSubClass.Id);
                            List<string> unlockedHeroes = (List<string>) Traverse.Create(PlayerManager.Instance).Field("unlockedHeroes").GetValue();
                            unlockedHeroes.Add(medsSubClass.Id.ToLower());
                            Traverse.Create(PlayerManager.Instance).Field("unlockedHeroes").SetValue(unlockedHeroes);
                            //Traverse.Create(Globals.Instance).Field("_SubClass").GetValue():
                            //HeroUnlock(medsSubClass.Id.ToLower(), save: false, achievement: false);
                        }

                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " subclass " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_SubClassSource").SetValue(medsSubClassesSource);
                Traverse.Create(Globals.Instance).Field("_SubClass").SetValue(medsSubClassCopy);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " subclasses from " + sFolderName); };

                // connect obelisk challenge packs to subclasses
                LogDebug("pack-subclass binding commenced...");
                foreach (string packID in medsSecondRunImport2.Keys)
                {
                    string subclassID = medsSecondRunImport2[packID].ToLower().Trim();
                    if (medsSubClassesSource.ContainsKey(subclassID) && medsPackDataSource.ContainsKey(packID))
                        medsPackDataSource[packID].RequiredClass = medsSubClassesSource[subclassID];
                }
                Traverse.Create(Globals.Instance).Field("_PackDataSource").SetValue(medsPackDataSource);

                // unlock auto-unlock heroes

            }

            // custom skins
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "skin")))
            {
                customCount = 0;
                LogInfo("Loading skins from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "skin"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " skin: " + f.Name);
                        SkinData medsSkin = ToData(JsonUtility.FromJson<SkinDataText>(File.ReadAllText(f.ToString())));
                        medsSkinsSource[medsSkin.SkinId.ToLower()] = UnityEngine.Object.Instantiate<SkinData>(medsSkin);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " skin " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_SkinDataSource").SetValue(medsSkinsSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " skins from " + sFolderName); };
            }


            // custom cardbacks
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "cardback")))
            {
                customCount = 0;
                LogInfo("Loading cardbacks from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "cardback"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " cardback: " + f.Name);
                        CardbackData medsCardback = ToData(JsonUtility.FromJson<CardbackDataText>(File.ReadAllText(f.ToString())));
                        medsCardbacksSource[medsCardback.CardbackId.ToLower()] = UnityEngine.Object.Instantiate<CardbackData>(medsCardback);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " cardback " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_CardbackDataSource").SetValue(medsCardbacksSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " cardbacks from " + sFolderName); };
            }


            // custom perkNodes
            medsSecondRunImport = new();
            medsSecondRunImport2 = new();
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "perkNode")))
            {
                customCount = 0;
                LogInfo("Loading perkNodes from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "perkNode"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " perkNode: " + f.Name);
                        PerkNodeData medsPN = ToData(JsonUtility.FromJson<PerkNodeDataText>(File.ReadAllText(f.ToString())));
                        medsPerksNodesSource[medsPN.Id] = UnityEngine.Object.Instantiate<PerkNodeData>(medsPN);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " perkNode " + f.Name + ": " + e.Message); }
                }
                // late binding of perknodes
                foreach (string key in medsPerksNodesSource.Keys)
                {
                    try
                    {
                        if (medsSecondRunImport.ContainsKey(key))
                        {
                            medsPerksNodesSource[key].PerksConnected = new PerkNodeData[medsSecondRunImport[key].Length];
                            for (int a = 0; a < medsSecondRunImport[key].Length; a++)
                                medsPerksNodesSource[key].PerksConnected[a] = medsPerksNodesSource.ContainsKey(medsSecondRunImport[key][a]) ? medsPerksNodesSource[medsSecondRunImport[key][a]] : (PerkNodeData)null;
                        }
                        if (medsSecondRunImport2.ContainsKey(key))
                            medsPerksNodesSource[key].PerkRequired = medsPerksNodesSource.ContainsKey(medsSecondRunImport2[key]) ? medsPerksNodesSource[medsSecondRunImport2[key]] : (PerkNodeData)null;
                    }
                    catch (Exception e) { LogError("Error performing late binding of " + sFolderName + " perknodes: " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_PerksNodesSource").SetValue(medsPerksNodesSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " perkNodes from " + sFolderName); };
            }

            // custom eventRequirements
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "eventRequirement")))
            {
                customCount = 0;
                LogInfo("Loading eventRequirements from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "eventRequirement"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " eventRequirement: " + f.Name);
                        EventRequirementData medsERD = ToData(JsonUtility.FromJson<EventRequirementDataText>(File.ReadAllText(f.ToString())));
                        medsEventRequirementDataSource[medsERD.RequirementId] = UnityEngine.Object.Instantiate<EventRequirementData>(medsERD);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " eventRequirement " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_Requirements").SetValue(medsEventRequirementDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " eventRequirements from " + sFolderName); };
            }

            // custom pairPacks
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "pairsPack")))
            {
                customCount = 0;
                LogInfo("Loading pairsPacks from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "pairsPack"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " pairsPack: " + f.Name);
                        CardPlayerPairsPackData medsCPP = ToData(JsonUtility.FromJson<CardPlayerPairsPackDataText>(File.ReadAllText(f.ToString())));
                        medsCardPlayerPairsPackDataSource[medsCPP.PackId] = UnityEngine.Object.Instantiate<CardPlayerPairsPackData>(medsCPP);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " pairsPack " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_CardPlayerPairsPackDataSource").SetValue(medsCardPlayerPairsPackDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " pairsPacks from " + sFolderName); };
            }

            // custom corruptionPacks
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "corruptionPack")))
            {
                customCount = 0;
                LogInfo("Loading corruptionPacks from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "corruptionPack"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " corruptionPack: " + f.Name);
                        CorruptionPackData medsCPD = ToData(JsonUtility.FromJson<CorruptionPackDataText>(File.ReadAllText(f.ToString())));
                        medsCorruptionPackDataSource[medsCPD.PackName] = UnityEngine.Object.Instantiate<CorruptionPackData>(medsCPD);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " corruptionPack " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_CorruptionPackDataSource").SetValue(medsCorruptionPackDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " corruptionPacks from " + sFolderName); };
            }

            // custom cardPlayerPacks
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "cardPlayerPack")))
            {
                customCount = 0;
                LogInfo("Loading cardPlayerPacks from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "cardPlayerPack"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " cardPlayerPack: " + f.Name);
                        CardPlayerPackData medsCPP = ToData(JsonUtility.FromJson<CardPlayerPackDataText>(File.ReadAllText(f.ToString())));
                        medsCardPlayerPackDataSource[medsCPP.PackId] = UnityEngine.Object.Instantiate<CardPlayerPackData>(medsCPP);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " cardPlayerPack " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_CardPlayerPackDataSource").SetValue(medsCardPlayerPackDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " cardPlayerPacks from " + sFolderName); };
            }

            // custom cinematics
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "cinematic")))
            {
                customCount = 0;
                LogInfo("Loading cinematic from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "cinematic"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " cinematic: " + f.Name);
                        CinematicData medsCinematic = ToData(JsonUtility.FromJson<CinematicDataText>(File.ReadAllText(f.ToString())));
                        medsCinematicDataSource[medsCinematic.CinematicId.ToLower()] = UnityEngine.Object.Instantiate<CinematicData>(medsCinematic);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " cinematic " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_Cinematics").SetValue(medsCinematicDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " cinematics from " + sFolderName); };
            }

            // custom combatData
            medsSecondRunCombatEvent = new();
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "combatData")))
            {
                customCount = 0;
                LogInfo("Loading combatData from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "combatData"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " combat: " + f.Name);
                        CombatData medsCombat = ToData(JsonUtility.FromJson<CombatDataText>(File.ReadAllText(f.ToString())));
                        medsCombatDataSource[medsCombat.CombatId] = UnityEngine.Object.Instantiate<CombatData>(medsCombat);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " combat " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_CombatDataSource").SetValue(medsCombatDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " combatData from " + sFolderName); };
            }

            // custom loot
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "loot")))
            {
                customCount = 0;
                LogInfo("Loading loot from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "loot"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " loot: " + f.Name);
                        LootData medsLoot = ToData(JsonUtility.FromJson<LootDataText>(File.ReadAllText(f.ToString())));
                        medsLootDataSource[medsLoot.Id] = UnityEngine.Object.Instantiate<LootData>(medsLoot);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " loot " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_LootDataSource").SetValue(medsLootDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " loot from " + sFolderName); };
            }

            // custom zones
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "zone")))
            {
                customCount = 0;
                LogInfo("Loading zones from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "zone"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " zone: " + f.Name);
                        ZoneData medsZone = ToData(JsonUtility.FromJson<ZoneDataText>(File.ReadAllText(f.ToString())));
                        medsZoneDataSource[medsZone.ZoneId.ToLower()] = UnityEngine.Object.Instantiate<ZoneData>(medsZone);
                        medsCustomZones[medsZone.ZoneId.ToLower()] = JsonUtility.FromJson<ZoneDataText>(File.ReadAllText(f.ToString()));
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " zone " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_ZoneDataSource").SetValue(medsZoneDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " zones from " + sFolderName); };
            }

            // custom nodes
            medsSecondRunImport = new();
            medsSecondRunImport2 = new();
            medsSecondRunNodesConnected = new();
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "node")))
            {
                customCount = 0;
                LogInfo("Loading nodes from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "node"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " node: " + f.Name);
                        NodeData medsNode = ToData(JsonUtility.FromJson<NodeDataText>(File.ReadAllText(f.ToString())));
                        string lower = medsNode.NodeId.ToLower();
                        medsNodeDataSource[lower] = UnityEngine.Object.Instantiate<NodeData>(medsNode);
                        medsNodeDataSource[lower].name = lower;
                        if (!medsNodesByZone.ContainsKey(medsNode.NodeZone.ZoneId.ToLower()))
                            medsNodesByZone[medsNode.NodeZone.ZoneId.ToLower()] = new List<NodeDataText>();
                        if (!medsNodesByZone[medsNode.NodeZone.ZoneId.ToLower()].Contains(JsonUtility.FromJson<NodeDataText>(File.ReadAllText(f.ToString()))))
                            medsNodesByZone[medsNode.NodeZone.ZoneId.ToLower()].Add(JsonUtility.FromJson<NodeDataText>(File.ReadAllText(f.ToString())));
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " node " + f.Name + ": " + e.Message); }
                }
                // late binding of nodesconnected
                LogDebug("Late binding of NodesConnected");
                foreach (KeyValuePair<string, string[]> kvp in medsSecondRunNodesConnected)
                {
                    try
                    {
                        medsNodeDataSource[kvp.Key].NodesConnected = new NodeData[kvp.Value.Length];
                        for (int a = 0; a < kvp.Value.Length; a++)
                            medsNodeDataSource[kvp.Key].NodesConnected[a] = GetNode(kvp.Value[a]);
                    }
                    catch (Exception e) { LogError("Error performing late binding of " + sFolderName + " NodesConnected: " + e.Message); }
                }
                LogDebug("Late binding of NodesConnectedRequirement");
                // late binding of nodesconnectedrequirement
                foreach (KeyValuePair<string, string[]> kvp in medsSecondRunImport)
                {
                    try
                    {
                        medsNodeDataSource[kvp.Key].NodesConnectedRequirement = new NodesConnectedRequirement[kvp.Value.Length];
                        for (int a = 0; a < kvp.Value.Length; a++)
                            medsNodeDataSource[kvp.Key].NodesConnectedRequirement[a] = ToData(JsonUtility.FromJson<NodesConnectedRequirementText>(kvp.Value[a]));
                    }
                    catch (Exception e) { LogError("Error performing late binding of " + sFolderName + " NodesConnectedRequirement: " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_NodeDataSource").SetValue(medsNodeDataSource);
                Traverse.Create(Globals.Instance).Field("_NodeCombatEventRelation").SetValue(medsNodeCombatEventRelation);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " nodes from " + sFolderName); };
            }

            // custom events
            medsSecondRunImport = new();
            //medsNodeEvent = new();
            //medsNodeEventPercent = new();
            //medsNodeEventPriority = new();
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "event")))
            {
                customCount = 0;
                LogInfo("Loading events from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "event"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " event: " + f.Name);
                        EventData medsEvent = ToData(JsonUtility.FromJson<EventDataText>(File.ReadAllText(f.ToString())));
                        medsEventDataSource[medsEvent.EventId] = UnityEngine.Object.Instantiate<EventData>(medsEvent);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " event " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_Events").SetValue(medsEventDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " events from " + sFolderName); };
            }

            // custom eventReplies
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "eventReply")))
            {
                customCount = 0;
                LogInfo("Loading eventReplies from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "eventReply"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " eventReply: " + f.Name);
                        string medsERDTString = File.ReadAllText(f.ToString());
                        EventReplyDataText medsERDT = JsonUtility.FromJson<EventReplyDataText>(medsERDTString);
                        if (medsSecondRunImport.ContainsKey(medsERDT.medsEvent.ToLower()))
                        {
                            string[] tempReplies = medsSecondRunImport[medsERDT.medsEvent.ToLower()];
                            Array.Resize(ref tempReplies, tempReplies.Length + 1);
                            tempReplies[tempReplies.Length - 1] = medsERDTString;
                            medsSecondRunImport[medsERDT.medsEvent.ToLower()] = tempReplies;
                        }
                        else
                        {
                            // #TODO: REWRITE THIS
                            // incorporate it into the reply-event below? so it just expands existing event array there
                            // rather than making a new one incorporating that one here, adding to it, then converting it BACK there?
                            // xdd
                            if (medsEventDataSource.ContainsKey(medsERDT.medsEvent.ToLower()))
                            {
                                medsSecondRunImport[medsERDT.medsEvent.ToLower()] = new string[medsEventDataSource[medsERDT.medsEvent.ToLower()].Replys.Length + 1];
                                for (int a = 0; a < medsEventDataSource[medsERDT.medsEvent.ToLower()].Replys.Length; a++)
                                    medsSecondRunImport[medsERDT.medsEvent.ToLower()][a] = JsonUtility.ToJson(ToText(medsEventDataSource[medsERDT.medsEvent.ToLower()].Replys[a]));
                                medsSecondRunImport[medsERDT.medsEvent.ToLower()][medsEventDataSource[medsERDT.medsEvent.ToLower()].Replys.Length] = medsERDTString;
                            }
                            else
                            {
                                medsSecondRunImport[medsERDT.medsEvent.ToLower()] = new string[1] { medsERDTString };
                            }
                        }
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " eventReplies " + f.Name + ": " + e.Message); }
                }
                if (customCount > 0) { LogInfo("Loaded " + customCount + " eventReplies from " + sFolderName); };
            }

            LogDebug("late reply-event bindings");
            // late reply-event bindings
            foreach (string eID in medsSecondRunImport.Keys)
            {
                try
                {
                    medsEventDataSource[eID].Replys = new EventReplyData[medsSecondRunImport[eID].Length];
                    for (int a = 0; a < medsSecondRunImport[eID].Length; a++)
                        medsEventDataSource[eID].Replys[a] = ToData(JsonUtility.FromJson<EventReplyDataText>(medsSecondRunImport[eID][a]), eID);
                    medsEventDataSource[eID].Init();
                }
                catch (Exception e) { LogError("Error performing " + sFolderName + " reply-event bindings: " + e.Message); }
                //LogDebug("reply-event binding: " + eID);
            }

            // late combat-event bindings
            foreach (string cID in medsSecondRunCombatEvent.Keys)
            {
                try
                {
                    medsCombatDataSource[cID].EventData = medsEventDataSource.ContainsKey(medsSecondRunCombatEvent[cID]) ? medsEventDataSource[medsSecondRunCombatEvent[cID]] : (EventData)null;
                }
                catch (Exception e) { LogError("Error performing " + sFolderName + " combat-event bindings: " + e.Message); }
            }
            Traverse.Create(Globals.Instance).Field("_NodeDataSource").SetValue(medsNodeDataSource);
            Traverse.Create(Globals.Instance).Field("_NodeCombatEventRelation").SetValue(medsNodeCombatEventRelation);
            Traverse.Create(Globals.Instance).Field("_CombatDataSource").SetValue(medsCombatDataSource);

            // custom challengeTraits
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "challengeTrait")))
            {
                customCount = 0;
                LogInfo("Loading challengeTraits from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "challengeTrait"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " challengeTrait: " + f.Name);
                        ChallengeTrait medsCT = ToData(JsonUtility.FromJson<ChallengeTraitText>(File.ReadAllText(f.ToString())));
                        medsChallengeTraitsSource[medsCT.Id.ToLower()] = UnityEngine.Object.Instantiate<ChallengeTrait>(medsCT);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " challengeTrait " + f.Name + ": " + e.Message); }
                }
                Traverse.Create(Globals.Instance).Field("_ChallengeTraitsSource").SetValue(medsChallengeTraitsSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " challengeTraits from " + sFolderName); };
            }

            // custom challengeData
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "challengeData")))
            {
                customCount = 0;
                LogInfo("Loading challengeData from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "challengeData"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " challengeData: " + f.Name);
                        ChallengeData medsCD = ToData(JsonUtility.FromJson<ChallengeDataText>(File.ReadAllText(f.ToString())));
                        medsChallengeDataSource[medsCD.Id.ToLower()] = UnityEngine.Object.Instantiate<ChallengeData>(medsCD);
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " challengeData " + f.Name + ": " + e.Message); }
                }
                // save vanilla+custom
                Traverse.Create(Globals.Instance).Field("_WeeklyDataSource").SetValue(medsChallengeDataSource);
                if (customCount > 0) { LogInfo("Loaded " + customCount + " challengeData from " + sFolderName); };
            }

            // custom roadsTXT
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "roadsTXT")))
            {
                customCount = 0;
                LogInfo("Loading roads from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "roadsTXT"))).GetFiles("*.txt");
                foreach (FileInfo f in medsFI)
                {
                    LogInfo("Loading " + sFolderName + " road: " + f.Name);
                    string[] fileText = File.ReadAllLines(f.ToString());
                    foreach (string roadText in fileText)
                    {
                        if (roadText.Length > 2 && roadText[..2] == @"\\")
                        {
                            // comment - ignore
                            continue;
                        }
                        string[] roadSplit = roadText.Replace(" ", "").Split("|");
                        if (roadSplit.Length != 2)
                        {
                            LogError("malformed road data in file " + f.Name + ": " + roadText);
                            continue;
                        }
                        if (!medsCustomRoads.ContainsKey(roadSplit[0].ToLower()))
                            medsCustomRoads[roadSplit[0].ToLower()] = new List<Vector3>();
                        foreach (string sVector in roadSplit[1].Split("),("))
                        {
                            if (sVector.Split(",").Length == 2)
                            {
                                string sVector2 = sVector.Replace("(", "").Replace(")", "");
                                try
                                {
                                    medsCustomRoads[roadSplit[0].ToLower()].Add(new Vector3(float.Parse(sVector2.Split(",")[0]), float.Parse(sVector2.Split(",")[1])));
                                    customCount++;
                                }
                                catch (Exception e) { LogError("cannot parse floats in " + sFolderName + " road " + f.Name + " " + sVector2 + ": " + e.Message); }
                            }
                        }
                    }
                }
                if (customCount > 0) { LogInfo("Loaded " + customCount + " roads from " + sFolderName); };
            }

            // custom prestige decks
            if (Directory.Exists(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "prestigeDeck")))
            {
                customCount = 0;
                LogInfo("Loading prestige decks from " + sFolderName);
                medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", sFolderName, "prestigeDeck"))).GetFiles("*.json");
                foreach (FileInfo f in medsFI)
                {
                    try
                    {
                        LogInfo("Loading " + sFolderName + " prestige deck: " + f.Name);
                        PrestigeDeck medsPD = JsonUtility.FromJson<PrestigeDeck>(File.ReadAllText(f.ToString()));
                        medsPrestigeDecks[medsPD.ID.ToLower()] = medsPD;
                        foreach (string cardID in medsPD.Cards)
                        {
                            medsIncludeInBaseSearch(medsPD.Name, cardID);
                            medsIncludeInBaseSearch(medsPD.ID, cardID);
                            if (medsCardsSource.ContainsKey(cardID))
                            {
                                CardData card = medsCardsSource[cardID];
                                if (card != null)
                                {
                                    if (!card.UpgradesTo1.IsNullOrWhiteSpace())
                                    {
                                        medsIncludeInBaseSearch(medsPD.Name, card.UpgradesTo1);
                                        medsIncludeInBaseSearch(medsPD.ID, card.UpgradesTo1);
                                    }
                                    if (!card.UpgradesTo2.IsNullOrWhiteSpace())
                                    {
                                        medsIncludeInBaseSearch(medsPD.Name, card.UpgradesTo2);
                                        medsIncludeInBaseSearch(medsPD.ID, card.UpgradesTo2);
                                    }
                                    if ((UnityEngine.Object)card.UpgradesToRare != (UnityEngine.Object)null)
                                    {
                                        medsIncludeInBaseSearch(medsPD.Name, card.UpgradesToRare.Id);
                                        medsIncludeInBaseSearch(medsPD.ID, card.UpgradesToRare.Id);
                                    }
                                }
                            }
                        }
                        customCount++;
                    }
                    catch (Exception e) { LogError("Error loading custom " + sFolderName + " prestige deck " + f.Name + ": " + e.Message); }
                }
                if (customCount > 0) { LogInfo("Loaded " + customCount + " prestige decks from " + sFolderName); };
            }
            LogInfo("Finished loading " + sFolderName);
            return true;
        }
    }
}
