using FungleAPI.Base.Roles;
using FungleAPI.Event.Types;
using FungleAPI.GameOver;
using FungleAPI.Networking;
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
using TheOldUs.RPCs;
using UnityEngine;

namespace TheOldUs.Roles.Neutrals
{
    internal class ExecutionerRole : NeutralBase, ICustomRole
    {
        public static Color ExecutionerColor;
        public ModdedTeam Team { get; } = ModdedTeam.Neutrals;
        public StringNames RoleName { get; } = new Translator("Executioner").StringName;
        public StringNames RoleBlur { get; } = new Translator("Make your target get ejected to win.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("Make your target get ejected to win.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Executioner need to make the target get ejected to win.").StringName;
        public Color RoleColor => ExecutionerColor;
        public void Start()
        {
            if (Player != null && Player.AmOwner)
            {
                Rpc<RpcSetTarget>.Instance.Send(Player);
            }
        }
        public static void SetRole(OnSetRole onSetRole)
        {
            if (onSetRole.Role.Role != CustomRoleManager.GetType<ExecutionerRole>())
            {
                onSetRole.Player.GetPlayerComponent<RoleHelper>().Target = null;
            }
        }
        public static void PlayerEjected(OnPlayerDie onPlayerDie)
        {
            if (AmongUsClient.Instance.AmHost && onPlayerDie.Reason == DeathReason.Exile && onPlayerDie.Player != null)
            {
                List<NetworkedPlayerInfo> winners = new List<NetworkedPlayerInfo>();
                foreach (PlayerControl playerControl in PlayerControl.AllPlayerControls)
                {
                    if (playerControl.GetPlayerComponent<RoleHelper>().Target == onPlayerDie.Player)
                    {
                        winners.Add(playerControl.Data);
                    }
                }
                GameManager.Instance.RpcEndGame(winners, "Executioner's victory", ExecutionerColor, ExecutionerColor);
            }
        }
    }
}
