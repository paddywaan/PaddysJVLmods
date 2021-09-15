﻿// PaddysJVLmods
// a Valheim mod skeleton using Jötunn
// 
// File:    PaddysJVLmods.cs
// Project: PaddysJVLmods

using BepInEx;
using BepInEx.Configuration;
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
        public static ConfigFile _conf;

        private void Awake()
        {
            
            _conf = base.Config;
            Hooks.Init();
        }
    }
}