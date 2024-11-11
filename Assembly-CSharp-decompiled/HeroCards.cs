// Decompiled with JetBrains decompiler
// Type: HeroCards
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class HeroCards
{
  [SerializeField]
  private CardData card;
  [SerializeField]
  private int unitsInDeck;

  public CardData Card
  {
    get => this.card;
    set => this.card = value;
  }

  public int UnitsInDeck
  {
    get => this.unitsInDeck;
    set => this.unitsInDeck = value;
  }
}
