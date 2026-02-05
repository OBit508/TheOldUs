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
    public class NovisorTransformButton : RoleButton<NovisorRole>
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
        public override float Cooldown => NovisorRole.TransformCooldown;
        public override string OverrideText => "Transform";
        public override bool TransformButton => true;
        public override float TransformDuration => NovisorRole.TransformDuration;
        public override Color32 TextOutlineColor { get; } = Color.red;
        public override Sprite ButtonSprite => null;
        public override void OnClick()
        {
            if (Novisor != null)
            {
                CustomRpcManager.Instance<RpcNovisorTransform>().Send((PlayerControl.LocalPlayer, true), PlayerControl.LocalPlayer);
            }
        }
        public override void EndTransform()
        {
            base.EndTransform();
            if (Novisor != null)
            {
                CustomRpcManager.Instance<RpcNovisorTransform>().Send((PlayerControl.LocalPlayer, false), PlayerControl.LocalPlayer);
                NovisorInvisibleButton novisorInvisible = CustomButton<NovisorInvisibleButton>.Instance;
                if (novisorInvisible.Transformed)
                {
                    novisorInvisible.EndTransform();
                }
            }
        }
    }
}
