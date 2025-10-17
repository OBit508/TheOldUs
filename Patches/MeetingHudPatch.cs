using AmongUs.GameOptions;
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
    [HarmonyPatch(typeof(MeetingHud))]
    internal static class MeetingHudPatch
    {
        [HarmonyPatch("UpdateButtons")]
        [HarmonyPrefix]
        public static bool UpdateButtonsPrefix(MeetingHud __instance)
        {
            if (PlayerControl.LocalPlayer.Data.IsDead && !__instance.amDead)
            {
                __instance.SetForegroundForDead();
            }
            if (AmongUsClient.Instance.AmHost)
            {
                for (int i = 0; i < __instance.playerStates.Length; i++)
                {
                    PlayerVoteArea playerVoteArea = __instance.playerStates[i];
                    NetworkedPlayerInfo playerById = GameData.Instance.GetPlayerById(playerVoteArea.TargetPlayerId);
                    if (playerById == null)
                    {
                        playerVoteArea.SetDisabled();
                    }
                    else
                    {
                        if (playerById.Disconnected || playerById.IsDead || JailBehaviour.ArrestedPlayers.Contains(playerById.Object))
                        {
                            playerVoteArea.SetDead(__instance.reporterId == playerById.PlayerId, true, playerById.Role.Role == RoleTypes.GuardianAngel);
                            __instance.SetDirtyBit(1U);
                        }
                    }
                }
            }
            return false;
        }
        [HarmonyPatch("CheckForEndVoting")]
        [HarmonyPrefix]
        public static bool CheckForEndVotingPrefix(MeetingHud __instance)
        {
            if (Enumerable.All<PlayerVoteArea>(__instance.playerStates, (PlayerVoteArea ps) => ps.AmDead || ps.DidVote || JailBehaviour.ArrestedPlayers.Contains(Helpers.GetPlayerById(ps.TargetPlayerId))))
            {
                Il2CppSystem.Collections.Generic.Dictionary<byte, int> dictionary = __instance.CalculateVotes();
                bool tie;
                Il2CppSystem.Collections.Generic.KeyValuePair<byte, int> max = dictionary.MaxPair(out tie);
                NetworkedPlayerInfo networkedPlayerInfo = Enumerable.FirstOrDefault<NetworkedPlayerInfo>(GameData.Instance.AllPlayers.ToSystemList(), (NetworkedPlayerInfo v) => !tie && v.PlayerId == max.Key);
                MeetingHud.VoterState[] array = new MeetingHud.VoterState[__instance.playerStates.Length];
                for (int i = 0; i < __instance.playerStates.Length; i++)
                {
                    PlayerVoteArea playerVoteArea = __instance.playerStates[i];
                    array[i] = new MeetingHud.VoterState
                    {
                        VoterId = playerVoteArea.TargetPlayerId,
                        VotedForId = playerVoteArea.VotedFor
                    };
                }
                __instance.RpcVotingComplete(array, networkedPlayerInfo, tie);
            }
            return false;
        }
    }
}
