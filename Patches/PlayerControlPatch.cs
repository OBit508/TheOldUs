using AmongUs.Data;
using AmongUs.Data.Player;
using FungleAPI.Role;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.Neutrals;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Patches
{
    [HarmonyPatch(typeof(PlayerControl), "Die")]
    internal static class PlayerControlPatch
    {
        public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] DeathReason reason, [HarmonyArgument(1)]  bool assignGhostRole)
        {
            if (reason == DeathReason.Exile && TOUSettings.ArrestWhenEjected)
            {
                JailBehaviour.ArrestedPlayers.Add(__instance);
                __instance.NetTransform.SnapTo(new Vector2(-12, 3));
                if (__instance.Data.RoleType == CustomRoleManager.GetType<JesterRole>())
                {
                    __instance.Data.Role.OnDeath(reason);
                }
                return false;
            }
            __instance.logger.Debug(string.Format("Player {0} dying for reason {1}", __instance.PlayerId, reason), null);
            if (!DestroyableSingleton<TutorialManager>.InstanceExists && __instance.AmOwner)
            {
                PlayerBanData ban = DataManager.Player.Ban;
                float banPoints = ban.BanPoints;
                ban.BanPoints = banPoints - 1f;
                DataManager.Player.Ban.PreviousGameStartDate = Il2CppSystem.DateTime.MinValue;
            }
            GameData.LastDeathReason = reason;
            if (__instance.inMovingPlat)
            {
                FungleShipStatus fungleShipStatus = ShipStatus.Instance as FungleShipStatus;
                if (fungleShipStatus != null)
                {
                    fungleShipStatus.Zipline.CancelZiplineUseForPlayer(__instance);
                }
            }
            __instance.cosmetics.AnimatePetMourning();
            __instance.FixMixedUpOutfit();
            __instance.Data.IsDead = true;
            __instance.clickKillCollider.enabled = false;
            __instance.gameObject.layer = LayerMask.NameToLayer("Ghost");
            __instance.cosmetics.SetNameMask(false);
            __instance.cosmetics.PettingHand.StopPetting();
            if (__instance.walkingToVent)
            {
                __instance.inVent = false;
                Vent.currentVent = null;
                __instance.moveable = true;
                __instance.MyPhysics.StopAllCoroutines();
            }
            __instance.Data.Role.OnDeath(reason);
            GameManager.Instance.OnPlayerDeath(__instance, assignGhostRole);
            if (__instance.AmOwner)
            {
                DestroyableSingleton<HudManager>.Instance.Chat.SetVisible(true);
                DestroyableSingleton<HudManager>.Instance.ShadowQuad.gameObject.SetActive(false);
                __instance.AdjustLighting();
                foreach (PlayerControl playerControl in PlayerControl.AllPlayerControls)
                {
                    playerControl.cosmetics.ToggleNameVisible(GameManager.Instance.LogicOptions.GetShowCrewmateNames());
                }
            }
            return false;
        }
    }
}
