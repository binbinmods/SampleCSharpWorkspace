using System;
using System.Collections.Generic;
using static Enums;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore;
using System.Reflection;
using HarmonyLib;
using BepInEx;
using Obeliskial_Essentials;
using static UnityEngine.JsonUtility;
using static Obeliskial_Essentials.Essentials;
using static Obeliskial_Content.Content;

namespace Obeliskial_Content
{
	public class DataTextConvert
	{

        /*
         *                                                                                           
         *    888888888888  ,ad8888ba,          88888888ba,         db    888888888888    db         
         *         88      d8"'    `"8b         88      `"8b       d88b        88        d88b        
         *         88     d8'        `8b        88        `8b     d8'`8b       88       d8'`8b       
         *         88     88          88        88         88    d8'  `8b      88      d8'  `8b      
         *         88     88          88        88         88   d8YaaaaY8b     88     d8YaaaaY8b     
         *         88     Y8,        ,8P        88         8P  d8""""""""8b    88    d8""""""""8b    
         *         88      Y8a.    .a8P         88      .a8P  d8'        `8b   88   d8'        `8b   
         *         88       `"Y8888Y"'          88888888Y"'  d8'          `8b  88  d8'          `8b  
         *
         *   Converts to corresponding AtO type (including string->Enums).
         */


        public static int ToData<T>(string text)
        {
            // I wanted to use this to convert enum arrays in a single line, but it's more effect than it's worth.
            // if (typeof(T).IsArray && typeof(T).GetElementType().BaseType == typeof(System.Enum))
            if (typeof(T).BaseType == typeof(System.Enum))
            {
                try
                {
                    return (int)Enum.Parse(typeof(T), text, true);
                }
                catch
                {
                    LogError("ToData<T> has captured a value of type " + typeof(T) + " that it cannot parse: " + text);
                    return 0;
                }
            }
            LogError("ToData<T> is capturing a type that it isn't set up for!!! " + typeof(T));
            return 0;
        }

        public static AuraCurseData ToData(AuraCurseDataText text)
        {
            AuraCurseData data = ScriptableObject.CreateInstance<AuraCurseData>();
            data.ACName = text.ACName;
            data.name = text.ACName;
            data.AuraConsumed = text.AuraConsumed;
            data.AuraDamageIncreasedPercent = text.AuraDamageIncreasedPercent;
            data.AuraDamageIncreasedPercent2 = text.AuraDamageIncreasedPercent2;
            data.AuraDamageIncreasedPercent3 = text.AuraDamageIncreasedPercent3;
            data.AuraDamageIncreasedPercent4 = text.AuraDamageIncreasedPercent4;
            data.AuraDamageIncreasedPercentPerStack = text.AuraDamageIncreasedPercentPerStack;
            data.AuraDamageIncreasedPercentPerStack2 = text.AuraDamageIncreasedPercentPerStack2;
            data.AuraDamageIncreasedPercentPerStack3 = text.AuraDamageIncreasedPercentPerStack3;
            data.AuraDamageIncreasedPercentPerStack4 = text.AuraDamageIncreasedPercentPerStack4;
            data.AuraDamageIncreasedPercentPerStackPerEnergy = text.AuraDamageIncreasedPercentPerStackPerEnergy;
            data.AuraDamageIncreasedPercentPerStackPerEnergy2 = text.AuraDamageIncreasedPercentPerStackPerEnergy2;
            data.AuraDamageIncreasedPercentPerStackPerEnergy3 = text.AuraDamageIncreasedPercentPerStackPerEnergy3;
            data.AuraDamageIncreasedPercentPerStackPerEnergy4 = text.AuraDamageIncreasedPercentPerStackPerEnergy4;
            data.AuraDamageIncreasedPerStack = text.AuraDamageIncreasedPerStack;
            data.AuraDamageIncreasedPerStack2 = text.AuraDamageIncreasedPerStack2;
            data.AuraDamageIncreasedPerStack3 = text.AuraDamageIncreasedPerStack3;
            data.AuraDamageIncreasedPerStack4 = text.AuraDamageIncreasedPerStack4;
            data.AuraDamageIncreasedTotal = text.AuraDamageIncreasedTotal;
            data.AuraDamageIncreasedTotal2 = text.AuraDamageIncreasedTotal2;
            data.AuraDamageIncreasedTotal3 = text.AuraDamageIncreasedTotal3;
            data.AuraDamageIncreasedTotal4 = text.AuraDamageIncreasedTotal4;
            data.AuraDamageType = (DamageType)ToData<DamageType>(text.AuraDamageType);
            data.AuraDamageType2 = (DamageType)ToData<DamageType>(text.AuraDamageType2);
            data.AuraDamageType3 = (DamageType)ToData<DamageType>(text.AuraDamageType3);
            data.AuraDamageType4 = (DamageType)ToData<DamageType>(text.AuraDamageType4);
            data.AuraDamageChargesBasedOnACCharges = medsAurasCursesSource.ContainsKey(text.AuraDamageChargesBasedOnACCharges) ? medsAurasCursesSource[text.AuraDamageChargesBasedOnACCharges] : (AuraCurseData)null;
            data.ConsumedDamageChargesBasedOnACCharges = medsAurasCursesSource.ContainsKey(text.ConsumedDamageChargesBasedOnACCharges) ? medsAurasCursesSource[text.ConsumedDamageChargesBasedOnACCharges] : (AuraCurseData)null;
            data.BlockChargesGainedPerStack = text.BlockChargesGainedPerStack;
            data.CardsDrawPerStack = text.CardsDrawPerStack;
            data.CharacterStatAbsolute = text.CharacterStatAbsolute;
            data.CharacterStatAbsoluteValue = text.CharacterStatAbsoluteValue;
            data.CharacterStatAbsoluteValuePerStack = text.CharacterStatAbsoluteValuePerStack;
            data.CharacterStatChargesMultiplierNeededForOne = text.CharacterStatChargesMultiplierNeededForOne;
            data.CharacterStatModified = (CharacterStat)ToData<CharacterStat>(text.CharacterStatModified);
            data.CharacterStatModifiedValue = text.CharacterStatModifiedValue;
            data.CharacterStatModifiedValuePerStack = text.CharacterStatModifiedValuePerStack;
            data.ChargesAuxNeedForOne1 = text.ChargesAuxNeedForOne1;
            data.ChargesAuxNeedForOne2 = text.ChargesAuxNeedForOne2;
            data.ChargesMultiplierDescription = text.ChargesMultiplierDescription;
            data.CombatlogShow = text.CombatlogShow;
            data.ConsumeAll = text.ConsumeAll;
            data.ConsumedAtCast = text.ConsumedAtCast;
            data.ConsumedAtRound = text.ConsumedAtRound;
            data.ConsumedAtRoundBegin = text.ConsumedAtRoundBegin;
            data.ConsumedAtTurn = text.ConsumedAtTurn;
            data.ConsumedAtTurnBegin = text.ConsumedAtTurnBegin;
            data.CursePreventedPerStack = text.CursePreventedPerStack;
            data.DamagePreventedPerStack = text.DamagePreventedPerStack;
            data.DamageReflectedConsumeCharges = text.DamageReflectedConsumeCharges;
            data.DamageReflectedType = (DamageType)ToData<DamageType>(text.DamageReflectedType);
            data.DamageSidesWhenConsumed = text.DamageSidesWhenConsumed;
            data.DamageSidesWhenConsumedPerCharge = text.DamageSidesWhenConsumedPerCharge;
            data.DamageTypeWhenConsumed = (DamageType)ToData<DamageType>(text.DamageTypeWhenConsumed);
            data.DamageWhenConsumed = text.DamageWhenConsumed;
            data.DamageWhenConsumedPerCharge = text.DamageWhenConsumedPerCharge;
            data.Description = text.Description;
            data.DieWhenConsumedAll = text.DieWhenConsumedAll;
            data.DisabledCardTypes = new CardType[text.DisabledCardTypes.Length];
            for (int a = 0; a < text.DisabledCardTypes.Length; a++)
                data.DisabledCardTypes[a] = (CardType)ToData<CardType>(text.DisabledCardTypes[a]);
            data.DoubleDamageIfCursesLessThan = text.DoubleDamageIfCursesLessThan;
            data.EffectTick = text.EffectTick;
            data.EffectTickSides = text.EffectTickSides;
            data.ExplodeAtStacks = text.ExplodeAtStacks;
            data.GainAuraCurseConsumption = medsAurasCursesSource.ContainsKey(text.GainAuraCurseConsumption) ? medsAurasCursesSource[text.GainAuraCurseConsumption] : (AuraCurseData)null;
            data.GainAuraCurseConsumption2 = medsAurasCursesSource.ContainsKey(text.GainAuraCurseConsumption2) ? medsAurasCursesSource[text.GainAuraCurseConsumption2] : (AuraCurseData)null;
            data.GainAuraCurseConsumptionPerCharge = text.GainAuraCurseConsumptionPerCharge;
            data.GainAuraCurseConsumptionPerCharge2 = text.GainAuraCurseConsumptionPerCharge2;
            data.GainCharges = text.GainCharges;
            data.GainChargesFromThisAuraCurse = medsAurasCursesSource.ContainsKey(text.GainChargesFromThisAuraCurse) ? medsAurasCursesSource[text.GainChargesFromThisAuraCurse] : (AuraCurseData)null;
            data.GainChargesFromThisAuraCurse2 = medsAurasCursesSource.ContainsKey(text.GainChargesFromThisAuraCurse2) ? medsAurasCursesSource[text.GainChargesFromThisAuraCurse2] : (AuraCurseData)null;
            data.HealAttackerConsumeCharges = text.HealAttackerConsumeCharges;
            data.HealAttackerPerStack = text.HealAttackerPerStack;
            data.HealDonePercent = text.HealDonePercent;
            data.HealDonePercentPerStack = text.HealDonePercentPerStack;
            data.HealDonePercentPerStackPerEnergy = text.HealDonePercentPerStackPerEnergy;
            data.HealDonePerStack = text.HealDonePerStack;
            data.HealReceivedTotal = text.HealReceivedTotal;
            data.HealSidesWhenConsumed = text.HealSidesWhenConsumed;
            data.HealSidesWhenConsumedPerCharge = text.HealSidesWhenConsumedPerCharge;
            data.HealWhenConsumed = text.HealWhenConsumed;
            data.HealWhenConsumedPerCharge = text.HealWhenConsumedPerCharge;
            data.IconShow = text.IconShow;
            data.Id = text.ID;
            data.IncreasedDamageReceivedType = (DamageType)ToData<DamageType>(text.IncreasedDamageReceivedType);
            data.IncreasedDamageReceivedType2 = (DamageType)ToData<DamageType>(text.IncreasedDamageReceivedType2);
            data.IncreasedDirectDamageChargesMultiplierNeededForOne = text.IncreasedDirectDamageChargesMultiplierNeededForOne;
            data.IncreasedDirectDamageChargesMultiplierNeededForOne2 = text.IncreasedDirectDamageChargesMultiplierNeededForOne2;
            data.IncreasedDirectDamageReceivedPerStack = text.IncreasedDirectDamageReceivedPerStack;
            data.IncreasedDirectDamageReceivedPerStack2 = text.IncreasedDirectDamageReceivedPerStack2;
            data.IncreasedDirectDamageReceivedPerTurn = text.IncreasedDirectDamageReceivedPerTurn;
            data.IncreasedDirectDamageReceivedPerTurn2 = text.IncreasedDirectDamageReceivedPerTurn2;
            data.IncreasedPercentDamageReceivedPerStack = text.IncreasedPercentDamageReceivedPerStack;
            data.IncreasedPercentDamageReceivedPerStack2 = text.IncreasedPercentDamageReceivedPerStack2;
            data.IncreasedPercentDamageReceivedPerTurn = text.IncreasedPercentDamageReceivedPerTurn;
            data.IncreasedPercentDamageReceivedPerTurn2 = text.IncreasedPercentDamageReceivedPerTurn2;
            data.Invulnerable = text.Invulnerable;
            data.IsAura = text.IsAura;
            data.MaxCharges = text.MaxCharges;
            data.MaxMadnessCharges = text.MaxMadnessCharges;
            data.ModifyCardCostPerChargeNeededForOne = text.ModifyCardCostPerChargeNeededForOne;
            data.NoRemoveBlockAtTurnEnd = text.NoRemoveBlockAtTurnEnd;
            data.Preventable = text.Preventable;
            data.PreventedAuraCurse = medsAurasCursesSource.ContainsKey(text.PreventedAuraCurse) ? medsAurasCursesSource[text.PreventedAuraCurse] : (AuraCurseData)null;
            data.PreventedAuraCurseStackPerStack = text.PreventedAuraCurseStackPerStack;
            data.PreventedDamagePerStack = text.PreventedDamagePerStack;
            data.PreventedDamageTypePerStack = (DamageType)ToData<DamageType>(text.PreventedDamageTypePerStack);
            data.PriorityOnConsumption = text.PriorityOnConsumption;
            data.ProduceDamageWhenConsumed = text.ProduceDamageWhenConsumed;
            data.ProduceHealWhenConsumed = text.ProduceHealWhenConsumed;
            data.Removable = text.Removable;
            data.RemoveAuraCurse = medsAurasCursesSource.ContainsKey(text.RemoveAuraCurse) ? medsAurasCursesSource[text.RemoveAuraCurse] : (AuraCurseData)null;
            data.RemoveAuraCurse2 = medsAurasCursesSource.ContainsKey(text.RemoveAuraCurse2) ? medsAurasCursesSource[text.RemoveAuraCurse2] : (AuraCurseData)null;
            data.ResistModified = (DamageType)ToData<DamageType>(text.ResistModified);
            data.ResistModified2 = (DamageType)ToData<DamageType>(text.ResistModified2);
            data.ResistModified3 = (DamageType)ToData<DamageType>(text.ResistModified3);
            data.ResistModifiedPercentagePerStack = text.ResistModifiedPercentagePerStack;
            data.ResistModifiedPercentagePerStack2 = text.ResistModifiedPercentagePerStack2;
            data.ResistModifiedPercentagePerStack3 = text.ResistModifiedPercentagePerStack3;
            data.ResistModifiedValue = text.ResistModifiedValue;
            data.ResistModifiedValue2 = text.ResistModifiedValue2;
            data.ResistModifiedValue3 = text.ResistModifiedValue3;
            data.RevealCardsPerCharge = text.RevealCardsPerCharge;
            data.SkipsNextTurn = text.SkipsNextTurn;
            data.Sound = GetAudio(text.Sound);
            data.Sprite = GetSprite(text.Sprite);
            try
            {
                foreach (TMP_SpriteAsset medsSAResistsIcons in Resources.FindObjectsOfTypeAll<TMP_SpriteAsset>())
                {
                    if (medsSAResistsIcons.name == "ResistsIcons")
                    { // #TMP #CRY #AURACURSE #TODO
                        GlyphMetrics medsGMFallback = new GlyphMetrics(data.Sprite.texture.width, data.Sprite.texture.height, 0f, 0f, 0f); // the 0fs are dodgy af :D idk how offset it should be yet, though!
                        GlyphRect medsGRFallback = new(0, 0, data.Sprite.texture.width, data.Sprite.texture.height);

                        TMP_SpriteGlyph medsSGFallback = new TMP_SpriteGlyph((uint)medsFallbackSpriteAsset.spriteGlyphTable.Count - 1, new GlyphMetrics(data.Sprite.texture.width, data.Sprite.texture.height, 0f, 0f, 0f), medsGRFallback, 1f, 0, data.Sprite);
                        /// medsFallbackSpriteAsset.spriteGlyphTable.Add(medsSGFallback);

                        TMP_SpriteCharacter medsSCFallback = new TMP_SpriteCharacter(65534U, medsSGFallback);
                        medsSCFallback.scale = 1f;
                        medsSCFallback.name = data.Sprite.name;
                        /// medsFallbackSpriteAsset.spriteCharacterTable.Add(medsSCFallback);
                        /// medsFallbackSpriteAsset.spriteSheet = medsSprite.texture;
                        /// medsFallbackSpriteAsset.fallbackSpriteAssets = new List<TMP_SpriteAsset>();
                        /// medsFallbackSpriteAsset.spriteInfoList = new List<TMP_Sprite>();
                        /// medsFallbackSpriteAsset.UpdateLookupTables();

                        // attach mod (fallback) spriteasset to AtO spriteasset
                        /// medsSAResistsIcons.fallbackSpriteAssets.Add(medsFallbackSpriteAsset);
                        /// 
                        medsSAResistsIcons.spriteGlyphTable.Add(medsSGFallback);
                        medsSAResistsIcons.spriteCharacterTable.Add(medsSCFallback);
                        medsSAResistsIcons.UpdateLookupTables();
                    }
                }
            }
            catch (Exception ex) { Log.LogError("Error adding TMP fallback sprite " + data.Sprite.name + ": " + ex.Message); }
            data.Stealth = text.Stealth;
            data.Taunt = text.Taunt;
            return data;
        }

        public static CardData ToData(CardDataText text)
        {
            CardData data = ScriptableObject.CreateInstance<CardData>();
            data.name = text.CardName;
            data.Id = text.ID;
            data.InternalId = text.ID;
            if (!text.medsCustomDescription.IsNullOrWhiteSpace())
                medsCustomCardDescriptions[data.Id] = text.medsCustomDescription;
            data.AcEnergyBonus = Globals.Instance.GetAuraCurseData(text.AcEnergyBonus);
            data.AcEnergyBonus2 = Globals.Instance.GetAuraCurseData(text.AcEnergyBonus2);
            data.AcEnergyBonusQuantity = text.AcEnergyBonusQuantity;
            data.AcEnergyBonus2Quantity = text.AcEnergyBonus2Quantity;
            data.AddCard = text.AddCard;
            data.AddCardChoose = text.AddCardChoose;
            data.AddCardCostTurn = text.AddCardCostTurn;
            data.AddCardFrom = (CardFrom)ToData<CardFrom>(text.AddCardFrom);
            data.AddCardId = text.AddCardId;
            if (text.AddCardList.Length > 0)
                medsSecondRunImport[text.ID] = text.AddCardList;
            data.AddCardPlace = (CardPlace)ToData<CardPlace>(text.AddCardPlace);
            data.AddCardReducedCost = text.AddCardReducedCost;
            data.AddCardType = (CardType)ToData<CardType>(text.AddCardType);
            data.AddCardTypeAux = new CardType[text.AddCardTypeAux.Length];
            for (int a = 0; a < text.AddCardTypeAux.Length; a++)
                data.AddCardTypeAux[a] = (CardType)ToData<CardType>(text.AddCardTypeAux[a]);
            data.AddCardVanish = text.AddCardVanish;
            data.Aura = Globals.Instance.GetAuraCurseData(text.Aura);
            data.Aura2 = Globals.Instance.GetAuraCurseData(text.Aura2);
            data.Aura3 = Globals.Instance.GetAuraCurseData(text.Aura3);
            data.AuraCharges = text.AuraCharges;
            data.AuraChargesSpecialValue1 = text.AuraChargesSpecialValue1;
            data.AuraChargesSpecialValue2 = text.AuraChargesSpecialValue2;
            data.AuraChargesSpecialValueGlobal = text.AuraChargesSpecialValueGlobal;
            data.AuraCharges2 = text.AuraCharges2;
            data.AuraCharges2SpecialValue1 = text.AuraCharges2SpecialValue1;
            data.AuraCharges2SpecialValue2 = text.AuraCharges2SpecialValue2;
            data.AuraCharges2SpecialValueGlobal = text.AuraCharges2SpecialValueGlobal;
            data.AuraCharges3 = text.AuraCharges3;
            data.AuraCharges3SpecialValue1 = text.AuraCharges3SpecialValue1;
            data.AuraCharges3SpecialValue2 = text.AuraCharges3SpecialValue2;
            data.AuraCharges3SpecialValueGlobal = text.AuraCharges3SpecialValueGlobal;
            data.AuraSelf = Globals.Instance.GetAuraCurseData(text.AuraSelf);
            data.AuraSelf2 = Globals.Instance.GetAuraCurseData(text.AuraSelf2);
            data.AuraSelf3 = Globals.Instance.GetAuraCurseData(text.AuraSelf3);
            data.AutoplayDraw = text.AutoplayDraw;
            data.AutoplayEndTurn = text.AutoplayEndTurn;
            data.BaseCard = text.BaseCard;
            data.CardClass = (CardClass)ToData<CardClass>(text.CardClass);
            data.CardName = text.CardName;
            data.CardNumber = text.CardNumber;
            data.CardRarity = (CardRarity)ToData<CardRarity>(text.CardRarity);
            data.CardType = (CardType)ToData<CardType>(text.CardType);
            data.CardTypeAux = new CardType[text.CardTypeAux.Length];
            for (int a = 0; a < text.CardTypeAux.Length; a++)
                data.CardTypeAux[a] = (CardType)ToData<CardType>(text.CardTypeAux[a]);
            data.CardUpgraded = (CardUpgraded)ToData<CardUpgraded>(text.CardUpgraded);
            data.Corrupted = text.Corrupted;
            data.Curse = Globals.Instance.GetAuraCurseData(text.Curse);
            data.Curse2 = Globals.Instance.GetAuraCurseData(text.Curse2);
            data.Curse3 = Globals.Instance.GetAuraCurseData(text.Curse3);
            data.CurseCharges = text.CurseCharges;
            data.CurseChargesSpecialValue1 = text.CurseChargesSpecialValue1;
            data.CurseChargesSpecialValue2 = text.CurseChargesSpecialValue2;
            data.CurseChargesSpecialValueGlobal = text.CurseChargesSpecialValueGlobal;
            data.CurseCharges2 = text.CurseCharges2;
            data.CurseCharges2SpecialValue1 = text.CurseCharges2SpecialValue1;
            data.CurseCharges2SpecialValue2 = text.CurseCharges2SpecialValue2;
            data.CurseCharges2SpecialValueGlobal = text.CurseCharges2SpecialValueGlobal;
            data.CurseCharges3 = text.CurseCharges3;
            data.CurseCharges3SpecialValue1 = text.CurseCharges3SpecialValue1;
            data.CurseCharges3SpecialValue2 = text.CurseCharges3SpecialValue2;
            data.CurseCharges3SpecialValueGlobal = text.CurseCharges3SpecialValueGlobal;
            data.CurseSelf = Globals.Instance.GetAuraCurseData(text.CurseSelf);
            data.CurseSelf2 = Globals.Instance.GetAuraCurseData(text.CurseSelf2);
            data.CurseSelf3 = Globals.Instance.GetAuraCurseData(text.CurseSelf3);
            data.Damage = text.Damage;
            data.DamageSpecialValue1 = text.DamageSpecialValue1;
            data.DamageSpecialValue2 = text.DamageSpecialValue2;
            data.DamageSpecialValueGlobal = text.DamageSpecialValueGlobal;
            data.Damage2 = text.Damage2;
            data.Damage2SpecialValue1 = text.Damage2SpecialValue1;
            data.Damage2SpecialValue2 = text.Damage2SpecialValue2;
            data.Damage2SpecialValueGlobal = text.Damage2SpecialValueGlobal;
            data.DamageEnergyBonus = text.DamageEnergyBonus;
            data.DamageSelf = text.DamageSelf;
            data.DamageSelf2 = text.DamageSelf2;
            data.DamageSides = text.DamageSides;
            data.DamageSides2 = text.DamageSides2;
            data.DamageType = (DamageType)ToData<DamageType>(text.DamageType);
            data.DamageType2 = (DamageType)ToData<DamageType>(text.DamageType2);
            data.Description = ""; // text.Description; // isn't this generated in-game? SetDescriptionNew #TODO: probably remove it?
            // data.DescriptionID = text.descriptionid
            data.DiscardCard = text.DiscardCard;
            data.DiscardCardAutomatic = text.DiscardCardAutomatic;
            data.DiscardCardPlace = (CardPlace)ToData<CardPlace>(text.DiscardCardPlace);
            data.DiscardCardType = (CardType)ToData<CardType>(text.DiscardCardType);
            data.DiscardCardTypeAux = new CardType[text.DiscardCardTypeAux.Length];
            for (int a = 0; a < text.DiscardCardTypeAux.Length; a++)
                data.DiscardCardTypeAux[a] = (CardType)ToData<CardType>(text.DiscardCardTypeAux[a]);
            data.DispelAuras = text.DispelAuras;
            data.DrawCard = text.DrawCard;
            data.EffectCastCenter = text.EffectCastCenter;
            data.EffectCaster = text.EffectCaster;
            data.EffectCasterRepeat = text.EffectCasterRepeat;
            data.EffectPostCastDelay = text.EffectPostCastDelay;
            data.EffectPostTargetDelay = text.EffectPostTargetDelay;
            data.EffectPreAction = text.EffectPreAction;
            data.EffectRepeat = text.EffectRepeat;
            data.EffectRepeatDelay = text.EffectRepeatDelay;
            data.EffectRepeatEnergyBonus = text.EffectRepeatEnergyBonus;
            data.EffectRepeatMaxBonus = text.EffectRepeatMaxBonus;
            data.EffectRepeatModificator = text.EffectRepeatModificator;
            data.EffectRepeatTarget = (EffectRepeatTarget)ToData<EffectRepeatTarget>(text.EffectRepeatTarget);
            data.EffectRequired = text.EffectRequired;
            data.EffectTarget = text.EffectTarget;
            data.EffectTrail = text.EffectTrail;
            data.EffectTrailAngle = (EffectTrailAngle)ToData<EffectTrailAngle>(text.EffectTrailAngle);
            data.EffectTrailRepeat = text.EffectTrailRepeat;
            data.EffectTrailSpeed = text.EffectTrailSpeed;
            data.EndTurn = text.EndTurn;
            data.EnergyCost = text.EnergyCost;
            data.EnergyCostForShow = text.EnergyCostForShow;
            data.EnergyRecharge = text.EnergyRecharge;
            data.EnergyReductionPermanent = text.EnergyReductionPermanent;
            data.EnergyReductionTemporal = text.EnergyReductionTemporal;
            data.EnergyReductionToZeroPermanent = text.EnergyReductionToZeroPermanent;
            data.EnergyReductionToZeroTemporal = text.EnergyReductionToZeroTemporal;
            data.ExhaustCounter = text.ExhaustCounter;
            data.FlipSprite = text.FlipSprite;
            data.Fluff = text.Fluff;
            data.FluffPercent = text.FluffPercent;
            data.GoldGainQuantity = text.GoldGainQuantity;
            data.Heal = text.Heal;
            data.HealAuraCurseName = Globals.Instance.GetAuraCurseData(text.HealAuraCurseName);
            data.HealAuraCurseName2 = Globals.Instance.GetAuraCurseData(text.HealAuraCurseName2);
            data.HealAuraCurseName3 = Globals.Instance.GetAuraCurseData(text.HealAuraCurseName3);
            data.HealAuraCurseName4 = Globals.Instance.GetAuraCurseData(text.HealAuraCurseName4);
            data.HealAuraCurseSelf = Globals.Instance.GetAuraCurseData(text.HealAuraCurseSelf);
            data.HealCurses = text.HealCurses;
            data.HealEnergyBonus = text.HealEnergyBonus;
            data.HealSelf = text.HealSelf;
            data.HealSelfPerDamageDonePercent = text.HealSelfPerDamageDonePercent;
            data.HealSelfSpecialValue1 = text.HealSelfSpecialValue1;
            data.HealSelfSpecialValue2 = text.HealSelfSpecialValue2;
            data.HealSelfSpecialValueGlobal = text.HealSelfSpecialValueGlobal;
            data.HealSides = text.HealSides;
            data.HealSpecialValue1 = text.HealSpecialValue1;
            data.HealSpecialValue2 = text.HealSpecialValue2;
            data.HealSpecialValueGlobal = text.HealSpecialValueGlobal;
            data.IgnoreBlock = text.IgnoreBlock;
            data.IgnoreBlock2 = text.IgnoreBlock2;
            data.IncreaseAuras = text.IncreaseAuras;
            data.IncreaseCurses = text.IncreaseCurses;
            data.Innate = text.Innate;
            data.IsPetAttack = text.IsPetAttack;
            data.IsPetCast = text.IsPetCast;
            data.KillPet = text.KillPet;
            data.Lazy = text.Lazy;
            data.LookCards = text.LookCards;
            data.LookCardsDiscardUpTo = text.LookCardsDiscardUpTo;
            data.LookCardsVanishUpTo = text.LookCardsVanishUpTo;
            data.MaxInDeck = text.MaxInDeck;
            data.ModifiedByTrait = text.ModifiedByTrait;
            data.EnergyRechargeSpecialValueGlobal = text.EnergyRechargeSpecialValueGlobal;
            data.MoveToCenter = text.MoveToCenter;
            data.OnlyInWeekly = text.OnlyInWeekly;
            data.Playable = text.Playable;
            data.PullTarget = text.PullTarget;
            data.PushTarget = text.PushTarget;
            data.ReduceAuras = text.ReduceAuras;
            data.ReduceCurses = text.ReduceCurses;
            data.RelatedCard = text.RelatedCard;
            data.RelatedCard2 = text.RelatedCard2;
            data.RelatedCard3 = text.RelatedCard3;
            data.SelfHealthLoss = text.SelfHealthLoss;
            data.SelfHealthLossSpecialGlobal = text.SelfHealthLossSpecialGlobal;
            data.SelfHealthLossSpecialValue1 = text.SelfHealthLossSpecialValue1;
            data.SelfHealthLossSpecialValue2 = text.SelfHealthLossSpecialValue2;
            data.ShardsGainQuantity = text.ShardsGainQuantity;
            data.ShowInTome = text.ShowInTome;
            data.Sku = text.Sku;
            data.Sound = GetAudio(text.Sound);
            data.SoundPreAction = GetAudio(text.SoundPreAction);
            data.SoundPreActionFemale = GetAudio(text.SoundPreActionFemale);
            data.SpecialAuraCurseName1 = Globals.Instance.GetAuraCurseData(text.SpecialAuraCurseName1);
            data.SpecialAuraCurseName2 = Globals.Instance.GetAuraCurseData(text.SpecialAuraCurseName2);
            data.SpecialAuraCurseNameGlobal = Globals.Instance.GetAuraCurseData(text.SpecialAuraCurseNameGlobal);
            data.SpecialValue1 = (CardSpecialValue)ToData<CardSpecialValue>(text.SpecialValue1);
            data.SpecialValue2 = (CardSpecialValue)ToData<CardSpecialValue>(text.SpecialValue2);
            data.SpecialValueGlobal = (CardSpecialValue)ToData<CardSpecialValue>(text.SpecialValueGlobal);
            data.SpecialValueModifier1 = text.SpecialValueModifier1;
            data.SpecialValueModifier2 = text.SpecialValueModifier2;
            data.SpecialValueModifierGlobal = text.SpecialValueModifierGlobal;
            data.Sprite = GetSprite(text.Sprite);
            data.Starter = text.Starter;
            data.StealAuras = text.StealAuras;
            data.SummonAura = Globals.Instance.GetAuraCurseData(text.SummonAura);
            data.SummonAura2 = Globals.Instance.GetAuraCurseData(text.SummonAura2);
            data.SummonAura3 = Globals.Instance.GetAuraCurseData(text.SummonAura3);
            data.SummonAuraCharges = text.SummonAuraCharges;
            data.SummonAuraCharges2 = text.SummonAuraCharges2;
            data.SummonAuraCharges3 = text.SummonAuraCharges3;
            data.TargetPosition = (CardTargetPosition)ToData<CardTargetPosition>(text.TargetPosition);
            data.TargetSide = (CardTargetSide)ToData<CardTargetSide>(text.TargetSide);
            data.TargetType = (CardTargetType)ToData<CardTargetType>(text.TargetType);
            data.TransferCurses = text.TransferCurses;
            data.UpgradedFrom = text.UpgradedFrom;
            data.UpgradesTo1 = text.UpgradesTo1;
            data.UpgradesTo2 = text.UpgradesTo2;
            data.UpgradesToRare = (CardData)null;
            if (!String.IsNullOrWhiteSpace(text.UpgradesToRare))
                medsSecondRunImport2[text.ID] = text.UpgradesToRare;
            data.Vanish = text.Vanish;
            data.Visible = text.Visible;
            data.Item = (ItemData)null;
            data.ItemEnchantment = (ItemData)null;
            if (!String.IsNullOrWhiteSpace(text.Item))
                medsCardsNeedingItems[text.ID] = text.Item;
            if (!String.IsNullOrWhiteSpace(text.ItemEnchantment))
                medsCardsNeedingItemEnchants[text.ID] = text.ItemEnchantment;
            data.SummonUnitNum = text.SummonUnitNum;
            if (!String.IsNullOrWhiteSpace(text.SummonUnit))
                medsCardsNeedingSummonUnits[text.ID] = text.SummonUnit;
            data.SelfKillHiddenSeconds = text.SelfKillHiddenSeconds;
            data.PetFront = text.PetFront;
            data.PetInvert = text.PetInvert;
            data.PetModel = GetGO(text.PetModel);
            data.PetOffset = ToData(text.PetOffset);
            data.PetSize = ToData(text.PetSize);
            return data;
        }
        public static TraitData ToData(TraitDataText text)
        {
            TraitData data = ScriptableObject.CreateInstance<TraitData>();
            data.Id = text.ID;
            data.name = text.ID;
            data.Activation = (EventActivation)ToData<EventActivation>(text.Activation);
            data.AuracurseBonus1 = Globals.Instance.GetAuraCurseData(text.AuraCurseBonus1);
            data.AuracurseBonus2 = Globals.Instance.GetAuraCurseData(text.AuraCurseBonus2);
            data.AuracurseBonus3 = Globals.Instance.GetAuraCurseData(text.AuraCurseBonus3);
            data.AuracurseBonusValue1 = text.AuraCurseBonusValue1;
            data.AuracurseBonusValue2 = text.AuraCurseBonusValue2;
            data.AuracurseBonusValue3 = text.AuraCurseBonusValue3;
            data.AuracurseImmune1 = text.AuraCurseImmune1;
            data.AuracurseImmune2 = text.AuraCurseImmune2;
            data.AuracurseImmune3 = text.AuraCurseImmune3;
            data.CharacterStatModified = (CharacterStat)ToData<CharacterStat>(text.CharacterStatModified);
            data.CharacterStatModifiedValue = text.CharacterStatModifiedValue;
            data.DamageBonusFlat = (DamageType)ToData<DamageType>(text.DamageBonusFlat);
            data.DamageBonusFlat2 = (DamageType)ToData<DamageType>(text.DamageBonusFlat2);
            data.DamageBonusFlat3 = (DamageType)ToData<DamageType>(text.DamageBonusFlat3);
            data.DamageBonusFlatValue = text.DamageBonusFlatValue;
            data.DamageBonusFlatValue2 = text.DamageBonusFlatValue2;
            data.DamageBonusFlatValue3 = text.DamageBonusFlatValue3;
            data.DamageBonusPercent = (DamageType)ToData<DamageType>(text.DamageBonusPercent);
            data.DamageBonusPercent2 = (DamageType)ToData<DamageType>(text.DamageBonusPercent2);
            data.DamageBonusPercent3 = (DamageType)ToData<DamageType>(text.DamageBonusPercent3);
            data.DamageBonusPercentValue = text.DamageBonusPercentValue;
            data.DamageBonusPercentValue2 = text.DamageBonusPercentValue2;
            data.DamageBonusPercentValue3 = text.DamageBonusPercentValue3;
            data.Description = text.Description;
            data.HealFlatBonus = text.HealFlatBonus;
            data.HealPercentBonus = text.HealPercentBonus;
            data.HealReceivedFlatBonus = text.HealReceivedFlatBonus;
            data.HealReceivedPercentBonus = text.HealReceivedPercentBonus;
            data.ResistModified1 = (DamageType)ToData<DamageType>(text.ResistModified1);
            data.ResistModified2 = (DamageType)ToData<DamageType>(text.ResistModified2);
            data.ResistModified3 = (DamageType)ToData<DamageType>(text.ResistModified3);
            data.ResistModifiedValue1 = text.ResistModifiedValue1;
            data.ResistModifiedValue2 = text.ResistModifiedValue2;
            data.ResistModifiedValue3 = text.ResistModifiedValue3;
            data.TimesPerRound = text.TimesPerRound;
            data.TimesPerTurn = text.TimesPerTurn;
            data.TraitCard = GetCard(text.TraitCard);
            data.TraitCardForAllHeroes = GetCard(text.TraitCardForAllHeroes);
            data.TraitName = text.TraitName;
            return data;
        }
        public static HeroCards ToData(HeroCardsText text)
        {
            HeroCards data = new();
            if (medsCardsSource.ContainsKey(text.Card))
            {
                data.UnitsInDeck = text.UnitsInDeck;
                data.Card = medsCardsSource[text.Card];
            }
            return data;
        }

        public static SubClassData ToData(SubClassDataText text)
        {
            //LogDebug("TEST 0");
            SubClassData data = UnityEngine.Object.Instantiate<SubClassData>(medsSubClassesSource["mercenary"]);
            //SubClassData data = ScriptableObject.CreateInstance<SubClassData>();
            //LogDebug("TEST 1");
            data.Id = text.ID;
            data.name = text.ID;
            data.ActionSound = GetAudio(text.ActionSound);
            Traverse.Create(data).Field("hitSound").SetValue(GetAudio(text.HitSound)); // with the sound rework, hitSound has no Setter, so need to access the private variable
            
            //data.GameObjectAnimated = GetGO(text.GameObjectAnimated);
            //data.ExpansionCharacter = false;
            data.OrderInList = text.OrderInList;
            data.Blocked = false;
            data.Cards = new HeroCards[text.Cards.Length];
            LogDebug("TEST 2");
            for (int a = 0; a < text.Cards.Length; a++)
                data.Cards[a] = ToData(JsonUtility.FromJson<HeroCardsText>(text.Cards[a]));
            if (medsPackDataSource.ContainsKey(text.ChallengePack0))
                data.ChallengePack0 = medsPackDataSource[text.ChallengePack0];
            else
                data.ChallengePack0 = (PackData)null;
            if (medsPackDataSource.ContainsKey(text.ChallengePack1))
                data.ChallengePack1 = medsPackDataSource[text.ChallengePack1];
            else
                data.ChallengePack1 = (PackData)null;
            if (medsPackDataSource.ContainsKey(text.ChallengePack2))
                data.ChallengePack2 = medsPackDataSource[text.ChallengePack2];
            else
                data.ChallengePack2 = (PackData)null;
            if (medsPackDataSource.ContainsKey(text.ChallengePack3))
                data.ChallengePack3 = medsPackDataSource[text.ChallengePack3];
            else
                data.ChallengePack3 = (PackData)null;
            if (medsPackDataSource.ContainsKey(text.ChallengePack4))
                data.ChallengePack4 = medsPackDataSource[text.ChallengePack4];
            else
                data.ChallengePack4 = (PackData)null;
            if (medsPackDataSource.ContainsKey(text.ChallengePack5))
                data.ChallengePack5 = medsPackDataSource[text.ChallengePack5];
            else
                data.ChallengePack5 = (PackData)null;
            if (medsPackDataSource.ContainsKey(text.ChallengePack6))
                data.ChallengePack6 = medsPackDataSource[text.ChallengePack6];
            else
                data.ChallengePack6 = (PackData)null;
            data.CharacterDescription = text.CharacterDescription;
            data.CharacterDescriptionStrength = text.CharacterDescriptionStrength;
            data.CharacterName = text.CharacterName;
            data.Energy = text.Energy;
            data.EnergyTurn = text.EnergyTurn;
            //data.ExpansionCharacter = text.ExpansionCharacter;
            data.Female = text.Female;
            data.FluffOffsetX = text.FluffOffsetX; // #CHARACTERSPRITES
            data.FluffOffsetY = text.FluffOffsetY; // #CHARACTERSPRITES
            data.HeroClass = (HeroClass)ToData<HeroClass>(text.HeroClass);
            data.HeroClassSecondary = (HeroClass)ToData<HeroClass>(text.HeroClassSecondary);
            data.Hp = text.HP;
            data.Item = (CardData)null;
            if (medsCardsSource.ContainsKey(text.Item))
                data.Item = medsCardsSource[text.Item];
            data.MainCharacter = true;
            data.MaxHp = text.MaxHP;
            data.ResistSlashing = text.ResistSlashing;
            data.ResistBlunt = text.ResistBlunt;
            data.ResistPiercing = text.ResistPiercing;
            data.ResistFire = text.ResistFire;
            data.ResistCold = text.ResistCold;
            data.ResistLightning = text.ResistLightning;
            data.ResistHoly = text.ResistHoly;
            data.ResistShadow = text.ResistShadow;
            data.ResistMind = text.ResistMind;
            data.Speed = text.Speed;
            //data.Sprite = GetSprite(text.Sprite, "positionTop"); //#charspriteborder
            //data.SpriteBorder = GetSprite(text.SpriteBorder, "positionTop");
            data.SpriteBorderLocked = GetSprite(text.SpriteBorderLocked, "positionTop");
            //data.SpriteBorderSmall = GetSprite(text.SpriteBorderSmall, "positionTop");
            //data.SpritePortrait = GetSprite(text.SpritePortrait);
            //data.SpriteSpeed = GetSprite(text.SpriteSpeed);
            data.StickerAngry = GetSprite(text.StickerAngry);
            data.StickerBase = GetSprite(text.StickerBase);
            data.StickerIndiferent = GetSprite(text.StickerIndifferent);
            data.StickerLove = GetSprite(text.StickerLove);
            data.StickerSurprise = GetSprite(text.StickerSurprise);
            data.StickerOffsetX = text.StickerOffsetX;
            data.SubClassName = text.SubclassName;
            data.Trait0 = (TraitData)null;
            data.Trait1A = (TraitData)null;
            data.Trait1B = (TraitData)null;
            data.Trait2A = (TraitData)null;
            data.Trait2B = (TraitData)null;
            data.Trait3A = (TraitData)null;
            data.Trait3B = (TraitData)null;
            data.Trait4A = (TraitData)null;
            data.Trait4B = (TraitData)null;
            data.Trait1ACard = (CardData)null;
            data.Trait1BCard = (CardData)null;
            data.Trait3ACard = (CardData)null;
            data.Trait3BCard = (CardData)null;
            if (medsTraitsSource.ContainsKey(text.Trait0))
                data.Trait0 = medsTraitsSource[text.Trait0];
            if (medsTraitsSource.ContainsKey(text.Trait1A))
                data.Trait1A = medsTraitsSource[text.Trait1A];
            if (medsTraitsSource.ContainsKey(text.Trait1B))
                data.Trait1B = medsTraitsSource[text.Trait1B];
            if (medsTraitsSource.ContainsKey(text.Trait2A))
                data.Trait2A = medsTraitsSource[text.Trait2A];
            if (medsTraitsSource.ContainsKey(text.Trait2B))
                data.Trait2B = medsTraitsSource[text.Trait2B];
            if (medsTraitsSource.ContainsKey(text.Trait3A))
                data.Trait3A = medsTraitsSource[text.Trait3A];
            if (medsTraitsSource.ContainsKey(text.Trait3B))
                data.Trait3B = medsTraitsSource[text.Trait3B];
            if (medsTraitsSource.ContainsKey(text.Trait4A))
                data.Trait4A = medsTraitsSource[text.Trait4A];
            if (medsTraitsSource.ContainsKey(text.Trait4B))
                data.Trait4B = medsTraitsSource[text.Trait4B];
            if (medsCardsSource.ContainsKey(text.Trait1A))
                data.Trait1ACard = medsCardsSource[text.Trait1A];
            if (medsCardsSource.ContainsKey(text.Trait1B))
                data.Trait1BCard = medsCardsSource[text.Trait1B];
            if (medsCardsSource.ContainsKey(text.Trait3A))
                data.Trait3ACard = medsCardsSource[text.Trait3A];
            if (medsCardsSource.ContainsKey(text.Trait3B))
                data.Trait3BCard = medsCardsSource[text.Trait3B];
            medsTexts[data.Id] = data.SubClassName;
            LogInfo(data.SubClassName + " AutoUnlock " + text.AutoUnlock);
            if (text.AutoUnlock)
                medsAutoUnlockHeroes.Add(text.ID);
                List<string> unlockedHeroes = (List<string>) Traverse.Create(PlayerManager.Instance).Field("unlockedHeroes").GetValue();
                unlockedHeroes.Add(data.SubClassName.ToLower());
                Traverse.Create(PlayerManager.Instance).Field("unlockedHeroes").SetValue(unlockedHeroes);

            data.InitialUnlock = text.AutoUnlock; // #XMAS
            data.SourceCharacterName = text.SourceCharacterName; // #XMAS
            return data;
        }

        public static PerkData ToData(PerkDataText text)
        {
            PerkData data = ScriptableObject.CreateInstance<PerkData>();
            data.name = text.ID;
            data.AdditionalCurrency = text.AdditionalCurrency;
            data.AdditionalShards = text.AdditionalShards;
            data.AuracurseBonus = Globals.Instance.GetAuraCurseData(text.AuraCurseBonus);
            data.AuracurseBonusValue = text.AuraCurseBonusValue;
            data.CardClass = (CardClass)ToData<CardClass>(text.CardClass);
            data.CustomDescription = text.CustomDescription;
            data.DamageFlatBonus = (DamageType)ToData<DamageType>(text.DamageFlatBonus);
            data.DamageFlatBonusValue = text.DamageFlatBonusValue;
            data.EnergyBegin = text.EnergyBegin;
            data.HealQuantity = text.HealQuantity;
            data.Icon = GetSprite(text.Icon);
            data.IconTextValue = text.IconTextValue;
            data.Id = text.ID;
            data.Level = text.Level;
            data.MainPerk = text.MainPerk;
            data.MaxHealth = text.MaxHealth;
            data.ObeliskPerk = text.ObeliskPerk;
            data.ResistModified = (DamageType)ToData<DamageType>(text.ResistModified);
            data.ResistModifiedValue = text.ResistModifiedValue;
            data.Row = text.Row;
            data.SpeedQuantity = text.SpeedQuantity;
            return data;
        }

        public static AICards ToData(AICardsText text)
        {
            AICards data = new();
            data.AddCardRound = text.AddCardRound;
            data.AuracurseCastIf = Globals.Instance.GetAuraCurseData(text.AuraCurseCastIf);
            if (medsCardsSource.ContainsKey(text.Card))
                data.Card = medsCardsSource[text.Card];
            else
                return null;
            data.OnlyCastIf = (OnlyCastIf)ToData<OnlyCastIf>(text.OnlyCastIf);
            data.PercentToCast = text.PercentToCast;
            data.Priority = text.Priority;
            data.TargetCast = (TargetCast)ToData<TargetCast>(text.TargetCast);
            data.UnitsInDeck = text.UnitsInDeck;
            data.ValueCastIf = text.ValueCastIf;
            return data;
        }

        public static NPCData ToData(NPCDataText text)
        {
            NPCData data = ScriptableObject.CreateInstance<NPCData>();
            data.name = text.ID;
            // set ID with reflections
            data.GetType().GetField("id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, text.ID);
            // set HP with reflections
            data.GetType().GetField("hp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, text.HP);
            // set Speed with reflections
            data.GetType().GetField("speed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, text.Speed);
            // set Sprite with reflections
            data.GetType().GetField("sprite", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, GetSprite(text.Sprite));
            // set SpriteSpeed with reflections
            data.GetType().GetField("spriteSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, GetSprite(text.SpriteSpeed));
            // do the rest, thankfully without :)
            data.AICards = new AICards[text.AICards.Length];
            for (int a = 0; a < text.AICards.Length; a++)
            {
                data.AICards[a] = ToData(JsonUtility.FromJson<AICardsText>(text.AICards[a]));
                if (data.AICards[a] == null)
                    Log.LogError("POTENTIALLY FATAL ERROR: unable to load NPC " + text.ID + " AICards: " + text.AICards[a]);
            }
            data.AuracurseImmune = new();
            for (int a = 0; a < text.AuraCurseImmune.Length; a++)
                if (!(data.AuracurseImmune.Contains(text.AuraCurseImmune[a])))
                    data.AuracurseImmune.Add(text.AuraCurseImmune[a]);
            data.BigModel = text.BigModel;
            data.CardsInHand = text.CardsInHand;
            data.Description = text.Description;
            data.Difficulty = text.Difficulty;
            data.Energy = text.Energy;
            data.EnergyTurn = text.EnergyTurn;
            data.ExperienceReward = text.ExperienceReward;
            data.Female = text.Female;
            data.FinishCombatOnDead = text.FinishCombatOnDead;
            data.FluffOffsetX = text.FluffOffsetX;
            data.FluffOffsetY = text.FluffOffsetY;
            data.GameObjectAnimated = GetGO(text.GameObjectAnimated);
            Traverse.Create(data).Field("hitSound").SetValue(GetAudio(text.HitSound)); // with the sound rework, hitSound has no Setter, so need to access the private variable
            data.GoldReward = text.GoldReward;
            data.IsBoss = text.IsBoss;
            data.IsNamed = text.IsNamed;
            data.NPCName = text.NPCName;
            medsTexts["monsters_" + text.ID + "_name"] = text.NPCName;
            data.PosBottom = text.PosBottom;
            data.PreferredPosition = (CardTargetPosition)ToData<CardTargetPosition>(text.PreferredPosition);
            data.ResistBlunt = text.ResistBlunt;
            data.ResistCold = text.ResistCold;
            data.ResistFire = text.ResistFire;
            data.ResistHoly = text.ResistHoly;
            data.ResistLightning = text.ResistLightning;
            data.ResistMind = text.ResistMind;
            data.ResistPiercing = text.ResistPiercing;
            data.ResistShadow = text.ResistShadow;
            data.ResistSlashing = text.ResistSlashing;
            data.ScriptableObjectName = text.ScriptableObjectName;
            data.SpritePortrait = GetSprite(text.SpritePortrait);
            data.TierMob = (CombatTier)ToData<CombatTier>(text.TierMob);
            data.TierReward = GetTierReward(text.TierReward);
            // store variants
            medsSecondRunImport[text.ID] = new string[4] { text.BaseMonster, text.HellModeMob, text.NgPlusMob, text.UpgradedMob };
            return data;
        }
        public static NodeData ToData(NodeDataText text)
        {
            NodeData data = ScriptableObject.CreateInstance<NodeData>();
            data.name = text.NodeId;
            data.CombatPercent = text.CombatPercent;
            data.Description = text.Description;
            data.DisableCorruption = text.DisableCorruption;
            data.DisableRandom = text.DisableRandom;
            data.EventPercent = text.EventPercent;
            data.ExistsPercent = text.ExistsPercent;
            data.ExistsSku = text.ExistsSku;
            data.GoToTown = text.GoToTown;
            data.NodeBackgroundImg = GetSprite(text.NodeBackgroundImg);
            data.NodeCombat = new CombatData[text.NodeCombat.Length];
            for (int a = 0; a < text.NodeCombat.Length; a++)
                data.NodeCombat[a] = Globals.Instance.GetCombatData(text.NodeCombat[a]);
            data.NodeCombatTier = (CombatTier)ToData<CombatTier>(text.NodeCombatTier);
            // data.NodeEvent done later, after events load
            data.NodeEventPercent = text.NodeEventPercent;
            data.NodeEventPriority = text.NodeEventPriority;
            data.NodeEventTier = (CombatTier)ToData<CombatTier>(text.NodeEventTier);
            data.NodeGround = (NodeGround)ToData<NodeGround>(text.NodeGround);
            data.NodeId = text.NodeId;
            data.NodeName = text.NodeName;
            medsTexts["nodes_" + text.NodeId + "_name"] = text.NodeName;
            data.NodeRequirement = GetEventRequirement(text.NodeRequirement);
            //LogDebug("NODES CONNECTED FOR " + text.NodeId + ": " + String.Join(",", text.NodesConnected));
            if (text.NodesConnected.Length > 0)
                medsSecondRunNodesConnected[text.NodeId] = text.NodesConnected;
            if (text.NodesConnectedRequirement.Length > 0)
                medsSecondRunImport[text.NodeId] = text.NodesConnectedRequirement;
            data.NodeZone = medsZoneDataSource.ContainsKey(text.NodeZone.ToLower()) ? medsZoneDataSource[text.NodeZone.ToLower()] : (ZoneData)null;
            data.TravelDestination = text.TravelDestination;
            data.VisibleIfNotRequirement = text.VisibleIfNotRequirement;
            data.SourceNodeName = text.SourceNodeName; //#XMAS 
            return data;
        }
        public static NodesConnectedRequirement ToData(NodesConnectedRequirementText text)
        {
            NodesConnectedRequirement data = new();
            data.NodeData = GetNode(text.NodeData);
            data.ConectionRequeriment = GetEventRequirement(text.ConnectionRequirement);
            data.ConectionIfNotNode = GetNode(text.ConnectionIfNotNode);
            return data;
        }
        public static LootData ToData(LootDataText text)
        {
            LootData data = ScriptableObject.CreateInstance<LootData>();
            data.name = text.ID;
            data.DefaultPercentEpic = text.DefaultPercentEpic;
            data.DefaultPercentMythic = text.DefaultPercentMythic;
            data.DefaultPercentRare = text.DefaultPercentRare;
            data.DefaultPercentUncommon = text.DefaultPercentUncommon;
            data.GoldQuantity = text.GoldQuantity;
            data.Id = text.ID;
            data.LootItemTable = new LootItem[text.LootItemTable.Length];
            for (int a = 0; a < text.LootItemTable.Length; a++)
                data.LootItemTable[a] = ToData(JsonUtility.FromJson<LootItemText>(text.LootItemTable[a]));
            data.NumItems = text.NumItems;
            data.ShadyModel = GetGO(text.ShadyModel); // #XMAS
            data.ShadyScaleX = text.ShadyScaleX; // #XMAS
            data.ShadyScaleY = text.ShadyScaleY; // #XMAS
            data.ShadyOffsetX = text.ShadyOffsetX; // #XMAS
            data.ShadyOffsetY = text.ShadyOffsetY; // #XMAS
            return data;
        }
        public static LootItem ToData(LootItemText text)
        {
            LootItem data = new();
            data.LootCard = GetCard(text.LootCard);
            data.LootPercent = text.LootPercent;
            data.LootRarity = (CardRarity)ToData<CardRarity>(text.LootRarity);
            data.LootType = (CardType)ToData<CardType>(text.LootType);
            return data;
        }
        public static PerkNodeData ToData(PerkNodeDataText text)
        {
            PerkNodeData data = ScriptableObject.CreateInstance<PerkNodeData>();
            data.name = text.ID;
            data.Column = text.Column;
            data.Cost = (PerkCost)ToData<PerkCost>(text.Cost);
            data.Id = text.ID;
            data.LockedInTown = text.LockedInTown;
            data.NotStack = text.NotStack;
            data.Perk = Globals.Instance.GetPerkData(text.Perk);
            if (text.PerkRequired.Length > 0)
                medsSecondRunImport2[text.ID] = text.PerkRequired;
            if (text.PerksConnected.Length > 0)
                medsSecondRunImport[text.ID] = text.PerksConnected;
            data.Row = text.Row;
            data.Sprite = GetSprite(text.Sprite);
            data.Type = text.Type;
            return data;
        }

        public static ChallengeData ToData(ChallengeDataText text)
        {
            ChallengeData data = ScriptableObject.CreateInstance<ChallengeData>();
            data.name = text.ID;
            data.Boss1 = GetNPC(text.Boss1);
            data.Boss2 = GetNPC(text.Boss2);
            data.BossCombat = Globals.Instance.GetCombatData(text.BossCombat);
            foreach (string cID in text.CorruptionList)
                if (!data.CorruptionList.Contains(GetCard(cID)))
                    data.CorruptionList.Add(GetCard(cID));
            data.Hero1 = Globals.Instance.GetSubClassData(text.Hero1);
            data.Hero2 = Globals.Instance.GetSubClassData(text.Hero2);
            data.Hero3 = Globals.Instance.GetSubClassData(text.Hero3);
            data.Hero4 = Globals.Instance.GetSubClassData(text.Hero4);
            data.Id = text.ID;
            data.IdSteam = text.IDSteam;
            data.Loot = medsLootDataSource.ContainsKey(text.Loot) ? medsLootDataSource[text.Loot] : (LootData)null;
            data.Seed = text.Seed;
            foreach (string cT in text.Traits)
                if (medsChallengeTraitsSource.ContainsKey(cT) && !data.Traits.Contains(medsChallengeTraitsSource[cT]))
                    data.Traits.Add(medsChallengeTraitsSource[cT]);
            data.Week = text.Week;
            return data;
        }

        public static ChallengeTrait ToData(ChallengeTraitText text)
        {
            ChallengeTrait data = ScriptableObject.CreateInstance<ChallengeTrait>();
            data.name = text.ID;
            data.Icon = GetSprite(text.Icon);
            data.Id = text.ID;
            data.IsMadnessTrait = text.IsMadnessTrait;
            data.Name = text.Name;
            data.Order = text.Order;
            return data;
        }

        public static CombatData ToData(CombatDataText text)
        {
            CombatData data = ScriptableObject.CreateInstance<CombatData>();
            data.name = text.CombatID;
            data.CinematicData = medsCinematicDataSource.ContainsKey(text.CinematicData) ? medsCinematicDataSource[text.CinematicData] : (CinematicData)null;
            data.CombatBackground = (CombatBackground)ToData<CombatBackground>(text.CombatBackground);
            data.CombatEffect = new CombatEffect[text.CombatEffect.Length];
            for (int a = 0; a < text.CombatEffect.Length; a++)
                data.CombatEffect[a] = ToData(JsonUtility.FromJson<CombatEffectText>(text.CombatEffect[a]));
            data.CombatId = text.CombatID;
            data.CombatMusic = GetAudio(text.CombatMusic);
            data.CombatTier = (CombatTier)ToData<CombatTier>(text.CombatTier);
            data.Description = text.Description;
            if (text.EventData.Length > 0)
                medsSecondRunCombatEvent[text.CombatID] = text.EventData;
            data.EventRequirementData = GetEventRequirement(text.EventRequirementData);
            data.HealHeroes = text.HealHeroes;
            data.NPCList = new NPCData[text.NPCList.Length];
            for (int a = 0; a < text.NPCList.Length; a++)
                data.NPCList[a] = medsNPCsSource.ContainsKey(text.NPCList[a]) ? medsNPCsSource[text.NPCList[a]] : (NPCData)null;
            data.NpcRemoveInMadness0Index = text.NPCRemoveInMadness0Index;
            data.ThermometerTierData = medsThermometerTierData.ContainsKey(text.ThermometerTierData) ? medsThermometerTierData[text.ThermometerTierData] : (ThermometerTierData)null;
            data.IsRift = text.IsRift; // #XMAS
            return data;
        }
        public static CombatEffect ToData(CombatEffectText text)
        {
            CombatEffect data = new();
            data.AuraCurse = Globals.Instance.GetAuraCurseData(text.AuraCurse);
            data.AuraCurseCharges = text.AuraCurseCharges;
            data.AuraCurseTarget = (CombatUnit)ToData<CombatUnit>(text.AuraCurseTarget);
            return data;
        }
        public static EventData ToData(EventDataText text)
        {
            EventData data = ScriptableObject.CreateInstance<EventData>();
            medsNodeEvent[text.EventID] = text.medsNode;
            medsNodeEventPercent[text.EventID] = text.medsPercent;
            medsNodeEventPriority[text.EventID] = text.medsPriority;
            medsTexts["events_" + text.EventID + "_nm"] = text.EventName;
            medsTexts["events_" + text.EventID + "_dsc"] = text.Description;
            medsTexts["events_" + text.EventID + "_dsca"] = text.DescriptionAction;
            data.name = text.EventID.ToLower();
            data.Description = text.Description;
            data.DescriptionAction = text.DescriptionAction;
            data.EventIconShader = (MapIconShader)ToData<MapIconShader>(text.EventIconShader);
            data.EventId = text.EventID;
            data.EventName = text.EventName;
            data.EventSpriteBook = GetSprite(text.EventSpriteBook);
            data.EventSpriteDecor = GetSprite(text.EventSpriteDecor);
            data.EventSpriteMap = GetSprite(text.EventSpriteMap);
            data.EventTier = (CombatTier)ToData<CombatTier>(text.EventTier);
            data.EventUniqueId = text.EventUniqueID;
            data.HistoryMode = text.HistoryMode;
            data.ReplyRandom = text.ReplyRandom;
            medsSecondRunImport[text.EventID] = text.Replies;
            data.RequiredClass = Globals.Instance.GetSubClassData(text.RequiredClass);
            data.Requirement = medsEventRequirementDataSource.ContainsKey(text.Requirement) ? medsEventRequirementDataSource[text.Requirement] : (EventRequirementData)null;
            return data;
        }
        public static EventReplyData ToData(EventReplyDataText text, string forceEventID = "")
        {
            EventReplyData data = new();
            if (forceEventID == "")
                forceEventID = text.medsEvent;
            medsTexts["events_" + forceEventID + "_rp" + text.IndexForAnswerTranslation] = text.ReplyText;
            medsTexts["events_" + forceEventID + "_rp" + text.IndexForAnswerTranslation + "_s"] = text.SSRewardText;
            medsTexts["events_" + forceEventID + "_rp" + text.IndexForAnswerTranslation + "_sc"] = text.SSCRewardText;
            medsTexts["events_" + forceEventID + "_rp" + text.IndexForAnswerTranslation + "_f"] = text.FLRewardText;
            medsTexts["events_" + forceEventID + "_rp" + text.IndexForAnswerTranslation + "_fc"] = text.FLCRewardText;
            data.DustCost = text.DustCost;
            data.GoldCost = text.GoldCost;
            data.IndexForAnswerTranslation = text.IndexForAnswerTranslation; // 66666; // used to capture texts
            data.RepeatForAllCharacters = text.RepeatForAllCharacters;
            data.ReplyActionText = (EventAction)ToData<EventAction>(text.ReplyActionText);
            data.ReplyShowCard = GetCard(text.ReplyShowCard);
            data.ReplyText = text.ReplyText;
            data.RequiredClass = Globals.Instance.GetSubClassData(text.RequiredClass);
            data.Requirement = Globals.Instance.GetRequirementData(text.Requirement);
            data.RequirementBlocked = Globals.Instance.GetRequirementData(text.RequirementBlocked);
            data.RequirementCard = new();
            for (int a = 0; a < text.RequirementCard.Length; a++)
                if (data.RequirementCard.Contains(GetCard(text.RequirementCard[a])))
                    data.RequirementCard.Add(GetCard(text.RequirementCard[a]));
            data.RequirementItem = GetCard(text.RequirementItem);
            data.RequirementMultiplayer = text.RequirementMultiplayer;
            data.RequirementSku = text.RequirementSku;
            data.SsAddCard1 = GetCard(text.SSAddCard1);
            data.SsAddCard2 = GetCard(text.SSAddCard2);
            data.SsAddCard3 = GetCard(text.SSAddCard3);
            data.SsAddItem = GetCard(text.SSAddItem);
            data.SsCardPlayerGame = text.SSCardPlayerGame;
            data.SsCardPlayerGamePackData = Globals.Instance.GetCardPlayerPackData(text.SSCardPlayerGamePackData);
            data.SsCardPlayerPairsGame = text.SSCardPlayerPairsGame;
            data.SsCardPlayerPairsGamePackData = Globals.Instance.GetCardPlayerPairsPackData(text.SSCardPlayerPairsGamePackData);
            data.SsCharacterReplacement = Globals.Instance.GetSubClassData(text.SSCharacterReplacement);
            data.SsCharacterReplacementPosition = text.SSCharacterReplacementPosition;
            data.SsCombat = Globals.Instance.GetCombatData(text.SSCombat);
            data.SsCorruptionUI = text.SSCorruptionUI;
            data.SsCorruptItemSlot = (ItemSlot)ToData<ItemSlot>(text.SSCorruptItemSlot);
            data.SsCraftUI = text.SSCraftUI;
            data.SsCraftUIMaxType = (CardRarity)ToData<CardRarity>(text.SSCraftUIMaxType);
            data.SsDiscount = text.SSDiscount;
            data.SsDustReward = text.SSDustReward;
            data.SsEvent = medsEventDataSource.ContainsKey(text.SSEvent) ? medsEventDataSource[text.SSEvent] : (EventData)null;
            data.SsExperienceReward = text.SSExperienceReward;
            data.SsFinishEarlyAccess = text.SSFinishEarlyAccess;
            data.SsFinishGame = text.SSFinishGame;
            data.SsFinishObeliskMap = text.SSFinishObeliskMap;
            data.SsGoldReward = text.SSGoldReward;
            data.SsHealerUI = text.SSHealerUI;
            data.SsLootList = Globals.Instance.GetLootData(text.SSLootList);
            data.SsMaxQuantity = text.SSMaxQuantity;
            data.SsMerchantUI = text.SSMerchantUI;
            data.SsNodeTravel = medsNodeDataSource.ContainsKey(text.SSNodeTravel) ? medsNodeDataSource[text.SSNodeTravel] : (NodeData)null;
            data.SsPerkData = Globals.Instance.GetPerkData(text.SSPerkData);
            data.SsPerkData1 = Globals.Instance.GetPerkData(text.SSPerkData1);
            data.SsRemoveItemSlot = (ItemSlot)ToData<ItemSlot>(text.SSRemoveItemSlot);
            data.SsRequirementLock = Globals.Instance.GetRequirementData(text.SSRequirementLock);
            data.SsRequirementLock2 = Globals.Instance.GetRequirementData(text.SSRequirementLock2);
            data.SsRequirementUnlock = Globals.Instance.GetRequirementData(text.SSRequirementUnlock);
            data.SsRequirementUnlock2 = Globals.Instance.GetRequirementData(text.SSRequirementUnlock2);
            data.SsRewardHealthFlat = text.SSRewardHealthFlat;
            data.SsRewardHealthPercent = text.SSRewardHealthPercent;
            data.SsRewardText = text.SSRewardText;
            data.SsRewardTier = GetTierReward(text.SSRewardTier);
            data.SsRoll = text.SSRoll;
            data.SsRollCard = (CardType)ToData<CardType>(text.SSRollCard);
            data.SsRollMode = (RollMode)ToData<RollMode>(text.SSRollMode);
            data.SsRollNumber = text.SSRollNumber;
            data.SsRollNumberCritical = text.SSRollNumberCritical;
            data.SsRollNumberCriticalFail = text.SSRollNumberCriticalFail;
            data.SsRollTarget = (RollTarget)ToData<RollTarget>(text.SSRollTarget);
            data.SsShopList = Globals.Instance.GetLootData(text.SSShopList);
            data.SsSteamStat = text.SSSteamStat;
            data.SsSupplyReward = text.SSSupplyReward;
            data.SsUnlockClass = Globals.Instance.GetSubClassData(text.SSUnlockClass);
            data.SsUnlockSkin = Globals.Instance.GetSkinData(text.SSUnlockSkin);
            data.SsUnlockSteamAchievement = text.SSUnlockSteamAchievement;
            data.SsUpgradeRandomCard = text.SSUpgradeRandomCard;
            data.SsUpgradeUI = text.SSUpgradeUI;
            data.SscAddCard1 = GetCard(text.SSCAddCard1);
            data.SscAddCard2 = GetCard(text.SSCAddCard2);
            data.SscAddCard3 = GetCard(text.SSCAddCard3);
            data.SscAddItem = GetCard(text.SSCAddItem);
            data.SscCardPlayerGame = text.SSCCardPlayerGame;
            data.SscCardPlayerGamePackData = Globals.Instance.GetCardPlayerPackData(text.SSCCardPlayerGamePackData);
            data.SscCardPlayerPairsGame = text.SSCCardPlayerPairsGame;
            data.SscCardPlayerPairsGamePackData = Globals.Instance.GetCardPlayerPairsPackData(text.SSCCardPlayerPairsGamePackData);
            data.SscCombat = Globals.Instance.GetCombatData(text.SSCCombat);
            data.SscCorruptionUI = text.SSCCorruptionUI;
            data.SscCorruptItemSlot = (ItemSlot)ToData<ItemSlot>(text.SSCCorruptItemSlot);
            data.SscCraftUI = text.SSCCraftUI;
            data.SscCraftUIMaxType = (CardRarity)ToData<CardRarity>(text.SSCCraftUIMaxType);
            data.SscDiscount = text.SSCDiscount;
            data.SscDustReward = text.SSCDustReward;
            data.SscEvent = medsEventDataSource.ContainsKey(text.SSCEvent) ? medsEventDataSource[text.SSCEvent] : (EventData)null;
            data.SscExperienceReward = text.SSCExperienceReward;
            data.SscFinishEarlyAccess = text.SSCFinishEarlyAccess;
            data.SscFinishGame = text.SSCFinishGame;
            data.SscGoldReward = text.SSCGoldReward;
            data.SscHealerUI = text.SSCHealerUI;
            data.SscLootList = Globals.Instance.GetLootData(text.SSCLootList);
            data.SscMaxQuantity = text.SSCMaxQuantity;
            data.SscMerchantUI = text.SSCMerchantUI;
            data.SscNodeTravel = medsNodeDataSource.ContainsKey(text.SSCNodeTravel) ? medsNodeDataSource[text.SSCNodeTravel] : (NodeData)null;
            data.SscRemoveItemSlot = (ItemSlot)ToData<ItemSlot>(text.SSCRemoveItemSlot);
            data.SscRequirementLock = Globals.Instance.GetRequirementData(text.SSCRequirementLock);
            data.SscRequirementUnlock = Globals.Instance.GetRequirementData(text.SSCRequirementUnlock);
            data.SscRequirementUnlock2 = Globals.Instance.GetRequirementData(text.SSCRequirementUnlock2);
            data.SscRewardHealthFlat = text.SSCRewardHealthFlat;
            data.SscRewardHealthPercent = text.SSCRewardHealthPercent;
            data.SscRewardText = text.SSCRewardText;
            data.SscRewardTier = GetTierReward(text.SSCRewardTier);
            data.SscShopList = Globals.Instance.GetLootData(text.SSCShopList);
            data.SscSupplyReward = text.SSCSupplyReward;
            data.SscUnlockClass = Globals.Instance.GetSubClassData(text.SSCUnlockClass);
            data.SscUnlockSteamAchievement = text.SSCUnlockSteamAchievement;
            data.SscUpgradeRandomCard = text.SSCUpgradeRandomCard;
            data.SscUpgradeUI = text.SSCUpgradeUI;
            data.FlAddCard1 = GetCard(text.FLAddCard1);
            data.FlAddCard2 = GetCard(text.FLAddCard2);
            data.FlAddCard3 = GetCard(text.FLAddCard3);
            data.FlAddItem = GetCard(text.FLAddItem);
            data.FlCardPlayerGame = text.FLCardPlayerGame;
            data.FlCardPlayerGamePackData = Globals.Instance.GetCardPlayerPackData(text.FLCardPlayerGamePackData);
            data.FlCardPlayerPairsGame = text.FLCardPlayerPairsGame;
            data.FlCardPlayerPairsGamePackData = Globals.Instance.GetCardPlayerPairsPackData(text.FLCardPlayerPairsGamePackData);
            data.FlCombat = Globals.Instance.GetCombatData(text.FLCombat);
            data.FlCorruptionUI = text.FLCorruptionUI;
            data.FlCorruptItemSlot = (ItemSlot)ToData<ItemSlot>(text.FLCorruptItemSlot);
            data.FlCraftUI = text.FLCraftUI;
            data.FlCraftUIMaxType = (CardRarity)ToData<CardRarity>(text.FLCraftUIMaxType);
            data.FlDiscount = text.FLDiscount;
            data.FlDustReward = text.FLDustReward;
            data.FlEvent = medsEventDataSource.ContainsKey(text.FLEvent) ? medsEventDataSource[text.FLEvent] : (EventData)null;
            data.FlExperienceReward = text.FLExperienceReward;
            data.FlGoldReward = text.FLGoldReward;
            data.FlHealerUI = text.FLHealerUI;
            data.FlLootList = Globals.Instance.GetLootData(text.FLLootList);
            data.FlMaxQuantity = text.FLMaxQuantity;
            data.FlMerchantUI = text.FLMerchantUI;
            data.FlNodeTravel = medsNodeDataSource.ContainsKey(text.FLNodeTravel) ? medsNodeDataSource[text.FLNodeTravel] : (NodeData)null;
            data.FlRemoveItemSlot = (ItemSlot)ToData<ItemSlot>(text.FLRemoveItemSlot);
            data.FlRequirementLock = Globals.Instance.GetRequirementData(text.FLRequirementLock);
            data.FlRequirementUnlock = Globals.Instance.GetRequirementData(text.FLRequirementUnlock);
            data.FlRequirementUnlock2 = Globals.Instance.GetRequirementData(text.FLRequirementUnlock2);
            data.FlRewardHealthFlat = text.FLRewardHealthFlat;
            data.FlRewardHealthPercent = text.FLRewardHealthPercent;
            data.FlRewardText = text.FLRewardText;
            data.FlRewardTier = GetTierReward(text.FLRewardTier);
            data.FlShopList = Globals.Instance.GetLootData(text.FLShopList);
            data.FlSupplyReward = text.FLSupplyReward;
            data.FlUnlockClass = Globals.Instance.GetSubClassData(text.FLUnlockClass);
            data.FlUnlockSteamAchievement = text.FLUnlockSteamAchievement;
            data.FlUpgradeRandomCard = text.FLUpgradeRandomCard;
            data.FlUpgradeUI = text.FLUpgradeUI;
            data.FlcAddCard1 = GetCard(text.FLCAddCard1);
            data.FlcAddCard2 = GetCard(text.FLCAddCard2);
            data.FlcAddCard3 = GetCard(text.FLCAddCard3);
            data.FlcAddItem = GetCard(text.FLCAddItem);
            data.FlcCardPlayerGame = text.FLCCardPlayerGame;
            data.FlcCardPlayerGamePackData = Globals.Instance.GetCardPlayerPackData(text.FLCCardPlayerGamePackData);
            data.FlcCardPlayerPairsGame = text.FLCCardPlayerPairsGame;
            data.FlcCardPlayerPairsGamePackData = Globals.Instance.GetCardPlayerPairsPackData(text.FLCCardPlayerPairsGamePackData);
            data.FlcCombat = Globals.Instance.GetCombatData(text.FLCCombat);
            data.FlcCorruptionUI = text.FLCCorruptionUI;
            data.FlcCorruptItemSlot = (ItemSlot)ToData<ItemSlot>(text.FLCCorruptItemSlot);
            data.FlcCraftUI = text.FLCCraftUI;
            data.FlcCraftUIMaxType = (CardRarity)ToData<CardRarity>(text.FLCCraftUIMaxType);
            data.FlcDiscount = text.FLCDiscount;
            data.FlcDustReward = text.FLCDustReward;
            data.FlcEvent = medsEventDataSource.ContainsKey(text.FLCEvent) ? medsEventDataSource[text.FLCEvent] : (EventData)null;
            data.FlcExperienceReward = text.FLCExperienceReward;
            data.FlcGoldReward = text.FLCGoldReward;
            data.FlcHealerUI = text.FLCHealerUI;
            data.FlcLootList = Globals.Instance.GetLootData(text.FLCLootList);
            data.FlcMaxQuantity = text.FLCMaxQuantity;
            data.FlcMerchantUI = text.FLCMerchantUI;
            data.FlcNodeTravel = medsNodeDataSource.ContainsKey(text.FLCNodeTravel) ? medsNodeDataSource[text.FLCNodeTravel] : (NodeData)null;
            data.FlcRemoveItemSlot = (ItemSlot)ToData<ItemSlot>(text.FLCRemoveItemSlot);
            data.FlcRequirementLock = Globals.Instance.GetRequirementData(text.FLCRequirementLock);
            data.FlcRequirementUnlock = Globals.Instance.GetRequirementData(text.FLCRequirementUnlock);
            data.FlcRequirementUnlock2 = Globals.Instance.GetRequirementData(text.FLCRequirementUnlock2);
            data.FlcRewardHealthFlat = text.FLCRewardHealthFlat;
            data.FlcRewardHealthPercent = text.FLCRewardHealthPercent;
            data.FlcRewardText = text.FLCRewardText;
            data.FlcRewardTier = GetTierReward(text.FLCRewardTier);
            data.FlcShopList = Globals.Instance.GetLootData(text.FLCShopList);
            data.FlcSupplyReward = text.FLCSupplyReward;
            data.FlcUnlockClass = Globals.Instance.GetSubClassData(text.FLCUnlockClass);
            data.FlcUnlockSteamAchievement = text.FLCUnlockSteamAchievement;
            data.FlcUpgradeRandomCard = text.FLCUpgradeRandomCard;
            data.FlcUpgradeUI = text.FLCUpgradeUI;

            return data;
        }
        public static EventRequirementData ToData(EventRequirementDataText text)
        {
            EventRequirementData data = ScriptableObject.CreateInstance<EventRequirementData>();
            data.name = text.RequirementID;
            data.AssignToPlayerAtBegin = text.AssignToPlayerAtBegin;
            data.Description = text.Description;
            if (text.Description.Length > 0)
                medsTexts["requirements_" + text.RequirementID.ToLower() + "_description"] = text.Description;
            data.ItemSprite = GetSprite(text.ItemSprite, "positionTop");
            data.RequirementId = text.RequirementID;
            data.RequirementName = text.RequirementName;
            if (text.RequirementName.Length > 0)
                medsTexts["requirements_" + text.RequirementID.ToLower() + "_name"] = text.RequirementName;
            data.RequirementTrack = text.RequirementTrack;
            data.TrackSprite = GetSprite(text.TrackSprite);
            data.ItemTrack = text.ItemTrack;
            // set requirementZoneFinishTrack with reflections
            data.GetType().GetField("requirementZoneFinishTrack", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(data, (Zone)ToData<Zone>(text.RequirementZoneFinishTrack));
            return data;
        }
        public static ZoneData ToData(ZoneDataText text)
        {
            ZoneData data = ScriptableObject.CreateInstance<ZoneData>();
            data.name = text.ZoneID;
            data.ChangeTeamOnEntrance = text.ChangeTeamOnEntrance;
            data.DisableExperienceOnThisZone = text.DisableExperienceOnThisZone;
            data.DisableMadnessOnThisZone = text.DisableMadnessOnThisZone;
            foreach (string sc in text.NewTeam)
            {
                SubClassData scd = Globals.Instance.GetSubClassData(sc);
                if (scd != (SubClassData)null && !data.NewTeam.Contains(scd))
                    data.NewTeam.Add(scd);
            }
            data.ObeliskFinal = text.ObeliskFinal;
            data.ObeliskHigh = text.ObeliskHigh;
            data.ObeliskLow = text.ObeliskLow;
            data.RestoreTeamOnExit = text.RestoreTeamOnExit;
            data.ZoneId = text.ZoneID;
            data.ZoneName = text.ZoneName;
            // COVERING ALL BASES AAAAAAAAAAAAAAAAAAAAAAAAAAH
            medsTexts[text.ZoneName] = text.ZoneName;
            medsTexts[text.ZoneID.ToLower()] = text.ZoneName;
            medsTexts[text.ZoneName.Replace(" ", "").ToLower()] = text.ZoneName;
            return data;
        }
        public static PackData ToData(PackDataText text)
        {
            PackData data = ScriptableObject.CreateInstance<PackData>();
            data.name = text.PackID;
            if (medsCardsSource.ContainsKey(text.Card0))
                data.Card0 = medsCardsSource[text.Card0];
            if (medsCardsSource.ContainsKey(text.Card1))
                data.Card1 = medsCardsSource[text.Card1];
            if (medsCardsSource.ContainsKey(text.Card2))
                data.Card2 = medsCardsSource[text.Card2];
            if (medsCardsSource.ContainsKey(text.Card3))
                data.Card3 = medsCardsSource[text.Card3];
            if (medsCardsSource.ContainsKey(text.Card4))
                data.Card4 = medsCardsSource[text.Card4];
            if (medsCardsSource.ContainsKey(text.Card5))
                data.Card5 = medsCardsSource[text.Card5];
            if (medsCardsSource.ContainsKey(text.CardSpecial0))
                data.CardSpecial0 = medsCardsSource[text.CardSpecial0];
            if (medsCardsSource.ContainsKey(text.CardSpecial1))
                data.CardSpecial1 = medsCardsSource[text.CardSpecial1];
            data.PackClass = (CardClass)ToData<CardClass>(text.PackClass);
            data.PackId = text.PackID;
            data.PackName = text.PackName;
            data.PerkList = new System.Collections.Generic.List<PerkData>();
            foreach (string perkID in text.PerkList)
                if (medsPerksSource.ContainsKey(perkID) && !data.PerkList.Contains(medsPerksSource[perkID]))
                    data.PerkList.Add(medsPerksSource[perkID]);
            if (medsCardsSource.ContainsKey(text.CardSpecial1))
                data.CardSpecial1 = medsCardsSource[text.CardSpecial1];
            if (text.RequiredClass.Length > 0)
                medsSecondRunImport2[text.PackID] = text.RequiredClass;
            return data;
        }
        public static CardPlayerPackData ToData(CardPlayerPackDataText text)
        {
            CardPlayerPackData data = ScriptableObject.CreateInstance<CardPlayerPackData>();
            data.name = text.PackId;
            if (medsCardsSource.ContainsKey(text.Card0))
                data.Card0 = medsCardsSource[text.Card0];
            if (medsCardsSource.ContainsKey(text.Card1))
                data.Card1 = medsCardsSource[text.Card1];
            if (medsCardsSource.ContainsKey(text.Card2))
                data.Card2 = medsCardsSource[text.Card2];
            if (medsCardsSource.ContainsKey(text.Card3))
                data.Card3 = medsCardsSource[text.Card3];
            data.Card0RandomBoon = text.Card0RandomBoon;
            data.Card0RandomInjury = text.Card0RandomInjury;
            data.Card1RandomBoon = text.Card1RandomBoon;
            data.Card1RandomInjury = text.Card1RandomInjury;
            data.Card2RandomBoon = text.Card2RandomBoon;
            data.Card2RandomInjury = text.Card2RandomInjury;
            data.Card3RandomBoon = text.Card3RandomBoon;
            data.Card3RandomInjury = text.Card3RandomInjury;
            data.ModIterations = text.ModIterations;
            data.ModSpeed = text.ModSpeed;
            data.PackId = text.PackId;
            return data;
        }
        public static CardPlayerPairsPackData ToData(CardPlayerPairsPackDataText text)
        {
            CardPlayerPairsPackData data = ScriptableObject.CreateInstance<CardPlayerPairsPackData>();
            data.name = text.PackId;
            data.PackId = text.PackId;
            if (medsCardsSource.ContainsKey(text.Card0))
                data.Card0 = medsCardsSource[text.Card0];
            if (medsCardsSource.ContainsKey(text.Card1))
                data.Card1 = medsCardsSource[text.Card1];
            if (medsCardsSource.ContainsKey(text.Card2))
                data.Card2 = medsCardsSource[text.Card2];
            if (medsCardsSource.ContainsKey(text.Card3))
                data.Card3 = medsCardsSource[text.Card3];
            if (medsCardsSource.ContainsKey(text.Card4))
                data.Card4 = medsCardsSource[text.Card4];
            if (medsCardsSource.ContainsKey(text.Card5))
                data.Card5 = medsCardsSource[text.Card5];
            return data;
        }
        public static ItemData ToData(ItemDataText text)
        {
            ItemData data = ScriptableObject.CreateInstance<ItemData>();
            data.name = text.ID;
            data.Acg1MultiplyByEnergyUsed = text.ACG1MultiplyByEnergyUsed;
            data.Acg2MultiplyByEnergyUsed = text.ACG2MultiplyByEnergyUsed;
            data.Acg3MultiplyByEnergyUsed = text.ACG3MultiplyByEnergyUsed;
            data.Activation = (EventActivation)ToData<EventActivation>(text.Activation);
            data.ActivationOnlyOnHeroes = text.ActivationOnlyOnHeroes;
            data.AuracurseBonus1 = Globals.Instance.GetAuraCurseData(text.AuraCurseBonus1);
            data.AuracurseBonus2 = Globals.Instance.GetAuraCurseData(text.AuraCurseBonus2);
            data.AuracurseBonusValue1 = text.AuraCurseBonusValue1;
            data.AuracurseBonusValue2 = text.AuraCurseBonusValue2;
            data.AuracurseCustomAC = Globals.Instance.GetAuraCurseData(text.AuraCurseCustomAC);
            data.AuracurseCustomModValue1 = text.AuraCurseCustomModValue1;
            data.AuracurseCustomModValue2 = text.AuraCurseCustomModValue2;
            data.AuracurseCustomString = text.AuraCurseCustomString;
            data.AuracurseGain1 = Globals.Instance.GetAuraCurseData(text.AuraCurseGain1);
            data.AuracurseGain2 = Globals.Instance.GetAuraCurseData(text.AuraCurseGain2);
            data.AuracurseGain3 = Globals.Instance.GetAuraCurseData(text.AuraCurseGain3);
            data.AuracurseGainValue1 = text.AuraCurseGainValue1;
            data.AuracurseGainValue2 = text.AuraCurseGainValue2;
            data.AuracurseGainValue3 = text.AuraCurseGainValue3;
            data.AuracurseGainSelf1 = Globals.Instance.GetAuraCurseData(text.AuraCurseGainSelf1);
            data.AuracurseGainSelf2 = Globals.Instance.GetAuraCurseData(text.AuraCurseGainSelf2);
            data.AuracurseGainSelfValue1 = text.AuraCurseGainSelfValue1;
            data.AuracurseGainSelfValue2 = text.AuraCurseGainSelfValue2;
            data.AuracurseImmune1 = Globals.Instance.GetAuraCurseData(text.AuraCurseImmune1);
            data.AuracurseImmune2 = Globals.Instance.GetAuraCurseData(text.AuraCurseImmune2);
            data.AuraCurseNumForOneEvent = text.AuraCurseNumForOneEvent;
            data.AuraCurseSetted = Globals.Instance.GetAuraCurseData(text.AuraCurseSetted);
            data.CardNum = text.CardNum;
            data.CardPlace = (CardPlace)ToData<CardPlace>(text.CardPlace);
            data.CardsReduced = text.CardsReduced;
            if (medsCardsSource.ContainsKey(text.CardToGain))
                data.CardToGain = medsCardsSource[text.CardToGain];
            else
                data.CardToGain = (CardData)null;
            data.CardToGainList = new();
            for (int a = 0; a < text.CardToGainList.Length; a++)
            {
                if (medsCardsSource.ContainsKey(text.CardToGainList[a]))
                {
                    CardData medsCardData = medsCardsSource[text.CardToGainList[a]];
                    if (!(data.CardToGainList.Contains(medsCardData)))
                        data.CardToGainList.Add(medsCardData);
                }
            }
            data.CardToGainType = (CardType)ToData<CardType>(text.CardToGainType);
            data.CardToReduceType = (CardType)ToData<CardType>(text.CardToReduceType);
            data.CastedCardType = (CardType)ToData<CardType>(text.CastedCardType);
            data.CastEnchantmentOnFinishSelfCast = text.CastEnchantmentOnFinishSelfCast;
            data.ChanceToDispel = text.ChanceToDispel;
            data.ChanceToDispelNum = text.ChanceToDispelNum;
            data.CharacterStatModified = (CharacterStat)ToData<CharacterStat>(text.CharacterStatModified);
            data.CharacterStatModified2 = (CharacterStat)ToData<CharacterStat>(text.CharacterStatModified2);
            data.CharacterStatModified3 = (CharacterStat)ToData<CharacterStat>(text.CharacterStatModified3);
            data.CharacterStatModifiedValue = text.CharacterStatModifiedValue;
            data.CharacterStatModifiedValue2 = text.CharacterStatModifiedValue2;
            data.CharacterStatModifiedValue3 = text.CharacterStatModifiedValue3;
            data.CostReducePermanent = text.CostReducePermanent;
            data.CostReduceReduction = text.CostReduceReduction;
            data.CostReduceEnergyRequirement = text.CostReduceEnergyRequirement;
            data.CostReduction = text.CostReduction;
            data.CostZero = text.CostZero;
            data.CursedItem = text.CursedItem;
            data.DamageFlatBonus = (DamageType)ToData<DamageType>(text.DamageFlatBonus);
            data.DamageFlatBonus2 = (DamageType)ToData<DamageType>(text.DamageFlatBonus2);
            data.DamageFlatBonus3 = (DamageType)ToData<DamageType>(text.DamageFlatBonus3);
            data.DamageFlatBonusValue = text.DamageFlatBonusValue;
            data.DamageFlatBonusValue2 = text.DamageFlatBonusValue2;
            data.DamageFlatBonusValue3 = text.DamageFlatBonusValue3;
            data.DamagePercentBonus = (DamageType)ToData<DamageType>(text.DamagePercentBonus);
            data.DamagePercentBonus2 = (DamageType)ToData<DamageType>(text.DamagePercentBonus2);
            data.DamagePercentBonus3 = (DamageType)ToData<DamageType>(text.DamagePercentBonus3);
            data.DamagePercentBonusValue = text.DamagePercentBonusValue;
            data.DamagePercentBonusValue2 = text.DamagePercentBonusValue2;
            data.DamagePercentBonusValue3 = text.DamagePercentBonusValue3;
            data.DamageToTarget = text.DamageToTarget;
            data.DamageToTargetType = (DamageType)ToData<DamageType>(text.DamageToTargetType);
            data.DestroyAfterUse = text.DestroyAfterUse;
            data.DestroyAfterUses = text.DestroyAfterUses;
            data.DestroyEndOfTurn = text.DestroyEndOfTurn;
            data.DestroyStartOfTurn = text.DestroyStartOfTurn;
            data.DrawCards = text.DrawCards;
            data.DrawMultiplyByEnergyUsed = text.DrawMultiplyByEnergyUsed;
            data.DropOnly = text.DropOnly;
            data.DttMultiplyByEnergyUsed = text.DTTMultiplyByEnergyUsed;
            data.DuplicateActive = text.DuplicateActive;
            data.EffectCaster = text.EffectCaster;
            data.EffectItemOwner = text.EffectItemOwner;
            data.EffectTarget = text.EffectTarget;
            data.EffectCasterDelay = text.EffectCasterDelay;
            data.EffectTargetDelay = text.EffectTargetDelay;
            data.EmptyHand = text.EmptyHand;
            data.EnergyQuantity = text.EnergyQuantity;
            data.ExactRound = text.ExactRound;
            data.HealFlatBonus = text.HealFlatBonus;
            data.HealPercentBonus = text.HealPercentBonus;
            data.HealPercentQuantity = text.HealPercentQuantity;
            data.HealPercentQuantitySelf = text.HealPercentQuantitySelf;
            data.HealQuantity = text.HealQuantity;
            data.HealReceivedFlatBonus = text.HealReceivedFlatBonus;
            data.HealReceivedPercentBonus = text.HealReceivedPercentBonus;
            data.Id = text.ID;
            data.IsEnchantment = text.IsEnchantment;
            data.ItemSound = GetAudio(text.ItemSound);
            data.ItemTarget = (ItemTarget)ToData<ItemTarget>(text.ItemTarget);
            data.LowerOrEqualPercentHP = text.LowerOrEqualPercentHP;
            data.MaxHealth = text.MaxHealth;
            data.ModifiedDamageType = (DamageType)ToData<DamageType>(text.ModifiedDamageType);
            data.NotShowCharacterBonus = text.NotShowCharacterBonus;
            data.OnlyAddItemToNPCs = text.OnlyAddItemToNPCs;
            data.PassSingleAndCharacterRolls = text.PassSingleAndCharacterRolls;
            data.PercentDiscountShop = text.PercentDiscountShop;
            data.PercentRetentionEndGame = text.PercentRetentionEndGame;
            data.Permanent = text.Permanent;
            data.QuestItem = text.QuestItem;
            data.ReduceHighestCost = text.ReduceHighestCost;
            data.ResistModified1 = (DamageType)ToData<DamageType>(text.ResistModified1);
            data.ResistModified2 = (DamageType)ToData<DamageType>(text.ResistModified2);
            data.ResistModified3 = (DamageType)ToData<DamageType>(text.ResistModified3);
            data.ResistModifiedValue1 = text.ResistModifiedValue1;
            data.ResistModifiedValue2 = text.ResistModifiedValue2;
            data.ResistModifiedValue3 = text.ResistModifiedValue3;
            data.RoundCycle = text.RoundCycle;
            data.SpriteBossDrop = GetSprite(text.SpriteBossDrop); // #TODO: SpriteBossDrop
            data.TimesPerCombat = text.TimesPerCombat;
            data.TimesPerTurn = text.TimesPerTurn;
            data.UsedEnergy = text.UsedEnergy;
            data.UseTheNextInsteadWhenYouPlay = text.UseTheNextInsteadWhenYouPlay;
            data.Vanish = text.Vanish;
            return data;
        }
        public static CardbackData ToData(CardbackDataText text)
        {
            CardbackData data = ScriptableObject.CreateInstance<CardbackData>();
            data.name = text.CardbackName;
            data.AdventureLevel = text.AdventureLevel;
            data.BaseCardback = text.BaseCardback;
            data.CardbackId = text.CardbackID;
            data.CardbackName = text.CardbackName;
            Log.LogDebug("about to get cardback sprite! " + text.CardbackSprite);
            data.CardbackSprite = GetSprite(text.CardbackSprite);
            if (medsSubClassesSource.ContainsKey(text.CardbackSubclass))
                data.CardbackSubclass = medsSubClassesSource[text.CardbackSubclass];
            data.Locked = text.Locked;
            data.ObeliskLevel = text.ObeliskLevel;
            data.RankLevel = text.RankLevel;
            data.ShowIfLocked = text.ShowIfLocked;
            data.Sku = text.Sku;
            data.SteamStat = text.SteamStat;
            return data;
        }
        public static SkinData ToData(SkinDataText text)
        {
            SkinData data = ScriptableObject.CreateInstance<SkinData>();
            data.name = text.SkinID;
            data.BaseSkin = text.BaseSkin;
            data.PerkLevel = text.PerkLevel;
            data.SkinGo = GetGO(text.SkinGO);
            data.SkinId = text.SkinID;
            data.SkinName = text.SkinName;
            data.SkinOrder = text.SkinOrder;
            if (medsSubClassesSource.ContainsKey(text.SkinSubclass))
                data.SkinSubclass = medsSubClassesSource[text.SkinSubclass];
            data.Sku = text.Sku;
            data.SpritePortrait = GetSprite(text.SpritePortrait);
            data.SpritePortraitGrande = GetSprite(text.SpritePortraitGrande);
            data.SpriteSilueta = GetSprite(text.SpriteSilueta, "positionTop");
            data.SpriteSiluetaGrande = GetSprite(text.SpriteSiluetaGrande, "positionTop");
            data.SteamStat = text.SteamStat;
            return data;
        }
        public static CinematicData ToData(CinematicDataText text)
        {
            CinematicData data = ScriptableObject.CreateInstance<CinematicData>();
            data.name = text.CinematicID;
            data.CinematicBSO = GetAudio(text.CinematicBSO);
            if (text.CinematicCombat.Length > 0)
                medsSecondRunCinematicCombat[text.CinematicID] = text.CinematicCombat;
            data.CinematicEndAdventure = text.CinematicEndAdventure;
            if (text.CinematicEvent.Length > 0)
                medsSecondRunCinematicEvent[text.CinematicID] = text.CinematicEvent;
            data.CinematicGo = GetGO(text.CinematicGo);
            data.CinematicId = text.CinematicID;
            return data;
        }
        public static CorruptionPackData ToData(CorruptionPackDataText text)
        {
            CorruptionPackData data = ScriptableObject.CreateInstance<CorruptionPackData>();
            data.name = text.PackName;
            foreach (string s in text.HighPack)
            {
                CardData crd = Globals.Instance.GetCardData(s);
                if (crd != (CardData)null && !data.HighPack.Contains(crd))
                    data.HighPack.Add(crd);
            }
            foreach (string s in text.LowPack)
            {
                CardData crd = Globals.Instance.GetCardData(s);
                if (crd != (CardData)null && !data.LowPack.Contains(crd))
                    data.LowPack.Add(crd);
            }
            data.PackClass = (CardClass)ToData<CardClass>(text.PackClass);
            data.PackName = text.PackName;
            data.PackTier = text.PackTier;
            return data;
        }
        public static KeyNotesData ToData(KeyNotesDataText text)
        {
            KeyNotesData data = ScriptableObject.CreateInstance<KeyNotesData>();
            data.name = text.ID;
            data.Id = text.ID;
            data.KeynoteName = text.KeynoteName;
            data.DescriptionExtended = text.DescriptionExtended;
            data.Description = text.Description;
            return data;
        }
        public static TierRewardData ToData(TierRewardDataText text)
        {
            TierRewardData data = ScriptableObject.CreateInstance<TierRewardData>();
            data.name = "Tier" + text.tierNum.ToString();
            data.TierNum = text.tierNum;
            data.Common = text.common;
            data.Uncommon = text.uncommon;
            data.Rare = text.rare;
            data.Epic = text.epic;
            data.Mythic = text.mythic;
            data.Dust = text.dust;
            return data;
        }
        public static Vector2 ToData(string text)
        {
            Vector2 data = new();
            string[] temp = text.Replace("(", "").Replace(")", "").Split(",");
            try
            {
                data = new Vector2(float.Parse(temp[0].Trim()), float.Parse(temp[1].Trim()));
            }
            catch
            {
                Log.LogError("Unable to parse Vector2 from string: " + text);
            }
            return data;
        }
        /*
         *                                                                                   
         *    888888888888  ,ad8888ba,          88           88  888b      88  88      a8P   
         *         88      d8"'    `"8b         88           88  8888b     88  88    ,88'    
         *         88     d8'        `8b        88           88  88 `8b    88  88  ,88"      
         *         88     88          88        88           88  88  `8b   88  88,d88'       
         *         88     88          88        88           88  88   `8b  88  8888"88,      
         *         88     Y8,        ,8P        88           88  88    `8b 88  88P   Y8b     
         *         88      Y8a.    .a8P         88           88  88     `8888  88     "88,   
         *         88       `"Y8888Y"'          88888888888  88  88      `888  88       Y8b  
         *
         *   Utilities for linking to AtO objects?
         */
        public static UnityEngine.AudioClip GetAudio(string audioClipName)
        {
            return medsAudioClips.ContainsKey(audioClipName) ? medsAudioClips[audioClipName] : (UnityEngine.AudioClip)null;
        }
        public static UnityEngine.Sprite GetSprite(string uncleanSpriteName, string type = "")
        {
            string spriteName = uncleanSpriteName.Trim().ToLower();
            //Log.LogDebug("getting sprite: " + spriteName);
            if (spriteName.Length == 0)
                return (Sprite)null;
            //LogDebug(spriteName + ".1");
            if (medsSprites.ContainsKey(spriteName))
            {
                if (type == "positionTop")
                {
                    Sprite tempSprite = Sprite.Create(medsSprites[spriteName].texture, new Rect(0, 0, medsSprites[spriteName].texture.width, medsSprites[spriteName].texture.height), new Vector2(0.5f, 0f), 99f, 0, SpriteMeshType.FullRect);
                    // 99f rather than 100f to increase the size a tiny bit and cover an unsightly border
                    return tempSprite;
                }
                return medsSprites[spriteName];
            }
            //LogDebug(spriteName + ".2");
            // sprite not found! 
            switch (type)
            {
                case "card":
                    Log.LogError("unable to get card sprite " + spriteName + "; using default card sprite instead!");
                    return medsSprites["medsDefaultCard"];
                case "auraCurse":
                    Log.LogError("unable to aura sprite " + spriteName + "; using default aura sprite instead!");
                    return medsSprites["medsDefaultAuraCurse"];
                    // case "charsprite", "charspriteborder" etc etc #charspriteborder
                    // case "perk"
                    // case 
            }
            //LogDebug(spriteName + ".3");
            return (Sprite)null;
        }
        public static UnityEngine.GameObject GetGO(string GOName)
        {
            //Log.LogDebug("GETGO: " + GOName);
            return (GOName != null && GOName.Length > 0 && medsGOs.ContainsKey(GOName)) ? UnityEngine.Object.Instantiate<GameObject>(medsGOs[GOName], new Vector3(0f, 0f), Quaternion.identity, medsInvisibleGOHolder.transform) : (UnityEngine.GameObject)null;
            //return (GOName.Length > 0 && Plugin.medsGOs.ContainsKey(GOName)) ? Plugin.medsGOs[GOName] : (UnityEngine.GameObject)null;
        }
        public static EventRequirementData GetEventRequirement(string nameERD)
        {
            return medsEventRequirementDataSource.ContainsKey(nameERD) ? medsEventRequirementDataSource[nameERD] : (EventRequirementData)null;
        }
        public static NPCData GetNPC(string nameNPC)
        {
            return medsNPCsSource.ContainsKey(nameNPC) ? medsNPCsSource[nameNPC] : (NPCData)null;
        }
        public static CardData GetCard(string cardID)
        {
            return medsCardsSource.ContainsKey(cardID) ? medsCardsSource[cardID] : (CardData)null;
        }
        public static NodeData GetNode(string nodeID)
        {
            return medsNodeDataSource.ContainsKey(nodeID) ? medsNodeDataSource[nodeID] : (NodeData)null;
        }
        public static TierRewardData GetTierReward(string tierNum)
        {
            if (tierNum.IsNullOrWhiteSpace())
                return (TierRewardData)null;
            if (int.TryParse(tierNum, out int t))
            {
                if (medsTierRewardDataSource.ContainsKey(t))
                    return medsTierRewardDataSource[t];
                else
                    Log.LogError("Could not find tierNum in TierRewardDataSource: " + t.ToString());
            }
            else
            {
                Log.LogError("Could not parse tierNum: " + tierNum);
            }
            return (TierRewardData)null;
        }
    }
}
