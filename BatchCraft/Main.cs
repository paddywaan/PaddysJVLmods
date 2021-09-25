// BatchCraft
// a Valheim mod skeleton using Jötunn
// 
// File:    BatchCraft.cs
// Project: BatchCraft

using BepInEx;
using BepInEx.Logging;
using UnityEngine;

namespace BatchCraft
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class Main : BaseUnityPlugin
    {
        public const string PluginGUID = "com.jotunn.BatchCraft";
        public const string PluginName = "BatchCraft";
        public const string PluginVersion = "0.0.1";
        internal static ManualLogSource logger;

        public void Awake()
        {
            logger = base.Logger;
            Hooks.Init();
            logger.LogWarning($"Loaded");
        }
    }
}