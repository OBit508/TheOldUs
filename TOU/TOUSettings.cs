using FungleAPI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FungleAPI.Configuration.Attributes;

namespace TheOldUs.TOU
{
    public class TouSettings : ModSettings
    {
        public class Jail : SettingsGroup
        {
            [ModdedToggleOption("When ejected players go to jail")]
            public static bool ArrestWhenEjected => true;
        }
        public class Ship : SettingsGroup
        {
            [ModdedToggleOption("Invert X")]
            public static bool InvertX => false;
            [ModdedToggleOption("Invert Y")]
            public static bool InvertY => false;
            [ModdedToggleOption("Better Doors")]
            public static bool BetterDoors => true;
        }
    }
}
