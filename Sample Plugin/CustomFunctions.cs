using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Obeliskial_Content;
using Obeliskial_Essentials;
using System.IO;
using static UnityEngine.Mathf;
using static SamplePlugin.Plugin;

namespace SamplePlugin
{
    public class CustomFunctions
    {

        public static string debugBase = "<RenameThis>";
        public static string perkBase = "<RenameThisForPerks>";

        /// <summary>
        /// Shortcut for Plugin.Log.LogDebug(debugBase). debugBase set in CustomFunctions
        /// </summary>
        /// <param name="s">String to be output to debug log</param>

        public static void PLog(string s)
        {
            Plugin.Log.LogDebug(debugBase + s);
        }

        /// <summary>
        /// Indirect healing for Traits (Ottis's Shielder, Malukah's Voodoo etc)
        /// </summary>
        /// <param name="_character">Source of Healing</param>
        /// <param name="_target">Target to Heal</param>
        /// <param name="healAmount">Amount to Heal</param>
        /// <param name="traitName">Trait it is attributed to</param>
        public static void TraitHeal(ref Character _character, Character _target, int healAmount, string traitName)
        {
            // Used to have a ref for _target. Need to make sure that it works without the ref
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

        /// <summary>
        /// TraitHeal but for Heroes (needed if ref is required)
        /// </summary>
        /// <param name="_character"></param>
        /// <param name="_target"></param>
        /// <param name="healAmount"></param>
        /// <param name="traitName"></param>
        public static void TraitHealHero(ref Character _character, ref Hero _target, int healAmount, string traitName)
        {
            if (_target == null || !_target.IsHero || !_target.Alive)
            {
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

        /// <summary>
        /// Grants AuraCurse to a character whenever they gain a different AuraCurse. I.e. When you gain Vitality, gain 2x Regen.
        /// </summary>
        /// <param name="ACGained">AuraCurse that triggers this</param>
        /// <param name="targetAC">AuraCurse that you are looking for</param>
        /// <param name="ACtoApply">AuraCurse that will be applied</param>
        /// <param name="nGained"> charges of AC that was gained by the target</param>
        /// <param name="nToApply"> charges of ACtoApply to apply</param>
        /// <param name="multiplier"> multiplier for nToApply </param>
        /// <param name="_character"> Character that will gain the AC</param>
        /// <param name="traitName">Trait this is attributed to< /param>
        public static void WhenYouGainXGainY(string ACGained, string targetAC, string ACtoApply, int nGained, int nToApply, float multiplier, ref Character _character, string traitName)
        {
            // Grants a multiplier or bonus charged amount of a second auraCurse given a first auraCurse

            // Makes sure it is a valid target (a living hero)
            if (MatchManager.Instance == null && ACGained == null && !IsLivingHero(_character))
                return;

            // Prevents infinite loop
            if (targetAC == ACtoApply)
                return;

            if (ACGained == targetAC)
            {
                int toApply = RoundToInt((nGained + nToApply) * multiplier);
                _character.SetAuraTrait(_character, ACtoApply, toApply);
                _character.HeroItem.ScrollCombatText(Texts.Instance.GetText("traits_" + traitName), Enums.CombatScrollEffectType.Trait);
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

        /// <summary>
        /// Function to reduce the cost of all cards of cardType by 1 for ever nCharges of an AuraCurse on them. Works like Zek's Dark Feast.
        /// </summary>
        /// <param name="cardType">Card type to reduce</param>
        /// <param name="auraCurseName">AuraCurse that determines the number of charges</param>
        /// <param name="nCharges">The number of charges needed to reduce the cost by 1</param>
        /// <param name="_character">Character</param>
        /// <param name="heroHand"></param>
        /// <param name="cardDataList">Cards in hand</param>
        /// <param name="traitName">Name of the trait that this is attributable to</param>
        /// <param name="applyToAllCards">Flag to change it from applying only to one card type to applying to all card types </param>
        public static void ReduceCostByStacks(Enums.CardType cardType, string auraCurseName, int nCharges, ref Character _character, ref List<string> heroHand, ref List<CardData> cardDataList, string traitName, bool applyToAllCards)
        {
            // Reduces the cost of all cards of cardType by 1 for every n_charges of the auraCurse
            if (!((UnityEngine.Object)_character.HeroData != (UnityEngine.Object)null))
                return;
            int num = FloorToInt((float)(_character.EffectCharges(auraCurseName) / nCharges));
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

        /// <summary>
        /// Adds an immunity to a character.
        /// </summary>
        /// <param name="immunity">AuraCurse to be immmune to</param>
        /// <param name="_character">Character to gain the immmunity</param>
        public static void AddImmunityToHero(string immunity, ref Hero _character)
        {
            if (_character==null)
                return;
            if (!_character.AuracurseImmune.Contains(immunity))
                _character.AuracurseImmune.Add(immunity);
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

        /// <summary>
        /// Gets the AuraCurseData. Syntatic sugar for Globals.Instance.GetAuraCurseData(ac) since its easily forgotten.
        /// </summary>
        /// <param name="auraCurse">AuraCurse you are looking for</param>
        /// <returns></returns>
        public static AuraCurseData GetAuraCurseData(string auraCurse)
        {
            return Globals.Instance.GetAuraCurseData(auraCurse);
        }

        /// <summary>
        /// Formats the text that will appear when you have a certain number of charges remaining.
        /// </summary>
        /// <param name="currentCharges"></param>
        /// <param name="chargesTotal"></param>
        /// <returns>A fraction A/B</returns>
        public static string TextChargesLeft(int currentCharges, int chargesTotal)
        {
            int cCharges = currentCharges;
            int cTotal = chargesTotal;
            return "<br><color=#FFF>" + cCharges.ToString() + "/" + cTotal.ToString() + "</color>";
        }

        /// <summary>
        /// A Duality trait
        /// </summary>
        /// <param name="_character">Character casting the card</param>
        /// <param name="_castedCard">Card that was class</param>
        /// <param name="class1">Card Class that could be reduced</param>
        /// <param name="class2">Card Class that could be reduced</param>
        /// <param name="traitName">Trait this is attributable to</param>
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

        /// <summary>
        /// For the rest of combat, reduces the cost of a card type when you play a different card type.
        /// </summary>
        /// <param name="_character">Character casting the card</param>
        /// <param name="_castedCard">Card that was cast</param>
        /// <param name="reduceThis">Card type to rduce</param>
        /// <param name="whenYouPlayThis"> Card type to trigger the effect</param>
        /// <param name="amountToReduce"> Amount of energy reduction per time this triggers</param>
        /// <param name="traitName"> Trait this is attributed to</param>
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

            if (reduceThis == Enums.CardType.None)
            {
                for (int handIndex = 0; handIndex < heroHand.Count; ++handIndex)
                {
                    CardData cardData = MatchManager.Instance.GetCardData(heroHand[handIndex]);
                    if ((Object)cardData != (Object)null)
                        cardDataList.Add(cardData);
                }
            }
            else
            {
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


        /// <summary>
        /// Counts all stacks of a given Aura or Curse
        /// </summary>
        /// <param name="auraCurse">AuraCurse to check</param>
        /// <param name="teamHero">Optional team of heroes</param>
        /// <param name="teamNpc">Optional team of Npcs</param>
        /// <param name="includeHeroes">Optional flag to turn off counting heroes</param>
        /// <param name="includeNpcs">Optional flag to turn off counting npcs</param>
        /// <returns></returns>
        public static int CountAllStacks(string auraCurse, Hero[] teamHero = null, NPC[] teamNpc = null, bool includeHeroes = true, bool includeNpcs = true)
        {
            if (MatchManager.Instance == null)
                return 0;

            // Assigns teamHero and teamNpc if null
            teamHero ??= MatchManager.Instance.GetTeamHero();
            teamNpc ??= MatchManager.Instance.GetTeamNPC();

            int stacks = 0;
            if (includeHeroes)
            {
                for (int index = 0; index < teamHero.Length; ++index)
                {
                    if (IsLivingHero(teamHero[index]))
                    {
                        stacks += teamHero[index].GetAuraCharges(auraCurse);
                    }
                }
            }
            if (includeNpcs)
            {
                for (int index = 0; index < teamNpc.Length; ++index)
                {
                    if (IsLivingNPC(teamNpc[index]))
                    {
                        stacks += teamNpc[index].GetAuraCharges(auraCurse);
                    }
                }
            }
            return stacks;
        }

        /// <summary>
        /// Deals indirect damage to all NPCs
        /// </summary>
        /// <param name="damageType">Damage type to deal</param>
        /// <param name="amount">Amount of damage to deal</param>
        public static void DealIndirectDamageToAllMonsters(Enums.DamageType damageType, int amount)
        {
            Plugin.Log.LogDebug(debugBase + "Dealing Indirect Damage");
            if (MatchManager.Instance == null)
                return;
            NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();
            for (int index = 0; index < teamNpc.Length; ++index)
            {
                NPC npc = teamNpc[index];
                if (IsLivingNPC(npc))
                {
                    npc.IndirectDamage(damageType, amount);
                }
            }
        }

        /// <summary>
        /// Gets a random character from a character array (either heroes or NPCs)
        /// </summary>
        /// <param name="array">Array to get the hero from</param>
        /// <returns>The random character</returns>
        public static Character GetRandomCharacter(Character[] array)
        {
            if (array == null)
            {
                Plugin.Log.LogDebug(debugBase + "Null Array");

            }
            List<Character> validCharacters = [];
            for (int index = 0; index < array.Length; index++)
            {
                if (array[index] == null)
                {
                    Plugin.Log.LogDebug(debugBase + "Null index");
                    continue;
                }
                Character _character = array[index];
                if (_character.Alive && _character != null)
                {
                    validCharacters.Add(_character);
                }
            }
            if (validCharacters.Count == 0)
                return null;

            int i = MatchManager.Instance.GetRandomIntRange(0, validCharacters.Count);

            if (i < validCharacters.Count)
                return validCharacters[i];
            if (validCharacters[i] == null)
                return null;
            else
                return validCharacters[0];
        }

        /// <summary>
        /// Checks to see if the character is a living hero
        /// </summary>
        /// <param name="_character">Character to check</param>
        /// <returns></returns>
        public static bool IsLivingHero(Character _character)
        {
            return _character != null && _character.Alive && _character.IsHero;
        }

        /// <summary>
        /// Checks to see if the character is a living npc.
        /// </summary>
        /// <param name="_character">Character to check</param>
        /// <returns></returns>
        public static bool IsLivingNPC(Character _character)
        {
            return _character != null && _character.Alive && !_character.IsHero;
        }

        /// <summary>
        /// DEPRECATED Checks to see if a character has a perk for setting in AtOManager.GlobalAuraCurseModificationByTraitsAndItems
        /// </summary>
        /// <param name="perkName">Perk to check</param>
        /// <param name="flag">Checks if it applies to heroes, monsters, or globally</param>
        /// <param name="__instance">AtO instance</param>
        /// <param name="_characterTarget">The CharacterTarget</param>
        /// <returns>true if the character has the perk </returns>
        public static bool CharacterHasPerkForSet(string perkName, bool flag, AtOManager __instance, Character _characterTarget)
        {
            return flag && _characterTarget != null && __instance.CharacterHavePerk(_characterTarget.SubclassName, perkBase + perkName);
        }

        /// <summary>
        /// DEPRECATED Checks to see if a character has a perk for consuming in AtOManager.GlobalAuraCurseModificationByTraitsAndItems
        /// </summary>
        /// <param name="perkName">Perk to check</param>
        /// <param name="flag">Checks if it applies to heroes, monsters, or globally</param>
        /// <param name="__instance">AtO instance</param>
        /// <param name="_characterTarget">The CharacterTarget</param>
        /// <returns>true if the character has the perk </returns>
        public static bool CharacterHasPerkForConsume(string perkName, bool flag, AtOManager __instance, Character _characterCaster)
        {
            return flag && _characterCaster != null && __instance.CharacterHavePerk(_characterCaster.SubclassName, perkBase + perkName);
        }


        /// <summary>
        /// Checks to see if your team has a perk.
        /// </summary>
        /// <param name="perkName">Perk to check</param>
        /// <returns>true if the team has the perk</returns>
        public static bool TeamHasPerk(string perkName)
        {
            if (AtOManager.Instance == null)
                return false;
            return AtOManager.Instance.TeamHavePerk(perkBase + perkName) || AtOManager.Instance.TeamHavePerk(perkName);
        }

        /// <summary>
        /// DEPRECATED: Checks if your team has a perk for the set function in AtOManager.GlobalAuraCurseModificationByTraitsAndItems
        /// </summary>
        /// <param name="perkName">Perk to check</param>
        /// <param name="flag">Checks if it applies to heroes, monsters, or globally</param>
        /// <param name="__instance">AtO instance</param>
        /// <param name="_characterTarget">The CharacterTarget</param>
        /// <returns>true if the team has the perk</returns>

        public static bool TeamHasPerkForSet(string perkName, bool flag, AtOManager __instance, Character _characterTarget)
        {
            return _characterTarget != null && __instance.TeamHavePerk(perkBase + perkName) && flag;
        }

        /// <summary>
        /// DEPRECATED: Checks if your team has a perk for the consume function in AtOManager.GlobalAuraCurseModificationByTraitsAndItems
        /// </summary>
        /// <param name="perkName">Perk to check</param>
        /// <param name="flag">Checks if it applies to heroes, monsters, or globally</param>
        /// <param name="__instance">AtO instance</param>
        /// <param name="_characterCaster">The CharacterCaster</param>
        /// <returns>true if the team has the perk</returns>
        public static bool TeamHasPerkForConsume(string perkName, bool flag, AtOManager __instance, Character _characterCaster)
        {
            if (__instance == null)
                return false;
            return _characterCaster != null && (__instance.TeamHavePerk(perkBase + perkName) || __instance.TeamHavePerk(perkName)) && flag;
        }

        /// <summary>
        /// Checks to see if a character has a perk.
        /// </summary>
        /// <param name="_character">Character to check</param>
        /// <param name="_perkID">String id of the perk</param>
        /// <returns></returns>
        public static bool CharacterObjectHavePerk(Character _character, string _perkID)
        {
            if (_character == null || AtOManager.Instance == null)
                return false;
            // if (_perkID.StartsWith(perkBase))
            //     AtOManager.Instance.CharacterHavePerk(_character.SubclassName, _perkID);

            return AtOManager.Instance.CharacterHavePerk(_character.SubclassName, perkBase + _perkID) || AtOManager.Instance.CharacterHavePerk(_character.SubclassName, _perkID);
        }

        /// <summary>
        /// Plays/Casts a card. Triggers all effects playing the card normally would. Card is treated as costing 0.
        /// </summary>
        /// <param name="cardToCast">string id of the card you want to cast</param>
        public static void PlayCardForFree(string cardToCast)
        {
            //Plugin.Log.LogDebug("Binbin PestilyBiohealer - trying to cast card: "+cardToCast);
            if (cardToCast == null || Globals.Instance == null)
                return;
            CardData card = Globals.Instance.GetCardData(cardToCast);

            if (card == null)
            {
                PLog("Invalid CardName");
                return;
            }

            MatchManager.Instance.StartCoroutine(MatchManager.Instance.CastCard(_automatic: true, _card: card, _energy: 0));

            //MatchManager.Instance.CastCard(_card: card);
        }
    }
}
