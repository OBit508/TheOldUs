using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Impostors;
using TheOldUs.RPCs;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class MovePlayerbutton : CustomAbilityButton
    {
        public override bool CanUse => !PsychicRole.MovingConsole;
        public override bool CanClick => CanUse;
        public override float Cooldown => PsychicRole.MovePlayerCooldown;
        public override string OverrideText => "Move Player";
        public override Color32 TextOutlineColor { get; } = new Color32(161, 121, 171, byte.MaxValue);
        public override Sprite ButtonSprite => TOUAssets.TemporaryButton;
        public override bool TransformButton => true;
        public override float TransformDuration => PsychicRole.MovePlayerDuration;
        public override void Click()
        {
            PsychicRole.MovingPlayer = true;
        }
        public override void Destransform()
        {
            base.Destransform();
            if (PsychicRole.MovingPlayer)
            {
                CustomRpcManager.Instance<RpcStopMove>().Send(PlayerControl.LocalPlayer, PlayerControl.LocalPlayer.NetId);
            }
            PsychicRole.MovingPlayer = false;
            PsychicRole.movedPlayer = null;
        }
    }
}
