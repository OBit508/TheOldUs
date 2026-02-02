using FungleAPI.Base.Roles;
using FungleAPI.Components;
using FungleAPI.Configuration;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Hud;
using FungleAPI.Role;
using FungleAPI.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Buttons;
using TheOldUs.Components;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Roles.Crewmates
{
    internal class SheriffRole : CrewmateBase, ICustomRole
    {
        [ModdedNumberOption("Kill Cooldown", 5, 60)]
        public static float KillCooldown => 15;
        [ModdedNumberOption("Uses Count", 0, 10, 1, null, true, NumberSuffixes.None)]
        public static float UsesCount => 0;
        [ModdedToggleOption("Target die too")]
        public static bool TargetDie => false;
        public ModdedTeam Team { get; } = ModdedTeam.Crewmates;
        public StringNames RoleName => TouTranslation.SheriffName;
        public StringNames RoleBlur => TouTranslation.SheriffBlur;
        public StringNames RoleBlurMed => TouTranslation.SheriffBlurMed;
        public Color RoleColor { get; } = TouPalette.SheriffColor;
        public bool CanKill => true;
        public RoleHintType HintType => RoleHintType.MiraAPI_RoleTab;
    }
}
