// Decompiled with JetBrains decompiler
// Type: EventReplyData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EventReplyData
{
  [Header("Repeat this reply for all characters")]
  [SerializeField]
  private bool repeatForAllCharacters;
  [Header("Requirements")]
  [SerializeField]
  private SubClassData requiredClass;
  [SerializeField]
  private int indexForAnswerTranslation;
  [SerializeField]
  private EventRequirementData requirement;
  [SerializeField]
  private EventRequirementData requirementBlocked;
  [SerializeField]
  private CardData requirementItem;
  [SerializeField]
  private List<CardData> requirementCard;
  [SerializeField]
  private bool requirementMultiplayer;
  [SerializeField]
  private string requirementSku;
  [Header("Reply")]
  [SerializeField]
  [TextArea]
  private string replyText;
  [SerializeField]
  private Enums.EventAction replyActionText;
  [SerializeField]
  private CardData replyShowCard;
  [Header("Gold Cost")]
  [SerializeField]
  private int goldCost;
  [Header("Dust Cost")]
  [SerializeField]
  private int dustCost;
  [Header("Rolls")]
  [SerializeField]
  private bool ssRoll;
  [SerializeField]
  private int ssRollNumber;
  [SerializeField]
  private int ssRollNumberCritical = -1;
  [SerializeField]
  private int ssRollNumberCriticalFail = -1;
  [SerializeField]
  private Enums.RollMode ssRollMode;
  [SerializeField]
  private Enums.RollTarget ssRollTarget;
  [SerializeField]
  private Enums.CardType ssRollCard;
  [Header("Success")]
  [SerializeField]
  private PerkData ssPerkData;
  [SerializeField]
  private PerkData ssPerkData1;
  [SerializeField]
  private NodeData ssNodeTravel;
  [SerializeField]
  [TextArea]
  private string ssRewardText;
  [SerializeField]
  private float ssRewardHealthPercent;
  [SerializeField]
  private int ssRewardHealthFlat;
  [SerializeField]
  private int ssGoldReward;
  [SerializeField]
  private int ssDustReward;
  [SerializeField]
  private int ssSupplyReward;
  [SerializeField]
  private int ssExperienceReward;
  [SerializeField]
  private EventRequirementData ssRequirementUnlock;
  [SerializeField]
  private EventRequirementData ssRequirementUnlock2;
  [SerializeField]
  private EventRequirementData ssRequirementLock;
  [SerializeField]
  private EventRequirementData ssRequirementLock2;
  [SerializeField]
  private CardData ssAddCard1;
  [SerializeField]
  private CardData ssAddCard2;
  [SerializeField]
  private CardData ssAddCard3;
  [SerializeField]
  private CombatData ssCombat;
  [SerializeField]
  private EventData ssEvent;
  [SerializeField]
  private TierRewardData ssRewardTier;
  [SerializeField]
  private int ssDiscount;
  [SerializeField]
  private int ssMaxQuantity;
  [SerializeField]
  private bool ssHealerUI;
  [SerializeField]
  private bool ssUpgradeUI;
  [SerializeField]
  private bool ssCraftUI;
  [SerializeField]
  private bool ssCorruptionUI;
  [SerializeField]
  private Enums.CardRarity ssCraftUIMaxType;
  [SerializeField]
  private bool ssMerchantUI;
  [SerializeField]
  private LootData ssShopList;
  [SerializeField]
  private LootData ssLootList;
  [SerializeField]
  private SubClassData ssUnlockClass;
  [SerializeField]
  private bool ssUpgradeRandomCard;
  [SerializeField]
  private CardData ssAddItem;
  [SerializeField]
  private Enums.ItemSlot ssRemoveItemSlot;
  [SerializeField]
  private Enums.ItemSlot ssCorruptItemSlot;
  [SerializeField]
  private bool ssCardPlayerGame;
  [SerializeField]
  private CardPlayerPackData ssCardPlayerGamePackData;
  [SerializeField]
  private bool ssCardPlayerPairsGame;
  [SerializeField]
  private CardPlayerPairsPackData ssCardPlayerPairsGamePackData;
  [SerializeField]
  private bool ssFinishGame;
  [SerializeField]
  private bool ssFinishEarlyAccess;
  [SerializeField]
  private bool ssFinishObeliskMap;
  [SerializeField]
  private string ssUnlockSteamAchievement;
  [SerializeField]
  private string ssSteamStat;
  [SerializeField]
  private SkinData ssUnlockSkin;
  [Header("Character Replacement")]
  [SerializeField]
  private SubClassData ssCharacterReplacement;
  [SerializeField]
  private int ssCharacterReplacementPosition;
  [Header("Fail")]
  [SerializeField]
  private NodeData flNodeTravel;
  [SerializeField]
  [TextArea]
  private string flRewardText;
  [SerializeField]
  private float flRewardHealthPercent;
  [SerializeField]
  private int flRewardHealthFlat;
  [SerializeField]
  private int flGoldReward;
  [SerializeField]
  private int flDustReward;
  [SerializeField]
  private int flSupplyReward;
  [SerializeField]
  private int flExperienceReward;
  [SerializeField]
  private EventRequirementData flRequirementUnlock;
  [SerializeField]
  private EventRequirementData flRequirementUnlock2;
  [SerializeField]
  private EventRequirementData flRequirementLock;
  [SerializeField]
  private CardData flAddCard1;
  [SerializeField]
  private CardData flAddCard2;
  [SerializeField]
  private CardData flAddCard3;
  [SerializeField]
  private CombatData flCombat;
  [SerializeField]
  private EventData flEvent;
  [SerializeField]
  private TierRewardData flRewardTier;
  [SerializeField]
  private int flDiscount;
  [SerializeField]
  private bool flHealerUI;
  [SerializeField]
  private int flMaxQuantity;
  [SerializeField]
  private bool flUpgradeUI;
  [SerializeField]
  private bool flCraftUI;
  [SerializeField]
  private Enums.CardRarity flCraftUIMaxType;
  [SerializeField]
  private bool flCorruptionUI;
  [SerializeField]
  private bool flMerchantUI;
  [SerializeField]
  private LootData flShopList;
  [SerializeField]
  private LootData flLootList;
  [SerializeField]
  private bool flUpgradeRandomCard;
  [SerializeField]
  private CardData flAddItem;
  [SerializeField]
  private Enums.ItemSlot flRemoveItemSlot;
  [SerializeField]
  private Enums.ItemSlot flCorruptItemSlot;
  [SerializeField]
  private bool flCardPlayerGame;
  [SerializeField]
  private CardPlayerPackData flCardPlayerGamePackData;
  [SerializeField]
  private bool flCardPlayerPairsGame;
  [SerializeField]
  private CardPlayerPairsPackData flCardPlayerPairsGamePackData;
  [SerializeField]
  private SubClassData flUnlockClass;
  [SerializeField]
  private string flUnlockSteamAchievement;
  [Header("Critical Success")]
  [SerializeField]
  private NodeData sscNodeTravel;
  [SerializeField]
  [TextArea]
  private string sscRewardText;
  [SerializeField]
  private float sscRewardHealthPercent;
  [SerializeField]
  private int sscRewardHealthFlat;
  [SerializeField]
  private int sscGoldReward;
  [SerializeField]
  private int sscDustReward;
  [SerializeField]
  private int sscSupplyReward;
  [SerializeField]
  private int sscExperienceReward;
  [SerializeField]
  private EventRequirementData sscRequirementUnlock;
  [SerializeField]
  private EventRequirementData sscRequirementUnlock2;
  [SerializeField]
  private EventRequirementData sscRequirementLock;
  [SerializeField]
  private CardData sscAddCard1;
  [SerializeField]
  private CardData sscAddCard2;
  [SerializeField]
  private CardData sscAddCard3;
  [SerializeField]
  private CombatData sscCombat;
  [SerializeField]
  private EventData sscEvent;
  [SerializeField]
  private TierRewardData sscRewardTier;
  [SerializeField]
  private int sscDiscount;
  [SerializeField]
  private int sscMaxQuantity;
  [SerializeField]
  private bool sscHealerUI;
  [SerializeField]
  private bool sscUpgradeUI;
  [SerializeField]
  private bool sscCraftUI;
  [SerializeField]
  private Enums.CardRarity sscCraftUIMaxType;
  [SerializeField]
  private bool sscCorruptionUI;
  [SerializeField]
  private bool sscMerchantUI;
  [SerializeField]
  private LootData sscShopList;
  [SerializeField]
  private LootData sscLootList;
  [SerializeField]
  private SubClassData sscUnlockClass;
  [SerializeField]
  private bool sscUpgradeRandomCard;
  [SerializeField]
  private CardData sscAddItem;
  [SerializeField]
  private Enums.ItemSlot sscRemoveItemSlot;
  [SerializeField]
  private Enums.ItemSlot sscCorruptItemSlot;
  [SerializeField]
  private bool sscCardPlayerGame;
  [SerializeField]
  private CardPlayerPackData sscCardPlayerGamePackData;
  [SerializeField]
  private bool sscCardPlayerPairsGame;
  [SerializeField]
  private CardPlayerPairsPackData sscCardPlayerPairsGamePackData;
  [SerializeField]
  private bool sscFinishGame;
  [SerializeField]
  private bool sscFinishEarlyAccess;
  [SerializeField]
  private string sscUnlockSteamAchievement;
  [Header("Critical Fail")]
  [SerializeField]
  private NodeData flcNodeTravel;
  [SerializeField]
  [TextArea]
  private string flcRewardText;
  [SerializeField]
  private float flcRewardHealthPercent;
  [SerializeField]
  private int flcRewardHealthFlat;
  [SerializeField]
  private int flcGoldReward;
  [SerializeField]
  private int flcDustReward;
  [SerializeField]
  private int flcSupplyReward;
  [SerializeField]
  private int flcExperienceReward;
  [SerializeField]
  private EventRequirementData flcRequirementUnlock;
  [SerializeField]
  private EventRequirementData flcRequirementUnlock2;
  [SerializeField]
  private EventRequirementData flcRequirementLock;
  [SerializeField]
  private CardData flcAddCard1;
  [SerializeField]
  private CardData flcAddCard2;
  [SerializeField]
  private CardData flcAddCard3;
  [SerializeField]
  private CombatData flcCombat;
  [SerializeField]
  private EventData flcEvent;
  [SerializeField]
  private TierRewardData flcRewardTier;
  [SerializeField]
  private int flcDiscount;
  [SerializeField]
  private bool flcHealerUI;
  [SerializeField]
  private int flcMaxQuantity;
  [SerializeField]
  private bool flcUpgradeUI;
  [SerializeField]
  private bool flcCraftUI;
  [SerializeField]
  private Enums.CardRarity flcCraftUIMaxType;
  [SerializeField]
  private bool flcCorruptionUI;
  [SerializeField]
  private bool flcMerchantUI;
  [SerializeField]
  private LootData flcShopList;
  [SerializeField]
  private LootData flcLootList;
  [SerializeField]
  private bool flcUpgradeRandomCard;
  [SerializeField]
  private CardData flcAddItem;
  [SerializeField]
  private Enums.ItemSlot flcRemoveItemSlot;
  [SerializeField]
  private Enums.ItemSlot flcCorruptItemSlot;
  [SerializeField]
  private bool flcCardPlayerGame;
  [SerializeField]
  private CardPlayerPackData flcCardPlayerGamePackData;
  [SerializeField]
  private bool flcCardPlayerPairsGame;
  [SerializeField]
  private CardPlayerPairsPackData flcCardPlayerPairsGamePackData;
  [SerializeField]
  private SubClassData flcUnlockClass;
  [SerializeField]
  private string flcUnlockSteamAchievement;

  public EventReplyData ShallowCopy() => (EventReplyData) this.MemberwiseClone();

  public SubClassData RequiredClass
  {
    get => this.requiredClass;
    set => this.requiredClass = value;
  }

  public EventRequirementData Requirement
  {
    get => this.requirement;
    set => this.requirement = value;
  }

  public EventRequirementData RequirementBlocked
  {
    get => this.requirementBlocked;
    set => this.requirementBlocked = value;
  }

  public string ReplyText
  {
    get => this.replyText;
    set => this.replyText = value;
  }

  public int GoldCost
  {
    get => this.goldCost;
    set => this.goldCost = value;
  }

  public bool SsRoll
  {
    get => this.ssRoll;
    set => this.ssRoll = value;
  }

  public int SsRollNumber
  {
    get => this.ssRollNumber;
    set => this.ssRollNumber = value;
  }

  public Enums.RollMode SsRollMode
  {
    get => this.ssRollMode;
    set => this.ssRollMode = value;
  }

  public Enums.RollTarget SsRollTarget
  {
    get => this.ssRollTarget;
    set => this.ssRollTarget = value;
  }

  public string SsRewardText
  {
    get => this.ssRewardText;
    set => this.ssRewardText = value;
  }

  public float SsRewardHealthPercent
  {
    get => this.ssRewardHealthPercent;
    set => this.ssRewardHealthPercent = value;
  }

  public int SsRewardHealthFlat
  {
    get => this.ssRewardHealthFlat;
    set => this.ssRewardHealthFlat = value;
  }

  public int SsGoldReward
  {
    get => this.ssGoldReward;
    set => this.ssGoldReward = value;
  }

  public int SsDustReward
  {
    get => this.ssDustReward;
    set => this.ssDustReward = value;
  }

  public int SsExperienceReward
  {
    get => this.ssExperienceReward;
    set => this.ssExperienceReward = value;
  }

  public EventRequirementData SsRequirementUnlock
  {
    get => this.ssRequirementUnlock;
    set => this.ssRequirementUnlock = value;
  }

  public EventRequirementData SsRequirementUnlock2
  {
    get => this.ssRequirementUnlock2;
    set => this.ssRequirementUnlock2 = value;
  }

  public EventRequirementData SsRequirementLock
  {
    get => this.ssRequirementLock;
    set => this.ssRequirementLock = value;
  }

  public EventRequirementData SsRequirementLock2
  {
    get => this.ssRequirementLock2;
    set => this.ssRequirementLock2 = value;
  }

  public CardData SsAddCard1
  {
    get => this.ssAddCard1;
    set => this.ssAddCard1 = value;
  }

  public CardData SsAddCard2
  {
    get => this.ssAddCard2;
    set => this.ssAddCard2 = value;
  }

  public CardData SsAddCard3
  {
    get => this.ssAddCard3;
    set => this.ssAddCard3 = value;
  }

  public CombatData SsCombat
  {
    get => this.ssCombat;
    set => this.ssCombat = value;
  }

  public EventData SsEvent
  {
    get => this.ssEvent;
    set => this.ssEvent = value;
  }

  public TierRewardData SsRewardTier
  {
    get => this.ssRewardTier;
    set => this.ssRewardTier = value;
  }

  public int SsDiscount
  {
    get => this.ssDiscount;
    set => this.ssDiscount = value;
  }

  public bool SsHealerUI
  {
    get => this.ssHealerUI;
    set => this.ssHealerUI = value;
  }

  public int SsMaxQuantity
  {
    get => this.ssMaxQuantity;
    set => this.ssMaxQuantity = value;
  }

  public bool SsUpgradeUI
  {
    get => this.ssUpgradeUI;
    set => this.ssUpgradeUI = value;
  }

  public bool SsMerchantUI
  {
    get => this.ssMerchantUI;
    set => this.ssMerchantUI = value;
  }

  public Enums.EventAction ReplyActionText
  {
    get => this.replyActionText;
    set => this.replyActionText = value;
  }

  public string FlRewardText
  {
    get => this.flRewardText;
    set => this.flRewardText = value;
  }

  public float FlRewardHealthPercent
  {
    get => this.flRewardHealthPercent;
    set => this.flRewardHealthPercent = value;
  }

  public int FlRewardHealthFlat
  {
    get => this.flRewardHealthFlat;
    set => this.flRewardHealthFlat = value;
  }

  public int FlGoldReward
  {
    get => this.flGoldReward;
    set => this.flGoldReward = value;
  }

  public int FlDustReward
  {
    get => this.flDustReward;
    set => this.flDustReward = value;
  }

  public int FlExperienceReward
  {
    get => this.flExperienceReward;
    set => this.flExperienceReward = value;
  }

  public CombatData FlCombat
  {
    get => this.flCombat;
    set => this.flCombat = value;
  }

  public EventData FlEvent
  {
    get => this.flEvent;
    set => this.flEvent = value;
  }

  public EventRequirementData FlRequirementUnlock
  {
    get => this.flRequirementUnlock;
    set => this.flRequirementUnlock = value;
  }

  public EventRequirementData FlRequirementUnlock2
  {
    get => this.flRequirementUnlock2;
    set => this.flRequirementUnlock2 = value;
  }

  public EventRequirementData FlRequirementLock
  {
    get => this.flRequirementLock;
    set => this.flRequirementLock = value;
  }

  public CardData FlAddCard1
  {
    get => this.flAddCard1;
    set => this.flAddCard1 = value;
  }

  public CardData FlAddCard2
  {
    get => this.flAddCard2;
    set => this.flAddCard2 = value;
  }

  public CardData FlAddCard3
  {
    get => this.flAddCard3;
    set => this.flAddCard3 = value;
  }

  public TierRewardData FlRewardTier
  {
    get => this.flRewardTier;
    set => this.flRewardTier = value;
  }

  public int FlDiscount
  {
    get => this.flDiscount;
    set => this.flDiscount = value;
  }

  public bool FlHealerUI
  {
    get => this.flHealerUI;
    set => this.flHealerUI = value;
  }

  public int FlMaxQuantity
  {
    get => this.flMaxQuantity;
    set => this.flMaxQuantity = value;
  }

  public bool FlUpgradeUI
  {
    get => this.flUpgradeUI;
    set => this.flUpgradeUI = value;
  }

  public bool FlMerchantUI
  {
    get => this.flMerchantUI;
    set => this.flMerchantUI = value;
  }

  public bool RequirementMultiplayer
  {
    get => this.requirementMultiplayer;
    set => this.requirementMultiplayer = value;
  }

  public NodeData SsNodeTravel
  {
    get => this.ssNodeTravel;
    set => this.ssNodeTravel = value;
  }

  public NodeData FlNodeTravel
  {
    get => this.flNodeTravel;
    set => this.flNodeTravel = value;
  }

  public SubClassData FlUnlockClass
  {
    get => this.flUnlockClass;
    set => this.flUnlockClass = value;
  }

  public SubClassData SsUnlockClass
  {
    get => this.ssUnlockClass;
    set => this.ssUnlockClass = value;
  }

  public bool SsFinishGame
  {
    get => this.ssFinishGame;
    set => this.ssFinishGame = value;
  }

  public LootData SsShopList
  {
    get => this.ssShopList;
    set => this.ssShopList = value;
  }

  public LootData FlShopList
  {
    get => this.flShopList;
    set => this.flShopList = value;
  }

  public LootData SsLootList
  {
    get => this.ssLootList;
    set => this.ssLootList = value;
  }

  public LootData FlLootList
  {
    get => this.flLootList;
    set => this.flLootList = value;
  }

  public bool SsCraftUI
  {
    get => this.ssCraftUI;
    set => this.ssCraftUI = value;
  }

  public bool FlCraftUI
  {
    get => this.flCraftUI;
    set => this.flCraftUI = value;
  }

  public bool SsUpgradeRandomCard
  {
    get => this.ssUpgradeRandomCard;
    set => this.ssUpgradeRandomCard = value;
  }

  public bool FlUpgradeRandomCard
  {
    get => this.flUpgradeRandomCard;
    set => this.flUpgradeRandomCard = value;
  }

  public CardData FlAddItem
  {
    get => this.flAddItem;
    set => this.flAddItem = value;
  }

  public CardData SsAddItem
  {
    get => this.ssAddItem;
    set => this.ssAddItem = value;
  }

  public int SsSupplyReward
  {
    get => this.ssSupplyReward;
    set => this.ssSupplyReward = value;
  }

  public int FlSupplyReward
  {
    get => this.flSupplyReward;
    set => this.flSupplyReward = value;
  }

  public bool SsFinishEarlyAccess
  {
    get => this.ssFinishEarlyAccess;
    set => this.ssFinishEarlyAccess = value;
  }

  public string FlUnlockSteamAchievement
  {
    get => this.flUnlockSteamAchievement;
    set => this.flUnlockSteamAchievement = value;
  }

  public string SsUnlockSteamAchievement
  {
    get => this.ssUnlockSteamAchievement;
    set => this.ssUnlockSteamAchievement = value;
  }

  public int SsRollNumberCritical
  {
    get => this.ssRollNumberCritical;
    set => this.ssRollNumberCritical = value;
  }

  public int SsRollNumberCriticalFail
  {
    get => this.ssRollNumberCriticalFail;
    set => this.ssRollNumberCriticalFail = value;
  }

  public NodeData SscNodeTravel
  {
    get => this.sscNodeTravel;
    set => this.sscNodeTravel = value;
  }

  public string SscRewardText
  {
    get => this.sscRewardText;
    set => this.sscRewardText = value;
  }

  public float SscRewardHealthPercent
  {
    get => this.sscRewardHealthPercent;
    set => this.sscRewardHealthPercent = value;
  }

  public int SscRewardHealthFlat
  {
    get => this.sscRewardHealthFlat;
    set => this.sscRewardHealthFlat = value;
  }

  public int SscGoldReward
  {
    get => this.sscGoldReward;
    set => this.sscGoldReward = value;
  }

  public int SscDustReward
  {
    get => this.sscDustReward;
    set => this.sscDustReward = value;
  }

  public int SscSupplyReward
  {
    get => this.sscSupplyReward;
    set => this.sscSupplyReward = value;
  }

  public int SscExperienceReward
  {
    get => this.sscExperienceReward;
    set => this.sscExperienceReward = value;
  }

  public EventRequirementData SscRequirementUnlock
  {
    get => this.sscRequirementUnlock;
    set => this.sscRequirementUnlock = value;
  }

  public EventRequirementData SscRequirementUnlock2
  {
    get => this.sscRequirementUnlock2;
    set => this.sscRequirementUnlock2 = value;
  }

  public EventRequirementData SscRequirementLock
  {
    get => this.sscRequirementLock;
    set => this.sscRequirementLock = value;
  }

  public CardData SscAddCard1
  {
    get => this.sscAddCard1;
    set => this.sscAddCard1 = value;
  }

  public CardData SscAddCard2
  {
    get => this.sscAddCard2;
    set => this.sscAddCard2 = value;
  }

  public CardData SscAddCard3
  {
    get => this.sscAddCard3;
    set => this.sscAddCard3 = value;
  }

  public CombatData SscCombat
  {
    get => this.sscCombat;
    set => this.sscCombat = value;
  }

  public EventData SscEvent
  {
    get => this.sscEvent;
    set => this.sscEvent = value;
  }

  public TierRewardData SscRewardTier
  {
    get => this.sscRewardTier;
    set => this.sscRewardTier = value;
  }

  public int SscDiscount
  {
    get => this.sscDiscount;
    set => this.sscDiscount = value;
  }

  public int SscMaxQuantity
  {
    get => this.sscMaxQuantity;
    set => this.sscMaxQuantity = value;
  }

  public bool SscHealerUI
  {
    get => this.sscHealerUI;
    set => this.sscHealerUI = value;
  }

  public bool SscUpgradeUI
  {
    get => this.sscUpgradeUI;
    set => this.sscUpgradeUI = value;
  }

  public bool SscCraftUI
  {
    get => this.sscCraftUI;
    set => this.sscCraftUI = value;
  }

  public bool SscMerchantUI
  {
    get => this.sscMerchantUI;
    set => this.sscMerchantUI = value;
  }

  public LootData SscShopList
  {
    get => this.sscShopList;
    set => this.sscShopList = value;
  }

  public LootData SscLootList
  {
    get => this.sscLootList;
    set => this.sscLootList = value;
  }

  public SubClassData SscUnlockClass
  {
    get => this.sscUnlockClass;
    set => this.sscUnlockClass = value;
  }

  public bool SscUpgradeRandomCard
  {
    get => this.sscUpgradeRandomCard;
    set => this.sscUpgradeRandomCard = value;
  }

  public CardData SscAddItem
  {
    get => this.sscAddItem;
    set => this.sscAddItem = value;
  }

  public bool SscFinishGame
  {
    get => this.sscFinishGame;
    set => this.sscFinishGame = value;
  }

  public bool SscFinishEarlyAccess
  {
    get => this.sscFinishEarlyAccess;
    set => this.sscFinishEarlyAccess = value;
  }

  public bool SsFinishObeliskMap
  {
    get => this.ssFinishObeliskMap;
    set => this.ssFinishObeliskMap = value;
  }

  public string SscUnlockSteamAchievement
  {
    get => this.sscUnlockSteamAchievement;
    set => this.sscUnlockSteamAchievement = value;
  }

  public NodeData FlcNodeTravel
  {
    get => this.flcNodeTravel;
    set => this.flcNodeTravel = value;
  }

  public string FlcRewardText
  {
    get => this.flcRewardText;
    set => this.flcRewardText = value;
  }

  public float FlcRewardHealthPercent
  {
    get => this.flcRewardHealthPercent;
    set => this.flcRewardHealthPercent = value;
  }

  public int FlcRewardHealthFlat
  {
    get => this.flcRewardHealthFlat;
    set => this.flcRewardHealthFlat = value;
  }

  public int FlcGoldReward
  {
    get => this.flcGoldReward;
    set => this.flcGoldReward = value;
  }

  public int FlcDustReward
  {
    get => this.flcDustReward;
    set => this.flcDustReward = value;
  }

  public int FlcSupplyReward
  {
    get => this.flcSupplyReward;
    set => this.flcSupplyReward = value;
  }

  public int FlcExperienceReward
  {
    get => this.flcExperienceReward;
    set => this.flcExperienceReward = value;
  }

  public EventRequirementData FlcRequirementUnlock
  {
    get => this.flcRequirementUnlock;
    set => this.flcRequirementUnlock = value;
  }

  public EventRequirementData FlcRequirementUnlock2
  {
    get => this.flcRequirementUnlock2;
    set => this.flcRequirementUnlock2 = value;
  }

  public EventRequirementData FlcRequirementLock
  {
    get => this.flcRequirementLock;
    set => this.flcRequirementLock = value;
  }

  public CardData FlcAddCard1
  {
    get => this.flcAddCard1;
    set => this.flcAddCard1 = value;
  }

  public CardData FlcAddCard2
  {
    get => this.flcAddCard2;
    set => this.flcAddCard2 = value;
  }

  public CardData FlcAddCard3
  {
    get => this.flcAddCard3;
    set => this.flcAddCard3 = value;
  }

  public CombatData FlcCombat
  {
    get => this.flcCombat;
    set => this.flcCombat = value;
  }

  public EventData FlcEvent
  {
    get => this.flcEvent;
    set => this.flcEvent = value;
  }

  public TierRewardData FlcRewardTier
  {
    get => this.flcRewardTier;
    set => this.flcRewardTier = value;
  }

  public int FlcDiscount
  {
    get => this.flcDiscount;
    set => this.flcDiscount = value;
  }

  public bool FlcHealerUI
  {
    get => this.flcHealerUI;
    set => this.flcHealerUI = value;
  }

  public int FlcMaxQuantity
  {
    get => this.flcMaxQuantity;
    set => this.flcMaxQuantity = value;
  }

  public bool FlcUpgradeUI
  {
    get => this.flcUpgradeUI;
    set => this.flcUpgradeUI = value;
  }

  public bool FlcCraftUI
  {
    get => this.flcCraftUI;
    set => this.flcCraftUI = value;
  }

  public bool FlcMerchantUI
  {
    get => this.flcMerchantUI;
    set => this.flcMerchantUI = value;
  }

  public LootData FlcShopList
  {
    get => this.flcShopList;
    set => this.flcShopList = value;
  }

  public LootData FlcLootList
  {
    get => this.flcLootList;
    set => this.flcLootList = value;
  }

  public bool FlcUpgradeRandomCard
  {
    get => this.flcUpgradeRandomCard;
    set => this.flcUpgradeRandomCard = value;
  }

  public CardData FlcAddItem
  {
    get => this.flcAddItem;
    set => this.flcAddItem = value;
  }

  public SubClassData FlcUnlockClass
  {
    get => this.flcUnlockClass;
    set => this.flcUnlockClass = value;
  }

  public string FlcUnlockSteamAchievement
  {
    get => this.flcUnlockSteamAchievement;
    set => this.flcUnlockSteamAchievement = value;
  }

  public Enums.CardType SsRollCard
  {
    get => this.ssRollCard;
    set => this.ssRollCard = value;
  }

  public CardData ReplyShowCard
  {
    get => this.replyShowCard;
    set => this.replyShowCard = value;
  }

  public CardData RequirementItem
  {
    get => this.requirementItem;
    set => this.requirementItem = value;
  }

  public bool RepeatForAllCharacters
  {
    get => this.repeatForAllCharacters;
    set => this.repeatForAllCharacters = value;
  }

  public int DustCost
  {
    get => this.dustCost;
    set => this.dustCost = value;
  }

  public PerkData SsPerkData
  {
    get => this.ssPerkData;
    set => this.ssPerkData = value;
  }

  public PerkData SsPerkData1
  {
    get => this.ssPerkData1;
    set => this.ssPerkData1 = value;
  }

  public Enums.CardRarity FlcCraftUIMaxType
  {
    get => this.flcCraftUIMaxType;
    set => this.flcCraftUIMaxType = value;
  }

  public Enums.CardRarity SscCraftUIMaxType
  {
    get => this.sscCraftUIMaxType;
    set => this.sscCraftUIMaxType = value;
  }

  public Enums.CardRarity FlCraftUIMaxType
  {
    get => this.flCraftUIMaxType;
    set => this.flCraftUIMaxType = value;
  }

  public Enums.CardRarity SsCraftUIMaxType
  {
    get => this.ssCraftUIMaxType;
    set => this.ssCraftUIMaxType = value;
  }

  public bool SsCardPlayerGame
  {
    get => this.ssCardPlayerGame;
    set => this.ssCardPlayerGame = value;
  }

  public CardPlayerPackData SsCardPlayerGamePackData
  {
    get => this.ssCardPlayerGamePackData;
    set => this.ssCardPlayerGamePackData = value;
  }

  public bool FlCardPlayerGame
  {
    get => this.flCardPlayerGame;
    set => this.flCardPlayerGame = value;
  }

  public CardPlayerPackData FlCardPlayerGamePackData
  {
    get => this.flCardPlayerGamePackData;
    set => this.flCardPlayerGamePackData = value;
  }

  public bool SscCardPlayerGame
  {
    get => this.sscCardPlayerGame;
    set => this.sscCardPlayerGame = value;
  }

  public CardPlayerPackData SscCardPlayerGamePackData
  {
    get => this.sscCardPlayerGamePackData;
    set => this.sscCardPlayerGamePackData = value;
  }

  public bool FlcCardPlayerGame
  {
    get => this.flcCardPlayerGame;
    set => this.flcCardPlayerGame = value;
  }

  public CardPlayerPackData FlcCardPlayerGamePackData
  {
    get => this.flcCardPlayerGamePackData;
    set => this.flcCardPlayerGamePackData = value;
  }

  public bool SsCorruptionUI
  {
    get => this.ssCorruptionUI;
    set => this.ssCorruptionUI = value;
  }

  public bool FlCorruptionUI
  {
    get => this.flCorruptionUI;
    set => this.flCorruptionUI = value;
  }

  public bool SscCorruptionUI
  {
    get => this.sscCorruptionUI;
    set => this.sscCorruptionUI = value;
  }

  public bool FlcCorruptionUI
  {
    get => this.flcCorruptionUI;
    set => this.flcCorruptionUI = value;
  }

  public int IndexForAnswerTranslation
  {
    get => this.indexForAnswerTranslation;
    set => this.indexForAnswerTranslation = value;
  }

  public Enums.ItemSlot SsRemoveItemSlot
  {
    get => this.ssRemoveItemSlot;
    set => this.ssRemoveItemSlot = value;
  }

  public Enums.ItemSlot SsCorruptItemSlot
  {
    get => this.ssCorruptItemSlot;
    set => this.ssCorruptItemSlot = value;
  }

  public Enums.ItemSlot SscRemoveItemSlot
  {
    get => this.sscRemoveItemSlot;
    set => this.sscRemoveItemSlot = value;
  }

  public Enums.ItemSlot SscCorruptItemSlot
  {
    get => this.sscCorruptItemSlot;
    set => this.sscCorruptItemSlot = value;
  }

  public Enums.ItemSlot FlRemoveItemSlot
  {
    get => this.flRemoveItemSlot;
    set => this.flRemoveItemSlot = value;
  }

  public Enums.ItemSlot FlCorruptItemSlot
  {
    get => this.flCorruptItemSlot;
    set => this.flCorruptItemSlot = value;
  }

  public Enums.ItemSlot FlcRemoveItemSlot
  {
    get => this.flcRemoveItemSlot;
    set => this.flcRemoveItemSlot = value;
  }

  public Enums.ItemSlot FlcCorruptItemSlot
  {
    get => this.flcCorruptItemSlot;
    set => this.flcCorruptItemSlot = value;
  }

  public string SsSteamStat
  {
    get => this.ssSteamStat;
    set => this.ssSteamStat = value;
  }

  public SubClassData SsCharacterReplacement
  {
    get => this.ssCharacterReplacement;
    set => this.ssCharacterReplacement = value;
  }

  public int SsCharacterReplacementPosition
  {
    get => this.ssCharacterReplacementPosition;
    set => this.ssCharacterReplacementPosition = value;
  }

  public SkinData SsUnlockSkin
  {
    get => this.ssUnlockSkin;
    set => this.ssUnlockSkin = value;
  }

  public string RequirementSku
  {
    get => this.requirementSku;
    set => this.requirementSku = value;
  }

  public List<CardData> RequirementCard
  {
    get => this.requirementCard;
    set => this.requirementCard = value;
  }

  public bool FlCardPlayerPairsGame
  {
    get => this.flCardPlayerPairsGame;
    set => this.flCardPlayerPairsGame = value;
  }

  public CardPlayerPairsPackData FlCardPlayerPairsGamePackData
  {
    get => this.flCardPlayerPairsGamePackData;
    set => this.flCardPlayerPairsGamePackData = value;
  }

  public bool FlcCardPlayerPairsGame
  {
    get => this.flcCardPlayerPairsGame;
    set => this.flcCardPlayerPairsGame = value;
  }

  public CardPlayerPairsPackData FlcCardPlayerPairsGamePackData
  {
    get => this.flcCardPlayerPairsGamePackData;
    set => this.flcCardPlayerPairsGamePackData = value;
  }

  public bool SsCardPlayerPairsGame
  {
    get => this.ssCardPlayerPairsGame;
    set => this.ssCardPlayerPairsGame = value;
  }

  public CardPlayerPairsPackData SsCardPlayerPairsGamePackData
  {
    get => this.ssCardPlayerPairsGamePackData;
    set => this.ssCardPlayerPairsGamePackData = value;
  }

  public bool SscCardPlayerPairsGame
  {
    get => this.sscCardPlayerPairsGame;
    set => this.sscCardPlayerPairsGame = value;
  }

  public CardPlayerPairsPackData SscCardPlayerPairsGamePackData
  {
    get => this.sscCardPlayerPairsGamePackData;
    set => this.sscCardPlayerPairsGamePackData = value;
  }
}
