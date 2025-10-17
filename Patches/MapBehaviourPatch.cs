using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.TOU;

namespace TheOldUs.Patches
{
    [HarmonyPatch(typeof(MapBehaviour), "FixedUpdate")]
    internal static class MapBehaviourPatch
    {
        public static void Postfix(MapBehaviour __instance)
        {
            if (TOUSettings.InvertY)
            {
                Vector3 pos = __instance.HerePoint.transform.localPosition;
                pos.y *= -1;
                __instance.HerePoint.transform.localPosition = pos;
            }
        }
    }
}
