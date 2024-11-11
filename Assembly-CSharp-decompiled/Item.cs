// Decompiled with JetBrains decompiler
// Type: Item
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;

public class Item
{
  public bool DoItem(
    Enums.EventActivation _theEvent,
    CardData _cardData,
    string _item,
    Character _character,
    Character _target,
    int _auxInt,
    string _auxString,
    int order,
    CardData castedCard,
    bool onlyCheckItemActivation = false)
  {
    if ((UnityEngine.Object) MatchManager.Instance == (UnityEngine.Object) null)
      return false;
    switch (_item)
    {
      case "surprisebox":
        if (!onlyCheckItemActivation)
          this.surprisebox(_character, false, _cardData.CardName);
        return true;
      case "surpriseboxrare":
        if (!onlyCheckItemActivation)
          this.surprisebox(_character, true, _cardData.CardName);
        return true;
      case "surprisegiftbox":
        if (!onlyCheckItemActivation)
          this.surprisegiftbox(_character, false, _cardData.CardName);
        return true;
      case "surprisegiftboxrare":
        if (!onlyCheckItemActivation)
          this.surprisegiftbox(_character, true, _cardData.CardName);
        return true;
      default:
        Character character = _character;
        Character target = _target;
        Enums.EventActivation theEvent = _theEvent;
        int auxInt = _auxInt;
        CardData cardItem = _cardData;
        ItemData itemData = !((UnityEngine.Object) _cardData.Item != (UnityEngine.Object) null) ? _cardData.ItemEnchantment : _cardData.Item;
        string cardName = _cardData.CardName;
        string lower = Enum.GetName(typeof (Enums.CardType), (object) _cardData.CardType).ToLower();
        if ((_theEvent == Enums.EventActivation.PreFinishCast || _theEvent == Enums.EventActivation.FinishCast || _theEvent == Enums.EventActivation.FinishFinishCast) && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && MatchManager.Instance.IsBeginTournPhase)
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD(cardName + " Broke because we have not began the turn phase", "item");
          return false;
        }
        if ((_theEvent == Enums.EventActivation.PreFinishCast || _theEvent == Enums.EventActivation.FinishCast || _theEvent == Enums.EventActivation.FinishFinishCast) && (UnityEngine.Object) castedCard != (UnityEngine.Object) null && (castedCard.AutoplayDraw || castedCard.AutoplayEndTurn) && (castedCard.CardClass == Enums.CardClass.Injury || castedCard.CardClass == Enums.CardClass.Boon || castedCard.CardClass == Enums.CardClass.Monster))
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD(cardName + " Broke because just casted an autoplay boon/injury/monster card " + castedCard.Id, "item");
          return false;
        }
        if ((_theEvent == Enums.EventActivation.PreFinishCast || _theEvent == Enums.EventActivation.FinishCast || _theEvent == Enums.EventActivation.FinishFinishCast) && (UnityEngine.Object) castedCard != (UnityEngine.Object) null && (UnityEngine.Object) castedCard.ItemEnchantment != (UnityEngine.Object) null && !(itemData.Id == "endlessbag") && !(itemData.Id == "endlessbagrare") && !(itemData.Id == "mirrorofkalandra") && !(itemData.Id == "mirrorofkalandrarare") && !(itemData.Id == "manaloop") && !(itemData.Id == "manalooprare"))
        {
          if (!castedCard.ItemEnchantment.CastEnchantmentOnFinishSelfCast && castedCard.ItemEnchantment.Id == _cardData.Id)
          {
            if (Globals.Instance.ShowDebug)
              Functions.DebugLogGD(cardName + " Broke because Item Enchantment just casted", "item");
            return false;
          }
          if (castedCard.ItemEnchantment.CastEnchantmentOnFinishSelfCast && castedCard.ItemEnchantment.Id != _cardData.Id)
          {
            if (Globals.Instance.ShowDebug)
              Functions.DebugLogGD(cardName + " Broke because this is not the Item Enchantment card", "item");
            return false;
          }
        }
        if (_theEvent == Enums.EventActivation.DamagedSecondary)
        {
          if ((double) itemData.LowerOrEqualPercentHP <= 0.0 || (double) itemData.LowerOrEqualPercentHP >= 100.0)
            return false;
          theEvent = Enums.EventActivation.Damaged;
        }
        if (itemData.UsedEnergy && MatchManager.Instance.energyJustWastedByHero < 1)
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD(cardName + " Broke because not used energy", "item");
          return false;
        }
        if (itemData.ExactRound > 0 && MatchManager.Instance.GetCurrentRound() != itemData.ExactRound)
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD(cardName + " Broke because Not exact round", "item");
          return false;
        }
        if (itemData.RoundCycle > 0 && MatchManager.Instance.GetCurrentRound() % itemData.RoundCycle != 0)
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD(cardName + " Broke because Not round cycle", "item");
          return false;
        }
        if ((UnityEngine.Object) itemData.AuraCurseSetted != (UnityEngine.Object) null && Globals.Instance.GetAuraCurseData(_auxString).Id != itemData.AuraCurseSetted.Id)
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD(cardName + " Broke because Not aura setted", "item");
          return false;
        }
        if (itemData.CastedCardType != Enums.CardType.None)
        {
          if ((UnityEngine.Object) castedCard != (UnityEngine.Object) null)
          {
            if (!castedCard.GetCardTypes().Contains(itemData.CastedCardType))
            {
              if (Globals.Instance.ShowDebug)
                Functions.DebugLogGD(cardName + " Broke because Not casted card type", "item");
              return false;
            }
          }
          else
          {
            if (Globals.Instance.ShowDebug)
              Functions.DebugLogGD(cardName + " Broke because  Not casted card", "item");
            return false;
          }
        }
        int cardsWaitingForReset = MatchManager.Instance.CardsWaitingForReset;
        int num = MatchManager.Instance.CountHeroHand(character.HeroIndex);
        if (itemData.EmptyHand)
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD("isBeginTournPhase -> " + MatchManager.Instance.IsBeginTournPhase.ToString(), "item");
          if (MatchManager.Instance.IsBeginTournPhase)
          {
            if (Globals.Instance.ShowDebug)
              Functions.DebugLogGD(cardName + " Broke because is begin tourn phase", "item");
            return false;
          }
          bool flag = true;
          if (num > 0)
            flag = false;
          if (cardsWaitingForReset > 0)
            flag = false;
          if (num == 0 && cardsWaitingForReset == 1 && (UnityEngine.Object) castedCard != (UnityEngine.Object) null && castedCard.CardClass == Enums.CardClass.Injury)
            flag = true;
          if (!flag)
          {
            if (Globals.Instance.ShowDebug)
              Functions.DebugLogGD(cardName + " Broke because Not empty hand valid item");
            return false;
          }
        }
        if (num == 10 && (itemData.CardNum > 0 && itemData.CardPlace == Enums.CardPlace.Hand || itemData.DrawCards > 0))
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD(cardName + " Broke because player at max cards", "item");
          return false;
        }
        if (itemData.TimesPerTurn > 0 && !MatchManager.Instance.CanExecuteItemInThisTurn(character.Id, itemData.Id, itemData.TimesPerTurn))
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD(cardName + " Broke because TimesPerTurn", "item");
          return false;
        }
        if (itemData.TimesPerCombat > 0 && !MatchManager.Instance.CanExecuteItemInThisCombat(character.Id, itemData.Id, itemData.TimesPerCombat))
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD(cardName + " Broke because TimesPerCombat", "item");
          return false;
        }
        if (itemData.Activation == Enums.EventActivation.Damaged && _character == _target)
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD(cardName + " Broke because Caster equal to Target", "item");
          return false;
        }
        if (itemData.Activation == Enums.EventActivation.Damaged && (double) character.GetHpPercent() > (double) itemData.LowerOrEqualPercentHP)
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD(cardName + " Broke because LowerOrEqualPercentHP", "item");
          return false;
        }
        if (onlyCheckItemActivation)
          return true;
        if (itemData.TimesPerTurn > 0)
          MatchManager.Instance.ItemExecuteForThisTurn(character.Id, itemData.Id, itemData.TimesPerTurn, lower);
        if (itemData.TimesPerCombat > 0)
        {
          MatchManager.Instance.ItemExecuteForThisCombat(character.Id, itemData.Id, itemData.TimesPerCombat, lower);
          this.ShowCombatText(lower, cardName, character, MatchManager.Instance.ItemExecutedInThisCombat(character.Id, itemData.Id), itemData.TimesPerCombat);
        }
        MatchManager.Instance.itemTimeout[order] = 0.0f;
        string castedCardId = "";
        if ((UnityEngine.Object) castedCard != (UnityEngine.Object) null)
          castedCardId = castedCard.Id;
        this.DoItemData(target, cardName, auxInt, cardItem, lower, itemData, character, order, castedCardId, theEvent);
        return true;
    }
  }

  private string TextChargesLeft(int currentCharges, int chargesTotal)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<br><color=#FFF>");
    stringBuilder.Append(currentCharges.ToString());
    stringBuilder.Append("/");
    stringBuilder.Append((chargesTotal + 1).ToString());
    return stringBuilder.ToString();
  }

  private void DoItemData(
    Character target,
    string itemName,
    int auxInt,
    CardData cardItem,
    string itemType,
    ItemData itemData,
    Character character,
    int order,
    string castedCardId = "",
    Enums.EventActivation theEvent = Enums.EventActivation.None)
  {
    int charges1 = -1;
    int chargesTotal = -1;
    bool flag1 = false;
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD(itemName + " DoItemData", "item");
    if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
    {
      if (itemData.DestroyAfterUses > 0 && itemData.Activation != Enums.EventActivation.Killed)
      {
        chargesTotal = itemData.DestroyAfterUses - 1;
        charges1 = MatchManager.Instance.EnchantmentExecutedTimes(character.Id, itemData.Id);
        if (charges1 >= chargesTotal)
        {
          if (character.IsHero)
            AtOManager.Instance.RemoveItemFromHero(true, character.HeroIndex, "", itemData.Id);
          else
            AtOManager.Instance.RemoveItemFromHero(false, character.NPCIndex, "", itemData.Id);
          MatchManager.Instance.RedrawCardsDescriptionPrecalculated();
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD("Destroyed because DestroyAfterUses", "item");
          if (charges1 > chargesTotal)
            return;
        }
        else
        {
          MatchManager.Instance.EnchantmentExecute(character.Id, itemData.Id);
          character.EnchantmentExecute(itemData.Id);
          ++charges1;
        }
      }
      if (!itemData.IsEnchantment && itemData.DrawCards > 0)
      {
        int drawCards = itemData.DrawCards;
        if (itemData.DrawMultiplyByEnergyUsed)
          drawCards *= MatchManager.Instance.energyJustWastedByHero;
        if (drawCards > 0)
        {
          MatchManager.Instance.itemTimeout[order] = !GameManager.Instance.IsMultiplayer() ? (GameManager.Instance.configGameSpeed != Enums.ConfigSpeed.Slow ? 0.4f : 0.5f) : 0.5f;
          if (MatchManager.Instance.CountHeroDeck() == 0)
            MatchManager.Instance.itemTimeout[order] += 0.7f;
          MatchManager.Instance.NewCard(drawCards, Enums.CardFrom.Deck);
        }
      }
    }
    List<Character> characterList = new List<Character>();
    if (itemData.ItemTarget == Enums.ItemTarget.Self || itemData.ItemTarget == Enums.ItemTarget.SelfEnemy)
      characterList.Add(character);
    else if (itemData.ItemTarget == Enums.ItemTarget.RandomHero)
      characterList.Add((Character) this.GetRandomHero());
    else if (itemData.ItemTarget == Enums.ItemTarget.RandomEnemy)
      characterList.Add((Character) this.GetRandomNPC());
    else if (itemData.ItemTarget == Enums.ItemTarget.Random)
      characterList.Add(this.GetRandomCharacter());
    else if (itemData.ItemTarget == Enums.ItemTarget.AllHero)
      characterList = this.GetAllHeroList();
    else if (itemData.ItemTarget == Enums.ItemTarget.AllEnemy)
      characterList = this.GetAllNPCList();
    else if (itemData.ItemTarget == Enums.ItemTarget.CurrentTarget)
      characterList.Add(target);
    else if (itemData.ItemTarget == Enums.ItemTarget.HighestFlatHpHero)
      characterList.Add(this.GetFlatHPCharacter(true, true));
    else if (itemData.ItemTarget == Enums.ItemTarget.HighestFlatHpEnemy)
      characterList.Add(this.GetFlatHPCharacter(true, false));
    else if (itemData.ItemTarget == Enums.ItemTarget.LowestFlatHpHero)
      characterList.Add(this.GetFlatHPCharacter(false, true));
    else if (itemData.ItemTarget == Enums.ItemTarget.LowestFlatHpEnemy)
      characterList.Add(this.GetFlatHPCharacter(false, false));
    if (characterList.Count == 0)
    {
      if (!Globals.Instance.ShowDebug)
        return;
      Functions.DebugLogGD(itemName + " Break item execution: No target for " + itemData.ItemTarget.ToString(), "item");
    }
    else
    {
      if (itemData.DamageToTarget > 0)
      {
        int num1 = !character.IsHero || cardItem.CardClass != Enums.CardClass.Monster ? character.DamageWithCharacterBonus(itemData.DamageToTarget, itemData.DamageToTargetType, Enums.CardClass.Item) : itemData.DamageToTarget;
        if (itemData.DttMultiplyByEnergyUsed)
          num1 *= MatchManager.Instance.energyJustWastedByHero;
        if (num1 > -1)
        {
          for (int index = 0; index < characterList.Count; ++index)
          {
            Character character1 = characterList[index];
            int num2 = num1;
            if (character1 != null)
            {
              int num3 = character1.IncreasedCursedDamagePerStack(itemData.DamageToTargetType);
              int damage = num2 + num3;
              character1.IndirectDamage(itemData.DamageToTargetType, damage);
            }
          }
        }
        flag1 = true;
      }
      if (itemData.AuracurseBonusValue1 != 0)
      {
        int cost = itemData.AuraCurseNumForOneEvent <= 0 ? itemData.AuracurseBonusValue1 : Functions.FuncRoundToInt((float) (auxInt / itemData.AuraCurseNumForOneEvent)) * itemData.AuracurseBonusValue1;
        character.ModifyAuraCurseQuantity(itemData.AuracurseBonus1.Id, cost);
      }
      if (itemData.AuracurseBonusValue2 != 0)
      {
        int cost = itemData.AuraCurseNumForOneEvent <= 0 ? itemData.AuracurseBonusValue2 : Functions.FuncRoundToInt((float) (auxInt / itemData.AuraCurseNumForOneEvent)) * itemData.AuracurseBonusValue2;
        character.ModifyAuraCurseQuantity(itemData.AuracurseBonus2.Id, cost);
      }
      bool flag2 = false;
      bool flag3 = false;
      for (int index = 0; index < characterList.Count; ++index)
      {
        Character character2 = characterList[index];
        if (character2 != null)
        {
          if ((UnityEngine.Object) itemData.AuracurseGain1 != (UnityEngine.Object) null)
          {
            int charges2 = itemData.AuraCurseNumForOneEvent <= 0 ? itemData.AuracurseGainValue1 : Functions.FuncRoundToInt((float) (auxInt / itemData.AuraCurseNumForOneEvent)) * itemData.AuracurseGainValue1;
            if (itemData.Acg1MultiplyByEnergyUsed)
              charges2 *= MatchManager.Instance.energyJustWastedByHero;
            if (itemType == "corruption")
              character2.SetAuraTrait((Character) null, itemData.AuracurseGain1.Id, charges2);
            else if (character.IsHero && (cardItem.CardClass == Enums.CardClass.Monster || cardItem.CardClass == Enums.CardClass.Injury || cardItem.CardClass == Enums.CardClass.Boon))
              character2.SetAuraTrait((Character) null, itemData.AuracurseGain1.Id, charges2);
            else
              character2.SetAuraTrait(character, itemData.AuracurseGain1.Id, charges2);
            flag2 = true;
            flag1 = true;
          }
          if ((UnityEngine.Object) itemData.AuracurseGain2 != (UnityEngine.Object) null)
          {
            int charges3 = itemData.AuraCurseNumForOneEvent <= 0 ? itemData.AuracurseGainValue2 : Functions.FuncRoundToInt((float) (auxInt / itemData.AuraCurseNumForOneEvent)) * itemData.AuracurseGainValue2;
            if (itemData.Acg2MultiplyByEnergyUsed)
              charges3 *= MatchManager.Instance.energyJustWastedByHero;
            if (itemType == "corruption")
              character2.SetAuraTrait((Character) null, itemData.AuracurseGain2.Id, charges3);
            else if (character.IsHero && (cardItem.CardClass == Enums.CardClass.Monster || cardItem.CardClass == Enums.CardClass.Injury || cardItem.CardClass == Enums.CardClass.Boon))
              character2.SetAuraTrait((Character) null, itemData.AuracurseGain2.Id, charges3);
            else
              character2.SetAuraTrait(character, itemData.AuracurseGain2.Id, charges3);
            flag2 = true;
            flag1 = true;
          }
          if ((UnityEngine.Object) itemData.AuracurseGain3 != (UnityEngine.Object) null)
          {
            int charges4 = itemData.AuraCurseNumForOneEvent <= 0 ? itemData.AuracurseGainValue3 : Functions.FuncRoundToInt((float) (auxInt / itemData.AuraCurseNumForOneEvent)) * itemData.AuracurseGainValue3;
            if (itemData.Acg3MultiplyByEnergyUsed)
              charges4 *= MatchManager.Instance.energyJustWastedByHero;
            if (itemType == "corruption")
              character2.SetAuraTrait((Character) null, itemData.AuracurseGain3.Id, charges4);
            else if (character.IsHero && (cardItem.CardClass == Enums.CardClass.Monster || cardItem.CardClass == Enums.CardClass.Injury || cardItem.CardClass == Enums.CardClass.Boon))
              character2.SetAuraTrait((Character) null, itemData.AuracurseGain3.Id, charges4);
            else
              character2.SetAuraTrait(character, itemData.AuracurseGain3.Id, charges4);
            flag2 = true;
            flag1 = true;
          }
          if (itemData.HealQuantity > 0)
          {
            character2.ModifyHp(itemData.HealQuantity);
            CastResolutionForCombatText _cast = new CastResolutionForCombatText();
            _cast.heal = itemData.HealQuantity;
            if ((UnityEngine.Object) character2.HeroItem != (UnityEngine.Object) null)
              character2.HeroItem.ScrollCombatTextDamageNew(_cast);
            else
              character2.NPCItem.ScrollCombatTextDamageNew(_cast);
            flag1 = true;
          }
          if (itemData.HealPercentQuantity != 0)
          {
            if (itemData.Activation == Enums.EventActivation.Killed)
            {
              if (itemData.HealPercentQuantity > 0)
              {
                if (character2.GetHp() > 0)
                  return;
                character2.Resurrect((float) itemData.HealPercentQuantity);
                flag2 = true;
                MatchManager.Instance.itemTimeout[order] = 0.5f;
              }
            }
            else if (character2.GetHp() > 0 && itemData.HealPercentQuantity != 0)
            {
              character2.PercentHeal((float) itemData.HealPercentQuantity, true);
              flag2 = true;
            }
            flag1 = true;
          }
          if (itemData.HealPercentQuantitySelf != 0)
          {
            if (itemData.Activation == Enums.EventActivation.Killed)
            {
              if (itemData.HealPercentQuantitySelf > 0)
              {
                if (character.GetHp() > 0)
                  return;
                character.Resurrect((float) itemData.HealPercentQuantitySelf);
                flag2 = true;
                MatchManager.Instance.itemTimeout[order] = 0.5f;
              }
            }
            else if (character.GetHp() > 0 && itemData.HealPercentQuantitySelf != 0 && !flag3)
            {
              flag3 = true;
              character.PercentHeal((float) itemData.HealPercentQuantitySelf, true);
              flag2 = true;
            }
            flag1 = true;
          }
          if (itemData.EnergyQuantity > 0)
          {
            character2.ModifyEnergy(itemData.EnergyQuantity, true);
            flag1 = true;
          }
        }
      }
      if ((UnityEngine.Object) itemData.AuracurseGainSelf1 != (UnityEngine.Object) null)
      {
        if (itemType == "corruption")
          character.SetAuraTrait((Character) null, itemData.AuracurseGainSelf1.Id, itemData.AuracurseGainSelfValue1);
        else if (character.IsHero && (cardItem.CardClass == Enums.CardClass.Monster || cardItem.CardClass == Enums.CardClass.Injury || cardItem.CardClass == Enums.CardClass.Boon))
          character.SetAuraTrait((Character) null, itemData.AuracurseGainSelf1.Id, itemData.AuracurseGainSelfValue1);
        else
          character.SetAuraTrait(character, itemData.AuracurseGainSelf1.Id, itemData.AuracurseGainSelfValue1);
        flag2 = true;
        flag1 = true;
      }
      if ((UnityEngine.Object) itemData.AuracurseGainSelf2 != (UnityEngine.Object) null)
      {
        if (itemType == "corruption")
          character.SetAuraTrait((Character) null, itemData.AuracurseGainSelf2.Id, itemData.AuracurseGainSelfValue2);
        else if (character.IsHero && (cardItem.CardClass == Enums.CardClass.Monster || cardItem.CardClass == Enums.CardClass.Injury || cardItem.CardClass == Enums.CardClass.Boon))
          character.SetAuraTrait((Character) null, itemData.AuracurseGainSelf2.Id, itemData.AuracurseGainSelfValue2);
        else
          character.SetAuraTrait(character, itemData.AuracurseGainSelf2.Id, itemData.AuracurseGainSelfValue2);
        flag2 = true;
        flag1 = true;
      }
      int num4 = MatchManager.Instance.CountHeroHand(character.HeroIndex);
      if (itemData.CardNum > 0 && (itemData.CardPlace != Enums.CardPlace.Hand || num4 < 10) && ((UnityEngine.Object) itemData.CardToGain != (UnityEngine.Object) null || itemData.CardToGainList != null || itemData.DuplicateActive))
      {
        for (int indexForBatch = 0; indexForBatch < itemData.CardNum; ++indexForBatch)
        {
          string str = "";
          if (itemData.DuplicateActive)
            str = castedCardId;
          else if (itemData.CardToGainList.Count > 0)
          {
            bool flag4 = false;
            while (!flag4)
            {
              int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, itemData.CardToGainList.Count, "item");
              if ((UnityEngine.Object) itemData.CardToGainList[randomIntRange] != (UnityEngine.Object) null)
              {
                str = itemData.CardToGainList[randomIntRange].Id;
                flag4 = true;
              }
            }
          }
          else
            str = !((UnityEngine.Object) itemData.CardToGain != (UnityEngine.Object) null) ? Functions.GetRandomCardIdByTypeAndRandomRarity(itemData.CardToGainType) : itemData.CardToGain.Id;
          string cardInDictionary = MatchManager.Instance.CreateCardInDictionary(str);
          CardData cardData = MatchManager.Instance.GetCardData(cardInDictionary);
          if (itemData.DuplicateActive)
            cardData = MatchManager.Instance.DuplicateCardData(cardData, MatchManager.Instance.GetCardData(str));
          cardData.Vanish = itemData.Vanish;
          if (itemData.Permanent)
          {
            if (itemData.CostZero)
              cardData.EnergyReductionToZeroPermanent = true;
            else
              cardData.EnergyReductionPermanent += itemData.CostReduction;
          }
          else if (itemData.CostZero)
            cardData.EnergyReductionToZeroTemporal = true;
          else
            cardData.EnergyReductionTemporal += itemData.CostReduction;
          MatchManager.Instance.ModifyCardInDictionary(cardInDictionary, cardData);
          if (character.IsHero)
          {
            MatchManager.Instance.itemTimeout[order] = itemData.CardPlace != Enums.CardPlace.Cast ? 0.5f : 0.5f;
            MatchManager.Instance.GenerateNewCard(1, cardInDictionary, false, itemData.CardPlace, heroIndex: character.HeroIndex, indexForBatch: indexForBatch);
          }
          else
            MatchManager.Instance.GenerateNewCard(1, cardInDictionary, false, itemData.CardPlace, heroIndex: character.NPCIndex, isHero: false);
        }
      }
      if (itemData.CardsReduced > 0 && (UnityEngine.Object) character.HeroData != (UnityEngine.Object) null)
      {
        List<string> heroHand = MatchManager.Instance.GetHeroHand(character.HeroIndex);
        List<CardData> cardDataList = new List<CardData>();
        for (int index = 0; index < heroHand.Count; ++index)
        {
          CardData cardData = MatchManager.Instance.GetCardData(heroHand[index]);
          if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && cardData.GetCardFinalCost() > 0 && (itemData.CardToReduceType == Enums.CardType.None || cardData.HasCardType(itemData.CardToReduceType)) && (itemData.CostReduceEnergyRequirement == 0 || cardData.GetCardFinalCost() >= itemData.CostReduceEnergyRequirement))
            cardDataList.Add(cardData);
        }
        if (cardDataList.Count > 0)
        {
          CardData cardData1 = (CardData) null;
          int num5 = itemData.CardsReduced;
          int costReduceReduction = itemData.CostReduceReduction;
          if (num5 > cardDataList.Count)
            num5 = cardDataList.Count;
          List<string> stringList = new List<string>();
          for (int index1 = 0; index1 < num5; ++index1)
          {
            if (itemData.ReduceHighestCost)
            {
              int num6 = -1;
              int index2 = -1;
              CardData cardData2 = (CardData) null;
              for (int index3 = 0; index3 < cardDataList.Count; ++index3)
              {
                int cardFinalCost = cardDataList[index3].GetCardFinalCost();
                if (cardFinalCost > num6)
                {
                  cardData2 = cardDataList[index3];
                  num6 = cardFinalCost;
                  index2 = index3;
                }
              }
              if (index2 > -1)
                cardDataList.RemoveAt(index2);
              if ((UnityEngine.Object) cardData2 != (UnityEngine.Object) null)
                cardData1 = cardData2;
            }
            else
            {
              int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, cardDataList.Count, "item");
              cardData1 = cardDataList[randomIntRange];
            }
            if ((UnityEngine.Object) cardData1 != (UnityEngine.Object) null && !stringList.Contains(cardData1.Id))
            {
              if (itemData.CostReducePermanent)
                cardData1.EnergyReductionPermanent += costReduceReduction;
              else
                cardData1.EnergyReductionTemporal += costReduceReduction;
              MatchManager.Instance.UpdateHandCards();
              CardItem fromTableByIndex = MatchManager.Instance.GetCardFromTableByIndex(cardData1.InternalId);
              fromTableByIndex.PlayDissolveParticle();
              fromTableByIndex.ShowEnergyModification(-costReduceReduction);
              stringList.Add(cardData1.Id);
              MatchManager.Instance.CreateLogCardModification(cardData1.InternalId, MatchManager.Instance.GetHero(character.HeroIndex));
            }
          }
          MatchManager.Instance.ItemActivationDisplay(itemType);
        }
      }
      if (itemData.ChanceToDispel > 0 && itemData.ChanceToDispelNum > 0)
      {
        int num7 = 0;
        if (itemData.ChanceToDispel < 100)
          num7 = MatchManager.Instance.GetRandomIntRange(0, 100, "item");
        if (num7 < itemData.ChanceToDispel)
        {
          character.HealCurses(itemData.ChanceToDispelNum);
          MatchManager.Instance.ItemActivationDisplay(itemType);
        }
        flag1 = true;
      }
      if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && itemData.IsEnchantment && itemData.DrawCards > 0)
      {
        int drawCards = itemData.DrawCards;
        if (itemData.DrawMultiplyByEnergyUsed)
          drawCards *= MatchManager.Instance.energyJustWastedByHero;
        if (drawCards > 0)
        {
          MatchManager.Instance.itemTimeout[order] = !GameManager.Instance.IsMultiplayer() ? (GameManager.Instance.configGameSpeed != Enums.ConfigSpeed.Slow ? 0.4f : 0.5f) : 0.5f;
          if (MatchManager.Instance.CountHeroDeck() == 0)
            MatchManager.Instance.itemTimeout[order] += 0.7f;
          MatchManager.Instance.NewCard(drawCards, Enums.CardFrom.Deck);
        }
      }
      if (itemData.DestroyAfterUse)
      {
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD(itemName + " itemData.DestroyAfterUse", "item");
        if ((UnityEngine.Object) character.HeroData != (UnityEngine.Object) null)
        {
          if (character.HeroIndex == MatchManager.Instance.GetHeroActive())
            MatchManager.Instance.ItemActivationDisplay(itemType);
          if (character.IsHero)
            AtOManager.Instance.RemoveItemFromHero(true, character.HeroIndex, "", itemData.Id);
          else
            AtOManager.Instance.RemoveItemFromHero(false, character.HeroIndex, "", itemData.Id);
          if ((UnityEngine.Object) cardItem != (UnityEngine.Object) null && (UnityEngine.Object) cardItem.Item != (UnityEngine.Object) null)
            MatchManager.Instance.DestroyedItemInThisTurn(character.HeroIndex, cardItem.Id);
          MatchManager.Instance.RefreshItems();
        }
        else if ((UnityEngine.Object) character.NpcData != (UnityEngine.Object) null)
          MatchManager.Instance.RemoveCorruptionItemFromNPC(character.NPCIndex);
      }
      if (itemData.ExactRound > 0 || itemData.RoundCycle > 0 || itemData.TimesPerTurn > 0)
        MatchManager.Instance.ItemActivationDisplay(itemType);
      if (!((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null))
        return;
      if (itemData.EffectCaster != "" && character != null)
      {
        if ((UnityEngine.Object) character.HeroItem != (UnityEngine.Object) null)
          EffectsManager.Instance.PlayEffectAC(itemData.EffectCaster, true, character.HeroItem.CharImageT, false, itemData.EffectCasterDelay);
        else if ((UnityEngine.Object) character.NPCItem != (UnityEngine.Object) null)
          EffectsManager.Instance.PlayEffectAC(itemData.EffectCaster, true, character.NPCItem.CharImageT, false, itemData.EffectCasterDelay);
      }
      if (itemData.EffectTarget != "")
      {
        for (int index = 0; index < characterList.Count; ++index)
        {
          Character character3 = characterList[index];
          if (character3 != null)
          {
            if ((UnityEngine.Object) character3.HeroItem != (UnityEngine.Object) null)
              EffectsManager.Instance.PlayEffectAC(itemData.EffectTarget, true, character3.HeroItem.CharImageT, false, itemData.EffectTargetDelay);
            else if ((UnityEngine.Object) character3.NPCItem != (UnityEngine.Object) null)
              EffectsManager.Instance.PlayEffectAC(itemData.EffectTarget, true, character3.NPCItem.CharImageT, false, itemData.EffectTargetDelay);
          }
        }
      }
      if ((UnityEngine.Object) itemData.ItemSound != (UnityEngine.Object) null)
        GameManager.Instance.PlayAudio(itemData.ItemSound, 0.25f);
      if (flag1)
        MatchManager.Instance.ReDrawInitiatives();
      if (flag2)
        this.ShowCombatText(itemType, itemName, character, charges1, chargesTotal);
      if (!(itemType == "corruption") || !((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null) || MatchManager.Instance.IsBeginTournPhase || theEvent == Enums.EventActivation.PreFinishCast || theEvent == Enums.EventActivation.FinishCast || theEvent == Enums.EventActivation.FinishFinishCast)
        return;
      MatchManager.Instance.DoItemEventDelay();
    }
  }

  private void ShowCombatText(
    string itemType,
    string itemName,
    Character character,
    int charges = -1,
    int chargesTotal = -1)
  {
    if (!((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null))
      return;
    Enums.CombatScrollEffectType type;
    switch (itemType)
    {
      case "weapon":
        type = Enums.CombatScrollEffectType.Weapon;
        break;
      case "armor":
        type = Enums.CombatScrollEffectType.Armor;
        break;
      case "jewelry":
        type = Enums.CombatScrollEffectType.Jewelry;
        break;
      case "accesory":
        type = Enums.CombatScrollEffectType.Accesory;
        break;
      default:
        type = Enums.CombatScrollEffectType.Corruption;
        break;
    }
    if ((UnityEngine.Object) character.HeroItem != (UnityEngine.Object) null)
      character.HeroItem.ScrollCombatText(itemName, type);
    else
      character.NPCItem.ScrollCombatText(itemName, type);
  }

  private Hero GetRandomHero()
  {
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    List<int> intList = new List<int>();
    for (int index = 0; index < teamHero.Length; ++index)
    {
      if (teamHero[index] != null && (UnityEngine.Object) teamHero[index].HeroData != (UnityEngine.Object) null && teamHero[index].Alive)
        intList.Add(index);
    }
    if (intList.Count > 0)
    {
      bool flag = false;
      int num = 0;
      while (!flag)
      {
        int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, intList.Count, "item");
        if (teamHero[intList[randomIntRange]] != null && teamHero[intList[randomIntRange]].Alive)
          return teamHero[intList[randomIntRange]];
        ++num;
        if (num > 10)
          flag = true;
      }
    }
    return (Hero) null;
  }

  private NPC GetRandomNPC()
  {
    NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();
    List<int> intList = new List<int>();
    for (int index = 0; index < teamNpc.Length; ++index)
    {
      if (teamNpc[index] != null && teamNpc[index].Alive)
        intList.Add(index);
    }
    if (intList.Count > 0)
    {
      bool flag = false;
      int num = 0;
      while (!flag)
      {
        int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, intList.Count, "item");
        if (teamNpc[intList[randomIntRange]] != null && teamNpc[intList[randomIntRange]].Alive)
          return teamNpc[intList[randomIntRange]];
        ++num;
        if (num > 10)
          flag = true;
      }
    }
    return (NPC) null;
  }

  private Character GetRandomCharacter() => MatchManager.Instance.GetRandomIntRange(0, 2, "item") == 0 ? (Character) this.GetRandomHero() : (Character) this.GetRandomNPC();

  private List<Character> GetAllHeroList()
  {
    Character[] teamHero = (Character[]) MatchManager.Instance.GetTeamHero();
    List<Character> allHeroList = new List<Character>();
    for (int index = 0; index < teamHero.Length; ++index)
    {
      if (teamHero[index] != null && (UnityEngine.Object) teamHero[index].HeroData != (UnityEngine.Object) null && teamHero[index].Alive)
        allHeroList.Add(teamHero[index]);
    }
    return allHeroList;
  }

  private List<Character> GetAllNPCList()
  {
    Character[] teamNpc = (Character[]) MatchManager.Instance.GetTeamNPC();
    List<Character> allNpcList = new List<Character>();
    for (int index = 0; index < teamNpc.Length; ++index)
    {
      if (teamNpc[index] != null && teamNpc[index].Alive)
        allNpcList.Add(teamNpc[index]);
    }
    return allNpcList;
  }

  private Character GetFlatHPCharacter(bool highestHp, bool isHero)
  {
    int num = 0;
    if (!highestHp)
      num = 10000;
    List<Character> characterList1 = new List<Character>();
    List<Character> characterList2 = !isHero ? this.GetAllNPCList() : this.GetAllHeroList();
    for (int index = 0; index < characterList2.Count; ++index)
    {
      if (highestHp)
      {
        if (characterList2[index].HpCurrent >= num)
        {
          characterList1.Add(characterList2[index]);
          num = characterList2[index].HpCurrent;
        }
      }
      else if (characterList2[index].HpCurrent <= num)
      {
        characterList1.Add(characterList2[index]);
        num = characterList2[index].HpCurrent;
      }
    }
    if (characterList1.Count > 1)
    {
      for (int index = characterList1.Count - 1; index >= 0; --index)
      {
        if (highestHp)
        {
          if (characterList1[index].HpCurrent < num)
            characterList1.RemoveAt(index);
        }
        else if (characterList1[index].HpCurrent > num)
          characterList1.RemoveAt(index);
      }
    }
    if (characterList1.Count > 1)
      return characterList1[MatchManager.Instance.GetRandomIntRange(0, characterList1.Count, "item")];
    return characterList1.Count == 1 ? characterList1[0] : (Character) null;
  }

  public void surprisebox(Character theCharacter, bool isRare, string itemName)
  {
    if (!((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null))
      return;
    int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, 6, "item");
    int charges = 2;
    if (isRare)
      charges = 4;
    switch (randomIntRange)
    {
      case 0:
        theCharacter.SetAuraTrait(theCharacter, "fast", charges);
        break;
      case 1:
        theCharacter.SetAuraTrait(theCharacter, "powerful", charges);
        break;
      case 2:
        theCharacter.SetAuraTrait(theCharacter, "bless", charges);
        break;
      case 3:
        theCharacter.SetAuraTrait(theCharacter, "slow", charges);
        break;
      case 4:
        theCharacter.SetAuraTrait(theCharacter, "vulnerable", charges);
        break;
      default:
        theCharacter.SetAuraTrait(theCharacter, "mark", charges);
        break;
    }
    theCharacter.HeroItem.ScrollCombatText(itemName, Enums.CombatScrollEffectType.Accesory);
  }

  public void surprisegiftbox(Character theCharacter, bool isRare, string itemName)
  {
    if (!((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null))
      return;
    int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, 6, "item");
    int charges = 2;
    if (isRare)
      charges = 4;
    string auracurse;
    switch (randomIntRange)
    {
      case 0:
        auracurse = "fast";
        break;
      case 1:
        auracurse = "powerful";
        break;
      case 2:
        auracurse = "bless";
        break;
      case 3:
        auracurse = "slow";
        break;
      case 4:
        auracurse = "vulnerable";
        break;
      default:
        auracurse = "mark";
        break;
    }
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    for (int index = 0; index < teamHero.Length; ++index)
    {
      if (teamHero[index] != null && (UnityEngine.Object) teamHero[index].HeroData != (UnityEngine.Object) null && teamHero[index].Alive)
        teamHero[index].SetAuraTrait(theCharacter, auracurse, charges);
    }
    theCharacter.HeroItem.ScrollCombatText(itemName, Enums.CombatScrollEffectType.Accesory);
  }
}
