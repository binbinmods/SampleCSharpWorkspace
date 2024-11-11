using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Obeliskial_Content;
using Obeliskial_Essentials;
using System.IO;
using static UnityEngine.Mathf;

namespace TheBiohealer
{
    public class TraitFunctions
    {

        public static void TraitHeal(ref Character _character, ref Character _target, int healAmount, string traitName)
        {
            int _hp = healAmount;
            if (_target.GetHpLeftForMax() < healAmount)
                _hp = _target.GetHpLeftForMax();
            if (_hp <= 0)
                return;
            _target.ModifyHp(_hp);
            CastResolutionForCombatText _cast = new CastResolutionForCombatText();
            _cast.heal = _hp;
            if ((Object)_target.HeroItem != (Object)null)
            {
                _target.HeroItem.ScrollCombatTextDamageNew(_cast);
                EffectsManager.Instance.PlayEffectAC("healimpactsmall", true, _target.HeroItem.CharImageT, false);
            }
            else
            {
                _target.NPCItem.ScrollCombatTextDamageNew(_cast);
                EffectsManager.Instance.PlayEffectAC("healimpactsmall", true, _target.NPCItem.CharImageT, false);
            }
            _target.SetEvent(Enums.EventActivation.Healed);
            _character.SetEvent(Enums.EventActivation.Heal, _target);
            _character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_" + traitName), Enums.CombatScrollEffectType.Trait);
        }

        public static void TraitHealHero(ref Character _character, ref Hero _target, int healAmount, string traitName)
        {
            if (_target==null || !_target.IsHero || !_target.Alive){
                return;
            }
            
            int _hp = healAmount;
            if (_target.GetHpLeftForMax() < healAmount)
                _hp = _target.GetHpLeftForMax();
            if (_hp <= 0)
                return;
            _target.ModifyHp(_hp);
            CastResolutionForCombatText _cast = new CastResolutionForCombatText();
            _cast.heal = _hp;
            if ((Object) _target.HeroItem != (Object) null)
            {
                _target.HeroItem.ScrollCombatTextDamageNew(_cast);
                EffectsManager.Instance.PlayEffectAC("healimpactsmall", true, _target.HeroItem.CharImageT, false);
            }
            else
            {
                _target.NPCItem.ScrollCombatTextDamageNew(_cast);
                EffectsManager.Instance.PlayEffectAC("healimpactsmall", true, _target.NPCItem.CharImageT, false);
            }
            _target.SetEvent(Enums.EventActivation.Healed);
            _character.SetEvent(Enums.EventActivation.Heal, _target);
            _character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_" + traitName), Enums.CombatScrollEffectType.Trait);
        }

        public static void WhenYouPlayXRefund1Energy(Enums.CardType cardType, ref Character _character, string traitName, string traitID)
        {
            // too lazy to write this since they all come with secondary effects
        }

        public static void ApplyAuraCurseTo(string auraCurse, int amount, bool allHeroFlag, bool allNpcFlag, bool randomHeroFlag, bool randomNpcFlag, ref Character _character, ref Hero[] teamHeroes, ref NPC[] teamNpc, string traitName, string soundEffect)
        {
            if (allNpcFlag)
            {
                for (int index = 0; index < teamNpc.Length; ++index)
                {
                    if (teamNpc[index] != null && teamNpc[index].Alive)
                    {
                        teamNpc[index].SetAuraTrait(_character, auraCurse, amount);
                        if ((Object)teamNpc[index].NPCItem != (Object)null)
                        {
                            teamNpc[index].NPCItem.ScrollCombatText(Texts.Instance.GetText("traits_" + traitName), Enums.CombatScrollEffectType.Trait);
                            EffectsManager.Instance.PlayEffectAC(soundEffect, true, teamNpc[index].NPCItem.CharImageT, false);
                        }
                    }
                }
            }
            if (allHeroFlag)
            {
                for (int index = 0; index < teamHeroes.Length; ++index)
                {
                    if (teamHeroes[index] != null && teamHeroes[index].Alive)
                    {
                        teamHeroes[index].SetAuraTrait(_character, auraCurse, amount);
                        if ((Object)teamHeroes[index].NPCItem != (Object)null)
                        {
                            teamHeroes[index].HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_" + traitName), Enums.CombatScrollEffectType.Trait);
                            EffectsManager.Instance.PlayEffectAC(soundEffect, true, teamHeroes[index].NPCItem.CharImageT, false);
                        }
                    }
                }
            }


        }

        public static void WhenYouGainXGainY(string gainedAuraCurse, string desiredAuraCurse, string appliedAuraCurse, int n_charges_incoming, int n_bonus_charges, float multiplier, ref Character _character, string traitName)
        {
            // Grants a multiplier or bonus charged amount of a second auraCurse given a first auraCurse
            //Plugin.Log.LogDebug("WhenYouGainXGainY Debug Start");
            if (MatchManager.Instance != null && gainedAuraCurse != null && _character.HeroData != null)
            {
                //Plugin.Log.LogDebug("WhenYouGainXGainY inside conditions 1");
                if (gainedAuraCurse == desiredAuraCurse)
                {
                    //Plugin.Log.LogDebug("WhenYouGainXGainY inside conditions 2");
                    int toApply = RoundToInt((n_charges_incoming + n_bonus_charges) * multiplier);
                    _character.SetAuraTrait(_character, appliedAuraCurse, toApply);
                    _character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_" + traitName), Enums.CombatScrollEffectType.Trait);
                }
            }
        }

        public static void WhenYouPlayXGainY(Enums.CardType desiredCardType, string desiredAuraCurse, int n_charges, CardData castedCard, ref Character _character, string traitName)
        {
            // Grants n_charges of desiredAuraCurse to self when you play a desired cardtype
            //Plugin.Log.LogDebug("WhenYouPlayXGainY Debug Start");
            if (MatchManager.Instance != null && castedCard != null && _character.HeroData != null)
            {
                //Plugin.Log.LogDebug("WhenYouPlayXGainY inside conditions 1");
                if (castedCard.GetCardTypes().Contains(desiredCardType))
                {
                    //Plugin.Log.LogDebug("WhenYouPlayXGainY inside conditions 2");

                    _character.SetAuraTrait(_character, desiredAuraCurse, n_charges);
                    _character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_" + traitName), Enums.CombatScrollEffectType.Trait);
                }
            }
        }

        public static void ReduceCostByStacks(Enums.CardType cardType, string auraCurseName, int n_charges, ref Character _character, ref List<string> heroHand, ref List<CardData> cardDataList, string traitName, bool applyToAllCards)
        {
            // Reduces the cost of all cards of cardType by 1 for every n_charges of the auraCurse
            if (!((UnityEngine.Object)_character.HeroData != (UnityEngine.Object)null))
                return;
            int num = FloorToInt((float)(_character.EffectCharges(auraCurseName) / n_charges));
            if (num <= 0)
                return;
            for (int index = 0; index < heroHand.Count; ++index)
            {
                CardData cardData = MatchManager.Instance.GetCardData(heroHand[index]);
                if ((cardData.GetCardFinalCost() > 0) && (cardData.GetCardTypes().Contains(cardType) || applyToAllCards)) //previous .Contains(Enums.CardType.Attack)
                    cardDataList.Add(cardData);
            }
            for (int index = 0; index < cardDataList.Count; ++index)
            {
                cardDataList[index].EnergyReductionTemporal += num;
                MatchManager.Instance.UpdateHandCards();
                CardItem fromTableByIndex = MatchManager.Instance.GetCardFromTableByIndex(cardDataList[index].InternalId);
                fromTableByIndex.PlayDissolveParticle();
                fromTableByIndex.ShowEnergyModification(-num);
                _character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_" + traitName), Enums.CombatScrollEffectType.Trait);
                MatchManager.Instance.CreateLogCardModification(cardDataList[index].InternalId, MatchManager.Instance.GetHero(_character.HeroIndex));
            }
        }

        public static void IncreaseChargesByStacks(string auraCurseToModify, float stacks_per_bonus, string auraCurseDependent, ref Character _character, string traitName)
        {
            // increases the amount of ACtoModify that by. 
            // For instance if you want to increase the amount of burn you apply by 1 per 10 stacks of spark, then IncreaseChargesByStacks("burn",10,"spark",..)
            // Currently does not output anything to the combat log, because I don't know if it should
            int n_stacks = _character.GetAuraCharges(auraCurseDependent);
            int toIncrease = FloorToInt(n_stacks / stacks_per_bonus);
            _character.ModifyAuraCurseQuantity(auraCurseToModify, toIncrease);
        }

        public static string TextChargesLeft(int currentCharges, int chargesTotal)
        {
            int cCharges = currentCharges;
            int cTotal = chargesTotal;
            return "<br><color=#FFF>" + cCharges.ToString() + "/" + cTotal.ToString() + "</color>";
        }

        public static void Duality(ref Character _character, ref CardData _castedCard, Enums.CardClass class1, Enums.CardClass class2, string traitName)
        {
            if (!((Object)MatchManager.Instance != (Object)null) || !((Object)_castedCard != (Object)null))
                return;
            TraitData traitData = Globals.Instance.GetTraitData(traitName);
            if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(traitName) && MatchManager.Instance.activatedTraits[traitName] > traitData.TimesPerTurn - 1)
                return;
            for (int index1 = 0; index1 < 2; ++index1)
            {
                Enums.CardClass cardClass1;
                Enums.CardClass cardClass2;
                if (index1 == 0)
                {
                    cardClass1 = class1;
                    cardClass2 = class2;
                }
                else
                {
                    cardClass1 = class2;
                    cardClass2 = class1;
                }
                if (_castedCard.CardClass == cardClass1)
                {
                    if (MatchManager.Instance.CountHeroHand() == 0 || !((Object)_character.HeroData != (Object)null))
                        break;
                    List<CardData> cardDataList = new List<CardData>();
                    List<string> heroHand = MatchManager.Instance.GetHeroHand(_character.HeroIndex);
                    int num1 = 0;
                    for (int index2 = 0; index2 < heroHand.Count; ++index2)
                    {
                        CardData cardData = MatchManager.Instance.GetCardData(heroHand[index2]);
                        if ((Object)cardData != (Object)null && cardData.CardClass == cardClass2 && _character.GetCardFinalCost(cardData) > num1)
                            num1 = _character.GetCardFinalCost(cardData);
                    }
                    if (num1 <= 0)
                        break;
                    for (int index3 = 0; index3 < heroHand.Count; ++index3)
                    {
                        CardData cardData = MatchManager.Instance.GetCardData(heroHand[index3]);
                        if ((Object)cardData != (Object)null && cardData.CardClass == cardClass2 && _character.GetCardFinalCost(cardData) >= num1)
                            cardDataList.Add(cardData);
                    }
                    if (cardDataList.Count <= 0)
                        break;
                    CardData cardData1 = cardDataList.Count != 1 ? cardDataList[MatchManager.Instance.GetRandomIntRange(0, cardDataList.Count, "trait")] : cardDataList[0];
                    if (!((Object)cardData1 != (Object)null))
                        break;
                    if (!MatchManager.Instance.activatedTraits.ContainsKey(traitName))
                        MatchManager.Instance.activatedTraits.Add(traitName, 1);
                    else
                        ++MatchManager.Instance.activatedTraits[traitName];
                    MatchManager.Instance.SetTraitInfoText();
                    int num2 = 1;
                    cardData1.EnergyReductionTemporal += num2;
                    MatchManager.Instance.GetCardFromTableByIndex(cardData1.InternalId).ShowEnergyModification(-num2);
                    MatchManager.Instance.UpdateHandCards();
                    _character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_" + traitName) + TextChargesLeft(MatchManager.Instance.activatedTraits[traitName], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
                    MatchManager.Instance.CreateLogCardModification(cardData1.InternalId, MatchManager.Instance.GetHero(_character.HeroIndex));
                    break;
                }
            }
        }

        public static void PermanentyReduceXWhenYouPlayY(ref Character _character, ref CardData _castedCard, Enums.CardType reduceThis, Enums.CardType whenYouPlayThis, int amountToReduce, string traitName)
        {
            if (!((Object)MatchManager.Instance != (Object)null) || !((Object)_castedCard != (Object)null))
                return;
            TraitData traitData = Globals.Instance.GetTraitData(traitName);
            if (MatchManager.Instance.activatedTraits != null && MatchManager.Instance.activatedTraits.ContainsKey(traitName) && MatchManager.Instance.activatedTraits[traitName] > traitData.TimesPerTurn - 1)
                return;

            if (!_castedCard.GetCardTypes().Contains(whenYouPlayThis))
                return;
            
            if (MatchManager.Instance.CountHeroHand() == 0 || !((Object)_character.HeroData != (Object)null))
                return;


            List<CardData> cardDataList = new List<CardData>();
            List<string> heroHand = MatchManager.Instance.GetHeroHand(_character.HeroIndex);

            if (reduceThis==Enums.CardType.None){ 
                for (int handIndex = 0; handIndex < heroHand.Count; ++handIndex)
                {
                    CardData cardData = MatchManager.Instance.GetCardData(heroHand[handIndex]);
                    if ((Object)cardData != (Object)null)
                        cardDataList.Add(cardData);
                }  
            }
            else{
               for (int handIndex = 0; handIndex < heroHand.Count; ++handIndex)
                {
                    CardData cardData = MatchManager.Instance.GetCardData(heroHand[handIndex]);
                    if ((Object)cardData != (Object)null && cardData.GetCardTypes().Contains(reduceThis))
                        cardDataList.Add(cardData);
                }  
            }

            if (!MatchManager.Instance.activatedTraits.ContainsKey(traitName))
                MatchManager.Instance.activatedTraits.Add(traitName, 1);
            else
                ++MatchManager.Instance.activatedTraits[traitName];

            CardData selectedCard = cardDataList[MatchManager.Instance.GetRandomIntRange(0, cardDataList.Count, "trait")];
            selectedCard.EnergyReductionPermanent += amountToReduce;
            MatchManager.Instance.GetCardFromTableByIndex(selectedCard.InternalId).ShowEnergyModification(-amountToReduce);
            MatchManager.Instance.UpdateHandCards();
            _character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_" + traitName) + TextChargesLeft(MatchManager.Instance.activatedTraits[traitName], traitData.TimesPerTurn), Enums.CombatScrollEffectType.Trait);
            MatchManager.Instance.CreateLogCardModification(selectedCard.InternalId, MatchManager.Instance.GetHero(_character.HeroIndex));
        }

        public static int CountAllStacks(string auraCurse, Hero[] teamHero, NPC[] teamNpc){
            int stacks = 0;
            for (int index = 0; index < teamHero.Length; ++index)
            {
                if (teamHero[index] != null && (UnityEngine.Object)teamHero[index].HeroData != (UnityEngine.Object)null && teamHero[index].Alive)
                {
                    stacks += teamHero[index].GetAuraCharges(auraCurse);
                }
            }
            for (int index = 0; index < teamNpc.Length; ++index)
            {
                if (teamNpc[index] != null && teamNpc[index].Alive)
                {
                    stacks += teamNpc[index].GetAuraCharges(auraCurse);
                }
            }
            return stacks;
        }

        public static void CastTargetCard(string cardToCast){
            //Plugin.Log.LogDebug("Binbin PestilyBiohealer - trying to cast card: "+cardToCast);
            CardData card = Globals.Instance.GetCardData(cardToCast);
            MatchManager.Instance.StartCoroutine(MatchManager.Instance.CastCard(_automatic: true, _card: card, _energy: 0));

            //MatchManager.Instance.CastCard(_card: card);
        }
    }
}
