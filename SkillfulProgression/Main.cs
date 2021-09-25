// SkillfulProgression
// a Valheim mod skeleton using Jötunn
// 
// File:    SkillfulProgression.cs
// Project: SkillfulProgression

using BepInEx;

namespace SkillfulProgression
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class SkillfulProgression : BaseUnityPlugin
    {
        public const string PluginGUID = "com.paddy." + PluginName;
        public const string PluginName = "SkillfulProgression";
        public const string PluginVersion = "1.0.3";

        private void Awake()
        {
            Conf.genSettings = base.Config;
            Hooks.Init();
        }
    }
}