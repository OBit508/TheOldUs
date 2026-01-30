using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Patches;
using TheOldUs.Roles.Crewmates;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class UnlockVentsButton : RoleButton<HackerRole>
    {
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override float Cooldown => HackerRole.UnlockVentsCooldown;
        public override string OverrideText => "Unlock Vents";
        public override int MaxUses => HackerRole.UnlockVentsUses;
        public override bool TransformButton => true;
        public override float TransformDuration => HackerRole.UnlockVentsDuration;
        public override Color32 TextOutlineColor { get; } = new Color32(0, 110, 17, byte.MaxValue);
        public override Sprite ButtonSprite => TouAssets.UnlockVents;
        public override void OnClick() { }
        public override void EndTransform()
        {
            if (Vent.currentVent != null)
            {
                PlayerControl.LocalPlayer.MyPhysics.RpcBootFromVent(Vent.currentVent.Id);
            }
        }
    }
}
