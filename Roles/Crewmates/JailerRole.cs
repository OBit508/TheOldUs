using FungleAPI.Base.Roles;
using FungleAPI.Configuration;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Hud;
using FungleAPI.Role;
using FungleAPI.Teams;
using FungleAPI.Translation;
using FungleAPI.Utilities;
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
    internal class JailerRole : CrewmateBase, ICustomRole
    {
        [ModdedNumberOption("Arrest Cooldown", 5, 120)]
        public static float ArrestCooldown => 15;
        [ModdedNumberOption("Arrest Uses", 0, 30, 1, null, true, NumberSuffixes.None)]
        public static int ArrestUses => 5;
        [ModdedNumberOption("Release Cooldown", 5, 120)]
        public static float ReleaseCooldown => 15;
        public ModdedTeam Team { get; } = ModdedTeam.Crewmates;
        public StringNames RoleName => TouTranslation.JailerName;
        public StringNames RoleBlur => TouTranslation.JailerBlur;
        public StringNames RoleBlurMed => TouTranslation.JailerBlurMed;
        public Color RoleColor { get; } = TouPalette.JailerColor;
        public override bool ValidTarget(NetworkedPlayerInfo target)
        {
            return base.ValidTarget(target) && !JailBehaviour.ArrestedPlayers.Contains(target.Object);
        }
        public RoleHintType HintType => RoleHintType.MiraAPI_RoleTab;
    }
}
