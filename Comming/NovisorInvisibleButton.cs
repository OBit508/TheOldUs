using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Comming
{
    public class NovisorInvisibleButton : RoleButton<NovisorRole>
    {
        public NovisorRole Novisor
        {
            get
            {
                if (PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.Data != null && PlayerControl.LocalPlayer.Data.Role != null && PlayerControl.LocalPlayer.Data.Role.SafeCast<NovisorRole>() != null)
                {
                    return PlayerControl.LocalPlayer.Data.Role.SafeCast<NovisorRole>();
                }
                return null;
            }
        }
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override bool CanUse() => base.CanUse() && Novisor != null && Novisor.Transformed;
        public override float Cooldown => NovisorRole.InvisibleCooldown;
        public override string OverrideText => "Invisible";
        public override bool TransformButton => true;
        public override float TransformDuration => NovisorRole.InvisibleDuration;
        public override Color32 TextOutlineColor { get; } = Color.red;
        public override Sprite ButtonSprite => TouAssets.TemporaryButton;
        public override void OnClick()
        {
            CustomRpcManager.Instance<RpcInvisible>().Send((PlayerControl.LocalPlayer, true), PlayerControl.LocalPlayer);
        }
        public override void EndTransform()
        {
            base.EndTransform();
            CustomRpcManager.Instance<RpcInvisible>().Send((PlayerControl.LocalPlayer, false), PlayerControl.LocalPlayer);
        }
    }
}
