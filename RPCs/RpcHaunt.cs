using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using FungleAPI.Utilities;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Impostors;
using static Il2CppSystem.Linq.Expressions.Interpreter.CastInstruction.CastInstructionNoT;

namespace TheOldUs.RPCs
{
    public class RpcHaunt : CustomRpc<(PlayerControl novisor, PlayerControl target)>
    {
        public override void Write(MessageWriter writer, (PlayerControl novisor, PlayerControl target) value)
        {
            writer.WritePlayer(value.novisor);
            writer.Write(value.target == null);
            if (value.target != null)
            {
                writer.WritePlayer(value.target);
            }
            value.novisor.Data.Role.SafeCast<NovisorRole>().Target = value.target;
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl novisor = reader.ReadPlayer();
            bool targetIsNull = reader.ReadBoolean();
            PlayerControl target = null;
            if (!targetIsNull)
            {
                target = reader.ReadPlayer();
            }
            novisor.Data.Role.SafeCast<NovisorRole>().Target = target;
        }
    }
}
