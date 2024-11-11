// Decompiled with JetBrains decompiler
// Type: CorruptionPackData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Corruption CardPack", menuName = "New Corruption CardPack", order = 65)]
public class CorruptionPackData : ScriptableObject
{
  [Header("Name and class")]
  [SerializeField]
  private string packName;
  [SerializeField]
  private Enums.CardClass packClass;
  [SerializeField]
  private int packTier;
  [SerializeField]
  private List<CardData> lowPack;
  [SerializeField]
  private List<CardData> highPack;

  public string PackName
  {
    get => this.packName;
    set => this.packName = value;
  }

  public Enums.CardClass PackClass
  {
    get => this.packClass;
    set => this.packClass = value;
  }

  public int PackTier
  {
    get => this.packTier;
    set => this.packTier = value;
  }

  public List<CardData> LowPack
  {
    get => this.lowPack;
    set => this.lowPack = value;
  }

  public List<CardData> HighPack
  {
    get => this.highPack;
    set => this.highPack = value;
  }
}
