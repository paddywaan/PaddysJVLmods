// ItemDropDecayConfig
// a Valheim mod skeleton using Jötunn
// 
// File:    ItemDropDecayConfig.cs
// Project: ItemDropDecayConfig

using BepInEx;

namespace ItemDropDecayConfig
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class ItemDropDecayConfig : BaseUnityPlugin
    {
        public const string PluginGUID = "com.paddy." + PluginName;
        public const string PluginName = "ItemDropDecayConfig";
        public const string PluginVersion = "0.0.1";

        private void Awake()
        {
            Conf.genSettings = base.Config;
            Hooks.Init();
        }
    }
}