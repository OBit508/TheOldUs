using FungleAPI;
using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Player;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.Crewmates;
using TheOldUs.Roles.Neutrals;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace TheOldUs.Buttons
{
    internal class GasolineButton : RoleTargetButton<PlayerControl, ArsonistRole>
    {
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override bool CanUse => Target != null;
        public override bool CanClick => CanUse;
        public override float Cooldown => ArsonistRole.GasolineCooldown;
        public override string OverrideText => "Gasoline";
        public override Color32 TextOutlineColor { get; } = new Color32(173, 95, 5, byte.MaxValue);
        public override Sprite ButtonSprite => TOUAssets.Gasoline;
        public override void SetOutline(PlayerControl target, bool active)
        {
            target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(TextOutlineColor));
        }
        public override PlayerControl GetTarget()
        {
            return Role != null ? Role.FindClosestTarget() : null;
        }
        public override void Click()
        {
            try
            {
                if (Target != null)
                {
                    Target.GetPlayerComponent<RoleHelper>().Soaked = true;
                    Target.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(TextOutlineColor));
                    Target = null;
                }
            }
            catch (Exception ex)
            {
                FungleAPIPlugin.Instance.Log.LogError(ex);
            }
        }
    }
}
