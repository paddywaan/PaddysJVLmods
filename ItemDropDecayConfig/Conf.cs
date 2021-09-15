using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemDropDecayConfig
{
    public static class Conf
    {
        public static ConfigFile genSettings;
        public static ConfigEntry<double> DecayTimer;

        static Conf()
        {
            DecayTimer = genSettings.Bind("SETTINGS", "Decay Timer", 3600d, "Time in seconds before an item decays. Only affects drops outside of base (crafting station/fire) and player radius.");
        }
    }
}