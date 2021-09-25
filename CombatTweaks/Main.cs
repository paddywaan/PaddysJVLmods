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
        public const string PluginGUID = "com.paddy." + PluginName;
        public const string PluginName = "CombatTweaks";
        public const string PluginVersion = "1.0.0";

        private void Awake()
        {
            CraftAll.Init();
        }
    }
}