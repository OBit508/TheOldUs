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
    public class SplitButton : CustomAbilityButton
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
        public override bool CanUse => Novisor != null && Novisor.Transformed;
        public override bool CanClick => CanUse;
        public override float Cooldown => NovisorRole.SplitCooldown;
        public override string OverrideText => "Split";
        public override Color32 TextOutlineColor { get; } = Color.red;
        public override Sprite ButtonSprite => TOUAssets.TemporaryButton;
        public override void Click()
        {
            CustomRpcManager.Instance<RpcSplit>().Send(PlayerControl.LocalPlayer.transform.position, PlayerControl.LocalPlayer.NetId);
        }
    }
}
