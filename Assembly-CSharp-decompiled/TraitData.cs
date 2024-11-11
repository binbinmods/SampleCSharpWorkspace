// Decompiled with JetBrains decompiler
// Type: TraitData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "Trait Data", order = 58)]
public class TraitData : ScriptableObject
{
  [SerializeField]
  private string traitName;
  [SerializeField]
  private string id;
  [TextArea]
  [SerializeField]
  private string description;
  [SerializeField]
  private Enums.EventActivation activation;
  [SerializeField]
  private int timesPerTurn;
  [SerializeField]
  private int timesPerRound;
  [Header("Card reward")]
  [SerializeField]
  private CardData traitCard;
  [Header("Card reward for all heroes")]
  [SerializeField]
  private CardData traitCardForAllHeroes;
  [Header("Character stat modification")]
  [SerializeField]
  private Enums.CharacterStat characterStatModified;
  [SerializeField]
  private int characterStatModifiedValue;
  [Header("Resist modification")]
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
  [Header("AuraCurse immunity")]
  [SerializeField]
  private string auracurseImmune1 = "";
  [SerializeField]
  private string auracurseImmune2 = "";
  [SerializeField]
  private string auracurseImmune3 = "";
  [Header("AuraCurse bonus")]
  [SerializeField]
  private AuraCurseData auracurseBonus1;
  [SerializeField]
  private int auracurseBonusValue1;
  [SerializeField]
  private AuraCurseData auracurseBonus2;
  [SerializeField]
  private int auracurseBonusValue2;
  [SerializeField]
  private AuraCurseData auracurseBonus3;
  [SerializeField]
  private int auracurseBonusValue3;
  [Header("Heal bonus")]
  [SerializeField]
  private int healFlatBonus;
  [SerializeField]
  private float healPercentBonus;
  [SerializeField]
  private int healReceivedFlatBonus;
  [SerializeField]
  private float healReceivedPercentBonus;
  [Header("Damage value bonus")]
  [SerializeField]
  private Enums.DamageType damageBonusFlat;
  [SerializeField]
  private int damageBonusFlatValue;
  [SerializeField]
  private Enums.DamageType damageBonusFlat2;
  [SerializeField]
  private int damageBonusFlatValue2;
  [SerializeField]
  private Enums.DamageType damageBonusFlat3;
  [SerializeField]
  private int damageBonusFlatValue3;
  [SerializeField]
  private Enums.DamageType damageBonusPercent;
  [SerializeField]
  private float damageBonusPercentValue;
  [SerializeField]
  private Enums.DamageType damageBonusPercent2;
  [SerializeField]
  private float damageBonusPercentValue2;
  [SerializeField]
  private Enums.DamageType damageBonusPercent3;
  [SerializeField]
  private float damageBonusPercentValue3;
  [Header("Keynotes")]
  [SerializeField]
  private List<KeyNotesData> keyNotes;

  public void Init() => this.id = Regex.Replace(this.traitName, "\\s+", "").ToLower();

  public void SetNameAndDescription()
  {
    string text1 = Texts.Instance.GetText(this.id, "Traits");
    if (text1 != "")
      this.traitName = text1;
    string text2 = Texts.Instance.GetText(this.id + "_description", "Traits");
    if (!(text2 != ""))
      return;
    this.description = text2;
  }

  public string TraitName
  {
    get => this.traitName;
    set => this.traitName = value;
  }

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }

  public string Description
  {
    get => this.description;
    set => this.description = value;
  }

  public Enums.EventActivation Activation
  {
    get => this.activation;
    set => this.activation = value;
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

  public string AuracurseImmune1
  {
    get => this.auracurseImmune1;
    set => this.auracurseImmune1 = value;
  }

  public string AuracurseImmune2
  {
    get => this.auracurseImmune2;
    set => this.auracurseImmune2 = value;
  }

  public string AuracurseImmune3
  {
    get => this.auracurseImmune3;
    set => this.auracurseImmune3 = value;
  }

  public List<KeyNotesData> KeyNotes
  {
    get => this.keyNotes;
    set => this.keyNotes = value;
  }

  public Enums.DamageType DamageBonusFlat
  {
    get => this.damageBonusFlat;
    set => this.damageBonusFlat = value;
  }

  public int DamageBonusFlatValue
  {
    get => this.damageBonusFlatValue;
    set => this.damageBonusFlatValue = value;
  }

  public Enums.DamageType DamageBonusFlat2
  {
    get => this.damageBonusFlat2;
    set => this.damageBonusFlat2 = value;
  }

  public int DamageBonusFlatValue2
  {
    get => this.damageBonusFlatValue2;
    set => this.damageBonusFlatValue2 = value;
  }

  public Enums.DamageType DamageBonusFlat3
  {
    get => this.damageBonusFlat3;
    set => this.damageBonusFlat3 = value;
  }

  public int DamageBonusFlatValue3
  {
    get => this.damageBonusFlatValue3;
    set => this.damageBonusFlatValue3 = value;
  }

  public Enums.DamageType DamageBonusPercent
  {
    get => this.damageBonusPercent;
    set => this.damageBonusPercent = value;
  }

  public float DamageBonusPercentValue
  {
    get => this.damageBonusPercentValue;
    set => this.damageBonusPercentValue = value;
  }

  public Enums.DamageType DamageBonusPercent2
  {
    get => this.damageBonusPercent2;
    set => this.damageBonusPercent2 = value;
  }

  public float DamageBonusPercentValue2
  {
    get => this.damageBonusPercentValue2;
    set => this.damageBonusPercentValue2 = value;
  }

  public Enums.DamageType DamageBonusPercent3
  {
    get => this.damageBonusPercent3;
    set => this.damageBonusPercent3 = value;
  }

  public float DamageBonusPercentValue3
  {
    get => this.damageBonusPercentValue3;
    set => this.damageBonusPercentValue3 = value;
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

  public AuraCurseData AuracurseBonus3
  {
    get => this.auracurseBonus3;
    set => this.auracurseBonus3 = value;
  }

  public int AuracurseBonusValue3
  {
    get => this.auracurseBonusValue3;
    set => this.auracurseBonusValue3 = value;
  }

  public CardData TraitCard
  {
    get => this.traitCard;
    set => this.traitCard = value;
  }

  public CardData TraitCardForAllHeroes
  {
    get => this.traitCardForAllHeroes;
    set => this.traitCardForAllHeroes = value;
  }

  public int TimesPerTurn
  {
    get => this.timesPerTurn;
    set => this.timesPerTurn = value;
  }

  public int TimesPerRound
  {
    get => this.timesPerRound;
    set => this.timesPerRound = value;
  }
}
