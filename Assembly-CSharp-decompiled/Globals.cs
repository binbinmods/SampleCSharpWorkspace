// Decompiled with JetBrains decompiler
// Type: Globals
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Globals : MonoBehaviour
{
  private DateTime initialWeeklyDate = new DateTime(2022, 8, 18, 16, 0, 0);
  public DateTime finishHalloweenEvent = new DateTime(2022, 11, 3, 19, 0, 0);
  private bool showDebug;
  private string currentLang = "en";
  private string defaultNickName = "AtODefaultPlayer";
  private bool saveAurasText;
  private string auraText = "";
  private bool saveClassText;
  private string classText = "";
  private bool saveMonsterText;
  private string monsterText = "";
  private bool saveKeynotesText;
  private string keynotesText = "";
  private bool saveEventsText;
  private string eventText = "";
  private Dictionary<string, string> savedEventLines = new Dictionary<string, string>();
  private bool saveRequirementsText;
  private string requirementsText = "";
  private bool saveTraitsText;
  private string traitsText = "";
  private bool saveNodesText;
  private string nodesText = "";
  private bool saveCardsText;
  private string cardsText = "";
  private string cardsFluffText = "";
  private int normalFPS = 120;
  private int combatFPS = 120;
  private List<CardData> _InstantiatedCardData;
  private Dictionary<string, GameObject> _ResourceEffectsGO;
  private Dictionary<string, string> _IncompatibleVersion;
  private Dictionary<float, UnityEngine.WaitForSeconds> _WaitForSecondsDict;
  [SerializeField]
  private Dictionary<string, CardData> _Cards;
  [SerializeField]
  private Dictionary<string, CardData> _CardsSource;
  [SerializeField]
  private Dictionary<string, HeroData> _Heroes;
  [SerializeField]
  private Dictionary<string, NPCData> _NPCs;
  [SerializeField]
  private Dictionary<string, NPCData> _NPCsNamed;
  [SerializeField]
  private Dictionary<string, NPCData> _NPCsSource;
  [SerializeField]
  private Dictionary<string, AuraCurseData> _AurasCurses;
  [SerializeField]
  private Dictionary<string, AuraCurseData> _AurasCursesSource;
  [SerializeField]
  private List<string> _AurasCursesIndex;
  [SerializeField]
  private Dictionary<string, SubClassData> _SubClass;
  [SerializeField]
  private Dictionary<string, SubClassData> _SubClassSource;
  [SerializeField]
  private Dictionary<string, TraitData> _Traits;
  [SerializeField]
  private Dictionary<string, TraitData> _TraitsSource;
  [SerializeField]
  private Dictionary<string, PerkData> _PerksSource;
  [SerializeField]
  private Dictionary<string, PerkNodeData> _PerksNodesSource;
  [SerializeField]
  private Dictionary<string, EventData> _Events;
  [SerializeField]
  private Dictionary<string, CinematicData> _Cinematics;
  [SerializeField]
  private Dictionary<string, EventRequirementData> _Requirements;
  [SerializeField]
  private SortedDictionary<string, KeyNotesData> _KeyNotes;
  [SerializeField]
  private Dictionary<string, CombatData> _CombatDataSource;
  [SerializeField]
  private Dictionary<string, NodeData> _NodeDataSource;
  private Dictionary<string, string> _NodeCombatEventRelation;
  [SerializeField]
  private Dictionary<int, TierRewardData> _TierRewardDataSource;
  [SerializeField]
  private Dictionary<string, ItemData> _ItemDataSource;
  [SerializeField]
  private Dictionary<string, LootData> _LootDataSource;
  [SerializeField]
  private Dictionary<string, PackData> _PackDataSource;
  [SerializeField]
  private Dictionary<string, SkinData> _SkinDataSource;
  [SerializeField]
  private Dictionary<string, CardbackData> _CardbackDataSource;
  [SerializeField]
  private Dictionary<string, CardPlayerPackData> _CardPlayerPackDataSource;
  [SerializeField]
  private Dictionary<string, CardPlayerPairsPackData> _CardPlayerPairsPackDataSource;
  [SerializeField]
  private Dictionary<string, CorruptionPackData> _CorruptionPackDataSource;
  [SerializeField]
  private Dictionary<string, ZoneData> _ZoneDataSource;
  [SerializeField]
  private Dictionary<string, ChallengeTrait> _ChallengeTraitsSource;
  [SerializeField]
  private Dictionary<string, ChallengeData> _WeeklyDataSource;
  [SerializeField]
  private Dictionary<string, int> _UpgradeCost;
  [SerializeField]
  private Dictionary<string, int> _CraftCost;
  [SerializeField]
  private Dictionary<string, int> _DivinationCost;
  [SerializeField]
  private Dictionary<int, int> _DivinationTier;
  [SerializeField]
  private Dictionary<string, int> _ItemCost;
  [SerializeField]
  private Dictionary<string, int> _CardEnergyCost;
  public const string tutorialSeed = "cban29t";
  public const int townTutorialStepCraft = 0;
  public const string tutorialCraftItem = "fireball";
  public const string tutorialCraftCharacterName = "Evelyn";
  public const int townTutorialStepUpgrade = 1;
  public const string tutorialUpgradeItem = "faststrike";
  public const string tutorialUpgradeCharacterName = "Magnus";
  public const int townTutorialStepItem = 2;
  public const string tutorialLootItem = "spyglass";
  public const string tutorialLootCharacterName = "Andrin";
  public const int npcHandicapDamageBonus = 25;
  public const int npcHandicapResistanceBonus = 25;
  public const float sourceWidth = 1920f;
  public const float sourceHeight = 1080f;
  public float sizeW;
  public float sizeH;
  public float multiplierX;
  public float multiplierY;
  public Vector3 scaleV = new Vector3(1f, 1f, 1f);
  public float scale = 1f;
  public const float npcActionDelay = 0.8f;
  public const int teamMaxChars = 4;
  public const int initialCopyCards = 4;
  public const int handCards = 5;
  public const int maxHandCards = 10;
  public const int minDeckCards = 15;
  public const float cardSmallScale = 0.75f;
  public const int maxHeroEnergy = 10;
  public const int initGold = 300;
  public const int initDust = 300;
  public const int initGoldMP = 75;
  public const int initDustMP = 75;
  public const int dmgPercentLowest = -50;
  public const int resistanceLimit = 95;
  public const int globalResurrectPercent = 70;
  public const float InjuryGenerationCardDelay = 4f;
  public const float InitiativePortraitWidth = 0.48f;
  public const float InitiativePortraitDistance = 0.24f;
  public Color MapArrow = new Color(0.0f, 0.97f, 1f, 0.6f);
  public Color MapArrowHighlight = new Color(0.0f, 0.0f, 1f, 0.6f);
  public Color MapArrowTemp = new Color(0.0f, 0.0f, 0.0f, 0.6f);
  public Color MapArrowTempChallenge = new Color(1f, 0.46f, 0.82f, 0.6f);
  public const float effectCastFixedY = 1.4f;
  private Dictionary<string, string> _SkillColor;
  private Dictionary<string, string> _ClassColor;
  private Dictionary<string, Color> _ColorColor;
  private Dictionary<string, Color> _RarityColor;
  private int[] _CorruptionBasePercent;
  public const int CorruptionIncrementPercent = 10;
  public const int CorruptionDustCost = 400;
  public const int CorruptionSpeedCost = 2;
  public const int CorruptionLifeCost = 20;
  public const int CorruptionResistCost = 10;
  public const int ScarabChance = 10;
  public const int PetCommonCost = 72;
  public const int PetUncommonCost = 156;
  public const int PetRareCost = 348;
  public const int PetEpicCost = 744;
  public const int PetMythicCost = 1200;
  private Dictionary<string, string> _CardsDescriptionNormalized;
  private Dictionary<Enums.CardType, List<string>> _CardListByType;
  private Dictionary<Enums.CardClass, List<string>> _CardListByClass;
  private Dictionary<Enums.CardClass, List<string>> _CardListNotUpgradedByClass;
  private Dictionary<Enums.CardType, List<string>> _CardItemByType;
  private Dictionary<string, List<string>> _CardsListSearch;
  private Dictionary<string, bool> _CardsListSearched;
  private List<string> _CardListNotUpgraded;
  private Dictionary<string, List<string>> _CardListByClassType;
  private Dictionary<int, int> _ExperienceByCardCost;
  private Dictionary<int, int> _ExperienceByLevel;
  private List<int> _PerkLevel;
  private List<int> _RankLevel;
  private List<string> _SkuAvailable;
  private const int maxPerkPoints = 50;

  public static Globals Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) Globals.Instance == (UnityEngine.Object) null)
      Globals.Instance = this;
    else if ((UnityEngine.Object) Globals.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this._WaitForSecondsDict = new Dictionary<float, UnityEngine.WaitForSeconds>();
    this._InstantiatedCardData = new List<CardData>();
    this._ResourceEffectsGO = new Dictionary<string, GameObject>();
    this._IncompatibleVersion = new Dictionary<string, string>();
    this._IncompatibleVersion.Add("0.6.83", "0.6.82");
    this._IncompatibleVersion.Add("0.7.9", "0.7.52");
    this._IncompatibleVersion.Add("0.8.1", "0.8.01");
    this._IncompatibleVersion.Add("0.8.95", "0.8.7");
    this._IncompatibleVersion.Add("1.0.0", "0.9.99");
    this._IncompatibleVersion.Add("1.1.20", "1.1.10");
    this._CorruptionBasePercent = new int[3];
    this._CorruptionBasePercent[0] = 70;
    this._CorruptionBasePercent[1] = 60;
    this._CorruptionBasePercent[2] = 50;
    this._CardsDescriptionNormalized = new Dictionary<string, string>();
    this._SkillColor = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._SkillColor.Add("Damage", "red");
    this._SkillColor.Add("Heal", "green");
    this._SkillColor.Add("Stun", "yellow");
    this._SkillColor.Add("DamageTurn", "red");
    this._SkillColor.Add("HealTurn", "green");
    this._ClassColor = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._ClassColor.Add("warrior", "#F3404E");
    this._ClassColor.Add("mage", "#3298FF");
    this._ClassColor.Add("healer", "#BBBBBB");
    this._ClassColor.Add("scout", "#34FF46");
    this._ClassColor.Add("magicknight", "#D07FFF");
    this._ClassColor.Add("monster", "#888888");
    this._ClassColor.Add("injury", "#B35248");
    this._ClassColor.Add("boon", "#12FFEF");
    this._ClassColor.Add("item", "#8A4400");
    this._ClassColor.Add("special", "#cea067");
    this._ClassColor.Add("pet", "#DB641E");
    this._ClassColor.Add("enchantment", "#cea067");
    this._RarityColor = new Dictionary<string, Color>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._RarityColor.Add("uncommon", new Color(0.0f, 0.7f, 0.0f, 1f));
    this._RarityColor.Add("rare", new Color(0.0f, 0.6f, 1f, 1f));
    this._RarityColor.Add("mythic", new Color(1f, 0.7f, 0.0f, 1f));
    this._RarityColor.Add("epic", new Color(0.8f, 0.0f, 1f, 1f));
    this._ColorColor = new Dictionary<string, Color>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._ColorColor.Add("greenCard", new Color(0.0f, 0.79f, 0.06f, 1f));
    this._ColorColor.Add("redCard", new Color(1f, 0.0f, 0.31f, 1f));
    this._ColorColor.Add("white", new Color(1f, 1f, 1f, 1f));
    this._ColorColor.Add("blueCardTitle", new Color(0.4f, 0.73f, 0.92f, 1f));
    this._ColorColor.Add("goldCardTitle", new Color(1f, 0.7f, 0.06f, 1f));
    this._ColorColor.Add("purple", new Color(0.85f, 0.45f, 1f, 1f));
    this._ColorColor.Add("grey", new Color(0.6f, 0.6f, 0.6f, 1f));
    this._ExperienceByLevel = new Dictionary<int, int>();
    this._ExperienceByLevel.Add(1, 160);
    this._ExperienceByLevel.Add(2, 480);
    this._ExperienceByLevel.Add(3, 900);
    this._ExperienceByLevel.Add(4, 1500);
    this._ExperienceByCardCost = new Dictionary<int, int>();
    this._ExperienceByCardCost.Add(0, 1);
    this._ExperienceByCardCost.Add(1, 1);
    this._ExperienceByCardCost.Add(2, 2);
    this._ExperienceByCardCost.Add(3, 4);
    this._ExperienceByCardCost.Add(4, 6);
    this._ExperienceByCardCost.Add(5, 9);
    this._ExperienceByCardCost.Add(6, 12);
    this._ExperienceByCardCost.Add(7, 16);
    this._ExperienceByCardCost.Add(8, 20);
    this._ExperienceByCardCost.Add(9, 26);
    this._ExperienceByCardCost.Add(10, 32);
    this._UpgradeCost = new Dictionary<string, int>();
    this._UpgradeCost.Add("Upgrade_Common", 60);
    this._UpgradeCost.Add("Upgrade_Uncommon", 180);
    this._UpgradeCost.Add("Upgrade_Rare", 420);
    this._UpgradeCost.Add("Upgrade_Epic", 1260);
    this._UpgradeCost.Add("Upgrade_Mythic", 1940);
    this._UpgradeCost.Add("Transform_Common", 60);
    this._UpgradeCost.Add("Transform_Uncommon", 180);
    this._UpgradeCost.Add("Transform_Rare", 420);
    this._UpgradeCost.Add("Transform_Epic", 1260);
    this._UpgradeCost.Add("Transform_Mythic", 1940);
    this._CraftCost = new Dictionary<string, int>();
    this._CraftCost.Add("Common", 60);
    this._CraftCost.Add("Uncommon", 180);
    this._CraftCost.Add("Rare", 420);
    this._CraftCost.Add("Epic", 1260);
    this._CraftCost.Add("Mythic", 1940);
    this._DivinationCost = new Dictionary<string, int>();
    this._DivinationCost.Add("0", 400);
    this._DivinationCost.Add("1", 800);
    this._DivinationCost.Add("2", 1600);
    this._DivinationCost.Add("3", 3200);
    this._DivinationCost.Add("4", 5000);
    this._DivinationTier = new Dictionary<int, int>();
    this._DivinationTier.Add(0, 2);
    this._DivinationTier.Add(1, 5);
    this._DivinationTier.Add(2, 6);
    this._DivinationTier.Add(3, 8);
    this._DivinationTier.Add(4, 10);
    this._ItemCost = new Dictionary<string, int>();
    this._ItemCost.Add("Weapon_Common", 114);
    this._ItemCost.Add("Weapon_Uncommon", 247);
    this._ItemCost.Add("Weapon_Rare", 551);
    this._ItemCost.Add("Weapon_Epic", 1178);
    this._ItemCost.Add("Weapon_Mythic", 2375);
    this._ItemCost.Add("Armor_Common", 120);
    this._ItemCost.Add("Armor_Uncommon", 260);
    this._ItemCost.Add("Armor_Rare", 580);
    this._ItemCost.Add("Armor_Epic", 1240);
    this._ItemCost.Add("Armor_Mythic", 2500);
    this._ItemCost.Add("Jewelry_Common", 132);
    this._ItemCost.Add("Jewelry_Uncommon", 286);
    this._ItemCost.Add("Jewelry_Rare", 638);
    this._ItemCost.Add("Jewelry_Epic", 1364);
    this._ItemCost.Add("Jewelry_Mythic", 2750);
    this._ItemCost.Add("Accesory_Common", 144);
    this._ItemCost.Add("Accesory_Uncommon", 312);
    this._ItemCost.Add("Accesory_Rare", 696);
    this._ItemCost.Add("Accesory_Epic", 1488);
    this._ItemCost.Add("Accesory_Mythic", 3000);
    this._PerkLevel = new List<int>();
    this._PerkLevel.Add(300);
    this._PerkLevel.Add(600);
    this._PerkLevel.Add(1000);
    this._PerkLevel.Add(1500);
    this._PerkLevel.Add(2100);
    this._PerkLevel.Add(2800);
    this._PerkLevel.Add(3600);
    this._PerkLevel.Add(4500);
    this._PerkLevel.Add(5500);
    this._PerkLevel.Add(6500);
    this._PerkLevel.Add(7500);
    this._PerkLevel.Add(8700);
    this._PerkLevel.Add(9900);
    this._PerkLevel.Add(11100);
    this._PerkLevel.Add(12500);
    this._PerkLevel.Add(14000);
    this._PerkLevel.Add(15500);
    this._PerkLevel.Add(17000);
    this._PerkLevel.Add(18500);
    this._PerkLevel.Add(20000);
    this._PerkLevel.Add(21500);
    this._PerkLevel.Add(23000);
    this._PerkLevel.Add(24500);
    this._PerkLevel.Add(26000);
    this._PerkLevel.Add(27500);
    this._PerkLevel.Add(29000);
    this._PerkLevel.Add(30700);
    this._PerkLevel.Add(32500);
    this._PerkLevel.Add(34500);
    this._PerkLevel.Add(36500);
    this._PerkLevel.Add(39000);
    this._PerkLevel.Add(41500);
    this._PerkLevel.Add(44000);
    this._PerkLevel.Add(46500);
    this._PerkLevel.Add(49000);
    this._PerkLevel.Add(51500);
    this._PerkLevel.Add(54000);
    this._PerkLevel.Add(56500);
    this._PerkLevel.Add(58000);
    this._PerkLevel.Add(61000);
    this._PerkLevel.Add(64000);
    this._PerkLevel.Add(67000);
    this._PerkLevel.Add(70500);
    this._PerkLevel.Add(74000);
    this._PerkLevel.Add(77500);
    this._PerkLevel.Add(81000);
    this._PerkLevel.Add(84500);
    this._PerkLevel.Add(88000);
    this._PerkLevel.Add(91500);
    this._PerkLevel.Add(95500);
    this._RankLevel = new List<int>();
    this._RankLevel.Add(4);
    this._RankLevel.Add(8);
    this._RankLevel.Add(12);
    this._RankLevel.Add(16);
    this._RankLevel.Add(20);
    this._RankLevel.Add(24);
    this._RankLevel.Add(28);
    this._RankLevel.Add(32);
    this._RankLevel.Add(36);
    this._RankLevel.Add(40);
    this._RankLevel.Add(44);
    this._SkuAvailable = new List<string>();
    this._SkuAvailable.Add("2168960");
    this._SkuAvailable.Add("2325780");
    this._SkuAvailable.Add("2511580");
  }

  public void CreateGameContent()
  {
    this._CardItemByType = new Dictionary<Enums.CardType, List<string>>();
    this._CardEnergyCost = new Dictionary<string, int>();
    KeyNotesData[] keyNotesDataArray = Resources.LoadAll<KeyNotesData>("KeyNotes");
    this.KeyNotes = new SortedDictionary<string, KeyNotesData>();
    for (int index = 0; index < keyNotesDataArray.Length; ++index)
    {
      string lower = keyNotesDataArray[index].KeynoteName.Replace(" ", "").ToLower();
      keyNotesDataArray[index].Id = lower;
      this.KeyNotes.Add(lower, UnityEngine.Object.Instantiate<KeyNotesData>(keyNotesDataArray[index]));
      if (this.saveKeynotesText)
      {
        this.keynotesText = this.keynotesText + lower + "_description=" + Functions.NormalizeTextForArchive(keyNotesDataArray[index].Description) + "\n";
        this.keynotesText = this.keynotesText + lower + "_descriptionExtended=" + Functions.NormalizeTextForArchive(keyNotesDataArray[index].DescriptionExtended) + "\n";
      }
      this.KeyNotes[lower].KeynoteName = Texts.Instance.GetText(this.KeyNotes[lower].KeynoteName);
      string text1 = Texts.Instance.GetText(lower + "_description", "keynotes");
      if (text1 != "")
        this.KeyNotes[lower].Description = text1;
      string text2 = Texts.Instance.GetText(lower + "_descriptionExtended", "keynotes");
      if (text2 != "")
        this.KeyNotes[lower].DescriptionExtended = text2;
    }
    CardData[] cardDataArray = Resources.LoadAll<CardData>("Cards");
    this._CardsSource = new Dictionary<string, CardData>();
    for (int index = 0; index < cardDataArray.Length; ++index)
    {
      cardDataArray[index].Id = cardDataArray[index].name.ToLower();
      this._CardsSource.Add(cardDataArray[index].Id, UnityEngine.Object.Instantiate<CardData>(cardDataArray[index]));
      if (this.saveCardsText)
      {
        if (cardDataArray[index].CardUpgraded == Enums.CardUpgraded.No && cardDataArray[index].CardClass != Enums.CardClass.MagicKnight)
          this.cardsText = this.cardsText + "c_" + cardDataArray[index].Id + "_name=" + Functions.NormalizeTextForArchive(cardDataArray[index].CardName) + "\n";
        if (cardDataArray[index].Fluff != "")
          this.cardsFluffText = this.cardsFluffText + "c_" + cardDataArray[index].Id + "_fluff=" + Functions.NormalizeTextForArchive(cardDataArray[index].Fluff) + "\n";
      }
    }
    HeroData[] heroDataArray = Resources.LoadAll<HeroData>("Heroes");
    this._Heroes = new Dictionary<string, HeroData>();
    for (int index = 0; index < heroDataArray.Length; ++index)
      this._Heroes.Add(heroDataArray[index].Id, UnityEngine.Object.Instantiate<HeroData>(heroDataArray[index]));
    SubClassData[] subClassDataArray = Resources.LoadAll<SubClassData>("SubClass");
    this._SubClassSource = new Dictionary<string, SubClassData>();
    for (int index = 0; index < subClassDataArray.Length; ++index)
      this._SubClassSource.Add(subClassDataArray[index].SubClassName.Replace(" ", "").ToLower(), UnityEngine.Object.Instantiate<SubClassData>(subClassDataArray[index]));
    NPCData[] npcDataArray = Resources.LoadAll<NPCData>("NPCs");
    this._NPCsSource = new Dictionary<string, NPCData>();
    for (int index = 0; index < npcDataArray.Length; ++index)
      this._NPCsSource.Add(npcDataArray[index].Id, UnityEngine.Object.Instantiate<NPCData>(npcDataArray[index]));
    this.CreateCharClones();
    this.CreateAuraCurses();
    this.CreateCardClones();
    this._TraitsSource = new Dictionary<string, TraitData>();
    TraitData[] traitDataArray = Resources.LoadAll<TraitData>("Traits");
    for (int index = 0; index < traitDataArray.Length; ++index)
    {
      traitDataArray[index].Init();
      this._TraitsSource.Add(traitDataArray[index].Id, UnityEngine.Object.Instantiate<TraitData>(traitDataArray[index]));
      if (this.saveTraitsText && (UnityEngine.Object) traitDataArray[index].TraitCard == (UnityEngine.Object) null)
      {
        this.traitsText = this.traitsText + traitDataArray[index].Id + "=" + Functions.NormalizeTextForArchive(traitDataArray[index].TraitName) + "\n";
        this.traitsText = this.traitsText + traitDataArray[index].Id + "_description=" + Functions.NormalizeTextForArchive(traitDataArray[index].Description) + "\n";
      }
    }
    this.CreateTraitClones();
    this._PerksSource = new Dictionary<string, PerkData>();
    PerkData[] perkDataArray = Resources.LoadAll<PerkData>("Perks");
    for (int index = 0; index < perkDataArray.Length; ++index)
    {
      perkDataArray[index].Init();
      this._PerksSource.Add(perkDataArray[index].Id, UnityEngine.Object.Instantiate<PerkData>(perkDataArray[index]));
    }
    this._PerksNodesSource = new Dictionary<string, PerkNodeData>();
    PerkNodeData[] perkNodeDataArray = Resources.LoadAll<PerkNodeData>("PerkNode");
    for (int index = 0; index < perkNodeDataArray.Length; ++index)
      this._PerksNodesSource.Add(perkNodeDataArray[index].Id.ToLower(), UnityEngine.Object.Instantiate<PerkNodeData>(perkNodeDataArray[index]));
    EventData[] eventDataArray = Resources.LoadAll<EventData>("World/Events");
    this._Events = new Dictionary<string, EventData>();
    for (int index1 = 0; index1 < eventDataArray.Length; ++index1)
    {
      EventData eventData = UnityEngine.Object.Instantiate<EventData>(eventDataArray[index1]);
      if (this.saveEventsText)
      {
        string lower = eventData.EventId.ToLower();
        this.eventText = this.eventText + lower + "_nm=" + this.EventLineForSave(eventData.EventName, lower + "_nm") + "\n";
        this.eventText = this.eventText + lower + "_dsc=" + this.EventLineForSave(eventData.Description, lower + "_dsc") + "\n";
        this.eventText = this.eventText + lower + "_dsca=" + this.EventLineForSave(eventData.DescriptionAction, lower + "_dsca") + "\n";
        for (int index2 = 0; index2 < eventData.Replys.Length; ++index2)
        {
          this.eventText = this.eventText + lower + "_rp" + index2.ToString() + "=" + this.EventLineForSave(eventData.Replys[index2].ReplyText, lower + "_rp" + index2.ToString()) + "\n";
          this.eventText = this.eventText + lower + "_rp" + index2.ToString() + "_s=" + this.EventLineForSave(eventData.Replys[index2].SsRewardText, lower + "_rp" + index2.ToString() + "_s") + "\n";
          if (eventData.Replys[index2].SscRewardText != "")
            this.eventText = this.eventText + lower + "_rp" + index2.ToString() + "_sc=" + this.EventLineForSave(eventData.Replys[index2].SscRewardText, lower + "_rp" + index2.ToString() + "_sc") + "\n";
          if (eventData.Replys[index2].FlRewardText != "")
            this.eventText = this.eventText + lower + "_rp" + index2.ToString() + "_f=" + this.EventLineForSave(eventData.Replys[index2].FlRewardText, lower + "_rp" + index2.ToString() + "_f") + "\n";
          if (eventData.Replys[index2].FlcRewardText != "")
            this.eventText = this.eventText + lower + "_rp" + index2.ToString() + "_fc=" + this.EventLineForSave(eventData.Replys[index2].FlcRewardText, lower + "_rp" + index2.ToString() + "_fc") + "\n";
        }
      }
      eventData.Init();
      this._Events.Add(eventData.EventId.ToLower(), eventData);
    }
    EventRequirementData[] eventRequirementDataArray = Resources.LoadAll<EventRequirementData>("World/Events/Requirements");
    this._Requirements = new Dictionary<string, EventRequirementData>();
    for (int index = 0; index < eventRequirementDataArray.Length; ++index)
    {
      string lower = eventRequirementDataArray[index].RequirementId.ToLower();
      this._Requirements.Add(lower, UnityEngine.Object.Instantiate<EventRequirementData>(eventRequirementDataArray[index]));
      if (this._Requirements[lower].ItemTrack || this._Requirements[lower].RequirementTrack)
      {
        if (this.saveRequirementsText)
        {
          this.requirementsText = this.requirementsText + lower + "_name=" + this._Requirements[lower].RequirementName + "\n";
          this.requirementsText = this.requirementsText + lower + "_description=" + this._Requirements[lower].Description + "\n";
        }
        string text3 = Texts.Instance.GetText(lower + "_name", "requirements");
        if (text3 != "")
          this._Requirements[lower].RequirementName = text3;
        string text4 = Texts.Instance.GetText(lower + "_description", "requirements");
        if (text4 != "")
          this._Requirements[lower].Description = text4;
      }
    }
    CombatData[] combatDataArray = Resources.LoadAll<CombatData>("World/Combats");
    this._CombatDataSource = new Dictionary<string, CombatData>();
    for (int index = 0; index < combatDataArray.Length; ++index)
      this._CombatDataSource.Add(combatDataArray[index].CombatId.Replace(" ", "").ToLower(), UnityEngine.Object.Instantiate<CombatData>(combatDataArray[index]));
    NodeData[] nodeDataArray = Resources.LoadAll<NodeData>("World/MapNodes");
    this._NodeDataSource = new Dictionary<string, NodeData>();
    this._NodeCombatEventRelation = new Dictionary<string, string>();
    for (int index3 = 0; index3 < nodeDataArray.Length; ++index3)
    {
      string lower = nodeDataArray[index3].NodeId.ToLower();
      this._NodeDataSource.Add(lower, UnityEngine.Object.Instantiate<NodeData>(nodeDataArray[index3]));
      if (this.saveNodesText)
      {
        if (!(nodeDataArray[index3].NodeName == "Obelisk Challenge"))
          this.nodesText = this.nodesText + nodeDataArray[index3].NodeId + "_name=" + Functions.NormalizeTextForArchive(nodeDataArray[index3].NodeName) + "\n";
        else
          continue;
      }
      this._NodeDataSource[lower].NodeName = Texts.Instance.GetText(this._NodeDataSource[lower].NodeId + "_name", "nodes");
      if (!this._NodeCombatEventRelation.ContainsKey(lower))
        this._NodeCombatEventRelation.Add(lower, lower);
      for (int index4 = 0; index4 < nodeDataArray[index3].NodeCombat.Length; ++index4)
      {
        if ((UnityEngine.Object) nodeDataArray[index3].NodeCombat[index4] != (UnityEngine.Object) null && !this._NodeCombatEventRelation.ContainsKey(nodeDataArray[index3].NodeCombat[index4].CombatId))
          this._NodeCombatEventRelation.Add(nodeDataArray[index3].NodeCombat[index4].CombatId, lower);
      }
      for (int index5 = 0; index5 < nodeDataArray[index3].NodeEvent.Length; ++index5)
      {
        if ((UnityEngine.Object) nodeDataArray[index3].NodeEvent[index5] != (UnityEngine.Object) null && !this._NodeCombatEventRelation.ContainsKey(nodeDataArray[index3].NodeEvent[index5].EventId))
          this._NodeCombatEventRelation.Add(nodeDataArray[index3].NodeEvent[index5].EventId, lower);
      }
    }
    TierRewardData[] tierRewardDataArray = Resources.LoadAll<TierRewardData>("Rewards");
    this._TierRewardDataSource = new Dictionary<int, TierRewardData>();
    for (int index = 0; index < tierRewardDataArray.Length; ++index)
      this._TierRewardDataSource.Add(tierRewardDataArray[index].TierNum, UnityEngine.Object.Instantiate<TierRewardData>(tierRewardDataArray[index]));
    ItemData[] itemDataArray = Resources.LoadAll<ItemData>("Items");
    this._ItemDataSource = new Dictionary<string, ItemData>();
    for (int index = 0; index < itemDataArray.Length; ++index)
    {
      itemDataArray[index].Id = itemDataArray[index].name.ToLower();
      this._ItemDataSource.Add(itemDataArray[index].Id, UnityEngine.Object.Instantiate<ItemData>(itemDataArray[index]));
    }
    LootData[] lootDataArray = Resources.LoadAll<LootData>("Loot");
    this._LootDataSource = new Dictionary<string, LootData>();
    for (int index = 0; index < lootDataArray.Length; ++index)
      this._LootDataSource.Add(lootDataArray[index].Id.ToLower(), UnityEngine.Object.Instantiate<LootData>(lootDataArray[index]));
    PackData[] packDataArray = Resources.LoadAll<PackData>("Packs");
    this._PackDataSource = new Dictionary<string, PackData>();
    for (int index = 0; index < packDataArray.Length; ++index)
      this._PackDataSource.Add(packDataArray[index].PackId.ToLower(), UnityEngine.Object.Instantiate<PackData>(packDataArray[index]));
    SkinData[] skinDataArray = Resources.LoadAll<SkinData>("Skins");
    this._SkinDataSource = new Dictionary<string, SkinData>();
    for (int index = 0; index < skinDataArray.Length; ++index)
      this._SkinDataSource.Add(skinDataArray[index].SkinId.ToLower(), UnityEngine.Object.Instantiate<SkinData>(skinDataArray[index]));
    CardbackData[] cardbackDataArray = Resources.LoadAll<CardbackData>("Cardbacks");
    this._CardbackDataSource = new Dictionary<string, CardbackData>();
    for (int index = 0; index < cardbackDataArray.Length; ++index)
      this._CardbackDataSource.Add(cardbackDataArray[index].CardbackId.ToLower(), UnityEngine.Object.Instantiate<CardbackData>(cardbackDataArray[index]));
    CorruptionPackData[] corruptionPackDataArray = Resources.LoadAll<CorruptionPackData>("CorruptionRewards");
    this._CorruptionPackDataSource = new Dictionary<string, CorruptionPackData>();
    for (int index = 0; index < corruptionPackDataArray.Length; ++index)
      this._CorruptionPackDataSource.Add(corruptionPackDataArray[index].name, UnityEngine.Object.Instantiate<CorruptionPackData>(corruptionPackDataArray[index]));
    CardPlayerPackData[] cardPlayerPackDataArray = Resources.LoadAll<CardPlayerPackData>("CardPlayer");
    this._CardPlayerPackDataSource = new Dictionary<string, CardPlayerPackData>();
    for (int index = 0; index < cardPlayerPackDataArray.Length; ++index)
      this._CardPlayerPackDataSource.Add(cardPlayerPackDataArray[index].PackId.ToLower(), UnityEngine.Object.Instantiate<CardPlayerPackData>(cardPlayerPackDataArray[index]));
    CardPlayerPairsPackData[] playerPairsPackDataArray = Resources.LoadAll<CardPlayerPairsPackData>("CardPlayerPairs");
    this._CardPlayerPairsPackDataSource = new Dictionary<string, CardPlayerPairsPackData>();
    for (int index = 0; index < playerPairsPackDataArray.Length; ++index)
      this._CardPlayerPairsPackDataSource.Add(playerPairsPackDataArray[index].PackId.ToLower(), UnityEngine.Object.Instantiate<CardPlayerPairsPackData>(playerPairsPackDataArray[index]));
    CinematicData[] cinematicDataArray = Resources.LoadAll<CinematicData>("Cinematics");
    this._Cinematics = new Dictionary<string, CinematicData>();
    for (int index = 0; index < cinematicDataArray.Length; ++index)
      this._Cinematics.Add(cinematicDataArray[index].CinematicId.Replace(" ", "").ToLower(), UnityEngine.Object.Instantiate<CinematicData>(cinematicDataArray[index]));
    ZoneData[] zoneDataArray = Resources.LoadAll<ZoneData>("World/Zones");
    this._ZoneDataSource = new Dictionary<string, ZoneData>();
    for (int index = 0; index < zoneDataArray.Length; ++index)
      this._ZoneDataSource.Add(zoneDataArray[index].ZoneId.ToLower(), UnityEngine.Object.Instantiate<ZoneData>(zoneDataArray[index]));
    ChallengeTrait[] challengeTraitArray = Resources.LoadAll<ChallengeTrait>("Challenge/Traits");
    this._ChallengeTraitsSource = new Dictionary<string, ChallengeTrait>();
    for (int index = 0; index < challengeTraitArray.Length; ++index)
      this._ChallengeTraitsSource.Add(challengeTraitArray[index].Id.ToLower(), UnityEngine.Object.Instantiate<ChallengeTrait>(challengeTraitArray[index]));
    ChallengeData[] challengeDataArray = Resources.LoadAll<ChallengeData>("Challenge/Weeks");
    this._WeeklyDataSource = new Dictionary<string, ChallengeData>();
    for (int index = 0; index < challengeDataArray.Length; ++index)
      this._WeeklyDataSource.Add(challengeDataArray[index].Id.ToLower(), UnityEngine.Object.Instantiate<ChallengeData>(challengeDataArray[index]));
    if (this.saveAurasText)
      SaveText.SaveAuras(this.auraText);
    if (this.saveCardsText)
    {
      SaveText.SaveCards(this.cardsText);
      SaveText.SaveCardsFluff(this.cardsFluffText);
    }
    if (this.saveClassText)
      SaveText.SaveClass(this.classText);
    if (this.saveMonsterText)
      SaveText.SaveMonster(this.monsterText);
    if (this.saveEventsText)
      SaveText.SaveEvents(this.eventText);
    if (this.saveKeynotesText)
      SaveText.SaveKeynotes(this.keynotesText);
    if (this.saveNodesText)
      SaveText.SaveNodes(this.nodesText);
    if (this.saveTraitsText)
      SaveText.SaveTraits(this.traitsText);
    if (!this.saveRequirementsText)
      return;
    SaveText.SaveRequirements(this.requirementsText);
  }

  public void SetLang(int _langIndex)
  {
    switch (_langIndex)
    {
      case 1:
        this.currentLang = "es";
        break;
      case 2:
        this.currentLang = "zh-CN";
        break;
      case 3:
        this.currentLang = "ko";
        break;
      case 4:
        this.currentLang = "sv";
        break;
      default:
        this.currentLang = "en";
        break;
    }
  }

  private string EventLineForSave(string _line, string _id)
  {
    foreach (KeyValuePair<string, string> savedEventLine in this.savedEventLines)
    {
      if (savedEventLine.Value == _line)
        return "rptd_" + savedEventLine.Key;
    }
    this.savedEventLines.Add(_id, _line);
    return Functions.NormalizeTextForArchive(_line);
  }

  public int GetExperienceByLevel(int level) => this._ExperienceByLevel.ContainsKey(level) ? this._ExperienceByLevel[level] : 10000;

  public int GetExperienceByCardCost(int cardCost) => this._ExperienceByCardCost.ContainsKey(cardCost) ? this._ExperienceByCardCost[cardCost] : 0;

  public void CreateCharClones()
  {
    this._NPCs = new Dictionary<string, NPCData>();
    this._NPCsNamed = new Dictionary<string, NPCData>();
    SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
    foreach (string key in this._NPCsSource.Keys)
    {
      if (!sortedDictionary.ContainsKey(key))
        sortedDictionary.Add(key, this._NPCsSource[key].NPCName);
      this._NPCs.Add(key, UnityEngine.Object.Instantiate<NPCData>(this._NPCsSource[key]));
      string text1 = Texts.Instance.GetText(key + "_name", "monsters");
      if (text1 != "")
        this._NPCs[key].NPCName = text1;
      if (this._NPCsSource[key].IsNamed && this._NPCsSource[key].Difficulty > -1)
      {
        this._NPCsNamed.Add(key, UnityEngine.Object.Instantiate<NPCData>(this._NPCsSource[key]));
        string text2 = Texts.Instance.GetText(key + "_name", "monsters");
        if (text2 != "")
          this._NPCsNamed[key].NPCName = text2;
      }
    }
    if (this.saveMonsterText)
    {
      foreach (KeyValuePair<string, string> keyValuePair in sortedDictionary)
        this.monsterText = this.monsterText + keyValuePair.Key + "_name=" + Functions.NormalizeTextForArchive(keyValuePair.Value) + "\n";
    }
    this._SubClass = new Dictionary<string, SubClassData>();
    foreach (string key in this._SubClassSource.Keys)
    {
      this._SubClass.Add(key, UnityEngine.Object.Instantiate<SubClassData>(this._SubClassSource[key]));
      if (this.saveClassText)
      {
        this.classText = this.classText + key + "_name=" + Functions.NormalizeTextForArchive(this._SubClassSource[key].CharacterName) + "\n";
        this.classText = this.classText + key + "_description=" + Functions.NormalizeTextForArchive(this._SubClassSource[key].CharacterDescription) + "\n";
        this.classText = this.classText + key + "_strength=" + Functions.NormalizeTextForArchive(this._SubClassSource[key].CharacterDescriptionStrength) + "\n";
      }
      this._SubClass[key].CharacterName = Texts.Instance.GetText(key + "_name", "class");
      this._SubClass[key].CharacterDescription = Texts.Instance.GetText(key + "_description", "class");
      this._SubClass[key].CharacterDescriptionStrength = Texts.Instance.GetText(key + "_strength", "class");
    }
  }

  public void CreateAuraCurses()
  {
    this._AurasCursesSource = new Dictionary<string, AuraCurseData>();
    this._AurasCursesIndex = new List<string>();
    AuraCurseData[] auraCurseDataArray1 = Resources.LoadAll<AuraCurseData>("Auras");
    for (int index = 0; index < auraCurseDataArray1.Length; ++index)
    {
      auraCurseDataArray1[index].Init();
      this._AurasCursesSource.Add(auraCurseDataArray1[index].Id, UnityEngine.Object.Instantiate<AuraCurseData>(auraCurseDataArray1[index]));
      this._AurasCursesIndex.Add(auraCurseDataArray1[index].Id.ToLower());
    }
    AuraCurseData[] auraCurseDataArray2 = Resources.LoadAll<AuraCurseData>("Curses");
    for (int index = 0; index < auraCurseDataArray2.Length; ++index)
    {
      auraCurseDataArray2[index].Init();
      this._AurasCursesSource.Add(auraCurseDataArray2[index].Id, UnityEngine.Object.Instantiate<AuraCurseData>(auraCurseDataArray2[index]));
      this._AurasCursesIndex.Add(auraCurseDataArray2[index].Id.ToLower());
    }
    this._AurasCurses = new Dictionary<string, AuraCurseData>();
    foreach (string key in this._AurasCursesSource.Keys)
    {
      this._AurasCurses.Add(key, this._AurasCursesSource[key]);
      this._AurasCurses[key].Init();
      if (this.saveAurasText)
        this.auraText = this.auraText + key + "_description=" + Functions.NormalizeTextForArchive(this._AurasCursesSource[key].Description) + "\n";
      this._AurasCurses[key].ACName = Texts.Instance.GetText(this._AurasCurses[key].Id);
      string text = Texts.Instance.GetText(key + "_description", "auracurse");
      if (text != "")
        this._AurasCurses[key].Description = text;
    }
  }

  public void ModifyAuraCurseKey(string _ac, string _key, string _valueStr = "", int _valueInt = 0)
  {
    AuraCurseData aurasCurse = this._AurasCurses[_ac];
    if ((UnityEngine.Object) aurasCurse == (UnityEngine.Object) null)
      return;
    if (_valueStr != "")
      typeof (AuraCurseData).GetProperty(_key).SetValue((object) aurasCurse, (object) _valueStr, (object[]) null);
    else
      typeof (AuraCurseData).GetProperty(_key).SetValue((object) aurasCurse, (object) _valueInt, (object[]) null);
  }

  public void IncludeInSearch(string _term, string id, bool includeFullTerm = true)
  {
    if (SceneStatic.GetSceneName() != "Game")
      return;
    _term = _term.ToLower();
    string[] strArray = _term.Split(' ', StringSplitOptions.None);
    id = id.ToLower();
    foreach (string key in strArray)
    {
      if (key.Trim() != "")
      {
        if (!this._CardsListSearch.ContainsKey(key))
          this._CardsListSearch.Add(key, new List<string>());
        if (!this._CardsListSearch[key].Contains(id))
          this._CardsListSearch[key].Add(id);
      }
    }
    if (!includeFullTerm)
      return;
    if (!this._CardsListSearch.ContainsKey(_term))
      this._CardsListSearch.Add(_term, new List<string>());
    if (this._CardsListSearch[_term].Contains(id))
      return;
    this._CardsListSearch[_term].Add(id);
  }

  public bool IsInSearch(string _term, string _id)
  {
    _term = _term.ToLower();
    _id = _id.ToLower();
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(_term);
    stringBuilder.Append('_');
    stringBuilder.Append(_id);
    if (this._CardsListSearched == null)
      this._CardsListSearched = new Dictionary<string, bool>();
    else if (this._CardsListSearched.ContainsKey(stringBuilder.ToString()))
      return this._CardsListSearched[stringBuilder.ToString()];
    string[] terms = _term.Split(' ', StringSplitOptions.None);
    bool flag = false;
    for (int i = 0; i < terms.Length; i++)
    {
      flag = false;
      foreach (string key in this._CardsListSearch.Keys.Where<string>((Func<string, bool>) (key => key.Contains(terms[i]))).ToList<string>())
      {
        if (this._CardsListSearch[key].Contains(_id))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        break;
    }
    if (flag)
      this._CardsListSearched.Add(stringBuilder.ToString(), true);
    else
      this._CardsListSearched.Add(stringBuilder.ToString(), false);
    return flag;
  }

  public void CreateCardClones()
  {
    this._CardsListSearch = new Dictionary<string, List<string>>();
    this._CardListByType = new Dictionary<Enums.CardType, List<string>>();
    this._CardListByClass = new Dictionary<Enums.CardClass, List<string>>();
    this._CardListNotUpgraded = new List<string>();
    this._CardListNotUpgradedByClass = new Dictionary<Enums.CardClass, List<string>>();
    this._CardListByClassType = new Dictionary<string, List<string>>();
    this._CardEnergyCost = new Dictionary<string, int>();
    foreach (Enums.CardType key in Enum.GetValues(typeof (Enums.CardType)))
    {
      if (key != Enums.CardType.None)
        this._CardListByType[key] = new List<string>();
    }
    foreach (Enums.CardClass key in Enum.GetValues(typeof (Enums.CardClass)))
    {
      this._CardListByClass[key] = new List<string>();
      this._CardListNotUpgradedByClass[key] = new List<string>();
    }
    this._Cards = new Dictionary<string, CardData>();
    foreach (string key in this._CardsSource.Keys)
      this._Cards.Add(key, this._CardsSource[key]);
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string key1 in this._CardsSource.Keys)
    {
      stringBuilder.Clear();
      this._Cards[key1].InitClone(key1);
      CardData card = this._Cards[key1];
      string text1;
      if (card.UpgradedFrom != "")
      {
        stringBuilder.Append("c_");
        stringBuilder.Append(card.UpgradedFrom);
        stringBuilder.Append("_name");
        text1 = Texts.Instance.GetText(stringBuilder.ToString(), "cards");
      }
      else
      {
        stringBuilder.Append("c_");
        stringBuilder.Append(card.Id);
        stringBuilder.Append("_name");
        text1 = Texts.Instance.GetText(stringBuilder.ToString(), "cards");
      }
      if (text1 != "")
        card.CardName = text1;
      stringBuilder.Clear();
      stringBuilder.Append("c_");
      stringBuilder.Append(card.Id);
      stringBuilder.Append("_fluff");
      string text2 = Texts.Instance.GetText(stringBuilder.ToString(), "cards");
      if (text2 != "")
        card.Fluff = text2;
      if ((card.CardClass != Enums.CardClass.Item || !card.Item.QuestItem) && card.ShowInTome)
      {
        this._CardEnergyCost.Add(card.Id, card.EnergyCost);
        this.IncludeInSearch(card.CardName, card.Id);
        this._CardListByClass[card.CardClass].Add(card.Id);
        if (card.CardUpgraded == Enums.CardUpgraded.No)
        {
          this._CardListNotUpgradedByClass[card.CardClass].Add(card.Id);
          this._CardListNotUpgraded.Add(card.Id);
          if (card.CardClass == Enums.CardClass.Item)
          {
            if (!this._CardItemByType.ContainsKey(card.CardType))
              this._CardItemByType.Add(card.CardType, new List<string>());
            this._CardItemByType[card.CardType].Add(card.Id);
          }
        }
        List<Enums.CardType> cardTypes = card.GetCardTypes();
        for (int index = 0; index < cardTypes.Count; ++index)
        {
          this._CardListByType[cardTypes[index]].Add(card.Id);
          string key2 = Enum.GetName(typeof (Enums.CardClass), (object) card.CardClass) + "_" + Enum.GetName(typeof (Enums.CardType), (object) cardTypes[index]);
          if (!this._CardListByClassType.ContainsKey(key2))
            this._CardListByClassType[key2] = new List<string>();
          this._CardListByClassType[key2].Add(card.Id);
          this.IncludeInSearch(Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) cardTypes[index])), card.Id);
        }
      }
    }
    foreach (string key in this._Cards.Keys)
      this._Cards[key].InitClone2();
    this._CardListNotUpgraded.Sort();
  }

  public void CreateTraitClones()
  {
    this._Traits = new Dictionary<string, TraitData>();
    foreach (string key in this._TraitsSource.Keys)
    {
      this._Traits.Add(key, UnityEngine.Object.Instantiate<TraitData>(this._TraitsSource[key]));
      this._Traits[key].SetNameAndDescription();
    }
  }

  public EventData GetEventData(string id)
  {
    id = id.ToLower();
    return this._Events.ContainsKey(id) ? this._Events[id] : (EventData) null;
  }

  public CinematicData GetCinematicData(string id)
  {
    id = id.ToLower();
    return this._Cinematics.ContainsKey(id) ? this._Cinematics[id] : (CinematicData) null;
  }

  public CombatData GetCombatData(string id)
  {
    if (id != null && id != "")
    {
      id = id.ToLower();
      if (this._CombatDataSource.ContainsKey(id))
        return this._CombatDataSource[id];
    }
    return (CombatData) null;
  }

  public NodeData GetNodeData(string id)
  {
    if (id != null && id != "")
    {
      id = id.ToLower();
      if (this._NodeDataSource.ContainsKey(id))
        return this._NodeDataSource[id];
    }
    return (NodeData) null;
  }

  public TierRewardData GetTierRewardData(int num)
  {
    if (num < 0)
      num = 0;
    return (UnityEngine.Object) this._TierRewardDataSource[num] != (UnityEngine.Object) null ? this._TierRewardDataSource[num] : (TierRewardData) null;
  }

  public ItemData GetItemData(string id) => this._ItemDataSource.ContainsKey(id) ? this._ItemDataSource[id] : (ItemData) null;

  public LootData GetLootData(string id)
  {
    if (id != null && id != "")
    {
      id = id.ToLower();
      if (this._LootDataSource.ContainsKey(id))
        return this._LootDataSource[id];
    }
    return (LootData) null;
  }

  public SubClassData GetSubClassData(string id)
  {
    if (id != null && id != "")
    {
      id = id.Replace(" ", "").ToLower();
      if (this._SubClass.ContainsKey(id))
        return this._SubClass[id];
    }
    return (SubClassData) null;
  }

  public AuraCurseData GetAuraCurseData(string id)
  {
    if (id != null && id != "")
    {
      id = id.ToLower();
      if (this._AurasCurses.ContainsKey(id))
        return this._AurasCurses[id];
    }
    return (AuraCurseData) null;
  }

  public int GetAuraCurseIndex(string id)
  {
    if (id != null && id != "")
    {
      id = id.ToLower();
      if (this._AurasCursesIndex.Contains(id))
        return this._AurasCursesIndex.IndexOf(id);
    }
    return -1;
  }

  public string GetAuraCurseFromIndex(int index) => index > -1 && index < this._AurasCursesIndex.Count ? this._AurasCursesIndex[index] : "";

  public TraitData GetTraitData(string id)
  {
    if (id != null && id != "")
    {
      id = Functions.Sanitize(id);
      if (id == "engineer")
        id = "inventor";
      if (this._Traits != null && this._Traits.ContainsKey(id))
        return this._Traits[id];
    }
    return (TraitData) null;
  }

  public PackData GetPackData(string id)
  {
    if (id != null && id != "")
    {
      id = id.ToLower();
      if (this._PackDataSource.ContainsKey(id))
        return this._PackDataSource[id];
    }
    return (PackData) null;
  }

  public SkinData GetSkinData(string id)
  {
    if (id != null && id != "")
    {
      id = id.ToLower();
      if (this._SkinDataSource.ContainsKey(id))
        return this._SkinDataSource[id];
    }
    return (SkinData) null;
  }

  public List<SkinData> GetSkinsBySubclass(string id)
  {
    List<SkinData> skinsBySubclass = new List<SkinData>();
    id = id.ToLower();
    foreach (KeyValuePair<string, SkinData> keyValuePair in this._SkinDataSource)
    {
      if (keyValuePair.Value.SkinSubclass.Id.ToLower() == id)
        skinsBySubclass.Add(keyValuePair.Value);
    }
    return skinsBySubclass;
  }

  public string GetSkinBaseIdBySubclass(string id)
  {
    if (id != null && id != "")
    {
      id = id.ToLower();
      foreach (KeyValuePair<string, SkinData> keyValuePair in this._SkinDataSource)
      {
        if (keyValuePair.Value.SkinSubclass.Id.ToLower() == id && keyValuePair.Value.BaseSkin)
          return keyValuePair.Value.SkinId.ToLower().Trim();
      }
    }
    return "";
  }

  public CardbackData GetCardbackData(string id)
  {
    id = id.ToLower();
    return this._CardbackDataSource.ContainsKey(id) ? this._CardbackDataSource[id] : (CardbackData) null;
  }

  public string GetCardbackBaseIdBySubclass(string id)
  {
    id = id.ToLower();
    if (id != null && id != "" && this._CardbackDataSource != null)
    {
      foreach (KeyValuePair<string, CardbackData> keyValuePair in this._CardbackDataSource)
      {
        if ((UnityEngine.Object) keyValuePair.Value.CardbackSubclass != (UnityEngine.Object) null && keyValuePair.Value.CardbackSubclass.Id.ToLower() == id && keyValuePair.Value.BaseCardback)
          return keyValuePair.Value.CardbackId.ToLower().Trim();
      }
    }
    return "";
  }

  public CardPlayerPackData GetCardPlayerPackData(string id)
  {
    id = id.ToLower();
    return this._CardPlayerPackDataSource.ContainsKey(id) ? this._CardPlayerPackDataSource[id] : (CardPlayerPackData) null;
  }

  public CardPlayerPairsPackData GetCardPlayerPairsPackData(string id)
  {
    id = id.ToLower();
    return this._CardPlayerPairsPackDataSource.ContainsKey(id) ? this._CardPlayerPairsPackDataSource[id] : (CardPlayerPairsPackData) null;
  }

  public ChallengeData GetWeeklyById(string _id)
  {
    _id = _id.ToLower();
    foreach (KeyValuePair<string, ChallengeData> keyValuePair in this._WeeklyDataSource)
    {
      if (this.WeeklyDataSource.ContainsKey(_id))
        return this.WeeklyDataSource[_id];
    }
    return (ChallengeData) null;
  }

  public ChallengeData GetWeeklyData(int _week)
  {
    string key1 = "week" + _week.ToString();
    if (this._WeeklyDataSource.ContainsKey(key1))
      return this._WeeklyDataSource[key1];
    int num1 = 16;
    int num2 = _week % num1;
    if (num2 == 0)
      num2 = num1;
    string key2 = "week" + num2.ToString();
    return this._WeeklyDataSource.ContainsKey(key2) ? this._WeeklyDataSource[key2] : (ChallengeData) null;
  }

  public List<PerkData> GetPerkDataClass(Enums.CardClass cardClass)
  {
    List<PerkData> perkDataClass = new List<PerkData>();
    foreach (KeyValuePair<string, PerkData> keyValuePair in this._PerksSource)
    {
      if (keyValuePair.Value.CardClass == cardClass)
        perkDataClass.Add(keyValuePair.Value);
    }
    return perkDataClass;
  }

  public PerkData GetPerkData(string id)
  {
    id = Functions.Sanitize(id).ToLower();
    return this._PerksSource.ContainsKey(id) ? this._PerksSource[id] : (PerkData) null;
  }

  public CardData GetCardData(string id, bool instantiate = true)
  {
    id = Functions.Sanitize(id).ToLower().Trim().Split("_", StringSplitOptions.None)[0];
    if (!(id != "") || !this._Cards.ContainsKey(id))
      return (CardData) null;
    if (!instantiate)
      return this._Cards[id];
    CardData cardData = UnityEngine.Object.Instantiate<CardData>(this._Cards[id]);
    this._InstantiatedCardData.Add(cardData);
    return cardData;
  }

  public void CleanInstantiatedCardData()
  {
    for (int index = this._InstantiatedCardData.Count - 1; index >= 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this._InstantiatedCardData[index], 1f);
    this._InstantiatedCardData.Clear();
  }

  public int InstantiatedCardDataCount() => this._InstantiatedCardData.Count;

  public HeroData GetHeroData(string id)
  {
    HeroData heroData;
    return !this._Heroes.TryGetValue(id.ToLower(), out heroData) ? (HeroData) null : heroData;
  }

  public KeyNotesData GetKeyNotesData(string id)
  {
    KeyNotesData keyNotesData;
    return !this.KeyNotes.TryGetValue(id.ToLower(), out keyNotesData) ? (KeyNotesData) null : keyNotesData;
  }

  public NPCData GetNPC(string id)
  {
    if (this.ShowDebug)
      Functions.DebugLogGD("GetNPC -> " + id, "trace");
    NPCData npcData;
    return !this._NPCs.TryGetValue(id.ToLower(), out npcData) ? (NPCData) null : npcData;
  }

  public NPCData GetNPCForRandom(
    bool _rare,
    int position,
    Enums.CombatTier _ct,
    NPCData[] _teamNPC)
  {
    bool flag1 = false;
    int num1 = 0;
    int num2 = UnityEngine.Random.Range(0, 100);
    switch (_ct)
    {
      case Enums.CombatTier.T1:
        num1 = !_rare ? 1 : 2;
        break;
      case Enums.CombatTier.T2:
        num1 = !_rare ? 3 : 4;
        break;
      case Enums.CombatTier.T3:
        num1 = !_rare ? (num2 >= 80 ? 7 : 5) : 6;
        break;
      case Enums.CombatTier.T4:
        num1 = !_rare ? (num2 >= 15 ? (num2 >= 85 ? 9 : 7) : 5) : 8;
        break;
      case Enums.CombatTier.T5:
        num1 = !_rare ? (num2 >= 20 ? 9 : 7) : 10;
        break;
      case Enums.CombatTier.T6:
        num1 = !_rare ? (num2 >= 60 ? 7 : 5) : (num2 >= 50 ? 8 : 6);
        break;
      case Enums.CombatTier.T7:
        num1 = !_rare ? (num2 >= 30 ? 9 : 7) : (num2 >= 50 ? 10 : 8);
        break;
      case Enums.CombatTier.T8:
        if (_rare)
        {
          if (GameManager.Instance.IsWeeklyChallenge())
          {
            ChallengeData weeklyData = this.GetWeeklyData(AtOManager.Instance.GetWeekly());
            if ((UnityEngine.Object) weeklyData != (UnityEngine.Object) null && (UnityEngine.Object) weeklyData.Boss1 != (UnityEngine.Object) null)
              return weeklyData.Boss1;
          }
          num1 = 15;
          break;
        }
        num1 = num2 >= 70 ? 5 : 3;
        break;
      case Enums.CombatTier.T9:
        if (_rare)
        {
          if (GameManager.Instance.IsWeeklyChallenge())
          {
            ChallengeData weeklyData = this.GetWeeklyData(AtOManager.Instance.GetWeekly());
            if ((UnityEngine.Object) weeklyData != (UnityEngine.Object) null && (UnityEngine.Object) weeklyData.Boss2 != (UnityEngine.Object) null)
              return weeklyData.Boss2;
          }
          num1 = 16;
          break;
        }
        num1 = num2 >= 30 ? 9 : 7;
        break;
      case Enums.CombatTier.T10:
        num1 = !_rare ? (num2 >= 20 ? 11 : 9) : (num2 >= 50 ? 12 : 10);
        break;
      case Enums.CombatTier.T11:
        num1 = !_rare ? (num2 >= 70 ? 13 : 11) : 12;
        break;
      case Enums.CombatTier.T12:
        num1 = !_rare ? (num2 >= 30 ? 13 : 11) : 12;
        break;
    }
    NPCData npcForRandom = (NPCData) null;
    int num3 = 0;
    while (!flag1)
    {
      ++num3;
      if (num3 > 10000)
        return (NPCData) null;
      npcForRandom = !_rare ? this._NPCs[this._NPCs.Keys.ToArray<string>()[UnityEngine.Random.Range(0, this._NPCs.Keys.Count)]] : this._NPCsNamed[this._NPCsNamed.Keys.ToArray<string>()[UnityEngine.Random.Range(0, this._NPCsNamed.Keys.Count)]];
      bool flag2 = true;
      if (_teamNPC != null)
      {
        int num4 = 0;
        for (int index = 0; index < _teamNPC.Length; ++index)
        {
          if ((UnityEngine.Object) _teamNPC[index] != (UnityEngine.Object) null && (UnityEngine.Object) _teamNPC[index].SpriteSpeed != (UnityEngine.Object) null && (UnityEngine.Object) npcForRandom.SpriteSpeed != (UnityEngine.Object) null && _teamNPC[index].SpriteSpeed.name == npcForRandom.SpriteSpeed.name)
          {
            ++num4;
            if (num4 >= 1)
            {
              flag2 = false;
              break;
            }
          }
        }
      }
      if (flag2 && npcForRandom.Difficulty == num1)
      {
        if (!_rare)
        {
          switch (position)
          {
            case -1:
              if (npcForRandom.PreferredPosition == Enums.CardTargetPosition.Anywhere || npcForRandom.PreferredPosition == Enums.CardTargetPosition.Back)
              {
                flag1 = true;
                continue;
              }
              continue;
            case 0:
              break;
            case 1:
              if (npcForRandom.PreferredPosition == Enums.CardTargetPosition.Anywhere || npcForRandom.PreferredPosition == Enums.CardTargetPosition.Front)
              {
                flag1 = true;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
        flag1 = true;
      }
    }
    if ((_ct == Enums.CombatTier.T6 || _ct == Enums.CombatTier.T7 || _ct == Enums.CombatTier.T9 || _ct == Enums.CombatTier.T10) && (UnityEngine.Object) npcForRandom.UpgradedMob != (UnityEngine.Object) null)
      npcForRandom = npcForRandom.UpgradedMob;
    return npcForRandom;
  }

  public EventRequirementData GetRequirementData(string id)
  {
    EventRequirementData eventRequirementData;
    return !this._Requirements.TryGetValue(id.ToLower(), out eventRequirementData) ? (EventRequirementData) null : eventRequirementData;
  }

  public int GetRemoveCost(Enums.CardType type, Enums.CardRarity rarity)
  {
    int num = 100;
    int removeCost;
    if (type == Enums.CardType.Injury || type == Enums.CardType.Boon)
    {
      switch (rarity)
      {
        case Enums.CardRarity.Common:
          removeCost = num;
          break;
        case Enums.CardRarity.Uncommon:
          removeCost = num + 50;
          break;
        case Enums.CardRarity.Rare:
          removeCost = num + 100;
          break;
        case Enums.CardRarity.Epic:
          removeCost = num + 150;
          break;
        default:
          removeCost = num + 200;
          break;
      }
    }
    else
    {
      switch (AtOManager.Instance.GetTownTier())
      {
        case 0:
          removeCost = 30;
          break;
        case 1:
          removeCost = 90;
          break;
        case 2:
          removeCost = 120;
          break;
        case 3:
          removeCost = 160;
          break;
        default:
          removeCost = 200;
          break;
      }
    }
    return removeCost;
  }

  public int GetUpgradeCost(string action, string rarity)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(action);
    stringBuilder.Append("_");
    stringBuilder.Append(rarity);
    return this._UpgradeCost.ContainsKey(stringBuilder.ToString()) ? this._UpgradeCost[stringBuilder.ToString()] : -1;
  }

  public int GetCraftCost(string cardId, float discountCraft = 0.0f, float discountUpgrade = 0.0f)
  {
    CardData cardData1 = this.GetCardData(cardId, false);
    int num1 = 0;
    StringBuilder stringBuilder = new StringBuilder();
    if (cardData1.CardUpgraded == Enums.CardUpgraded.No)
    {
      stringBuilder.Append(Enum.GetName(typeof (Enums.CardRarity), (object) cardData1.CardRarity));
      if (this._CraftCost.ContainsKey(stringBuilder.ToString()))
        num1 = this._CraftCost[stringBuilder.ToString()];
      if ((double) discountCraft != 0.0)
        num1 -= Functions.FuncRoundToInt((float) num1 * discountCraft);
    }
    else
    {
      CardData cardData2 = this.GetCardData(cardData1.UpgradedFrom, false);
      stringBuilder.Append(Enum.GetName(typeof (Enums.CardRarity), (object) cardData2.CardRarity));
      if (this._CraftCost.ContainsKey(stringBuilder.ToString()))
      {
        int num2 = this._CraftCost[stringBuilder.ToString()];
        if ((double) discountCraft != 0.0)
          num2 -= Functions.FuncRoundToInt((float) num2 * discountCraft);
        float upgradeCost = (float) this.GetUpgradeCost("Upgrade", Enum.GetName(typeof (Enums.CardRarity), (object) cardData1.CardRarity));
        if ((double) discountUpgrade != 0.0)
          upgradeCost -= (float) Functions.FuncRoundToInt(upgradeCost * discountUpgrade);
        num1 = num2 + Functions.FuncRoundToInt(upgradeCost);
      }
    }
    return num1 > 0 ? num1 : -1;
  }

  public int GetItemCost(string cardId)
  {
    CardData cardData = this.GetCardData(cardId, false);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Enum.GetName(typeof (Enums.CardType), (object) cardData.CardType));
    stringBuilder.Append("_");
    stringBuilder.Append(Enum.GetName(typeof (Enums.CardRarity), (object) cardData.CardRarity));
    return this._ItemCost.ContainsKey(stringBuilder.ToString()) ? this._ItemCost[stringBuilder.ToString()] : -1;
  }

  public bool ExistsCraftRarity(Enums.CardRarity rarity, int zoneTier)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(zoneTier);
    stringBuilder.Append("_");
    stringBuilder.Append(Enum.GetName(typeof (Enums.CardRarity), (object) rarity));
    return this._CraftCost.ContainsKey(stringBuilder.ToString());
  }

  public int GetDivinationCost(string divinationTier)
  {
    if (!this._DivinationCost.ContainsKey(divinationTier))
      return -1;
    int divinationCost = this._DivinationCost[divinationTier];
    if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_4_5"))
      divinationCost -= Functions.FuncRoundToInt((float) divinationCost * 0.4f);
    else if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_4_3"))
      divinationCost -= Functions.FuncRoundToInt((float) divinationCost * 0.25f);
    else if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_4_1"))
      divinationCost -= Functions.FuncRoundToInt((float) divinationCost * 0.1f);
    if (AtOManager.Instance.Sandbox_divinationsPrice != 0)
      divinationCost += Functions.FuncRoundToInt((float) ((double) divinationCost * (double) AtOManager.Instance.Sandbox_divinationsPrice * 0.0099999997764825821));
    return divinationCost;
  }

  public int GetDivinationTier(int divinationIndex) => this._DivinationTier.ContainsKey(divinationIndex) ? this._DivinationTier[divinationIndex] : 0;

  public int GetCostReroll()
  {
    if (AtOManager.Instance.Sandbox_freeRerolls)
      return 0;
    int num1;
    switch (AtOManager.Instance.GetTownTier())
    {
      case 0:
        num1 = 150;
        break;
      case 1:
        num1 = 200;
        break;
      case 2:
        num1 = 250;
        break;
      default:
        num1 = 300;
        break;
    }
    int num2 = 4;
    if (GameManager.Instance.IsMultiplayer())
    {
      num2 = 0;
      Hero[] team = AtOManager.Instance.GetTeam();
      if (team != null)
      {
        for (int index = 0; index < 4; ++index)
        {
          if (team[index] != null && team[index].Owner == NetworkManager.Instance.GetPlayerNick())
            ++num2;
        }
      }
    }
    int costReroll = num1 * num2;
    if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_5_4"))
      costReroll -= Functions.FuncRoundToInt((float) costReroll * 0.5f);
    return costReroll;
  }

  public UnityEngine.WaitForSeconds WaitForSeconds(float _time)
  {
    if (this._WaitForSecondsDict.ContainsKey(_time))
      return this._WaitForSecondsDict[_time];
    UnityEngine.WaitForSeconds waitForSeconds = new UnityEngine.WaitForSeconds(_time);
    this._WaitForSecondsDict.Add(_time, waitForSeconds);
    return waitForSeconds;
  }

  public GameObject GetResourceEffect(string _effect) => Resources.Load("Effects/" + _effect) as GameObject;

  public Dictionary<string, CardData> Cards
  {
    get => this._Cards;
    set => this._Cards = value;
  }

  public Dictionary<string, HeroData> Heroes
  {
    get => this._Heroes;
    set => this._Heroes = value;
  }

  public Dictionary<string, NPCData> NPCs => this._NPCs;

  public Dictionary<string, string> SkillColor
  {
    get => this._SkillColor;
    set => this._SkillColor = value;
  }

  public Dictionary<string, string> ClassColor
  {
    get => this._ClassColor;
    set => this._ClassColor = value;
  }

  public Dictionary<string, Color> ColorColor
  {
    get => this._ColorColor;
    set => this._ColorColor = value;
  }

  public Dictionary<string, Color> RarityColor
  {
    get => this._RarityColor;
    set => this._RarityColor = value;
  }

  public Dictionary<string, string> CardsDescriptionNormalized
  {
    get => this._CardsDescriptionNormalized;
    set => this._CardsDescriptionNormalized = value;
  }

  public Dictionary<string, SubClassData> SubClass
  {
    get => this._SubClass;
    set => this._SubClass = value;
  }

  public Dictionary<Enums.CardType, List<string>> CardListByType
  {
    get => this._CardListByType;
    set => this._CardListByType = value;
  }

  public Dictionary<Enums.CardClass, List<string>> CardListByClass
  {
    get => this._CardListByClass;
    set => this._CardListByClass = value;
  }

  public List<string> CardListNotUpgraded
  {
    get => this._CardListNotUpgraded;
    set => this._CardListNotUpgraded = value;
  }

  public Dictionary<Enums.CardClass, List<string>> CardListNotUpgradedByClass
  {
    get => this._CardListNotUpgradedByClass;
    set => this._CardListNotUpgradedByClass = value;
  }

  public Dictionary<string, List<string>> CardListByClassType
  {
    get => this._CardListByClassType;
    set => this._CardListByClassType = value;
  }

  public string CurrentLang
  {
    get => this.currentLang;
    set => this.currentLang = value;
  }

  public Dictionary<string, EventRequirementData> Requirements
  {
    get => this._Requirements;
    set => this._Requirements = value;
  }

  public Dictionary<string, NodeData> NodeDataSource
  {
    get => this._NodeDataSource;
    set => this._NodeDataSource = value;
  }

  public Dictionary<Enums.CardType, List<string>> CardItemByType
  {
    get => this._CardItemByType;
    set => this._CardItemByType = value;
  }

  public Dictionary<string, PackData> PackDataSource
  {
    get => this._PackDataSource;
    set => this._PackDataSource = value;
  }

  public Dictionary<string, SkinData> SkinDataSource
  {
    get => this._SkinDataSource;
    set => this._SkinDataSource = value;
  }

  public List<int> PerkLevel
  {
    get => this._PerkLevel;
    set => this._PerkLevel = value;
  }

  public Dictionary<string, CorruptionPackData> CorruptionPackDataSource
  {
    get => this._CorruptionPackDataSource;
    set => this._CorruptionPackDataSource = value;
  }

  public SortedDictionary<string, KeyNotesData> KeyNotes
  {
    get => this._KeyNotes;
    set => this._KeyNotes = value;
  }

  public Dictionary<string, int> CardEnergyCost
  {
    get => this._CardEnergyCost;
    set => this._CardEnergyCost = value;
  }

  public Dictionary<string, List<string>> CardsListSearch
  {
    get => this._CardsListSearch;
    set => this._CardsListSearch = value;
  }

  public Dictionary<string, ZoneData> ZoneDataSource
  {
    get => this._ZoneDataSource;
    set => this._ZoneDataSource = value;
  }

  public Dictionary<string, EventData> Events
  {
    get => this._Events;
    set => this._Events = value;
  }

  public int CombatFPS
  {
    get => this.combatFPS;
    set => this.combatFPS = value;
  }

  public int NormalFPS
  {
    get => this.normalFPS;
    set => this.normalFPS = value;
  }

  public DateTime InitialWeeklyDate
  {
    get => this.initialWeeklyDate;
    set => this.initialWeeklyDate = value;
  }

  public Dictionary<string, CardPlayerPackData> CardPlayerPackDataSource
  {
    get => this._CardPlayerPackDataSource;
    set => this._CardPlayerPackDataSource = value;
  }

  public string DefaultNickName
  {
    get => this.defaultNickName;
    set => this.defaultNickName = value;
  }

  public Dictionary<string, string> IncompatibleVersion
  {
    get => this._IncompatibleVersion;
    set => this._IncompatibleVersion = value;
  }

  public Dictionary<string, ChallengeTrait> ChallengeTraitsSource
  {
    get => this._ChallengeTraitsSource;
    set => this._ChallengeTraitsSource = value;
  }

  public int[] CorruptionBasePercent
  {
    get => this._CorruptionBasePercent;
    set => this._CorruptionBasePercent = value;
  }

  public List<int> RankLevel
  {
    get => this._RankLevel;
    set => this._RankLevel = value;
  }

  public static int MaxPerkPoints => 50;

  public Dictionary<string, CardbackData> CardbackDataSource
  {
    get => this._CardbackDataSource;
    set => this._CardbackDataSource = value;
  }

  public Dictionary<string, PerkNodeData> PerksNodesSource
  {
    get => this._PerksNodesSource;
    set => this._PerksNodesSource = value;
  }

  public Dictionary<string, ChallengeData> WeeklyDataSource
  {
    get => this._WeeklyDataSource;
    set => this._WeeklyDataSource = value;
  }

  public bool ShowDebug
  {
    get => this.showDebug;
    set => this.showDebug = value;
  }

  public List<string> SkuAvailable
  {
    get => this._SkuAvailable;
    set => this._SkuAvailable = value;
  }

  public Dictionary<string, string> NodeCombatEventRelation
  {
    get => this._NodeCombatEventRelation;
    set => this._NodeCombatEventRelation = value;
  }
}
