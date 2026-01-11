using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Impostors;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Buttons
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
        public override bool CanUse => true;
        public override bool CanClick => CanUse;
        public override float Cooldown => NovisorRole.TransformCooldown;
        public override string OverrideText => "Transform";
        public override bool TransformButton => true;
        public override float TransformDuration => NovisorRole.TransformDuration;
        public override Color32 TextOutlineColor { get; } = Color.red;
        public override Sprite ButtonSprite => TOUAssets.TemporaryButton;
        public override void Click()
        {
            if (Novisor != null)
            {
                HudManager.Instance.ImpostorVentButton.ToggleVisible(false);
                CustomRpcManager.Instance<RpcNovisorTransform>().Send((PlayerControl.LocalPlayer, true), PlayerControl.LocalPlayer);
            }
        }
        public override void Destransform()
        {
            if (Novisor != null)
            {
                HudManager.Instance.ImpostorVentButton.ToggleVisible(true);
                CustomRpcManager.Instance<RpcNovisorTransform>().Send((PlayerControl.LocalPlayer, false), PlayerControl.LocalPlayer);
                NovisorInvisibleButton novisorInvisible = CustomAbilityButton.Instance<NovisorInvisibleButton>();
                if (novisorInvisible.Transformed)
                {
                    novisorInvisible.Destransform();
                }
            }
        }
    }
}
