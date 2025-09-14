using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using UnityEngine;
using FungleAPI.Configuration.Attributes;
using TheOldUs.Assets;
using TheOldUs.Components;
using FungleAPI.Components;

namespace TheOldUs.Roles.Sheriff
{
    public class SheriffRole : TOUBaseRole, ICustomRole
    {
        [ModdedNumberOption("Kill Cooldown", null, 5, 60)]
        public static float KillCooldown => 15;
        [ModdedNumberOption("Uses Count", null, 0, 10, 1, null, true, NumberSuffixes.None)]
        public static float UsesCount => 0;
        [ModdedToggleOption("Target die too", null)]
        public static bool TargetDie => false;
        public ModdedTeam Team { get; } = ModdedTeam.Crewmates;
        public StringNames RoleName { get; } = new Translator("Sheriff").StringName;
        public StringNames RoleBlur { get; } = new Translator("You can shoot others players.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("You can shoot others players but if you shoot an crewmate you die.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The sheriff can shoot others players but if he shoot an crewmate he die.").StringName;
        public Color RoleColor { get; } = new Color32(254, 153, 0, byte.MaxValue);
        public RoleConfig Configuration => new RoleConfig(this)
        {
            CanKill = true,
            Buttons = new CustomAbilityButton[] { CustomAbilityButton.Instance<SheriffKill>() },
        };
    }
}
