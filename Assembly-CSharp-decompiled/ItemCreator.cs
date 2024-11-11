// Decompiled with JetBrains decompiler
// Type: ItemCreator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreator : MonoBehaviour
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
      this.GenerateItems();
      foreach (Dropdown dropElement in this.dropElements)
        dropElement.options.Clear();
      this.dropElements[0].AddOptions(this.cardList);
      this.dropElements[1].AddOptions(new List<string>()
      {
        "Hero1",
        "Hero2",
        "Hero3",
        "Hero4"
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

  private void GenerateItems() => this.StartCoroutine(this.GenerateItemsWait());

  private IEnumerator GenerateItemsWait()
  {
    while (Globals.Instance.Cards == null)
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    for (int index = 0; index < Globals.Instance.CardListByClass[Enums.CardClass.Item].Count; ++index)
      this.cardList.Add(Globals.Instance.CardListByClass[Enums.CardClass.Item][index]);
    this.cardList.Sort();
  }

  public void GenerateAction()
  {
    string text1 = this.dropElements[0].options[this.dropElements[0].value].text;
    string text2 = this.dropElements[1].options[this.dropElements[1].value].text;
    int _heroIndex = 0;
    switch (text2)
    {
      case "Hero1":
        _heroIndex = 0;
        break;
      case "Hero2":
        _heroIndex = 1;
        break;
      case "Hero3":
        _heroIndex = 2;
        break;
      case "Hero4":
        _heroIndex = 3;
        break;
    }
    AtOManager.Instance.AddItemToHero(_heroIndex, text1);
  }
}
