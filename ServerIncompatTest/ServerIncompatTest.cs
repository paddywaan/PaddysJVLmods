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

    [BepInPlugin(ModGUID, ModName, ModVersion)]
    [BepInDependency(Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class ServerIncompatTest : BaseUnityPlugin
    {
        private const string ModGUID = "com.jotunn.ClientIncompat";
        private const string ModName = "Jotunn Test Mod #3: ClientIncompat";
        private const string ModVersion = "0.6.0";

        public void Awake()
        {
            base.Logger.LogWarning($"{ModGUID} {ModVersion}: {ModName}");
            base.Logger.LogWarning($"Loading Serverside mod to reject incompat client");
            base.Logger.LogWarning($"This mod is designed to be incompatible with the client side version.");
        }
    }
}
