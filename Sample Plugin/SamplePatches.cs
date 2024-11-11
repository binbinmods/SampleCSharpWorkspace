using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using static Obeliskial_Essentials.Essentials;
using System;

// Make sure your namespace is the same everywhere
namespace SamplePlugin{

    [HarmonyPatch] //DO NOT REMOVE/CHANGE

    public class NameYourPluginClass
    {
        // To create a patch, you need to declare either a prefix or a postfix. 
        // Prefixes are executed before the original code, postfixes are executed after
        // Then you need to tell Harmony which method to patch.

        [HarmonyPrefix]
        [HarmonyPatch(typeof(AtOManager),nameof(AtOManager.BeginAdventure))]
        public static void BeginAdventurePrefix(AtOManager __instance){
            Plugin.Log.LogInfo("Begin Adventure Prefix");
            return;       
            
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(AtOManager),nameof(AtOManager.BeginAdventure))]
        public static void BeginAdventurePostfix(AtOManager __instance, ref bool __result){
            Plugin.Log.LogInfo("Begin Adventure Postfix");
            return;       
            
        }

    }
}