using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Obeliskial_Content;
using static TheSubclass.Plugin;
using static TheSubclass.CustomFunctions;
using UnityEngine;


// NOTE: This sample module assumes that you know how to perform basic Harmony patches. 
// If needed look at the Sample Plugin File(s) - in particular the SamplePatches.cs file

namespace TheSubclass
{
    [HarmonyPatch]
    internal class Traits
    {
        // list of your trait IDs
        public static string heroName = "<heroName>";

        public static string subclassname = "<subclassname>";

        public static string[] simpleTraitList = ["trait0", "trait1a", "trait1b", "trait2a", "trait2b", "trait3a", "trait3b", "trait4a", "trait4b"];

        public static string[] myTraitList = simpleTraitList; //(string[])simpleTraitList.Select(trait => heroName + trait); // Needs testing

        static string trait0 = myTraitList[0];
        static string trait2a = myTraitList[3];
        static string trait2b = myTraitList[4];
        static string trait4a = myTraitList[7];
        static string trait4b = myTraitList[8];


        public static string debugBase = "Binbin - Testing " + heroName + " ";

        public static void DoCustomTrait(string _trait, ref Trait __instance)
        {
            // get info you may need
            Enums.EventActivation _theEvent = Traverse.Create(__instance).Field("theEvent").GetValue<Enums.EventActivation>();
            Character _character = Traverse.Create(__instance).Field("character").GetValue<Character>();
            Character _target = Traverse.Create(__instance).Field("target").GetValue<Character>();
            int _auxInt = Traverse.Create(__instance).Field("auxInt").GetValue<int>();
            string _auxString = Traverse.Create(__instance).Field("auxString").GetValue<string>();
            CardData _castedCard = Traverse.Create(__instance).Field("castedCard").GetValue<CardData>();
            Traverse.Create(__instance).Field("character").SetValue(_character);
            Traverse.Create(__instance).Field("target").SetValue(_target);
            Traverse.Create(__instance).Field("theEvent").SetValue(_theEvent);
            Traverse.Create(__instance).Field("auxInt").SetValue(_auxInt);
            Traverse.Create(__instance).Field("auxString").SetValue(_auxString);
            Traverse.Create(__instance).Field("castedCard").SetValue(_castedCard);
            TraitData traitData = Globals.Instance.GetTraitData(_trait);
            List<CardData> cardDataList = [];
            List<string> heroHand = MatchManager.Instance.GetHeroHand(_character.HeroIndex);
            Hero[] teamHero = MatchManager.Instance.GetTeamHero();
            NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();

            if (_trait == trait0)
            { // TODO trait 0
                string traitName = traitData.TraitName;
                string traitId = _trait;
                LogDebug($"Handling Trait {traitId}: {traitName}");

                if (!IsLivingHero(_character))
                {
                    return;
                }
                int bonusActivations = _character.HaveTrait(trait4a) ? 1 : 0;
                if (CanIncrementTraitActivations(_trait, bonusActivations:bonusActivations))
                {
                    IncrementTraitActivations(_trait);
                    DisplayRemainingChargesForTrait(ref _character, traitData);

                }
            }


            else if (_trait == trait2a)
            { // TODO trait 2a
                string traitName = traitData.TraitName;
                string traitId = _trait;
                LogDebug($"Handling Trait {traitId}: {traitName}");
                DisplayTraitScroll(ref _character, traitData);

            }



            else if (_trait == trait2b)
            { // TODO trait 2b
                string traitName = traitData.TraitName;
                string traitId = _trait;
                LogDebug($"Handling Trait {traitId}: {traitName}");
                DisplayTraitScroll(ref _character, traitData);

            }

            else if (_trait == trait4a)
            { // TODO trait 4a
                string traitName = traitData.TraitName;
                string traitId = _trait;
                LogDebug($"Handling Trait {traitId}: {traitName}");

            }

            else if (_trait == trait4b)
            { // TODO trait 4b
                string traitName = traitData.TraitName;
                string traitId = _trait;
                LogDebug($"Handling Trait {traitId}: {traitName}");
                if (CanIncrementTraitActivations(_trait))
                {
                    IncrementTraitActivations(_trait);
                    DisplayRemainingChargesForTrait(ref _character, traitData);

                }
            }

        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Trait), "DoTrait")]
        public static bool DoTrait(Enums.EventActivation _theEvent, string _trait, Character _character, Character _target, int _auxInt, string _auxString, CardData _castedCard, ref Trait __instance)
        {
            if ((UnityEngine.Object)MatchManager.Instance == (UnityEngine.Object)null)
                return false;
            Traverse.Create(__instance).Field("character").SetValue(_character);
            Traverse.Create(__instance).Field("target").SetValue(_target);
            Traverse.Create(__instance).Field("theEvent").SetValue(_theEvent);
            Traverse.Create(__instance).Field("auxInt").SetValue(_auxInt);
            Traverse.Create(__instance).Field("auxString").SetValue(_auxString);
            Traverse.Create(__instance).Field("castedCard").SetValue(_castedCard);
            if (Content.medsCustomTraitsSource.Contains(_trait) && myTraitList.Contains(_trait))
            {
                DoCustomTrait(_trait, ref __instance);
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Character), "SetEvent")]
        public static void SetEventPrefix(ref Character __instance, ref Enums.EventActivation theEvent, Character target = null)
        {
            /*if (theEvent == Enums.EventActivation.AuraCurseSet && !__instance.IsHero && target != null && target.IsHero && target.HaveTrait("ulfvitrconductor") && __instance.HasEffect("spark"))
            { // if NPC has wet applied to them, deal 50% of their sparks as indirect lightning damage
                __instance.IndirectDamage(Enums.DamageType.Lightning, Functions.FuncRoundToInt((float)__instance.GetAuraCharges("spark") * 0.5f));
            }
            if (theEvent == Enums.EventActivation.BeginTurn && __instance.IsHero && (__instance.HaveTrait("pestilyhealingtoxins")||__instance.HaveTrait("pestilytoxichealing"))){
                level5ActivationCounter=0;
                // Plugin.Log.LogInfo("Binbin - PestilyBiohealer - Reset Activation Counter: "+ level5ActivationCounter);
            }
            
            */
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(AtOManager), "HeroLevelUp")]
        public static bool HeroLevelUpPrefix(ref AtOManager __instance, Hero[] ___teamAtO, int heroIndex, string traitId)
        {
            Hero hero = ___teamAtO[heroIndex];
            Plugin.Log.LogDebug(debugBase + "Level up before conditions for subclass " + hero.SubclassName + " trait id " + traitId);

            string traitOfInterest = myTraitList[4]; //Learn real magic
            if (hero.AssignTrait(traitId))
            {
                TraitData traitData = Globals.Instance.GetTraitData(traitId);
                if ((UnityEngine.Object)traitData != (UnityEngine.Object)null && traitId == traitOfInterest)
                {
                    Plugin.Log.LogDebug(debugBase + "Learn Real Magic inside conditions");
                    Globals.Instance.SubClass[hero.SubclassName].HeroClassSecondary = Enums.HeroClass.Mage;
                }

            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), nameof(Character.DamageBonus))]
        public static void DamageBonusPostfix(ref Character __instance, ref float[] __result, Enums.DamageType DT)
        {
            LogDebug("DamageBonusPostfix");
            // __result is a float[] of [bonusFlatDamage, bonusPercentDamage]
            if (!IsLivingHero(__instance) || AtOManager.Instance == null || MatchManager.Instance == null)
                return;

            string traitOfInterest = trait4a;
            if (AtOManager.Instance.CharacterHaveTrait(__instance.SubclassName, traitOfInterest))// && DT == Enums.DamageType.All)
            {
                int bonusDamage = 1;
                float bonusPercentDamage = 10.0f;
                __result[0] += bonusDamage;
                __result[1] += bonusPercentDamage;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), nameof(Character.GetTraitDamagePercentModifiers))]
        public static void GetTraitDamagePercentModifiersPostfix(ref Character __instance, ref float __result, Enums.DamageType DamageType)
        {
            LogInfo("GetTraitDamagePercentModifiersPostfix");
            // trait0: Gain 5% damage 
            string traitOfInterest = trait0;
            if (IsLivingHero(__instance) && AtOManager.Instance!= null && AtOManager.Instance.CharacterHaveTrait(__instance.SubclassName, traitOfInterest)&& MatchManager.Instance!=null)
            {                
                int percentToIncrease = 5;
                __result += percentToIncrease;
            }
        }



        [HarmonyPostfix]
        [HarmonyPatch(typeof(AtOManager), "GlobalAuraCurseModificationByTraitsAndItems")]
        public static void GlobalAuraCurseModificationByTraitsAndItemsPostfix(ref AtOManager __instance, ref AuraCurseData __result, string _type, string _acId, Character _characterCaster, Character _characterTarget)
        {
            LogInfo($"GACM {subclassname}");

            Character characterOfInterest = _type == "set" ? _characterTarget : _characterCaster;
            string traitOfInterest;

            switch (_acId)
            {
                case "burn":
                    traitOfInterest = trait0;
                    if (IfCharacterHas(characterOfInterest, CharacterHas.Trait, traitOfInterest, AppliesTo.None))
                    {
                        __result.ACName = "something";
                    }
                    break;
            }

        }



    }
}
