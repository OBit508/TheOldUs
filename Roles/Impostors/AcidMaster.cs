using FungleAPI.Base.Roles;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.Roles.Impostors
{
    internal class AcidMaster : ImpostorBase, ICustomRole
    {
        [ModdedNumberOption("Tsunami Speed", 1, 15, 0.5f)]
        public static float TsunamiSpeed => 5;
        [ModdedNumberOption("Acid Cooldown", 5, 60)]
        public static float AcidCooldown => 15;
        [ModdedNumberOption("Acid Uses", 0, 10, 1, null, true, NumberSuffixes.None)]
        public static int AcidUses => 3;
        [ModdedNumberOption("Extra kill cooldown", 5, 60)]
        public static float ExtraKillCooldown => 15;
        [ModdedNumberOption("Deadbody dissolve delay", 5, 30)]
        public static float DissolveDelay => 15;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName { get; } = new Translator("Acid Master").StringName;
        public StringNames RoleBlur { get; } = new Translator("You can summon a acid tsunami.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("You can summon a acid tsunami that kills players.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Acid Master can summon a acid tsunami that destroy bodies and kill players.").StringName;
        public Color RoleColor { get; } = new Color32(0, 255, 8, byte.MaxValue);
        public bool UseVanillaKillButton => false;
        public bool CanKill => true;
        public DeadBodyType CreatedDeadBodyOnKill => DeadBodyType.Viper;
        public override void KillAnimSpecialSetup(DeadBody deadBody, PlayerControl killer, PlayerControl victim)
        {
            if (killer == Player)
            {
                ViperDeadBody viperDeadBody = deadBody.SafeCast<ViperDeadBody>();
                if (viperDeadBody != null)
                {
                    viperDeadBody.SetupViperInfo(DissolveDelay, Player, victim);
                }
            }
        }
    }
}
