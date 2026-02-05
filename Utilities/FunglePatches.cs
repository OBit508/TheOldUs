using AmongUs.Data;
using FungleAPI.Event;
using FungleAPI.Event.Types;
using FungleAPI.GameOver;
using FungleAPI.GameOver.Ends;
using FungleAPI.Hud;
using FungleAPI.Player;
using FungleAPI.Role;
using FungleAPI.Teams;
using FungleAPI.Translation;
using FungleAPI.Utilities;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Patches;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Utilities
{
    internal static class FunglePatches
    {
        public static void PatchFungleAPI()
        {
            HarmonyHelper.Remove_FungleAPI_HarmonyLib_Patch(typeof(Vent).GetMethod("CanUse"), "VentPatch", "CanUsePrefix");
            HarmonyHelper.Remove_FungleAPI_HarmonyLib_Patch(typeof(ExileController).GetMethod("Begin"), "ExileControllerPatch", "BeginPostfix");
            HarmonyHelper.Remove_FungleAPI_HarmonyLib_Patch(typeof(LogicGameFlowNormal).GetMethod("CheckEndCriteria"), "LogicGameFlowNormalPatch", "Prefix");
        }
        public static bool CanVentOrig(this RoleBehaviour roleBehaviour)
        {
            if (roleBehaviour.CustomRole() != null)
            {
                return roleBehaviour.CustomRole().CanKill;
            }
            return roleBehaviour.CanUseKillButton;
        }
        public static bool ForceVentUse(this Vent vent)
        {
            return ShipStatusPatch.AcidVents.Contains(vent) && AcidTsunami.Instance != null;
        }
        [HarmonyPatch(typeof(ExileController))]
        public class ExileControllerPatch
        {
            public static MethodInfo OnEject_Controller_Set = typeof(OnEject).GetProperty("Controller").GetSetMethod(true);
            public static MethodInfo OnEject_Target_Set = typeof(OnEject).GetProperty("Target").GetSetMethod(true);
            [HarmonyPatch("Begin")]
            [HarmonyPostfix]
            public static void BeginPostfix(ExileController __instance)
            {
                if (!TouSettings.Jail.ArrestWhenEjected)
                {
                    if (__instance.initData.networkedPlayer != null && __instance.initData.networkedPlayer.Role != null && __instance.initData.networkedPlayer.Role.CustomRole() != null && GameOptionsManager.Instance.currentNormalGameOptions.GetBool(AmongUs.GameOptions.BoolOptionNames.ConfirmImpostor))
                    {
                        __instance.completeString = __instance.initData.networkedPlayer.Role.CustomRole().ExileText(__instance);
                    }
                    __instance.ImpostorText.text = FungleTranslation.TeamsRemainText.GetString();
                    Dictionary<ModdedTeam, ChangeableValue<int>> teams = new Dictionary<ModdedTeam, ChangeableValue<int>>();
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                    {
                        if (player.Data != __instance.initData.networkedPlayer && !player.Data.IsDead)
                        {
                            ModdedTeam team = player.Data.Role.GetTeam();
                            if (teams.ContainsKey(team))
                            {
                                teams[team].Value++;
                            }
                            else
                            {
                                teams.Add(team, new ChangeableValue<int>(1));
                            }
                        }
                    }
                    foreach (KeyValuePair<ModdedTeam, ChangeableValue<int>> pair in teams)
                    {
                        __instance.ImpostorText.text += pair.Value.Value.ToString() + " " + pair.Key.TeamColor.ToTextColor() + (pair.Value.Value == 1 ? pair.Key.TeamName.GetString() : pair.Key.PluralName.GetString()) + "</color>" + (pair.Key == teams.Last().Key ? "" : ", ");
                    }
                }
                OnEject onEject = new OnEject();
                OnEject_Controller_Set.Invoke(onEject, new object[] { __instance });
                OnEject_Target_Set.Invoke(onEject, new object[] { __instance.initData.networkedPlayer });
                EventManager.CallEvent(onEject);
            }
        }
        [HarmonyPatch(typeof(CustomRoleManager))]
        public class CustomRoleManagerPatch
        {
            [HarmonyPatch("CanVent")]
            [HarmonyPrefix]
            public static bool CanVentPrefix([HarmonyArgument(0)] RoleBehaviour roleBehaviour, ref bool __result)
            {
                if (!roleBehaviour.IsDead && AcidTsunami.Instance != null)
                {
                    __result = true;
                    return false;
                }
                __result = roleBehaviour.CanVentOrig();
                return false;
            }
        }
        [HarmonyPatch(typeof(Vent), "CanUse")]
        public class VentPatch
        {
            public static bool Prefix(Vent __instance, NetworkedPlayerInfo pc, ref bool canUse, ref bool couldUse, ref float __result)
            {
                float num = float.MaxValue;
                PlayerControl @object = pc.Object;
                bool playerCanVent = pc.Role.CanVent() && !ShipStatusPatch.AcidVents.Contains(__instance) || __instance.ForceVentUse();
                couldUse = playerCanVent && GameManager.Instance.LogicUsables.CanUse(__instance.SafeCast<IUsable>(), @object) && pc.Role.CanUse(__instance.SafeCast<IUsable>()) && (!@object.MustCleanVent(__instance.Id) || (@object.inVent && Vent.currentVent == __instance)) && !pc.IsDead && (@object.CanMove || @object.inVent);
                ISystemType systemType;
                if (ShipStatus.Instance.Systems.TryGetValue(SystemTypes.Ventilation, out systemType))
                {
                    VentilationSystem ventilationSystem = systemType.SafeCast<VentilationSystem>();
                    if (ventilationSystem != null && ventilationSystem.IsVentCurrentlyBeingCleaned(__instance.Id))
                    {
                        couldUse = false;
                    }
                }
                canUse = couldUse;
                if (canUse)
                {
                    Vector3 center = @object.Collider.bounds.center;
                    Vector3 position = __instance.transform.position;
                    num = Vector2.Distance(center, position);
                    canUse &= num <= __instance.UsableDistance && !PhysicsHelpers.AnythingBetween(@object.Collider, center, position, Constants.ShipOnlyMask, false);
                }
                __result = num;
                return false;
            }
        }
        [HarmonyPatch(typeof(LogicGameFlowNormal), "CheckEndCriteria")]
        public class LogicGameFlowNormalPatch
        {
            public static bool Prefix()
            {
                if (!GameData.Instance)
                {
                    return false;
                }
                ISystemType systemType;
                if (ShipStatus.Instance.Systems.TryGetValue(SystemTypes.LifeSupp, out systemType))
                {
                    LifeSuppSystemType lifeSuppSystemType = systemType.SafeCast<LifeSuppSystemType>();
                    if (lifeSuppSystemType.Countdown < 0f)
                    {
                        if (!TutorialManager.InstanceExists)
                        {
                            GameManager.Instance.RpcEndGame(GameOverReason.ImpostorsBySabotage, !DataManager.Player.Ads.HasPurchasedAdRemoval);
                            return false;
                        }
                        HudManager.Instance.ShowPopUp(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameOverSabotage));
                        lifeSuppSystemType.Countdown = 10000f;
                    }
                }
                foreach (ISystemType systemType2 in ShipStatus.Instance.Systems.Values)
                {
                    ICriticalSabotage criticalSabotage = systemType2.SafeCast<ICriticalSabotage>();
                    if (criticalSabotage != null && criticalSabotage.Countdown < 0f)
                    {
                        if (!TutorialManager.InstanceExists)
                        {
                            GameManager.Instance.RpcEndGame(GameOverReason.ImpostorsBySabotage, !DataManager.Player.Ads.HasPurchasedAdRemoval);
                            return false;
                        }
                        HudManager.Instance.ShowPopUp(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameOverSabotage));
                        criticalSabotage.ClearSabotage();
                    }
                }
                bool onlyCrewmates = true;
                Dictionary<ModdedTeam, ChangeableValue<int>> independentTeams = new Dictionary<ModdedTeam, ChangeableValue<int>>();
                List<PlayerControl> neutralKillerCount = new List<PlayerControl>();
                int crewmateCount = 0;
                foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                {
                    if (!player.Data.IsDead && !JailBehaviour.ArrestedPlayers.Contains(player))
                    {
                        ModdedTeam team = player.Data.Role.GetTeam();
                        if (team != ModdedTeam.Crewmates)
                        {
                            if (team != ModdedTeam.Neutrals)
                            {
                                if (independentTeams.ContainsKey(team))
                                {
                                    independentTeams[team].Value++;
                                }
                                else
                                {
                                    independentTeams.Add(team, new ChangeableValue<int>(1));
                                }
                                if (player.Data.Role.CanKill())
                                {
                                    onlyCrewmates = false;
                                }
                            }
                            else if (player.Data.Role.CanKill())
                            {
                                neutralKillerCount.Add(player);
                                onlyCrewmates = false;
                            }
                        }
                        else
                        {
                            crewmateCount++;
                        }
                    }
                }
                GameManager gameManager = GameManager.Instance;
                if (!onlyCrewmates && TutorialManager.InstanceExists || !TutorialManager.InstanceExists)
                {
                    if (independentTeams.Count <= 0)
                    {
                        if (neutralKillerCount.Count <= 0)
                        {
                            if (crewmateCount <= 0)
                            {
                                if (TutorialManager.InstanceExists)
                                {
                                    DestroyableSingleton<HudManager>.Instance.ShowPopUp(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameOverImpostorDead));
                                    gameManager.ReviveEveryoneFreeplay();
                                }
                                else
                                {
                                    gameManager.RpcEndGame<ImpostorDisconnect>();
                                }
                                return false;
                            }
                            if (TutorialManager.InstanceExists)
                            {
                                DestroyableSingleton<HudManager>.Instance.ShowPopUp(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameOverImpostorDead));
                                gameManager.ReviveEveryoneFreeplay();
                            }
                            else
                            {
                                gameManager.RpcEndGame<CrewmatesByVote>();
                            }
                        }
                        else if (neutralKillerCount.Count == 1 && crewmateCount <= 1)
                        {
                            NetworkedPlayerInfo data = neutralKillerCount[0].Data;
                            ICustomRole customRole = data.Role.CustomRole();
                            if (customRole != null && customRole.NeutralGameOver != null)
                            {
                                gameManager.RpcEndGame(customRole.NeutralGameOver);
                                return false;
                            }
                            gameManager.RpcEndGame(customRole.NeutralGameOver);
                        }
                    }
                    else if (independentTeams.Count == 1)
                    {
                        KeyValuePair<ModdedTeam, ChangeableValue<int>> pair = independentTeams.First();
                        if (neutralKillerCount.Count <= 0 && pair.Value.Value >= crewmateCount)
                        {
                            if (pair.Key == ModdedTeam.Impostors)
                            {
                                if (TutorialManager.InstanceExists)
                                {
                                    DestroyableSingleton<HudManager>.Instance.ShowPopUp(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameOverImpostorKills));
                                    gameManager.ReviveEveryoneFreeplay();
                                }
                                else
                                {
                                    switch (GameData.LastDeathReason)
                                    {
                                        case DeathReason.Exile:
                                            gameManager.RpcEndGame<ImpostorsByVote>();
                                            break;
                                        case DeathReason.Kill:
                                            gameManager.RpcEndGame<ImpostorsByKill>();
                                            break;
                                        default:
                                            gameManager.RpcEndGame<CrewmateDisconnect>();
                                            break;
                                    }
                                }
                                return false;
                            }
                            if (TutorialManager.InstanceExists)
                            {
                                HudManager.Instance.ShowPopUp(pair.Key.VictoryText);
                            }
                            else
                            {
                                gameManager.RpcEndGame(pair.Key.DefaultGameOver);
                            }
                        }
                    }
                }
                if (onlyCrewmates && TutorialManager.InstanceExists || !TutorialManager.InstanceExists)
                {
                    if (TutorialManager.InstanceExists)
                    {
                        GameData.Instance.RecomputeTaskCounts();
                        if (GameData.Instance.TotalTasks <= GameData.Instance.CompletedTasks && PlayerControl.LocalPlayer.Data.Role.TasksCountTowardProgress)
                        {
                            DestroyableSingleton<HudManager>.Instance.ShowPopUp(DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GameOverTaskWin));
                            ShipStatus.Instance.Begin();
                        }
                    }
                    else
                    {
                        gameManager.CheckEndGameViaTasks();
                    }
                }
                return false;
            }
        }
    }
}
