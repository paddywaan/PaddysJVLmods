// StackingNotifications
// a Valheim mod skeleton using Jötunn
// 
// File:    StackingNotifications.cs
// Project: StackingNotifications

using BepInEx;

namespace StackingNotifications
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class StackingNotifications : BaseUnityPlugin
    {
        public const string PluginGUID = "com.paddy." + PluginName;
        public const string PluginName = "StackingNotifications";
        public const string PluginVersion = "1.0.1";

        private void Awake()
        {
            Conf.genSettings = base.Config;
            Hooks.Init();
        }

    }
}