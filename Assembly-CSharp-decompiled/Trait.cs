// Decompiled with JetBrains decompiler
// Type: Trait
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public class Trait
{
  private Enums.EventActivation theEvent;
  private Character character;
  private Character target;
  private int auxInt;
  private string auxString = "";
  private CardData castedCard;

  public void DoTrait(
    Enums.EventActivation _theEvent,
    string _trait,
    Character _character,
    Character _target,
    int _auxInt,
    string _auxString,
    CardData _castedCard)
  {
    if ((Object) MatchManager.Instance == (Object) null)
      return;
    this.character = _character;
    this.target = _target;
    this.theEvent = _theEvent;
    this.auxInt = _auxInt;
    this.auxString = _auxString;
    this.castedCard = _castedCard;
    MethodInfo method = this.GetType().GetMethod(_trait);
    if (method != (MethodInfo) null)
      method.Invoke((object) this, (object[]) null);
    else
      Debug.LogWarning((object) ("The trait method called " + _trait + " is missing on the Trait Class"));
  }

  public void DoTraitFunction(string _trait)
  {
    MethodInfo method = this.GetType().GetMethod(_trait);
    if (method != (MethodInfo) null)
      method.Invoke((object) this, (object[]) null);
    else
      Debug.LogWarning((object) ("The trait method called " + _trait + " is missing on the Trait Class"));
  }

  private string TextChargesLeft(int currentCharges, int chargesTotal)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<br><color=#FFF>");
    stringBuilder.Append(currentCharges.ToString());
    stringBuilder.Append("/");
    stringBuilder.Append(chargesTotal.ToString());
    return stringBuilder.ToString();
  }

  public void accurateshots()
  {
    if (this.target == null || !this.target.Alive)
      return;
    this.target.SetAuraTrait(this.character, "sight", 2);
    this.target.SetAuraTrait(this.character, "bleed", 1);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Accurate Shots"), Enums.CombatScrollEffectType.Trait);
  }

  public void ardent()
  {
    this.character.SetAuraTrait(this.character, "powerful", 2);
    this.character.SetAuraTrait(this.character, "burn", 2);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Ardent"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("powerful", true, this.character.HeroItem.CharImageT, false);
    EffectsManager.Instance.PlayEffectAC("burnsmall", true, this.character.HeroItem.CharImageT, false);
  }

  public void blessed()
  {
    this.character.SetAuraTrait(this.character, "bless", 1);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Blessed"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("bless", true, this.character.HeroItem.CharImageT, false);
  }

  public void bloody()
  {
    if (this.target == null || !this.target.Alive)
      return;
    this.target.SetAuraTrait(this.character, "bleed", 2);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Bloody"), Enums.CombatScrollEffectType.Trait);
  }

  public void butcher()
  {
    if (this.target == null || this.target.IsHero || !MatchManager.Instance.AnyNPCAlive())
      return;
    if (!this.character.HaveTrait("threestarchef"))
    {
      switch (MatchManager.Instance.GetRandomIntRange(0, 3, "trait"))
      {
        case 0:
          string cardInDictionary1 = MatchManager.Instance.CreateCardInDictionary("premiummeat");
          MatchManager.Instance.GetCardData(cardInDictionary1);
          MatchManager.Instance.GenerateNewCard(1, cardInDictionary1, false, Enums.CardPlace.RandomDeck, heroIndex: this.character.HeroIndex);
          break;
        case 1:
          string cardInDictionary2 = MatchManager.Instance.CreateCardInDictionary("meat");
          MatchManager.Instance.GetCardData(cardInDictionary2);
          MatchManager.Instance.GenerateNewCard(1, cardInDictionary2, false, Enums.CardPlace.RandomDeck, heroIndex: this.character.HeroIndex);
          break;
        default:
          string cardInDictionary3 = MatchManager.Instance.CreateCardInDictionary("spoiledmeat");
          MatchManager.Instance.GetCardData(cardInDictionary3);
          MatchManager.Instance.GenerateNewCard(1, cardInDictionary3, false, Enums.CardPlace.RandomDeck, heroIndex: this.character.HeroIndex);
          break;
      }
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Butcher"), Enums.CombatScrollEffectType.Trait);
    }
    else
    {
      string cardInDictionary = MatchManager.Instance.CreateCardInDictionary("gourmetmeat");
      MatchManager.Instance.GetCardData(cardInDictionary);
      MatchManager.Instance.GenerateNewCard(1, cardInDictionary, false, Enums.CardPlace.RandomDeck, heroIndex: this.character.HeroIndex);
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_threestarchef"), Enums.CombatScrollEffectType.Trait);
    }
    MatchManager.Instance.ItemTraitActivated();
  }

  public void cantor()
  {
    if (this.character.GetAuraCharges("stanzai") != 0 && this.character.GetAuraCharges("stanzaii") != 0 && this.character.GetAuraCharges("stanzaiii") != 0)
      return;
    this.character.SetAuraTrait(this.character, "stanzai", 1);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Cantor"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("songself", true, this.character.HeroItem.CharImageT, false);
  }

  public void cautious()
  {
    this.character.SetAuraTrait(this.character, "buffer", 2);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Cautious"), Enums.CombatScrollEffectType.Trait);
  }

  public void chastise()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (chastise));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (chastise)) && MatchManager.Instance.activatedTraits[nameof (chastise)] > traitData.TimesPerTurn - 1)
      return;
    if (MatchManager.Instance.CountHeroHand() == 10)
    {
      Debug.Log((object) "[TRAIT EXECUTION] Broke because player at max cards");
    }
    else
    {
      if (!this.castedCard.GetCardTypes().Contains(Enums.CardType.Holy_Spell) || !((Object) this.character.HeroData != (Object) null))
        return;
      if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (chastise)))
        MatchManager.Instance.activatedTraits.Add(nameof (chastise), 1);
      else
        ++MatchManager.Instance.activatedTraits[nameof (chastise)];
      MatchManager.Instance.SetTraitInfoText();
      string str = "holysmite";
      int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, 100, "trait");
      string cardInDictionary = MatchManager.Instance.CreateCardInDictionary(randomIntRange >= 45 ? (randomIntRange >= 90 ? str + "rare" : str + "b") : str + "a");
      CardData cardData = MatchManager.Instance.GetCardData(cardInDictionary);
      cardData.Vanish = true;
      cardData.EnergyReductionToZeroPermanent = true;
      MatchManager.Instance.GenerateNewCard(1, cardInDictionary, false, Enums.CardPlace.Hand);
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Chastise") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (chastise)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
      MatchManager.Instance.ItemTraitActivated();
      MatchManager.Instance.CreateLogCardModification(cardData.InternalId, MatchManager.Instance.GetHero(this.character.HeroIndex));
    }
  }

  public void choir()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || MatchManager.Instance.GetCurrentRound() != 1)
      return;
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    for (int index = 0; index < teamHero.Length; ++index)
    {
      if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
        teamHero[index].SetAuraTrait(this.character, "stanzai", 1);
    }
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Choir"), Enums.CombatScrollEffectType.Trait);
  }

  public void clever()
  {
    this.character.SetAuraTrait(this.character, "inspire", 1);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Clever"), Enums.CombatScrollEffectType.Trait);
  }

  public void combatready()
  {
    this.character.SetAuraTrait(this.character, "energize", 1);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Combat Ready"), Enums.CombatScrollEffectType.Trait);
  }

  public void countermeasures()
  {
    if (!((Object) MatchManager.Instance != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (countermeasures));
    if (MatchManager.Instance.activatedTraitsRound != null && MatchManager.Instance.activatedTraitsRound.ContainsKey(nameof (countermeasures)) && MatchManager.Instance.activatedTraitsRound[nameof (countermeasures)] > traitData.TimesPerRound - 1 || this.character == null || !this.character.Alive || !((Object) this.character.HeroItem != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraitsRound.ContainsKey(nameof (countermeasures)))
      MatchManager.Instance.activatedTraitsRound.Add(nameof (countermeasures), 1);
    else
      ++MatchManager.Instance.activatedTraitsRound[nameof (countermeasures)];
    MatchManager.Instance.SetTraitInfoText();
    this.character.SetAuraTrait(this.character, "thorns", 3);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Countermeasures") + this.TextChargesLeft(MatchManager.Instance.activatedTraitsRound[nameof (countermeasures)], traitData.TimesPerRound), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("thorns", true, this.character.HeroItem.CharImageT, false);
  }

  public void darkfeast()
  {
    if (!((Object) this.character.HeroData != (Object) null))
      return;
    int num = Mathf.FloorToInt((float) (this.character.EffectCharges("dark") / 8));
    if (num <= 0)
      return;
    List<string> heroHand = MatchManager.Instance.GetHeroHand(this.character.HeroIndex);
    List<CardData> cardDataList = new List<CardData>();
    for (int index = 0; index < heroHand.Count; ++index)
    {
      CardData cardData = MatchManager.Instance.GetCardData(heroHand[index]);
      if (cardData.GetCardFinalCost() > 0)
        cardDataList.Add(cardData);
    }
    for (int index = 0; index < cardDataList.Count; ++index)
    {
      CardData cardData = cardDataList[index];
      cardData.EnergyReductionTemporal += num;
      MatchManager.Instance.UpdateHandCards();
      CardItem fromTableByIndex = MatchManager.Instance.GetCardFromTableByIndex(cardData.InternalId);
      fromTableByIndex.PlayDissolveParticle();
      fromTableByIndex.ShowEnergyModification(-num);
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Dark Feast"), Enums.CombatScrollEffectType.Trait);
      MatchManager.Instance.CreateLogCardModification(cardData.InternalId, MatchManager.Instance.GetHero(this.character.HeroIndex));
    }
  }

  public void defensemastery()
  {
    if (!((Object) this.character.HeroData != (Object) null))
      return;
    int num = 1;
    if (num <= 0)
      return;
    List<string> heroHand = MatchManager.Instance.GetHeroHand(this.character.HeroIndex);
    List<CardData> cardDataList = new List<CardData>();
    for (int index = 0; index < heroHand.Count; ++index)
    {
      CardData cardData = MatchManager.Instance.GetCardData(heroHand[index]);
      if ((Object) cardData != (Object) null && cardData.GetCardFinalCost() > 0 && cardData.HasCardType(Enums.CardType.Defense))
        cardDataList.Add(cardData);
    }
    for (int index = 0; index < cardDataList.Count; ++index)
    {
      CardData cardData = cardDataList[index];
      if ((Object) cardData != (Object) null)
      {
        cardData.EnergyReductionTemporal += num;
        MatchManager.Instance.UpdateHandCards();
        CardItem fromTableByIndex = MatchManager.Instance.GetCardFromTableByIndex(cardData.InternalId);
        fromTableByIndex.PlayDissolveParticle();
        fromTableByIndex.ShowEnergyModification(-num);
        this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Defense Mastery"), Enums.CombatScrollEffectType.Trait);
        MatchManager.Instance.CreateLogCardModification(cardData.InternalId, MatchManager.Instance.GetHero(this.character.HeroIndex));
      }
    }
  }

  public void dinnerisready()
  {
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    for (int index = 0; index < teamHero.Length; ++index)
    {
      if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
      {
        if (!this.character.HaveTrait("threestarchef"))
        {
          switch (MatchManager.Instance.GetRandomIntRange(0, 3, "trait"))
          {
            case 0:
              string cardInDictionary1 = MatchManager.Instance.CreateCardInDictionary("premiummeat");
              MatchManager.Instance.GetCardData(cardInDictionary1);
              MatchManager.Instance.GenerateNewCard(1, cardInDictionary1, false, Enums.CardPlace.RandomDeck, heroIndex: teamHero[index].HeroIndex);
              continue;
            case 1:
              string cardInDictionary2 = MatchManager.Instance.CreateCardInDictionary("meat");
              MatchManager.Instance.GetCardData(cardInDictionary2);
              MatchManager.Instance.GenerateNewCard(1, cardInDictionary2, false, Enums.CardPlace.RandomDeck, heroIndex: teamHero[index].HeroIndex);
              continue;
            default:
              string cardInDictionary3 = MatchManager.Instance.CreateCardInDictionary("spoiledmeat");
              MatchManager.Instance.GetCardData(cardInDictionary3);
              MatchManager.Instance.GenerateNewCard(1, cardInDictionary3, false, Enums.CardPlace.RandomDeck, heroIndex: teamHero[index].HeroIndex);
              continue;
          }
        }
        else
        {
          switch (MatchManager.Instance.GetRandomIntRange(0, 3, "trait"))
          {
            case 0:
              string cardInDictionary4 = MatchManager.Instance.CreateCardInDictionary("premiummeat");
              MatchManager.Instance.GetCardData(cardInDictionary4);
              MatchManager.Instance.GenerateNewCard(1, cardInDictionary4, false, Enums.CardPlace.RandomDeck, heroIndex: teamHero[index].HeroIndex);
              continue;
            case 1:
              string cardInDictionary5 = MatchManager.Instance.CreateCardInDictionary("meat");
              MatchManager.Instance.GetCardData(cardInDictionary5);
              MatchManager.Instance.GenerateNewCard(1, cardInDictionary5, false, Enums.CardPlace.RandomDeck, heroIndex: teamHero[index].HeroIndex);
              continue;
            default:
              string cardInDictionary6 = MatchManager.Instance.CreateCardInDictionary("gourmetmeat");
              MatchManager.Instance.GetCardData(cardInDictionary6);
              MatchManager.Instance.GenerateNewCard(1, cardInDictionary6, false, Enums.CardPlace.RandomDeck, heroIndex: teamHero[index].HeroIndex);
              continue;
          }
        }
      }
    }
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_DinnerIsReady"), Enums.CombatScrollEffectType.Trait);
    MatchManager.Instance.ItemTraitActivated();
  }

  public void domeoflight()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (domeoflight));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (domeoflight)) && MatchManager.Instance.activatedTraits[nameof (domeoflight)] > traitData.TimesPerTurn - 1 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Defense))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (domeoflight)))
      MatchManager.Instance.activatedTraits.Add(nameof (domeoflight), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (domeoflight)];
    MatchManager.Instance.SetTraitInfoText();
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    for (int index = 0; index < teamHero.Length; ++index)
    {
      if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
      {
        teamHero[index].SetAuraTrait(this.character, "shield", 9);
        if ((Object) teamHero[index].HeroItem != (Object) null)
          EffectsManager.Instance.PlayEffectAC("shield1", true, teamHero[index].HeroItem.CharImageT, false);
      }
    }
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Dome Of Light") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (domeoflight)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
  }

  public void elementalproliferation()
  {
    if (this.target == null || !this.target.Alive)
      return;
    this.target.SetAuraTrait(this.character, "burn", 2);
    this.target.SetAuraTrait(this.character, "chill", 2);
    this.target.SetAuraTrait(this.character, "spark", 2);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Elemental Proliferation"), Enums.CombatScrollEffectType.Trait);
  }

  public void elementalweaver()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    string internalId = this.castedCard.InternalId;
    int _energy = 0;
    int num1 = 0;
    if (this.castedCard.GetCardTypes().Contains(Enums.CardType.Fire_Spell))
    {
      for (int index = 0; index < this.character.CardsPlayedRound.Count; ++index)
      {
        if ((Object) this.character.CardsPlayedRound[index] != (Object) null && this.character.CardsPlayedRound[index].HasCardType(Enums.CardType.Fire_Spell))
          ++num1;
      }
      if (num1 <= 1)
        ++_energy;
    }
    int num2 = 0;
    if (this.castedCard.GetCardTypes().Contains(Enums.CardType.Cold_Spell))
    {
      for (int index = 0; index < this.character.CardsPlayedRound.Count; ++index)
      {
        if ((Object) this.character.CardsPlayedRound[index] != (Object) null && this.character.CardsPlayedRound[index].HasCardType(Enums.CardType.Cold_Spell))
          ++num2;
      }
      if (num2 <= 1)
        ++_energy;
    }
    int num3 = 0;
    if (this.castedCard.GetCardTypes().Contains(Enums.CardType.Lightning_Spell))
    {
      for (int index = 0; index < this.character.CardsPlayedRound.Count; ++index)
      {
        if ((Object) this.character.CardsPlayedRound[index] != (Object) null && this.character.CardsPlayedRound[index].HasCardType(Enums.CardType.Lightning_Spell))
          ++num3;
      }
      if (num3 <= 1)
        ++_energy;
    }
    if (_energy <= 0)
      return;
    this.character.ModifyEnergy(_energy, true);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (elementalweaver)))
      MatchManager.Instance.activatedTraits.Add(nameof (elementalweaver), _energy);
    else
      MatchManager.Instance.activatedTraits[nameof (elementalweaver)] += _energy;
    MatchManager.Instance.SetTraitInfoText();
    TraitData traitData = Globals.Instance.GetTraitData(nameof (elementalweaver));
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Elemental Weaver") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (elementalweaver)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("energy", true, this.character.HeroItem.CharImageT, false);
  }

  public void engineer()
  {
    bool flag = false;
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    int index = 0;
    while (!flag)
    {
      index = MatchManager.Instance.GetRandomIntRange(0, teamHero.Length, "trait");
      if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
      {
        teamHero[index].SetAuraTrait(this.character, "energize", 1);
        flag = true;
      }
    }
    teamHero[index].HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Engineer"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("energy", true, teamHero[index].HeroItem.CharImageT, false);
  }

  public void envenom()
  {
    if (this.target == null || !this.target.Alive)
      return;
    this.target.SetAuraTrait(this.character, "poison", 3);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Envenom"), Enums.CombatScrollEffectType.Trait);
  }

  public void elusive()
  {
    if (this.character == null || !this.character.Alive)
      return;
    this.character.SetAuraTrait(this.character, "evasion", 2);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Elusive"), Enums.CombatScrollEffectType.Trait);
  }

  public void fanfare()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (fanfare));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (fanfare)) && MatchManager.Instance.activatedTraits[nameof (fanfare)] > traitData.TimesPerTurn - 1 || MatchManager.Instance.energyJustWastedByHero <= 0 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Song) || !((Object) this.character.HeroData != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (fanfare)))
      MatchManager.Instance.activatedTraits.Add(nameof (fanfare), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (fanfare)];
    MatchManager.Instance.SetTraitInfoText();
    this.character.ModifyEnergy(1, true);
    if ((Object) this.character.HeroItem != (Object) null)
    {
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Fanfare") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (fanfare)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
      EffectsManager.Instance.PlayEffectAC("energy", true, this.character.HeroItem.CharImageT, false);
    }
    Hero lowestHealthHero = this.GetLowestHealthHero();
    if (lowestHealthHero == null || !lowestHealthHero.Alive)
      return;
    lowestHealthHero.SetAuraTrait(this.character, "regeneration", 2);
    if (!((Object) lowestHealthHero.HeroItem != (Object) null))
      return;
    EffectsManager.Instance.PlayEffectAC("regeneration", true, lowestHealthHero.HeroItem.CharImageT, false);
  }

  public void firestarter()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (firestarter));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (firestarter)) && MatchManager.Instance.activatedTraits[nameof (firestarter)] > traitData.TimesPerTurn - 1 || MatchManager.Instance.energyJustWastedByHero <= 0 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Fire_Spell) || !((Object) this.character.HeroData != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (firestarter)))
      MatchManager.Instance.activatedTraits.Add(nameof (firestarter), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (firestarter)];
    MatchManager.Instance.SetTraitInfoText();
    this.character.ModifyEnergy(1, true);
    if ((Object) this.character.HeroItem != (Object) null)
    {
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Firestarter") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (firestarter)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
      EffectsManager.Instance.PlayEffectAC("energy", true, this.character.HeroItem.CharImageT, false);
    }
    bool flag = false;
    NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();
    while (!flag)
    {
      int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, teamNpc.Length, "trait");
      if (teamNpc[randomIntRange] != null && teamNpc[randomIntRange].Alive)
      {
        teamNpc[randomIntRange].SetAuraTrait(this.character, "burn", 1);
        if ((Object) teamNpc[randomIntRange].NPCItem != (Object) null)
          EffectsManager.Instance.PlayEffectAC("burnsmall", true, teamNpc[randomIntRange].NPCItem.CharImageT, false);
        flag = true;
      }
    }
  }

  public void furious()
  {
    this.character.SetAuraTrait(this.character, "fury", 2);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Furious"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("fury", true, this.character.HeroItem.CharImageT, false);
  }

  public void glasscannon()
  {
  }

  public void gluttony()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (gluttony));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (gluttony)) && MatchManager.Instance.activatedTraits[nameof (gluttony)] > traitData.TimesPerTurn - 1 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Food) || !((Object) this.character.HeroData != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (gluttony)))
      MatchManager.Instance.activatedTraits.Add(nameof (gluttony), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (gluttony)];
    MatchManager.Instance.SetTraitInfoText();
    this.character.SetAuraTrait(this.character, "vitality", 2);
    this.character.ModifyEnergy(1, true);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Gluttony") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (gluttony)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("heart", true, this.character.HeroItem.CharImageT, false);
    EffectsManager.Instance.PlayEffectAC("energy", true, this.character.HeroItem.CharImageT, false);
  }

  public void healingbrew()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (healingbrew));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (healingbrew)) && MatchManager.Instance.activatedTraits[nameof (healingbrew)] > traitData.TimesPerTurn - 1 || MatchManager.Instance.energyJustWastedByHero <= 0 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Healing_Spell) || !((Object) this.character.HeroData != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (healingbrew)))
      MatchManager.Instance.activatedTraits.Add(nameof (healingbrew), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (healingbrew)];
    MatchManager.Instance.SetTraitInfoText();
    this.character.ModifyEnergy(1, true);
    if ((Object) this.character.HeroItem != (Object) null)
    {
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Healing Brew") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (healingbrew)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
      EffectsManager.Instance.PlayEffectAC("energy", true, this.character.HeroItem.CharImageT, false);
    }
    Hero lowestHealthHero = this.GetLowestHealthHero();
    if (lowestHealthHero == null || !lowestHealthHero.Alive)
      return;
    lowestHealthHero.SetAuraTrait(this.character, "regeneration", 2);
    if (!((Object) lowestHealthHero.HeroItem != (Object) null))
      return;
    EffectsManager.Instance.PlayEffectAC("regeneration", true, lowestHealthHero.HeroItem.CharImageT, false);
  }

  public void healingsurge()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (healingsurge));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (healingsurge)) && MatchManager.Instance.activatedTraits[nameof (healingsurge)] > traitData.TimesPerTurn - 1 || MatchManager.Instance.energyJustWastedByHero <= 0 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Healing_Spell) || !((Object) this.character.HeroData != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (healingsurge)))
      MatchManager.Instance.activatedTraits.Add(nameof (healingsurge), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (healingsurge)];
    MatchManager.Instance.SetTraitInfoText();
    this.character.ModifyEnergy(1, true);
    if ((Object) this.character.HeroItem != (Object) null)
    {
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Healing Surge") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (healingsurge)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
      EffectsManager.Instance.PlayEffectAC("energy", true, this.character.HeroItem.CharImageT, false);
    }
    Hero lowestHealthHero = this.GetLowestHealthHero();
    if (lowestHealthHero == null || !lowestHealthHero.Alive)
      return;
    lowestHealthHero.SetAuraTrait(this.character, "bless", 1);
    if (!((Object) lowestHealthHero.HeroItem != (Object) null))
      return;
    EffectsManager.Instance.PlayEffectAC("bless", true, lowestHealthHero.HeroItem.CharImageT, false);
  }

  public void hexmastery()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (hexmastery));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (hexmastery)) && MatchManager.Instance.activatedTraits[nameof (hexmastery)] > traitData.TimesPerTurn - 1 || MatchManager.Instance.energyJustWastedByHero <= 0 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Curse_Spell) || !((Object) this.character.HeroData != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (hexmastery)))
      MatchManager.Instance.activatedTraits.Add(nameof (hexmastery), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (hexmastery)];
    MatchManager.Instance.SetTraitInfoText();
    this.character.SetAuraTrait(this.character, "powerful", 1);
    this.character.ModifyEnergy(1, true);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Hex Mastery") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (hexmastery)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("powerful", true, this.character.HeroItem.CharImageT, false);
    EffectsManager.Instance.PlayEffectAC("energy", true, this.character.HeroItem.CharImageT, false);
  }

  public void incantation()
  {
    bool flag = false;
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    int index = 0;
    while (!flag)
    {
      index = MatchManager.Instance.GetRandomIntRange(0, teamHero.Length, "trait");
      if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
      {
        switch (MatchManager.Instance.GetRandomIntRange(0, 4, "trait"))
        {
          case 0:
            teamHero[index].SetAuraTrait(this.character, "courage", 2);
            break;
          case 1:
            teamHero[index].SetAuraTrait(this.character, "insulate", 2);
            break;
          case 2:
            teamHero[index].SetAuraTrait(this.character, "reinforce", 2);
            break;
          default:
            teamHero[index].SetAuraTrait(this.character, "energize", 1);
            break;
        }
        flag = true;
      }
    }
    teamHero[index].HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Incantion"), Enums.CombatScrollEffectType.Trait);
  }

  public void incendiary()
  {
    if (this.target == null || !this.target.Alive)
      return;
    this.target.SetAuraTrait(this.character, "burn", 3);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Incendiary"), Enums.CombatScrollEffectType.Trait);
  }

  public void indomitable()
  {
    if (this.character == null || !this.character.Alive)
      return;
    this.character.SetAuraTrait(this.character, "shield", Functions.FuncRoundToInt((float) this.auxInt * 0.5f));
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Indomitable"), Enums.CombatScrollEffectType.Trait);
  }

  public void ironfurnace()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (ironfurnace));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (ironfurnace)) && MatchManager.Instance.activatedTraits[nameof (ironfurnace)] > traitData.TimesPerTurn - 1 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Attack))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (ironfurnace)))
      MatchManager.Instance.activatedTraits.Add(nameof (ironfurnace), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (ironfurnace)];
    MatchManager.Instance.SetTraitInfoText();
    this.character.SetAuraTrait(this.character, "block", 8);
    this.character.SetAuraTrait(this.character, "furnace", 1);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Iron Furnace") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (ironfurnace)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("firepower", true, this.character.HeroItem.CharImageT, false);
  }

  public void jinx()
  {
    if (this.target == null || !this.target.Alive)
      return;
    this.target.SetAuraTrait(this.character, "dark", 2);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Jinx"), Enums.CombatScrollEffectType.Trait);
  }

  public void keensenses()
  {
    if ((Object) this.character.HeroData != (Object) null)
    {
      NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();
      for (int index = 0; index < teamNpc.Length; ++index)
      {
        if (teamNpc[index] != null && teamNpc[index].Alive)
          teamNpc[index].SetAuraTrait(this.character, "mark", 1);
      }
    }
    else
    {
      Hero[] teamHero = MatchManager.Instance.GetTeamHero();
      for (int index = 0; index < teamHero.Length; ++index)
      {
        if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
          teamHero[index].SetAuraTrait(this.character, "mark", 1);
      }
    }
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Keen Senses"), Enums.CombatScrollEffectType.Trait);
  }

  public void lethalshots()
  {
    if (this.target == null || !this.target.Alive)
      return;
    switch (MatchManager.Instance.GetRandomIntRange(0, 3, "trait"))
    {
      case 0:
        this.target.SetAuraTrait(this.character, "bleed", 3);
        break;
      case 1:
        this.target.SetAuraTrait(this.character, "poison", 3);
        break;
      default:
        this.target.SetAuraTrait(this.character, "vulnerable", 1);
        break;
    }
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Lethal Shots"), Enums.CombatScrollEffectType.Trait);
  }

  public void maledict()
  {
    bool flag = false;
    NPC[] npcArray = (NPC[]) null;
    Hero[] heroArray = (Hero[]) null;
    if ((Object) this.character.HeroItem != (Object) null)
      npcArray = MatchManager.Instance.GetTeamNPC();
    else
      heroArray = MatchManager.Instance.GetTeamHero();
    Character character = (Character) null;
    while (!flag)
    {
      if ((Object) this.character.HeroItem != (Object) null)
      {
        int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, npcArray.Length, "trait");
        character = (Character) npcArray[randomIntRange];
      }
      else
      {
        int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, heroArray.Length, "trait");
        character = (Character) heroArray[randomIntRange];
        if ((Object) character.HeroData == (Object) null)
          character = (Character) null;
      }
      if (character != null && character.Alive)
      {
        switch (MatchManager.Instance.GetRandomIntRange(0, 3, "trait"))
        {
          case 0:
            character.SetAuraTrait(this.character, "dark", 2);
            break;
          case 1:
            character.SetAuraTrait(this.character, "slow", 1);
            break;
          default:
            character.SetAuraTrait(this.character, "vulnerable", 1);
            break;
        }
        flag = true;
      }
    }
    if (character == null)
      return;
    if ((Object) character.NPCItem != (Object) null)
      character.NPCItem.ScrollCombatText(Texts.Instance.GetText("traits_Maledict"), Enums.CombatScrollEffectType.Trait);
    else
      character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Maledict"), Enums.CombatScrollEffectType.Trait);
  }

  public void magicduality()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (magicduality));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (magicduality)) && MatchManager.Instance.activatedTraits[nameof (magicduality)] > traitData.TimesPerTurn - 1)
      return;
    for (int index1 = 0; index1 < 2; ++index1)
    {
      Enums.CardClass cardClass1;
      Enums.CardClass cardClass2;
      if (index1 == 0)
      {
        cardClass1 = Enums.CardClass.Warrior;
        cardClass2 = Enums.CardClass.Mage;
      }
      else
      {
        cardClass1 = Enums.CardClass.Mage;
        cardClass2 = Enums.CardClass.Warrior;
      }
      if (this.castedCard.CardClass == cardClass1)
      {
        if (MatchManager.Instance.CountHeroHand() == 0 || !((Object) this.character.HeroData != (Object) null))
          break;
        List<CardData> cardDataList = new List<CardData>();
        List<string> heroHand = MatchManager.Instance.GetHeroHand(this.character.HeroIndex);
        int num1 = 0;
        for (int index2 = 0; index2 < heroHand.Count; ++index2)
        {
          CardData cardData = MatchManager.Instance.GetCardData(heroHand[index2]);
          if ((Object) cardData != (Object) null && cardData.CardClass == cardClass2 && this.character.GetCardFinalCost(cardData) > num1)
            num1 = this.character.GetCardFinalCost(cardData);
        }
        if (num1 <= 0)
          break;
        for (int index3 = 0; index3 < heroHand.Count; ++index3)
        {
          CardData cardData = MatchManager.Instance.GetCardData(heroHand[index3]);
          if ((Object) cardData != (Object) null && cardData.CardClass == cardClass2 && this.character.GetCardFinalCost(cardData) >= num1)
            cardDataList.Add(cardData);
        }
        if (cardDataList.Count <= 0)
          break;
        CardData cardData1 = cardDataList.Count != 1 ? cardDataList[MatchManager.Instance.GetRandomIntRange(0, cardDataList.Count, "trait")] : cardDataList[0];
        if (!((Object) cardData1 != (Object) null))
          break;
        if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (magicduality)))
          MatchManager.Instance.activatedTraits.Add(nameof (magicduality), 1);
        else
          ++MatchManager.Instance.activatedTraits[nameof (magicduality)];
        MatchManager.Instance.SetTraitInfoText();
        int num2 = 1;
        cardData1.EnergyReductionTemporal += num2;
        MatchManager.Instance.GetCardFromTableByIndex(cardData1.InternalId).ShowEnergyModification(-num2);
        MatchManager.Instance.UpdateHandCards();
        this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_MagicDuality") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (magicduality)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
        MatchManager.Instance.CreateLogCardModification(cardData1.InternalId, MatchManager.Instance.GetHero(this.character.HeroIndex));
        break;
      }
    }
  }

  public void marksmanship()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null) || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Ranged_Attack))
      return;
    this.character.SetAuraTrait(this.character, "sharp", 2);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Marksmanship"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("sharp", true, this.character.HeroItem.CharImageT, false);
  }

  public void mentalist()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null) || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Spell))
      return;
    this.character.SetAuraTrait(this.character, "powerful", 2);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Mentalist"), Enums.CombatScrollEffectType.Trait);
  }

  public void minddevourer() => this.mentalleech();

  public void mentalleech()
  {
    bool flag = false;
    int num1 = 0;
    int num2 = 0;
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();
    for (int index = 0; index < teamNpc.Length; ++index)
    {
      if (teamNpc[index] != null && teamNpc[index].Alive)
      {
        teamNpc[index].SetAuraTrait(this.character, "insane", 1);
        if ((Object) teamNpc[index].NPCItem != (Object) null)
          EffectsManager.Instance.PlayEffectAC("mindimpact2", true, teamNpc[index].NPCItem.CharImageT, false);
      }
    }
    for (int index = 0; index < teamHero.Length; ++index)
    {
      if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
      {
        num1 += teamHero[index].GetAuraCharges("insane");
        num2 += teamHero[index].GetAuraCharges("sight");
      }
    }
    for (int index = 0; index < teamNpc.Length; ++index)
    {
      if (teamNpc[index] != null && teamNpc[index].Alive)
      {
        num1 += teamNpc[index].GetAuraCharges("insane");
        num2 += teamNpc[index].GetAuraCharges("sight");
      }
    }
    int heal = Functions.FuncRoundToInt((float) ((double) num1 * 0.20000000298023224 + (double) num2 * 0.10000000149011612));
    for (int index = 0; index < teamHero.Length; ++index)
    {
      if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
      {
        int num3 = teamHero[index].HealReceivedFinal(heal);
        int _hp = num3;
        if (teamHero[index].GetHpLeftForMax() < num3)
          _hp = teamHero[index].GetHpLeftForMax();
        if (_hp > 0)
        {
          teamHero[index].ModifyHp(_hp, false);
          AtOManager.Instance.combatStats[this.character.HeroIndex, 3] += _hp;
          AtOManager.Instance.combatStatsCurrent[this.character.HeroIndex, 3] += _hp;
          teamHero[index].HeroItem.ScrollCombatTextDamageNew(new CastResolutionForCombatText()
          {
            heal = _hp
          });
          teamHero[index].SetEvent(Enums.EventActivation.Healed);
          this.character.SetEvent(Enums.EventActivation.Heal, (Character) teamHero[index]);
          flag = true;
          if ((Object) teamHero[index].HeroItem != (Object) null)
            EffectsManager.Instance.PlayEffectAC("healimpactsmall", true, teamHero[index].HeroItem.CharImageT, false);
        }
      }
    }
    if (!flag)
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Mentalleech"), Enums.CombatScrollEffectType.Trait);
  }

  public void mindflayer()
  {
    if (MatchManager.Instance.CountHeroHand() == 10)
    {
      Debug.Log((object) "[TRAIT EXECUTION] Broke because player at max cards");
    }
    else
    {
      bool flag = false;
      Hero[] teamHero = MatchManager.Instance.GetTeamHero();
      int heroIndex = 0;
      while (!flag)
      {
        heroIndex = MatchManager.Instance.GetRandomIntRange(0, teamHero.Length, "trait");
        if (teamHero[heroIndex] != null && (Object) teamHero[heroIndex].HeroData != (Object) null && teamHero[heroIndex].Alive)
          flag = true;
      }
      string cardInDictionary = MatchManager.Instance.CreateCardInDictionary("friendlytadpole");
      MatchManager.Instance.GetCardData(cardInDictionary);
      MatchManager.Instance.GenerateNewCard(1, cardInDictionary, false, Enums.CardPlace.RandomDeck, heroIndex: heroIndex);
      teamHero[heroIndex].HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Mind Flayer"), Enums.CombatScrollEffectType.Trait);
      MatchManager.Instance.ItemTraitActivated();
    }
  }

  public void mojo()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null) || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Healing_Spell))
      return;
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    for (int index = 0; index < teamHero.Length; ++index)
    {
      if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
      {
        teamHero[index].HealCurses(1);
        if ((Object) teamHero[index].HeroItem != (Object) null)
        {
          teamHero[index].HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Mojo"), Enums.CombatScrollEffectType.Trait);
          EffectsManager.Instance.PlayEffectAC("dispel", true, teamHero[index].HeroItem.CharImageT, false);
        }
      }
    }
  }

  public void momentum()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (momentum));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (momentum)) && MatchManager.Instance.activatedTraits[nameof (momentum)] > traitData.TimesPerTurn - 1 || MatchManager.Instance.energyJustWastedByHero <= 0 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Melee_Attack) || !((Object) this.character.HeroData != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (momentum)))
      MatchManager.Instance.activatedTraits.Add(nameof (momentum), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (momentum)];
    MatchManager.Instance.SetTraitInfoText();
    this.character.SetAuraTrait(this.character, "powerful", 1);
    this.character.ModifyEnergy(1, true);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Momentum") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (momentum)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("powerful", true, this.character.HeroItem.CharImageT, false);
    EffectsManager.Instance.PlayEffectAC("energy", true, this.character.HeroItem.CharImageT, false);
  }

  public void nightstalker()
  {
    this.character.SetAuraTrait(this.character, "stealth", 2);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Nightstalker"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("stealth", true, this.character.HeroItem.CharImageT, false);
  }

  public void offensemastery()
  {
    if (!((Object) this.character.HeroData != (Object) null))
      return;
    int num = 1;
    if (num <= 0)
      return;
    List<string> heroHand = MatchManager.Instance.GetHeroHand(this.character.HeroIndex);
    List<CardData> cardDataList = new List<CardData>();
    for (int index = 0; index < heroHand.Count; ++index)
    {
      CardData cardData = MatchManager.Instance.GetCardData(heroHand[index]);
      if (cardData.GetCardFinalCost() > 0 && cardData.HasCardType(Enums.CardType.Attack))
        cardDataList.Add(cardData);
    }
    for (int index = 0; index < cardDataList.Count; ++index)
    {
      CardData cardData = cardDataList[index];
      cardData.EnergyReductionTemporal += num;
      MatchManager.Instance.UpdateHandCards();
      CardItem fromTableByIndex = MatchManager.Instance.GetCardFromTableByIndex(cardData.InternalId);
      fromTableByIndex.PlayDissolveParticle();
      fromTableByIndex.ShowEnergyModification(-num);
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Offense Mastery"), Enums.CombatScrollEffectType.Trait);
      MatchManager.Instance.CreateLogCardModification(cardData.InternalId, MatchManager.Instance.GetHero(this.character.HeroIndex));
    }
  }

  public void overflow()
  {
    int heal = Functions.FuncRoundToInt((float) this.auxInt * 0.35f);
    if (AtOManager.Instance.TeamHaveTrait("beaconoflight"))
      heal = Functions.FuncRoundToInt((float) this.auxInt * 0.7f);
    int _hp = this.target.HealReceivedFinal(heal);
    if (this.target.GetHpLeftForMax() < _hp)
      _hp = this.target.GetHpLeftForMax();
    if (_hp <= 0)
      return;
    this.target.ModifyHp(_hp);
    CastResolutionForCombatText _cast = new CastResolutionForCombatText();
    _cast.heal = _hp;
    if ((Object) this.target.HeroItem != (Object) null)
    {
      this.target.HeroItem.ScrollCombatTextDamageNew(_cast);
      EffectsManager.Instance.PlayEffectAC("healimpactsmall", true, this.target.HeroItem.CharImageT, false);
    }
    else
    {
      this.target.NPCItem.ScrollCombatTextDamageNew(_cast);
      EffectsManager.Instance.PlayEffectAC("healimpactsmall", true, this.target.NPCItem.CharImageT, false);
    }
    this.target.SetEvent(Enums.EventActivation.Healed);
    this.character.SetEvent(Enums.EventActivation.Heal, this.target);
    this.target.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Overflow"), Enums.CombatScrollEffectType.Trait);
  }

  public void pestilent()
  {
    this.character.SetAuraTrait(this.character, "dark", 1);
    if ((Object) this.character.HeroData != (Object) null)
    {
      NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();
      for (int index = 0; index < teamNpc.Length; ++index)
      {
        if (teamNpc[index] != null && teamNpc[index].Alive)
        {
          teamNpc[index].SetAuraTrait(this.character, "dark", 1);
          if ((Object) teamNpc[index].NPCItem != (Object) null)
          {
            teamNpc[index].NPCItem.ScrollCombatText(Texts.Instance.GetText("traits_Pestilent"), Enums.CombatScrollEffectType.Trait);
            EffectsManager.Instance.PlayEffectAC("shadowimpactsmall", true, teamNpc[index].NPCItem.CharImageT, false);
          }
        }
      }
    }
    else
    {
      Hero[] teamHero = MatchManager.Instance.GetTeamHero();
      for (int index = 0; index < teamHero.Length; ++index)
      {
        if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
        {
          teamHero[index].SetAuraTrait(this.character, "dark", 1);
          if ((Object) teamHero[index].HeroItem != (Object) null)
          {
            teamHero[index].HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Pestilent"), Enums.CombatScrollEffectType.Trait);
            EffectsManager.Instance.PlayEffectAC("shadowimpactsmall", true, teamHero[index].HeroItem.CharImageT, false);
          }
        }
      }
    }
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Pestilent"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("shadowimpactsmall", true, this.character.HeroItem.CharImageT, false);
  }

  public void premonition()
  {
    if ((Object) this.character.HeroData != (Object) null)
    {
      NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();
      for (int index = 0; index < teamNpc.Length; ++index)
      {
        if (teamNpc[index] != null && teamNpc[index].Alive)
          teamNpc[index].SetAuraTrait(this.character, "sight", 2);
      }
    }
    else
    {
      Hero[] teamHero = MatchManager.Instance.GetTeamHero();
      for (int index = 0; index < teamHero.Length; ++index)
      {
        if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
          teamHero[index].SetAuraTrait(this.character, "sight", 2);
      }
    }
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Premonition"), Enums.CombatScrollEffectType.Trait);
  }

  public void rangedmastery()
  {
    if (!((Object) this.character.HeroData != (Object) null))
      return;
    int num = 1;
    if (num <= 0)
      return;
    List<string> heroHand = MatchManager.Instance.GetHeroHand(this.character.HeroIndex);
    List<CardData> cardDataList = new List<CardData>();
    for (int index = 0; index < heroHand.Count; ++index)
    {
      CardData cardData = MatchManager.Instance.GetCardData(heroHand[index]);
      if (cardData.GetCardFinalCost() > 0 && cardData.HasCardType(Enums.CardType.Ranged_Attack))
        cardDataList.Add(cardData);
    }
    for (int index = 0; index < cardDataList.Count; ++index)
    {
      CardData cardData = cardDataList[index];
      cardData.EnergyReductionTemporal += num;
      MatchManager.Instance.UpdateHandCards();
      CardItem fromTableByIndex = MatchManager.Instance.GetCardFromTableByIndex(cardData.InternalId);
      fromTableByIndex.PlayDissolveParticle();
      fromTableByIndex.ShowEnergyModification(-num);
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Ranged Mastery"), Enums.CombatScrollEffectType.Trait);
      MatchManager.Instance.CreateLogCardModification(cardData.InternalId, MatchManager.Instance.GetHero(this.character.HeroIndex));
    }
  }

  public void resourceful()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (resourceful));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (resourceful)) && MatchManager.Instance.activatedTraits[nameof (resourceful)] > traitData.TimesPerTurn - 1 || MatchManager.Instance.energyJustWastedByHero <= 0 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Skill) || !((Object) this.character.HeroData != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (resourceful)))
      MatchManager.Instance.activatedTraits.Add(nameof (resourceful), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (resourceful)];
    MatchManager.Instance.SetTraitInfoText();
    this.character.ModifyEnergy(1, true);
    if ((Object) this.character.HeroItem != (Object) null)
    {
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Resourceful") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (resourceful)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
      EffectsManager.Instance.PlayEffectAC("energy", true, this.character.HeroItem.CharImageT, false);
    }
    NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();
    for (int index = 0; index < teamNpc.Length; ++index)
    {
      if (teamNpc[index] != null && teamNpc[index].Alive)
        teamNpc[index].SetAuraTrait(this.character, "sight", 1);
    }
  }

  public void reverberation()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (reverberation));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (reverberation)) && MatchManager.Instance.activatedTraits[nameof (reverberation)] > traitData.TimesPerTurn - 1 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Holy_Spell) || !((Object) this.character.HeroData != (Object) null))
      return;
    List<CardData> cardDataList = new List<CardData>();
    List<string> heroHand = MatchManager.Instance.GetHeroHand(this.character.HeroIndex);
    int num1 = 0;
    for (int index = 0; index < heroHand.Count; ++index)
    {
      CardData cardData = MatchManager.Instance.GetCardData(heroHand[index]);
      if ((Object) cardData != (Object) null && cardData.GetCardTypes().Contains(Enums.CardType.Holy_Spell) && this.character.GetCardFinalCost(cardData) > num1)
        num1 = this.character.GetCardFinalCost(cardData);
    }
    if (num1 <= 0)
      return;
    for (int index = 0; index < heroHand.Count; ++index)
    {
      CardData cardData = MatchManager.Instance.GetCardData(heroHand[index]);
      if ((Object) cardData != (Object) null && cardData.GetCardTypes().Contains(Enums.CardType.Holy_Spell) && this.character.GetCardFinalCost(cardData) >= num1)
        cardDataList.Add(cardData);
    }
    if (cardDataList.Count <= 0)
      return;
    CardData cardData1 = cardDataList.Count != 1 ? cardDataList[MatchManager.Instance.GetRandomIntRange(0, cardDataList.Count, "trait")] : cardDataList[0];
    if (!((Object) cardData1 != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (reverberation)))
      MatchManager.Instance.activatedTraits.Add(nameof (reverberation), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (reverberation)];
    MatchManager.Instance.SetTraitInfoText();
    int num2 = 3;
    cardData1.EnergyReductionTemporal += num2;
    MatchManager.Instance.GetCardFromTableByIndex(cardData1.InternalId).ShowEnergyModification(-num2);
    MatchManager.Instance.UpdateHandCards();
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Reverberation") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (reverberation)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
    MatchManager.Instance.CreateLogCardModification(cardData1.InternalId, MatchManager.Instance.GetHero(this.character.HeroIndex));
  }

  public void scholar()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (scholar));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (scholar)) && MatchManager.Instance.activatedTraits[nameof (scholar)] > traitData.TimesPerTurn - 1 || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Book) || MatchManager.Instance.CountHeroHand() == 0 || !((Object) this.character.HeroData != (Object) null))
      return;
    List<CardData> cardDataList = new List<CardData>();
    List<string> heroHand = MatchManager.Instance.GetHeroHand(this.character.HeroIndex);
    int num1 = 0;
    for (int index = 0; index < heroHand.Count; ++index)
    {
      CardData cardData = MatchManager.Instance.GetCardData(heroHand[index]);
      if ((Object) cardData != (Object) null && this.character.GetCardFinalCost(cardData) > num1)
        num1 = this.character.GetCardFinalCost(cardData);
    }
    if (num1 <= 0)
      return;
    for (int index = 0; index < heroHand.Count; ++index)
    {
      CardData cardData = MatchManager.Instance.GetCardData(heroHand[index]);
      if ((Object) cardData != (Object) null && this.character.GetCardFinalCost(cardData) >= num1)
        cardDataList.Add(cardData);
    }
    if (cardDataList.Count <= 0)
      return;
    CardData cardData1 = cardDataList.Count != 1 ? cardDataList[MatchManager.Instance.GetRandomIntRange(0, cardDataList.Count, "trait")] : cardDataList[0];
    if (!((Object) cardData1 != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (scholar)))
      MatchManager.Instance.activatedTraits.Add(nameof (scholar), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (scholar)];
    MatchManager.Instance.SetTraitInfoText();
    int num2 = 1;
    cardData1.EnergyReductionPermanent += num2;
    MatchManager.Instance.GetCardFromTableByIndex(cardData1.InternalId).ShowEnergyModification(-num2);
    MatchManager.Instance.UpdateHandCards();
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Scholar") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (scholar)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
    MatchManager.Instance.CreateLogCardModification(cardData1.InternalId, MatchManager.Instance.GetHero(this.character.HeroIndex));
  }

  public void shielder()
  {
    if (!(this.auxString == "shield") || this.target == null || !this.target.IsHero)
      return;
    int num = this.target.HealReceivedFinal(Functions.FuncRoundToInt((float) this.auxInt * 0.3f));
    int _hp = num;
    if (this.target.GetHpLeftForMax() < num)
      _hp = this.target.GetHpLeftForMax();
    if (_hp <= 0)
      return;
    this.target.ModifyHp(_hp);
    CastResolutionForCombatText _cast = new CastResolutionForCombatText();
    _cast.heal = _hp;
    if ((Object) this.target.HeroItem != (Object) null)
    {
      this.target.HeroItem.ScrollCombatTextDamageNew(_cast);
      EffectsManager.Instance.PlayEffectAC("healimpactsmall", true, this.target.HeroItem.CharImageT, false);
    }
    else
    {
      this.target.NPCItem.ScrollCombatTextDamageNew(_cast);
      EffectsManager.Instance.PlayEffectAC("healimpactsmall", true, this.target.NPCItem.CharImageT, false);
    }
    this.target.SetEvent(Enums.EventActivation.Healed);
    this.character.SetEvent(Enums.EventActivation.Heal, this.target);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Shielder"), Enums.CombatScrollEffectType.Trait);
  }

  public void shieldexpert()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null) || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Attack))
      return;
    this.character.SetAuraTrait(this.character, "block", 9);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Shield Expert"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("guardsmall", true, this.character.HeroItem.CharImageT, false);
  }

  public void spiky()
  {
    this.character.SetAuraTrait(this.character, "thorns", 5);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Spiky"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("thorns", true, this.character.HeroItem.CharImageT, false);
  }

  public void tactician()
  {
    if (!((Object) this.character.HeroData != (Object) null))
      return;
    int num = 1;
    if (num <= 0)
      return;
    List<string> heroHand = MatchManager.Instance.GetHeroHand(this.character.HeroIndex);
    List<CardData> cardDataList = new List<CardData>();
    for (int index = 0; index < heroHand.Count; ++index)
    {
      CardData cardData = MatchManager.Instance.GetCardData(heroHand[index]);
      if (cardData.GetCardFinalCost() > 0 && cardData.HasCardType(Enums.CardType.Skill))
        cardDataList.Add(cardData);
    }
    for (int index = 0; index < cardDataList.Count; ++index)
    {
      CardData cardData = cardDataList[index];
      cardData.EnergyReductionTemporal += num;
      MatchManager.Instance.UpdateHandCards();
      CardItem fromTableByIndex = MatchManager.Instance.GetCardFromTableByIndex(cardData.InternalId);
      fromTableByIndex.PlayDissolveParticle();
      fromTableByIndex.ShowEnergyModification(-num);
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Tactician"), Enums.CombatScrollEffectType.Trait);
      MatchManager.Instance.CreateLogCardModification(cardData.InternalId, MatchManager.Instance.GetHero(this.character.HeroIndex));
    }
  }

  public void temperate()
  {
    this.character.SetAuraTrait(this.character, "Insulate", 1);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Temperate"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("insulate", true, this.character.HeroItem.CharImageT, false);
  }

  public void trailblazer()
  {
    this.character.SetAuraTrait(this.character, "Fast", 1);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Trailblazer"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("speed1", true, this.character.HeroItem.CharImageT, false);
  }

  public void trickster()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null) || !this.castedCard.GetCardTypes().Contains(Enums.CardType.Song))
      return;
    this.character.SetAuraTrait(this.character, "sharp", 2);
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Trickster"), Enums.CombatScrollEffectType.Trait);
  }

  public void unbreakable()
  {
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    for (int index = 0; index < teamHero.Length; ++index)
    {
      if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
      {
        teamHero[index].SetAuraTrait(this.character, "block", 12);
        teamHero[index].SetAuraTrait(this.character, "fortify", 1);
        if ((Object) teamHero[index].HeroItem != (Object) null)
          EffectsManager.Instance.PlayEffectAC("intercept", true, teamHero[index].HeroItem.CharImageT, false);
      }
    }
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Unbreakable"), Enums.CombatScrollEffectType.Trait);
  }

  public void vampirism()
  {
    if (this.character == null || this.character.GetHp() <= 0)
      return;
    int heal = Functions.FuncRoundToInt((float) this.auxInt * 0.3f);
    Enums.CardClass CC = Enums.CardClass.None;
    if ((Object) this.castedCard != (Object) null)
      CC = this.castedCard.CardClass;
    int _hp = this.character.HealReceivedFinal(this.character.HealWithCharacterBonus(heal, CC));
    this.character.ModifyHp(_hp);
    CastResolutionForCombatText _cast = new CastResolutionForCombatText();
    _cast.heal = _hp;
    if ((Object) this.character.HeroItem != (Object) null)
    {
      this.character.HeroItem.ScrollCombatTextDamageNew(_cast);
    }
    else
    {
      if (!((Object) this.character.NPCItem != (Object) null))
        return;
      this.character.NPCItem.ScrollCombatTextDamageNew(_cast);
    }
  }

  public void veilofshadows()
  {
    if (!((Object) MatchManager.Instance != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (veilofshadows));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (veilofshadows)) && MatchManager.Instance.activatedTraits[nameof (veilofshadows)] > traitData.TimesPerTurn - 1 || !((Object) this.character.HeroData != (Object) null))
      return;
    if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (veilofshadows)))
      MatchManager.Instance.activatedTraits.Add(nameof (veilofshadows), 1);
    else
      ++MatchManager.Instance.activatedTraits[nameof (veilofshadows)];
    MatchManager.Instance.SetTraitInfoText();
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Veilofshadows") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (veilofshadows)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
  }

  public void versatile()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    bool flag = false;
    string internalId = this.castedCard.InternalId;
    if (this.castedCard.GetCardTypes().Contains(Enums.CardType.Fire_Spell))
    {
      flag = true;
      this.character.SetAuraTrait(this.character, "powerful", 2);
    }
    if (this.castedCard.GetCardTypes().Contains(Enums.CardType.Cold_Spell))
    {
      flag = true;
      this.character.SetAuraTrait(this.character, "block", 6);
    }
    if (this.castedCard.GetCardTypes().Contains(Enums.CardType.Lightning_Spell))
    {
      flag = true;
      this.character.HealCurses(1);
    }
    if (!flag)
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Versatile"), Enums.CombatScrollEffectType.Trait);
  }

  public void voodoo()
  {
    if (!(this.auxString == "dark"))
      return;
    Hero lowestHealthHero = this.GetLowestHealthHero();
    if (lowestHealthHero == null || !lowestHealthHero.Alive)
      return;
    int num = lowestHealthHero.HealReceivedFinal(this.auxInt);
    int _hp = num;
    if (lowestHealthHero.GetHpLeftForMax() < num)
      _hp = lowestHealthHero.GetHpLeftForMax();
    if (_hp <= 0)
      return;
    lowestHealthHero.ModifyHp(_hp, false);
    AtOManager.Instance.combatStats[this.character.HeroIndex, 3] += _hp;
    AtOManager.Instance.combatStatsCurrent[this.character.HeroIndex, 3] += _hp;
    lowestHealthHero.HeroItem.ScrollCombatTextDamageNew(new CastResolutionForCombatText()
    {
      heal = _hp
    });
    lowestHealthHero.SetEvent(Enums.EventActivation.Healed);
    this.character.SetEvent(Enums.EventActivation.Heal, (Character) lowestHealthHero);
    if (!((Object) lowestHealthHero.HeroItem != (Object) null))
      return;
    EffectsManager.Instance.PlayEffectAC("healimpactsmall", true, lowestHealthHero.HeroItem.CharImageT, false);
  }

  public void warriorduality()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (warriorduality));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (warriorduality)) && MatchManager.Instance.activatedTraits[nameof (warriorduality)] > traitData.TimesPerTurn - 1)
      return;
    for (int index1 = 0; index1 < 2; ++index1)
    {
      Enums.CardClass cardClass1;
      Enums.CardClass cardClass2;
      if (index1 == 0)
      {
        cardClass1 = Enums.CardClass.Warrior;
        cardClass2 = Enums.CardClass.Healer;
      }
      else
      {
        cardClass1 = Enums.CardClass.Healer;
        cardClass2 = Enums.CardClass.Warrior;
      }
      if (this.castedCard.CardClass == cardClass1)
      {
        if (MatchManager.Instance.CountHeroHand() == 0 || !((Object) this.character.HeroData != (Object) null))
          break;
        List<CardData> cardDataList = new List<CardData>();
        List<string> heroHand = MatchManager.Instance.GetHeroHand(this.character.HeroIndex);
        int num1 = 0;
        for (int index2 = 0; index2 < heroHand.Count; ++index2)
        {
          CardData cardData = MatchManager.Instance.GetCardData(heroHand[index2]);
          if ((Object) cardData != (Object) null && cardData.CardClass == cardClass2 && this.character.GetCardFinalCost(cardData) > num1)
            num1 = this.character.GetCardFinalCost(cardData);
        }
        if (num1 <= 0)
          break;
        for (int index3 = 0; index3 < heroHand.Count; ++index3)
        {
          CardData cardData = MatchManager.Instance.GetCardData(heroHand[index3]);
          if ((Object) cardData != (Object) null && cardData.CardClass == cardClass2 && this.character.GetCardFinalCost(cardData) >= num1)
            cardDataList.Add(cardData);
        }
        if (cardDataList.Count <= 0)
          break;
        CardData cardData1 = cardDataList.Count != 1 ? cardDataList[MatchManager.Instance.GetRandomIntRange(0, cardDataList.Count, "trait")] : cardDataList[0];
        if (!((Object) cardData1 != (Object) null))
          break;
        if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (warriorduality)))
          MatchManager.Instance.activatedTraits.Add(nameof (warriorduality), 1);
        else
          ++MatchManager.Instance.activatedTraits[nameof (warriorduality)];
        MatchManager.Instance.SetTraitInfoText();
        int num2 = 1;
        cardData1.EnergyReductionTemporal += num2;
        MatchManager.Instance.GetCardFromTableByIndex(cardData1.InternalId).ShowEnergyModification(-num2);
        MatchManager.Instance.UpdateHandCards();
        this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_WarriorDuality") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (warriorduality)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
        MatchManager.Instance.CreateLogCardModification(cardData1.InternalId, MatchManager.Instance.GetHero(this.character.HeroIndex));
        break;
      }
    }
  }

  public void weaponexpert()
  {
  }

  public void welltrained()
  {
    this.character.SetAuraTrait(this.character, "reinforce", 2);
    if (!((Object) this.character.HeroItem != (Object) null))
      return;
    this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Well Trained"), Enums.CombatScrollEffectType.Trait);
    EffectsManager.Instance.PlayEffectAC("reinforce", true, this.character.HeroItem.CharImageT, false);
  }

  public void widesleeves()
  {
    if (!((Object) MatchManager.Instance != (Object) null) || !((Object) this.castedCard != (Object) null))
      return;
    TraitData traitData = Globals.Instance.GetTraitData(nameof (widesleeves));
    if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(nameof (widesleeves)) && MatchManager.Instance.activatedTraits[nameof (widesleeves)] > traitData.TimesPerTurn - 1)
      return;
    if (MatchManager.Instance.CountHeroHand() == 10)
    {
      Debug.Log((object) "[TRAIT EXECUTION] Broke because player at max cards");
    }
    else
    {
      if (!this.castedCard.GetCardTypes().Contains(Enums.CardType.Small_Weapon) || !((Object) this.character.HeroData != (Object) null))
        return;
      if (!MatchManager.Instance.activatedTraits.ContainsKey(nameof (widesleeves)))
        MatchManager.Instance.activatedTraits.Add(nameof (widesleeves), 1);
      else
        ++MatchManager.Instance.activatedTraits[nameof (widesleeves)];
      MatchManager.Instance.SetTraitInfoText();
      int randomIntRange1 = MatchManager.Instance.GetRandomIntRange(0, 100, "trait");
      int randomIntRange2 = MatchManager.Instance.GetRandomIntRange(0, Globals.Instance.CardListByType[Enums.CardType.Small_Weapon].Count, "trait");
      string str;
      CardData cardData1 = Globals.Instance.GetCardData(str = Globals.Instance.CardListByType[Enums.CardType.Small_Weapon][randomIntRange2], false);
      string cardInDictionary = MatchManager.Instance.CreateCardInDictionary(Functions.GetCardByRarity(randomIntRange1, cardData1));
      CardData cardData2 = MatchManager.Instance.GetCardData(cardInDictionary);
      cardData2.Vanish = true;
      cardData2.EnergyReductionToZeroPermanent = true;
      MatchManager.Instance.GenerateNewCard(1, cardInDictionary, false, Enums.CardPlace.Hand);
      this.character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_Wide Sleeves") + this.TextChargesLeft(MatchManager.Instance.activatedTraits[nameof (widesleeves)], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
      MatchManager.Instance.ItemTraitActivated();
      MatchManager.Instance.CreateLogCardModification(cardData2.InternalId, MatchManager.Instance.GetHero(this.character.HeroIndex));
    }
  }

  private Hero GetLowestHealthHero()
  {
    int num1 = -1;
    float num2 = 99.9999f;
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    for (int index = 0; index < teamHero.Length; ++index)
    {
      if (teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive)
      {
        float hpPercent = teamHero[index].GetHpPercent();
        if ((double) hpPercent <= (double) num2)
        {
          num2 = hpPercent;
          num1 = index;
        }
      }
    }
    if (num1 > -1)
    {
      List<int> intList = new List<int>();
      intList.Add(num1);
      for (int index = 0; index < teamHero.Length; ++index)
      {
        if (index != num1 && teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null && teamHero[index].Alive && (double) teamHero[index].GetHpPercent() == (double) num2)
          intList.Add(index);
      }
      if (intList.Count > 0)
      {
        int num3 = 0;
        int auxInt = this.auxInt;
        for (; num3 < 10; ++num3)
        {
          int index = intList.Count <= 1 ? intList[0] : intList[MatchManager.Instance.GetRandomIntRange(0, intList.Count, "trait")];
          if (num3 == 9)
            index = intList[0];
          if (index < teamHero.Length && teamHero[index] != null && (Object) teamHero[index].HeroData != (Object) null)
            return teamHero[index];
        }
      }
    }
    return (Hero) null;
  }
}
