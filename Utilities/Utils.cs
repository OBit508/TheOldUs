using AmongUs.Data;
using FungleAPI;
using FungleAPI.GameOver;
using FungleAPI.GameOver.Ends;
using FungleAPI.Networking;
using FungleAPI.Role;
using FungleAPI.Teams;
using FungleAPI.Utilities;
using HarmonyLib;
using Hazel;
using PowerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.GameOvers;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Utilities
{
    internal static class Utils
    {
        public static void RpcDie(this PlayerControl player, DeadBodyType createdBody, float dissolveDelay = 0)
        {
            CustomRpcManager.Instance<RpcDie>().Send((player, createdBody, dissolveDelay), PlayerControl.LocalPlayer);
        }
        public static void Die(this PlayerControl player, DeadBodyType createdBody, float dissolveDelay = 0)
        {
            player.Die(DeathReason.Kill, true);
            DeadBody body = Helpers.CreateCustomBody(player, createdBody);
            if (createdBody == DeadBodyType.Viper)
            {
                body.SafeCast<ViperDeadBody>().SetupViperInfo(dissolveDelay, null, player);
            }
        }
        public static Vent CreateAcidVent(Vector2 position)
        {
            Vent vent = Helpers.CreateVent(Helpers.VentType.Skeld, position);
            vent.EnterVentAnim = null;
            vent.ExitVentAnim = null;
            vent.GetComponent<SpriteAnim>().enabled = false;
            vent.GetComponent<Animator>().enabled = false;
            vent.GetComponent<SpriteRenderer>().sprite = TOUAssets.AcidVent;
            return vent;
        }
        public static PlayerControl FindClosestTarget(this RoleBehaviour role, Predicate<PlayerControl> toRemove)
        {
            List<PlayerControl> playersInAbilityRangeSorted = role.GetPlayersInAbilityRangeSorted(RoleBehaviour.GetTempPlayerList()).ToSystemList();
            playersInAbilityRangeSorted.RemoveAll(toRemove);
            if (playersInAbilityRangeSorted.Count <= 0)
            {
                return null;
            }
            return playersInAbilityRangeSorted[0];
        }
        public static void SetOutline(this DeadBody body, bool active, Color color)
        {
            foreach (SpriteRenderer rend in body.bodyRenderers)
            {
                rend.material.SetFloat("_Outline", active ? 1 : 0);
                rend.material.SetColor("_OutlineColor", color);
            }
        }
        public static void WriteConsole(this MessageWriter writer, Console console)
        {
            writer.Write(console.ConsoleId);
            writer.Write((int)console.Room);
        }
        public static Console ReadConsole(this MessageReader reader)
        {
            int id = reader.ReadInt32();
            SystemTypes type = (SystemTypes)reader.ReadInt32();
            return ShipStatus.Instance.AllConsoles.FirstOrDefault(c => c.ConsoleId == id && c.Room == type);
        }
        public static void PatchFungleAPI()
        {
            Assembly fungleApi = FungleAPIPlugin.Plugin.ModAssembly;
            foreach (Type type in fungleApi.GetTypes())
            {
                if (type.Name == "LogicGameFlowNormalPatch")
                {
                    TheOldUsPlugin.Harmony.Patch(type.GetMethod("CheckEndCriteria"), new HarmonyMethod(typeof(Utils).GetMethod("CheckEndCriteria")));
                    TheOldUsPlugin.Plugin.BasePlugin.Log.LogWarning("Patched FungleAPI CheckEndCriteria");
                }
                else if (type.Name == "ExileControllerPatch")
                {
                    TheOldUsPlugin.Harmony.Patch(type.GetMethod("BeginPostfix"), new HarmonyMethod(typeof(Utils).GetMethod("BeginPostfix")));
                    TheOldUsPlugin.Plugin.BasePlugin.Log.LogWarning("Patched FungleAPI BeginPostfix");
                }
                else if (type.Name == "CustomRoleManager")
                {
                    TheOldUsPlugin.Harmony.Patch(type.GetMethod("CanVent"), new HarmonyMethod(typeof(Utils).GetMethod("CanVent")));
                    TheOldUsPlugin.Plugin.BasePlugin.Log.LogWarning("Patched FungleAPI CanVent");
                }
                else if (type.Name == "VentPatch")
                {
                    FungleAPIPlugin.Harmony.Unpatch(typeof(Vent).GetMethod("CanUse"), type.GetMethod("CanUsePrefix"));
                    TheOldUsPlugin.Plugin.BasePlugin.Log.LogWarning("Removed FungleAPI CanUsePrefix patch");
                }
            }
        }
        public static bool CanVent(RoleBehaviour roleBehaviour, ref bool __result)
        {
            if (AcidTsunami.Instance != null && !roleBehaviour.IsDead)
            {
                __result = true;
                return false;
            }
            return true;
        }
        public static bool BeginPostfix()
        {
            return !TOUSettings.Jail.ArrestWhenEjected;
        }
        public static bool CheckEndCriteria()
        {
            if (!GameData.Instance)
            {
                return false;
            }
            if (ShipStatus.Instance != null && ShipStatus.Instance.SafeCast<SkeldShipStatus>() == null)
            {
                GameManager.Instance.RpcEndGame<NotSkeldGameOver>();
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
                        string winText = "Victory of the " + data.Role.NiceName;
                        if (data.Role.CustomRole() != null)
                        {
                            winText = data.Role.CustomRole().NeutralWinText;
                        }
                        if (TutorialManager.InstanceExists)
                        {
                            DestroyableSingleton<HudManager>.Instance.ShowPopUp(winText);
                            gameManager.ReviveEveryoneFreeplay();
                        }
                        else
                        {
                            gameManager.RpcEndGame(new List<NetworkedPlayerInfo>() { data }, winText, data.Role.NameColor, data.Role.NameColor);
                        }
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
                            gameManager.RpcEndGame(pair.Key);
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
