// Decompiled with JetBrains decompiler
// Type: CharacterMeter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMeter : MonoBehaviour
{
  public TMP_Text[] stats;
  public Transform[] icons;
  public Transform detailedData;
  public Image image;
  public Transform content;

  public void DoStats(int _index)
  {
    int num1 = 0;
    int num2 = 0;
    StringBuilder stringBuilder = new StringBuilder();
    if (AtOManager.Instance.combatStats == null)
      AtOManager.Instance.InitCombatStats();
    if (AtOManager.Instance.combatStatsCurrent == null)
      AtOManager.Instance.InitCombatStatsCurrent();
    int num3 = 5;
    if (AtOManager.Instance.combatStats.GetLength(1) == 12)
    {
      num3 = 12;
      this.detailedData.gameObject.SetActive(true);
    }
    else
      this.detailedData.gameObject.SetActive(false);
    int num4 = 0;
    int num5 = 0;
    for (int index1 = 0; index1 < num3; ++index1)
    {
      int total = 0;
      bool flag = true;
      if (AtOManager.Instance.combatStatsCurrent != null && index1 < AtOManager.Instance.combatStatsCurrent.GetLength(1))
        num2 = AtOManager.Instance.combatStatsCurrent[_index, index1];
      int combatStat1 = AtOManager.Instance.combatStats[_index, index1];
      if (index1 != 0 && index1 > 4)
      {
        num4 += num2;
        num5 += combatStat1;
      }
      this.image.sprite = (Sprite) null;
      if (!TomeManager.Instance.IsActive())
      {
        if (AtOManager.Instance.GetHero(_index) != null && (Object) AtOManager.Instance.GetHero(_index).HeroData != (Object) null)
          this.image.sprite = AtOManager.Instance.GetHero(_index).SpriteSpeed;
      }
      else
      {
        GameObject gameObject = GameObject.Find("char" + (3 - _index).ToString());
        if ((Object) gameObject != (Object) null)
          this.image.sprite = gameObject.transform.GetComponent<SpriteRenderer>().sprite;
      }
      for (int index2 = 0; index2 < 4; ++index2)
      {
        int combatStat2 = AtOManager.Instance.combatStats[index2, index1];
        total += AtOManager.Instance.combatStats[index2, index1];
        if (AtOManager.Instance.combatStatsCurrent != null && index1 < AtOManager.Instance.combatStatsCurrent.GetLength(1))
          num1 += AtOManager.Instance.combatStatsCurrent[index2, index1];
        if (!TomeManager.Instance.IsActive() && ((bool) (Object) MatchManager.Instance || (bool) (Object) RewardsManager.Instance))
        {
          if (num2 == 0 || num2 < AtOManager.Instance.combatStatsCurrent[index2, index1])
            flag = false;
        }
        else if (combatStat1 == 0 || combatStat1 < AtOManager.Instance.combatStats[index2, index1])
          flag = false;
      }
      DamageMeterManager.Instance.SetTotal(index1, total);
      stringBuilder.Clear();
      if (flag & index1 < 5)
        stringBuilder.Append("<color=#FFCC00>");
      if (!TomeManager.Instance.IsActive() && ((bool) (Object) MatchManager.Instance || (bool) (Object) RewardsManager.Instance))
      {
        if (index1 > 4 && num2 > 0)
          stringBuilder.Append("<color=#D9D9D9>");
        if (num2 > 0)
          stringBuilder.Append(num2);
        else
          stringBuilder.Append("-");
      }
      else
      {
        if (index1 > 4 && combatStat1 > 0)
          stringBuilder.Append("<color=#D9D9D9>");
        stringBuilder.Append(combatStat1);
      }
      if (!TomeManager.Instance.IsActive() && ((bool) (Object) MatchManager.Instance || (bool) (Object) RewardsManager.Instance))
      {
        if (index1 < 5)
          stringBuilder.Append("\n<size=-12><color=#CCC>(");
        else
          stringBuilder.Append("\n<size=-12><color=#888>(");
        stringBuilder.Append(combatStat1);
        stringBuilder.Append(")</color></size>");
      }
      if (flag & index1 < 5)
        stringBuilder.Append("</color>");
      this.stats[index1].text = stringBuilder.ToString();
    }
    if ((Object) this.image.sprite == (Object) null)
      this.content.gameObject.SetActive(false);
    else
      this.content.gameObject.SetActive(true);
  }
}
