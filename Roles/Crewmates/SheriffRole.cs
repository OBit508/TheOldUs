using FungleAPI.Base.Roles;
using FungleAPI.Components;
using FungleAPI.Configuration;
using FungleAPI.Configuration.Attributes;
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
using TheOldUs.Components;
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
        public StringNames RoleName { get; } = new Translator("Sheriff").StringName;
        public StringNames RoleBlur { get; } = new Translator("You can shoot others players.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("You can shoot others players but if you shoot an crewmate you die.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The sheriff can shoot others players but if he shoot an crewmate he die.").StringName;
        public Color RoleColor { get; } = new Color(1, (float)(204.0 / 255.0), 0, 1);
        public bool CanKill => true;
        public override Il2CppSystem.Collections.Generic.List<PlayerControl> GetValidTargets()
        {
            return GetTempPlayerList();
        }
    }
}
