using FungleAPI.Configuration.Attributes;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.BaseRole;
using TheOldUs.Roles.Sheriff;
using UnityEngine;

namespace TheOldUs.Roles.Jailer
{
    internal class JailerRole : CrewmateBase, ICustomRole
    {
        [ModdedNumberOption("Arrest Cooldown", null, 5, 120)]
        public static float ArrestCooldown => 15;
        [ModdedNumberOption("Arrest Uses", null, 0, 30, 1, null, true, NumberSuffixes.None)]
        public static int ArrestUses => 5;
        [ModdedNumberOption("Release Cooldown", null, 5, 120)]
        public static float ReleaseCooldown => 15;
        [ModdedNumberOption("Release Uses", null, 0, 30, 1, null, true, NumberSuffixes.None)]
        public static int ReleaseUses => 5;
        public ModdedTeam Team { get; } = ModdedTeam.Crewmates;
        public StringNames RoleName { get; } = new Translator("Jailer").StringName;
        public StringNames RoleBlur { get; } = new Translator("You need to arrest all the impostors.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("You need to arrest all the impostors to win.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Jailer can arrest any player and if he want to he can release any player on the jail.").StringName;
        public Color RoleColor { get; } = Color.blue;
        public RoleConfig Configuration => new RoleConfig(this)
        {
            Buttons = new CustomAbilityButton[] { CustomAbilityButton.Instance<JailerArrest>(), CustomAbilityButton.Instance<JailerRelease>() },
            GhostRole = AmongUs.GameOptions.RoleTypes.CrewmateGhost
        };
    }
}
