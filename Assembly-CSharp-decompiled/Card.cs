// Decompiled with JetBrains decompiler
// Type: Card
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class Card
{
  [SerializeField]
  private CardData cardData;
  [SerializeField]
  private string id;

  public void InitData()
  {
    if (!((UnityEngine.Object) this.cardData != (UnityEngine.Object) null))
      return;
    this.id = this.cardData.Id;
  }

  public CardData CardData
  {
    get => this.cardData;
    set => this.cardData = value;
  }

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }
}
