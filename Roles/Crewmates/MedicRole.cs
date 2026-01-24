using FungleAPI.Base.Roles;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Role;
using FungleAPI.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.Roles.Crewmates
{
    internal class MedicRole : CrewmateBase, ICustomRole
    {
        [ModdedNumberOption("Revive Cooldown", 5, 60)]
        public static float ReviveCooldown => 25;
        [ModdedNumberOption("Revive Uses", 1, 5, 1, null, false, NumberSuffixes.None)]
        public static int ReviveUses => 3;
        public ModdedTeam Team { get; } = ModdedTeam.Crewmates;
        public StringNames RoleName { get; } = new Translator("Medic").StringName;
        public StringNames RoleBlur { get; } = new Translator("Revive dead bodies.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("The Medic can revive dead bodies.").StringName;
        public StringNames RoleBlurLong => RoleBlurMed;
        public Color RoleColor { get; } = new Color32(40, 165, 0, byte.MaxValue);
    }
}
