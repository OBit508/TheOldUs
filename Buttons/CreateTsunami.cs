using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using FungleAPI.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.Impostors;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class CreateTsunami : RoleButton<AcidMaster>
    {
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override bool CanUse() => base.CanUse() && AcidTsunami.Instance == null;
        public override float Cooldown => AcidMaster.AcidCooldown;
        public override string OverrideText => "Acid";
        public override int MaxUses => AcidMaster.AcidUses;
        public override Color32 TextOutlineColor { get; } = new Color32(0, 255, 8, byte.MaxValue);
        public override Sprite ButtonSprite => TouAssets.Acid;
        public override void OnClick()
        {
            CustomRpcManager.Instance<RpcSummonTsunami>().Send(PlayerControl.LocalPlayer);
        }
    }
}
