// Decompiled with JetBrains decompiler
// Type: CardCreator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCreator : MonoBehaviour
{
  public Dropdown[] dropElements;
  private List<string> cardList = new List<string>();
  private bool created;

  public void Draw()
  {
    if (this.gameObject.activeSelf)
    {
      this.gameObject.SetActive(false);
    }
    else
    {
      this.gameObject.SetActive(true);
      if (this.created)
        return;
      this.GenerateCards();
      foreach (Dropdown dropElement in this.dropElements)
        dropElement.options.Clear();
      this.dropElements[0].AddOptions(this.cardList);
      this.dropElements[1].AddOptions(new List<string>()
      {
        "Hand",
        "Discard",
        "TopDeck",
        "BottomDeck",
        "RandomDeck"
      });
      this.dropElements[2].AddOptions(new List<string>()
      {
        "1",
        "2",
        "3"
      });
      this.created = true;
    }
  }

  public void SelectByName(string value)
  {
    int num = -1;
    for (int index = 0; index < this.cardList.Count; ++index)
    {
      if (this.cardList[index].StartsWith(value))
      {
        num = index;
        break;
      }
    }
    if (num <= -1)
      return;
    this.dropElements[0].value = num;
  }

  private void GenerateCards() => this.StartCoroutine(this.GenerateCardsWait());

  private IEnumerator GenerateCardsWait()
  {
    while (Globals.Instance.Cards == null)
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    foreach (KeyValuePair<string, CardData> card in Globals.Instance.Cards)
      this.cardList.Add(card.Value.Id);
    this.cardList.Sort();
  }

  public void GenerateAction()
  {
    string text1 = this.dropElements[0].options[this.dropElements[0].value].text;
    string text2 = this.dropElements[2].options[this.dropElements[2].value].text;
    Enums.CardPlace where;
    switch (this.dropElements[1].options[this.dropElements[1].value].text)
    {
      case "Hand":
        where = Enums.CardPlace.Hand;
        break;
      case "Discard":
        where = Enums.CardPlace.Discard;
        break;
      case "TopDeck":
        where = Enums.CardPlace.TopDeck;
        break;
      case "BottomDeck":
        where = Enums.CardPlace.BottomDeck;
        break;
      default:
        where = Enums.CardPlace.RandomDeck;
        break;
    }
    MatchManager.Instance.CardCreatorAction(int.Parse(text2), text1, true, where, false);
  }
}
