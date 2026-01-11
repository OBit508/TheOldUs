using FungleAPI.Base.Roles;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Player;
using FungleAPI.Role;
using FungleAPI.Teams;
using FungleAPI.Translation;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using UnityEngine;

namespace TheOldUs.Roles.Neutrals
{
    internal class ArsonistRole : NeutralBase, ICustomRole
    {
        [ModdedNumberOption("Gasoline Cooldown", 5, 50)]
        public static int GasolineCooldown => 20;
        public ModdedTeam Team { get; } = ModdedTeam.Neutrals;
        public StringNames RoleName { get; } = new Translator("Arsonist").StringName;
        public StringNames RoleBlur { get; } = new Translator("Put gas on others.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("Put gasoline on others and light it.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Arsonist needs to put gasoline on all the players in order to start a fire.").StringName;
        public Color RoleColor { get; } = new Color32(173, 95, 5, byte.MaxValue);
        public override bool IsValidTarget(NetworkedPlayerInfo target)
        {
            return base.IsValidTarget(target) && !target.Object.GetPlayerComponent<RoleHelper>().Soaked;
        }
    }
}
