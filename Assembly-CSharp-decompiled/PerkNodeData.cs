// Decompiled with JetBrains decompiler
// Type: PerkNodeData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "New Perk Node", menuName = "Perk Node Data", order = 57)]
public class PerkNodeData : ScriptableObject
{
  [SerializeField]
  private string id = "";
  [SerializeField]
  private int type;
  [SerializeField]
  private Sprite sprite;
  [SerializeField]
  private int column;
  [SerializeField]
  private int row;
  [SerializeField]
  private bool lockedInTown;
  [SerializeField]
  private bool notStack;
  [SerializeField]
  private Enums.PerkCost cost;
  [SerializeField]
  private PerkData perk;
  [SerializeField]
  private PerkNodeData perkRequired;
  [SerializeField]
  private PerkNodeData[] perksConnected;

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }

  public int Type
  {
    get => this.type;
    set => this.type = value;
  }

  public int Column
  {
    get => this.column;
    set => this.column = value;
  }

  public int Row
  {
    get => this.row;
    set => this.row = value;
  }

  public PerkNodeData PerkRequired
  {
    get => this.perkRequired;
    set => this.perkRequired = value;
  }

  public PerkData Perk
  {
    get => this.perk;
    set => this.perk = value;
  }

  public Sprite Sprite
  {
    get => this.sprite;
    set => this.sprite = value;
  }

  public Enums.PerkCost Cost
  {
    get => this.cost;
    set => this.cost = value;
  }

  public PerkNodeData[] PerksConnected
  {
    get => this.perksConnected;
    set => this.perksConnected = value;
  }

  public bool LockedInTown
  {
    get => this.lockedInTown;
    set => this.lockedInTown = value;
  }

  public bool NotStack
  {
    get => this.notStack;
    set => this.notStack = value;
  }
}
