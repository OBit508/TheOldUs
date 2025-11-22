using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Patches
{
    [HarmonyPatch(typeof(ExileController))]
    internal static class ExileControllerPatch
    {
        [HarmonyPatch("WrapUp")]
        [HarmonyPrefix]
        public static bool WrapUpPrefix(ExileController __instance)
        {
            if (__instance.initData.networkedPlayer != null)
            {
                PlayerControl @object = __instance.initData.networkedPlayer.Object;
                if (@object)
                {
                    @object.Exiled();
                }
                __instance.initData.networkedPlayer.IsDead = !TOUSettings.Jail.ArrestWhenEjected;
            }
            if (DestroyableSingleton<TutorialManager>.InstanceExists || (GameManager.Instance != null && !GameManager.Instance.LogicFlow.IsGameOverDueToDeath()))
            {
                __instance.ReEnableGameplay();
            }
            GameObject.Destroy(__instance.gameObject);
            return false;
        }
        [HarmonyPatch("Begin")]
        [HarmonyPrefix]
        public static bool BeginPrefix(ExileController __instance, [HarmonyArgument(0)] ExileController.InitProperties init)
        {
            if (TOUSettings.Jail.ArrestWhenEjected)
            {
                if (__instance.specialInputHandler != null)
                {
                    __instance.specialInputHandler.disableVirtualCursor = true;
                }
                ExileController.Instance = __instance;
                ControllerManager.Instance.CloseAndResetAll();
                __instance.initData = init;
                __instance.Text.gameObject.SetActive(false);
                __instance.Text.text = string.Empty;
                if (DestroyableSingleton<HudManager>.InstanceExists)
                {
                    DestroyableSingleton<HudManager>.Instance.SetMapButtonEnabled(false);
                }
                __instance.ImpostorText.gameObject.SetActive(false);
                __instance.Player.gameObject.SetActive(false);
                if (init != null && init.outfit != null)
                {
                    __instance.completeString = init.outfit.PlayerName + " was sent to jail.";
                }
                else
                {
                    if (init.voteTie)
                    {
                        __instance.completeString = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.NoExileTie);
                    }
                    else
                    {
                        __instance.completeString = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.NoExileSkip);
                    }
                }
                __instance.StartCoroutine(__instance.Animate());
                return false;
            }
            return true;
        }
    }
}
