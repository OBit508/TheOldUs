using FungleAPI;
using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Player;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Crewmates;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;
using static Il2CppSystem.Linq.Expressions.Interpreter.NullableMethodCallInstruction;

namespace TheOldUs.Buttons
{
    internal class ReviveButton : RoleTargetButton<DeadBody, MedicRole>
    {
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override float Cooldown => MedicRole.ReviveCooldown;
        public override string OverrideText => "Revive";
        public override int MaxUses => MedicRole.ReviveUses;
        public override Color32 TextOutlineColor { get; } = new Color32(40, 165, 0, byte.MaxValue);
        public override Sprite ButtonSprite => TouAssets.Revive;
        public override void SetOutline(DeadBody target, bool active)
        {
            foreach (SpriteRenderer spriteRenderer in target.bodyRenderers)
            {
                spriteRenderer.material.SetFloat("_Outline", active ? 1 : 0);
                spriteRenderer.material.SetColor("_OutlineColor", TextOutlineColor);
            }
        }
        public override DeadBody GetTarget()
        {
            return Role != null ? Role.FindClosestBody() : null;
        }
        public override void OnClick()
        {
            Rpc<RpcRevive>.Instance.Send(Target, PlayerControl.LocalPlayer);
            Target = null;
        }
    }
}
