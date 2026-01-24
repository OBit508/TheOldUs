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
    internal class UnequipGunButton : RoleButton<HitmanRole>
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
        public override bool CanUse => true;
        public override bool CanClick => CanUse;
        public override float Cooldown => 1;
        public override string OverrideText => "Unequip Gun";
        public override Color32 TextOutlineColor { get; } = Palette.Orange;
        public override Sprite ButtonSprite => TouAssets.UnequipGun;
        public override void Click()
        {
            CustomRpcManager.Instance<RpcUpdateGun>().Send(false, PlayerControl.LocalPlayer);
            Button.ToggleVisible(false);
            CustomAbilityButton.Instance<ReloadButton>().Button.ToggleVisible(false);
            CustomAbilityButton.Instance<EquipGunButton>().Button.ToggleVisible(true);
        }
    }
}
