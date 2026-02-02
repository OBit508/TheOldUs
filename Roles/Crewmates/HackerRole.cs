using FungleAPI.Attributes;
using FungleAPI.Base.Roles;
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
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Roles.Crewmates
{
    internal class HackerRole : CrewmateBase, ICustomRole
    {
        [TranslationHelper("hacker_teleportCooldown")]
        [ModdedNumberOption(null, 5, 60)]
        public static float TeleportCooldown => 15;
        [TranslationHelper("hacker_teleportDelay")]
        [ModdedNumberOption(null, 0, 60)]
        public static float TeleportDelay => 3;
        [TranslationHelper("hacker_teleportUses")]
        [ModdedNumberOption(null, 0, 10, 1, null, true, NumberSuffixes.None)]
        public static int TeleportUses => 5;
        [TranslationHelper("hacker_unlockVentsCooldown")]
        [ModdedNumberOption("Unlock Vents Cooldown", 5, 60)]
        public static float UnlockVentsCooldown => 25;
        [TranslationHelper("hacker_unlockVentsDuration")]
        [ModdedNumberOption("Unlock Vents Duration", 5, 60)]
        public static float UnlockVentsDuration => 10;
        [TranslationHelper("hacker_unlockVentsUses")]
        [ModdedNumberOption("Unlock Vents Uses", 0, 10, 1, null, true, NumberSuffixes.None)]
        public static int UnlockVentsUses => 5;
        public ModdedTeam Team { get; } = ModdedTeam.Crewmates;
        public StringNames RoleName => TouTranslation.HackerName;
        public StringNames RoleBlur => TouTranslation.HackerBlur;
        public StringNames RoleBlurMed => TouTranslation.HackerBlurMed;
        public Color RoleColor { get; } = TouPalette.HackerColor;
        public CustomAbilityButton Button = CustomButton<UnlockVentsButton>.Instance;
        public bool CanUseVent => Button.Transformed;
        public RoleHintType HintType => RoleHintType.MiraAPI_RoleTab;
    }
}
