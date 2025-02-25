using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
// using static Obeliskial_Essentials.Essentials;
using System;
using static SamplePlugin.Plugin;
using static SamplePlugin.CustomFunctions;

// Make sure your namespace is the same everywhere
namespace SamplePlugin{

    [HarmonyPatch] // DO NOT REMOVE/CHANGE - This tells your plugin that this is part of the mod

    public class NameYourPluginClass
    {
        // The base game is organized into a large number of classes
        // AtOManager is the class that handles the majority of the game logic.
        // MatchManager handles things to do with combats
        // MapManager handles things to do with the map/node selection
        // Character handles things to do which your characters traits, stats, and stuff like that

        // I will show some of the most commonly patched methods below and how to patch them

        // Patches must start with either a [HarmonyPrefix] or [HarmonyPostfix] tag
        // Followed by [HarmonyPatch(typeof(<class>),nameof(<class>.<method>))]
        // This tells your plugin which base game method to patch and whether it will be a prefix or a postfix
        
        // Prefixes are executed before the original code, postfixes are executed after

        [HarmonyPrefix]
        [HarmonyPatch(typeof(AtOManager),nameof(AtOManager.BeginAdventure))]
        public static void BeginAdventurePrefix(AtOManager __instance){
            // BeginAdventure
            LogInfo("Begin Adventure Prefix");
            return;       
            
        }
        
        // Postfixes work mostly the same
        [HarmonyPostfix]
        [HarmonyPatch(typeof(AtOManager),nameof(AtOManager.BeginAdventure))]
        public static void BeginAdventurePostfix(AtOManager __instance){
            Plugin.Log.LogInfo("Begin Adventure Postfix");
            return;       
            
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character),nameof(Character.GetEnergy))]
        public static void GetEnergyPostfix(Character __instance, ref int __result){
            /* 
            This code will execute any time the GetEnergy method is called
            You can then go and look up in the Vanilla code, and search for GetEnergy to see where it is used
            Ideally you should have done that first, but I just picked a random function
            Get Energy is only called on NPCs (in the DoAI method of the AI class) and sets a variable called _energy. 
            This variable is used to make sure NPC cards don't cost too much, but they don't have costs, 
            so the GetEnergy function is pretty much useless, but it is called during every NPCs turn.
             */

            // Postfixes can reference the return value of the vanilla code with the __result parameter
            // I typically prefer to use "pass-through" mode where the postfixes are all voids and modify the __result rather than returning a new __result

            Plugin.Log.LogInfo("GetEnergy Postfix");
            Plugin.Log.LogInfo("GetEnergy Energy: " + __result);
            
            // This modifies the output of the vanilla GetEnergy function and will be passed to the _energy variable
            __result = 9;

            Plugin.Log.LogInfo("GetEnergy Energy After modification: " + __result);
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character),nameof(Character.BeginTurn))]
        public static void BeginTurnPostfix(Character __instance){
            /* 
            This is going to show how to reference a private variable
            */
            Plugin.Log.LogInfo("GetEnergy Postfix");
            Plugin.Log.LogInfo("GetEnergy Energy: ");
            

        }

    }
}