using FungleAPI.Attributes;
using FungleAPI.Configuration;
using FungleAPI.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheOldUs.TOU
{
    public class TouSettings : ModSettings
    {
        [TranslationHelper("tousettings_jail")]
        public class Jail : SettingsGroup
        {
            [TranslationHelper("tousettings_jail_arrestWhenEjected")]
            [ModdedToggleOption("When ejected players go to jail")]
            public static bool ArrestWhenEjected => true;
        }
        [TranslationHelper("tousettings_ship")]
        public class Ship : SettingsGroup
        {
            [TranslationHelper("tousettings_ship_invertX")]
            [ModdedToggleOption("Invert X")]
            public static bool InvertX => false;
            [TranslationHelper("tousettings_ship_invertY")]
            [ModdedToggleOption("Invert Y")]
            public static bool InvertY => false;
            [TranslationHelper("tousettings_ship_betterDoors")]
            [ModdedToggleOption("Better Doors")]
            public static bool BetterDoors => true;
        }
    }
}
