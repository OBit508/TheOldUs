using FungleAPI.Utilities;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Patches
{
    [HarmonyPatch(typeof(PlayerVoteArea), "SetDead")]
    internal static class PlayerVoteAreaPatch
    {
        public static Sprite XMark;
        public static void Postfix(PlayerVoteArea __instance)
        {
            if (XMark == null)
            {
                XMark = __instance.XMark.sprite;
            }
            __instance.XMark.sprite = JailBehaviour.ArrestedPlayers.Contains(Helpers.GetPlayerById(__instance.TargetPlayerId)) ? TouAssets.Cuffs : XMark;
        }
    }
}
