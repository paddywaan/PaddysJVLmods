// ItemDropDecayConfig
// a Valheim mod skeleton using Jötunn
// 
// File:    ItemDropDecayConfig.cs
// Project: ItemDropDecayConfig

using BepInEx;
using BepInEx.Configuration;

namespace ItemDropDecayConfig
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    public class ItemDropDecayConfig : BaseUnityPlugin
    {
        public const string PluginGUID = "com.paddy." + PluginName;
        public const string PluginName = "ItemDropDecayConfig";
        public const string PluginVersion = "0.0.1";
        public static ConfigEntry<double> DecayTimer;

        private void Awake()
        {
            //Conf.genSettings = base.Config;
            DecayTimer = base.Config.Bind("SETTINGS", "Decay Timer", 3600d, "Time in seconds before an item decays. Only affects drops outside of base (crafting station/fire) and player radius.");
            Hooks.Init();
        }
    }
}