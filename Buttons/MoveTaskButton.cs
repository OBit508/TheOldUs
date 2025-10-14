using FungleAPI;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Role;
using Rewired.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.Impostors;
using TheOldUs.RPCs;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class MoveTaskButton : CustomAbilityButton
    {
        public override bool CanUse => !PsychicRole.MovingPlayer;
        public override bool CanClick => CanUse;
        public override float Cooldown => PsychicRole.MoveConsoleCooldown;
        public override string OverrideText => "Move Task";
        public override Color32 TextOutlineColor { get; } = new Color32(161, 121, 171, byte.MaxValue);
        public override Sprite ButtonSprite => TOUAssets.TemporaryButton;
        public override bool TransformButton => true;
        public override float TransformDuration => PsychicRole.MoveConsoleDuration;
        public override void Click()
        {
            PsychicRole.MovingConsole = true;
        }
        public override void Destransform()
        {
            base.Destransform();
            if (PsychicRole.MovingConsole)
            {
                CustomRpcManager.Instance<RpcStopMove>().Send(PlayerControl.LocalPlayer, PlayerControl.LocalPlayer.NetId);
            }
            PsychicRole.MovingConsole = false;
            PsychicRole.movedConsole = null;
        }
    }
}
