// Decompiled with JetBrains decompiler
// Type: LootItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class LootItem
{
  [SerializeField]
  private CardData lootCard;
  [SerializeField]
  private float lootPercent;
  [SerializeField]
  private Enums.CardType lootType;
  [SerializeField]
  private Enums.CardRarity lootRarity;

  public CardData LootCard
  {
    get => this.lootCard;
    set => this.lootCard = value;
  }

  public float LootPercent
  {
    get => this.lootPercent;
    set => this.lootPercent = value;
  }

  public Enums.CardType LootType
  {
    get => this.lootType;
    set => this.lootType = value;
  }

  public Enums.CardRarity LootRarity
  {
    get => this.lootRarity;
    set => this.lootRarity = value;
  }
}
