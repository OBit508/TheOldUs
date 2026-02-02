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
    public class SplitButton : RoleButton<NovisorRole>
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
        public override float Cooldown => NovisorRole.SplitCooldown;
        public override string OverrideText => "Split";
        public override Color32 TextOutlineColor { get; } = Color.red;
        public override Sprite ButtonSprite => TouAssets.TemporaryButton;
        public override void OnClick()
        {
            CustomRpcManager.Instance<RpcSplit>().Send(PlayerControl.LocalPlayer.transform.position, PlayerControl.LocalPlayer);
        }
    }
}
