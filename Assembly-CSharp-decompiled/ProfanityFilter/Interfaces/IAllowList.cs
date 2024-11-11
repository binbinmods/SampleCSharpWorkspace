// Decompiled with JetBrains decompiler
// Type: ProfanityFilter.Interfaces.IAllowList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.ObjectModel;

namespace ProfanityFilter.Interfaces
{
  public interface IAllowList
  {
    void Add(string wordToAllowlist);

    bool Contains(string wordToCheck);

    bool Remove(string wordToRemove);

    void Clear();

    int Count { get; }

    ReadOnlyCollection<string> ToList { get; }
  }
}
