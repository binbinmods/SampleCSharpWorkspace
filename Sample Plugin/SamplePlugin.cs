using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using static Obeliskial_Essentials.Essentials;
using System;


namespace SamplePlugin{
    [HarmonyPatch]
    public class NameYourPluginClass
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(AtOManager),"IsTownRerollAvailable")]
        public static bool IsTownRerollAvailable(AtOManager __instance, ref bool __result){

            return true;       
            
        }

    }
}