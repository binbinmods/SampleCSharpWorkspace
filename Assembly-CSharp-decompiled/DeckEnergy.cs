// Decompiled with JetBrains decompiler
// Type: DeckEnergy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DeckEnergy : MonoBehaviour
{
  public TMP_Text cardChallengeEnergy;
  public TMP_Text[] cardChallengeBarSup;
  public Transform[] cardChallengeBar;
  private Dictionary<int, int> dictEnergyCost = new Dictionary<int, int>();

  public void WriteEnergy(int _heroIndex, int _type)
  {
    Hero hero = AtOManager.Instance.GetHero(_heroIndex);
    if (hero == null)
      return;
    List<string> stringList = new List<string>();
    this.dictEnergyCost.Clear();
    switch (_type)
    {
      case 0:
        stringList = hero.Cards;
        break;
      case 1:
        stringList = MatchManager.Instance.GetHeroDeck(_heroIndex);
        break;
      case 2:
        stringList = MatchManager.Instance.GetHeroDiscard(_heroIndex);
        break;
      case 3:
        stringList = MatchManager.Instance.GetHeroVanish(_heroIndex);
        break;
    }
    float num1 = 0.0f;
    int num2 = 0;
    for (int index = 0; index < stringList.Count; ++index)
    {
      if (stringList[index] != "")
      {
        CardData cardData;
        int num3;
        if ((bool) (Object) MatchManager.Instance)
        {
          cardData = MatchManager.Instance.GetCardData(stringList[index]);
          num3 = hero.GetCardFinalCost(cardData);
        }
        else
        {
          cardData = Globals.Instance.GetCardData(stringList[index], false);
          num3 = cardData.EnergyCost;
        }
        if ((Object) cardData != (Object) null && cardData.Playable)
        {
          num1 += (float) num3;
          this.AddToDictEnergy(num3);
          ++num2;
        }
      }
    }
    if (num2 == 0)
    {
      this.gameObject.SetActive(false);
    }
    else
    {
      this.gameObject.SetActive(true);
      float num4 = num1 / (float) num2;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Texts.Instance.GetText("challengeEnergy"));
      stringBuilder.Append("<br><color=#FF9F2A><size=+2>");
      stringBuilder.Append(num4.ToString("0.00"));
      stringBuilder.Append("  <sprite name=energy>");
      this.cardChallengeEnergy.text = stringBuilder.ToString();
      this.WriteEnergyCostBars();
    }
  }

  private void AddToDictEnergy(int value)
  {
    if (value > 5)
      value = 5;
    if (this.dictEnergyCost.ContainsKey(value))
      this.dictEnergyCost[value]++;
    else
      this.dictEnergyCost.Add(value, 1);
  }

  private void WriteEnergyCostBars()
  {
    float num1 = -1f;
    foreach (KeyValuePair<int, int> keyValuePair in this.dictEnergyCost)
    {
      if ((double) keyValuePair.Value >= (double) num1)
        num1 = (float) keyValuePair.Value;
    }
    float num2 = 0.8f;
    for (int key = 0; key < 6; ++key)
    {
      int num3 = 0;
      if (this.dictEnergyCost.ContainsKey(key))
        num3 = this.dictEnergyCost[key];
      this.cardChallengeBar[key].transform.localScale = new Vector3(0.25f, num2 * (float) num3 / num1, 1f);
      if (this.dictEnergyCost.ContainsKey(key))
      {
        this.cardChallengeBarSup[key].text = this.dictEnergyCost[key].ToString();
        this.cardChallengeBarSup[key].transform.localPosition = new Vector3(this.cardChallengeBarSup[key].transform.localPosition.x, this.cardChallengeBar[key].transform.localScale.y + 0.15f, this.cardChallengeBarSup[key].transform.localPosition.z);
      }
      else
        this.cardChallengeBarSup[key].text = "";
    }
  }
}
