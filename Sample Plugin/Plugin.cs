using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using static Obeliskial_Essentials.Essentials;

namespace SamplePlugin{

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.stiffmeds.obeliskialessentials")]
    [BepInProcess("AcrossTheObelisk.exe")]

    public class Plugin : BaseUnityPlugin
    {
        
        // You can create configs that users can set by creating a ConfigEntry object here, and then using 
        // config = Config.Bind()
        public static ConfigEntry<bool> SampleBooleanConfig { get; set; }
        public static ConfigEntry<int> SampleIntegerConfig { get; set; }

        internal const int ModDate = 20241023;
        private readonly Harmony harmony = new(PluginInfo.PLUGIN_GUID);
        internal static ManualLogSource Log;
        private void Awake()
        {
            Log = Logger;
            Log.LogInfo($"{PluginInfo.PLUGIN_GUID} {PluginInfo.PLUGIN_VERSION} has loaded!");


            
            SampleBooleanConfig = Config.Bind(new ConfigDefinition("Debug", "Name of Config"), true, new ConfigDescription("Description of Config"));
            SampleIntegerConfig = Config.Bind(new ConfigDefinition("Debug", "Name of Config"), 3, new ConfigDescription("Description of Config)"));
            

            // register with Obeliskial Essentials
            RegisterMod(
                _name: PluginInfo.PLUGIN_NAME,
                _author: "binbin",
                _description: "Sample Plugin",
                _version: PluginInfo.PLUGIN_VERSION,
                _date: ModDate,
                _link: @"https://github.com/binbinmods/SampleCSharpWorkspace"
            );
            
            // apply patches
            harmony.PatchAll();
        }

    }
}