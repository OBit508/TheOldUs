using FungleAPI;
using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Role;
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
using static UnityEngine.GraphicsBuffer;

namespace TheOldUs.Buttons
{
    internal class DigVentButton : RoleButton<DiggerRole>
    {
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override bool CanUse => true;
        public override bool CanClick => CanUse;
        public override float Cooldown => DiggerRole.CreateVentCooldown;
        public override string OverrideText => "Dig Vent";
        public override bool HaveUses => DiggerRole.MaxVents > 0;
        public override int NumUses => DiggerRole.MaxVents;
        public override Color32 TextOutlineColor { get; } = Color.red;
        public override Sprite ButtonSprite => TouAssets.CreateVent;
        public override void Click()
        {
            CustomRpcManager.Instance<RpcCreateVent>().Send(PlayerControl.LocalPlayer.transform.position, PlayerControl.LocalPlayer);
        }
    }
}
