// Decompiled with JetBrains decompiler
// Type: CardPlayerPackData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "New CardPlayerPack", menuName = "New CardPlayerPack", order = 64)]
public class CardPlayerPackData : ScriptableObject
{
  [SerializeField]
  private string packId;
  [Header("Pack Cards")]
  [SerializeField]
  private CardData card0;
  [SerializeField]
  private bool card0RandomBoon;
  [SerializeField]
  private bool card0RandomInjury;
  [SerializeField]
  private CardData card1;
  [SerializeField]
  private bool card1RandomBoon;
  [SerializeField]
  private bool card1RandomInjury;
  [SerializeField]
  private CardData card2;
  [SerializeField]
  private bool card2RandomBoon;
  [SerializeField]
  private bool card2RandomInjury;
  [SerializeField]
  private CardData card3;
  [SerializeField]
  private bool card3RandomBoon;
  [SerializeField]
  private bool card3RandomInjury;
  [Header("Difficulty +- X (Base = 10)")]
  [SerializeField]
  private int modSpeed;
  [SerializeField]
  private int modIterations;

  public string PackId
  {
    get => this.packId;
    set => this.packId = value;
  }

  public CardData Card0
  {
    get => this.card0;
    set => this.card0 = value;
  }

  public bool Card0RandomBoon
  {
    get => this.card0RandomBoon;
    set => this.card0RandomBoon = value;
  }

  public bool Card0RandomInjury
  {
    get => this.card0RandomInjury;
    set => this.card0RandomInjury = value;
  }

  public CardData Card1
  {
    get => this.card1;
    set => this.card1 = value;
  }

  public bool Card1RandomBoon
  {
    get => this.card1RandomBoon;
    set => this.card1RandomBoon = value;
  }

  public bool Card1RandomInjury
  {
    get => this.card1RandomInjury;
    set => this.card1RandomInjury = value;
  }

  public CardData Card2
  {
    get => this.card2;
    set => this.card2 = value;
  }

  public bool Card2RandomBoon
  {
    get => this.card2RandomBoon;
    set => this.card2RandomBoon = value;
  }

  public bool Card2RandomInjury
  {
    get => this.card2RandomInjury;
    set => this.card2RandomInjury = value;
  }

  public CardData Card3
  {
    get => this.card3;
    set => this.card3 = value;
  }

  public bool Card3RandomBoon
  {
    get => this.card3RandomBoon;
    set => this.card3RandomBoon = value;
  }

  public bool Card3RandomInjury
  {
    get => this.card3RandomInjury;
    set => this.card3RandomInjury = value;
  }

  public int ModSpeed
  {
    get => this.modSpeed;
    set => this.modSpeed = value;
  }

  public int ModIterations
  {
    get => this.modIterations;
    set => this.modIterations = value;
  }
}
