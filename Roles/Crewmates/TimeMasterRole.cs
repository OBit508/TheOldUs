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
    internal class TimeMasterRole : CrewmateBase, ICustomRole
    {
        [ModdedNumberOption("Rewind Cooldown", 7, 60)]
        public static float RewindCooldown => 30;
        [ModdedNumberOption("Rewind Duration", 5, 20)]
        public static float RewindDuration => 10;
        public ModdedTeam Team { get; } = ModdedTeam.Crewmates;
        public StringNames RoleName { get; } = new Translator("Time Master").StringName;
        public StringNames RoleBlur { get; } = new Translator("Rewind the time.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("The Time Master can rewind the time.").StringName;
        public StringNames RoleBlurLong => RoleBlurMed;
        public Color RoleColor { get; } = new Color32(0, 124, 228, byte.MaxValue);
    }
}
