using AmongUs.GameOptions;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using FungleAPI.Base.Rpc;
using FungleAPI.Components;
using FungleAPI.Networking;
using FungleAPI.Player;
using FungleAPI.Utilities;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.RPCs
{
    internal class RpcRevive : AdvancedRpc<DeadBody>
    {
        public static void Revive(DeadBody value)
        {
            value.myCollider.enabled = false;
            foreach (SpriteRenderer rend in value.bodyRenderers)
            {
                rend.gameObject.SetActive(false);
            }
            PlayerControl playerControl = Helpers.GetPlayerById(value.ParentId);
            if (playerControl != null)
            {
                PlayerHelper playerHelper = playerControl.GetPlayerComponent<PlayerHelper>();
                playerHelper.StartCoroutine(CoRevive(playerHelper, RoleManager.Instance.GetRole(playerHelper.OldRole.Role)).WrapToIl2Cpp());
            }
        }
        public static System.Collections.IEnumerator CoRevive(PlayerHelper playerHelper, RoleBehaviour oldRole)
        {
            yield return playerHelper.player.CoSetRole(oldRole.Role, false);
            playerHelper.OldRole = oldRole;
        }
        public override void Write(MessageWriter messageWriter, DeadBody value)
        {
            messageWriter.Write(value.ParentId);
            Revive(value);
        }
        public override void Handle(MessageReader messageReader)
        {
            DeadBody deadBody = Helpers.GetBodyById(messageReader.ReadByte());
            if (deadBody != null)
            {
                Revive(deadBody);
            }
        }
    }
}
