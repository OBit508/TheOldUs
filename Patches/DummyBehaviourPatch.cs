using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;

namespace TheOldUs.Patches
{
    [HarmonyPatch(typeof(DummyBehaviour), "Update")]
    internal static class DummyBehaviourPatch
    {
        public static bool Prefix(DummyBehaviour __instance)
        {
            NetworkedPlayerInfo data = __instance.myPlayer.Data;
            if (data == null || data.IsDead)
            {
                return false;
            }
            if (MeetingHud.Instance)
            {
                if (!__instance.voted && !JailBehaviour.ArrestedPlayers.Contains(__instance.Player))
                {
                    __instance.voted = true;
                    __instance.StartCoroutine(__instance.DoVote());
                    return false;
                }
            }
            else
            {
                __instance.voted = false;
            }
            return false;
        }
    }
}
