﻿using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackingNotifications
{
    public static class Conf
    {
        public static ConfigFile genSettings;

        static Conf()
        {
            genSettings.Bind("SETTINGS", "NEW SETTING", "", "Does things");
        }
    }
}