using FungleAPI.Base.Rpc;
using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using FungleAPI.Role;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using static Il2CppSystem.Linq.Expressions.Interpreter.CastInstruction.CastInstructionNoT;

namespace TheOldUs.Comming
{
    public class RpcInvisible : AdvancedRpc<(PlayerControl player, bool invisible)>
    {
        public override void Write(MessageWriter writer, (PlayerControl player, bool invisible) value)
        {
            writer.WritePlayer(value.player);
            writer.Write(value.invisible);
            value.player.GetComponent<PlayerAnimationHelper>().Invisible = value.invisible;
            if (value.player.Data.Role.GetTeam() != PlayerControl.LocalPlayer.Data.Role.GetTeam() && !PlayerControl.LocalPlayer.Data.IsDead)
            {
                value.player.Visible = !value.invisible;
            }
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl player = reader.ReadPlayer();
            bool invisible = reader.ReadBoolean();
            player.GetComponent<PlayerAnimationHelper>().Invisible = invisible;
            if (player.Data.Role.GetTeam() != PlayerControl.LocalPlayer.Data.Role.GetTeam() && !PlayerControl.LocalPlayer.Data.IsDead)
            {
                player.Visible = !invisible;
            }
        }
    }
}
