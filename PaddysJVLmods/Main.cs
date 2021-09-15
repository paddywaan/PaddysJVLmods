// PaddysJVLmods
// a Valheim mod skeleton using Jötunn
// 
// File:    PaddysJVLmods.cs
// Project: PaddysJVLmods

using BepInEx;
using Jotunn.Utils;

namespace PaddysJVLmods
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Major)]
    internal class PaddysJVLmods : BaseUnityPlugin
    {
        public const string PluginGUID = "com.paddy." + PluginName;
        public const string PluginName = "PaddysJVLmods";
        public const string PluginVersion = "1.0.2";

        private void Awake()
        {
            Conf.genSettings = base.Config;
            Hooks.Init();
        }
    }
}