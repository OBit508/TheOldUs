using FungleAPI;
using FungleAPI.Networking;
using FungleAPI.Role;
using Rewired.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Assets;
using TheOldUs.Components;
using TheOldUs.Roles.Jailer;
using UnityEngine;

namespace TheOldUs.Roles.Psychic
{
    internal class MoveTask : CustomAbilityButton
    {
        public override bool CanUse => !PsychicRole.MovingPlayer;
        public override bool CanClick => CanUse;
        public override float Cooldown => PsychicRole.MoveConsoleCooldown;
        public override string OverrideText => "Move Task";
        public override Color32 TextOutlineColor { get; } = new Color32(161, 121, 171, byte.MaxValue);
        public override Sprite ButtonSprite => ButtonSprites.TemporaryButton;
        public override bool TransformButton => true;
        public override float TransformDuration => PsychicRole.MoveConsoleDuration;
        public override void Click()
        {
            PsychicRole.MovingConsole = true;
        }
        public override void Destransform()
        {
            PsychicRole.MovingConsole = false;
            PsychicRole.console = null;
        }
    }
}
