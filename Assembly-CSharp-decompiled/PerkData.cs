// Decompiled with JetBrains decompiler
// Type: PerkData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "New Perk", menuName = "Perk Data", order = 57)]
public class PerkData : ScriptableObject
{
  [SerializeField]
  private string id = "";
  [Header("Attributes")]
  [SerializeField]
  private Enums.CardClass cardClass;
  [SerializeField]
  private bool mainPerk;
  [SerializeField]
  private bool obeliskPerk;
  [SerializeField]
  private int level;
  [SerializeField]
  private int row;
  [SerializeField]
  private Sprite icon;
  [SerializeField]
  private string iconTextValue;
  [SerializeField]
  private string customDescription;
  [Header("Currency")]
  [SerializeField]
  private int additionalCurrency;
  [SerializeField]
  private int additionalShards;
  [Header("Stat modification")]
  [SerializeField]
  private int maxHealth;
  [Header("Energy begin combat")]
  [SerializeField]
  private int energyBegin;
  [Header("Speed combat")]
  [SerializeField]
  private int speedQuantity;
  [Header("Damage value bonus")]
  [SerializeField]
  private Enums.DamageType damageFlatBonus;
  [SerializeField]
  private int damageFlatBonusValue;
  [Header("AuraCurse bonus")]
  [SerializeField]
  private AuraCurseData auracurseBonus;
  [SerializeField]
  private int auracurseBonusValue;
  [Header("Resist modification")]
  [SerializeField]
  private Enums.DamageType resistModified;
  [SerializeField]
  private int resistModifiedValue;
  [Header("Heal bonus")]
  [SerializeField]
  private int healQuantity;

  public void Init() => this.id = this.id.ToLower();

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }

  public int Level
  {
    get => this.level;
    set => this.level = value;
  }

  public int Row
  {
    get => this.row;
    set => this.row = value;
  }

  public int MaxHealth
  {
    get => this.maxHealth;
    set => this.maxHealth = value;
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

  public int EnergyBegin
  {
    get => this.energyBegin;
    set => this.energyBegin = value;
  }

  public AuraCurseData AuracurseBonus
  {
    get => this.auracurseBonus;
    set => this.auracurseBonus = value;
  }

  public int AuracurseBonusValue
  {
    get => this.auracurseBonusValue;
    set => this.auracurseBonusValue = value;
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

  public Enums.CardClass CardClass
  {
    get => this.cardClass;
    set => this.cardClass = value;
  }

  public int SpeedQuantity
  {
    get => this.speedQuantity;
    set => this.speedQuantity = value;
  }

  public Sprite Icon
  {
    get => this.icon;
    set => this.icon = value;
  }

  public string IconTextValue
  {
    get => this.iconTextValue;
    set => this.iconTextValue = value;
  }

  public int AdditionalCurrency
  {
    get => this.additionalCurrency;
    set => this.additionalCurrency = value;
  }

  public int HealQuantity
  {
    get => this.healQuantity;
    set => this.healQuantity = value;
  }

  public bool ObeliskPerk
  {
    get => this.obeliskPerk;
    set => this.obeliskPerk = value;
  }

  public bool MainPerk
  {
    get => this.mainPerk;
    set => this.mainPerk = value;
  }

  public int AdditionalShards
  {
    get => this.additionalShards;
    set => this.additionalShards = value;
  }

  public string CustomDescription
  {
    get => this.customDescription;
    set => this.customDescription = value;
  }
}
