using FungleAPI.Configuration.Attributes;
using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.BaseRole;
using TheOldUs.Roles.Sheriff;
using UnityEngine;

namespace TheOldUs.Roles
{
    internal class JesterRole : NeutralBase, ICustomRole
    {
        public ModdedTeam Team { get; } = ModdedTeam.Neutrals;
        public StringNames RoleName { get; } = new Translator("Jester").StringName;
        public StringNames RoleBlur { get; } = new Translator("When you get ejected you win.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("Try to get ejected to win.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("When the Jester get ejected he win.").StringName;
        public Color RoleColor { get; } = new Color32(173, 54, 181, byte.MaxValue);
        public RoleConfig Configuration => new RoleConfig(this)
        {
            CanVent = true,
            GhostRole = CustomRoleManager.NeutralGhost.Role
        };
        public override void OnDeath(DeathReason reason)
        {
            if (reason == DeathReason.Exile && AmongUsClient.Instance.AmHost)
            {
                EndGameHelper.RpcCustomEndGame(new List<NetworkedPlayerInfo>() { Player.Data }, RoleColor);
            }
        }

    }
}
