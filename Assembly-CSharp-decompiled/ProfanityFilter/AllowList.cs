// Decompiled with JetBrains decompiler
// Type: ProfanityFilter.AllowList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using ProfanityFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ProfanityFilter
{
  public class AllowList : IAllowList
  {
    private List<string> _allowList;

    public AllowList() => this._allowList = new List<string>();

    public ReadOnlyCollection<string> ToList => new ReadOnlyCollection<string>((IList<string>) this._allowList);

    public void Add(string wordToAllowlist)
    {
      if (string.IsNullOrEmpty(wordToAllowlist))
        throw new ArgumentNullException(nameof (wordToAllowlist));
      if (this._allowList.Contains(wordToAllowlist.ToLower(CultureInfo.InvariantCulture)))
        return;
      this._allowList.Add(wordToAllowlist.ToLower(CultureInfo.InvariantCulture));
    }

    public bool Contains(string wordToCheck)
    {
      if (string.IsNullOrEmpty(wordToCheck))
        throw new ArgumentNullException(nameof (wordToCheck));
      return this._allowList.Contains(wordToCheck.ToLower(CultureInfo.InvariantCulture));
    }

    public int Count => this._allowList.Count;

    public void Clear() => this._allowList.Clear();

    public bool Remove(string wordToRemove)
    {
      if (string.IsNullOrEmpty(wordToRemove))
        throw new ArgumentNullException(nameof (wordToRemove));
      return this._allowList.Remove(wordToRemove.ToLower(CultureInfo.InvariantCulture));
    }
  }
}
