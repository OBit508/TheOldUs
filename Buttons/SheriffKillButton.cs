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
using TheOldUs.Roles.Crewmates;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class SheriffKillButton : RoleTargetButton<PlayerControl, SheriffRole>
    {
        public override float Cooldown => SheriffRole.KillCooldown;
        public override string OverrideText => TouTranslation.Shoot.GetString();
        public override int MaxUses => (int)SheriffRole.UsesCount;
        public override Color32 TextOutlineColor { get; } = TouPalette.SheriffColor;
        public override Sprite ButtonSprite => TouAssets.SheriffKill;
        public override void SetOutline(PlayerControl target, bool active)
        {
            target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(TextOutlineColor));
        }
        public override PlayerControl GetTarget()
        {
            return Role != null ? Role.FindClosestTarget() : null;
        }
        public override void OnClick()
        {
            try
            {
                if (Target.Data.Role.GetTeam() == FungleAPI.Teams.ModdedTeam.Crewmates)
                {
                    PlayerControl.LocalPlayer.RpcCustomMurderPlayer(PlayerControl.LocalPlayer, PlayerControl.LocalPlayer.protectedByGuardianId == -1 ? MurderResultFlags.Succeeded : MurderResultFlags.FailedProtected, false, true, false);
                    if (SheriffRole.TargetDie)
                    {
                        PlayerControl.LocalPlayer.RpcCustomMurderPlayer(Target, Target.protectedByGuardianId == -1 ? MurderResultFlags.Succeeded : MurderResultFlags.FailedProtected, false, true, false, true, true);
                        Target?.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(TextOutlineColor));
                        Target = null;
                    }
                    return;
                }
                PlayerControl.LocalPlayer.RpcCustomMurderPlayer(Target, Target.protectedByGuardianId == -1 ? MurderResultFlags.Succeeded : MurderResultFlags.FailedProtected);
            }
            catch (Exception ex)
            {
                FungleAPIPlugin.Instance.Log.LogError(ex);
            }
        }
    }
}
