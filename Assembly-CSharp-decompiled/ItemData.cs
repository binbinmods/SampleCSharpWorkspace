// Decompiled with JetBrains decompiler
// Type: ItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item Data", order = 57)]
public class ItemData : ScriptableObject
{
  [SerializeField]
  private bool dropOnly;
  [SerializeField]
  private bool questItem;
  [SerializeField]
  private string id = "";
  [SerializeField]
  private bool cursedItem;
  [SerializeField]
  private Enums.EventActivation activation;
  [SerializeField]
  private bool activationOnlyOnHeroes;
  [SerializeField]
  private Sprite spriteBossDrop;
  [SerializeField]
  private bool isEnchantment;
  [Header("REQUISITE")]
  [SerializeField]
  private int exactRound;
  [SerializeField]
  private int roundCycle;
  [SerializeField]
  private int timesPerTurn;
  [SerializeField]
  private int timesPerCombat;
  [SerializeField]
  private AuraCurseData auraCurseSetted;
  [SerializeField]
  private int auraCurseNumForOneEvent;
  [SerializeField]
  private Enums.CardType castedCardType;
  [SerializeField]
  private bool emptyHand;
  [SerializeField]
  private bool usedEnergy;
  [SerializeField]
  private float lowerOrEqualPercentHP = 100f;
  [Header("Not show character bonus for aura/curse gain")]
  [SerializeField]
  private bool notShowCharacterBonus;
  [Header("Destroy after being used")]
  [SerializeField]
  private bool destroyAfterUse;
  [Header("Target for the weapon effects")]
  [SerializeField]
  private Enums.ItemTarget itemTarget;
  [Header("Draw Cards")]
  [SerializeField]
  private int drawCards;
  [SerializeField]
  private bool drawMultiplyByEnergyUsed;
  [Header("Gain Card ID")]
  [SerializeField]
  private int cardNum;
  [SerializeField]
  private CardData cardToGain;
  [SerializeField]
  private List<CardData> cardToGainList;
  [SerializeField]
  private Enums.CardType cardToGainType;
  [SerializeField]
  private Enums.CardPlace cardPlace;
  [SerializeField]
  private bool costZero;
  [SerializeField]
  private int costReduction;
  [SerializeField]
  private bool vanish;
  [SerializeField]
  private bool permanent;
  [SerializeField]
  private bool duplicateActive;
  [Header("Event")]
  [SerializeField]
  private bool passSingleAndCharacterRolls;
  [Header("Corruption")]
  [SerializeField]
  private bool onlyAddItemToNPCs;
  [Header("Reduce Card Cost")]
  [SerializeField]
  private int cardsReduced;
  [SerializeField]
  private Enums.CardType cardToReduceType;
  [SerializeField]
  private int costReduceReduction;
  [SerializeField]
  private int costReduceEnergyRequirement;
  [SerializeField]
  private bool costReducePermanent;
  [SerializeField]
  private bool reduceHighestCost;
  [Header("Stat modification for wielder (all combat)")]
  [SerializeField]
  private int maxHealth;
  [SerializeField]
  private Enums.CharacterStat characterStatModified;
  [SerializeField]
  private int characterStatModifiedValue;
  [SerializeField]
  private Enums.CharacterStat characterStatModified2;
  [SerializeField]
  private int characterStatModifiedValue2;
  [SerializeField]
  private Enums.CharacterStat characterStatModified3;
  [SerializeField]
  private int characterStatModifiedValue3;
  [Header("Heal bonus for wielder")]
  [SerializeField]
  private int healFlatBonus;
  [SerializeField]
  private float healPercentBonus;
  [SerializeField]
  private int healReceivedFlatBonus;
  [SerializeField]
  private float healReceivedPercentBonus;
  [Header("Damage value bonus for wielder (all combat)")]
  [SerializeField]
  private Enums.DamageType damageFlatBonus;
  [SerializeField]
  private int damageFlatBonusValue;
  [SerializeField]
  private Enums.DamageType damageFlatBonus2;
  [SerializeField]
  private int damageFlatBonusValue2;
  [SerializeField]
  private Enums.DamageType damageFlatBonus3;
  [SerializeField]
  private int damageFlatBonusValue3;
  [SerializeField]
  private Enums.DamageType damagePercentBonus;
  [SerializeField]
  private float damagePercentBonusValue;
  [SerializeField]
  private Enums.DamageType damagePercentBonus2;
  [SerializeField]
  private float damagePercentBonusValue2;
  [SerializeField]
  private Enums.DamageType damagePercentBonus3;
  [SerializeField]
  private float damagePercentBonusValue3;
  [Header("Heal for target")]
  [SerializeField]
  private int healQuantity;
  [SerializeField]
  private int healPercentQuantity;
  [SerializeField]
  private int healPercentQuantitySelf;
  [Header("Gain Energy for target")]
  [SerializeField]
  private int energyQuantity;
  [Header("AuraCurse bonus for target")]
  [SerializeField]
  private AuraCurseData auracurseBonus1;
  [SerializeField]
  private int auracurseBonusValue1;
  [SerializeField]
  private AuraCurseData auracurseBonus2;
  [SerializeField]
  private int auracurseBonusValue2;
  [Header("AuraCurse immunity for wielder")]
  [SerializeField]
  private AuraCurseData auracurseImmune1;
  [SerializeField]
  private AuraCurseData auracurseImmune2;
  [Header("Resist modification for target")]
  [SerializeField]
  private Enums.DamageType resistModified1;
  [SerializeField]
  private int resistModifiedValue1;
  [SerializeField]
  private Enums.DamageType resistModified2;
  [SerializeField]
  private int resistModifiedValue2;
  [SerializeField]
  private Enums.DamageType resistModified3;
  [SerializeField]
  private int resistModifiedValue3;
  [Header("AuraCurse gained on target")]
  [SerializeField]
  private AuraCurseData auracurseGain1;
  [SerializeField]
  private int auracurseGainValue1;
  [SerializeField]
  private bool acg1MultiplyByEnergyUsed;
  [SerializeField]
  private AuraCurseData auracurseGain2;
  [SerializeField]
  private int auracurseGainValue2;
  [SerializeField]
  private bool acg2MultiplyByEnergyUsed;
  [SerializeField]
  private AuraCurseData auracurseGain3;
  [SerializeField]
  private int auracurseGainValue3;
  [SerializeField]
  private bool acg3MultiplyByEnergyUsed;
  [Header("AuraCurse gained on self")]
  [SerializeField]
  private AuraCurseData auracurseGainSelf1;
  [SerializeField]
  private int auracurseGainSelfValue1;
  [SerializeField]
  private AuraCurseData auracurseGainSelf2;
  [SerializeField]
  private int auracurseGainSelfValue2;
  [Header("Chance to dispel")]
  [SerializeField]
  private int chanceToDispel;
  [SerializeField]
  private int chanceToDispelNum;
  [Header("Rewards and discounts")]
  [SerializeField]
  private int percentRetentionEndGame;
  [SerializeField]
  private int percentDiscountShop;
  [Header("ENCHANT DATA")]
  [SerializeField]
  private bool useTheNextInsteadWhenYouPlay;
  [SerializeField]
  private int destroyAfterUses;
  [SerializeField]
  private bool destroyStartOfTurn;
  [SerializeField]
  private bool destroyEndOfTurn;
  [SerializeField]
  [Tooltip("This must be activated for enchantments that need to be activated on self cast")]
  private bool castEnchantmentOnFinishSelfCast;
  [SerializeField]
  private Enums.DamageType modifiedDamageType;
  [SerializeField]
  private int damageToTarget;
  [SerializeField]
  private bool dttMultiplyByEnergyUsed;
  [SerializeField]
  private Enums.DamageType damageToTargetType;
  [Header("GRAPHICAL & SOUND EFFECTS")]
  [SerializeField]
  private string effectItemOwner = "";
  [SerializeField]
  private string effectCaster = "";
  [SerializeField]
  private float effectCasterDelay;
  [SerializeField]
  private string effectTarget = "";
  [SerializeField]
  private float effectTargetDelay;
  [SerializeField]
  private AudioClip itemSound;
  [Header("CUSTOM for AuraCurse modifications")]
  [SerializeField]
  private string auracurseCustomString;
  [SerializeField]
  private AuraCurseData auracurseCustomAC;
  [SerializeField]
  private int auracurseCustomModValue1;
  [SerializeField]
  private int auracurseCustomModValue2;

  public bool DropOnly
  {
    get => this.dropOnly;
    set => this.dropOnly = value;
  }

  public Enums.EventActivation Activation
  {
    get => this.activation;
    set => this.activation = value;
  }

  public int RoundCycle
  {
    get => this.roundCycle;
    set => this.roundCycle = value;
  }

  public Enums.ItemTarget ItemTarget
  {
    get => this.itemTarget;
    set => this.itemTarget = value;
  }

  public Enums.CharacterStat CharacterStatModified
  {
    get => this.characterStatModified;
    set => this.characterStatModified = value;
  }

  public int CharacterStatModifiedValue
  {
    get => this.characterStatModifiedValue;
    set => this.characterStatModifiedValue = value;
  }

  public Enums.CharacterStat CharacterStatModified2
  {
    get => this.characterStatModified2;
    set => this.characterStatModified2 = value;
  }

  public int CharacterStatModifiedValue2
  {
    get => this.characterStatModifiedValue2;
    set => this.characterStatModifiedValue2 = value;
  }

  public Enums.CharacterStat CharacterStatModified3
  {
    get => this.characterStatModified3;
    set => this.characterStatModified3 = value;
  }

  public int CharacterStatModifiedValue3
  {
    get => this.characterStatModifiedValue3;
    set => this.characterStatModifiedValue3 = value;
  }

  public int HealFlatBonus
  {
    get => this.healFlatBonus;
    set => this.healFlatBonus = value;
  }

  public float HealPercentBonus
  {
    get => this.healPercentBonus;
    set => this.healPercentBonus = value;
  }

  public Enums.DamageType DamageFlatBonus
  {
    get => this.damageFlatBonus;
    set => this.damageFlatBonus = value;
  }

  public int DamageFlatBonusValue
  {
    get => this.damageFlatBonusValue;
    set => this.damageFlatBonusValue = value;
  }

  public Enums.DamageType DamageFlatBonus2
  {
    get => this.damageFlatBonus2;
    set => this.damageFlatBonus2 = value;
  }

  public int DamageFlatBonusValue2
  {
    get => this.damageFlatBonusValue2;
    set => this.damageFlatBonusValue2 = value;
  }

  public Enums.DamageType DamageFlatBonus3
  {
    get => this.damageFlatBonus3;
    set => this.damageFlatBonus3 = value;
  }

  public int DamageFlatBonusValue3
  {
    get => this.damageFlatBonusValue3;
    set => this.damageFlatBonusValue3 = value;
  }

  public Enums.DamageType DamagePercentBonus
  {
    get => this.damagePercentBonus;
    set => this.damagePercentBonus = value;
  }

  public float DamagePercentBonusValue
  {
    get => this.damagePercentBonusValue;
    set => this.damagePercentBonusValue = value;
  }

  public Enums.DamageType DamagePercentBonus2
  {
    get => this.damagePercentBonus2;
    set => this.damagePercentBonus2 = value;
  }

  public float DamagePercentBonusValue2
  {
    get => this.damagePercentBonusValue2;
    set => this.damagePercentBonusValue2 = value;
  }

  public Enums.DamageType DamagePercentBonus3
  {
    get => this.damagePercentBonus3;
    set => this.damagePercentBonus3 = value;
  }

  public float DamagePercentBonusValue3
  {
    get => this.damagePercentBonusValue3;
    set => this.damagePercentBonusValue3 = value;
  }

  public AuraCurseData AuracurseBonus1
  {
    get => this.auracurseBonus1;
    set => this.auracurseBonus1 = value;
  }

  public int AuracurseBonusValue1
  {
    get => this.auracurseBonusValue1;
    set => this.auracurseBonusValue1 = value;
  }

  public AuraCurseData AuracurseBonus2
  {
    get => this.auracurseBonus2;
    set => this.auracurseBonus2 = value;
  }

  public int AuracurseBonusValue2
  {
    get => this.auracurseBonusValue2;
    set => this.auracurseBonusValue2 = value;
  }

  public AuraCurseData AuracurseImmune1
  {
    get => this.auracurseImmune1;
    set => this.auracurseImmune1 = value;
  }

  public AuraCurseData AuracurseImmune2
  {
    get => this.auracurseImmune2;
    set => this.auracurseImmune2 = value;
  }

  public Enums.DamageType ResistModified1
  {
    get => this.resistModified1;
    set => this.resistModified1 = value;
  }

  public int ResistModifiedValue1
  {
    get => this.resistModifiedValue1;
    set => this.resistModifiedValue1 = value;
  }

  public Enums.DamageType ResistModified2
  {
    get => this.resistModified2;
    set => this.resistModified2 = value;
  }

  public int ResistModifiedValue2
  {
    get => this.resistModifiedValue2;
    set => this.resistModifiedValue2 = value;
  }

  public Enums.DamageType ResistModified3
  {
    get => this.resistModified3;
    set => this.resistModified3 = value;
  }

  public int ResistModifiedValue3
  {
    get => this.resistModifiedValue3;
    set => this.resistModifiedValue3 = value;
  }

  public AuraCurseData AuracurseGain1
  {
    get => this.auracurseGain1;
    set => this.auracurseGain1 = value;
  }

  public int AuracurseGainValue1
  {
    get => this.auracurseGainValue1;
    set => this.auracurseGainValue1 = value;
  }

  public AuraCurseData AuracurseGain2
  {
    get => this.auracurseGain2;
    set => this.auracurseGain2 = value;
  }

  public int AuracurseGainValue2
  {
    get => this.auracurseGainValue2;
    set => this.auracurseGainValue2 = value;
  }

  public int HealQuantity
  {
    get => this.healQuantity;
    set => this.healQuantity = value;
  }

  public AuraCurseData AuraCurseSetted
  {
    get => this.auraCurseSetted;
    set => this.auraCurseSetted = value;
  }

  public int AuraCurseNumForOneEvent
  {
    get => this.auraCurseNumForOneEvent;
    set => this.auraCurseNumForOneEvent = value;
  }

  public Enums.CardType CastedCardType
  {
    get => this.castedCardType;
    set => this.castedCardType = value;
  }

  public int ExactRound
  {
    get => this.exactRound;
    set => this.exactRound = value;
  }

  public int EnergyQuantity
  {
    get => this.energyQuantity;
    set => this.energyQuantity = value;
  }

  public int TimesPerTurn
  {
    get => this.timesPerTurn;
    set => this.timesPerTurn = value;
  }

  public int DrawCards
  {
    get => this.drawCards;
    set => this.drawCards = value;
  }

  public int MaxHealth
  {
    get => this.maxHealth;
    set => this.maxHealth = value;
  }

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }

  public int PercentRetentionEndGame
  {
    get => this.percentRetentionEndGame;
    set => this.percentRetentionEndGame = value;
  }

  public int PercentDiscountShop
  {
    get => this.percentDiscountShop;
    set => this.percentDiscountShop = value;
  }

  public bool CursedItem
  {
    get => this.cursedItem;
    set => this.cursedItem = value;
  }

  public AuraCurseData AuracurseGainSelf1
  {
    get => this.auracurseGainSelf1;
    set => this.auracurseGainSelf1 = value;
  }

  public int AuracurseGainSelfValue1
  {
    get => this.auracurseGainSelfValue1;
    set => this.auracurseGainSelfValue1 = value;
  }

  public AuraCurseData AuracurseGainSelf2
  {
    get => this.auracurseGainSelf2;
    set => this.auracurseGainSelf2 = value;
  }

  public int AuracurseGainSelfValue2
  {
    get => this.auracurseGainSelfValue2;
    set => this.auracurseGainSelfValue2 = value;
  }

  public int CardNum
  {
    get => this.cardNum;
    set => this.cardNum = value;
  }

  public CardData CardToGain
  {
    get => this.cardToGain;
    set => this.cardToGain = value;
  }

  public Enums.CardPlace CardPlace
  {
    get => this.cardPlace;
    set => this.cardPlace = value;
  }

  public bool CostZero
  {
    get => this.costZero;
    set => this.costZero = value;
  }

  public int CostReduction
  {
    get => this.costReduction;
    set => this.costReduction = value;
  }

  public bool Vanish
  {
    get => this.vanish;
    set => this.vanish = value;
  }

  public bool Permanent
  {
    get => this.permanent;
    set => this.permanent = value;
  }

  public Enums.CardType CardToGainType
  {
    get => this.cardToGainType;
    set => this.cardToGainType = value;
  }

  public Enums.CardType CardToReduceType
  {
    get => this.cardToReduceType;
    set => this.cardToReduceType = value;
  }

  public int CardsReduced
  {
    get => this.cardsReduced;
    set => this.cardsReduced = value;
  }

  public int CostReduceReduction
  {
    get => this.costReduceReduction;
    set => this.costReduceReduction = value;
  }

  public bool CostReducePermanent
  {
    get => this.costReducePermanent;
    set => this.costReducePermanent = value;
  }

  public bool EmptyHand
  {
    get => this.emptyHand;
    set => this.emptyHand = value;
  }

  public int ChanceToDispel
  {
    get => this.chanceToDispel;
    set => this.chanceToDispel = value;
  }

  public int ChanceToDispelNum
  {
    get => this.chanceToDispelNum;
    set => this.chanceToDispelNum = value;
  }

  public bool DestroyAfterUse
  {
    get => this.destroyAfterUse;
    set => this.destroyAfterUse = value;
  }

  public int HealPercentQuantity
  {
    get => this.healPercentQuantity;
    set => this.healPercentQuantity = value;
  }

  public int HealPercentQuantitySelf
  {
    get => this.healPercentQuantitySelf;
    set => this.healPercentQuantitySelf = value;
  }

  public bool OnlyAddItemToNPCs
  {
    get => this.onlyAddItemToNPCs;
    set => this.onlyAddItemToNPCs = value;
  }

  public Sprite SpriteBossDrop
  {
    get => this.spriteBossDrop;
    set => this.spriteBossDrop = value;
  }

  public bool DuplicateActive
  {
    get => this.duplicateActive;
    set => this.duplicateActive = value;
  }

  public Enums.DamageType ModifiedDamageType
  {
    get => this.modifiedDamageType;
    set => this.modifiedDamageType = value;
  }

  public bool DestroyStartOfTurn
  {
    get => this.destroyStartOfTurn;
    set => this.destroyStartOfTurn = value;
  }

  public int DamageToTarget
  {
    get => this.damageToTarget;
    set => this.damageToTarget = value;
  }

  public Enums.DamageType DamageToTargetType
  {
    get => this.damageToTargetType;
    set => this.damageToTargetType = value;
  }

  public bool ReduceHighestCost
  {
    get => this.reduceHighestCost;
    set => this.reduceHighestCost = value;
  }

  public bool CastEnchantmentOnFinishSelfCast
  {
    get => this.castEnchantmentOnFinishSelfCast;
    set => this.castEnchantmentOnFinishSelfCast = value;
  }

  public bool QuestItem
  {
    get => this.questItem;
    set => this.questItem = value;
  }

  public string EffectCaster
  {
    get => this.effectCaster;
    set => this.effectCaster = value;
  }

  public string EffectTarget
  {
    get => this.effectTarget;
    set => this.effectTarget = value;
  }

  public string EffectItemOwner
  {
    get => this.effectItemOwner;
    set => this.effectItemOwner = value;
  }

  public int DestroyAfterUses
  {
    get => this.destroyAfterUses;
    set => this.destroyAfterUses = value;
  }

  public bool NotShowCharacterBonus
  {
    get => this.notShowCharacterBonus;
    set => this.notShowCharacterBonus = value;
  }

  public AuraCurseData AuracurseGain3
  {
    get => this.auracurseGain3;
    set => this.auracurseGain3 = value;
  }

  public int AuracurseGainValue3
  {
    get => this.auracurseGainValue3;
    set => this.auracurseGainValue3 = value;
  }

  public AudioClip ItemSound
  {
    get => this.itemSound;
    set => this.itemSound = value;
  }

  public bool Acg1MultiplyByEnergyUsed
  {
    get => this.acg1MultiplyByEnergyUsed;
    set => this.acg1MultiplyByEnergyUsed = value;
  }

  public bool Acg2MultiplyByEnergyUsed
  {
    get => this.acg2MultiplyByEnergyUsed;
    set => this.acg2MultiplyByEnergyUsed = value;
  }

  public bool Acg3MultiplyByEnergyUsed
  {
    get => this.acg3MultiplyByEnergyUsed;
    set => this.acg3MultiplyByEnergyUsed = value;
  }

  public bool DttMultiplyByEnergyUsed
  {
    get => this.dttMultiplyByEnergyUsed;
    set => this.dttMultiplyByEnergyUsed = value;
  }

  public bool DrawMultiplyByEnergyUsed
  {
    get => this.drawMultiplyByEnergyUsed;
    set => this.drawMultiplyByEnergyUsed = value;
  }

  public bool UsedEnergy
  {
    get => this.usedEnergy;
    set => this.usedEnergy = value;
  }

  public float LowerOrEqualPercentHP
  {
    get => this.lowerOrEqualPercentHP;
    set => this.lowerOrEqualPercentHP = value;
  }

  public int HealReceivedFlatBonus
  {
    get => this.healReceivedFlatBonus;
    set => this.healReceivedFlatBonus = value;
  }

  public float HealReceivedPercentBonus
  {
    get => this.healReceivedPercentBonus;
    set => this.healReceivedPercentBonus = value;
  }

  public bool IsEnchantment
  {
    get => this.isEnchantment;
    set => this.isEnchantment = value;
  }

  public bool UseTheNextInsteadWhenYouPlay
  {
    get => this.useTheNextInsteadWhenYouPlay;
    set => this.useTheNextInsteadWhenYouPlay = value;
  }

  public string AuracurseCustomString
  {
    get => this.auracurseCustomString;
    set => this.auracurseCustomString = value;
  }

  public AuraCurseData AuracurseCustomAC
  {
    get => this.auracurseCustomAC;
    set => this.auracurseCustomAC = value;
  }

  public int AuracurseCustomModValue1
  {
    get => this.auracurseCustomModValue1;
    set => this.auracurseCustomModValue1 = value;
  }

  public int AuracurseCustomModValue2
  {
    get => this.auracurseCustomModValue2;
    set => this.auracurseCustomModValue2 = value;
  }

  public bool PassSingleAndCharacterRolls
  {
    get => this.passSingleAndCharacterRolls;
    set => this.passSingleAndCharacterRolls = value;
  }

  public bool DestroyEndOfTurn
  {
    get => this.destroyEndOfTurn;
    set => this.destroyEndOfTurn = value;
  }

  public int TimesPerCombat
  {
    get => this.timesPerCombat;
    set => this.timesPerCombat = value;
  }

  public List<CardData> CardToGainList
  {
    get => this.cardToGainList;
    set => this.cardToGainList = value;
  }

  public bool ActivationOnlyOnHeroes
  {
    get => this.activationOnlyOnHeroes;
    set => this.activationOnlyOnHeroes = value;
  }

  public float EffectCasterDelay
  {
    get => this.effectCasterDelay;
    set => this.effectCasterDelay = value;
  }

  public float EffectTargetDelay
  {
    get => this.effectTargetDelay;
    set => this.effectTargetDelay = value;
  }

  public int CostReduceEnergyRequirement
  {
    get => this.costReduceEnergyRequirement;
    set => this.costReduceEnergyRequirement = value;
  }
}
