using FungleAPI.Role;
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
    [HarmonyPatch(typeof(Vent), "CanUse")]
    internal static class VentPatch
    {
        public static bool Prefix(Vent __instance, NetworkedPlayerInfo pc, ref bool canUse, ref bool couldUse, ref float __result)
        {
            float num = float.MaxValue;
            PlayerControl @object = pc.Object;
            bool playerCanVent = pc.Role.CanVent || pc.Role.CustomRole() != null && pc.Role.CustomRole().CanUseVent;
            couldUse = pc.Role.CanVent() && GameManager.Instance.LogicUsables.CanUse(__instance.SafeCast<IUsable>(), @object) && (pc.Role.CanUse(__instance.SafeCast<IUsable>()) || AcidTsunami.Instance != null) && (playerCanVent && !ShipStatusPatch.AcidVents.Contains(__instance) || AcidTsunami.Instance != null && ShipStatusPatch.AcidVents.Contains(__instance)) && (!@object.MustCleanVent(__instance.Id) || (@object.inVent && Vent.currentVent == __instance)) && !pc.IsDead && (@object.CanMove || @object.inVent);
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
}
