// Decompiled with JetBrains decompiler
// Type: TMPro.TMP_DigitValidator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;

namespace TMPro
{
  [Serializable]
  public class TMP_DigitValidator : TMP_InputValidator
  {
    public override char Validate(ref string text, ref int pos, char ch)
    {
      if (ch < '0' || ch > '9')
        return char.MinValue;
      ++pos;
      return ch;
    }
  }
}
