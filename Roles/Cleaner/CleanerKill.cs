using FungleAPI;
using FungleAPI.Role;
using FungleAPI.Utilities;
using Rewired.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Assets;
using TheOldUs.Roles.Psychic;
using TheOldUs.Roles.Sheriff;
using UnityEngine;

namespace TheOldUs.Roles.Cleaner
{
    internal class CleanerKill : CustomAbilityButton
    {
        public static PlayerControl Target;
        public static bool OnlyMe
        {
            get
            {
                int count = 0;
                foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                {
                    if (!player.Data.IsDead && player.Data.Role.GetTeam() == PlayerControl.LocalPlayer.Data.Role.GetTeam())
                    {
                        count++;
                    }
                }
                return count <= 1;
            }
        }
        public override bool CanUse 
        {
            get
            {
                return OnlyMe && Target != null;
            }
        }
        public override bool CanClick => CanUse;
        public override float Cooldown => GameOptionsManager.Instance.currentGameOptions.GetFloat(AmongUs.GameOptions.FloatOptionNames.KillCooldown);
        public override string OverrideText => StringNames.KillLabel.GetString();
        public override Color32 TextOutlineColor { get; } = Palette.ImpostorRed;
        public override Sprite ButtonSprite => HudManager.Instance.KillButton.graphic.sprite;
        public override void Update()
        {
            base.Update();
            PlayerControl newTarget = null;
            if (OnlyMe)
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
