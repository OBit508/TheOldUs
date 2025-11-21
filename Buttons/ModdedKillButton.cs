using FungleAPI;
using FungleAPI.Hud;
using FungleAPI.Player;
using FungleAPI.Role;
using FungleAPI.Utilities;
using Rewired.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class ModdedKillButton : CustomAbilityButton
    {
        public PlayerControl Target;
        public bool CanKill => PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.Data != null && PlayerControl.LocalPlayer.Data.Role != null && PlayerControl.LocalPlayer.Data.Role.CanKill();
        public override bool CanUse 
        {
            get
            {
                return CanKill && Target != null;
            }
        }
        public override bool CanClick => CanUse;
        public override float Cooldown => GameOptionsManager.Instance.currentGameOptions.GetFloat(AmongUs.GameOptions.FloatOptionNames.KillCooldown);
        public override string OverrideText => StringNames.KillLabel.GetString();
        public override Color32 TextOutlineColor => Color.red;
        public override Sprite ButtonSprite => HudManager.Instance.KillButton.graphic.sprite;
        public override void Update()
        {
            base.Update();
            PlayerControl newTarget = null;
            if (CanKill)
            {
                newTarget = PlayerControl.LocalPlayer.Data.Role.FindClosestTarget();
            }
            if (newTarget != Target)
            {
                if (Target != null && !Target.IsNullOrDestroyed())
                {
                    Target?.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(PlayerControl.LocalPlayer.Data.Role.TeamColor));
                }
                newTarget?.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(PlayerControl.LocalPlayer.Data.Role.TeamColor));
                Target = newTarget;
            }
        }
        public override void Click()
        {
            PlayerControl.LocalPlayer.RpcCustomMurderPlayer(Target, Target.protectedByGuardianId == -1 ? MurderResultFlags.Succeeded : MurderResultFlags.FailedProtected);
        }
    }
}
