// Decompiled with JetBrains decompiler
// Type: CombatEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class CombatEffect
{
  [SerializeField]
  private AuraCurseData auraCurse;
  [SerializeField]
  private int auraCurseCharges;
  [SerializeField]
  private Enums.CombatUnit auraCurseTarget;

  public AuraCurseData AuraCurse
  {
    get => this.auraCurse;
    set => this.auraCurse = value;
  }

  public int AuraCurseCharges
  {
    get => this.auraCurseCharges;
    set => this.auraCurseCharges = value;
  }

  public Enums.CombatUnit AuraCurseTarget
  {
    get => this.auraCurseTarget;
    set => this.auraCurseTarget = value;
  }
}
