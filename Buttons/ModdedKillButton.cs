using FungleAPI;
using FungleAPI.Base.Buttons;
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
using TheOldUs.Roles.Impostors;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class ModdedKillButton : TargetButton<PlayerControl>
    {
        public bool CanKill => PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.Data != null && PlayerControl.LocalPlayer.Data.Role != null && PlayerControl.LocalPlayer.Data.Role.CanKill();
        public override bool CanUse 
        {
            get
            {
                return CanKill && Target != null;
            }
        }
        public override bool CanClick => CanUse;
        public override float Cooldown
        {
            get
            {
                float cooldown = GameOptionsManager.Instance.currentGameOptions.GetFloat(AmongUs.GameOptions.FloatOptionNames.KillCooldown);
                if (PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.Data != null && PlayerControl.LocalPlayer.Data.Role != null)
                {
                    Type type = PlayerControl.LocalPlayer.Data.Role.GetType();
                    return type == typeof(AcidMaster) ? cooldown + AcidMaster.ExtraKillCooldown : cooldown;
                }
                return cooldown;
            }
        }
        public override string OverrideText => StringNames.KillLabel.GetString();
        public override Color32 TextOutlineColor => Color.red;
        public override Sprite ButtonSprite => HudManager.Instance.KillButton.graphic.sprite;
        public override bool Active
        {
            get
            {
                if (PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.Data != null && PlayerControl.LocalPlayer.Data.Role != null)
                {
                    Type type = PlayerControl.LocalPlayer.Data.Role.GetType();
                    return type == typeof(CleanerRole) || type == typeof(PsychicRole) || type == typeof(AcidMaster);
                }
                return false;
            }
        }
        public override void SetOutline(PlayerControl target, bool active)
        {
            target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(TextOutlineColor));
        }
        public override PlayerControl GetTarget()
        {
            return PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.Data != null && PlayerControl.LocalPlayer.Data.Role != null ? PlayerControl.LocalPlayer.Data.Role.FindClosestTarget() : null;
        }
        public override void Click()
        {
            PlayerControl.LocalPlayer.RpcCustomMurderPlayer(Target, Target.protectedByGuardianId == -1 ? MurderResultFlags.Succeeded : MurderResultFlags.FailedProtected);
        }
    }
}
