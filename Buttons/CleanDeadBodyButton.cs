using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Role;
using FungleAPI.Translation;
using FungleAPI.Utilities;
using Rewired.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Impostors;
using TheOldUs.RPCs;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class CleanDeadBodyButton : CustomAbilityButton
    {
        public static DeadBody Target;
        public override bool CanUse => Target != null;
        public override bool CanClick => CanUse;
        public override float Cooldown => CleanerRole.CleanCooldown;
        public override string OverrideText => "Clean";
        public override Color32 TextOutlineColor { get; } = new Color32(47, 173, 212, byte.MaxValue);
        public override Sprite ButtonSprite => TOUAssets.TemporaryButton;
        public override void Update()
        {
            base.Update();
            DeadBody newTarget = PlayerControl.LocalPlayer.Data.Role.FindClosestBody();
            if (newTarget != Target)
            {
                if (Target != null && !Target.IsNullOrDestroyed())
                {
                    Target?.SetOutline(false, TextOutlineColor);
                }
                newTarget?.SetOutline(true, TextOutlineColor);
                Target = newTarget;
            }
        }
        public override void Click()
        {
            CustomRpcManager.Instance<RpcCleanDeadBody>().Send(Target, PlayerControl.LocalPlayer.NetId);
        }
    }
}
