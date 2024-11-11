// Decompiled with JetBrains decompiler
// Type: PlayerPerk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

[Serializable]
public class PlayerPerk
{
  public Dictionary<string, string[]> PerkConfigTitle = new Dictionary<string, string[]>();
  public Dictionary<string, List<string>[]> PerkConfigPerks = new Dictionary<string, List<string>[]>();
  public Dictionary<string, int[]> PerkConfigPoints = new Dictionary<string, int[]>();
}
