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
    [HarmonyPatch(typeof(PlayerControl), "Exiled")]
    internal static class PlayerControlPatch
    {
        public static bool Prefix(PlayerControl __instance)
        {
            if (TOUSettings.Jail.ArrestWhenEjected)
            {
                JailBehaviour.ArrestedPlayers.Add(__instance);
                __instance.NetTransform.SnapTo(new Vector2(TOUSettings.Ship.InvertX ? 12 : -12, TOUSettings.Ship.InvertY ? -3 : 3));
                if (__instance.Data.RoleType == CustomRoleManager.GetType<JesterRole>())
                {
                    __instance.Data.Role.OnDeath(DeathReason.Exile);
                }
                return false;
            }
            return true;
        }
    }
}
