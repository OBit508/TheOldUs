using AmongUs.GameOptions;
using FungleAPI.Role;
using FungleAPI.Utilities;
using Il2CppSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheOldUs.Roles.BaseRole
{
    internal class CrewmateBase : RoleBehaviour
    {
        public override bool IsDead => false;
        public override bool CanUse(IUsable usable)
        {
            return usable.SafeCast<ZiplineConsole>() != null || usable.SafeCast<Ladder>() != null || usable.SafeCast<PlatformConsole>() != null || usable.SafeCast<Console>() != null || usable.SafeCast<DoorConsole>() != null;
        }
        public virtual Il2CppSystem.Collections.Generic.List<PlayerControl> GetValidTargets()
        {
            List<PlayerControl> targets = GetTempPlayerList().ToSystemList();
            targets.RemoveAll(t => t.Data.Role.GetTeam() == this.GetTeam() && !this.GetTeam().FriendlyFire);
            return targets.ToIl2CppList();
        }
        public override PlayerControl FindClosestTarget()
        {
            Il2CppSystem.Collections.Generic.List<PlayerControl> playersInAbilityRangeSorted = GetPlayersInAbilityRangeSorted(GetValidTargets());
            if (playersInAbilityRangeSorted.Count <= 0)
            {
                return null;
            }
            return playersInAbilityRangeSorted[0];
        }
        public override bool DidWin(GameOverReason gameOverReason)
        {
            return CustomRoleManager.DidWin(this, gameOverReason);
        }
        public override DeadBody FindClosestBody()
        {
            return Player.GetClosestDeadBody(GetAbilityDistance());
        }
    }
}
