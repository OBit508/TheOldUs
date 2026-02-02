using FungleAPI.Attributes;
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
        [TranslationHelper("sheriff_killCooldown")]
        [ModdedNumberOption(null, 5, 60)]
        public static float KillCooldown => 15;
        [TranslationHelper("sheriff_killUses")]
        [ModdedNumberOption(null, 0, 10, 1, null, true, NumberSuffixes.None)]
        public static float UsesCount => 0;
        [TranslationHelper("sheriff_targetDie")]
        [ModdedToggleOption(null)]
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
