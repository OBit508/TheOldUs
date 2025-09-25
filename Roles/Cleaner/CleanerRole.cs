using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FungleAPI.Configuration.Attributes;
using TheOldUs.Roles.BaseRole;

namespace TheOldUs.Roles.Cleaner
{
    internal class CleanerRole : ImpostorBase, ICustomRole
    {
        [ModdedNumberOption("Clean Cooldown", null, 5, 60)]
        public static float CleanCooldown => 15;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName { get; } = new Translator("Cleaner").StringName;
        public StringNames RoleBlur { get; } = new Translator("You can clean dead bodies.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("You can help others imporstors cleaning dead bodies.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Cleaner can clean dead bodies.").StringName;
        public Color RoleColor { get; } = new Color32(47, 173, 212, byte.MaxValue);
        public RoleConfig Configuration => new RoleConfig(this)
        {
            Buttons = new CustomAbilityButton[] { CustomAbilityButton.Instance<CleanerKill>(), CustomAbilityButton.Instance<Clean>() },
            UseVanillaKillButton = false,
            GhostRole = AmongUs.GameOptions.RoleTypes.ImpostorGhost
        };
    }
}
