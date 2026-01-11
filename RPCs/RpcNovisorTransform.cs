using FungleAPI.Base.Rpc;
using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Impostors;

namespace TheOldUs.RPCs
{
    public class RpcNovisorTransform : AdvancedRpc<(PlayerControl novisor, bool transformed)>
    {
        public override void Write(MessageWriter writer, (PlayerControl novisor, bool transformed) value)
        {
            writer.WritePlayer(value.novisor);
            writer.Write(value.transformed);
            value.novisor.Data.Role.GetComponent<NovisorRole>().Transformed = value.transformed;
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl novisor = reader.ReadPlayer();
            bool transformed = reader.ReadBoolean();
            novisor.Data.Role.GetComponent<NovisorRole>().Transformed = transformed;
        }
    }
}
