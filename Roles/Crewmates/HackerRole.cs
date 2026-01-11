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
using UnityEngine;

namespace TheOldUs.Roles.Crewmates
{
    internal class HackerRole : CrewmateBase, ICustomRole
    {
        [ModdedNumberOption("Teleport Cooldown", 5, 60)]
        public static float TeleportCooldown => 15;
        [ModdedNumberOption("Teleport Delay", 0, 60)]
        public static float TeleportDelay => 3;
        [ModdedNumberOption("Teleport Uses", 0, 10, 1, null, true, NumberSuffixes.None)]
        public static int TeleportUses => 5;
        [ModdedNumberOption("Unlock Vents Cooldown", 5, 60)]
        public static float UnlockVentsCooldown => 25;
        [ModdedNumberOption("Unlock Vents Duration", 5, 60)]
        public static float UnlockVentsDuration => 10;
        [ModdedNumberOption("Unlock Vents Uses", 0, 10, 1, null, true, NumberSuffixes.None)]
        public static int UnlockVentsUses => 5;
        public ModdedTeam Team { get; } = ModdedTeam.Crewmates;
        public StringNames RoleName { get; } = new Translator("Hacker").StringName;
        public StringNames RoleBlur { get; } = new Translator("You are a hacker.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("Use your powers to survive.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Hacker can Teleport and Unlock all Vents for a short time.").StringName;
        public Color RoleColor { get; } = new Color32(0, 110, 17, byte.MaxValue);
        public CustomAbilityButton Button = CustomAbilityButton.Instance<UnlockVentsButton>();
        public bool CanUseVent => Button.Transformed;
    }
}
