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
    internal class MedicRole : CrewmateBase, ICustomRole
    {
        [ModdedNumberOption("Revive Cooldown", 5, 60)]
        public static float ReviveCooldown => 25;
        [ModdedNumberOption("Revive Uses", 1, 5, 1, null, false, NumberSuffixes.None)]
        public static int ReviveUses => 3;
        public ModdedTeam Team { get; } = ModdedTeam.Crewmates;
        public StringNames RoleName => TouTranslation.MedicName;
        public StringNames RoleBlur => TouTranslation.MedicBlur;
        public StringNames RoleBlurMed => TouTranslation.MedicBlurMed;
        public Color RoleColor { get; } = TouPalette.MedicColor;
        public RoleHintType HintType => RoleHintType.MiraAPI_RoleTab;
    }
}
