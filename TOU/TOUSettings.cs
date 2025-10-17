using FungleAPI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FungleAPI.Configuration.Attributes;

namespace TheOldUs.TOU
{
    public class TOUSettings : ModSettings
    {
        [SettingsGroup("Jail")]
        public static string JailId => "__jail";
        [ModdedToggleOption("When ejected players go to jail", "__jail")]
        public static bool ArrestWhenEjected => true;
        [SettingsGroup("Ship")]
        public static string ShipId => "__ship";
        [ModdedToggleOption("Invert X", "__ship")]
        public static bool InvertX => false;
        [ModdedToggleOption("Invert Y", "__ship")]
        public static bool InvertY => false;
        [ModdedToggleOption("Better Doors", "__ship")]
        public static bool BetterDoors => true;
    }
}
