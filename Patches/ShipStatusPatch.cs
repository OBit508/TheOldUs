using BepInEx.Unity.IL2CPP.Utils.Collections;
using FungleAPI.GameOver;
using FungleAPI.Networking;
using FungleAPI.Utilities;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.GameOvers;
using TheOldUs.Roles.Neutrals;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Patches
{
    [HarmonyPatch(typeof(ShipStatus), "Start")]
    internal static class ShipStatusPatch
    {
        public static Vector2[] Points = new Vector2[] { new Vector2(-4, 1), new Vector2(-12, 1), new Vector2(-17, -5), new Vector2(-9, -14.5f), new Vector2(0, -16), new Vector2(4, -15), new Vector2(12, -3.5f), new Vector2(7.5f, 1.2f), new Vector2(1.2f, -7), new Vector2(9, -12.5f) };
        public static Dictionary<PlayerControl, Console> MovingConsoles = new Dictionary<PlayerControl, Console>();
        public static Dictionary<PlayerControl, PlayerControl> MovingPlayers = new Dictionary<PlayerControl, PlayerControl>();
        public static Dictionary<PlayerControl, (ChangeableValue<float>, Vector2)> WaitingPlayers = new Dictionary<PlayerControl, (ChangeableValue<float>, Vector2)>();
        public static Dictionary<Console, (ChangeableValue<float>, Vector2)> WaitingConsoles = new Dictionary<Console, (ChangeableValue<float>, Vector2)>();
        public static List<Vent> AcidVents = new List<Vent>();
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostfix(ShipStatus __instance)
        {
            AcidVents.Clear();
            ArsonistRole.SoakedPlayers.Clear();
            MovingConsoles.Clear();
            MovingPlayers.Clear();
            WaitingConsoles.Clear();
            MovingPlayers.Clear();
            TOUAssets.Jail.Instantiate(__instance.transform).transform.position = new Vector3(-12, 3.756f, 3);
            AcidVents.Add(Utils.CreateAcidVent(new Vector2(-11.2f, 2.9f)));
            foreach (Vector2 pos in Points)
            {
                AcidVents.Add(Utils.CreateAcidVent(pos));
            }
            Vector3 size = Vector3.one * 1.2f;
            if (TOUSettings.Ship.InvertX)
            {
                size.x *= -1;
            }
            if (TOUSettings.Ship.InvertY)
            {
                size.y *= -1;
            }
            __instance.transform.localScale = size;
        }
        [HarmonyPatch("FixedUpdate")]
        [HarmonyPostfix]
        public static void FixedUpdatePostfix(ShipStatus __instance)
        {
            foreach (KeyValuePair<PlayerControl, (ChangeableValue<float>, Vector2)> pair in WaitingPlayers)
            {
                if (pair.Key == null || pair.Key.Data.Disconnected)
                {
                    WaitingPlayers.Remove(pair.Key);
                    continue;
                }
                if (AmongUsClient.Instance.AmHost && !MovingPlayers.ContainsValue(pair.Key))
                {
                    pair.Value.Item1.Value -= Time.fixedDeltaTime;
                    if (pair.Value.Item1.Value <= 0)
                    {
                        CustomRpcManager.Instance<RpcSendBack>().Send((pair.Key, null, pair.Value.Item2), __instance.NetId);
                        WaitingPlayers.Remove(pair.Key);
                    }
                }
            }
            foreach (KeyValuePair<Console, (ChangeableValue<float>, Vector2)> pair in WaitingConsoles)
            {
                if (AmongUsClient.Instance.AmHost && !MovingConsoles.ContainsValue(pair.Key))
                {
                    pair.Value.Item1.Value -= Time.deltaTime;
                    if (pair.Value.Item1.Value <= 0)
                    {
                        CustomRpcManager.Instance<RpcSendBack>().Send((null, pair.Key, pair.Value.Item2), __instance.NetId);
                        WaitingConsoles.Remove(pair.Key);
                    }
                }
            }
            foreach (KeyValuePair<PlayerControl, PlayerControl> pair in MovingPlayers)
            {
                if (pair.Value == null || pair.Value.Data.Disconnected || pair.Value.Data.IsDead || pair.Key == null || pair.Key.Data.Disconnected || pair.Key.Data.IsDead)
                {
                    MovingPlayers.Remove(pair.Key);
                }
            }
            foreach (PlayerControl player in MovingConsoles.Keys)
            {
                if (AmongUsClient.Instance.AmHost && player == null || player.Data.Disconnected || player.Data.IsDead)
                {
                    MovingConsoles.Remove(player);
                }
            }
        }
    }
}
