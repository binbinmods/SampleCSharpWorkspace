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
    [HarmonyPatch] // DO NOT REMOVE/CHANGE - This tells your plugin that this is part of the mod

    internal class Traits
    {
        /*
        This Traits class has already been set up to do some of the heavy lifting for you to make life easier.
        Because of that there is some extra code in here which is unnecessary if you know what you are doing

        Before I start with anything I first need to explain a bit about how Traits work.
        In general there are two categories of traits, activated and nonactivated traits
        Activated traits are things like a Duality which have an Activation condition tied to when you play a card.
        Or the "Masteries" which trigger at the start of turn, after cards are dealt (BeginTurnCardsDealth)
        Likewise Ottis's Shielder has an activation condition for when you apply an AuraCurse (AuraCurseSet)
        Malukah's Jinx has an Activation of "Damage" for when she damages an enemy
        
        Non-Activated Traits are things that are always active:
        the bonus Sanctify for Ottis, Navalea's Hatred, or Yogger's Big Bad Wolf. 
        Also things like Laia's Zealotry: "Zeal on this hero increases all damage done by 1.5% per Burn charge"

        Some traits can have multiple components. Those will need to be handled a bit differently. 

        I'll start explaining the "DoTrait" Prefix.
        You can pretty much completely ignore this function. 
        All that it does is 
        1. Executes when your the trait Activation Event fires
        2. Checks to see if the trait that is being fired is a modded one
        3. If so, launches the "DoCustomTrait" function.
        So all of the logic is in the "DoCustomTrait" Function.
        This tutorial will continue there
        */


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

        // these are just some variable definitions that might be useful later
        public static string heroName = "<ulfvitr>";

        public static string subclassname = "<stormshaman>";

        // make sure that the names of your traits here match with the names of your traits in the subclass.json 
        // and the respective trait jsons
        public static string[] myTraitList = ["trait0", "trait1a", "trait1b", "trait2a", "trait2b", "trait3a", "trait3b", "trait4a", "trait4b"];

        public static string debugBase = "Binbin - Testing " + heroName + " ";

        public static void DoCustomTrait(string _trait, ref Trait __instance)
        {
            // These are a bunch of functions that access frequently used info. 
            // They primarily use Traveres to get/set private variables.
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

            // This is unnecessary, but I personally prefer to refer to the traits as "trait0" rather than the trait's id
            // So I wrote this to save myself the hassle from time to time
            string trait0 = myTraitList[0];
            string trait2a = myTraitList[3];
            string trait2b = myTraitList[4];
            string trait4a = myTraitList[7];
            string trait4b = myTraitList[8];

            // This is where you actually code your activated traits
            if (_trait == trait0)
            { // TODO trait 0
            
                // As a demonstration, I will show how I would do Ottis's Shielder Trait:
                // When you apply Shield, heal for 30% of that amount.
                string traitName = traitData.TraitName;
                LogDebug($"Executing Trait {_trait} - {traitName}");
                
                // This trait has 3 conditions (that correspond to this if-statement): 
                // 1. We need to be applying Shield
                // 2. Our target needs to be a Hero that is alive
                // 3. The caster needs to be alive.
                // Keep in mind that we do **NOT** need to check the Activation condition (that we are setting an AuraCurse), 
                // that is already taken care of before the DoCustomTrait is called

                // For the Activation AuraCurseSet, the _auxString is the aura or curse that is being set
                // And the _auxInt is the amount that is being applied.
                if (_auxString == "shield" && IsLivingHero(_target) && IsLivingHero(_character))
                {
                    // To calculate the amount we need to heal, just multiply _auxInt by 30%
                    int amountToHeal = Mathf.RoundToInt(0.30f * _auxInt);

                    // I have already written a function that handles the healing part, so I'll just use that
                    TraitHeal(ref _character, ref _target, amountToHeal, traitName);
                }
            }


            else if (_trait == trait2a)
            { // TODO trait 2a
                // As a second example, I will show how I would implement a trait like Andrin's Momentum
                // When you play a Melee Attack, gain 1 powerful and 1 energy. 3x/turn
                // For traits with activations, we need to do 3 things.
                // 1. "TimesPerTurn" property of the trait.json file needs to be set
                // 2. We need to check if you have used up all of the activations
                // 3. You need to increment the number of activations

                string traitName = traitData.TraitName;
                LogDebug($"Executing Trait {_trait} - {traitName}");

                // Conditions:
                // 1. Melee Attack - uses the _castedCard.HasCardType function
                // 2. We need to not have run out of activations - uses a custom function that checks if you can increment a trait
                // 3. Our hero needs to be alive (it never hurts to check this in case somehow your character manages to cast something from beyond the grave)

                if(_castedCard.HasCardType(Enums.CardType.Melee_Attack)&& CanIncrementTraitActivations(_trait) && IsLivingHero(_character))
                {
                    // Gain 1 powerful
                    _character.SetAuraTrait(_character,"powerful",1);
                    
                    // Again, I have a custom function for common things like gaining energy
                    GainEnergy(_character,1);

                    // don't forget to increment the trait activations
                    IncrementTraitActivations(_trait); 

                }

            }



            else if (_trait == trait2b)
            { // TODO trait 2b
                string traitName = traitData.TraitName;
                LogDebug($"Executing Trait {_trait} - {traitName}");

            }

            else if (_trait == trait4a)
            { // TODO trait 4a
                string traitName = traitData.TraitName;
                LogDebug($"Executing Trait {_trait} - {traitName}");

            }

            else if (_trait == trait4b)
            { // TODO trait 4b
                string traitName = traitData.TraitName;
                LogDebug($"Executing Trait {_trait} - {traitName}");

            }

        }



        [HarmonyPrefix]
        [HarmonyPatch(typeof(Character), "SetEvent")]
        public static void SetEventPrefix(ref Character __instance, ref Enums.EventActivation theEvent, Character target = null)
        {
            /*
            if (theEvent == Enums.EventActivation.AuraCurseSet && !__instance.IsHero && target != null && target.IsHero && target.HaveTrait("ulfvitrconductor") && __instance.HasEffect("spark"))
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
            LogDebug(debugBase + "Level up before conditions for subclass " + hero.SubclassName + " trait id " + traitId);

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
        [HarmonyPatch(typeof(AtOManager), "GlobalAuraCurseModificationByTraitsAndItems")]
        public static void GlobalAuraCurseModificationByTraitsAndItemsPostfix(ref AtOManager __instance, ref AuraCurseData __result, string _type, string _acId, Character _characterCaster, Character _characterTarget)
        {
            // Shadow Poison -  +1 Shadow Damage per 10 stacks of Poison on you. 
            // Antidote - You are immune to Poison damage, Poison stacks on you are limited to 300
            
        }



    }
}