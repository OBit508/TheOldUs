using FungleAPI.Role;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;

namespace TheOldUs.Patches
{
    [HarmonyPatch(typeof(CustomRoleManager))]
    internal static class CustomRoleManagerPatch
    {
        [HarmonyPatch("CanKill")]
        [HarmonyPrefix]
        public static bool CanKillPrefix([HarmonyArgument(0)] RoleBehaviour roleBehaviour)
        {
            if (roleBehaviour != null && roleBehaviour.Player != null && PlayerJail.Jails.ContainsKey(roleBehaviour.Player))
            {
                return false;
            }
            return true;
        }
        [HarmonyPatch("CanVent")]
        [HarmonyPrefix]
        public static bool CanVentPrefix([HarmonyArgument(0)] RoleBehaviour roleBehaviour)
        {
            if (roleBehaviour != null && roleBehaviour.Player != null && PlayerJail.Jails.ContainsKey(roleBehaviour.Player))
            {
                return false;
            }
            return true;
        }
    }
}
