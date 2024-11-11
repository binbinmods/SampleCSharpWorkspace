using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using static Obeliskial_Essentials.Essentials;
using Obeliskial_Essentials;
using System.IO;
using UnityEngine;
using System;

namespace TheSubclass
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.stiffmeds.obeliskialessentials")]
    [BepInDependency("com.stiffmeds.obeliskialcontent")]
    [BepInProcess("AcrossTheObelisk.exe")]
    public class Plugin : BaseUnityPlugin
    {
        internal int ModDate = int.Parse(DateTime.Today.ToString("yyyyMMdd"));
        private readonly Harmony harmony = new(PluginInfo.PLUGIN_GUID);
        internal static ManualLogSource Log;

        public static string characterName = "<HeroName>";
        public static string subclassName = "<Subclass>"; // needs caps

        private void Awake()
        {
            Log = Logger;
            Log.LogInfo($"{PluginInfo.PLUGIN_GUID} {PluginInfo.PLUGIN_VERSION} has loaded!");
            // register with Obeliskial Essentials
            RegisterMod(
                _name: PluginInfo.PLUGIN_NAME,
                _author: "binbin",
                _description: "<Hero, The Subclass>.",
                _version: PluginInfo.PLUGIN_VERSION,
                _date: ModDate,
                _link: @"https://github.com/binbinmods/heronamesubclass",
                _contentFolder: "<Hero>",
                _type: ["content", "hero", "trait"]
            );
            // apply patches
            harmony.PatchAll();
        }

        [HarmonyPatch]
        internal class Patches
        {

            [HarmonyPrefix]
            [HarmonyPatch(typeof(EventData), "Init")]
            public static void InitPrefix(ref Globals __instance)
            {
                // Do Not Change this for now`

                //Plugin.Log.LogDebug("Binbin -- Attempting to add subclass to list for all events");

                //Plugin.Log.LogDebug("Binbin -- After Adding List: " + string.Join(";", Globals.Instance.SubClass.Select(x => x.Key).ToArray()));
                //Plugin.Log.LogDebug("Binbin -- medsSubClassesSource: " + string.Join(";", Content.medsSubClassesSource.Select(x => x.Key).ToArray()));
                string p = Path.Combine(Paths.ConfigPath, "Obeliskial_importing", characterName, "subclass");
                if (Directory.Exists(p))
                {
                    //Plugin.Log.LogDebug("Binbin -- Path: " + p);
                    FileInfo[] medsFI = (new DirectoryInfo(Path.Combine(Paths.ConfigPath, "Obeliskial_importing", characterName, "subclass"))).GetFiles("*.json");
                    foreach (FileInfo f in medsFI)
                    {
                        try
                        {
                            SubClassData subclass = Obeliskial_Content.DataTextConvert.ToData(JsonUtility.FromJson<SubClassDataText>(File.ReadAllText(f.ToString())));
                            //Log.LogInfo("Binbin -- subclass to add : " + subclass.SubClassName);
                            if (subclass != null && !Globals.Instance.SubClass.ContainsKey(subclass.SubClassName))
                            {
                                Globals.Instance.SubClass.Add(subclass.SubClassName.ToLower(), subclass);

                                //Plugin.Log.LogDebug("Binbin -- Subclass Would be added: " + subclass.SubClassName);
                            }
                        }
                        catch (Exception e) { Log.LogError("Binbin -- Error loading custom " + characterName + " subclass " + f.Name + ": " + e.Message); }
                    }
                }


            }
            [HarmonyPostfix]
            [HarmonyPatch(typeof(EventData), "Init")]
            public static void InitPostfix(ref Globals __instance)
            {
                // This prevents there from being the same class there multiple times leading to a base game error.

                if (Globals.Instance.SubClass.ContainsKey(subclassName))
                {
                    Globals.Instance.SubClass.Remove(subclassName);
                }
            }
        }
    }
}
