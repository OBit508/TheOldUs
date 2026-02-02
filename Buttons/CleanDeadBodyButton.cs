using FungleAPI.Base.Buttons;
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
using TheOldUs.TOU;
using TheOldUs.Utilities;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class CleanDeadBodyButton : RoleTargetButton<DeadBody, JanitorRole>
    {
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override float Cooldown => JanitorRole.CleanCooldown;
        public override string OverrideText => TouTranslation.Clean.GetString();
        public override Color32 TextOutlineColor { get; } = TouPalette.JanitorColor;
        public override Sprite ButtonSprite => TouAssets.Clean;
        public override void SetOutline(DeadBody target, bool active)
        {
            target?.SetOutline(active, TextOutlineColor);
        }
        public override DeadBody GetTarget()
        {
            return Role != null ? Role.FindClosestBody() : null;
        }
        public override void OnClick()
        {
            CustomRpcManager.Instance<RpcCleanDeadBody>().Send(Target, PlayerControl.LocalPlayer);
        }
    }
}
