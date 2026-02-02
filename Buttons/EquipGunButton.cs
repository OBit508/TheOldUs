using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.Crewmates;
using TheOldUs.Roles.Impostors;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class EquipGunButton : RoleButton<HitmanRole>
    {
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
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
        public override bool Active => base.Active && Helper != null && !Helper.ShowingGun;
        public override float Cooldown => 1;
        public override string OverrideText => TouTranslation.EquipGun.GetString();
        public override Color32 TextOutlineColor { get; } = TouPalette.HitmanColor;
        public override Sprite ButtonSprite => TouAssets.EquipGun;
        public override void OnClick()
        {
            CustomRpcManager.Instance<RpcUpdateGun>().Send(true, PlayerControl.LocalPlayer);
            Button.ToggleVisible(false);
            CustomButton<ReloadButton>.Instance.Button.ToggleVisible(true);
            CustomButton<UnequipGunButton>.Instance.Button.ToggleVisible(true);
        }
    }
}
