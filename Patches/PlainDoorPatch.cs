using FungleAPI.Utilities;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using UnityEngine;

namespace TheOldUs.Patches
{
    [HarmonyPatch(typeof(PlainDoor))]
    internal static class PlainDoorPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostfix(PlainDoor __instance)
        {
            if (__instance.SafeCast<AutoOpenDoor>() != null)
            {
                BoxCollider2D betterDoor = new GameObject("BetterDoor").AddComponent<BoxCollider2D>();
                betterDoor.transform.SetParent(__instance.transform);
                betterDoor.transform.localPosition = Vector3.zero;
                betterDoor.transform.localScale = Vector3.one;
                betterDoor.size *= 2;
                betterDoor.isTrigger = true;
                betterDoor.gameObject.AddComponent<BetterDoorHelper>();
            }
        }
        [HarmonyPatch("SetDoorway")]
        [HarmonyPrefix]
        public static bool SetDoorwayPrefix(PlainDoor __instance, [HarmonyArgument(0)] bool open)
        {
            try
            {
                BetterDoorHelper helper = __instance.transform.GetChild(1).GetComponent<BetterDoorHelper>();
                if (helper != null)
                {
                    __instance.Open = open;
                    if (open != helper.Open)
                    {
                        helper.SetDoorway();
                    }
                    return false;
                }
            }
            catch
            {
            }
            return false;
        }
    }
}
