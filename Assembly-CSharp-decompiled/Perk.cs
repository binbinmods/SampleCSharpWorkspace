// Decompiled with JetBrains decompiler
// Type: Perk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;

public static class Perk
{
  public static int GetMaxHealth(string _perk)
  {
    PerkData perkData = Globals.Instance.GetPerkData(_perk);
    return (UnityEngine.Object) perkData == (UnityEngine.Object) null ? 0 : perkData.MaxHealth;
  }

  public static int GetSpeed(string _perk)
  {
    PerkData perkData = Globals.Instance.GetPerkData(_perk);
    return (UnityEngine.Object) perkData == (UnityEngine.Object) null ? 0 : perkData.SpeedQuantity;
  }

  public static int GetEnergyBegin(string _perk)
  {
    PerkData perkData = Globals.Instance.GetPerkData(_perk);
    return (UnityEngine.Object) perkData == (UnityEngine.Object) null ? 0 : perkData.EnergyBegin;
  }

  public static int GetDamageBonus(string _perk, Enums.DamageType _dmgType)
  {
    PerkData perkData = Globals.Instance.GetPerkData(_perk);
    return (UnityEngine.Object) perkData == (UnityEngine.Object) null || perkData.DamageFlatBonus != _dmgType && perkData.DamageFlatBonus != Enums.DamageType.All ? 0 : perkData.DamageFlatBonusValue;
  }

  public static int GetHealBonus(string _perk)
  {
    PerkData perkData = Globals.Instance.GetPerkData(_perk);
    return (UnityEngine.Object) perkData == (UnityEngine.Object) null ? 0 : perkData.HealQuantity;
  }

  public static int GetAuraCurseBonus(string _perk, string _auraCurse)
  {
    PerkData perkData = Globals.Instance.GetPerkData(_perk);
    return (UnityEngine.Object) perkData == (UnityEngine.Object) null || !(perkData.AuracurseBonus.Id == _auraCurse) ? 0 : perkData.AuracurseBonusValue;
  }

  public static Dictionary<string, int> GetAuraCurseBonusDict(string _hero)
  {
    Dictionary<string, int> auraCurseBonusDict = new Dictionary<string, int>();
    List<string> heroPerks = PlayerManager.Instance.GetHeroPerks(_hero);
    if (heroPerks != null)
    {
      for (int index = 0; index < heroPerks.Count; ++index)
      {
        PerkData perkData = Globals.Instance.GetPerkData(heroPerks[index]);
        if ((UnityEngine.Object) perkData != (UnityEngine.Object) null && (UnityEngine.Object) perkData.AuracurseBonus != (UnityEngine.Object) null)
        {
          if (auraCurseBonusDict.ContainsKey(perkData.AuracurseBonus.Id))
            auraCurseBonusDict[perkData.AuracurseBonus.Id] += perkData.AuracurseBonusValue;
          else
            auraCurseBonusDict.Add(perkData.AuracurseBonus.Id, perkData.AuracurseBonusValue);
        }
      }
    }
    return auraCurseBonusDict;
  }

  public static int GetResistBonus(string _perk, Enums.DamageType _dmgType)
  {
    PerkData perkData = Globals.Instance.GetPerkData(_perk);
    return (UnityEngine.Object) perkData == (UnityEngine.Object) null || perkData.ResistModified != _dmgType && perkData.ResistModified != Enums.DamageType.All ? 0 : perkData.ResistModifiedValue;
  }

  public static int GetCurrencyBonus(string _perk)
  {
    PerkData perkData = Globals.Instance.GetPerkData(_perk);
    return (UnityEngine.Object) perkData == (UnityEngine.Object) null ? 0 : perkData.AdditionalCurrency;
  }

  public static int GetShardsBonus(string _perk)
  {
    PerkData perkData = Globals.Instance.GetPerkData(_perk);
    return (UnityEngine.Object) perkData == (UnityEngine.Object) null ? 0 : perkData.AdditionalShards;
  }

  public static int GetPointsNeededForIndex(int _index)
  {
    switch (_index)
    {
      case 0:
        return 0;
      case 1:
        return 3;
      case 2:
        return 6;
      case 3:
        return 10;
      case 4:
        return 15;
      default:
        return 20;
    }
  }

  public static string RomanLevel(int level)
  {
    switch (level)
    {
      case 1:
        return "I";
      case 2:
        return "II";
      case 3:
        return "III";
      case 4:
        return "IV";
      case 5:
        return "V";
      case 6:
        return "VI";
      case 7:
        return "VII";
      default:
        return "";
    }
  }

  public static string PerkDescription(
    PerkData perkData,
    bool doPopup = false,
    int _index = -1,
    int pointsAvailable = -1,
    bool enabled = false,
    bool active = false)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (perkData.MaxHealth > 0)
      stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemMaxHp")), (object) perkData.IconTextValue));
    else if (perkData.AdditionalCurrency > 0)
      stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemInitialCurrency")), (object) perkData.IconTextValue));
    else if (perkData.SpeedQuantity > 0)
      stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemSpeed")), (object) perkData.IconTextValue));
    else if (perkData.HealQuantity > 0)
      stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemHealDone").Replace("<c>", "")), (object) perkData.IconTextValue));
    else if (perkData.EnergyBegin > 0)
      stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemInitialEnergy")), (object) perkData.IconTextValue));
    else if ((UnityEngine.Object) perkData.AuracurseBonus != (UnityEngine.Object) null)
      stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("perkAuraDescription")), (object) perkData.IconTextValue));
    else if (perkData.ResistModified == Enums.DamageType.All)
      stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemAllResistances")), (object) perkData.IconTextValue));
    else if (perkData.DamageFlatBonus == Enums.DamageType.All)
      stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemAllDamages")), (object) perkData.IconTextValue));
    else if (perkData.DamageFlatBonus == Enums.DamageType.All)
      stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemAllDamages")), (object) perkData.IconTextValue));
    else if (perkData.DamageFlatBonus != Enums.DamageType.None && perkData.DamageFlatBonus != Enums.DamageType.All)
      stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemSingleDamage")), (object) Enum.GetName(typeof (Enums.DamageType), (object) perkData.DamageFlatBonus), (object) perkData.IconTextValue));
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      if (stringBuilder.Length > 0 & doPopup)
      {
        if (!enabled)
        {
          stringBuilder.Append("<br></size><line-height=5><br><color=#FF6666>");
          stringBuilder.Append(string.Format(Texts.Instance.GetText("requiredPoints"), (object) Perk.GetPointsNeededForIndex(_index)));
        }
        else if (!active)
        {
          if (pointsAvailable > 0)
          {
            stringBuilder.Append("<br></size><line-height=5><br><color=#66FF66>");
            stringBuilder.Append(Texts.Instance.GetText("rankPerkPress"));
          }
          else
          {
            stringBuilder.Append("<br></size><line-height=5><br><color=#FF6666>");
            stringBuilder.Append(Texts.Instance.GetText("rankPerkNotEnough"));
            enabled = false;
          }
        }
        stringBuilder.Insert(0, "<color=#FFF><size=+5>");
      }
    }
    else if (stringBuilder.Length > 0 & doPopup)
      stringBuilder.Insert(0, "<color=#FFF><size=+5>");
    if (stringBuilder.Length <= 0)
      return "";
    stringBuilder.Replace("<c>", "");
    stringBuilder.Replace("</c>", "");
    return stringBuilder.ToString();
  }
}
