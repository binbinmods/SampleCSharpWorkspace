// Decompiled with JetBrains decompiler
// Type: PackData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardPack", menuName = "New CardPack", order = 64)]
public class PackData : ScriptableObject
{
  [SerializeField]
  private string packId;
  [Header("Name and class")]
  [SerializeField]
  private string packName;
  [SerializeField]
  private SubClassData requiredClass;
  [SerializeField]
  private Enums.CardClass packClass;
  [Header("Pack Cards")]
  [SerializeField]
  private CardData card0;
  [SerializeField]
  private CardData card1;
  [SerializeField]
  private CardData card2;
  [SerializeField]
  private CardData card3;
  [SerializeField]
  private CardData card4;
  [SerializeField]
  private CardData card5;
  [Header("Special Cards")]
  [SerializeField]
  private CardData cardSpecial0;
  [SerializeField]
  private CardData cardSpecial1;
  [Header("Perks")]
  [SerializeField]
  private List<PerkData> perkList;

  public string PackId
  {
    get => this.packId;
    set => this.packId = value;
  }

  public string PackName
  {
    get => this.packName;
    set => this.packName = value;
  }

  public SubClassData RequiredClass
  {
    get => this.requiredClass;
    set => this.requiredClass = value;
  }

  public Enums.CardClass PackClass
  {
    get => this.packClass;
    set => this.packClass = value;
  }

  public CardData Card0
  {
    get => this.card0;
    set => this.card0 = value;
  }

  public CardData Card1
  {
    get => this.card1;
    set => this.card1 = value;
  }

  public CardData Card2
  {
    get => this.card2;
    set => this.card2 = value;
  }

  public CardData Card3
  {
    get => this.card3;
    set => this.card3 = value;
  }

  public CardData Card4
  {
    get => this.card4;
    set => this.card4 = value;
  }

  public CardData Card5
  {
    get => this.card5;
    set => this.card5 = value;
  }

  public CardData CardSpecial0
  {
    get => this.cardSpecial0;
    set => this.cardSpecial0 = value;
  }

  public CardData CardSpecial1
  {
    get => this.cardSpecial1;
    set => this.cardSpecial1 = value;
  }

  public List<PerkData> PerkList
  {
    get => this.perkList;
    set => this.perkList = value;
  }
}
