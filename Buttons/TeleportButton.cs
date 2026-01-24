using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using FungleAPI.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Patches;
using TheOldUs.Roles.Crewmates;
using TheOldUs.Roles.Impostors;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class TeleportButton : RoleButton<HackerRole>
    {
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override bool CanUse => !Transformed;
        public override bool CanClick => CanUse;
        public override float Cooldown => HackerRole.TeleportCooldown;
        public override string OverrideText => "Teleport";
        public override bool HaveUses => HackerRole.TeleportUses > 0;
        public override int NumUses => HackerRole.TeleportUses;
        public override bool TransformButton => true;
        public override float TransformDuration => HackerRole.TeleportDelay;
        public override Color32 TextOutlineColor { get; } = new Color32(0, 110, 17, byte.MaxValue);
        public override Sprite ButtonSprite => TouAssets.Teleport;
        public override void Destransform()
        {
            PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(ShipStatusPatch.Points[new System.Random().Next(0, ShipStatusPatch.Points.Count() - 1)]);
        }
    }
}
