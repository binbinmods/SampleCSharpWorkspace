// Decompiled with JetBrains decompiler
// Type: PlayerDeck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

[Serializable]
public class PlayerDeck
{
  public Dictionary<string, string[]> DeckTitle = new Dictionary<string, string[]>();
  public Dictionary<string, List<string>[]> DeckCards = new Dictionary<string, List<string>[]>();
}
