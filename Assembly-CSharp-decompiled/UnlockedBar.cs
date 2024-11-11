// Decompiled with JetBrains decompiler
// Type: UnlockedBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UnlockedBar : MonoBehaviour
{
  public string type;
  public Transform maskTransform;
  public SpriteRenderer barSprite;
  public TMP_Text titleText;
  public TMP_Text cardsText;
  public SpriteRenderer sigil0;
  public SpriteRenderer sigil1;
  public SpriteRenderer sigil2;
  public SpriteRenderer sigil3;
  public SpriteRenderer sigil4;
  private float scale100 = 3.38f;
  private int cardsTotal = -1;
  private int cardsUnlocked;

  public void InitBar()
  {
    this.cardsTotal = -1;
    this.cardsUnlocked = 0;
    this.SetBasics();
    this.CalculateUnlock();
  }

  private void SetBasics()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=+.3>");
    if (this.type == "warriorCards")
    {
      this.titleText.color = this.cardsText.color = this.barSprite.color = Functions.HexToColor(Globals.Instance.ClassColor["warrior"]);
      stringBuilder.Append("<sprite name=slashing>");
    }
    else if (this.type == "scoutCards")
    {
      this.titleText.color = this.cardsText.color = this.barSprite.color = Functions.HexToColor(Globals.Instance.ClassColor["scout"]);
      stringBuilder.Append("<sprite name=piercing>");
    }
    else if (this.type == "mageCards")
    {
      this.titleText.color = this.cardsText.color = this.barSprite.color = Functions.HexToColor(Globals.Instance.ClassColor["mage"]);
      stringBuilder.Append("<sprite name=fire>");
    }
    else if (this.type == "healerCards")
    {
      this.titleText.color = this.cardsText.color = this.barSprite.color = Functions.HexToColor(Globals.Instance.ClassColor["healer"]);
      stringBuilder.Append("<sprite name=heal>");
    }
    else if (this.type == "equipment")
    {
      this.titleText.color = this.cardsText.color = this.barSprite.color = Functions.HexToColor(Globals.Instance.ClassColor["item"]);
      stringBuilder.Append("<sprite name=jewelry>");
    }
    else if (this.type == "mapNodes")
    {
      this.titleText.color = this.cardsText.color = this.barSprite.color = Functions.HexToColor(Globals.Instance.ClassColor["boon"]);
      stringBuilder.Append("<sprite name=node>");
    }
    else if (this.type == "uniqueBosses")
    {
      this.titleText.color = this.cardsText.color = this.barSprite.color = Functions.HexToColor("#784FD4");
      stringBuilder.Append("<sprite name=bossIcon>");
    }
    stringBuilder.Append("</size> ");
    stringBuilder.Append(Texts.Instance.GetText(this.type));
    this.titleText.text = stringBuilder.ToString();
  }

  private void CalculateUnlock()
  {
    if (this.cardsTotal == -1)
    {
      Enums.CardClass key = Enums.CardClass.Warrior;
      bool flag1 = false;
      bool flag2 = false;
      if (this.type == "warriorCards")
        key = Enums.CardClass.Warrior;
      else if (this.type == "scoutCards")
        key = Enums.CardClass.Scout;
      else if (this.type == "mageCards")
        key = Enums.CardClass.Mage;
      else if (this.type == "healerCards")
        key = Enums.CardClass.Healer;
      else if (this.type == "equipment")
        key = Enums.CardClass.Item;
      else if (this.type == "mapNodes")
        flag1 = true;
      else if (this.type == "uniqueBosses")
        flag2 = true;
      if (flag1)
      {
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        this.cardsUnlocked = 0;
        foreach (KeyValuePair<string, NodeData> keyValuePair in Globals.Instance.NodeDataSource)
        {
          if (keyValuePair.Key != "tutorial_0" && keyValuePair.Key != "tutorial_1" && keyValuePair.Key != "tutorial_2" && (keyValuePair.Value.NodeZone.ZoneId == "Aquarfall" || keyValuePair.Value.NodeZone.ZoneId == "Sectarium" || keyValuePair.Value.NodeZone.ZoneId == "Senenthia" || keyValuePair.Value.NodeZone.ZoneId == "Spiderlair" || keyValuePair.Value.NodeZone.ZoneId == "Velkarath" || keyValuePair.Value.NodeZone.ZoneId == "Voidhigh" || keyValuePair.Value.NodeZone.ZoneId == "Voidlow" || keyValuePair.Value.NodeZone.ZoneId == "Faeborg" || keyValuePair.Value.NodeZone.ZoneId == "Frozensewers" || keyValuePair.Value.NodeZone.ZoneId == "Blackforge" || keyValuePair.Value.NodeZone.ZoneId == "Ulminin" || keyValuePair.Value.NodeZone.ZoneId == "Pyramid") && !keyValuePair.Value.GoToTown && !keyValuePair.Value.TravelDestination)
            stringList1.Add(keyValuePair.Key);
        }
        if (PlayerManager.Instance.UnlockedNodes != null)
        {
          for (int index = 0; index < PlayerManager.Instance.UnlockedNodes.Count; ++index)
          {
            if (Globals.Instance.NodeCombatEventRelation.ContainsKey(PlayerManager.Instance.UnlockedNodes[index]))
            {
              string str = Globals.Instance.NodeCombatEventRelation[PlayerManager.Instance.UnlockedNodes[index]];
              if (stringList1.Contains(str))
                stringList2.Add(str);
            }
          }
        }
        this.cardsTotal = stringList1.Count;
        this.cardsUnlocked = stringList2.Count;
      }
      else if (flag2)
      {
        this.cardsUnlocked = PlayerManager.Instance.BossesKilledName == null ? 0 : PlayerManager.Instance.BossesKilledName.Count;
        this.cardsTotal = 0;
        List<string> stringList = new List<string>();
        foreach (KeyValuePair<string, NPCData> npC in Globals.Instance.NPCs)
        {
          if (npC.Value.IsBoss && !stringList.Contains(npC.Value.NPCName))
          {
            stringList.Add(npC.Value.NPCName);
            ++this.cardsTotal;
          }
        }
      }
      else
      {
        List<string> stringList = Globals.Instance.CardListNotUpgradedByClass[key];
        this.cardsTotal = stringList.Count;
        for (int index = 0; index < this.cardsTotal; ++index)
        {
          if (PlayerManager.Instance.IsCardUnlocked(stringList[index]))
            ++this.cardsUnlocked;
        }
      }
    }
    if (this.cardsUnlocked > this.cardsTotal)
      this.cardsUnlocked = this.cardsTotal;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=+1.4>");
    stringBuilder.Append(this.cardsUnlocked);
    stringBuilder.Append("</size> <voffset=.4><color=#333>/");
    stringBuilder.Append(this.cardsTotal);
    stringBuilder.Append("</color>");
    this.cardsText.text = stringBuilder.ToString();
    this.maskTransform.localScale = new Vector3((float) this.cardsUnlocked / (float) this.cardsTotal * this.scale100, 2.03f, this.maskTransform.localScale.z);
  }
}
