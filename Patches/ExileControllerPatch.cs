using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.TOU;

namespace TheOldUs.Patches
{
    [HarmonyPatch(typeof(ExileController), "Begin")]
    internal static class ExileControllerPatch
    {
        public static bool Prefix(ExileController __instance, [HarmonyArgument(0)] ExileController.InitProperties init)
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
                __instance.Player.gameObject.SetActive(false);
                __instance.ImpostorText.color = new UnityEngine.Color(0, 0, 0, 0);
                __instance.EjectSound = null;
                if (init != null && init.outfit != null)
                {
                    __instance.completeString = init.outfit.PlayerName + " was sent to the jail.";
                    __instance.Player.UpdateFromPlayerOutfit(init.outfit, PlayerMaterial.MaskType.Exile, false, false, new Action(delegate
                    {
                        SkinViewData skinViewData;
                        if (GameManager.Instance != null)
                        {
                            skinViewData = ShipStatus.Instance.CosmeticsCache.GetSkin(__instance.initData.outfit.SkinId);
                        }
                        else
                        {
                            skinViewData = __instance.Player.GetSkinView();
                        }
                        if (GameManager.Instance != null && !DestroyableSingleton<HatManager>.Instance.CheckLongModeValidCosmetic(init.outfit.SkinId, __instance.Player.GetIgnoreLongMode()))
                        {
                            skinViewData = ShipStatus.Instance.CosmeticsCache.GetSkin("skin_None");
                        }
                        if (__instance.useIdleAnim)
                        {
                            __instance.Player.FixSkinSprite(skinViewData.IdleFrame);
                            return;
                        }
                        __instance.Player.FixSkinSprite(skinViewData.EjectFrame);
                    }), false);
                    __instance.Player.ToggleName(false);
                    if (!__instance.useIdleAnim)
                    {
                        __instance.Player.SetCustomHatPosition(__instance.exileHatPosition);
                        __instance.Player.SetCustomVisorPosition(__instance.exileVisorPosition);
                    }
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
                    __instance.Player.gameObject.SetActive(false);
                }
                __instance.StartCoroutine(__instance.Animate());
                return false;
            }
            return true;
        }
    }
}
