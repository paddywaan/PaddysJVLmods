// CombatTweaks
// a Valheim mod skeleton using Jötunn
// 
// File:    CombatTweaks.cs
// Project: CombatTweaks

using BepInEx;

namespace CombatTweaks
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class CombatTweaks : BaseUnityPlugin
    {
        public const string PluginGUID = "com.jotunn.jotunnmodstub";
        public const string PluginName = "CombatTweaks";
        public const string PluginVersion = "0.0.1";

        private void Awake()
        {
            Conf.genSettings = base.Config;
            Hooks.Init();
        }
    }
}