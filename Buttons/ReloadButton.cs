using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using FungleAPI.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.Crewmates;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class ReloadButton : RoleButton<HitmanRole>
    {
        public static RoleHelper Helper
        {
            get
            {
                if (PlayerControl.LocalPlayer != null)
                {
                    return PlayerControl.LocalPlayer.GetComponent<RoleHelper>();
                }
                return null;
            }
        }
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override bool Active => base.Active && Helper != null && Helper.ShowingGun;
        public override bool CanUse => !HitmanRole.CanShoot;
        public override bool CanClick => CanUse;
        public override float Cooldown => HitmanRole.ReloadCooldown;
        public override bool HaveUses => HitmanRole.ReloadUses > 0;
        public override int NumUses => HitmanRole.ReloadUses;
        public override string OverrideText => "Reload";
        public override Color32 TextOutlineColor { get; } = Palette.Orange;
        public override Sprite ButtonSprite => TOUAssets.Reload;
        public override void Click()
        {
            HitmanRole.CanShoot = true;
        }
    }
}
