using FungleAPI;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Role;
using FungleAPI.Utilities;
using Rewired.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.Crewmates;
using TheOldUs.Roles;
using TheOldUs.RPCs;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using TheOldUs.TOU;
using FungleAPI.Base.Buttons;

namespace TheOldUs.Buttons
{
    internal class ArrestPlayerButton : RoleTargetButton<PlayerControl, JailerRole>
    {
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override float Cooldown => JailerRole.ArrestCooldown;
        public override string OverrideText => TouTranslation.Arrest.GetString();
        public override int MaxUses => JailerRole.ArrestUses;
        public override Color32 TextOutlineColor { get; } = TouPalette.JailerColor;
        public override Sprite ButtonSprite => TouAssets.JailerArrest;
        public override void SetOutline(PlayerControl target, bool active)
        {
            target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(TextOutlineColor));
        }
        public override PlayerControl GetTarget()
        {
            return Role != null ? Role.FindClosestTarget() : null;
        }
        public override void OnClick()
        {
            try
            {
                if (Target != null)
                {
                    CustomRpcManager.Instance<RpcArrestPlayer>().Send((Target, true), PlayerControl.LocalPlayer);
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
