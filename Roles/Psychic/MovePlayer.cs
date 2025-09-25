using FungleAPI.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Assets;
using UnityEngine;

namespace TheOldUs.Roles.Psychic
{
    internal class MovePlayer : CustomAbilityButton
    {
        public override bool CanUse => !PsychicRole.MovingConsole;
        public override bool CanClick => CanUse;
        public override float Cooldown => PsychicRole.MovePlayerCooldown;
        public override string OverrideText => "Move Player";
        public override Color32 TextOutlineColor { get; } = new Color32(161, 121, 171, byte.MaxValue);
        public override Sprite ButtonSprite => ButtonSprites.TemporaryButton;
        public override bool TransformButton => true;
        public override float TransformDuration => PsychicRole.MovePlayerDuration;
        public override void Click()
        {
            PsychicRole.MovingPlayer = true;
        }
        public override void Destransform()
        {
            PsychicRole.MovingPlayer = false;
            PsychicRole.player = null;
        }
    }
}
