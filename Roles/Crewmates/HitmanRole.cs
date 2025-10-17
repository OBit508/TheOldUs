using FungleAPI.Configuration.Attributes;
using FungleAPI.Hud;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Buttons;
using TheOldUs.Roles.BaseRole;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Roles.Crewmates
{
    internal class HitmanRole : CrewmateBase, ICustomRole
    {
        [ModdedNumberOption("Reload Cooldown", null, 5, 120)]
        public static float ReloadCooldown => 15;
        [ModdedNumberOption("Reload Uses", null, 0, 30, 1, null, true, NumberSuffixes.None)]
        public static int ReloadUses => 5;
        public ModdedTeam Team { get; } = ModdedTeam.Crewmates;
        public StringNames RoleName { get; } = new Translator("Hitman").StringName;
        public StringNames RoleBlur { get; } = new Translator("Shoot the impostors.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("Use your gun to shoot the impostors.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Hitman can use a gun to shoot players.").StringName;
        public Color RoleColor { get; } = Palette.Orange;
        public List<CustomAbilityButton> Buttons { get; } = new List<CustomAbilityButton>() { };
    }
}
