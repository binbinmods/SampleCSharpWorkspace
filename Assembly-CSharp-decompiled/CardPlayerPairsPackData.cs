// Decompiled with JetBrains decompiler
// Type: CardPlayerPairsPackData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "New CardPlayerPairsPack", menuName = "New CardPlayerPairsPack", order = 64)]
public class CardPlayerPairsPackData : ScriptableObject
{
  [SerializeField]
  private string packId;
  [Header("Pack Cards (will instance two cards for each)")]
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

  public string PackId
  {
    get => this.packId;
    set => this.packId = value;
  }
}
