using FungleAPI.Utilities;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;

namespace TheOldUs.Patches
{
    [HarmonyPatch(typeof(UseButton), "FixedUpdate")]
    internal static class UseButtonPatch
    {
        public static void Postfix(UseButton __instance)
        {
            BetterDoorHelper helper = __instance.currentTarget.SafeCast<BetterDoorHelper>();
            if (helper != null && helper.timer > 0)
            {
                __instance.SetCoolDown(helper.timer, 3);
            }
        }
    }
}
