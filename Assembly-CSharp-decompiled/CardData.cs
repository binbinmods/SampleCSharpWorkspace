// Decompiled with JetBrains decompiler
// Type: CardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Data", order = 51)]
[Serializable]
public class CardData : ScriptableObject
{
  [Header("General Attributes")]
  [SerializeField]
  private string cardName;
  [SerializeField]
  private string id;
  [SerializeField]
  [HideInInspector]
  private string internalId;
  [SerializeField]
  [HideInInspector]
  private bool visible;
  [SerializeField]
  private string upgradesTo1 = "";
  [SerializeField]
  private string upgradesTo2 = "";
  [SerializeField]
  private Enums.CardUpgraded cardUpgraded;
  [SerializeField]
  private string upgradedFrom = "";
  [SerializeField]
  private string baseCard = "";
  [SerializeField]
  private int cardNumber;
  [SerializeField]
  private CardData upgradesToRare;
  [Header("Description and Image")]
  [TextArea]
  [SerializeField]
  private string description = "";
  [SerializeField]
  private string fluff = "";
  [SerializeField]
  private float fluffPercent;
  [SerializeField]
  private string descriptionNormalized = "";
  [SerializeField]
  private string relatedCard;
  [SerializeField]
  private string relatedCard2;
  [SerializeField]
  private string relatedCard3;
  [SerializeField]
  private List<KeyNotesData> keyNotes;
  [SerializeField]
  private Sprite sprite;
  [SerializeField]
  private bool flipSprite;
  [SerializeField]
  private bool showInTome = true;
  [Header("DLC Requeriment")]
  [SerializeField]
  private string sku;
  [Header("Description CUSTOM")]
  [SerializeField]
  private string descriptionId = "";
  [Header("PET Data")]
  [SerializeField]
  private GameObject petModel;
  [SerializeField]
  private bool petFront = true;
  [SerializeField]
  private bool petInvert = true;
  [SerializeField]
  private Vector2 petOffset = new Vector2(0.0f, 0.0f);
  [SerializeField]
  private Vector2 petSize = new Vector2(1f, 1f);
  [Header("PET Effect")]
  [SerializeField]
  private bool isPetAttack;
  [SerializeField]
  private bool isPetCast;
  [Header("Kill PET")]
  [SerializeField]
  private bool killPet;
  [Header("Game Parameters")]
  [SerializeField]
  private int maxInDeck;
  [SerializeField]
  private Enums.CardRarity cardRarity;
  [SerializeField]
  private Enums.CardType cardType;
  [SerializeField]
  private Enums.CardType[] cardTypeAux;
  private List<Enums.CardType> cardTypeList;
  [SerializeField]
  private Enums.CardClass cardClass;
  [SerializeField]
  private ItemData item;
  [SerializeField]
  private ItemData itemEnchantment;
  [SerializeField]
  private int energyCost;
  [SerializeField]
  private int exhaustCounter;
  [SerializeField]
  [HideInInspector]
  private int energyReductionPermanent;
  [SerializeField]
  [HideInInspector]
  private int energyReductionTemporal;
  [SerializeField]
  [HideInInspector]
  private bool energyReductionToZeroPermanent;
  [SerializeField]
  [HideInInspector]
  private bool energyReductionToZeroTemporal;
  [SerializeField]
  [HideInInspector]
  private int energyCostOriginal;
  [SerializeField]
  [HideInInspector]
  private int energyCostForShow;
  [SerializeField]
  private bool playable = true;
  [SerializeField]
  private bool autoplayDraw;
  [SerializeField]
  private bool autoplayEndTurn;
  [Header("Required Conditions")]
  [SerializeField]
  private string effectRequired = "";
  [Header("Custom")]
  [SerializeField]
  private bool vanish;
  [SerializeField]
  private bool innate;
  [SerializeField]
  private bool lazy;
  [SerializeField]
  private bool endTurn;
  [SerializeField]
  private bool moveToCenter;
  [SerializeField]
  private bool corrupted;
  [SerializeField]
  private bool starter;
  [SerializeField]
  private bool onlyInWeekly;
  [SerializeField]
  [HideInInspector]
  private bool modifiedByTrait;
  [Header("Self Kill Hidden (seconds)")]
  [SerializeField]
  private float selfKillHiddenSeconds;
  [Header("Target Data")]
  [SerializeField]
  private Enums.CardTargetType targetType;
  [SerializeField]
  private Enums.CardTargetSide targetSide;
  [SerializeField]
  private Enums.CardTargetPosition targetPosition;
  [Header("Special Value")]
  [SerializeField]
  private Enums.CardSpecialValue specialValueGlobal;
  [SerializeField]
  private AuraCurseData specialAuraCurseNameGlobal;
  [SerializeField]
  private float specialValueModifierGlobal;
  [SerializeField]
  private Enums.CardSpecialValue specialValue1;
  [SerializeField]
  private AuraCurseData specialAuraCurseName1;
  [SerializeField]
  private float specialValueModifier1;
  [SerializeField]
  private Enums.CardSpecialValue specialValue2;
  [SerializeField]
  private AuraCurseData specialAuraCurseName2;
  [SerializeField]
  private float specialValueModifier2;
  [Header("Repeat Cast")]
  [SerializeField]
  private int effectRepeat = 1;
  [SerializeField]
  private float effectRepeatDelay;
  [SerializeField]
  private int effectRepeatEnergyBonus;
  [SerializeField]
  private int effectRepeatMaxBonus;
  [SerializeField]
  private Enums.EffectRepeatTarget effectRepeatTarget;
  [SerializeField]
  private int effectRepeatModificator;
  [Header("Damage")]
  [SerializeField]
  private Enums.DamageType damageType;
  [SerializeField]
  private Enums.DamageType damageTypeOriginal;
  [SerializeField]
  private int damage;
  [SerializeField]
  [HideInInspector]
  private int damagePreCalculated;
  [SerializeField]
  private int damageSides;
  [SerializeField]
  [HideInInspector]
  private int damageSidesPreCalculated;
  [SerializeField]
  private int damageSelf;
  [SerializeField]
  private bool damageSpecialValueGlobal;
  [SerializeField]
  private bool damageSpecialValue1;
  [SerializeField]
  private bool damageSpecialValue2;
  [SerializeField]
  [HideInInspector]
  private int damageSelfPreCalculated;
  [SerializeField]
  [HideInInspector]
  private int damageSelfPreCalculated2;
  [SerializeField]
  private bool ignoreBlock;
  [SerializeField]
  private Enums.DamageType damageType2;
  [SerializeField]
  private Enums.DamageType damageType2Original;
  [SerializeField]
  private int damage2;
  [SerializeField]
  [HideInInspector]
  private int damagePreCalculated2;
  [SerializeField]
  private int damageSides2;
  [SerializeField]
  [HideInInspector]
  private int damageSidesPreCalculated2;
  [SerializeField]
  private int damageSelf2;
  [SerializeField]
  private bool damage2SpecialValueGlobal;
  [SerializeField]
  private bool damage2SpecialValue1;
  [SerializeField]
  private bool damage2SpecialValue2;
  [SerializeField]
  private bool ignoreBlock2;
  [SerializeField]
  private int selfHealthLoss;
  [SerializeField]
  private bool selfHealthLossSpecialGlobal;
  [SerializeField]
  private bool selfHealthLossSpecialValue1;
  [SerializeField]
  private bool selfHealthLossSpecialValue2;
  [Header("Damage Energy Bonus")]
  [SerializeField]
  private int damageEnergyBonus;
  [Header("Aura/Curse Energy Bonus")]
  [SerializeField]
  private AuraCurseData acEnergyBonus;
  [SerializeField]
  private int acEnergyBonusQuantity;
  [SerializeField]
  private AuraCurseData acEnergyBonus2;
  [SerializeField]
  private int acEnergyBonus2Quantity;
  [Header("Heal")]
  [SerializeField]
  private int heal;
  [SerializeField]
  [HideInInspector]
  private int healPreCalculated;
  [SerializeField]
  private int healSides;
  [SerializeField]
  private int healSelf;
  [SerializeField]
  [HideInInspector]
  private int healSelfPreCalculated;
  [SerializeField]
  private int healEnergyBonus;
  [SerializeField]
  private float healSelfPerDamageDonePercent;
  [SerializeField]
  private bool healSpecialValueGlobal;
  [SerializeField]
  private bool healSpecialValue1;
  [SerializeField]
  private bool healSpecialValue2;
  [SerializeField]
  private bool healSelfSpecialValueGlobal;
  [SerializeField]
  private bool healSelfSpecialValue1;
  [SerializeField]
  private bool healSelfSpecialValue2;
  [Header("Aura Curse Dispels")]
  [SerializeField]
  private int healCurses;
  [SerializeField]
  private int dispelAuras;
  [SerializeField]
  private int transferCurses;
  [SerializeField]
  private int stealAuras;
  [SerializeField]
  private int reduceCurses;
  [SerializeField]
  private int reduceAuras;
  [SerializeField]
  private int increaseCurses;
  [SerializeField]
  private int increaseAuras;
  [SerializeField]
  private AuraCurseData healAuraCurseSelf;
  [SerializeField]
  private AuraCurseData healAuraCurseName;
  [SerializeField]
  private AuraCurseData healAuraCurseName2;
  [SerializeField]
  private AuraCurseData healAuraCurseName3;
  [SerializeField]
  private AuraCurseData healAuraCurseName4;
  [Header("Energy")]
  [SerializeField]
  private int energyRecharge;
  [SerializeField]
  private bool energyRechargeSpecialValueGlobal;
  [Header("Aura / Buffs")]
  [SerializeField]
  private AuraCurseData aura;
  [SerializeField]
  private AuraCurseData auraSelf;
  [SerializeField]
  private int auraCharges;
  [SerializeField]
  private bool auraChargesSpecialValueGlobal;
  [SerializeField]
  private bool auraChargesSpecialValue1;
  [SerializeField]
  private bool auraChargesSpecialValue2;
  [SerializeField]
  private AuraCurseData aura2;
  [SerializeField]
  private AuraCurseData auraSelf2;
  [SerializeField]
  private int auraCharges2;
  [SerializeField]
  private bool auraCharges2SpecialValueGlobal;
  [SerializeField]
  private bool auraCharges2SpecialValue1;
  [SerializeField]
  private bool auraCharges2SpecialValue2;
  [SerializeField]
  private AuraCurseData aura3;
  [SerializeField]
  private AuraCurseData auraSelf3;
  [SerializeField]
  private int auraCharges3;
  [SerializeField]
  private bool auraCharges3SpecialValueGlobal;
  [SerializeField]
  private bool auraCharges3SpecialValue1;
  [SerializeField]
  private bool auraCharges3SpecialValue2;
  [Header("Curse / DeBuffs")]
  [SerializeField]
  private AuraCurseData curse;
  [SerializeField]
  private AuraCurseData curseSelf;
  [SerializeField]
  private int curseCharges;
  [SerializeField]
  private bool curseChargesSpecialValueGlobal;
  [SerializeField]
  private bool curseChargesSpecialValue1;
  [SerializeField]
  private bool curseChargesSpecialValue2;
  [SerializeField]
  private AuraCurseData curse2;
  [SerializeField]
  private AuraCurseData curseSelf2;
  [SerializeField]
  private int curseCharges2;
  [SerializeField]
  private bool curseCharges2SpecialValueGlobal;
  [SerializeField]
  private bool curseCharges2SpecialValue1;
  [SerializeField]
  private bool curseCharges2SpecialValue2;
  [SerializeField]
  private AuraCurseData curse3;
  [SerializeField]
  private AuraCurseData curseSelf3;
  [SerializeField]
  private int curseCharges3;
  [SerializeField]
  private bool curseCharges3SpecialValueGlobal;
  [SerializeField]
  private bool curseCharges3SpecialValue1;
  [SerializeField]
  private bool curseCharges3SpecialValue2;
  [Header("Character Interation")]
  [SerializeField]
  private int pushTarget;
  [SerializeField]
  private int pullTarget;
  [Header("Card Management")]
  [SerializeField]
  private int drawCard;
  [SerializeField]
  private bool drawCardSpecialValueGlobal;
  [SerializeField]
  private int discardCard;
  [SerializeField]
  private Enums.CardType discardCardType;
  [SerializeField]
  private Enums.CardType[] discardCardTypeAux;
  [SerializeField]
  private bool discardCardAutomatic;
  [SerializeField]
  private Enums.CardPlace discardCardPlace;
  [SerializeField]
  private int addCard;
  [SerializeField]
  private string addCardId = "";
  [SerializeField]
  private Enums.CardType addCardType;
  [SerializeField]
  private Enums.CardType[] addCardTypeAux;
  [SerializeField]
  private CardData[] addCardList;
  [SerializeField]
  private int addCardChoose;
  [SerializeField]
  private Enums.CardFrom addCardFrom;
  [SerializeField]
  private Enums.CardPlace addCardPlace;
  [SerializeField]
  private int addCardReducedCost;
  [SerializeField]
  private bool addCardCostTurn;
  [SerializeField]
  private bool addCardVanish;
  [Header("Look cards")]
  [SerializeField]
  private int lookCards;
  [SerializeField]
  private int lookCardsDiscardUpTo;
  [SerializeField]
  private int lookCardsVanishUpTo;
  [Header("Summon Units")]
  [SerializeField]
  private NPCData summonUnit;
  [SerializeField]
  private int summonUnitNum;
  [SerializeField]
  private AuraCurseData summonAura;
  [SerializeField]
  private int summonAuraCharges;
  [SerializeField]
  private AuraCurseData summonAura2;
  [SerializeField]
  private int summonAuraCharges2;
  [SerializeField]
  private AuraCurseData summonAura3;
  [SerializeField]
  private int summonAuraCharges3;
  [Header("Sounds")]
  [SerializeField]
  private AudioClip sound;
  [SerializeField]
  private AudioClip soundPreAction;
  [SerializeField]
  private AudioClip soundPreActionFemale;
  [Header("InGame Effects")]
  [SerializeField]
  private string effectPreAction = "";
  [SerializeField]
  private string effectCaster = "";
  [SerializeField]
  private bool effectCasterRepeat;
  [SerializeField]
  private float effectPostCastDelay;
  [SerializeField]
  private bool effectCastCenter;
  [SerializeField]
  private string effectTrail = "";
  [SerializeField]
  private bool effectTrailRepeat;
  [SerializeField]
  private float effectTrailSpeed = 1f;
  [SerializeField]
  private Enums.EffectTrailAngle effectTrailAngle;
  [SerializeField]
  private string effectTarget = "";
  [SerializeField]
  private float effectPostTargetDelay;
  [Header("InGame Effects")]
  [SerializeField]
  private int goldGainQuantity;
  [SerializeField]
  private int shardsGainQuantity;
  [SerializeField]
  [HideInInspector]
  private string target = "";
  [SerializeField]
  [HideInInspector]
  private int enchantDamagePreCalculated;
  private string colorUpgradePlain = "5E3016";
  private string colorUpgradeGold = "875700";
  private string colorUpgradeBlue = "215382";
  private string colorUpgradeRare = "7F15A6";

  public void Init(string newId) => this.id = newId;

  public void InitClone(string _internalId)
  {
    this.id = _internalId;
    this.internalId = _internalId;
    if (this.energyCostOriginal == 0)
      this.energyCostOriginal = this.energyCost;
    this.damageTypeOriginal = this.damageType;
    this.damageType2Original = this.damageType2;
    this.damagePreCalculated = this.damage;
    this.damagePreCalculated2 = this.damage2;
    this.damageSelfPreCalculated = this.damageSelf;
    this.damageSidesPreCalculated = this.damageSides;
    this.damageSidesPreCalculated2 = this.damageSides2;
    this.damageSelfPreCalculated2 = this.damageSelf2;
    this.effectRequired = this.effectRequired.ToLower();
    if ((UnityEngine.Object) this.itemEnchantment != (UnityEngine.Object) null)
      this.enchantDamagePreCalculated = this.itemEnchantment.DamageToTarget;
    else if ((UnityEngine.Object) this.item != (UnityEngine.Object) null)
      this.enchantDamagePreCalculated = this.item.DamageToTarget;
    this.healPreCalculated = this.heal;
    this.healSelfPreCalculated = this.healSelf;
    if (this.innate)
    {
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData("innate"));
      Globals.Instance.IncludeInSearch(Texts.Instance.GetText("innate"), this.id);
    }
    if (this.vanish)
    {
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData("vanish"));
      Globals.Instance.IncludeInSearch(Texts.Instance.GetText("vanish"), this.id);
    }
    if (this.energyRecharge > 0 || this.energyRechargeSpecialValueGlobal)
    {
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData("energy"));
      Globals.Instance.IncludeInSearch(Texts.Instance.GetText("energy"), this.id);
    }
    if ((UnityEngine.Object) this.aura != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.aura.Id));
    if ((UnityEngine.Object) this.aura2 != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.aura2.Id));
    if ((UnityEngine.Object) this.aura3 != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.aura3.Id));
    if ((UnityEngine.Object) this.auraSelf != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.auraSelf.Id));
    if ((UnityEngine.Object) this.auraSelf2 != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.auraSelf2.Id));
    if ((UnityEngine.Object) this.auraSelf3 != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.auraSelf3.Id));
    if ((UnityEngine.Object) this.curse != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.curse.Id));
    if ((UnityEngine.Object) this.curse2 != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.curse2.Id));
    if ((UnityEngine.Object) this.curse3 != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.curse3.Id));
    if ((UnityEngine.Object) this.curseSelf != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.curseSelf.Id));
    if ((UnityEngine.Object) this.curseSelf2 != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.curseSelf2.Id));
    if ((UnityEngine.Object) this.curseSelf3 != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.curseSelf3.Id));
    if ((UnityEngine.Object) this.healAuraCurseSelf != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.healAuraCurseSelf.Id));
    if ((UnityEngine.Object) this.healAuraCurseName != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.healAuraCurseName.Id));
    if ((UnityEngine.Object) this.healAuraCurseName2 != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.healAuraCurseName2.Id));
    if ((UnityEngine.Object) this.healAuraCurseName3 != (UnityEngine.Object) null)
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.healAuraCurseName3.Id));
    if (!((UnityEngine.Object) this.healAuraCurseName4 != (UnityEngine.Object) null))
      return;
    this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.healAuraCurseName4.Id));
  }

  public void InitClone2()
  {
    if (this.effectRequired != "")
    {
      this.KeyNotes.Add(Globals.Instance.GetKeyNotesData(this.effectRequired));
      Globals.Instance.IncludeInSearch(Texts.Instance.GetText(this.effectRequired), this.id);
    }
    this.SetDescriptionNew();
    this.SetTarget();
    this.SetRarity();
  }

  public void ModifyDamageType(Enums.DamageType dt = Enums.DamageType.None, Character ch = null)
  {
    int damageType = (int) this.damageType;
    if (dt == Enums.DamageType.None)
    {
      this.damageType = this.damageTypeOriginal;
      this.damageType2 = this.damageType2Original;
    }
    else
    {
      this.damageType = dt;
      this.damageType2 = dt;
    }
  }

  public void SetEnchantDamagePrecalculated(int damage) => this.enchantDamagePreCalculated = damage;

  public void SetDamagePrecalculated(int damage) => this.damagePreCalculated = damage;

  public void SetDamagePrecalculated2(int damage) => this.damagePreCalculated2 = damage;

  public void SetDamageSelfPrecalculated(int damage) => this.damageSelfPreCalculated = damage;

  public void SetDamageSelfPrecalculated2(int damage) => this.damageSelfPreCalculated2 = damage;

  public void SetDamageSidesPrecalculated(int damage) => this.damageSidesPreCalculated = damage;

  public void SetDamageSidesPrecalculated2(int damage) => this.damageSidesPreCalculated2 = damage;

  public void SetHealPrecalculated(int heal) => this.healPreCalculated = heal;

  public void SetHealSelfPrecalculated(int heal) => this.healSelfPreCalculated = heal;

  private string ColorTextArray(string type, params string[] text)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<nobr>");
    switch (type)
    {
      case "":
        int num = 0;
        foreach (string str in text)
        {
          if (num > 0)
            stringBuilder.Append(" ");
          stringBuilder.Append(str);
          ++num;
        }
        if (type != "")
          stringBuilder.Append("</color>");
        stringBuilder.Append("</nobr> ");
        return stringBuilder.ToString();
      case "damage":
        stringBuilder.Append("<color=#B00A00>");
        goto case "";
      case "heal":
        stringBuilder.Append("<color=#1E650F>");
        goto case "";
      case "aura":
        stringBuilder.Append("<color=#263ABC>");
        goto case "";
      case "curse":
        stringBuilder.Append("<color=#720070>");
        goto case "";
      case "system":
        stringBuilder.Append("<color=#5E3016>");
        goto case "";
      default:
        stringBuilder.Append("<color=#5E3016");
        stringBuilder.Append(">");
        goto case "";
    }
  }

  private int GetFinalAuraCharges(string acId, int charges, Character character = null) => character == null ? charges : charges + character.GetAuraCurseQuantityModification(acId, this.cardClass);

  private string SpriteText(string sprite)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string str = sprite.ToLower().Replace(" ", "");
    switch (str)
    {
      case "block":
      case "card":
        stringBuilder.Append("<space=.2>");
        break;
      case "piercing":
        stringBuilder.Append("<space=.4>");
        break;
      case "bleed":
        stringBuilder.Append("<space=.1>");
        break;
      case "bless":
        stringBuilder.Append("<space=.1>");
        break;
      default:
        stringBuilder.Append("<space=.3>");
        break;
    }
    stringBuilder.Append("<size=+.1><sprite name=");
    stringBuilder.Append(str);
    stringBuilder.Append("></size>");
    switch (str)
    {
      case "bleed":
        stringBuilder.Append("<space=-.4>");
        goto case "reinforce";
      case "reinforce":
      case "fire":
        return stringBuilder.ToString();
      case "card":
        stringBuilder.Append("<space=-.2>");
        goto case "reinforce";
      case "powerful":
      case "fury":
        stringBuilder.Append("<space=-.1>");
        goto case "reinforce";
      default:
        stringBuilder.Append("<space=-.2>");
        goto case "reinforce";
    }
  }

  public string GetRequireText()
  {
    string requireText = "";
    if (this.effectRequired != "")
    {
      requireText = string.Format(Texts.Instance.GetText("requireSkill"), (object) Globals.Instance.GetKeyNotesData(this.effectRequired).KeynoteName);
      if (this.effectRequired.ToLower() == "stanzai" || this.effectRequired.ToLower() == "stanzaii")
        requireText += "+";
    }
    return requireText;
  }

  private void SetRarity()
  {
    if (this.cardRarity == Enums.CardRarity.Common)
      Globals.Instance.IncludeInSearch(Texts.Instance.GetText("common"), this.id);
    else if (this.cardRarity == Enums.CardRarity.Uncommon)
      Globals.Instance.IncludeInSearch(Texts.Instance.GetText("uncommon"), this.id);
    else if (this.cardRarity == Enums.CardRarity.Rare)
      Globals.Instance.IncludeInSearch(Texts.Instance.GetText("rare"), this.id);
    else if (this.cardRarity == Enums.CardRarity.Epic)
    {
      Globals.Instance.IncludeInSearch(Texts.Instance.GetText("epic"), this.id);
    }
    else
    {
      if (this.cardRarity != Enums.CardRarity.Mythic)
        return;
      Globals.Instance.IncludeInSearch(Texts.Instance.GetText("mythic"), this.id);
    }
  }

  private void SetTarget()
  {
    if (this.autoplayDraw)
      this.target = Texts.Instance.GetText("onDraw");
    else if (this.autoplayEndTurn)
      this.target = Texts.Instance.GetText("onEndTurn");
    else if (this.targetType == Enums.CardTargetType.Global && this.targetSide == Enums.CardTargetSide.Anyone)
      this.target = Texts.Instance.GetText("global");
    else if (this.targetSide == Enums.CardTargetSide.Self)
      this.target = Texts.Instance.GetText("self");
    else if (this.targetSide == Enums.CardTargetSide.Anyone && this.targetPosition == Enums.CardTargetPosition.Random)
      this.target = Texts.Instance.GetText("random");
    else if (this.targetType == Enums.CardTargetType.Single && this.targetSide == Enums.CardTargetSide.Anyone && this.targetPosition == Enums.CardTargetPosition.Anywhere)
      this.target = Texts.Instance.GetText("anyone");
    else if (this.cardClass != Enums.CardClass.Monster)
    {
      if (this.targetSide == Enums.CardTargetSide.Friend)
        this.target = this.targetType != Enums.CardTargetType.Global ? (this.targetPosition != Enums.CardTargetPosition.Random ? (this.targetPosition != Enums.CardTargetPosition.Front ? (this.targetPosition != Enums.CardTargetPosition.Back ? Texts.Instance.GetText("hero") : Texts.Instance.GetText("backHero")) : Texts.Instance.GetText("frontHero")) : Texts.Instance.GetText("randomHero")) : Texts.Instance.GetText("allHeroes");
      else if (this.targetSide == Enums.CardTargetSide.FriendNotSelf)
        this.target = Texts.Instance.GetText("otherHero");
      else if (this.targetSide == Enums.CardTargetSide.Enemy)
        this.target = this.targetType != Enums.CardTargetType.Global ? (this.targetPosition != Enums.CardTargetPosition.Random ? (this.targetPosition != Enums.CardTargetPosition.Front ? (this.targetPosition != Enums.CardTargetPosition.Back ? Texts.Instance.GetText("monster") : Texts.Instance.GetText("backMonster")) : Texts.Instance.GetText("frontMonster")) : Texts.Instance.GetText("randomMonster")) : Texts.Instance.GetText("allMonsters");
    }
    else if (this.cardClass == Enums.CardClass.Monster)
    {
      if (this.targetSide == Enums.CardTargetSide.Friend)
        this.target = this.targetType != Enums.CardTargetType.Global ? (this.targetPosition != Enums.CardTargetPosition.Random ? (this.targetPosition != Enums.CardTargetPosition.Front ? (this.targetPosition != Enums.CardTargetPosition.Back ? (this.targetPosition != Enums.CardTargetPosition.Middle ? (this.targetPosition != Enums.CardTargetPosition.Slowest ? (this.targetPosition != Enums.CardTargetPosition.Fastest ? (this.targetPosition != Enums.CardTargetPosition.LeastHP ? (this.targetPosition != Enums.CardTargetPosition.MostHP ? Texts.Instance.GetText("monster") : Texts.Instance.GetText("mostHPMonster")) : Texts.Instance.GetText("leastHPMonster")) : Texts.Instance.GetText("fastestMonster")) : Texts.Instance.GetText("slowestMonster")) : Texts.Instance.GetText("middleMonster")) : Texts.Instance.GetText("backMonster")) : Texts.Instance.GetText("frontMonster")) : Texts.Instance.GetText("randomMonster")) : Texts.Instance.GetText("allMonsters");
      else if (this.targetSide == Enums.CardTargetSide.FriendNotSelf)
        this.target = Texts.Instance.GetText("otherMonster");
      else if (this.targetSide == Enums.CardTargetSide.Enemy)
        this.target = this.targetType != Enums.CardTargetType.Global ? (this.targetPosition != Enums.CardTargetPosition.Random ? (this.targetPosition != Enums.CardTargetPosition.Front ? (this.targetPosition != Enums.CardTargetPosition.Back ? (this.targetPosition != Enums.CardTargetPosition.Middle ? (this.targetPosition != Enums.CardTargetPosition.Slowest ? (this.targetPosition != Enums.CardTargetPosition.Fastest ? (this.targetPosition != Enums.CardTargetPosition.LeastHP ? (this.targetPosition != Enums.CardTargetPosition.MostHP ? Texts.Instance.GetText("hero") : Texts.Instance.GetText("mostHPHero")) : Texts.Instance.GetText("leastHPHero")) : Texts.Instance.GetText("fastestHero")) : Texts.Instance.GetText("slowestHero")) : Texts.Instance.GetText("middleHero")) : Texts.Instance.GetText("backHero")) : Texts.Instance.GetText("frontHero")) : Texts.Instance.GetText("randomHero")) : Texts.Instance.GetText("allHeroes");
    }
    Globals.Instance.IncludeInSearch(this.target, this.id);
  }

  public void SetDescriptionNew(bool forceDescription = false, Character character = null, bool includeInSearch = true)
  {
    if (forceDescription || !Globals.Instance.CardsDescriptionNormalized.ContainsKey(this.id))
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      string str1 = "<line-height=15%><br></line-height>";
      string str2 = "<color=#444><size=-.15>";
      string str3 = "</size></color>";
      string str4 = "<color=#5E3016><size=-.15>";
      int num1 = 0;
      if (this.descriptionId != "")
        stringBuilder1.Append(Functions.FormatStringCard(Texts.Instance.GetText(this.descriptionId)));
      else if ((UnityEngine.Object) this.item == (UnityEngine.Object) null && (UnityEngine.Object) this.itemEnchantment == (UnityEngine.Object) null)
      {
        string str5 = "";
        string str6 = "";
        string str7 = "";
        float num2 = 0.0f;
        string str8 = "";
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = true;
        bool flag4 = false;
        StringBuilder stringBuilder3 = new StringBuilder();
        if (this.damage > 0 || this.damage2 > 0 || this.damageSelf > 0 || this.DamageSelf2 > 0)
          flag3 = false;
        if (this.drawCard != 0 && !this.drawCardSpecialValueGlobal)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDraw"), (object) this.ColorTextArray("", this.NumFormat(this.drawCard), this.SpriteText("card"))));
          stringBuilder1.Append("<br>");
        }
        if (this.specialValueGlobal == Enums.CardSpecialValue.DiscardedCards)
        {
          if (this.discardCardPlace == Enums.CardPlace.Vanish)
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsVanish"), (object) this.ColorTextArray("", "X", this.SpriteText("card"))));
          else
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDiscard"), (object) this.ColorTextArray("", "X", this.SpriteText("card"))));
          stringBuilder1.Append("\n");
        }
        if (this.drawCardSpecialValueGlobal)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDraw"), (object) this.ColorTextArray("aura", "X", this.SpriteText("card"))));
          stringBuilder1.Append("<br>");
        }
        if (this.addCard != 0)
        {
          string str9;
          if (this.addCardId != "")
          {
            stringBuilder2.Append(this.ColorTextArray("", this.NumFormat(this.addCard), this.SpriteText("card")));
            CardData cardData = Globals.Instance.GetCardData(this.addCardId, false);
            if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
            {
              stringBuilder2.Append(this.ColorFromCardDataRarity(cardData));
              stringBuilder2.Append(cardData.CardName);
              stringBuilder2.Append("</color>");
            }
            str9 = stringBuilder2.ToString();
            stringBuilder2.Clear();
          }
          else
          {
            if (this.addCardChoose > 0)
              stringBuilder2.Append(this.ColorTextArray("", this.NumFormat(this.addCardChoose), this.SpriteText("card")));
            else
              stringBuilder2.Append(this.ColorTextArray("", this.NumFormat(this.addCard), this.SpriteText("card")));
            if (this.addCardType != Enums.CardType.None)
            {
              stringBuilder2.Append("<color=#5E3016>");
              stringBuilder2.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) this.addCardType)));
              stringBuilder2.Append("</color>");
            }
            str9 = stringBuilder2.ToString();
            stringBuilder2.Clear();
          }
          string str10 = "";
          if (this.addCardReducedCost == -1)
            str10 = !this.addCardVanish ? (!this.addCardCostTurn ? string.Format(Texts.Instance.GetText("cardsAddCost"), (object) 0) : string.Format(Texts.Instance.GetText("cardsAddCostTurn"), (object) 0)) : (!this.addCardCostTurn ? string.Format(Texts.Instance.GetText("cardsAddCostVanish"), (object) 0) : string.Format(Texts.Instance.GetText("cardsAddCostVanish"), (object) 0));
          else if (this.addCardReducedCost > 0)
            str10 = !this.addCardVanish ? (!this.addCardCostTurn ? string.Format(Texts.Instance.GetText("cardsAddCostReduced"), (object) this.addCardReducedCost) : string.Format(Texts.Instance.GetText("cardsAddCostReducedTurn"), (object) this.addCardReducedCost)) : (!this.addCardCostTurn ? string.Format(Texts.Instance.GetText("cardsAddCostReducedVanish"), (object) this.addCardReducedCost) : string.Format(Texts.Instance.GetText("cardsAddCostReducedVanishTurn"), (object) this.addCardReducedCost));
          string _id = "";
          if (this.addCardId != "")
          {
            if (this.addCardPlace == Enums.CardPlace.RandomDeck)
              _id = this.targetSide == Enums.CardTargetSide.Self || this.targetSide == Enums.CardTargetSide.Enemy && this.cardClass != Enums.CardClass.Monster ? "cardsIDShuffleDeck" : "cardsIDShuffleTargetDeck";
            else if (this.addCardPlace == Enums.CardPlace.Hand)
              _id = "cardsIDPlaceHand";
            else if (this.addCardPlace == Enums.CardPlace.TopDeck)
              _id = this.targetSide != Enums.CardTargetSide.Self ? "cardsIDPlaceTargetTopDeck" : "cardsIDPlaceTopDeck";
            else if (this.addCardPlace == Enums.CardPlace.Discard)
              _id = this.targetSide == Enums.CardTargetSide.Self || this.targetSide == Enums.CardTargetSide.Enemy && this.cardClass != Enums.CardClass.Monster ? "cardsIDPlaceDiscard" : "cardsIDPlaceTargetDiscard";
          }
          else if (this.addCardFrom == Enums.CardFrom.Game)
          {
            if (this.addCardPlace == Enums.CardPlace.RandomDeck)
              _id = this.addCardChoose != 0 ? "cardsDiscoverNumberToDeck" : "cardsDiscoverToDeck";
            else if (this.addCardPlace == Enums.CardPlace.Hand)
              _id = this.addCardChoose != 0 ? "cardsDiscoverNumberToHand" : "cardsDiscoverToHand";
            else if (this.addCardPlace == Enums.CardPlace.TopDeck && this.addCardChoose != 0)
              _id = "cardsDiscoverNumberToTopDeck";
          }
          else if (this.addCardFrom == Enums.CardFrom.Deck)
          {
            if (this.addCardPlace == Enums.CardPlace.Hand)
              _id = this.addCardChoose <= 0 ? (this.addCard <= 1 ? "cardsRevealItToHand" : "cardsRevealThemToHand") : "cardsRevealNumberToHand";
            else if (this.addCardPlace == Enums.CardPlace.TopDeck)
              _id = this.addCardChoose <= 0 ? (this.addCard <= 1 ? "cardsRevealItToTopDeck" : "cardsRevealThemToTopDeck") : "cardsRevealNumberToTopDeck";
          }
          else if (this.addCardFrom == Enums.CardFrom.Discard)
          {
            if (this.addCardPlace == Enums.CardPlace.TopDeck)
              _id = "cardsPickToTop";
            else if (this.addCardPlace == Enums.CardPlace.Hand)
              _id = "cardsPickToHand";
          }
          else if (this.addCardFrom == Enums.CardFrom.Hand)
          {
            if (this.targetSide == Enums.CardTargetSide.Friend)
            {
              if (this.addCardPlace == Enums.CardPlace.TopDeck)
                _id = "cardsDuplicateToTargetTopDeck";
              else if (this.addCardPlace == Enums.CardPlace.RandomDeck)
                _id = "cardsDuplicateToTargetRandomDeck";
            }
            else if (this.addCardPlace == Enums.CardPlace.Hand)
              _id = "cardsDuplicateToHand";
          }
          else if (this.addCardFrom == Enums.CardFrom.Vanish)
          {
            if (this.addCardPlace == Enums.CardPlace.TopDeck)
              _id = "cardsFromVanishToTop";
            else if (this.addCardPlace == Enums.CardPlace.Hand)
              _id = "cardsFromVanishToHand";
          }
          stringBuilder1.Append(string.Format(Texts.Instance.GetText(_id), (object) str9, (object) this.ColorTextArray("", this.NumFormat(this.addCard))));
          if (str10 != "")
          {
            stringBuilder1.Append(" ");
            stringBuilder1.Append(str2);
            stringBuilder1.Append(str10);
            stringBuilder1.Append(str3);
          }
          stringBuilder1.Append("\n");
        }
        if (this.discardCard != 0)
        {
          if (this.discardCardType != Enums.CardType.None)
          {
            stringBuilder2.Append("<color=#5E3016>");
            stringBuilder2.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) this.discardCardType)));
            stringBuilder2.Append("</color>");
          }
          if (this.discardCardPlace == Enums.CardPlace.Discard)
          {
            if (!this.discardCardAutomatic)
            {
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDiscard"), (object) this.ColorTextArray("", this.NumFormat(this.discardCard), this.SpriteText("card"))));
              stringBuilder1.Append(stringBuilder2);
              stringBuilder1.Append("\n");
            }
            else
            {
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDiscard"), (object) this.ColorTextArray("", this.NumFormat(this.discardCard), this.SpriteText("cardrandom"))));
              stringBuilder1.Append(stringBuilder2);
              stringBuilder1.Append("\n");
            }
          }
          else if (this.discardCardPlace == Enums.CardPlace.TopDeck)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsPlaceToTop"), (object) this.ColorTextArray("", this.NumFormat(this.discardCard), this.SpriteText("card"), stringBuilder2.ToString().Trim())));
            stringBuilder1.Append("\n");
          }
          else if (this.discardCardPlace == Enums.CardPlace.Vanish)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsVanish"), (object) this.ColorTextArray("", this.NumFormat(this.discardCard), this.SpriteText("card"), stringBuilder2.ToString().Trim())));
            stringBuilder1.Append("\n");
          }
          stringBuilder2.Clear();
        }
        if (this.lookCards != 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsLook"), (object) this.ColorTextArray("", this.NumFormat(this.lookCards), this.SpriteText("card"))));
          stringBuilder1.Append("\n");
          if (this.lookCardsDiscardUpTo == -1)
          {
            stringBuilder1.Append(Texts.Instance.GetText("cardsDiscardAny"));
            stringBuilder1.Append("\n");
          }
          else if (this.lookCardsDiscardUpTo > 0)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDiscardUpTo"), (object) this.ColorTextArray("", this.NumFormat(this.lookCardsDiscardUpTo))));
            stringBuilder1.Append("\n");
          }
          else if (this.lookCardsVanishUpTo > 0)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsVanishUpTo"), (object) this.ColorTextArray("", this.NumFormat(this.lookCardsVanishUpTo))));
            stringBuilder1.Append("\n");
          }
        }
        num1 = 0;
        if ((UnityEngine.Object) this.summonUnit != (UnityEngine.Object) null && this.summonUnitNum > 0)
        {
          stringBuilder2.Append("\n<color=#5E3016>");
          if (this.summonUnitNum > 1)
          {
            stringBuilder2.Append(this.summonUnitNum);
            stringBuilder2.Append(" ");
          }
          if ((UnityEngine.Object) this.summonUnit != (UnityEngine.Object) null && (UnityEngine.Object) Globals.Instance.GetNPC(this.summonUnit.Id) != (UnityEngine.Object) null)
            stringBuilder2.Append(Globals.Instance.GetNPC(this.summonUnit.Id).NPCName);
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsSummon"), (object) stringBuilder2.ToString()));
          stringBuilder1.Append("</color>\n");
          stringBuilder2.Clear();
        }
        if (this.dispelAuras > 0)
        {
          if (this.targetSide == Enums.CardTargetSide.Enemy)
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsPurge"), (object) this.dispelAuras));
          else
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDispel"), (object) this.dispelAuras));
          stringBuilder1.Append("\n");
        }
        if (this.dispelAuras == -1)
        {
          if (this.targetSide == Enums.CardTargetSide.Enemy)
            stringBuilder1.Append(Texts.Instance.GetText("cardsDispelAll"));
          else
            stringBuilder1.Append(Texts.Instance.GetText("cardsPurgeAll"));
          stringBuilder1.Append("\n");
        }
        num1 = 0;
        if (this.specialValueGlobal == Enums.CardSpecialValue.None && this.specialValue1 == Enums.CardSpecialValue.None && this.specialValue2 == Enums.CardSpecialValue.None)
        {
          StringBuilder stringBuilder4 = new StringBuilder();
          StringBuilder stringBuilder5 = new StringBuilder();
          if ((UnityEngine.Object) this.healAuraCurseName != (UnityEngine.Object) null)
          {
            if (this.healAuraCurseName.IsAura)
              stringBuilder4.Append(this.SpriteText(this.healAuraCurseName.ACName));
            else
              stringBuilder5.Append(this.SpriteText(this.healAuraCurseName.ACName));
          }
          if ((UnityEngine.Object) this.healAuraCurseName2 != (UnityEngine.Object) null)
          {
            if (this.healAuraCurseName2.IsAura)
              stringBuilder4.Append(this.SpriteText(this.healAuraCurseName2.ACName));
            else
              stringBuilder5.Append(this.SpriteText(this.healAuraCurseName2.ACName));
          }
          if ((UnityEngine.Object) this.healAuraCurseName3 != (UnityEngine.Object) null)
          {
            if (this.healAuraCurseName3.IsAura)
              stringBuilder4.Append(this.SpriteText(this.healAuraCurseName3.ACName));
            else
              stringBuilder5.Append(this.SpriteText(this.healAuraCurseName3.ACName));
          }
          if ((UnityEngine.Object) this.healAuraCurseName4 != (UnityEngine.Object) null)
          {
            if (this.healAuraCurseName4.IsAura)
              stringBuilder4.Append(this.SpriteText(this.healAuraCurseName4.ACName));
            else
              stringBuilder5.Append(this.SpriteText(this.healAuraCurseName4.ACName));
          }
          if (stringBuilder4.Length > 0)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsPurge"), (object) stringBuilder4.ToString()));
            stringBuilder1.Append("\n");
          }
          if (stringBuilder5.Length > 0)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDispel"), (object) stringBuilder5.ToString()));
            stringBuilder1.Append("\n");
          }
          if (this.healCurses > 0)
          {
            if (this.targetSide == Enums.CardTargetSide.Enemy)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsPurge"), (object) this.ColorTextArray("aura", this.NumFormatItem(this.healCurses))));
            else
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDispel"), (object) this.ColorTextArray("curse", this.NumFormatItem(this.healCurses))));
            stringBuilder1.Append("\n");
          }
          if (this.healCurses == -1)
          {
            if (this.targetSide == Enums.CardTargetSide.Enemy)
              stringBuilder1.Append(Texts.Instance.GetText("cardsPurgeAll"));
            else
              stringBuilder1.Append(Texts.Instance.GetText("cardsDispelAll"));
            stringBuilder1.Append("\n");
          }
          if ((UnityEngine.Object) this.healAuraCurseSelf != (UnityEngine.Object) null)
          {
            if (this.healAuraCurseSelf.IsAura)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsPurgeYour"), (object) this.SpriteText(this.healAuraCurseSelf.ACName)));
            else
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDispelYour"), (object) this.SpriteText(this.healAuraCurseSelf.ACName)));
            stringBuilder1.Append("\n");
          }
        }
        else
        {
          if (this.healCurses > 0)
          {
            if (this.targetSide == Enums.CardTargetSide.Enemy)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsPurge"), (object) this.healCurses));
            else
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDispel"), (object) this.healCurses));
            stringBuilder1.Append("\n");
          }
          if ((UnityEngine.Object) this.healAuraCurseName4 != (UnityEngine.Object) null && (UnityEngine.Object) this.healAuraCurseName3 == (UnityEngine.Object) null)
          {
            StringBuilder stringBuilder6 = new StringBuilder();
            StringBuilder stringBuilder7 = new StringBuilder();
            if (this.healAuraCurseName4.IsAura)
              stringBuilder6.Append(this.SpriteText(this.healAuraCurseName4.ACName));
            else
              stringBuilder7.Append(this.SpriteText(this.healAuraCurseName4.ACName));
            if (stringBuilder6.Length > 0)
            {
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsPurge"), (object) stringBuilder6.ToString()));
              stringBuilder1.Append("\n");
            }
            if (stringBuilder7.Length > 0)
            {
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDispel"), (object) stringBuilder7.ToString()));
              stringBuilder1.Append("\n");
            }
          }
        }
        if (this.transferCurses > 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsTransferCurse"), (object) this.transferCurses.ToString()));
          stringBuilder1.Append("\n");
        }
        if (this.stealAuras > 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsStealAuras"), (object) this.stealAuras.ToString()));
          stringBuilder1.Append("\n");
        }
        if (this.increaseAuras > 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsIncreaseAura"), (object) this.increaseAuras.ToString()));
          stringBuilder1.Append("\n");
        }
        if (this.increaseCurses > 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsIncreaseCurse"), (object) this.increaseCurses.ToString()));
          stringBuilder1.Append("\n");
        }
        if (this.reduceAuras > 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsReduceAura"), (object) this.reduceAuras.ToString()));
          stringBuilder1.Append("\n");
        }
        if (this.reduceCurses > 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsReduceCurse"), (object) this.reduceCurses.ToString()));
          stringBuilder1.Append("\n");
        }
        int num3 = 0;
        if (this.damage > 0 && !this.damageSpecialValue1 && !this.damageSpecialValue2 && !this.damageSpecialValueGlobal)
        {
          ++num3;
          stringBuilder2.Append(this.ColorTextArray("damage", this.NumFormat(this.damagePreCalculated), this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType))));
          if (this.damage2 > 0 && this.damageType == this.damageType2 && (this.damage2SpecialValue1 || this.damage2SpecialValue2 || this.damage2SpecialValueGlobal))
          {
            stringBuilder2.Append("<space=-.3>");
            stringBuilder2.Append("+");
            stringBuilder2.Append(this.ColorTextArray("damage", "X", this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType))));
          }
        }
        if (this.damage2 > 0 && !this.damage2SpecialValue1 && !this.damage2SpecialValue2 && !this.damage2SpecialValueGlobal)
        {
          ++num3;
          stringBuilder2.Append(this.ColorTextArray("damage", this.NumFormat(this.damagePreCalculated2), this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType2))));
        }
        if (num3 > 0)
        {
          if (flag4 && num3 > 1)
          {
            stringBuilder2.Insert(0, str1);
            stringBuilder2.Insert(0, "\n");
          }
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDealDamage"), (object) stringBuilder2.ToString()));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if ((double) this.healSelfPerDamageDonePercent > 0.0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsHealSelfPerDamage"), (object) this.healSelfPerDamageDonePercent.ToString()));
          stringBuilder1.Append("\n");
        }
        if (this.selfHealthLoss != 0 && !this.selfHealthLossSpecialGlobal)
        {
          if (this.selfHealthLossSpecialValue1)
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsLoseHp"), (object) this.ColorTextArray("damage", "X HP")));
          else
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsLoseHp"), (object) this.ColorTextArray("damage", this.NumFormat(this.selfHealthLoss), "HP")));
          stringBuilder1.Append("\n");
        }
        if ((this.targetSide == Enums.CardTargetSide.Friend || this.targetSide == Enums.CardTargetSide.FriendNotSelf) && this.SpecialValueGlobal == Enums.CardSpecialValue.AuraCurseYours && (UnityEngine.Object) this.SpecialAuraCurseNameGlobal != (UnityEngine.Object) null && (double) this.SpecialValueModifierGlobal > 0.0)
        {
          flag1 = true;
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsShareYour"), (object) this.SpriteText(this.SpecialAuraCurseNameGlobal.ACName)));
          stringBuilder1.Append("\n");
        }
        if (!this.Damage2SpecialValue1 && !flag1 && (this.specialValueGlobal != Enums.CardSpecialValue.None || this.specialValue1 != Enums.CardSpecialValue.None || this.specialValue2 != Enums.CardSpecialValue.None))
        {
          if (!this.damageSpecialValueGlobal && !this.damageSpecialValue1 && !this.damageSpecialValue2)
          {
            if (this.targetSide == Enums.CardTargetSide.Self && (this.specialValueGlobal == Enums.CardSpecialValue.AuraCurseTarget || this.specialValueGlobal == Enums.CardSpecialValue.AuraCurseYours))
            {
              if ((UnityEngine.Object) this.specialAuraCurseNameGlobal != (UnityEngine.Object) null)
                str5 = this.SpriteText(this.specialAuraCurseNameGlobal.ACName);
              if (this.auraChargesSpecialValueGlobal)
              {
                if ((UnityEngine.Object) this.aura != (UnityEngine.Object) null)
                  str6 = this.SpriteText(this.aura.ACName);
                if ((UnityEngine.Object) this.aura2 != (UnityEngine.Object) null && this.auraCharges2SpecialValueGlobal)
                  str7 = this.SpriteText(this.aura2.ACName);
              }
              else if (this.curseChargesSpecialValueGlobal)
              {
                if ((UnityEngine.Object) this.curse != (UnityEngine.Object) null)
                  str6 = this.SpriteText(this.curse.ACName);
                if ((UnityEngine.Object) this.curse2 != (UnityEngine.Object) null && this.curseCharges2SpecialValueGlobal)
                  str7 = this.SpriteText(this.curse2.ACName);
              }
            }
            else if (this.specialValue1 == Enums.CardSpecialValue.AuraCurseTarget)
            {
              if ((UnityEngine.Object) this.specialAuraCurseName1 != (UnityEngine.Object) null)
                str5 = this.SpriteText(this.specialAuraCurseName1.ACName);
              if (this.auraChargesSpecialValue1)
              {
                if ((UnityEngine.Object) this.aura != (UnityEngine.Object) null)
                  str6 = this.SpriteText(this.aura.ACName);
                if ((UnityEngine.Object) this.aura2 != (UnityEngine.Object) null && this.auraCharges2SpecialValue1)
                  str7 = this.SpriteText(this.aura2.ACName);
              }
              else if (this.curseChargesSpecialValue1)
              {
                if ((UnityEngine.Object) this.curse != (UnityEngine.Object) null)
                  str6 = this.SpriteText(this.curse.ACName);
                if ((UnityEngine.Object) this.curse2 != (UnityEngine.Object) null && this.CurseCharges2SpecialValue1)
                  str7 = this.SpriteText(this.curse2.ACName);
              }
            }
            if (str5 != "" && str6 != "")
            {
              flag2 = true;
              if (str5 == str6)
              {
                if (this.specialValueGlobal == Enums.CardSpecialValue.AuraCurseTarget)
                {
                  if ((double) this.specialValueModifierGlobal == 100.0)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDoubleTarget"), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                  else if ((double) this.specialValueModifierGlobal == 200.0)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsTripleTarget"), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                  else if ((double) this.specialValueModifierGlobal < 100.0)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsLosePercentTarget"), (object) (float) (100.0 - (double) this.specialValueModifierGlobal), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                }
                else if (this.specialValueGlobal == Enums.CardSpecialValue.AuraCurseYours)
                {
                  if ((double) this.specialValueModifierGlobal == 100.0)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDoubleYour"), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                  else if ((double) this.specialValueModifierGlobal == 200.0)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsTripleYour"), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                  else if ((double) this.specialValueModifierGlobal < 100.0)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsLosePercentYour"), (object) (float) (100.0 - (double) this.specialValueModifierGlobal), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                }
                else if (this.specialValue1 == Enums.CardSpecialValue.AuraCurseTarget)
                {
                  if ((double) this.specialValueModifier1 == 100.0)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDoubleTarget"), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                  else if ((double) this.specialValueModifier1 == 200.0)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsTripleTarget"), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                  else if ((double) this.specialValueModifier1 < 100.0 && (UnityEngine.Object) this.healAuraCurseName != (UnityEngine.Object) null)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsLosePercentTarget"), (object) (float) (100.0 - (double) this.specialValueModifier1), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                  else
                    flag2 = false;
                }
                else if (this.specialValue1 == Enums.CardSpecialValue.AuraCurseYours)
                {
                  if ((double) this.specialValueModifier1 == 100.0)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDoubleYour"), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                  else if ((double) this.specialValueModifier1 == 200.0)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsTripleYour"), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                  else if ((double) this.specialValueModifier1 < 100.0)
                  {
                    stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsLosePercentYour"), (object) (float) (100.0 - (double) this.specialValueModifier1), (object) str5));
                    stringBuilder1.Append("\n");
                  }
                }
              }
              else
              {
                stringBuilder2.Append(str6);
                if ((double) this.specialValueModifier1 > 0.0)
                  num2 = this.specialValueModifier1 / 100f;
                if ((double) num2 > 0.0 && (double) num2 != 1.0)
                  str8 = "x " + num2.ToString();
                if (str8 != "")
                {
                  stringBuilder2.Append(" <c>");
                  stringBuilder2.Append(str8);
                  stringBuilder2.Append("</c>");
                }
                if (str7 != "")
                  stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsTransformIntoAnd"), (object) str5, (object) stringBuilder2.ToString(), (object) str7));
                else
                  stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsTransformInto"), (object) str5, (object) stringBuilder2.ToString()));
                stringBuilder1.Append("\n");
                stringBuilder2.Clear();
                num2 = 0.0f;
                str8 = "";
              }
            }
          }
        }
        if (this.energyRechargeSpecialValueGlobal)
          stringBuilder2.Append(this.ColorTextArray("aura", "X", this.SpriteText("energy")));
        int num4 = 0;
        if (this.damage > 0 && (this.damageSpecialValue1 || this.damageSpecialValueGlobal))
        {
          stringBuilder2.Append(this.ColorTextArray("damage", "X", this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType))));
          ++num4;
        }
        if (this.damage2 > 0 && (this.damageSpecialValue2 || this.damageSpecialValueGlobal))
        {
          stringBuilder2.Append(this.ColorTextArray("damage", "X", this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType2))));
          ++num4;
        }
        else if (this.damage2 > 0 && this.damage2SpecialValueGlobal && this.damageType != this.damageType2)
        {
          stringBuilder2.Append(this.ColorTextArray("damage", "X", this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType2))));
          ++num4;
        }
        if (this.damage2 > 0 && this.damage2SpecialValue1 && (this.damageSpecialValue1 || this.damageSpecialValue2 || this.damageSpecialValueGlobal))
        {
          stringBuilder2.Append(this.ColorTextArray("damage", "X", this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType2))));
          ++num4;
        }
        if (num4 > 0)
        {
          if (flag4 && num4 > 1)
          {
            stringBuilder2.Insert(0, str1);
            stringBuilder2.Insert(0, "\n");
          }
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDealDamage"), (object) stringBuilder2.ToString()));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        AuraCurseData auraCurseData = (AuraCurseData) null;
        if (!flag1 && !flag2)
        {
          int num5 = 0;
          if ((UnityEngine.Object) this.aura != (UnityEngine.Object) null && (this.auraChargesSpecialValue1 || this.auraChargesSpecialValueGlobal))
          {
            ++num5;
            stringBuilder2.Append(this.ColorTextArray("aura", "X", this.SpriteText(this.aura.ACName)));
            auraCurseData = this.aura;
          }
          if ((UnityEngine.Object) this.aura2 != (UnityEngine.Object) null && (this.auraCharges2SpecialValue1 || this.auraCharges2SpecialValueGlobal))
          {
            ++num5;
            if ((UnityEngine.Object) this.aura != (UnityEngine.Object) null && (UnityEngine.Object) this.aura == (UnityEngine.Object) this.aura2)
            {
              stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(this.aura.Id, this.auraCharges, character)), this.SpriteText(this.aura.ACName)));
              stringBuilder2.Append("+");
            }
            stringBuilder2.Append(this.ColorTextArray("aura", "X", this.SpriteText(this.aura2.ACName)));
            auraCurseData = this.aura2;
          }
          if ((UnityEngine.Object) this.aura3 != (UnityEngine.Object) null && (this.auraCharges3SpecialValue1 || this.auraCharges3SpecialValueGlobal))
          {
            ++num5;
            if ((UnityEngine.Object) this.aura != (UnityEngine.Object) null && (UnityEngine.Object) this.aura == (UnityEngine.Object) this.aura3)
            {
              stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(this.aura.Id, this.auraCharges, character)), this.SpriteText(this.aura.ACName)));
              stringBuilder2.Append("+");
            }
            if ((UnityEngine.Object) this.aura2 != (UnityEngine.Object) null && (UnityEngine.Object) this.aura == (UnityEngine.Object) this.aura3)
            {
              stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(this.aura2.Id, this.auraCharges2, character)), this.SpriteText(this.aura3.ACName)));
              stringBuilder2.Append("+");
            }
            stringBuilder2.Append(this.ColorTextArray("aura", "X", this.SpriteText(this.aura3.ACName)));
            auraCurseData = this.aura3;
          }
          if (num5 > 0)
          {
            if (this.targetSide == Enums.CardTargetSide.Self)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsGain"), (object) stringBuilder2.ToString()));
            else
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsGrant"), (object) stringBuilder2.ToString()));
            stringBuilder1.Append("\n");
            stringBuilder2.Clear();
          }
        }
        if (!flag1 && !flag2)
        {
          int num6 = 0;
          if ((UnityEngine.Object) this.curse != (UnityEngine.Object) null && (this.curseChargesSpecialValue1 || this.curseChargesSpecialValueGlobal))
          {
            ++num6;
            stringBuilder2.Append(this.ColorTextArray("curse", "X", this.SpriteText(this.curse.ACName)));
          }
          if ((UnityEngine.Object) this.curse2 != (UnityEngine.Object) null && (this.curseCharges2SpecialValue1 || this.curseCharges2SpecialValueGlobal))
          {
            ++num6;
            stringBuilder2.Append(this.ColorTextArray("curse", "X", this.SpriteText(this.curse2.ACName)));
          }
          if ((UnityEngine.Object) this.curse3 != (UnityEngine.Object) null && (this.curseCharges3SpecialValue1 || this.curseCharges3SpecialValueGlobal))
          {
            ++num6;
            stringBuilder2.Append(this.ColorTextArray("curse", "X", this.SpriteText(this.curse3.ACName)));
          }
          if (num6 > 0)
          {
            if (this.targetSide == Enums.CardTargetSide.Self)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsSuffer"), (object) stringBuilder2.ToString()));
            else
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsApply"), (object) stringBuilder2.ToString()));
            stringBuilder1.Append("\n");
            stringBuilder2.Clear();
          }
          int num7 = 0;
          if ((UnityEngine.Object) this.curseSelf != (UnityEngine.Object) null && (this.curseChargesSpecialValue1 || this.curseChargesSpecialValueGlobal))
          {
            ++num7;
            stringBuilder2.Append(this.ColorTextArray("curse", "X", this.SpriteText(this.curseSelf.ACName)));
          }
          if ((UnityEngine.Object) this.curseSelf2 != (UnityEngine.Object) null && (this.curseCharges2SpecialValue1 || this.curseCharges2SpecialValueGlobal))
          {
            ++num7;
            stringBuilder2.Append(this.ColorTextArray("curse", "X", this.SpriteText(this.curseSelf2.ACName)));
          }
          if ((UnityEngine.Object) this.curseSelf3 != (UnityEngine.Object) null && (this.curseCharges3SpecialValue1 || this.curseCharges3SpecialValueGlobal))
          {
            ++num7;
            stringBuilder2.Append(this.ColorTextArray("curse", "X", this.SpriteText(this.curseSelf3.ACName)));
          }
          if (num7 > 0)
          {
            if (this.targetSide == Enums.CardTargetSide.Self || (UnityEngine.Object) this.curseSelf != (UnityEngine.Object) null || (UnityEngine.Object) this.curseSelf2 != (UnityEngine.Object) null || (UnityEngine.Object) this.curseSelf3 != (UnityEngine.Object) null)
            {
              if (this.targetSide == Enums.CardTargetSide.Self)
              {
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsSuffer"), (object) stringBuilder2.ToString()));
                stringBuilder1.Append("\n");
              }
              else if (!flag3)
              {
                stringBuilder3.Append(string.Format(Texts.Instance.GetText("cardsYouSuffer"), (object) stringBuilder2.ToString()));
                stringBuilder3.Append("\n");
              }
              else
              {
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsYouSuffer"), (object) stringBuilder2.ToString()));
                stringBuilder1.Append("\n");
              }
            }
            else
            {
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsApply"), (object) stringBuilder2.ToString()));
              stringBuilder1.Append("\n");
            }
            stringBuilder2.Clear();
          }
        }
        int num8 = 0;
        if (this.heal > 0 && (this.healSpecialValue1 || this.healSpecialValueGlobal))
        {
          stringBuilder2.Append(this.ColorTextArray("heal", "X", this.SpriteText("heal")));
          num1 = num8 + 1;
        }
        if (stringBuilder2.Length > 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsHeal"), (object) stringBuilder2.ToString()));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        int num9 = 0;
        if (this.healSelf > 0 && (this.healSelfSpecialValue1 || this.healSelfSpecialValueGlobal))
        {
          stringBuilder2.Append(this.ColorTextArray("heal", "X", this.SpriteText("heal")));
          num1 = num9 + 1;
        }
        if (stringBuilder2.Length > 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsHealSelf"), (object) stringBuilder2.ToString()));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if (!flag1 && !flag2)
        {
          if ((double) this.specialValueModifierGlobal > 0.0)
            num2 = this.specialValueModifierGlobal / 100f;
          else if ((double) this.specialValueModifier1 > 0.0)
            num2 = this.specialValueModifier1 / 100f;
          if ((double) num2 > 0.0 && (double) num2 != 1.0)
            str8 = "x" + num2.ToString();
          if (str8 == "")
            str8 = "<space=-.1>";
          if (this.specialValue1 == Enums.CardSpecialValue.AuraCurseYours)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYour"), (object) this.SpriteText(this.SpecialAuraCurseName1.ACName), (object) str8));
          else if (this.specialValue1 == Enums.CardSpecialValue.AuraCurseTarget)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsTarget"), (object) this.SpriteText(this.SpecialAuraCurseName1.ACName), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.AuraCurseYours)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYour"), (object) this.SpriteText(this.SpecialAuraCurseNameGlobal.ACName), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.AuraCurseTarget)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsTarget"), (object) this.SpriteText(this.SpecialAuraCurseNameGlobal.ACName), (object) str8));
          if (this.specialValueGlobal == Enums.CardSpecialValue.HealthYours)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYourHp"), (object) str8));
          else if (this.specialValue1 == Enums.CardSpecialValue.HealthYours)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYourHp"), (object) str8));
          else if (this.specialValue1 == Enums.CardSpecialValue.HealthTarget)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsTargetHp"), (object) str8));
          if (this.specialValueGlobal == Enums.CardSpecialValue.SpeedYours)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYourSpeed"), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.SpeedTarget)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsTargetSpeed"), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.SpeedDifference || this.specialValue1 == Enums.CardSpecialValue.SpeedDifference)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsDifferenceSpeed"), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.DiscardedCards)
          {
            if (this.discardCardPlace == Enums.CardPlace.Vanish)
              stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYourVanishedCards"), (object) str8));
            else
              stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYourDiscardedCards"), (object) str8));
          }
          else if (this.specialValueGlobal == Enums.CardSpecialValue.VanishedCards)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYourVanishedCards"), (object) str8));
          if (this.specialValueGlobal == Enums.CardSpecialValue.CardsHand || this.specialValue1 == Enums.CardSpecialValue.CardsHand || this.specialValue2 == Enums.CardSpecialValue.CardsHand)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYourHand"), (object) this.SpriteText("card"), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.CardsDeck || this.specialValue1 == Enums.CardSpecialValue.CardsDeck || this.specialValue2 == Enums.CardSpecialValue.CardsDeck)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYourDeck"), (object) this.SpriteText("card"), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.CardsDiscard || this.specialValue1 == Enums.CardSpecialValue.CardsDiscard || this.specialValue2 == Enums.CardSpecialValue.CardsDiscard)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYourDiscard"), (object) this.SpriteText("card"), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.CardsVanish || this.specialValue1 == Enums.CardSpecialValue.CardsVanish || this.specialValue2 == Enums.CardSpecialValue.CardsVanish)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYourVanish"), (object) this.SpriteText("card"), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.CardsDeckTarget || this.specialValue1 == Enums.CardSpecialValue.CardsDeckTarget || this.specialValue2 == Enums.CardSpecialValue.CardsDeckTarget)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsTargetDeck"), (object) this.SpriteText("card"), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.CardsDiscardTarget || this.specialValue1 == Enums.CardSpecialValue.CardsDiscardTarget || this.specialValue2 == Enums.CardSpecialValue.CardsDiscardTarget)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsTargetDiscard"), (object) this.SpriteText("card"), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.CardsVanishTarget || this.specialValue1 == Enums.CardSpecialValue.CardsVanishTarget || this.specialValue2 == Enums.CardSpecialValue.CardsVanishTarget)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsTargetVanish"), (object) this.SpriteText("card"), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.MissingHealthYours || this.specialValue1 == Enums.CardSpecialValue.MissingHealthYours)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsYourMissingHealth"), (object) str8));
          else if (this.specialValueGlobal == Enums.CardSpecialValue.MissingHealthTarget || this.specialValue1 == Enums.CardSpecialValue.MissingHealthTarget)
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsXEqualsTargetMissingHealth"), (object) str8));
          if (stringBuilder2.Length > 0)
          {
            stringBuilder1.Append(str4);
            stringBuilder1.Append(stringBuilder2);
            stringBuilder1.Append(str3);
            stringBuilder1.Append("\n");
            stringBuilder2.Clear();
            if ((UnityEngine.Object) this.healAuraCurseName != (UnityEngine.Object) null)
            {
              if (this.targetSide == Enums.CardTargetSide.Self)
              {
                if (this.healAuraCurseName.IsAura)
                  stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsPurgeYour"), (object) this.SpriteText(this.healAuraCurseName.ACName)));
                else
                  stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDispelYour"), (object) this.SpriteText(this.healAuraCurseName.ACName)));
              }
              else if (this.healAuraCurseName.IsAura)
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsPurge"), (object) this.SpriteText(this.healAuraCurseName.ACName)));
              else
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDispel"), (object) this.SpriteText(this.healAuraCurseName.ACName)));
              stringBuilder1.Append("\n");
            }
            if ((UnityEngine.Object) this.healAuraCurseSelf != (UnityEngine.Object) null)
            {
              if (this.healAuraCurseSelf.IsAura)
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsPurgeYour"), (object) this.SpriteText(this.healAuraCurseSelf.ACName)));
              else
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDispelYour"), (object) this.SpriteText(this.healAuraCurseSelf.ACName)));
              stringBuilder1.Append("\n");
            }
          }
          num2 = 0.0f;
        }
        if (this.selfHealthLoss > 0 && this.selfHealthLossSpecialGlobal)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsYouLose"), (object) this.ColorTextArray("damage", "X", "HP")));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        int num10 = 0;
        if (this.damageSelf > 0)
        {
          ++num10;
          if (this.damageSpecialValueGlobal || this.damageSpecialValue1 || this.damageSpecialValue2)
            stringBuilder2.Append(this.ColorTextArray("damage", "X", this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType))));
          else
            stringBuilder2.Append(this.ColorTextArray("damage", this.NumFormat(this.damageSelfPreCalculated), this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType))));
        }
        if (this.damageSelf2 > 0)
        {
          ++num10;
          if (this.damage2SpecialValueGlobal || this.damage2SpecialValue1 || this.damage2SpecialValue2)
            stringBuilder2.Append(this.ColorTextArray("damage", "X", this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType2))));
          else
            stringBuilder2.Append(this.ColorTextArray("damage", this.NumFormat(this.damageSelfPreCalculated2), this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType2))));
        }
        if ((UnityEngine.Object) this.curseSelf != (UnityEngine.Object) null && this.curseCharges > 0)
        {
          ++num10;
          stringBuilder2.Append(this.ColorTextArray("curse", this.NumFormat(this.GetFinalAuraCharges(this.curseSelf.Id, this.curseCharges, character)), this.SpriteText(this.curseSelf.ACName)));
        }
        if ((UnityEngine.Object) this.curseSelf2 != (UnityEngine.Object) null && this.curseCharges2 > 0)
        {
          ++num10;
          stringBuilder2.Append(this.ColorTextArray("curse", this.NumFormat(this.GetFinalAuraCharges(this.curseSelf2.Id, this.curseCharges2, character)), this.SpriteText(this.curseSelf2.ACName)));
        }
        if ((UnityEngine.Object) this.curseSelf3 != (UnityEngine.Object) null && this.curseCharges3 > 0)
        {
          ++num10;
          stringBuilder2.Append(this.ColorTextArray("curse", this.NumFormat(this.GetFinalAuraCharges(this.curseSelf3.Id, this.curseCharges3, character)), this.SpriteText(this.curseSelf3.ACName)));
        }
        if (num10 > 0)
        {
          if (num10 > 2)
          {
            stringBuilder2.Insert(0, str1);
            stringBuilder2.Insert(0, "\n");
          }
          if (this.targetSide == Enums.CardTargetSide.Self)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsSuffer"), (object) stringBuilder2.ToString()));
            stringBuilder1.Append("\n");
          }
          else
          {
            stringBuilder3.Append(string.Format(Texts.Instance.GetText("cardsYouSuffer"), (object) stringBuilder2.ToString()));
            stringBuilder3.Append("\n");
          }
          stringBuilder2.Clear();
        }
        int num11 = 0;
        if (this.energyRecharge < 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsLoseHp"), (object) stringBuilder2.Append(this.ColorTextArray("system", this.NumFormat(Mathf.Abs(this.energyRecharge)), this.SpriteText("energy")))));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if (this.energyRecharge > 0)
        {
          ++num11;
          stringBuilder2.Append(this.ColorTextArray("system", this.NumFormat(this.energyRecharge), this.SpriteText("energy")));
        }
        if ((UnityEngine.Object) this.aura != (UnityEngine.Object) null && this.auraCharges > 0 && (UnityEngine.Object) this.aura != (UnityEngine.Object) auraCurseData)
        {
          ++num11;
          stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(this.aura.Id, this.auraCharges, character)), this.SpriteText(this.aura.ACName)));
        }
        if ((UnityEngine.Object) this.aura2 != (UnityEngine.Object) null && this.auraCharges2 > 0 && (UnityEngine.Object) this.aura2 != (UnityEngine.Object) auraCurseData)
        {
          ++num11;
          stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(this.aura2.Id, this.auraCharges2, character)), this.SpriteText(this.aura2.ACName)));
        }
        if ((UnityEngine.Object) this.aura3 != (UnityEngine.Object) null && this.auraCharges3 > 0 && (UnityEngine.Object) this.aura3 != (UnityEngine.Object) auraCurseData)
        {
          ++num11;
          stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(this.aura3.Id, this.auraCharges3, character)), this.SpriteText(this.aura3.ACName)));
        }
        if (num11 > 0)
        {
          if (num11 > 2)
          {
            stringBuilder2.Insert(0, str1);
            stringBuilder2.Insert(0, "\n");
          }
          if (this.targetSide == Enums.CardTargetSide.Self)
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsGain"), (object) stringBuilder2.ToString()));
          else
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsGrant"), (object) stringBuilder2.ToString()));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        int num12 = 0;
        if ((UnityEngine.Object) this.auraSelf != (UnityEngine.Object) null && this.auraCharges > 0)
        {
          ++num12;
          stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(this.auraSelf.Id, this.auraCharges, character)), this.SpriteText(this.auraSelf.ACName)));
        }
        if ((UnityEngine.Object) this.auraSelf2 != (UnityEngine.Object) null && this.auraCharges2 > 0)
        {
          ++num12;
          stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(this.auraSelf2.Id, this.auraCharges2, character)), this.SpriteText(this.auraSelf2.ACName)));
        }
        if ((UnityEngine.Object) this.auraSelf3 != (UnityEngine.Object) null && this.auraCharges3 > 0)
        {
          ++num12;
          stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(this.auraSelf3.Id, this.auraCharges3, character)), this.SpriteText(this.auraSelf3.ACName)));
        }
        if (!flag1)
        {
          if ((UnityEngine.Object) this.auraSelf != (UnityEngine.Object) null && (this.auraChargesSpecialValue1 || this.auraChargesSpecialValueGlobal))
          {
            ++num12;
            stringBuilder2.Append(this.ColorTextArray("aura", "X", this.SpriteText(this.auraSelf.ACName)));
          }
          if ((UnityEngine.Object) this.auraSelf2 != (UnityEngine.Object) null && (this.auraCharges2SpecialValue1 || this.auraCharges2SpecialValueGlobal))
          {
            ++num12;
            stringBuilder2.Append(this.ColorTextArray("aura", "X", this.SpriteText(this.auraSelf2.ACName)));
          }
          if ((UnityEngine.Object) this.auraSelf3 != (UnityEngine.Object) null && (this.auraCharges3SpecialValue1 || this.auraCharges3SpecialValueGlobal))
          {
            ++num12;
            stringBuilder2.Append(this.ColorTextArray("aura", "X", this.SpriteText(this.auraSelf3.ACName)));
          }
        }
        if (num12 > 0)
        {
          if (num12 > 2)
          {
            stringBuilder2.Insert(0, str1);
            stringBuilder2.Insert(0, "\n");
          }
          if (this.targetSide == Enums.CardTargetSide.Self)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsGain"), (object) stringBuilder2.ToString()));
            stringBuilder1.Append("\n");
          }
          else
          {
            stringBuilder3.Append(string.Format(Texts.Instance.GetText("cardsYouGain"), (object) stringBuilder2.ToString()));
            stringBuilder3.Append("\n");
          }
          stringBuilder2.Clear();
        }
        int num13 = 0;
        if (this.curseCharges > 0 && (UnityEngine.Object) this.curse != (UnityEngine.Object) null)
        {
          ++num13;
          stringBuilder2.Append(this.ColorTextArray("curse", this.NumFormat(this.GetFinalAuraCharges(this.curse.Id, this.curseCharges, character)), this.SpriteText(this.curse.ACName)));
        }
        if (this.curseCharges2 > 0 && (UnityEngine.Object) this.curse2 != (UnityEngine.Object) null)
        {
          ++num13;
          stringBuilder2.Append(this.ColorTextArray("curse", this.NumFormat(this.GetFinalAuraCharges(this.curse2.Id, this.curseCharges2, character)), this.SpriteText(this.curse2.ACName)));
        }
        if (this.curseCharges3 > 0 && (UnityEngine.Object) this.curse3 != (UnityEngine.Object) null)
        {
          ++num13;
          stringBuilder2.Append(this.ColorTextArray("curse", this.NumFormat(this.GetFinalAuraCharges(this.curse3.Id, this.curseCharges3, character)), this.SpriteText(this.curse3.ACName)));
        }
        if (num13 > 0)
        {
          if (num13 > 2)
          {
            stringBuilder2.Insert(0, str1);
            stringBuilder2.Insert(0, "\n");
          }
          if (this.targetSide == Enums.CardTargetSide.Self)
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsSuffer"), (object) stringBuilder2.ToString()));
          else
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsApply"), (object) stringBuilder2.ToString()));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if (this.heal > 0 && !this.healSpecialValue1 && !this.healSpecialValueGlobal)
        {
          stringBuilder2.Append(this.ColorTextArray("heal", this.NumFormat(this.healPreCalculated), this.SpriteText("heal")));
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsHeal"), (object) stringBuilder2.ToString()));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if (this.healSelf > 0 && !this.healSelfSpecialValue1 && !this.healSelfSpecialValueGlobal)
        {
          stringBuilder2.Append(this.ColorTextArray("heal", this.NumFormat(this.healSelfPreCalculated), this.SpriteText("heal")));
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsHealSelf"), (object) stringBuilder2.ToString()));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if (this.damageSides > 0)
          stringBuilder2.Append(this.ColorTextArray("damage", this.NumFormat(this.damageSidesPreCalculated), this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType))));
        if (this.damageSides2 > 0)
          stringBuilder2.Append(this.ColorTextArray("damage", this.NumFormat(this.damageSidesPreCalculated2), this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType2))));
        if (stringBuilder2.Length > 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsTargetSides"), (object) stringBuilder2.ToString()));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if (stringBuilder3.Length > 0)
          stringBuilder1.Append(stringBuilder3.ToString());
        if (this.killPet)
        {
          stringBuilder1.Append(Texts.Instance.GetText("killPet"));
          stringBuilder1.Append("\n");
        }
        if (this.damageEnergyBonus > 0 || this.healEnergyBonus > 0 || (UnityEngine.Object) this.acEnergyBonus != (UnityEngine.Object) null)
        {
          StringBuilder stringBuilder8 = new StringBuilder();
          stringBuilder8.Append("<line-height=40%><br></line-height>");
          StringBuilder stringBuilder9 = new StringBuilder();
          stringBuilder9.Append(str2);
          stringBuilder9.Append("[");
          stringBuilder9.Append(Texts.Instance.GetText("overchargeAcronym"));
          stringBuilder9.Append("]");
          stringBuilder9.Append(str3);
          stringBuilder9.Append("  ");
          if (this.damageEnergyBonus > 0)
          {
            stringBuilder8.Append(stringBuilder9.ToString());
            stringBuilder8.Append(string.Format(Texts.Instance.GetText("cardsDealDamage"), (object) this.ColorTextArray("damage", this.NumFormat(this.damageEnergyBonus), this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) this.damageType)))));
            stringBuilder8.Append("\n");
          }
          if ((UnityEngine.Object) this.acEnergyBonus != (UnityEngine.Object) null)
          {
            stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.acEnergyBonusQuantity), this.SpriteText(this.acEnergyBonus.ACName)));
            if ((UnityEngine.Object) this.acEnergyBonus2 != (UnityEngine.Object) null)
            {
              stringBuilder2.Append(" ");
              stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.acEnergyBonus2Quantity), this.SpriteText(this.acEnergyBonus2.ACName)));
            }
            if (this.acEnergyBonus.IsAura)
            {
              if (this.targetSide == Enums.CardTargetSide.Self)
              {
                stringBuilder8.Append(stringBuilder9.ToString());
                stringBuilder8.Append(string.Format(Texts.Instance.GetText("cardsGain"), (object) stringBuilder2.ToString()));
              }
              else
              {
                stringBuilder8.Append(stringBuilder9.ToString());
                stringBuilder8.Append(string.Format(Texts.Instance.GetText("cardsGrant"), (object) stringBuilder2.ToString()));
              }
            }
            else if (!this.acEnergyBonus.IsAura)
            {
              if (this.targetSide == Enums.CardTargetSide.Self)
              {
                stringBuilder8.Append(stringBuilder9.ToString());
                stringBuilder8.Append(string.Format(Texts.Instance.GetText("cardsSuffer"), (object) stringBuilder2.ToString()));
              }
              else
              {
                stringBuilder8.Append(stringBuilder9.ToString());
                stringBuilder8.Append(string.Format(Texts.Instance.GetText("cardsApply"), (object) stringBuilder2.ToString()));
              }
            }
            stringBuilder8.Append("\n");
          }
          if (this.healEnergyBonus > 0)
          {
            stringBuilder8.Append(stringBuilder9.ToString());
            stringBuilder8.Append(string.Format(Texts.Instance.GetText("cardsHeal"), (object) this.ColorTextArray("heal", this.NumFormat(this.healEnergyBonus), this.SpriteText("heal"))));
            stringBuilder8.Append("\n");
          }
          stringBuilder1.Append(stringBuilder8.ToString());
          stringBuilder2.Clear();
          stringBuilder8.Clear();
        }
        if (this.effectRepeat > 1 || this.effectRepeatMaxBonus > 0)
        {
          stringBuilder1.Append(str1);
          stringBuilder1.Append("<nobr><size=-.05><color=#1A505A>- ");
          if (this.effectRepeatMaxBonus > 0)
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsRepeatUpTo"), (object) this.effectRepeatMaxBonus));
          else if (this.effectRepeatTarget == Enums.EffectRepeatTarget.Chain)
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsRepeatChain"), (object) (this.effectRepeat - 1)));
          else if (this.effectRepeatTarget == Enums.EffectRepeatTarget.NoRepeat)
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsRepeatJump"), (object) (this.effectRepeat - 1)));
          else
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsRepeat"), (object) (this.effectRepeat - 1)));
          if (this.effectRepeatModificator != 0 && this.effectRepeatTarget != Enums.EffectRepeatTarget.Chain)
          {
            stringBuilder1.Append(" (");
            if (this.effectRepeatModificator > 0)
              stringBuilder1.Append("+");
            stringBuilder1.Append(this.effectRepeatModificator);
            stringBuilder1.Append("%)");
          }
          stringBuilder1.Append(" -</color></size></nobr>");
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if (this.ignoreBlock || this.ignoreBlock2)
        {
          stringBuilder1.Append(str1);
          stringBuilder1.Append(str2);
          stringBuilder1.Append(Texts.Instance.GetText("cardsIgnoreBlock"));
          stringBuilder1.Append(str3);
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if (this.goldGainQuantity != 0 && this.shardsGainQuantity != 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("customGainPerHeroAnd"), (object) this.ColorTextArray("aura", this.goldGainQuantity.ToString(), this.SpriteText("gold")), (object) this.ColorTextArray("aura", this.shardsGainQuantity.ToString(), this.SpriteText("dust"))));
          stringBuilder1.Append("\n");
        }
        else if (this.goldGainQuantity != 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("customGainPerHero"), (object) this.ColorTextArray("aura", this.goldGainQuantity.ToString(), this.SpriteText("gold"))));
          stringBuilder1.Append("\n");
        }
        else if (this.shardsGainQuantity != 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("customGainPerHero"), (object) this.ColorTextArray("aura", this.shardsGainQuantity.ToString(), this.SpriteText("dust"))));
          stringBuilder1.Append("\n");
        }
        if ((double) this.selfKillHiddenSeconds > 0.0)
        {
          stringBuilder1.Append(Texts.Instance.GetText("escapes"));
          stringBuilder1.Append("\n");
        }
      }
      else
      {
        ItemData itemData = !((UnityEngine.Object) this.item != (UnityEngine.Object) null) ? this.itemEnchantment : this.item;
        if (itemData.MaxHealth != 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemMaxHp"), (object) this.NumFormatItem(itemData.MaxHealth, true)));
          stringBuilder1.Append("\n");
        }
        if (itemData.ResistModified1 == Enums.DamageType.All)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemAllResistances"), (object) this.NumFormatItem(itemData.ResistModifiedValue1, true, true)));
          stringBuilder1.Append("\n");
        }
        int num14 = 0;
        int num15 = 0;
        if (itemData.ResistModified1 != Enums.DamageType.None && itemData.ResistModified1 != Enums.DamageType.All)
        {
          stringBuilder2.Append(this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) itemData.ResistModified1)));
          num15 = itemData.ResistModifiedValue1;
          ++num14;
        }
        if (itemData.ResistModified2 != Enums.DamageType.None && itemData.ResistModified2 != Enums.DamageType.All)
        {
          stringBuilder2.Append(this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) itemData.ResistModified2)));
          if (num15 == 0)
            num15 = itemData.ResistModifiedValue2;
          ++num14;
        }
        if (itemData.ResistModified3 != Enums.DamageType.None && itemData.ResistModified3 != Enums.DamageType.All)
        {
          stringBuilder2.Append(this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) itemData.ResistModified3)));
          if (num15 == 0)
            num15 = itemData.ResistModifiedValue3;
          ++num14;
        }
        if (num14 > 0)
        {
          if (num14 > 1)
            stringBuilder2.Append("\n");
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemXResistances"), (object) stringBuilder2.ToString(), (object) this.NumFormatItem(num15, true, true)));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if (itemData.CharacterStatModified == Enums.CharacterStat.Speed)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemSpeed"), (object) this.NumFormatItem(itemData.CharacterStatModifiedValue, true)));
          stringBuilder1.Append("\n");
        }
        if (itemData.CharacterStatModified == Enums.CharacterStat.EnergyTurn)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemEnergyRegeneration"), (object) this.SpriteText("energy"), (object) this.NumFormatItem(itemData.CharacterStatModifiedValue, true)));
          stringBuilder1.Append("\n");
        }
        if (itemData.CharacterStatModified2 == Enums.CharacterStat.Speed)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemSpeed"), (object) this.NumFormatItem(itemData.CharacterStatModifiedValue2, true)));
          stringBuilder1.Append("\n");
        }
        if (itemData.CharacterStatModified2 == Enums.CharacterStat.EnergyTurn)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemEnergyRegeneration"), (object) this.SpriteText("energy"), (object) this.NumFormatItem(itemData.CharacterStatModifiedValue2, true)));
          stringBuilder1.Append("\n");
        }
        if (itemData.DamageFlatBonus == Enums.DamageType.All)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemAllDamages"), (object) this.NumFormatItem(itemData.DamageFlatBonusValue, true)));
          stringBuilder1.Append("\n");
        }
        int num16 = 0;
        int num17 = 0;
        if (itemData.DamageFlatBonus != Enums.DamageType.None && itemData.DamageFlatBonus != Enums.DamageType.All)
        {
          stringBuilder2.Append(this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) itemData.DamageFlatBonus)));
          num17 = itemData.DamageFlatBonusValue;
          ++num16;
        }
        if (itemData.DamageFlatBonus2 != Enums.DamageType.None && itemData.DamageFlatBonus2 != Enums.DamageType.All)
        {
          stringBuilder2.Append(this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) itemData.DamageFlatBonus2)));
          ++num16;
        }
        if (itemData.DamageFlatBonus3 != Enums.DamageType.None && itemData.DamageFlatBonus3 != Enums.DamageType.All)
        {
          stringBuilder2.Append(this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) itemData.DamageFlatBonus3)));
          ++num16;
        }
        if (itemData.DamagePercentBonus == Enums.DamageType.All)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemAllDamages"), (object) this.NumFormatItem(Functions.FuncRoundToInt(itemData.DamagePercentBonusValue), true, true)));
          stringBuilder1.Append("\n");
        }
        if (num16 > 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemXDamages"), (object) stringBuilder2.ToString(), (object) this.NumFormatItem(num17, true)));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if (itemData.UseTheNextInsteadWhenYouPlay && (double) itemData.HealPercentBonus != 0.0)
        {
          string str11 = "";
          if (itemData.DestroyAfterUses > 1)
            str11 = "(" + itemData.DestroyAfterUses.ToString() + ") ";
          StringBuilder stringBuilder10 = new StringBuilder();
          stringBuilder10.Append("<size=-.15><color=#444>[");
          stringBuilder10.Append(Texts.Instance.GetText("itemTheNext"));
          stringBuilder10.Append("]</color></size><br>");
          stringBuilder2.Append("<color=#5E3016>");
          stringBuilder2.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) itemData.CastedCardType)));
          stringBuilder2.Append("</color>");
          stringBuilder1.Append(string.Format(stringBuilder10.ToString(), (object) str11, (object) stringBuilder2.ToString()));
          stringBuilder2.Clear();
          stringBuilder10.Clear();
        }
        if (itemData.HealFlatBonus != 0)
        {
          stringBuilder1.Append(this.SpriteText("heal"));
          stringBuilder1.Append(" ");
          stringBuilder1.Append(Functions.LowercaseFirst(Texts.Instance.GetText("healDone")));
          stringBuilder1.Append(this.NumFormatItem(itemData.HealFlatBonus, true));
          stringBuilder1.Append("\n");
        }
        if ((double) itemData.HealPercentBonus != 0.0)
        {
          stringBuilder1.Append(this.SpriteText("heal"));
          stringBuilder1.Append(" ");
          stringBuilder1.Append(Functions.LowercaseFirst(Texts.Instance.GetText("healDone")));
          stringBuilder1.Append(this.NumFormatItem(Functions.FuncRoundToInt(itemData.HealPercentBonus), true, true));
          stringBuilder1.Append("\n");
        }
        if (itemData.HealReceivedFlatBonus != 0)
        {
          stringBuilder1.Append(this.SpriteText("heal"));
          stringBuilder1.Append(" ");
          stringBuilder1.Append(Functions.LowercaseFirst(Texts.Instance.GetText("healTaken")));
          stringBuilder1.Append(this.NumFormatItem(Functions.FuncRoundToInt((float) itemData.HealReceivedFlatBonus), true));
          stringBuilder1.Append("\n");
        }
        if ((double) itemData.HealReceivedPercentBonus != 0.0)
        {
          stringBuilder1.Append(this.SpriteText("heal"));
          stringBuilder1.Append(" ");
          stringBuilder1.Append(Functions.LowercaseFirst(Texts.Instance.GetText("healTaken")));
          stringBuilder1.Append(this.NumFormatItem(Functions.FuncRoundToInt(itemData.HealReceivedPercentBonus), true, true));
          stringBuilder1.Append("\n");
        }
        if ((UnityEngine.Object) itemData.AuracurseBonus1 != (UnityEngine.Object) null && itemData.AuracurseBonusValue1 > 0 && (UnityEngine.Object) itemData.AuracurseBonus2 != (UnityEngine.Object) null && itemData.AuracurseBonusValue2 > 0 && itemData.AuracurseBonusValue1 == itemData.AuracurseBonusValue2)
        {
          stringBuilder2.Append(this.SpriteText(itemData.AuracurseBonus1.ACName));
          stringBuilder2.Append(this.SpriteText(itemData.AuracurseBonus2.ACName));
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemCharges"), (object) stringBuilder2.ToString(), (object) this.NumFormatItem(itemData.AuracurseBonusValue1, true)));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        else
        {
          if ((UnityEngine.Object) itemData.AuracurseBonus1 != (UnityEngine.Object) null && itemData.AuracurseBonusValue1 > 0)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemCharges"), (object) this.SpriteText(itemData.AuracurseBonus1.ACName), (object) this.NumFormatItem(itemData.AuracurseBonusValue1, true)));
            stringBuilder1.Append("\n");
          }
          if ((UnityEngine.Object) itemData.AuracurseBonus2 != (UnityEngine.Object) null && itemData.AuracurseBonusValue2 > 0)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemCharges"), (object) this.SpriteText(itemData.AuracurseBonus2.ACName), (object) this.NumFormatItem(itemData.AuracurseBonusValue2, true)));
            stringBuilder1.Append("\n");
          }
        }
        int num18 = 0;
        if ((UnityEngine.Object) itemData.AuracurseImmune1 != (UnityEngine.Object) null)
        {
          ++num18;
          stringBuilder2.Append(this.ColorTextArray("curse", this.SpriteText(itemData.AuracurseImmune1.Id)));
        }
        if ((UnityEngine.Object) itemData.AuracurseImmune2 != (UnityEngine.Object) null)
        {
          ++num18;
          stringBuilder2.Append(this.ColorTextArray("curse", this.SpriteText(itemData.AuracurseImmune2.Id)));
        }
        if (num18 > 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsImmuneTo"), (object) stringBuilder2.ToString()));
          stringBuilder1.Append("\n");
          stringBuilder2.Clear();
        }
        if (itemData.PercentDiscountShop != 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemDiscount"), (object) this.NumFormatItem(itemData.PercentDiscountShop, true, true)));
          stringBuilder1.Append("\n");
        }
        if (itemData.PercentRetentionEndGame != 0)
        {
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemDieRetain"), (object) this.NumFormatItem(itemData.PercentRetentionEndGame, true, true)));
          stringBuilder1.Append("\n");
        }
        if (itemData.AuracurseCustomString != "" && (UnityEngine.Object) itemData.AuracurseCustomAC != (UnityEngine.Object) null)
        {
          StringBuilder stringBuilder11 = new StringBuilder();
          if ((itemData.AuracurseCustomString == "itemCustomTextMaxChargesIncrasedOnEnemies" || itemData.AuracurseCustomString == "itemCustomTextMaxChargesIncrasedOnHeroes") && itemData.AuracurseCustomModValue1 > 0)
            stringBuilder11.Append("+");
          stringBuilder11.Append(itemData.AuracurseCustomModValue1);
          stringBuilder1.Append(string.Format(Texts.Instance.GetText(itemData.AuracurseCustomString), (object) this.ColorTextArray("aura", this.SpriteText(itemData.AuracurseCustomAC.Id)), (object) stringBuilder11.ToString(), (object) itemData.AuracurseCustomModValue2));
          stringBuilder1.Append("\n");
        }
        if (itemData.Id == "harleyrare")
        {
          stringBuilder1.Append(Texts.Instance.GetText("immortal"));
          stringBuilder1.Append("\n");
        }
        if (itemData.ModifiedDamageType != Enums.DamageType.None)
        {
          stringBuilder1.Append("<nobr>");
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsTransformDamage"), (object) this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) itemData.ModifiedDamageType))));
          stringBuilder1.Append("</nobr>");
          stringBuilder1.Append("\n");
        }
        if (itemData.IsEnchantment && (itemData.CastedCardType != Enums.CardType.None || (itemData.Activation == Enums.EventActivation.PreFinishCast || itemData.Activation == Enums.EventActivation.FinishCast || itemData.Activation == Enums.EventActivation.FinishFinishCast) && !itemData.EmptyHand))
        {
          if (itemData.CastedCardType != Enums.CardType.None)
          {
            stringBuilder2.Append("<color=#5E3016>");
            stringBuilder2.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) itemData.CastedCardType)));
            stringBuilder2.Append("</color>");
          }
          else
            stringBuilder2.Append(" <sprite name=cards>");
          if (itemData.UseTheNextInsteadWhenYouPlay)
          {
            if ((double) itemData.HealPercentBonus == 0.0)
            {
              string str12 = "";
              if (itemData.DestroyAfterUses > 1)
                str12 = "(" + itemData.DestroyAfterUses.ToString() + ") ";
              StringBuilder stringBuilder12 = new StringBuilder();
              stringBuilder12.Append("<size=-.15><color=#444>[");
              stringBuilder12.Append(Texts.Instance.GetText("itemTheNext"));
              stringBuilder12.Append("]</color></size><br>");
              stringBuilder1.Append(string.Format(stringBuilder12.ToString(), (object) str12, (object) stringBuilder2.ToString()));
            }
          }
          else
          {
            stringBuilder1.Append("<size=-.15>");
            stringBuilder1.Append("<color=#444>[");
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemWhenYouPlay"), (object) stringBuilder2.ToString()));
            stringBuilder1.Append("]</color>");
            stringBuilder1.Append("</size><br>");
          }
          stringBuilder2.Clear();
        }
        if (itemData.Activation != Enums.EventActivation.None && itemData.Activation != Enums.EventActivation.PreBeginCombat)
        {
          if (stringBuilder1.Length > 0)
            stringBuilder1.Append("<line-height=15%><br></line-height>");
          StringBuilder stringBuilder13 = new StringBuilder();
          if (itemData.TimesPerTurn == 1)
            stringBuilder13.Append(Texts.Instance.GetText("itemOncePerTurn"));
          else if (itemData.TimesPerTurn == 2)
            stringBuilder13.Append(Texts.Instance.GetText("itemTwicePerTurn"));
          else if (itemData.TimesPerTurn == 3)
            stringBuilder13.Append(Texts.Instance.GetText("itemThricePerTurn"));
          else if (itemData.TimesPerTurn == 4)
            stringBuilder13.Append(Texts.Instance.GetText("itemFourPerTurn"));
          else if (itemData.TimesPerTurn == 5)
            stringBuilder13.Append(Texts.Instance.GetText("itemFivePerTurn"));
          else if (itemData.TimesPerTurn == 6)
            stringBuilder13.Append(Texts.Instance.GetText("itemSixPerTurn"));
          else if (itemData.TimesPerTurn == 7)
            stringBuilder13.Append(Texts.Instance.GetText("itemSevenPerTurn"));
          else if (itemData.TimesPerTurn == 8)
            stringBuilder13.Append(Texts.Instance.GetText("itemEightPerTurn"));
          if (stringBuilder13.Length > 0)
          {
            stringBuilder1.Append("<size=-.15>");
            stringBuilder1.Append("<color=#444>[");
            stringBuilder1.Append(stringBuilder13.ToString());
            stringBuilder1.Append("]</color>");
            stringBuilder1.Append("</size><br>");
          }
          StringBuilder stringBuilder14 = new StringBuilder();
          if (itemData.Activation == Enums.EventActivation.BeginCombat)
            stringBuilder14.Append(Texts.Instance.GetText("itemCombatStart"));
          else if (itemData.Activation == Enums.EventActivation.BeginCombatEnd)
            stringBuilder14.Append(Texts.Instance.GetText("itemCombatEnd"));
          else if (itemData.Activation == Enums.EventActivation.BeginTurnAboutToDealCards || itemData.Activation == Enums.EventActivation.BeginTurnCardsDealt)
          {
            if (itemData.RoundCycle > 1)
              stringBuilder14.Append(string.Format(Texts.Instance.GetText("itemEveryNRounds"), (object) itemData.RoundCycle.ToString()));
            else if (itemData.ExactRound == 1)
              stringBuilder14.Append(Texts.Instance.GetText("itemFirstTurn"));
            else
              stringBuilder14.Append(Texts.Instance.GetText("itemEveryRound"));
          }
          else if (itemData.Activation == Enums.EventActivation.Damage)
            stringBuilder14.Append(Texts.Instance.GetText("itemDamageDone"));
          else if (itemData.Activation == Enums.EventActivation.Damaged)
          {
            if ((double) itemData.LowerOrEqualPercentHP < 100.0)
              stringBuilder14.Append(string.Format(Texts.Instance.GetText("itemWhenDamagedBelow"), (object) (itemData.LowerOrEqualPercentHP.ToString() + "%")));
            else
              stringBuilder14.Append(Texts.Instance.GetText("itemWhenDamaged"));
          }
          else if (itemData.Activation == Enums.EventActivation.Hitted)
            stringBuilder14.Append(Texts.Instance.GetText("itemWhenHitted"));
          else if (itemData.Activation == Enums.EventActivation.Block)
            stringBuilder14.Append(Texts.Instance.GetText("itemWhenBlock"));
          else if (itemData.Activation == Enums.EventActivation.Heal)
            stringBuilder14.Append(Texts.Instance.GetText("itemHealDoneAction"));
          else if (itemData.Activation == Enums.EventActivation.Healed)
            stringBuilder14.Append(Texts.Instance.GetText("itemWhenHealed"));
          else if (itemData.Activation == Enums.EventActivation.Evaded)
            stringBuilder14.Append(Texts.Instance.GetText("itemWhenEvaded"));
          else if (itemData.Activation == Enums.EventActivation.Evade)
            stringBuilder14.Append(Texts.Instance.GetText("itemWhenEvade"));
          else if (itemData.Activation == Enums.EventActivation.BeginRound)
          {
            if (itemData.RoundCycle > 1)
              stringBuilder14.Append(string.Format(Texts.Instance.GetText("itemEveryRoundRoundN"), (object) itemData.RoundCycle.ToString()));
            else
              stringBuilder14.Append(Texts.Instance.GetText("itemEveryRoundRound"));
          }
          else if (itemData.Activation == Enums.EventActivation.BeginTurn)
          {
            if (itemData.RoundCycle > 1)
              stringBuilder14.Append(string.Format(Texts.Instance.GetText("itemEveryNRounds"), (object) itemData.RoundCycle.ToString()));
            else
              stringBuilder14.Append(Texts.Instance.GetText("itemEveryRound"));
          }
          else if (itemData.Activation == Enums.EventActivation.Killed)
            stringBuilder14.Append(Texts.Instance.GetText("itemWhenKilled"));
          else if ((UnityEngine.Object) itemData.AuraCurseSetted != (UnityEngine.Object) null && itemData.AuraCurseNumForOneEvent == 0)
            stringBuilder14.Append(string.Format(Texts.Instance.GetText("itemWhenYouApply"), (object) this.ColorTextArray("curse", this.SpriteText(itemData.AuraCurseSetted.Id))));
          if (stringBuilder14.Length > 0)
          {
            stringBuilder1.Append("<size=-.15>");
            stringBuilder1.Append("<color=#444>[");
            stringBuilder1.Append(stringBuilder14.ToString());
            stringBuilder1.Append("]</color>");
            stringBuilder1.Append("</size><br>");
          }
          if (itemData.UsedEnergy)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemApplyForEnergyUsed"), (object) this.ColorTextArray("system", this.SpriteText("energy"))));
            stringBuilder1.Append("\n");
          }
          if (itemData.EmptyHand)
          {
            stringBuilder1.Append(Texts.Instance.GetText("itemWhenHandEmpty"));
            stringBuilder1.Append(":<br>");
          }
          if (itemData.ChanceToDispel > 0 && itemData.ChanceToDispelNum > 0)
          {
            if (itemData.ChanceToDispel < 100)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemChanceToDispel"), (object) this.ColorTextArray("aura", this.NumFormatItem(itemData.ChanceToDispel, percent: true)), (object) this.ColorTextArray("curse", this.NumFormatItem(itemData.ChanceToDispelNum))));
            else
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDispel"), (object) this.ColorTextArray("curse", this.NumFormatItem(itemData.ChanceToDispelNum))));
            stringBuilder1.Append("\n");
          }
          if (!itemData.IsEnchantment && itemData.CastedCardType != Enums.CardType.None)
          {
            stringBuilder2.Append("<color=#5E3016>");
            stringBuilder2.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) itemData.CastedCardType)));
            stringBuilder2.Append("</color>");
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemWhenYouPlay"), (object) stringBuilder2.ToString()));
            stringBuilder2.Clear();
            stringBuilder1.Append(":\n");
          }
          else if (!itemData.IsEnchantment && itemData.CastedCardType == Enums.CardType.None && (itemData.Activation == Enums.EventActivation.PreFinishCast || itemData.Activation == Enums.EventActivation.FinishCast || itemData.Activation == Enums.EventActivation.FinishFinishCast) && !itemData.EmptyHand)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemWhenYouPlay"), (object) "  <sprite name=cards>"));
            stringBuilder1.Append(":\n");
          }
          if (itemData.DrawCards > 0)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDraw"), (object) this.ColorTextArray("", this.NumFormat(itemData.DrawCards), this.SpriteText("card"))));
            stringBuilder1.Append("<br>");
          }
          if (itemData.HealQuantity > 0)
          {
            stringBuilder2.Append("<color=#111111>");
            stringBuilder2.Append(this.NumFormatItem(itemData.HealQuantity, true));
            stringBuilder2.Append("</color>");
            if (itemData.ItemTarget == Enums.ItemTarget.AllHero)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemRecoverHeroes"), (object) stringBuilder2.ToString()));
            else if (itemData.ItemTarget == Enums.ItemTarget.Self)
            {
              if (itemData.Activation == Enums.EventActivation.Killed)
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemResurrectHP"), (object) stringBuilder2.ToString()));
              else
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemRecoverSelf"), (object) stringBuilder2.ToString()));
            }
            else if (itemData.ItemTarget == Enums.ItemTarget.AllEnemy)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemRecoverSelf"), (object) stringBuilder2.ToString()));
            stringBuilder1.Append("<br>");
            stringBuilder2.Clear();
          }
          if (itemData.EnergyQuantity > 0 && itemData.ItemTarget == Enums.ItemTarget.Self)
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsGain"), (object) this.ColorTextArray("system", this.NumFormat(itemData.EnergyQuantity), this.SpriteText("energy"))));
          if (itemData.HealPercentQuantity > 0)
          {
            stringBuilder2.Append("<color=#111111>");
            stringBuilder2.Append(this.NumFormatItem(itemData.HealPercentQuantity, true, true));
            stringBuilder2.Append("</color>");
            if (itemData.ItemTarget == Enums.ItemTarget.AllHero)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemRecoverHeroes"), (object) stringBuilder2.ToString()));
            else if (itemData.ItemTarget == Enums.ItemTarget.Self)
            {
              if (itemData.Activation == Enums.EventActivation.Killed)
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemResurrectHP"), (object) stringBuilder2.ToString()));
              else
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemRecoverSelf"), (object) stringBuilder2.ToString()));
            }
            else if (itemData.ItemTarget == Enums.ItemTarget.LowestFlatHpEnemy)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemRecoverLowestHPMonster"), (object) stringBuilder2.ToString()));
            else if (itemData.ItemTarget == Enums.ItemTarget.LowestFlatHpHero)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemRecoverLowestHPHero"), (object) stringBuilder2.ToString()));
            else if (itemData.ItemTarget == Enums.ItemTarget.AllEnemy)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemRecoverSelf"), (object) stringBuilder2.ToString()));
            stringBuilder1.Append("<br>");
            stringBuilder2.Clear();
          }
          if (itemData.HealPercentQuantitySelf < 0)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsYouLose"), (object) this.ColorTextArray("damage", this.NumFormat(Mathf.Abs(itemData.HealPercentQuantitySelf)), "<space=-.1>% HP")));
            stringBuilder1.Append("<br>");
          }
          if (itemData.DamageToTarget > 0)
          {
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsDealDamage"), (object) this.ColorTextArray("damage", this.NumFormat(this.enchantDamagePreCalculated), this.SpriteText(Enum.GetName(typeof (Enums.DamageType), (object) itemData.DamageToTargetType)))));
            stringBuilder1.Append("\n");
          }
          int num19 = 0;
          bool flag5 = true;
          if ((UnityEngine.Object) itemData.AuracurseGain1 != (UnityEngine.Object) null && itemData.AuracurseGainValue1 > 0)
          {
            ++num19;
            if (itemData.NotShowCharacterBonus)
              stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(itemData.AuracurseGainValue1), this.SpriteText(itemData.AuracurseGain1.Id)));
            else
              stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(itemData.AuracurseGain1.Id, itemData.AuracurseGainValue1, character)), this.SpriteText(itemData.AuracurseGain1.Id)));
            if (!itemData.AuracurseGain1.IsAura)
              flag5 = false;
          }
          if ((UnityEngine.Object) itemData.AuracurseGain2 != (UnityEngine.Object) null && itemData.AuracurseGainValue2 > 0)
          {
            ++num19;
            if (itemData.NotShowCharacterBonus)
              stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(itemData.AuracurseGainValue2), this.SpriteText(itemData.AuracurseGain2.Id)));
            else
              stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(itemData.AuracurseGain2.Id, itemData.AuracurseGainValue2, character)), this.SpriteText(itemData.AuracurseGain2.Id)));
          }
          if ((UnityEngine.Object) itemData.AuracurseGain3 != (UnityEngine.Object) null && itemData.AuracurseGainValue3 > 0)
          {
            ++num19;
            if (itemData.NotShowCharacterBonus)
              stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(itemData.AuracurseGainValue3), this.SpriteText(itemData.AuracurseGain3.Id)));
            else
              stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(itemData.AuracurseGain3.Id, itemData.AuracurseGainValue3, character)), this.SpriteText(itemData.AuracurseGain3.Id)));
          }
          int num20;
          if (num19 > 0)
          {
            if (itemData.ItemTarget == Enums.ItemTarget.Self)
            {
              if (itemData.HealQuantity > 0 || itemData.EnergyQuantity > 0 || itemData.HealPercentQuantity > 0)
              {
                StringBuilder stringBuilder15 = new StringBuilder();
                if (flag5)
                  stringBuilder15.Append(string.Format(Texts.Instance.GetText("cardsAnd"), (object) stringBuilder1.ToString(), (object) string.Format(Functions.LowercaseFirst(Texts.Instance.GetText("cardsGain")), (object) stringBuilder2.ToString())));
                else
                  stringBuilder15.Append(string.Format(Texts.Instance.GetText("cardsAnd"), (object) stringBuilder1.ToString(), (object) string.Format(Functions.LowercaseFirst(Texts.Instance.GetText("cardsSuffer")), (object) stringBuilder2.ToString())));
                stringBuilder1.Clear();
                stringBuilder1.Append(stringBuilder15.ToString());
              }
              else if (flag5)
              {
                string str13 = stringBuilder1.ToString();
                if (str13.Length > 8 && str13.Substring(str13.Length - 9) == "<c>, </c>")
                  stringBuilder1.Append(string.Format(Functions.LowercaseFirst(Texts.Instance.GetText("cardsGain")), (object) stringBuilder2.ToString()));
                else
                  stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsGain"), (object) stringBuilder2.ToString()));
              }
              else if (stringBuilder1.ToString().Substring(stringBuilder1.ToString().Length - 9) == "<c>, </c>")
                stringBuilder1.Append(string.Format(Functions.LowercaseFirst(Texts.Instance.GetText("cardsSuffer")), (object) stringBuilder2.ToString()));
              else
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsSuffer"), (object) stringBuilder2.ToString()));
            }
            else if (itemData.ItemTarget == Enums.ItemTarget.AllEnemy)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemApplyEnemies"), (object) stringBuilder2.ToString()));
            else if (itemData.ItemTarget == Enums.ItemTarget.AllHero)
            {
              if (this.cardClass == Enums.CardClass.Monster)
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemApplyHeroesFromMonster"), (object) stringBuilder2.ToString()));
              else
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemApplyHeroes"), (object) stringBuilder2.ToString()));
            }
            else if (itemData.ItemTarget == Enums.ItemTarget.RandomHero)
            {
              if ((UnityEngine.Object) itemData.AuraCurseSetted != (UnityEngine.Object) null && itemData.AuraCurseNumForOneEvent > 0)
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemForEveryCharge"), (object) this.ColorTextArray("curse", this.SpriteText(itemData.AuraCurseSetted.Id))));
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemApplyRandomHero"), (object) stringBuilder2.ToString()));
            }
            else if (itemData.ItemTarget == Enums.ItemTarget.RandomEnemy)
            {
              if ((UnityEngine.Object) itemData.AuraCurseSetted != (UnityEngine.Object) null && itemData.AuraCurseNumForOneEvent > 0)
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemForEveryCharge"), (object) this.ColorTextArray("curse", this.SpriteText(itemData.AuraCurseSetted.Id))));
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemApplyRandomEnemy"), (object) stringBuilder2.ToString()));
            }
            else if (itemData.ItemTarget == Enums.ItemTarget.HighestFlatHpEnemy)
            {
              if ((UnityEngine.Object) itemData.AuraCurseSetted != (UnityEngine.Object) null && itemData.AuraCurseNumForOneEvent > 0)
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemForEveryCharge"), (object) this.ColorTextArray("curse", this.SpriteText(itemData.AuraCurseSetted.Id))));
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemApplyHighestFlatHpEnemy"), (object) stringBuilder2.ToString()));
            }
            else if (itemData.ItemTarget == Enums.ItemTarget.LowestFlatHpEnemy)
            {
              if ((UnityEngine.Object) itemData.AuraCurseSetted != (UnityEngine.Object) null && itemData.AuraCurseNumForOneEvent > 0)
              {
                StringBuilder stringBuilder16 = stringBuilder1;
                string text = Texts.Instance.GetText("itemForEveryCharge");
                string[] strArray = new string[2];
                string str14;
                if (itemData.AuraCurseNumForOneEvent <= 1)
                {
                  str14 = "";
                }
                else
                {
                  num20 = itemData.AuraCurseNumForOneEvent;
                  str14 = num20.ToString();
                }
                strArray[0] = str14;
                strArray[1] = this.SpriteText(itemData.AuraCurseSetted.Id);
                string str15 = this.ColorTextArray("curse", strArray);
                string str16 = string.Format(text, (object) str15);
                stringBuilder16.Append(str16);
              }
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemApplyLowestFlatHpEnemy"), (object) stringBuilder2.ToString()));
            }
            else if (itemData.ItemTarget == Enums.ItemTarget.HighestFlatHpHero)
            {
              if ((UnityEngine.Object) itemData.AuraCurseSetted != (UnityEngine.Object) null && itemData.AuraCurseNumForOneEvent > 0)
              {
                StringBuilder stringBuilder17 = stringBuilder1;
                string text = Texts.Instance.GetText("itemForEveryCharge");
                string[] strArray = new string[2];
                string str17;
                if (itemData.AuraCurseNumForOneEvent <= 1)
                {
                  str17 = "";
                }
                else
                {
                  num20 = itemData.AuraCurseNumForOneEvent;
                  str17 = num20.ToString();
                }
                strArray[0] = str17;
                strArray[1] = this.SpriteText(itemData.AuraCurseSetted.Id);
                string str18 = this.ColorTextArray("curse", strArray);
                string str19 = string.Format(text, (object) str18);
                stringBuilder17.Append(str19);
              }
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemApplyHighestFlatHpHero"), (object) stringBuilder2.ToString()));
            }
            else if (itemData.ItemTarget == Enums.ItemTarget.LowestFlatHpHero)
            {
              if ((UnityEngine.Object) itemData.AuraCurseSetted != (UnityEngine.Object) null && itemData.AuraCurseNumForOneEvent > 0)
              {
                StringBuilder stringBuilder18 = stringBuilder1;
                string text = Texts.Instance.GetText("itemForEveryCharge");
                string[] strArray = new string[2];
                string str20;
                if (itemData.AuraCurseNumForOneEvent <= 1)
                {
                  str20 = "";
                }
                else
                {
                  num20 = itemData.AuraCurseNumForOneEvent;
                  str20 = num20.ToString();
                }
                strArray[0] = str20;
                strArray[1] = this.SpriteText(itemData.AuraCurseSetted.Id);
                string str21 = this.ColorTextArray("curse", strArray);
                string str22 = string.Format(text, (object) str21);
                stringBuilder18.Append(str22);
              }
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemApplyLowestFlatHpHero"), (object) stringBuilder2.ToString()));
            }
            else if (this.targetSide == Enums.CardTargetSide.Enemy || itemData.ItemTarget == Enums.ItemTarget.CurrentTarget)
            {
              if ((UnityEngine.Object) itemData.AuraCurseSetted != (UnityEngine.Object) null && itemData.AuraCurseNumForOneEvent > 0)
              {
                StringBuilder stringBuilder19 = stringBuilder1;
                string text = Texts.Instance.GetText("itemApplyForEvery");
                string[] strArray = new string[2];
                string str23;
                if (itemData.AuraCurseNumForOneEvent <= 1)
                {
                  str23 = "";
                }
                else
                {
                  num20 = itemData.AuraCurseNumForOneEvent;
                  str23 = num20.ToString();
                }
                strArray[0] = str23;
                strArray[1] = this.SpriteText(itemData.AuraCurseSetted.Id);
                string str24 = this.ColorTextArray("curse", strArray);
                string str25 = stringBuilder2.ToString();
                string str26 = string.Format(text, (object) str24, (object) str25);
                stringBuilder19.Append(str26);
              }
              else if (itemData.ItemTarget == Enums.ItemTarget.Random)
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemApplyRandom"), (object) stringBuilder2.ToString()));
              else
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsApply"), (object) stringBuilder2.ToString()));
            }
            else
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsGrant"), (object) stringBuilder2.ToString()));
            stringBuilder1.Append("\n");
            stringBuilder2.Clear();
          }
          int num21 = 0;
          bool flag6 = true;
          if ((UnityEngine.Object) itemData.AuracurseGainSelf1 != (UnityEngine.Object) null && itemData.AuracurseGainSelfValue1 > 0)
          {
            ++num21;
            stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(itemData.AuracurseGainSelf1.Id, itemData.AuracurseGainSelfValue1, character)), this.SpriteText(itemData.AuracurseGainSelf1.Id)));
            if (!itemData.AuracurseGainSelf1.IsAura)
              flag6 = false;
          }
          if ((UnityEngine.Object) itemData.AuracurseGainSelf2 != (UnityEngine.Object) null && itemData.AuracurseGainSelfValue2 > 0)
          {
            ++num21;
            stringBuilder2.Append(this.ColorTextArray("aura", this.NumFormat(this.GetFinalAuraCharges(itemData.AuracurseGainSelf2.Id, itemData.AuracurseGainSelfValue2, character)), this.SpriteText(itemData.AuracurseGainSelf2.Id)));
          }
          if (num21 > 0)
          {
            if (itemData.HealQuantity > 0 || itemData.EnergyQuantity > 0 || itemData.HealPercentQuantity > 0)
            {
              StringBuilder stringBuilder20 = new StringBuilder();
              if (flag6)
                stringBuilder20.Append(string.Format(Texts.Instance.GetText("cardsAnd"), (object) stringBuilder1.ToString(), (object) string.Format(Functions.LowercaseFirst(Texts.Instance.GetText("cardsGain")), (object) stringBuilder2.ToString())));
              else
                stringBuilder20.Append(string.Format(Texts.Instance.GetText("cardsAnd"), (object) stringBuilder1.ToString(), (object) string.Format(Functions.LowercaseFirst(Texts.Instance.GetText("cardsSuffer")), (object) stringBuilder2.ToString())));
              stringBuilder1.Clear();
              stringBuilder1.Append(stringBuilder20.ToString());
            }
            else if (flag6)
            {
              if (stringBuilder1.ToString().Substring(stringBuilder1.ToString().Length - 9) == "<c>, </c>")
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsGain"), (object) stringBuilder2.ToString()));
              else
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsGain"), (object) stringBuilder2.ToString()));
            }
            else if (stringBuilder1.ToString().Substring(stringBuilder1.ToString().Length - 9) == "<c>, </c>")
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsSuffer"), (object) stringBuilder2.ToString()));
            else
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsSuffer"), (object) stringBuilder2.ToString()));
            stringBuilder1.Append("\n");
            stringBuilder2.Clear();
          }
          if (itemData.CardNum > 0)
          {
            string str27;
            if ((UnityEngine.Object) itemData.CardToGain != (UnityEngine.Object) null)
            {
              if (itemData.CardNum > 1)
                stringBuilder2.Append(this.ColorTextArray("", this.NumFormat(itemData.CardNum), this.SpriteText("card")));
              else
                stringBuilder2.Append(this.SpriteText("card"));
              CardData cardData = Globals.Instance.GetCardData(itemData.CardToGain.Id, false);
              if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
              {
                stringBuilder2.Append(this.ColorFromCardDataRarity(cardData));
                stringBuilder2.Append(cardData.CardName);
                stringBuilder2.Append("</color>");
              }
              str27 = stringBuilder2.ToString();
              stringBuilder2.Clear();
            }
            else
            {
              if (itemData.CardNum > 1)
                stringBuilder2.Append(this.ColorTextArray("", this.NumFormat(itemData.CardNum), this.SpriteText("card")));
              else
                stringBuilder2.Append(this.SpriteText("card"));
              if (itemData.CardToGainType != Enums.CardType.None)
              {
                stringBuilder2.Append("<color=#5E3016>");
                stringBuilder2.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) itemData.CardToGainType)));
                stringBuilder2.Append("</color>");
              }
              str27 = stringBuilder2.ToString();
              stringBuilder2.Clear();
            }
            string str28 = "";
            if (itemData.Permanent)
            {
              if (itemData.Vanish)
              {
                if (itemData.CostZero)
                  str28 = string.Format(Texts.Instance.GetText("cardsAddCostVanish"), (object) 0);
                else if (itemData.CostReduction > 0)
                  str28 = string.Format(Texts.Instance.GetText("cardsAddCostReducedVanish"), (object) this.NumFormatItem(itemData.CostReduction, true));
              }
              else if (itemData.CostZero)
                str28 = string.Format(Texts.Instance.GetText("cardsAddCost"), (object) 0);
              else if (itemData.CostReduction > 0)
                str28 = string.Format(Texts.Instance.GetText("cardsAddCostReduced"), (object) this.NumFormatItem(itemData.CostReduction, true));
            }
            else if (itemData.Vanish)
            {
              if (itemData.CostZero)
                str28 = string.Format(Texts.Instance.GetText("cardsAddCostVanishTurn"), (object) 0);
              else if (itemData.CostReduction > 0)
                str28 = string.Format(Texts.Instance.GetText("cardsAddCostReducedVanishTurn"), (object) this.NumFormatItem(itemData.CostReduction, true));
            }
            else if (itemData.CostZero)
              str28 = string.Format(Texts.Instance.GetText("cardsAddCostTurn"), (object) 0);
            else if (itemData.CostReduction > 0)
              str28 = string.Format(Texts.Instance.GetText("cardsAddCostReducedTurn"), (object) this.NumFormatItem(itemData.CostReduction, true));
            if (itemData.DuplicateActive)
            {
              if (itemData.CardPlace == Enums.CardPlace.Hand)
                stringBuilder1.Append(Texts.Instance.GetText("cardsDuplicateHand"));
            }
            else if (itemData.CardPlace == Enums.CardPlace.RandomDeck)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsIDShuffleDeck"), (object) str27));
            else if (itemData.CardPlace == Enums.CardPlace.Cast)
            {
              if ((UnityEngine.Object) itemData.CardToGain != (UnityEngine.Object) null)
              {
                CardData cardData = Globals.Instance.GetCardData(itemData.CardToGain.Id, false);
                if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
                {
                  stringBuilder2.Append(this.ColorFromCardDataRarity(cardData));
                  stringBuilder2.Append(cardData.CardName);
                  stringBuilder2.Append("</color>");
                }
              }
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsCast"), (object) stringBuilder2.ToString()));
              stringBuilder2.Clear();
            }
            else if (itemData.CardPlace == Enums.CardPlace.Hand)
            {
              if (itemData.CardNum > 1)
                stringBuilder2.Append(this.ColorTextArray("", this.NumFormat(itemData.CardNum), this.SpriteText("card")));
              else
                stringBuilder2.Append(this.SpriteText("card"));
              if ((UnityEngine.Object) itemData.CardToGain != (UnityEngine.Object) null)
              {
                CardData cardData = Globals.Instance.GetCardData(itemData.CardToGain.Id, false);
                if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
                {
                  stringBuilder2.Append(this.ColorFromCardDataRarity(cardData));
                  stringBuilder2.Append(cardData.CardName);
                  stringBuilder2.Append("</color>");
                }
              }
              stringBuilder2.Clear();
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("cardsIDPlaceHand"), (object) str27));
            }
            if (str28 != "")
            {
              stringBuilder1.Append(" ");
              stringBuilder1.Append(str2);
              stringBuilder1.Append(str28);
              stringBuilder1.Append(str3);
            }
            if (itemData.CardsReduced == 0)
              stringBuilder1.Append("\n");
            else
              stringBuilder1.Append(" ");
          }
          if (itemData.CardsReduced > 0)
          {
            num20 = itemData.CardsReduced;
            string str29 = "<color=#5E3016>" + num20.ToString() + "</color>";
            string str30 = "<color=#5E3016>" + Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) itemData.CardToReduceType)) + "</color>";
            if (itemData.CardToReduceType == Enums.CardType.None)
              str30 = "  <sprite name=cards>";
            num20 = itemData.CostReduceReduction;
            string str31 = "<color=#111111>" + num20.ToString() + "</color>";
            string str32 = "<space=-.2>";
            if (itemData.CostReduceEnergyRequirement > 0)
              str32 = "<color=#444><size=-.2>" + string.Format(Texts.Instance.GetText("itemReduceCost"), (object) itemData.CostReduceEnergyRequirement) + "</size></color>";
            if (itemData.CostReducePermanent && itemData.ReduceHighestCost)
            {
              string str33;
              if (itemData.CardsReduced == 1)
              {
                str33 = "";
              }
              else
              {
                num20 = itemData.CardsReduced;
                str33 = "<color=#111111>(" + num20.ToString() + ")</color> ";
              }
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemReduceHighestPermanent"), (object) str33, (object) str30, (object) str31, (object) str32));
            }
            else if (itemData.CostReducePermanent)
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemReduce"), (object) str29, (object) str30, (object) str31, (object) str32));
            else if (itemData.ReduceHighestCost)
            {
              string str34;
              if (itemData.CardsReduced == 1)
              {
                str34 = "";
              }
              else
              {
                num20 = itemData.CardsReduced;
                str34 = "<color=#111111>(" + num20.ToString() + ")</color> ";
              }
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemReduceHighestTurn"), (object) str34, (object) str30, (object) str31, (object) str32));
            }
            else
              stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemReduceTurn"), (object) str29, (object) str30, (object) str31, (object) str32));
            stringBuilder1.Append("\n");
          }
        }
        if (itemData.DestroyStartOfTurn || itemData.DestroyEndOfTurn)
        {
          stringBuilder1.Append("<voffset=-.1><size=-.05><color=#1A505A>- ");
          stringBuilder1.Append(Texts.Instance.GetText("itemDestroyStartTurn"));
          stringBuilder1.Append(" -</color></size>");
        }
        if (itemData.DestroyAfterUses > 0 && !itemData.UseTheNextInsteadWhenYouPlay)
        {
          stringBuilder1.Append("<nobr><size=-.05><color=#1A505A>- ");
          if (itemData.DestroyAfterUses > 1)
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("itemLastUses"), (object) itemData.DestroyAfterUses));
          else
            stringBuilder1.Append(Texts.Instance.GetText("itemLastUse"));
          stringBuilder1.Append(" -</color></size></nobr>");
        }
        if (itemData.TimesPerCombat > 0)
        {
          stringBuilder1.Append("<nobr><size=-.05><color=#1A505A>- ");
          if (itemData.TimesPerCombat == 1)
            stringBuilder1.Append(Texts.Instance.GetText("itemOncePerCombat"));
          else if (itemData.TimesPerCombat == 2)
            stringBuilder1.Append(Texts.Instance.GetText("itemTwicePerCombat"));
          else if (itemData.TimesPerCombat == 3)
            stringBuilder1.Append(Texts.Instance.GetText("itemThricePerCombat"));
          stringBuilder1.Append(" -</color></size></nobr>");
        }
        if (itemData.PassSingleAndCharacterRolls)
        {
          stringBuilder1.Append(Texts.Instance.GetText("cardsPassEventRoll"));
          stringBuilder1.Append("\n");
        }
      }
      stringBuilder1.Replace("<c>", "<color=#5E3016>");
      stringBuilder1.Replace("</c>", "</color>");
      stringBuilder1.Replace("<nb>", "<nobr>");
      stringBuilder1.Replace("</nb>", "</nobr>");
      stringBuilder1.Replace("<br1>", "<br><line-height=15%><br></line-height>");
      stringBuilder1.Replace("<br2>", "<br><line-height=30%><br></line-height>");
      stringBuilder1.Replace("<br3>", "<br><line-height=50%><br></line-height>");
      this.descriptionNormalized = stringBuilder1.ToString();
      this.descriptionNormalized = Regex.Replace(this.descriptionNormalized, "[,][ ]*(<(.*?)>)*(.)", (MatchEvaluator) (m => m.ToString().ToLower()));
      this.descriptionNormalized = Regex.Replace(this.descriptionNormalized, "<br>\\w", (MatchEvaluator) (m => m.ToString().ToUpper()));
      Globals.Instance.CardsDescriptionNormalized[this.id] = stringBuilder1.ToString();
      if (includeInSearch)
        Globals.Instance.IncludeInSearch(Regex.Replace(Regex.Replace(this.descriptionNormalized, "<sprite name=(.*?)>", (MatchEvaluator) (m => Texts.Instance.GetText(m.Groups[1].Value))), "(<(.*?)>)*", ""), this.id, false);
    }
    else
      this.descriptionNormalized = Globals.Instance.CardsDescriptionNormalized[this.id];
  }

  private string NumFormatItem(int num, bool plus = false, bool percent = false)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(" <nobr>");
    if (num > 0)
    {
      stringBuilder.Append("<color=#263ABC><size=+.1>");
      if (plus)
        stringBuilder.Append("+");
    }
    else
    {
      stringBuilder.Append("<color=#720070><size=+.1>");
      if (plus)
        stringBuilder.Append("-");
    }
    stringBuilder.Append(Mathf.Abs(num));
    if (percent)
      stringBuilder.Append("%");
    stringBuilder.Append("</color></size></nobr>");
    return stringBuilder.ToString();
  }

  private string NumFormat(int num)
  {
    if (num < 0)
      num = 0;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=+.1>");
    stringBuilder.Append(num);
    stringBuilder.Append("</size>");
    return stringBuilder.ToString();
  }

  public List<Enums.CardType> GetCardTypes()
  {
    if (this.cardTypeList == null)
    {
      this.cardTypeList = new List<Enums.CardType>();
      if (this.cardType != Enums.CardType.None)
      {
        this.cardTypeList.Add(this.cardType);
        for (int index = 0; index < this.cardTypeAux.Length; ++index)
        {
          if (this.cardTypeAux[index] != Enums.CardType.None)
            this.cardTypeList.Add(this.cardTypeAux[index]);
        }
      }
    }
    return this.cardTypeList;
  }

  public bool HasCardType(Enums.CardType type)
  {
    if (this.cardType == type)
      return true;
    for (int index = 0; index < this.cardTypeAux.Length; ++index)
    {
      if (this.cardTypeAux[index] == type)
        return true;
    }
    return false;
  }

  public void DoExhaust()
  {
    if (!(bool) (UnityEngine.Object) MatchManager.Instance)
      return;
    Hero heroHeroActive = MatchManager.Instance.GetHeroHeroActive();
    if (heroHeroActive == null)
      return;
    this.exhaustCounter += heroHeroActive.GetAuraCharges("Exhaust");
  }

  public bool GetIgnoreBlock(int _index = 0)
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
    {
      Hero heroHeroActive = MatchManager.Instance.GetHeroHeroActive();
      if (heroHeroActive != null && heroHeroActive.SubclassName == "archer" && AtOManager.Instance.CharacterHaveTrait("archer", "perforatingshots") && this.HasCardType(Enums.CardType.Ranged_Attack))
        return true;
    }
    if (_index == 0)
      return this.ignoreBlock;
    return _index == 1 && this.ignoreBlock2;
  }

  public void ResetExhaust() => this.exhaustCounter = 0;

  public int GetCardFinalCost()
  {
    int cardFinalCost = this.EnergyCostOriginal;
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
    {
      if (this.EnergyReductionToZeroPermanent || this.EnergyReductionToZeroTemporal)
        cardFinalCost = 0;
      int num = cardFinalCost - this.EnergyReductionPermanent - this.EnergyReductionTemporal;
      cardFinalCost = this.CardClass == Enums.CardClass.Special && !this.Playable || this.CardClass == Enums.CardClass.Boon || this.CardClass == Enums.CardClass.Injury || this.CardClass == Enums.CardClass.Monster && !this.Playable ? 0 : num + this.ExhaustCounter;
      if (cardFinalCost < 0)
        cardFinalCost = 0;
    }
    return cardFinalCost;
  }

  private string ColorFromCardDataRarity(CardData _cData)
  {
    if (!((UnityEngine.Object) _cData != (UnityEngine.Object) null))
      return "<color=#5E3016>";
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<color=#");
    if (_cData.cardUpgraded == Enums.CardUpgraded.A)
      stringBuilder.Append(this.colorUpgradeBlue);
    else if (_cData.cardUpgraded == Enums.CardUpgraded.B)
      stringBuilder.Append(this.colorUpgradeGold);
    else if (_cData.cardUpgraded == Enums.CardUpgraded.Rare)
      stringBuilder.Append(this.colorUpgradeRare);
    else
      stringBuilder.Append(this.colorUpgradePlain);
    stringBuilder.Append(">");
    return stringBuilder.ToString();
  }

  public string CardName
  {
    get => this.cardName;
    set => this.cardName = value;
  }

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }

  public string InternalId
  {
    get => this.internalId;
    set => this.internalId = value;
  }

  public bool Visible
  {
    get => this.visible;
    set => this.visible = value;
  }

  public string UpgradesTo1
  {
    get => this.upgradesTo1;
    set => this.upgradesTo1 = value;
  }

  public string UpgradesTo2
  {
    get => this.upgradesTo2;
    set => this.upgradesTo2 = value;
  }

  public Enums.CardUpgraded CardUpgraded
  {
    get => this.cardUpgraded;
    set => this.cardUpgraded = value;
  }

  public string UpgradedFrom
  {
    get => this.upgradedFrom;
    set => this.upgradedFrom = value;
  }

  public string BaseCard
  {
    get => this.baseCard;
    set => this.baseCard = value;
  }

  public int CardNumber
  {
    get => this.cardNumber;
    set => this.cardNumber = value;
  }

  public string Description
  {
    get => this.description;
    set => this.description = value;
  }

  public string Fluff
  {
    get => this.fluff;
    set => this.fluff = value;
  }

  public string DescriptionNormalized
  {
    get => this.descriptionNormalized;
    set => this.descriptionNormalized = value;
  }

  public List<KeyNotesData> KeyNotes
  {
    get => this.keyNotes;
    set => this.keyNotes = value;
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

  public AudioClip SoundPreAction
  {
    get => this.soundPreAction;
    set => this.soundPreAction = value;
  }

  public string EffectPreAction
  {
    get => this.effectPreAction;
    set => this.effectPreAction = value;
  }

  public string EffectCaster
  {
    get => this.effectCaster;
    set => this.effectCaster = value;
  }

  public float EffectPostCastDelay
  {
    get => this.effectPostCastDelay;
    set => this.effectPostCastDelay = value;
  }

  public bool EffectCasterRepeat
  {
    get => this.effectCasterRepeat;
    set => this.effectCasterRepeat = value;
  }

  public bool EffectCastCenter
  {
    get => this.effectCastCenter;
    set => this.effectCastCenter = value;
  }

  public string EffectTrail
  {
    get => this.effectTrail;
    set => this.effectTrail = value;
  }

  public bool EffectTrailRepeat
  {
    get => this.effectTrailRepeat;
    set => this.effectTrailRepeat = value;
  }

  public float EffectTrailSpeed
  {
    get => this.effectTrailSpeed;
    set => this.effectTrailSpeed = value;
  }

  public Enums.EffectTrailAngle EffectTrailAngle
  {
    get => this.effectTrailAngle;
    set => this.effectTrailAngle = value;
  }

  public string EffectTarget
  {
    get => this.effectTarget;
    set => this.effectTarget = value;
  }

  public int MaxInDeck
  {
    get => this.maxInDeck;
    set => this.maxInDeck = value;
  }

  public Enums.CardRarity CardRarity
  {
    get => this.cardRarity;
    set => this.cardRarity = value;
  }

  public Enums.CardType CardType
  {
    get => this.cardType;
    set => this.cardType = value;
  }

  public Enums.CardType[] CardTypeAux
  {
    get => this.cardTypeAux;
    set => this.cardTypeAux = value;
  }

  public Enums.CardClass CardClass
  {
    get => this.cardClass;
    set => this.cardClass = value;
  }

  public int EnergyCost
  {
    get => this.energyCost;
    set => this.energyCost = value;
  }

  public int EnergyCostOriginal
  {
    get => this.energyCostOriginal;
    set => this.energyCostOriginal = value;
  }

  public int EnergyCostForShow
  {
    get => this.energyCostForShow;
    set => this.energyCostForShow = value;
  }

  public bool Playable
  {
    get => this.playable;
    set => this.playable = value;
  }

  public bool AutoplayDraw
  {
    get => this.autoplayDraw;
    set => this.autoplayDraw = value;
  }

  public bool AutoplayEndTurn
  {
    get => this.autoplayEndTurn;
    set => this.autoplayEndTurn = value;
  }

  public Enums.CardTargetType TargetType
  {
    get => this.targetType;
    set => this.targetType = value;
  }

  public Enums.CardTargetSide TargetSide
  {
    get => this.targetSide;
    set => this.targetSide = value;
  }

  public Enums.CardTargetPosition TargetPosition
  {
    get => this.targetPosition;
    set => this.targetPosition = value;
  }

  public string EffectRequired
  {
    get => this.effectRequired;
    set => this.effectRequired = value;
  }

  public int EffectRepeat
  {
    get => this.effectRepeat;
    set => this.effectRepeat = value;
  }

  public float EffectRepeatDelay
  {
    get => this.effectRepeatDelay;
    set => this.effectRepeatDelay = value;
  }

  public int EffectRepeatEnergyBonus
  {
    get => this.effectRepeatEnergyBonus;
    set => this.effectRepeatEnergyBonus = value;
  }

  public int EffectRepeatMaxBonus
  {
    get => this.effectRepeatMaxBonus;
    set => this.effectRepeatMaxBonus = value;
  }

  public Enums.EffectRepeatTarget EffectRepeatTarget
  {
    get => this.effectRepeatTarget;
    set => this.effectRepeatTarget = value;
  }

  public int EffectRepeatModificator
  {
    get => this.effectRepeatModificator;
    set => this.effectRepeatModificator = value;
  }

  public Enums.DamageType DamageType
  {
    get => this.damageType;
    set => this.damageType = value;
  }

  public int Damage
  {
    get => this.damage;
    set => this.damage = value;
  }

  public int DamagePreCalculated
  {
    get => this.damagePreCalculated;
    set => this.damagePreCalculated = value;
  }

  public int DamageSides
  {
    get => this.damageSides;
    set => this.damageSides = value;
  }

  public int DamageSidesPreCalculated
  {
    get => this.damageSidesPreCalculated;
    set => this.damageSidesPreCalculated = value;
  }

  public int DamageSelf
  {
    get => this.damageSelf;
    set => this.damageSelf = value;
  }

  public int DamageSelfPreCalculated
  {
    get => this.damageSelfPreCalculated;
    set => this.damageSelfPreCalculated = value;
  }

  public int DamageSelfPreCalculated2
  {
    get => this.damageSelfPreCalculated2;
    set => this.damageSelfPreCalculated2 = value;
  }

  public bool IgnoreBlock
  {
    get => this.ignoreBlock;
    set => this.ignoreBlock = value;
  }

  public Enums.DamageType DamageType2
  {
    get => this.damageType2;
    set => this.damageType2 = value;
  }

  public int Damage2
  {
    get => this.damage2;
    set => this.damage2 = value;
  }

  public int DamagePreCalculated2
  {
    get => this.damagePreCalculated2;
    set => this.damagePreCalculated2 = value;
  }

  public int DamageSides2
  {
    get => this.damageSides2;
    set => this.damageSides2 = value;
  }

  public int DamageSidesPreCalculated2
  {
    get => this.damageSidesPreCalculated2;
    set => this.damageSidesPreCalculated2 = value;
  }

  public int DamageSelf2
  {
    get => this.damageSelf2;
    set => this.damageSelf2 = value;
  }

  public bool IgnoreBlock2
  {
    get => this.ignoreBlock2;
    set => this.ignoreBlock2 = value;
  }

  public int SelfHealthLoss
  {
    get => this.selfHealthLoss;
    set => this.selfHealthLoss = value;
  }

  public int DamageEnergyBonus
  {
    get => this.damageEnergyBonus;
    set => this.damageEnergyBonus = value;
  }

  public int Heal
  {
    get => this.heal;
    set => this.heal = value;
  }

  public int HealSides
  {
    get => this.healSides;
    set => this.healSides = value;
  }

  public int HealSelf
  {
    get => this.healSelf;
    set => this.healSelf = value;
  }

  public int HealEnergyBonus
  {
    get => this.healEnergyBonus;
    set => this.healEnergyBonus = value;
  }

  public float HealSelfPerDamageDonePercent
  {
    get => this.healSelfPerDamageDonePercent;
    set => this.healSelfPerDamageDonePercent = value;
  }

  public int HealCurses
  {
    get => this.healCurses;
    set => this.healCurses = value;
  }

  public AuraCurseData HealAuraCurseSelf
  {
    get => this.healAuraCurseSelf;
    set => this.healAuraCurseSelf = value;
  }

  public AuraCurseData HealAuraCurseName
  {
    get => this.healAuraCurseName;
    set => this.healAuraCurseName = value;
  }

  public AuraCurseData HealAuraCurseName2
  {
    get => this.healAuraCurseName2;
    set => this.healAuraCurseName2 = value;
  }

  public AuraCurseData HealAuraCurseName3
  {
    get => this.healAuraCurseName3;
    set => this.healAuraCurseName3 = value;
  }

  public AuraCurseData HealAuraCurseName4
  {
    get => this.healAuraCurseName4;
    set => this.healAuraCurseName4 = value;
  }

  public int DispelAuras
  {
    get => this.dispelAuras;
    set => this.dispelAuras = value;
  }

  public int EnergyRecharge
  {
    get => this.energyRecharge;
    set => this.energyRecharge = value;
  }

  public AuraCurseData Aura
  {
    get => this.aura;
    set => this.aura = value;
  }

  public AuraCurseData AuraSelf
  {
    get => this.auraSelf;
    set => this.auraSelf = value;
  }

  public int AuraCharges
  {
    get => this.auraCharges;
    set => this.auraCharges = value;
  }

  public AuraCurseData Aura2
  {
    get => this.aura2;
    set => this.aura2 = value;
  }

  public AuraCurseData AuraSelf2
  {
    get => this.auraSelf2;
    set => this.auraSelf2 = value;
  }

  public int AuraCharges2
  {
    get => this.auraCharges2;
    set => this.auraCharges2 = value;
  }

  public AuraCurseData Aura3
  {
    get => this.aura3;
    set => this.aura3 = value;
  }

  public AuraCurseData AuraSelf3
  {
    get => this.auraSelf3;
    set => this.auraSelf3 = value;
  }

  public int AuraCharges3
  {
    get => this.auraCharges3;
    set => this.auraCharges3 = value;
  }

  public AuraCurseData Curse
  {
    get => this.curse;
    set => this.curse = value;
  }

  public AuraCurseData CurseSelf
  {
    get => this.curseSelf;
    set => this.curseSelf = value;
  }

  public int CurseCharges
  {
    get => this.curseCharges;
    set => this.curseCharges = value;
  }

  public AuraCurseData Curse2
  {
    get => this.curse2;
    set => this.curse2 = value;
  }

  public AuraCurseData CurseSelf2
  {
    get => this.curseSelf2;
    set => this.curseSelf2 = value;
  }

  public int CurseCharges2
  {
    get => this.curseCharges2;
    set => this.curseCharges2 = value;
  }

  public AuraCurseData Curse3
  {
    get => this.curse3;
    set => this.curse3 = value;
  }

  public AuraCurseData CurseSelf3
  {
    get => this.curseSelf3;
    set => this.curseSelf3 = value;
  }

  public int CurseCharges3
  {
    get => this.curseCharges3;
    set => this.curseCharges3 = value;
  }

  public int PushTarget
  {
    get => this.pushTarget;
    set => this.pushTarget = value;
  }

  public int PullTarget
  {
    get => this.pullTarget;
    set => this.pullTarget = value;
  }

  public int DrawCard
  {
    get => this.drawCard;
    set => this.drawCard = value;
  }

  public int DiscardCard
  {
    get => this.discardCard;
    set => this.discardCard = value;
  }

  public Enums.CardType DiscardCardType
  {
    get => this.discardCardType;
    set => this.discardCardType = value;
  }

  public Enums.CardType[] DiscardCardTypeAux
  {
    get => this.discardCardTypeAux;
    set => this.discardCardTypeAux = value;
  }

  public bool DiscardCardAutomatic
  {
    get => this.discardCardAutomatic;
    set => this.discardCardAutomatic = value;
  }

  public Enums.CardPlace DiscardCardPlace
  {
    get => this.discardCardPlace;
    set => this.discardCardPlace = value;
  }

  public int AddCard
  {
    get => this.addCard;
    set => this.addCard = value;
  }

  public string AddCardId
  {
    get => this.addCardId;
    set => this.addCardId = value;
  }

  public Enums.CardType AddCardType
  {
    get => this.addCardType;
    set => this.addCardType = value;
  }

  public Enums.CardType[] AddCardTypeAux
  {
    get => this.addCardTypeAux;
    set => this.addCardTypeAux = value;
  }

  public int AddCardChoose
  {
    get => this.addCardChoose;
    set => this.addCardChoose = value;
  }

  public Enums.CardFrom AddCardFrom
  {
    get => this.addCardFrom;
    set => this.addCardFrom = value;
  }

  public Enums.CardPlace AddCardPlace
  {
    get => this.addCardPlace;
    set => this.addCardPlace = value;
  }

  public int AddCardReducedCost
  {
    get => this.addCardReducedCost;
    set => this.addCardReducedCost = value;
  }

  public bool AddCardCostTurn
  {
    get => this.addCardCostTurn;
    set => this.addCardCostTurn = value;
  }

  public bool AddCardVanish
  {
    get => this.addCardVanish;
    set => this.addCardVanish = value;
  }

  public int LookCards
  {
    get => this.lookCards;
    set => this.lookCards = value;
  }

  public int LookCardsDiscardUpTo
  {
    get => this.lookCardsDiscardUpTo;
    set => this.lookCardsDiscardUpTo = value;
  }

  public NPCData SummonUnit
  {
    get => this.summonUnit;
    set => this.summonUnit = value;
  }

  public int SummonUnitNum
  {
    get => this.summonUnitNum;
    set => this.summonUnitNum = value;
  }

  public bool Vanish
  {
    get => this.vanish;
    set => this.vanish = value;
  }

  public bool Lazy
  {
    get => this.lazy;
    set => this.lazy = value;
  }

  public bool Innate
  {
    get => this.innate;
    set => this.innate = value;
  }

  public bool Corrupted
  {
    get => this.corrupted;
    set => this.corrupted = value;
  }

  public bool EndTurn
  {
    get => this.endTurn;
    set => this.endTurn = value;
  }

  public bool MoveToCenter
  {
    get => this.moveToCenter;
    set => this.moveToCenter = value;
  }

  public bool ModifiedByTrait
  {
    get => this.modifiedByTrait;
    set => this.modifiedByTrait = value;
  }

  public float EffectPostTargetDelay
  {
    get => this.effectPostTargetDelay;
    set => this.effectPostTargetDelay = value;
  }

  public Enums.CardSpecialValue SpecialValueGlobal
  {
    get => this.specialValueGlobal;
    set => this.specialValueGlobal = value;
  }

  public float SpecialValueModifierGlobal
  {
    get => this.specialValueModifierGlobal;
    set => this.specialValueModifierGlobal = value;
  }

  public Enums.CardSpecialValue SpecialValue1
  {
    get => this.specialValue1;
    set => this.specialValue1 = value;
  }

  public float SpecialValueModifier1
  {
    get => this.specialValueModifier1;
    set => this.specialValueModifier1 = value;
  }

  public Enums.CardSpecialValue SpecialValue2
  {
    get => this.specialValue2;
    set => this.specialValue2 = value;
  }

  public float SpecialValueModifier2
  {
    get => this.specialValueModifier2;
    set => this.specialValueModifier2 = value;
  }

  public bool DamageSpecialValueGlobal
  {
    get => this.damageSpecialValueGlobal;
    set => this.damageSpecialValueGlobal = value;
  }

  public bool DamageSpecialValue1
  {
    get => this.damageSpecialValue1;
    set => this.damageSpecialValue1 = value;
  }

  public bool DamageSpecialValue2
  {
    get => this.damageSpecialValue2;
    set => this.damageSpecialValue2 = value;
  }

  public bool Damage2SpecialValueGlobal
  {
    get => this.damage2SpecialValueGlobal;
    set => this.damage2SpecialValueGlobal = value;
  }

  public bool Damage2SpecialValue1
  {
    get => this.damage2SpecialValue1;
    set => this.damage2SpecialValue1 = value;
  }

  public bool Damage2SpecialValue2
  {
    get => this.damage2SpecialValue2;
    set => this.damage2SpecialValue2 = value;
  }

  public AuraCurseData SpecialAuraCurseNameGlobal
  {
    get => this.specialAuraCurseNameGlobal;
    set => this.specialAuraCurseNameGlobal = value;
  }

  public AuraCurseData SpecialAuraCurseName1
  {
    get => this.specialAuraCurseName1;
    set => this.specialAuraCurseName1 = value;
  }

  public AuraCurseData SpecialAuraCurseName2
  {
    get => this.specialAuraCurseName2;
    set => this.specialAuraCurseName2 = value;
  }

  public bool AuraChargesSpecialValue1
  {
    get => this.auraChargesSpecialValue1;
    set => this.auraChargesSpecialValue1 = value;
  }

  public bool AuraChargesSpecialValue2
  {
    get => this.auraChargesSpecialValue2;
    set => this.auraChargesSpecialValue2 = value;
  }

  public bool AuraChargesSpecialValueGlobal
  {
    get => this.auraChargesSpecialValueGlobal;
    set => this.auraChargesSpecialValueGlobal = value;
  }

  public bool AuraCharges2SpecialValue1
  {
    get => this.auraCharges2SpecialValue1;
    set => this.auraCharges2SpecialValue1 = value;
  }

  public bool AuraCharges2SpecialValue2
  {
    get => this.auraCharges2SpecialValue2;
    set => this.auraCharges2SpecialValue2 = value;
  }

  public bool AuraCharges2SpecialValueGlobal
  {
    get => this.auraCharges2SpecialValueGlobal;
    set => this.auraCharges2SpecialValueGlobal = value;
  }

  public bool AuraCharges3SpecialValue1
  {
    get => this.auraCharges3SpecialValue1;
    set => this.auraCharges3SpecialValue1 = value;
  }

  public bool AuraCharges3SpecialValue2
  {
    get => this.auraCharges3SpecialValue2;
    set => this.auraCharges3SpecialValue2 = value;
  }

  public bool AuraCharges3SpecialValueGlobal
  {
    get => this.auraCharges3SpecialValueGlobal;
    set => this.auraCharges3SpecialValueGlobal = value;
  }

  public bool CurseChargesSpecialValue1
  {
    get => this.curseChargesSpecialValue1;
    set => this.curseChargesSpecialValue1 = value;
  }

  public bool CurseChargesSpecialValue2
  {
    get => this.curseChargesSpecialValue2;
    set => this.curseChargesSpecialValue2 = value;
  }

  public bool CurseChargesSpecialValueGlobal
  {
    get => this.curseChargesSpecialValueGlobal;
    set => this.curseChargesSpecialValueGlobal = value;
  }

  public bool CurseCharges2SpecialValue1
  {
    get => this.curseCharges2SpecialValue1;
    set => this.curseCharges2SpecialValue1 = value;
  }

  public bool CurseCharges2SpecialValue2
  {
    get => this.curseCharges2SpecialValue2;
    set => this.curseCharges2SpecialValue2 = value;
  }

  public bool CurseCharges2SpecialValueGlobal
  {
    get => this.curseCharges2SpecialValueGlobal;
    set => this.curseCharges2SpecialValueGlobal = value;
  }

  public bool CurseCharges3SpecialValue1
  {
    get => this.curseCharges3SpecialValue1;
    set => this.curseCharges3SpecialValue1 = value;
  }

  public bool CurseCharges3SpecialValue2
  {
    get => this.curseCharges3SpecialValue2;
    set => this.curseCharges3SpecialValue2 = value;
  }

  public bool CurseCharges3SpecialValueGlobal
  {
    get => this.curseCharges3SpecialValueGlobal;
    set => this.curseCharges3SpecialValueGlobal = value;
  }

  public bool HealSpecialValueGlobal
  {
    get => this.healSpecialValueGlobal;
    set => this.healSpecialValueGlobal = value;
  }

  public bool HealSpecialValue1
  {
    get => this.healSpecialValue1;
    set => this.healSpecialValue1 = value;
  }

  public bool HealSpecialValue2
  {
    get => this.healSpecialValue2;
    set => this.healSpecialValue2 = value;
  }

  public bool SelfHealthLossSpecialGlobal
  {
    get => this.selfHealthLossSpecialGlobal;
    set => this.selfHealthLossSpecialGlobal = value;
  }

  public bool SelfHealthLossSpecialValue1
  {
    get => this.selfHealthLossSpecialValue1;
    set => this.selfHealthLossSpecialValue1 = value;
  }

  public bool SelfHealthLossSpecialValue2
  {
    get => this.selfHealthLossSpecialValue2;
    set => this.selfHealthLossSpecialValue2 = value;
  }

  public float FluffPercent
  {
    get => this.fluffPercent;
    set => this.fluffPercent = value;
  }

  public ItemData Item
  {
    get => this.item;
    set => this.item = value;
  }

  public AuraCurseData SummonAura
  {
    get => this.summonAura;
    set => this.summonAura = value;
  }

  public int SummonAuraCharges
  {
    get => this.summonAuraCharges;
    set => this.summonAuraCharges = value;
  }

  public AuraCurseData SummonAura2
  {
    get => this.summonAura2;
    set => this.summonAura2 = value;
  }

  public int SummonAuraCharges2
  {
    get => this.summonAuraCharges2;
    set => this.summonAuraCharges2 = value;
  }

  public AuraCurseData SummonAura3
  {
    get => this.summonAura3;
    set => this.summonAura3 = value;
  }

  public int SummonAuraCharges3
  {
    get => this.summonAuraCharges3;
    set => this.summonAuraCharges3 = value;
  }

  public bool HealSelfSpecialValueGlobal
  {
    get => this.healSelfSpecialValueGlobal;
    set => this.healSelfSpecialValueGlobal = value;
  }

  public bool HealSelfSpecialValue1
  {
    get => this.healSelfSpecialValue1;
    set => this.healSelfSpecialValue1 = value;
  }

  public bool HealSelfSpecialValue2
  {
    get => this.healSelfSpecialValue2;
    set => this.healSelfSpecialValue2 = value;
  }

  public GameObject PetModel
  {
    get => this.petModel;
    set => this.petModel = value;
  }

  public bool PetFront
  {
    get => this.petFront;
    set => this.petFront = value;
  }

  public Vector2 PetOffset
  {
    get => this.petOffset;
    set => this.petOffset = value;
  }

  public Vector2 PetSize
  {
    get => this.petSize;
    set => this.petSize = value;
  }

  public bool PetInvert
  {
    get => this.petInvert;
    set => this.petInvert = value;
  }

  public bool IsPetAttack
  {
    get => this.isPetAttack;
    set => this.isPetAttack = value;
  }

  public bool IsPetCast
  {
    get => this.isPetCast;
    set => this.isPetCast = value;
  }

  public CardData UpgradesToRare
  {
    get => this.upgradesToRare;
    set => this.upgradesToRare = value;
  }

  public int ExhaustCounter
  {
    get => this.exhaustCounter;
    set => this.exhaustCounter = value;
  }

  public bool Starter
  {
    get => this.starter;
    set => this.starter = value;
  }

  public string Target
  {
    get => this.target;
    set => this.target = value;
  }

  public ItemData ItemEnchantment
  {
    get => this.itemEnchantment;
    set => this.itemEnchantment = value;
  }

  public CardData[] AddCardList
  {
    get => this.addCardList;
    set => this.addCardList = value;
  }

  public bool ShowInTome
  {
    get => this.showInTome;
    set => this.showInTome = value;
  }

  public int LookCardsVanishUpTo
  {
    get => this.lookCardsVanishUpTo;
    set => this.lookCardsVanishUpTo = value;
  }

  public int TransferCurses
  {
    get => this.transferCurses;
    set => this.transferCurses = value;
  }

  public bool KillPet
  {
    get => this.killPet;
    set => this.killPet = value;
  }

  public int ReduceCurses
  {
    get => this.reduceCurses;
    set => this.reduceCurses = value;
  }

  public AuraCurseData AcEnergyBonus
  {
    get => this.acEnergyBonus;
    set => this.acEnergyBonus = value;
  }

  public int AcEnergyBonusQuantity
  {
    get => this.acEnergyBonusQuantity;
    set => this.acEnergyBonusQuantity = value;
  }

  public int EnergyReductionPermanent
  {
    get => this.energyReductionPermanent;
    set => this.energyReductionPermanent = value;
  }

  public int EnergyReductionTemporal
  {
    get => this.energyReductionTemporal;
    set => this.energyReductionTemporal = value;
  }

  public bool EnergyReductionToZeroPermanent
  {
    get => this.energyReductionToZeroPermanent;
    set => this.energyReductionToZeroPermanent = value;
  }

  public bool EnergyReductionToZeroTemporal
  {
    get => this.energyReductionToZeroTemporal;
    set => this.energyReductionToZeroTemporal = value;
  }

  public AuraCurseData AcEnergyBonus2
  {
    get => this.acEnergyBonus2;
    set => this.acEnergyBonus2 = value;
  }

  public int AcEnergyBonus2Quantity
  {
    get => this.acEnergyBonus2Quantity;
    set => this.acEnergyBonus2Quantity = value;
  }

  public int StealAuras
  {
    get => this.stealAuras;
    set => this.stealAuras = value;
  }

  public bool FlipSprite
  {
    get => this.flipSprite;
    set => this.flipSprite = value;
  }

  public AudioClip SoundPreActionFemale
  {
    get => this.soundPreActionFemale;
    set => this.soundPreActionFemale = value;
  }

  public int ReduceAuras
  {
    get => this.reduceAuras;
    set => this.reduceAuras = value;
  }

  public int IncreaseCurses
  {
    get => this.increaseCurses;
    set => this.increaseCurses = value;
  }

  public int IncreaseAuras
  {
    get => this.increaseAuras;
    set => this.increaseAuras = value;
  }

  public bool OnlyInWeekly
  {
    get => this.onlyInWeekly;
    set => this.onlyInWeekly = value;
  }

  public string RelatedCard
  {
    get => this.relatedCard;
    set => this.relatedCard = value;
  }

  public string RelatedCard2
  {
    get => this.relatedCard2;
    set => this.relatedCard2 = value;
  }

  public string RelatedCard3
  {
    get => this.relatedCard3;
    set => this.relatedCard3 = value;
  }

  public int GoldGainQuantity
  {
    get => this.goldGainQuantity;
    set => this.goldGainQuantity = value;
  }

  public int ShardsGainQuantity
  {
    get => this.shardsGainQuantity;
    set => this.shardsGainQuantity = value;
  }

  public string Sku
  {
    get => this.sku;
    set => this.sku = value;
  }

  public bool EnergyRechargeSpecialValueGlobal
  {
    get => this.energyRechargeSpecialValueGlobal;
    set => this.energyRechargeSpecialValueGlobal = value;
  }

  public bool DrawCardSpecialValueGlobal
  {
    get => this.drawCardSpecialValueGlobal;
    set => this.drawCardSpecialValueGlobal = value;
  }

  public float SelfKillHiddenSeconds
  {
    get => this.selfKillHiddenSeconds;
    set => this.selfKillHiddenSeconds = value;
  }
}
