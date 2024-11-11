// Decompiled with JetBrains decompiler
// Type: PerksWindowUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PerksWindowUI : MonoBehaviour
{
  public TMP_Text[] perkTextBlock;

  public bool IsActive() => this.gameObject.activeSelf;

  public void DoPerks(Hero currentHero)
  {
    for (int index = 0; index < this.perkTextBlock.Length; ++index)
      this.perkTextBlock[index].text = "";
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    List<string> heroPerks = PlayerManager.Instance.GetHeroPerks(currentHero.Id);
    for (int index1 = 0; index1 < this.perkTextBlock.Length; ++index1)
    {
      int num = 0;
      for (int index2 = 0; index2 < 6; ++index2)
      {
        string id = currentHero.ClassName + (index1 + 1).ToString();
        if (index2 == 0)
          id += "a";
        else if (index2 == 1)
          id += "b";
        else if (index2 == 2)
          id += "c";
        else if (index2 == 3)
          id += "d";
        else if (index2 == 4)
          id += "e";
        else if (index2 == 5)
          id += "f";
        PerkData perkData = Globals.Instance.GetPerkData(id);
        if ((Object) perkData != (Object) null)
        {
          bool flag = false;
          if (heroPerks != null && heroPerks.Contains(id))
          {
            flag = true;
            stringBuilder2.Append("<color=#E0A44E><b>");
            ++num;
          }
          stringBuilder2.Append("<sprite name=" + perkData.Icon.name + ">");
          stringBuilder2.Append(Perk.PerkDescription(perkData));
          if (flag)
            stringBuilder2.Append("</b></color>");
          stringBuilder2.Append("<br>");
          stringBuilder1.Append(stringBuilder2.ToString());
          stringBuilder2.Clear();
        }
      }
      stringBuilder2.Append("<size=+1.2><sprite name=perk><color=#CC81FF><b>");
      stringBuilder2.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("rankTier")), (object) Perk.RomanLevel(index1 + 1)).ToUpper());
      stringBuilder2.Append("</b></color></size>  <color=#FFF><size=+.3>(");
      stringBuilder2.Append(num);
      stringBuilder2.Append("/6)</size></color>");
      stringBuilder2.Append("<br><line-height=20%><br></line-height><color=#777>");
      stringBuilder2.Append(stringBuilder1.ToString());
      this.perkTextBlock[index1].text = stringBuilder2.ToString();
      stringBuilder1.Clear();
      stringBuilder2.Clear();
    }
  }

  public void DoPerksOld(Hero currentHero)
  {
    for (int index = 0; index < this.perkTextBlock.Length; ++index)
    {
      this.perkTextBlock[index].gameObject.SetActive(false);
      this.perkTextBlock[index].text = "";
    }
    List<string> heroPerks = PlayerManager.Instance.GetHeroPerks(currentHero.Id);
    if (heroPerks != null && heroPerks.Count > 0)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      for (int index1 = 0; index1 < heroPerks.Count; ++index1)
      {
        for (int index2 = 0; index2 < 6; ++index2)
        {
          string id = currentHero.ClassName + (index1 + 1).ToString();
          if (index2 == 0)
            id += "a";
          else if (index2 == 1)
            id += "b";
          else if (index2 == 2)
            id += "c";
          else if (index2 == 3)
            id += "d";
          else if (index2 == 4)
            id += "e";
          else if (index2 == 5)
            id += "f";
          PerkData perkData = Globals.Instance.GetPerkData(id);
          if ((Object) perkData != (Object) null)
          {
            stringBuilder2.Append("<sprite name=" + perkData.IconTextValue + ">");
            stringBuilder2.Append(Perk.PerkDescription(perkData));
            stringBuilder1.Append(stringBuilder2.ToString());
          }
        }
        this.perkTextBlock[index1].text += stringBuilder1.ToString();
        stringBuilder1.Clear();
      }
      for (int index = 0; index < this.perkTextBlock.Length; ++index)
      {
        if (this.perkTextBlock[index].text != "")
        {
          stringBuilder1.Append("<size=+.8><sprite name=perk><color=#CC81FF>");
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("rankTier"), (object) Perk.RomanLevel(index + 1)));
          stringBuilder1.Append("</color></size><br><br>");
          this.perkTextBlock[index].text = stringBuilder1.ToString() + this.perkTextBlock[index].text;
          stringBuilder1.Clear();
          this.perkTextBlock[index].gameObject.SetActive(true);
        }
      }
    }
    else
    {
      this.perkTextBlock[0].gameObject.SetActive(true);
      this.perkTextBlock[0].text = "<size=+2>" + Texts.Instance.GetText("perkNone");
    }
  }
}
