// Decompiled with JetBrains decompiler
// Type: DeckWindowUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckWindowUI : MonoBehaviour
{
  public Transform injuryTitle;
  public TMP_Text injuryText;
  public Transform injuryContent;
  public Transform deckTitle;
  public TMP_Text deckText;
  public Transform deckContent;
  public Transform unlockedTitle;
  public Transform unlockedContent;
  public Transform upgradedTitle;
  public Transform scrollContent;
  private List<string> deckCards = new List<string>();
  private List<string> injuryCards = new List<string>();
  private int currentIndex = -1;

  private void Start() => this.Resize();

  public bool IsActive() => this.gameObject.activeSelf;

  public void ShowUnlockedCards(List<string> _unlockedCards)
  {
    if (_unlockedCards == null || _unlockedCards.Count == 0)
      return;
    List<string> listCards = new List<string>();
    for (int index = 0; index < _unlockedCards.Count; ++index)
    {
      CardData cardData = Globals.Instance.GetCardData(_unlockedCards[index], false);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && cardData.ShowInTome)
        listCards.Add(_unlockedCards[index]);
    }
    this.unlockedTitle.gameObject.SetActive(true);
    this.upgradedTitle.gameObject.SetActive(false);
    this.deckTitle.gameObject.SetActive(false);
    this.injuryTitle.gameObject.SetActive(false);
    this.injuryContent.gameObject.SetActive(false);
    this.Show(listCards: listCards);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("unlockedCards"));
    stringBuilder.Append(" <color=#AAA>[<color=#888>");
    stringBuilder.Append(listCards.Count);
    stringBuilder.Append("</color>]</color>");
    if (!((UnityEngine.Object) this.unlockedTitle.GetChild(0) != (UnityEngine.Object) null) || !((UnityEngine.Object) this.unlockedTitle.GetChild(0).GetComponent<TMP_Text>() != (UnityEngine.Object) null))
      return;
    this.unlockedTitle.GetChild(0).GetComponent<TMP_Text>().text = stringBuilder.ToString();
  }

  public void ShowUpgradedCards(List<string> upgradedCards)
  {
    this.upgradedTitle.gameObject.SetActive(true);
    this.unlockedTitle.gameObject.SetActive(false);
    this.deckTitle.gameObject.SetActive(false);
    this.injuryTitle.gameObject.SetActive(false);
    this.injuryContent.gameObject.SetActive(false);
    List<string> listCards = new List<string>();
    for (int index = 0; index < upgradedCards.Count; ++index)
      listCards.Add("char_" + index.ToString() + "_" + upgradedCards[index]);
    this.Show(listCards: listCards, sort: false);
    AtOManager.Instance.upgradedCardsList = new List<string>();
  }

  public void Show(int index = -1, List<string> listCards = null, bool discard = false, bool sort = true, bool isHero = true)
  {
    if (!(bool) (UnityEngine.Object) MatchManager.Instance)
    {
      if (index > -1)
        this.SetDecks(index);
      if (listCards == null)
        return;
      this.SetList(listCards, sort);
    }
    else if (listCards != null)
    {
      this.SetList(listCards, sort);
    }
    else
    {
      if (index <= -1)
        return;
      this.StartCoroutine(this.SetCombatDeck(index, discard));
    }
  }

  public void DestroyDeck()
  {
    if ((UnityEngine.Object) this.deckContent == (UnityEngine.Object) null)
      return;
    foreach (Component component in this.deckContent)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    GameManager.Instance.CleanTempContainer();
    PopupManager.Instance.ClosePopup();
  }

  public void Hide() => this.DestroyDeck();

  public void Resize()
  {
  }

  public void SetList(List<string> cardList, bool sort)
  {
    if (sort)
      cardList.Sort();
    foreach (Component component in this.deckContent)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    for (int index = 0; index < cardList.Count; ++index)
      this.SetCard((Hero) null, 0, cardList[index]);
  }

  private string formatNum(int num)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(" <color=#AAA>[<color=#888>");
    stringBuilder.Append(num);
    stringBuilder.Append("</color>]</color>");
    return stringBuilder.ToString();
  }

  public void HideInjury()
  {
    this.injuryTitle.gameObject.SetActive(false);
    this.injuryContent.gameObject.SetActive(false);
  }

  public void SetTitle(string title, int num = -1)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(title);
    if (num > -1)
      stringBuilder.Append(this.formatNum(num));
    this.deckText.text = stringBuilder.ToString();
  }

  public void SetDecks(int heroIndex)
  {
    this.currentIndex = heroIndex;
    Hero hero = AtOManager.Instance.GetHero(heroIndex);
    if (hero == null)
      return;
    this.deckCards.Clear();
    this.injuryCards.Clear();
    for (int index = 0; index < hero.Cards.Count; ++index)
    {
      CardData cardData = Globals.Instance.GetCardData(hero.Cards[index], false);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
      {
        if (cardData.CardClass != Enums.CardClass.Injury && cardData.CardClass != Enums.CardClass.Boon)
          this.deckCards.Add(cardData.Id);
        else
          this.injuryCards.Add(cardData.Id);
      }
    }
    this.deckCards.Sort();
    this.injuryCards.Sort();
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("heroCards").Replace("<hero>", hero.SourceName));
    stringBuilder.Append(this.formatNum(this.deckCards.Count));
    this.deckText.text = stringBuilder.ToString();
    foreach (Component component in this.deckContent)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    for (int index = 0; index < this.deckCards.Count; ++index)
      this.SetCard(hero, 0, this.deckCards[index]);
    if (this.injuryCards.Count == 0)
    {
      this.injuryTitle.gameObject.SetActive(false);
      this.injuryContent.gameObject.SetActive(false);
    }
    else
    {
      stringBuilder.Clear();
      stringBuilder.Append(Texts.Instance.GetText("heroInjuriesBoons").Replace("<hero>", hero.SourceName));
      stringBuilder.Append(this.formatNum(this.injuryCards.Count));
      this.injuryText.text = stringBuilder.ToString();
      this.injuryTitle.gameObject.SetActive(true);
      this.injuryContent.gameObject.SetActive(true);
      foreach (Component component in this.injuryContent)
        UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
      for (int index = 0; index < this.injuryCards.Count; ++index)
        this.SetCard(hero, 1, this.injuryCards[index]);
    }
  }

  private void SetCard(Hero hero, int type, string cardId)
  {
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, this.transform.position, Quaternion.identity, !this.unlockedTitle.gameObject.activeSelf ? (type != 0 ? this.injuryContent : this.deckContent) : this.unlockedContent);
    gameObject.AddComponent(typeof (ContentSizeFitter));
    CardItem component = gameObject.GetComponent<CardItem>();
    gameObject.name = "TMP_" + type.ToString() + "_" + cardId;
    string[] strArray = cardId.Split('_', StringSplitOptions.None);
    if (strArray[0] == "char")
      component.SetCard(strArray[2], _theHero: hero);
    else
      component.SetCard(cardId, _theHero: hero);
    component.TopLayeringOrder("UI", 20000);
    component.transform.localScale = Vector3.zero;
    component.SetDestinationLocalScale(1.02f);
    component.cardmakebig = true;
    component.cardmakebigSize = 1.02f;
    component.cardmakebigSizeMax = 1.2f;
    component.active = true;
    component.lockPosition = true;
    component.DisableTrail();
    component.CreateColliderAdjusted();
  }

  public IEnumerator SetCombatDeck(int heroIndex, bool discard)
  {
    this.currentIndex = heroIndex;
    Hero hero = AtOManager.Instance.GetHero(heroIndex);
    if (hero != null)
    {
      foreach (Component component in this.deckContent)
        UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
      this.injuryText.gameObject.SetActive(false);
      this.injuryTitle.gameObject.SetActive(false);
      StringBuilder stringBuilder = new StringBuilder();
      if (!discard)
      {
        List<string> heroDeck = MatchManager.Instance.GetHeroDeck(this.currentIndex);
        this.deckCards.Clear();
        for (int index = 0; index < heroDeck.Count; ++index)
          this.deckCards.Add(heroDeck[index]);
        this.deckCards.Sort();
        stringBuilder.Append(Texts.Instance.GetText("heroDrawPile").Replace("<hero>", hero.SourceName));
      }
      else
      {
        List<string> heroDiscard = MatchManager.Instance.GetHeroDiscard(this.currentIndex);
        List<string> stringList = new List<string>();
        for (int index = heroDiscard.Count - 1; index >= 0; --index)
          stringList.Add(heroDiscard[index]);
        this.deckCards.Clear();
        for (int index = 0; index < stringList.Count; ++index)
          this.deckCards.Add(stringList[index]);
        stringBuilder.Append(Texts.Instance.GetText("heroDiscardPile").Replace("<hero>", hero.SourceName));
      }
      if (this.deckCards != null)
      {
        stringBuilder.Append(this.formatNum(this.deckCards.Count));
        this.deckText.text = stringBuilder.ToString();
        int totalCards = this.deckCards.Count;
        for (int i = 0; i < this.deckCards.Count; ++i)
        {
          this.SetCombatCard(hero, this.deckCards[i], i, totalCards);
          yield return (object) null;
        }
      }
    }
  }

  private void SetCombatCard(Hero hero, string cardId, int position, int total)
  {
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, this.transform.position, Quaternion.identity, this.deckContent);
    gameObject.AddComponent(typeof (ContentSizeFitter));
    CardItem component = gameObject.GetComponent<CardItem>();
    gameObject.name = "TMP_" + cardId;
    component.SetCard(cardId, _theHero: hero);
    component.TopLayeringOrder("UI", 20000);
    component.transform.localScale = Vector3.zero;
    component.SetDestinationLocalScale(1.02f);
    component.cardmakebig = true;
    component.cardmakebigSize = 1.02f;
    component.cardmakebigSizeMax = 1.2f;
    component.active = true;
    component.lockPosition = true;
    component.DisableTrail();
    component.CreateColliderAdjusted();
  }
}
