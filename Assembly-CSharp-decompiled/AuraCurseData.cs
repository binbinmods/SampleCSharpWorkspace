// Decompiled with JetBrains decompiler
// Type: AuraCurseData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName = "New AuraCurse", menuName = "AuraCurse Data", order = 56)]
public class AuraCurseData : ScriptableObject
{
  [Header("General Attributes")]
  [SerializeField]
  private string acName;
  [SerializeField]
  private string id;
  [SerializeField]
  private bool isAura;
  [SerializeField]
  private int maxCharges = -1;
  [SerializeField]
  private int maxMadnessCharges = -1;
  [SerializeField]
  private int auraConsumed = 1;
  [TextArea]
  [SerializeField]
  private string description;
  [Tooltip("Field to use for plain charges, 1/1. Introduce the value for each charge, f.e. stealth 20%/charge => 20")]
  [SerializeField]
  private int chargesMultiplierDescription = 1;
  [Tooltip("First Field to use for a 1/N format. Introduce the number of charges for one bonus, f.e. 1/3 => 3")]
  [SerializeField]
  private int chargesAuxNeedForOne1 = 1;
  [Tooltip("Second Field to use for a 1/N format. Introduce the number of charges for one bonus, f.e. 1/3 => 3")]
  [SerializeField]
  private int chargesAuxNeedForOne2 = 1;
  [SerializeField]
  private Sprite sprite;
  [SerializeField]
  private AudioClip sound;
  [SerializeField]
  private string effectTick;
  [SerializeField]
  private string effectTickSides;
  [Header("Config")]
  [SerializeField]
  private bool removable = true;
  [SerializeField]
  private bool gainCharges = true;
  [SerializeField]
  private bool iconShow = true;
  [SerializeField]
  private bool combatlogShow = true;
  [SerializeField]
  private bool preventable = true;
  [Header("Expiration")]
  [SerializeField]
  private int priorityOnConsumption;
  [SerializeField]
  private bool consumeAll;
  [SerializeField]
  private bool consumedAtCast;
  [SerializeField]
  private bool consumedAtTurnBegin;
  [SerializeField]
  private bool consumedAtTurn;
  [SerializeField]
  private bool consumedAtRoundBegin;
  [SerializeField]
  private bool consumedAtRound;
  [SerializeField]
  private bool produceDamageWhenConsumed;
  [SerializeField]
  private bool produceHealWhenConsumed;
  [SerializeField]
  private bool dieWhenConsumedAll;
  [Header("Aura Damage Bonus")]
  [SerializeField]
  private Enums.DamageType auraDamageType;
  [SerializeField]
  private AuraCurseData auraDamageChargesBasedOnACCharges;
  [SerializeField]
  private int auraDamageIncreasedTotal;
  [SerializeField]
  private float auraDamageIncreasedPerStack;
  [SerializeField]
  private int auraDamageIncreasedPercent;
  [SerializeField]
  private float auraDamageIncreasedPercentPerStack;
  [SerializeField]
  private float auraDamageIncreasedPercentPerStackPerEnergy;
  [SerializeField]
  private Enums.DamageType auraDamageType2;
  [SerializeField]
  private int auraDamageIncreasedTotal2;
  [SerializeField]
  private float auraDamageIncreasedPerStack2;
  [SerializeField]
  private int auraDamageIncreasedPercent2;
  [SerializeField]
  private float auraDamageIncreasedPercentPerStack2;
  [SerializeField]
  private float auraDamageIncreasedPercentPerStackPerEnergy2;
  [SerializeField]
  private Enums.DamageType auraDamageType3;
  [SerializeField]
  private int auraDamageIncreasedTotal3;
  [SerializeField]
  private float auraDamageIncreasedPerStack3;
  [SerializeField]
  private int auraDamageIncreasedPercent3;
  [SerializeField]
  private float auraDamageIncreasedPercentPerStack3;
  [SerializeField]
  private float auraDamageIncreasedPercentPerStackPerEnergy3;
  [SerializeField]
  private Enums.DamageType auraDamageType4;
  [SerializeField]
  private int auraDamageIncreasedTotal4;
  [SerializeField]
  private float auraDamageIncreasedPerStack4;
  [SerializeField]
  private int auraDamageIncreasedPercent4;
  [SerializeField]
  private float auraDamageIncreasedPercentPerStack4;
  [SerializeField]
  private float auraDamageIncreasedPercentPerStackPerEnergy4;
  [Header("Aura Heal Bonus")]
  [SerializeField]
  private int healDoneTotal;
  [SerializeField]
  private int healDonePerStack;
  [SerializeField]
  private int healDonePercent;
  [SerializeField]
  private int healDonePercentPerStack;
  [SerializeField]
  private int healDonePercentPerStackPerEnergy;
  [SerializeField]
  private int healReceivedTotal;
  [SerializeField]
  private int healReceivedPerStack;
  [SerializeField]
  private int healReceivedPercent;
  [SerializeField]
  private int healReceivedPercentPerStack;
  [Header("Aura Draw Bonus")]
  [SerializeField]
  private int cardsDrawPerStack;
  [Header("Aura Damage Reflected")]
  [SerializeField]
  private int damageReflectedPerStack;
  [SerializeField]
  private Enums.DamageType damageReflectedType;
  [SerializeField]
  private int damageReflectedConsumeCharges;
  [Header("Block")]
  [SerializeField]
  private int blockChargesGainedPerStack;
  [SerializeField]
  private bool noRemoveBlockAtTurnEnd;
  [Header("Prevention")]
  [SerializeField]
  private int damagePreventedPerStack;
  [SerializeField]
  private int cursePreventedPerStack;
  [SerializeField]
  private AuraCurseData preventedAuraCurse;
  [SerializeField]
  private int preventedAuraCurseStackPerStack;
  [Header("Damage received")]
  [SerializeField]
  private Enums.DamageType increasedDamageReceivedType;
  [SerializeField]
  private int increasedDirectDamageChargesMultiplierNeededForOne = 1;
  [SerializeField]
  private int increasedDirectDamageReceivedPerTurn;
  [SerializeField]
  private int increasedDirectDamageReceivedPerStack;
  [SerializeField]
  private int increasedPercentDamageReceivedPerTurn;
  [SerializeField]
  private int increasedPercentDamageReceivedPerStack;
  [SerializeField]
  private Enums.DamageType increasedDamageReceivedType2;
  [SerializeField]
  private int increasedDirectDamageChargesMultiplierNeededForOne2 = 1;
  [SerializeField]
  private int increasedDirectDamageReceivedPerTurn2;
  [SerializeField]
  private int increasedDirectDamageReceivedPerStack2;
  [SerializeField]
  private int increasedPercentDamageReceivedPerTurn2;
  [SerializeField]
  private int increasedPercentDamageReceivedPerStack2;
  [Header("Damage prevented")]
  [SerializeField]
  private Enums.DamageType preventedDamageTypePerStack;
  [SerializeField]
  private int preventedDamagePerStack;
  [Header("Heal attacker")]
  [SerializeField]
  private int healAttackerPerStack;
  [SerializeField]
  private int healAttackerConsumeCharges;
  [Header("Character stat modification")]
  [SerializeField]
  private Enums.CharacterStat characterStatModified;
  [SerializeField]
  private int characterStatChargesMultiplierNeededForOne = 1;
  [SerializeField]
  private int characterStatModifiedValue;
  [SerializeField]
  private int characterStatModifiedValuePerStack;
  [SerializeField]
  private bool characterStatAbsolute;
  [SerializeField]
  private int characterStatAbsoluteValue;
  [SerializeField]
  private int characterStatAbsoluteValuePerStack;
  [Header("Resist modification")]
  [SerializeField]
  private Enums.DamageType resistModified;
  [SerializeField]
  private int resistModifiedValue;
  [SerializeField]
  private float resistModifiedPercentagePerStack;
  [SerializeField]
  private Enums.DamageType resistModified2;
  [SerializeField]
  private int resistModifiedValue2;
  [SerializeField]
  private float resistModifiedPercentagePerStack2;
  [SerializeField]
  private Enums.DamageType resistModified3;
  [SerializeField]
  private int resistModifiedValue3;
  [SerializeField]
  private float resistModifiedPercentagePerStack3;
  [Header("Explode at stacks")]
  [SerializeField]
  private int explodeAtStacks;
  [Header("Consume damage")]
  [SerializeField]
  private Enums.DamageType damageTypeWhenConsumed;
  [SerializeField]
  private AuraCurseData consumedDamageChargesBasedOnACCharges;
  [SerializeField]
  private int damageWhenConsumed;
  [SerializeField]
  private float damageWhenConsumedPerCharge;
  [SerializeField]
  private int damageSidesWhenConsumed;
  [SerializeField]
  private int damageSidesWhenConsumedPerCharge;
  [SerializeField]
  private int doubleDamageIfCursesLessThan;
  [Header("Consume heal")]
  [SerializeField]
  private int healWhenConsumed;
  [SerializeField]
  private float healWhenConsumedPerCharge;
  [SerializeField]
  private int healSidesWhenConsumed;
  [SerializeField]
  private float healSidesWhenConsumedPerCharge;
  [Header("Remove Aura Curse")]
  [SerializeField]
  private AuraCurseData removeAuraCurse;
  [SerializeField]
  private AuraCurseData removeAuraCurse2;
  [Header("Aura Curse Gained on Consumption")]
  [SerializeField]
  private AuraCurseData gainAuraCurseConsumption;
  [SerializeField]
  private int gainAuraCurseConsumptionPerCharge;
  [SerializeField]
  private AuraCurseData gainChargesFromThisAuraCurse;
  [SerializeField]
  private AuraCurseData gainAuraCurseConsumption2;
  [SerializeField]
  private int gainAuraCurseConsumptionPerCharge2;
  [SerializeField]
  private AuraCurseData gainChargesFromThisAuraCurse2;
  [Header("Reveal Cards")]
  [SerializeField]
  private int revealCardsPerCharge;
  [Header("Cost Cards")]
  [SerializeField]
  private int modifyCardCostPerChargeNeededForOne;
  [Header("Disabled Cards")]
  [SerializeField]
  private Enums.CardType[] disabledCardTypes;
  [Header("Misc")]
  [SerializeField]
  private bool invulnerable;
  [SerializeField]
  private bool stealth;
  [SerializeField]
  private bool taunt;
  [SerializeField]
  private bool skipsNextTurn;

  public void Init()
  {
    if (!(this.id == "") && this.id != null)
      return;
    this.id = Regex.Replace(this.acName, "\\s+", "").ToLower();
  }

  public int GetMaxCharges()
  {
    int maxCharges = this.maxCharges;
    if (MadnessManager.Instance.IsMadnessTraitActive("restrictedpower") || AtOManager.Instance.IsChallengeTraitActive("restrictedpower"))
      maxCharges = this.maxMadnessCharges;
    return maxCharges;
  }

  public string ACName
  {
    get => this.acName;
    set => this.acName = value;
  }

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }

  public bool IsAura
  {
    get => this.isAura;
    set => this.isAura = value;
  }

  public int MaxCharges
  {
    get => this.maxCharges;
    set => this.maxCharges = value;
  }

  public int AuraConsumed
  {
    get => this.auraConsumed;
    set => this.auraConsumed = value;
  }

  public string Description
  {
    get => this.description;
    set => this.description = value;
  }

  public int ChargesMultiplierDescription
  {
    get => this.chargesMultiplierDescription;
    set => this.chargesMultiplierDescription = value;
  }

  public int ChargesAuxNeedForOne1
  {
    get => this.chargesAuxNeedForOne1;
    set => this.chargesAuxNeedForOne1 = value;
  }

  public int ChargesAuxNeedForOne2
  {
    get => this.chargesAuxNeedForOne2;
    set => this.chargesAuxNeedForOne2 = value;
  }

  public Sprite Sprite
  {
    get => this.sprite;
    set => this.sprite = value;
  }

  public AudioClip Sound
  {
    get => this.sound;
    set => this.sound = value;
  }

  public string EffectTick
  {
    get => this.effectTick;
    set => this.effectTick = value;
  }

  public string EffectTickSides
  {
    get => this.effectTickSides;
    set => this.effectTickSides = value;
  }

  public bool Removable
  {
    get => this.removable;
    set => this.removable = value;
  }

  public bool GainCharges
  {
    get => this.gainCharges;
    set => this.gainCharges = value;
  }

  public bool IconShow
  {
    get => this.iconShow;
    set => this.iconShow = value;
  }

  public bool CombatlogShow
  {
    get => this.combatlogShow;
    set => this.combatlogShow = value;
  }

  public int PriorityOnConsumption
  {
    get => this.priorityOnConsumption;
    set => this.priorityOnConsumption = value;
  }

  public bool ConsumeAll
  {
    get => this.consumeAll;
    set => this.consumeAll = value;
  }

  public bool ConsumedAtCast
  {
    get => this.consumedAtCast;
    set => this.consumedAtCast = value;
  }

  public bool ConsumedAtTurnBegin
  {
    get => this.consumedAtTurnBegin;
    set => this.consumedAtTurnBegin = value;
  }

  public bool ConsumedAtTurn
  {
    get => this.consumedAtTurn;
    set => this.consumedAtTurn = value;
  }

  public bool ConsumedAtRoundBegin
  {
    get => this.consumedAtRoundBegin;
    set => this.consumedAtRoundBegin = value;
  }

  public bool ConsumedAtRound
  {
    get => this.consumedAtRound;
    set => this.consumedAtRound = value;
  }

  public bool ProduceDamageWhenConsumed
  {
    get => this.produceDamageWhenConsumed;
    set => this.produceDamageWhenConsumed = value;
  }

  public bool ProduceHealWhenConsumed
  {
    get => this.produceHealWhenConsumed;
    set => this.produceHealWhenConsumed = value;
  }

  public bool DieWhenConsumedAll
  {
    get => this.dieWhenConsumedAll;
    set => this.dieWhenConsumedAll = value;
  }

  public Enums.DamageType AuraDamageType
  {
    get => this.auraDamageType;
    set => this.auraDamageType = value;
  }

  public int AuraDamageIncreasedTotal
  {
    get => this.auraDamageIncreasedTotal;
    set => this.auraDamageIncreasedTotal = value;
  }

  public float AuraDamageIncreasedPerStack
  {
    get => this.auraDamageIncreasedPerStack;
    set => this.auraDamageIncreasedPerStack = value;
  }

  public int AuraDamageIncreasedPercent
  {
    get => this.auraDamageIncreasedPercent;
    set => this.auraDamageIncreasedPercent = value;
  }

  public float AuraDamageIncreasedPercentPerStack
  {
    get => this.auraDamageIncreasedPercentPerStack;
    set => this.auraDamageIncreasedPercentPerStack = value;
  }

  public Enums.DamageType AuraDamageType2
  {
    get => this.auraDamageType2;
    set => this.auraDamageType2 = value;
  }

  public int AuraDamageIncreasedTotal2
  {
    get => this.auraDamageIncreasedTotal2;
    set => this.auraDamageIncreasedTotal2 = value;
  }

  public float AuraDamageIncreasedPerStack2
  {
    get => this.auraDamageIncreasedPerStack2;
    set => this.auraDamageIncreasedPerStack2 = value;
  }

  public int AuraDamageIncreasedPercent2
  {
    get => this.auraDamageIncreasedPercent2;
    set => this.auraDamageIncreasedPercent2 = value;
  }

  public float AuraDamageIncreasedPercentPerStack2
  {
    get => this.auraDamageIncreasedPercentPerStack2;
    set => this.auraDamageIncreasedPercentPerStack2 = value;
  }

  public Enums.DamageType AuraDamageType3
  {
    get => this.auraDamageType3;
    set => this.auraDamageType3 = value;
  }

  public int AuraDamageIncreasedTotal3
  {
    get => this.auraDamageIncreasedTotal3;
    set => this.auraDamageIncreasedTotal3 = value;
  }

  public float AuraDamageIncreasedPerStack3
  {
    get => this.auraDamageIncreasedPerStack3;
    set => this.auraDamageIncreasedPerStack3 = value;
  }

  public int AuraDamageIncreasedPercent3
  {
    get => this.auraDamageIncreasedPercent3;
    set => this.auraDamageIncreasedPercent3 = value;
  }

  public float AuraDamageIncreasedPercentPerStack3
  {
    get => this.auraDamageIncreasedPercentPerStack3;
    set => this.auraDamageIncreasedPercentPerStack3 = value;
  }

  public Enums.DamageType AuraDamageType4
  {
    get => this.auraDamageType4;
    set => this.auraDamageType4 = value;
  }

  public int AuraDamageIncreasedTotal4
  {
    get => this.auraDamageIncreasedTotal4;
    set => this.auraDamageIncreasedTotal4 = value;
  }

  public float AuraDamageIncreasedPerStack4
  {
    get => this.auraDamageIncreasedPerStack4;
    set => this.auraDamageIncreasedPerStack4 = value;
  }

  public int AuraDamageIncreasedPercent4
  {
    get => this.auraDamageIncreasedPercent4;
    set => this.auraDamageIncreasedPercent4 = value;
  }

  public float AuraDamageIncreasedPercentPerStack4
  {
    get => this.auraDamageIncreasedPercentPerStack4;
    set => this.auraDamageIncreasedPercentPerStack4 = value;
  }

  public int HealDoneTotal
  {
    get => this.healDoneTotal;
    set => this.healDoneTotal = value;
  }

  public int HealDonePerStack
  {
    get => this.healDonePerStack;
    set => this.healDonePerStack = value;
  }

  public int HealDonePercent
  {
    get => this.healDonePercent;
    set => this.healDonePercent = value;
  }

  public int HealDonePercentPerStack
  {
    get => this.healDonePercentPerStack;
    set => this.healDonePercentPerStack = value;
  }

  public int HealReceivedTotal
  {
    get => this.healReceivedTotal;
    set => this.healReceivedTotal = value;
  }

  public int HealReceivedPerStack
  {
    get => this.healReceivedPerStack;
    set => this.healReceivedPerStack = value;
  }

  public int HealReceivedPercent
  {
    get => this.healReceivedPercent;
    set => this.healReceivedPercent = value;
  }

  public int HealReceivedPercentPerStack
  {
    get => this.healReceivedPercentPerStack;
    set => this.healReceivedPercentPerStack = value;
  }

  public int CardsDrawPerStack
  {
    get => this.cardsDrawPerStack;
    set => this.cardsDrawPerStack = value;
  }

  public int DamageReflectedPerStack
  {
    get => this.damageReflectedPerStack;
    set => this.damageReflectedPerStack = value;
  }

  public Enums.DamageType DamageReflectedType
  {
    get => this.damageReflectedType;
    set => this.damageReflectedType = value;
  }

  public int BlockChargesGainedPerStack
  {
    get => this.blockChargesGainedPerStack;
    set => this.blockChargesGainedPerStack = value;
  }

  public bool NoRemoveBlockAtTurnEnd
  {
    get => this.noRemoveBlockAtTurnEnd;
    set => this.noRemoveBlockAtTurnEnd = value;
  }

  public int DamagePreventedPerStack
  {
    get => this.damagePreventedPerStack;
    set => this.damagePreventedPerStack = value;
  }

  public int CursePreventedPerStack
  {
    get => this.cursePreventedPerStack;
    set => this.cursePreventedPerStack = value;
  }

  public Enums.DamageType IncreasedDamageReceivedType
  {
    get => this.increasedDamageReceivedType;
    set => this.increasedDamageReceivedType = value;
  }

  public int IncreasedDirectDamageChargesMultiplierNeededForOne
  {
    get => this.increasedDirectDamageChargesMultiplierNeededForOne;
    set => this.increasedDirectDamageChargesMultiplierNeededForOne = value;
  }

  public int IncreasedDirectDamageReceivedPerTurn
  {
    get => this.increasedDirectDamageReceivedPerTurn;
    set => this.increasedDirectDamageReceivedPerTurn = value;
  }

  public int IncreasedDirectDamageReceivedPerStack
  {
    get => this.increasedDirectDamageReceivedPerStack;
    set => this.increasedDirectDamageReceivedPerStack = value;
  }

  public int IncreasedPercentDamageReceivedPerTurn
  {
    get => this.increasedPercentDamageReceivedPerTurn;
    set => this.increasedPercentDamageReceivedPerTurn = value;
  }

  public int IncreasedPercentDamageReceivedPerStack
  {
    get => this.increasedPercentDamageReceivedPerStack;
    set => this.increasedPercentDamageReceivedPerStack = value;
  }

  public Enums.DamageType PreventedDamageTypePerStack
  {
    get => this.preventedDamageTypePerStack;
    set => this.preventedDamageTypePerStack = value;
  }

  public int PreventedDamagePerStack
  {
    get => this.preventedDamagePerStack;
    set => this.preventedDamagePerStack = value;
  }

  public AuraCurseData PreventedAuraCurse
  {
    get => this.preventedAuraCurse;
    set => this.preventedAuraCurse = value;
  }

  public int PreventedAuraCurseStackPerStack
  {
    get => this.preventedAuraCurseStackPerStack;
    set => this.preventedAuraCurseStackPerStack = value;
  }

  public int HealAttackerPerStack
  {
    get => this.healAttackerPerStack;
    set => this.healAttackerPerStack = value;
  }

  public int HealAttackerConsumeCharges
  {
    get => this.healAttackerConsumeCharges;
    set => this.healAttackerConsumeCharges = value;
  }

  public Enums.CharacterStat CharacterStatModified
  {
    get => this.characterStatModified;
    set => this.characterStatModified = value;
  }

  public int CharacterStatChargesMultiplierNeededForOne
  {
    get => this.characterStatChargesMultiplierNeededForOne;
    set => this.characterStatChargesMultiplierNeededForOne = value;
  }

  public int CharacterStatModifiedValue
  {
    get => this.characterStatModifiedValue;
    set => this.characterStatModifiedValue = value;
  }

  public int CharacterStatModifiedValuePerStack
  {
    get => this.characterStatModifiedValuePerStack;
    set => this.characterStatModifiedValuePerStack = value;
  }

  public bool CharacterStatAbsolute
  {
    get => this.characterStatAbsolute;
    set => this.characterStatAbsolute = value;
  }

  public int CharacterStatAbsoluteValue
  {
    get => this.characterStatAbsoluteValue;
    set => this.characterStatAbsoluteValue = value;
  }

  public int CharacterStatAbsoluteValuePerStack
  {
    get => this.characterStatAbsoluteValuePerStack;
    set => this.characterStatAbsoluteValuePerStack = value;
  }

  public Enums.DamageType ResistModified
  {
    get => this.resistModified;
    set => this.resistModified = value;
  }

  public int ResistModifiedValue
  {
    get => this.resistModifiedValue;
    set => this.resistModifiedValue = value;
  }

  public float ResistModifiedPercentagePerStack
  {
    get => this.resistModifiedPercentagePerStack;
    set => this.resistModifiedPercentagePerStack = value;
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

  public float ResistModifiedPercentagePerStack2
  {
    get => this.resistModifiedPercentagePerStack2;
    set => this.resistModifiedPercentagePerStack2 = value;
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

  public float ResistModifiedPercentagePerStack3
  {
    get => this.resistModifiedPercentagePerStack3;
    set => this.resistModifiedPercentagePerStack3 = value;
  }

  public int ExplodeAtStacks
  {
    get => this.explodeAtStacks;
    set => this.explodeAtStacks = value;
  }

  public Enums.DamageType DamageTypeWhenConsumed
  {
    get => this.damageTypeWhenConsumed;
    set => this.damageTypeWhenConsumed = value;
  }

  public int DamageWhenConsumed
  {
    get => this.damageWhenConsumed;
    set => this.damageWhenConsumed = value;
  }

  public float DamageWhenConsumedPerCharge
  {
    get => this.damageWhenConsumedPerCharge;
    set => this.damageWhenConsumedPerCharge = value;
  }

  public int DamageSidesWhenConsumed
  {
    get => this.damageSidesWhenConsumed;
    set => this.damageSidesWhenConsumed = value;
  }

  public int DamageSidesWhenConsumedPerCharge
  {
    get => this.damageSidesWhenConsumedPerCharge;
    set => this.damageSidesWhenConsumedPerCharge = value;
  }

  public int HealWhenConsumed
  {
    get => this.healWhenConsumed;
    set => this.healWhenConsumed = value;
  }

  public float HealWhenConsumedPerCharge
  {
    get => this.healWhenConsumedPerCharge;
    set => this.healWhenConsumedPerCharge = value;
  }

  public int HealSidesWhenConsumed
  {
    get => this.healSidesWhenConsumed;
    set => this.healSidesWhenConsumed = value;
  }

  public AuraCurseData RemoveAuraCurse
  {
    get => this.removeAuraCurse;
    set => this.removeAuraCurse = value;
  }

  public AuraCurseData RemoveAuraCurse2
  {
    get => this.removeAuraCurse2;
    set => this.removeAuraCurse2 = value;
  }

  public AuraCurseData GainAuraCurseConsumption
  {
    get => this.gainAuraCurseConsumption;
    set => this.gainAuraCurseConsumption = value;
  }

  public int GainAuraCurseConsumptionPerCharge
  {
    get => this.gainAuraCurseConsumptionPerCharge;
    set => this.gainAuraCurseConsumptionPerCharge = value;
  }

  public AuraCurseData GainChargesFromThisAuraCurse
  {
    get => this.gainChargesFromThisAuraCurse;
    set => this.gainChargesFromThisAuraCurse = value;
  }

  public AuraCurseData GainAuraCurseConsumption2
  {
    get => this.gainAuraCurseConsumption2;
    set => this.gainAuraCurseConsumption2 = value;
  }

  public int GainAuraCurseConsumptionPerCharge2
  {
    get => this.gainAuraCurseConsumptionPerCharge2;
    set => this.gainAuraCurseConsumptionPerCharge2 = value;
  }

  public AuraCurseData GainChargesFromThisAuraCurse2
  {
    get => this.gainChargesFromThisAuraCurse2;
    set => this.gainChargesFromThisAuraCurse2 = value;
  }

  public int RevealCardsPerCharge
  {
    get => this.revealCardsPerCharge;
    set => this.revealCardsPerCharge = value;
  }

  public int ModifyCardCostPerChargeNeededForOne
  {
    get => this.modifyCardCostPerChargeNeededForOne;
    set => this.modifyCardCostPerChargeNeededForOne = value;
  }

  public Enums.CardType[] DisabledCardTypes
  {
    get => this.disabledCardTypes;
    set => this.disabledCardTypes = value;
  }

  public bool Invulnerable
  {
    get => this.invulnerable;
    set => this.invulnerable = value;
  }

  public bool Stealth
  {
    get => this.stealth;
    set => this.stealth = value;
  }

  public bool Taunt
  {
    get => this.taunt;
    set => this.taunt = value;
  }

  public bool SkipsNextTurn
  {
    get => this.skipsNextTurn;
    set => this.skipsNextTurn = value;
  }

  public int DamageReflectedConsumeCharges
  {
    get => this.damageReflectedConsumeCharges;
    set => this.damageReflectedConsumeCharges = value;
  }

  public int MaxMadnessCharges
  {
    get => this.maxMadnessCharges;
    set => this.maxMadnessCharges = value;
  }

  public bool Preventable
  {
    get => this.preventable;
    set => this.preventable = value;
  }

  public float AuraDamageIncreasedPercentPerStackPerEnergy
  {
    get => this.auraDamageIncreasedPercentPerStackPerEnergy;
    set => this.auraDamageIncreasedPercentPerStackPerEnergy = value;
  }

  public float AuraDamageIncreasedPercentPerStackPerEnergy2
  {
    get => this.auraDamageIncreasedPercentPerStackPerEnergy2;
    set => this.auraDamageIncreasedPercentPerStackPerEnergy2 = value;
  }

  public int HealDonePercentPerStackPerEnergy
  {
    get => this.healDonePercentPerStackPerEnergy;
    set => this.healDonePercentPerStackPerEnergy = value;
  }

  public float AuraDamageIncreasedPercentPerStackPerEnergy3
  {
    get => this.auraDamageIncreasedPercentPerStackPerEnergy3;
    set => this.auraDamageIncreasedPercentPerStackPerEnergy3 = value;
  }

  public float HealSidesWhenConsumedPerCharge
  {
    get => this.healSidesWhenConsumedPerCharge;
    set => this.healSidesWhenConsumedPerCharge = value;
  }

  public Enums.DamageType IncreasedDamageReceivedType2
  {
    get => this.increasedDamageReceivedType2;
    set => this.increasedDamageReceivedType2 = value;
  }

  public int IncreasedDirectDamageChargesMultiplierNeededForOne2
  {
    get => this.increasedDirectDamageChargesMultiplierNeededForOne2;
    set => this.increasedDirectDamageChargesMultiplierNeededForOne2 = value;
  }

  public int IncreasedDirectDamageReceivedPerTurn2
  {
    get => this.increasedDirectDamageReceivedPerTurn2;
    set => this.increasedDirectDamageReceivedPerTurn2 = value;
  }

  public int IncreasedDirectDamageReceivedPerStack2
  {
    get => this.increasedDirectDamageReceivedPerStack2;
    set => this.increasedDirectDamageReceivedPerStack2 = value;
  }

  public int IncreasedPercentDamageReceivedPerTurn2
  {
    get => this.increasedPercentDamageReceivedPerTurn2;
    set => this.increasedPercentDamageReceivedPerTurn2 = value;
  }

  public int IncreasedPercentDamageReceivedPerStack2
  {
    get => this.increasedPercentDamageReceivedPerStack2;
    set => this.increasedPercentDamageReceivedPerStack2 = value;
  }

  public float AuraDamageIncreasedPercentPerStackPerEnergy4
  {
    get => this.auraDamageIncreasedPercentPerStackPerEnergy4;
    set => this.auraDamageIncreasedPercentPerStackPerEnergy4 = value;
  }

  public int DoubleDamageIfCursesLessThan
  {
    get => this.doubleDamageIfCursesLessThan;
    set => this.doubleDamageIfCursesLessThan = value;
  }

  public AuraCurseData AuraDamageChargesBasedOnACCharges
  {
    get => this.auraDamageChargesBasedOnACCharges;
    set => this.auraDamageChargesBasedOnACCharges = value;
  }

  public AuraCurseData ConsumedDamageChargesBasedOnACCharges
  {
    get => this.consumedDamageChargesBasedOnACCharges;
    set => this.consumedDamageChargesBasedOnACCharges = value;
  }
}
