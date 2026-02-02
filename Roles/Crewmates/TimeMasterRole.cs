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
using TheOldUs.TOU;
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
        public StringNames RoleName => TouTranslation.TimeMasterName;
        public StringNames RoleBlur => TouTranslation.TimeMasterBlur;
        public StringNames RoleBlurMed => TouTranslation.TimeMasterBlurMed;
        public Color RoleColor { get; } = TouPalette.TimeMasterColor;
        public RoleHintType HintType => RoleHintType.MiraAPI_RoleTab;
    }
}
