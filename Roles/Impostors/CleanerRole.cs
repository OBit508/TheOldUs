using FungleAPI.Configuration;
using FungleAPI.Configuration.Attributes;
using FungleAPI.GameOver;
using FungleAPI.GameOver.Ends;
using FungleAPI.Hud;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Buttons;
using UnityEngine;

namespace TheOldUs.Roles.Impostors
{
    internal class CleanerRole : ImpostorRole, ICustomRole
    {
        [ModdedNumberOption("Clean Cooldown", 5, 60)]
        public static float CleanCooldown => 15;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName { get; } = new Translator("Cleaner").StringName;
        public StringNames RoleBlur { get; } = new Translator("You can clean dead bodies.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("You can help others imporstors cleaning dead bodies.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Cleaner can clean dead bodies.").StringName;
        public Color RoleColor { get; } = new Color32(47, 173, 212, byte.MaxValue);
        public bool UseVanillaKillButton => false;
        public bool CanKill => PlayerControl.AllPlayerControls.FindAll(FungleAPI.Utilities.Il2CppUtils.ToIl2CppPredicate(new Predicate<PlayerControl>(p => p.Data.Role.GetTeam() == Team))).Count <= 1;
    }
}
