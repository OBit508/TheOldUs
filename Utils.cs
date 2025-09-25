using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs
{
    internal static class Utils
    {
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
    }
}
