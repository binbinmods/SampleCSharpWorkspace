// Decompiled with JetBrains decompiler
// Type: LootData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot Table Data", order = 57)]
public class LootData : ScriptableObject
{
  [SerializeField]
  private string id;
  [SerializeField]
  private int numItems;
  [SerializeField]
  private int goldQuantity;
  [SerializeField]
  private LootItem[] lootItemTable;
  [SerializeField]
  private float defaultPercentUncommon;
  [SerializeField]
  private float defaultPercentRare;
  [SerializeField]
  private float defaultPercentEpic;
  [SerializeField]
  private float defaultPercentMythic;

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }

  public int NumItems
  {
    get => this.numItems;
    set => this.numItems = value;
  }

  public LootItem[] LootItemTable
  {
    get => this.lootItemTable;
    set => this.lootItemTable = value;
  }

  public float DefaultPercentUncommon
  {
    get => this.defaultPercentUncommon;
    set => this.defaultPercentUncommon = value;
  }

  public float DefaultPercentRare
  {
    get => this.defaultPercentRare;
    set => this.defaultPercentRare = value;
  }

  public float DefaultPercentEpic
  {
    get => this.defaultPercentEpic;
    set => this.defaultPercentEpic = value;
  }

  public float DefaultPercentMythic
  {
    get => this.defaultPercentMythic;
    set => this.defaultPercentMythic = value;
  }

  public int GoldQuantity
  {
    get => this.goldQuantity;
    set => this.goldQuantity = value;
  }
}
