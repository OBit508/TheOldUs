using FungleAPI.Configuration;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Buttons;
using UnityEngine;

namespace TheOldUs.Roles.Impostors
{
    internal class VentCreatorRole : ImpostorRole, ICustomRole
    {
        [ModdedNumberOption("Create Vent Cooldown", 1, 60)]
        public static float CreateVentCooldown => 10;
        [ModdedNumberOption("Max Vents", 0, 120, 1, null, true, NumberSuffixes.None)]
        public static int MaxVents => 7;
        [ModdedNumberOption("Connect Distance", 0.5f, 10, 0.5f)]
        public static float ConnectDistance => 4;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName { get; } = new Translator("VentCreator").StringName;
        public StringNames RoleBlur { get; } = new Translator("You can create vents.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("you can create vents.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The VentCreator can create vents and he's vents will connect with all nearby vents.").StringName;
        public Color RoleColor { get; } = Color.red;
    }
}
