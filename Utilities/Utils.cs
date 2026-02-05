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
using TheOldUs.Patches;
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
            vent.GetComponent<SpriteRenderer>().sprite = TouAssets.AcidVent;
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
    }
}
