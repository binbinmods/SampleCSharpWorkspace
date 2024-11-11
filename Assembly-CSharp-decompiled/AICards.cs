// Decompiled with JetBrains decompiler
// Type: AICards
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class AICards
{
  [SerializeField]
  private CardData card;
  [SerializeField]
  private int unitsInDeck;
  [SerializeField]
  private int addCardRound;
  [Header("Order of cast (priority: less is first) ")]
  [SerializeField]
  private int priority;
  [SerializeField]
  private float percentToCast;
  [Header("Cast card only if")]
  [SerializeField]
  private Enums.OnlyCastIf onlyCastIf;
  [SerializeField]
  private float valueCastIf;
  [SerializeField]
  private AuraCurseData auracurseCastIf;
  [Header("If you can cast, choose among the possible targets")]
  [SerializeField]
  private Enums.TargetCast targetCast;

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

  public int AddCardRound
  {
    get => this.addCardRound;
    set => this.addCardRound = value;
  }

  public int Priority
  {
    get => this.priority;
    set => this.priority = value;
  }

  public float PercentToCast
  {
    get => this.percentToCast;
    set => this.percentToCast = value;
  }

  public Enums.OnlyCastIf OnlyCastIf
  {
    get => this.onlyCastIf;
    set => this.onlyCastIf = value;
  }

  public float ValueCastIf
  {
    get => this.valueCastIf;
    set => this.valueCastIf = value;
  }

  public AuraCurseData AuracurseCastIf
  {
    get => this.auracurseCastIf;
    set => this.auracurseCastIf = value;
  }

  public Enums.TargetCast TargetCast
  {
    get => this.targetCast;
    set => this.targetCast = value;
  }
}
