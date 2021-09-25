using BepInEx;
using BepInEx.Configuration;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;

namespace TestMod
{

    //[BepInPlugin(ModGUID, ModName, ModVersion)]
    //[BepInDependency(Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.Minor)]
    //internal class ClientIncompatTest : BaseUnityPlugin
    //{
    //    private const string ModGUID = "com.jotunn.ClientIncompat";
    //    private const string ModName = "Jotunn Test Mod #3: ClientIncompat";
    //    private const string ModVersion = "0.5.0";
    //    private const string JotunnTestModConfigSection = "Test#1: ClientIncompatServer";
    //    private const string OrderConfigSection = "Order Test";

    //    public void Awake()
    //    {
    //        base.Logger.LogWarning($"{ModGUID} {ModVersion}: {ModName}");
    //        base.Logger.LogWarning($"Loading incompatible clientside test mod.");
    //        base.Logger.LogWarning($"This mod is designed to be incompatible with the server side version.");
    //    }
    //}

    //[BepInPlugin(ModGUID, ModName, ModVersion)]
    //[BepInDependency(Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.Minor)]
    //internal class ClientIncompatTest : BaseUnityPlugin
    //{
    //    private const string ModGUID = "com.jotunn.ClientIncompat";
    //    private const string ModName = "Jotunn Test Mod #3: ClientIncompat";
    //    private const string ModVersion = "0.6.0";
    //    private const string JotunnTestModConfigSection = "Test2: ClientCompatServer";
    //    private const string OrderConfigSection = "Order Test";

    //    public void Awake()
    //    {
    //        base.Logger.LogWarning($"{ModGUID} {ModVersion}: {ModName}");
    //        base.Logger.LogWarning($"Loading incompatible clientside test mod.");
    //        base.Logger.LogWarning($"This mod is designed to be incompatible with the server side version.");
    //    }
    //}

    [BepInPlugin(ModGUID, ModName, ModVersion)]
    [BepInDependency(Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.Minor)]
    internal class ClientIncompatTest : BaseUnityPlugin
    {
        private const string ModGUID = "com.jotunn.ClientIncompat";
        private const string ModName = "Jotunn Test Mod #3: ClientIncompat";
        private const string ModVersion = "1.0.0";
        private const string JotunnTestModConfigSection = "Test#3: ClientCompatMajor";
        private const string OrderConfigSection = "Order Test";

        public void Awake()
        {
            base.Logger.LogWarning($"{ModGUID} {ModVersion}: {ModName}");
            base.Logger.LogWarning($"Loading incompatible clientside test mod.");
            base.Logger.LogWarning($"This mod is designed to be incompatible with the server side version.");
        }
    }

}
