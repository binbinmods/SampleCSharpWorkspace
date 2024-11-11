// Decompiled with JetBrains decompiler
// Type: NPC
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NPC : Character
{
  [SerializeField]
  private string internalId = "";
  private CardItem currentCardItem;

  public void InitData()
  {
    if (!((UnityEngine.Object) this.NpcData != (UnityEngine.Object) null))
      return;
    this.GameName = this.NpcData.Id;
    this.ScriptableObjectName = this.NpcData.ScriptableObjectName;
    this.NpcData.NPCName = Texts.Instance.GetText(this.GameName + "_name", "monsters");
    this.SourceName = this.NpcData.NPCName;
    this.ClassName = this.SubclassName = "Monster";
    this.SpriteSpeed = this.NpcData.SpriteSpeed;
    this.SpritePortrait = this.NpcData.SpritePortrait;
    this.Id = this.NpcData.Id + "_" + this.internalId;
    this.Hp = this.HpCurrent = this.NpcData.Hp;
    this.Energy = this.EnergyCurrent = 10000;
    this.EnergyTurn = 0;
    this.Speed = this.NpcData.Speed;
    this.IsHero = false;
    this.ResistSlashing = this.NpcData.ResistSlashing;
    if (this.ResistSlashing >= 100)
      this.ImmuneSlashing = true;
    this.ResistBlunt = this.NpcData.ResistBlunt;
    if (this.ResistBlunt >= 100)
      this.ImmuneBlunt = true;
    this.ResistPiercing = this.NpcData.ResistPiercing;
    if (this.ResistPiercing >= 100)
      this.ImmunePiercing = true;
    this.ResistFire = this.NpcData.ResistFire;
    if (this.ResistFire >= 100)
      this.ImmuneFire = true;
    this.ResistCold = this.NpcData.ResistCold;
    if (this.ResistCold >= 100)
      this.ImmuneCold = true;
    this.ResistLightning = this.NpcData.ResistLightning;
    if (this.ResistLightning >= 100)
      this.ImmuneLightning = true;
    this.ResistMind = this.NpcData.ResistMind;
    if (this.ResistMind >= 100)
      this.ImmuneMind = true;
    this.ResistHoly = this.NpcData.ResistHoly;
    if (this.ResistHoly >= 100)
      this.ImmuneHoly = true;
    this.ResistShadow = this.NpcData.ResistShadow;
    if (this.ResistShadow >= 100)
      this.ImmuneShadow = true;
    for (int index = 0; index < this.NpcData.AuracurseImmune.Count; ++index)
    {
      string lower = this.NpcData.AuracurseImmune[index].Trim().ToLower();
      bool flag = true;
      if (lower == "bleed" && AtOManager.Instance.TeamHavePerk("mainperkbleed2c"))
        flag = false;
      if (lower == "sight" && AtOManager.Instance.TeamHavePerk("mainperksight1c"))
        flag = false;
      if (flag && !this.AuracurseImmune.Contains(lower))
        this.AuracurseImmune.Add(lower);
    }
    this.Alive = true;
    this.AuraList = new List<Aura>();
    this.SetInitalCards(this.NpcData);
    switch (AtOManager.Instance.GetNgPlus())
    {
      case 1:
        if (AtOManager.Instance.GetTownTier() == 0)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp - (float) this.Hp * 0.1f);
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 1)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp - (float) this.Hp * 0.05f);
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 2 || AtOManager.Instance.GetTownTier() != 3)
          break;
        break;
      case 2:
        if (AtOManager.Instance.GetTownTier() != 0 && AtOManager.Instance.GetTownTier() != 1)
        {
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.1f);
            ++this.Speed;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.15f);
            ++this.Speed;
            break;
          }
          break;
        }
        break;
      case 3:
        if (AtOManager.Instance.GetTownTier() == 0)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.05f);
          ++this.Speed;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 1)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.1f);
          ++this.Speed;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 2)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.15f);
          ++this.Speed;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 3)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.2f);
          ++this.Speed;
          break;
        }
        break;
      case 4:
        if (AtOManager.Instance.GetTownTier() == 0)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.15f);
          ++this.Speed;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 1)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.25f);
          ++this.Speed;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 2)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.3f);
          this.Speed += 2;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 3)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.35f);
          this.Speed += 2;
          break;
        }
        break;
      case 5:
        if (AtOManager.Instance.GetTownTier() == 0)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.25f);
          this.Speed += 2;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 1)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.35f);
          this.Speed += 2;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 2)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.4f);
          this.Speed += 2;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 3)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.45f);
          this.Speed += 2;
          break;
        }
        break;
      case 6:
        if (AtOManager.Instance.GetTownTier() == 0)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.3f);
          this.Speed += 2;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 1)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.45f);
          this.Speed += 2;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 2)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.5f);
          this.Speed += 2;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 3)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.55f);
          this.Speed += 3;
          break;
        }
        break;
      case 7:
        if (AtOManager.Instance.GetTownTier() == 0)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.35f);
          this.Speed += 2;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 1)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.5f);
          this.Speed += 2;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 2)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.55f);
          this.Speed += 3;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 3)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.6f);
          this.Speed += 3;
          break;
        }
        break;
      case 8:
        if (AtOManager.Instance.GetTownTier() == 0)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.4f);
          this.Speed += 2;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 1)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.55f);
          this.Speed += 3;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 2)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.6f);
          this.Speed += 3;
          break;
        }
        if (AtOManager.Instance.GetTownTier() == 3)
        {
          this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.65f);
          this.Speed += 3;
          break;
        }
        break;
    }
    if (AtOManager.Instance.IsChallengeTraitActive("fastmonsters"))
      ++this.Speed;
    if (AtOManager.Instance.IsChallengeTraitActive("slowmonsters"))
      --this.Speed;
    if (AtOManager.Instance.IsChallengeTraitActive("ludicrousspeed"))
      this.Speed += 3;
    if (AtOManager.Instance.IsChallengeTraitActive("vigorousmonsters"))
      this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.25f);
    if (AtOManager.Instance.IsChallengeTraitActive("gargantuanmonsters"))
      this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * 0.5f);
    if (AtOManager.Instance.IsChallengeTraitActive("punymonsters"))
      this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp - (float) this.Hp * 0.25f);
    if (AtOManager.Instance.Sandbox_additionalMonsterHP != 0)
      this.Hp = this.HpCurrent = Functions.FuncRoundToInt((float) this.Hp + (float) this.Hp * ((float) AtOManager.Instance.Sandbox_additionalMonsterHP * 0.01f));
    if (this.Hp > 0)
      return;
    this.Hp = this.HpCurrent = 1;
  }

  public bool NPCIsBoss() => (UnityEngine.Object) this.NpcData != (UnityEngine.Object) null && this.NpcData.IsBoss;

  public bool IsBigModel() => (UnityEngine.Object) this.NpcData != (UnityEngine.Object) null && this.NpcData.BigModel;

  private void SetInitalCards(NPCData npcData)
  {
    this.Cards = new List<string>();
    if (npcData.AICards == null)
      return;
    for (int index1 = 0; index1 < npcData.AICards.Length; ++index1)
    {
      for (int index2 = 0; index2 < npcData.AICards[index1].UnitsInDeck; ++index2)
        this.Cards.Add(npcData.AICards[index1].Card.Id);
    }
  }

  private float GetCardPriorityValue(string cardName)
  {
    for (int index = 0; index < this.NpcData.AICards.Length; ++index)
    {
      if (cardName == this.NpcData.AICards[index].Card.Id)
        return (float) this.NpcData.AICards[index].Priority + 1f / 1000f * this.NpcData.AICards[index].PercentToCast;
    }
    return 10000f;
  }

  public string GetCardPriorityText(string _cardName)
  {
    string str = _cardName.Split('_', StringSplitOptions.None)[0];
    for (int index = 0; index < this.NpcData.AICards.Length; ++index)
    {
      if (str == this.NpcData.AICards[index].Card.Id)
      {
        AICards aiCard = this.NpcData.AICards[index];
        string cardPriorityText = "";
        if (aiCard.TargetCast == Enums.TargetCast.Back)
          cardPriorityText = Texts.Instance.GetText("priorityBack");
        else if (aiCard.TargetCast == Enums.TargetCast.Middle)
          cardPriorityText = Texts.Instance.GetText("priorityMiddle");
        else if (aiCard.TargetCast == Enums.TargetCast.AnyButFront)
          cardPriorityText = Texts.Instance.GetText("priorityFront");
        else if (aiCard.TargetCast == Enums.TargetCast.AnyButBack)
          cardPriorityText = Texts.Instance.GetText("priorityAnyButBack");
        else if (aiCard.TargetCast == Enums.TargetCast.LessHealthPercent)
          cardPriorityText = Texts.Instance.GetText("priorityLessHealthPercent");
        else if (aiCard.TargetCast == Enums.TargetCast.MoreHealthPercent)
          cardPriorityText = Texts.Instance.GetText("priorityMoreHealthPercent");
        else if (aiCard.TargetCast == Enums.TargetCast.LessHealthFlat)
          cardPriorityText = Texts.Instance.GetText("priorityLessHealthFlat");
        else if (aiCard.TargetCast == Enums.TargetCast.MoreHealthFlat)
          cardPriorityText = Texts.Instance.GetText("priorityMoreHealthFlat");
        else if (aiCard.TargetCast == Enums.TargetCast.LessHealthAbsolute)
          cardPriorityText = Texts.Instance.GetText("priorityLessHealthAbsolute");
        else if (aiCard.TargetCast == Enums.TargetCast.MoreHealthAbsolute)
          cardPriorityText = Texts.Instance.GetText("priorityMoreHealthAbsolute");
        else if (aiCard.TargetCast == Enums.TargetCast.LessInitiative)
          cardPriorityText = Texts.Instance.GetText("priorityLessInitiative");
        else if (aiCard.TargetCast == Enums.TargetCast.MoreInitiative)
          cardPriorityText = Texts.Instance.GetText("priorityMoreInitiative");
        return cardPriorityText;
      }
    }
    return "";
  }

  public void CheckRevealCardsCurse()
  {
    if (MatchManager.Instance.CountNPCHand(this.NPCIndex) < 1)
      return;
    int numCards = 0;
    for (int index = 0; index < this.AuraList.Count; ++index)
    {
      if (this.AuraList[index] != null && (UnityEngine.Object) this.AuraList[index].ACData != (UnityEngine.Object) null && this.AuraList[index].ACData.RevealCardsPerCharge > 0)
        numCards += this.AuraList[index].ACData.RevealCardsPerCharge * this.AuraList[index].AuraCharges;
    }
    if (numCards <= 0)
      return;
    this.RevealCards(numCards);
  }

  public void RevealCards(int numCards)
  {
    if (this.NPCItem.cardsCI.Length == 0)
      return;
    int num1 = MatchManager.Instance.CountNPCHand(this.NPCIndex);
    if (num1 < 1 || numCards < 1)
      return;
    if (num1 <= numCards)
    {
      for (int index = 0; index < this.NPCItem.cardsCI.Length; ++index)
      {
        if ((UnityEngine.Object) this.NPCItem.cardsCI[index] != (UnityEngine.Object) null)
          this.NPCItem.cardsCI[index].RevealCard();
      }
    }
    else
    {
      int num2 = 0;
      for (int index = 0; index < this.NPCItem.cardsCI.Length; ++index)
      {
        if ((UnityEngine.Object) this.NPCItem.cardsCI[index] != (UnityEngine.Object) null && this.NPCItem.cardsCI[index].IsRevealed())
          ++num2;
      }
      if (num2 >= numCards)
        return;
      for (int index = 0; index < numCards - num2; ++index)
      {
        bool flag = false;
        int num3 = 0;
        while (num3 < 50 && !flag)
        {
          int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, this.NPCItem.cardsCI.Length);
          if ((UnityEngine.Object) this.NPCItem.cardsCI[randomIntRange] != (UnityEngine.Object) null && !this.NPCItem.cardsCI[randomIntRange].IsRevealed())
          {
            this.NPCItem.cardsCI[randomIntRange].RevealCard();
            flag = true;
          }
          else
            ++num3;
        }
      }
    }
  }

  public void RedrawRevealedCards()
  {
    for (int index = 0; index < this.NPCItem.cardsCI.Length; ++index)
    {
      if ((UnityEngine.Object) this.NPCItem.cardsCI[index] != (UnityEngine.Object) null && this.NPCItem.cardsCI[index].cardrevealed)
        this.NPCItem.cardsCI[index].RedrawDescriptionPrecalculatedNPC(this);
    }
  }

  public override void BeginTurn() => base.BeginTurn();

  public override void BeginTurnPerks() => base.BeginTurnPerks();

  public override void BeginRound()
  {
    base.BeginRound();
    if ((UnityEngine.Object) this.NpcData != (UnityEngine.Object) null && this.NpcData.AICards != null)
    {
      for (int index1 = 0; index1 < this.NpcData.AICards.Length; ++index1)
      {
        if (this.NpcData.AICards[index1] != null && this.NpcData.AICards[index1].AddCardRound == MatchManager.Instance.GetCurrentRound())
        {
          for (int index2 = 0; index2 < this.NpcData.AICards[index1].UnitsInDeck; ++index2)
            MatchManager.Instance.AddCardToNPCDeck(this.NPCIndex, this.NpcData.AICards[index1].Card.Id);
        }
      }
    }
    this.CreateOverDeck(true);
  }

  public override void CreateOverDeck(bool getCardFromDeck)
  {
    if ((UnityEngine.Object) this.NpcData == (UnityEngine.Object) null)
      return;
    int num1 = this.NpcData.CardsInHand;
    if (num1 < 1)
      num1 = 1;
    List<CardItem> cardItemList = new List<CardItem>();
    List<Transform> transformList = new List<Transform>();
    int num2 = 0;
    for (int position = 0; position < num1; ++position)
    {
      if (getCardFromDeck)
        MatchManager.Instance.GetCardFromDeckToHandNPC(this.NPCIndex);
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.NPCItem.cardsGOT);
      gameObject.gameObject.SetActive(false);
      gameObject.transform.localScale = Vector3.zero;
      transformList.Add(gameObject.transform);
      CardItem component = gameObject.GetComponent<CardItem>();
      component.SetCard(MatchManager.Instance.CardFromNPCHand(this.NPCIndex, position), false, _theNPC: this);
      gameObject.name = component.InternalId;
      if ((UnityEngine.Object) component.CardData != (UnityEngine.Object) null && component.CardData.Corrupted)
        ++num2;
      cardItemList.Add(component);
    }
    if (num2 > 0)
    {
      for (int position = num1; position < num1 + num2; ++position)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.NPCItem.cardsGOT);
        gameObject.gameObject.SetActive(false);
        gameObject.transform.localScale = Vector3.zero;
        if (getCardFromDeck)
          MatchManager.Instance.GetCardFromDeckToHandNPC(this.NPCIndex);
        transformList.Add(gameObject.transform);
        CardItem component = gameObject.GetComponent<CardItem>();
        component.SetCard(MatchManager.Instance.CardFromNPCHand(this.NPCIndex, position), false, _theNPC: this);
        cardItemList.Add(component);
      }
    }
    for (int index = cardItemList.Count - 1; index >= 0; --index)
    {
      if ((UnityEngine.Object) cardItemList[index] == (UnityEngine.Object) null || (UnityEngine.Object) cardItemList[index].CardData == (UnityEngine.Object) null)
      {
        cardItemList.RemoveAt(index);
        transformList.RemoveAt(index);
      }
    }
    this.NPCItem.cardsCI = new CardItem[cardItemList.Count];
    this.NPCItem.cardsT = new Transform[cardItemList.Count];
    this.NPCItem.cardsCI = cardItemList.ToArray();
    this.NPCItem.cardsT = transformList.ToArray();
    SortedList<double, CardItem> sortedList = new SortedList<double, CardItem>();
    for (int index = 0; index < this.NPCItem.cardsCI.Length; ++index)
    {
      if (index < this.NPCItem.cardsCI.Length && (UnityEngine.Object) this.NPCItem.cardsCI[index] != (UnityEngine.Object) null && (UnityEngine.Object) this.NPCItem.cardsCI[index].CardData != (UnityEngine.Object) null)
      {
        string cardName = this.NPCItem.cardsCI[index].CardData.Id.Split('_', StringSplitOptions.None)[0];
        sortedList.Add((double) this.GetCardPriorityValue(cardName) + 1E-06 * (double) (index + 1), this.NPCItem.cardsCI[index]);
      }
    }
    int num3 = 0;
    foreach (KeyValuePair<double, CardItem> keyValuePair in sortedList)
    {
      CardItem cardItem = keyValuePair.Value;
      cardItem.PositionCardInNPC(sortedList.Count - 1 - num3, sortedList.Count);
      cardItem.DefaultElementsLayeringOrder(100 * num3);
      cardItem.CreateColliderAdjusted();
      cardItem.ShowBackImage(true);
      cardItem.ShowCardNPC(num3);
      if (cardItem.CardData.Corrupted)
        cardItem.RevealCard();
      ++num3;
    }
    MatchManager.Instance.RemoveCardsFromNPCHand(this.NPCIndex, num3);
    this.CheckRevealCardsCurse();
  }

  public void CastCardNPC(int theCard, Transform targetT)
  {
    if (!((UnityEngine.Object) this.NPCItem.cardsCI[theCard] != (UnityEngine.Object) null))
      return;
    this.currentCardItem = this.NPCItem.cardsCI[theCard];
    this.currentCardItem.CastCardNPC(targetT);
    this.currentCardItem.RemoveAmplifyNewCard();
    this.NPCItem.cardsCI[theCard] = (CardItem) null;
  }

  public void CastCardNPCEnd()
  {
    if (!((UnityEngine.Object) this.currentCardItem != (UnityEngine.Object) null))
      return;
    this.currentCardItem.DiscardCardNPC(0);
    this.currentCardItem = (CardItem) null;
  }

  public void DiscardHand()
  {
    if (!((UnityEngine.Object) this.NPCItem != (UnityEngine.Object) null))
      return;
    for (int cardPosition = 0; cardPosition < this.NPCItem.cardsCI.Length; ++cardPosition)
    {
      if ((UnityEngine.Object) this.NPCItem.cardsCI[cardPosition] != (UnityEngine.Object) null)
      {
        MatchManager.Instance.NPCDiscard(this.NPCIndex, cardPosition, false);
        this.NPCItem.cardsCI[cardPosition].DiscardCardNPC(this.NPCItem.cardsCI.Length - 1 - cardPosition);
      }
    }
    MatchManager.Instance.ResetDeckHandNPC(this.NPCIndex);
  }

  public string InternalId
  {
    get => this.internalId;
    set => this.internalId = value;
  }
}
