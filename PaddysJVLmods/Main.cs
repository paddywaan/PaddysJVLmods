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
        public const string PluginGUID = "com.jotunn.jotunnmodstub";
        public const string PluginName = "PaddysJVLmods";
        public const string PluginVersion = "0.0.2";

        private void Awake()
        {
            Hooks.Init();
        }
    }
}