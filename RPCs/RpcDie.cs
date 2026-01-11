using FungleAPI.Base.Rpc;
using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using FungleAPI.Utilities;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Utilities;

namespace TheOldUs.RPCs
{
    internal class RpcDie : AdvancedRpc<(PlayerControl player, DeadBodyType type, float dissolveDelay)>
    {
        public override void Write(MessageWriter writer, (PlayerControl player, DeadBodyType type, float dissolveDelay) value)
        {
            writer.WritePlayer(value.player);
            writer.Write((int)value.type);
            writer.Write(value.dissolveDelay);
            Utils.Die(value.player, value.type, value.dissolveDelay);
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl player = reader.ReadPlayer();
            DeadBodyType body = (DeadBodyType)reader.ReadInt32();
            player.Die(body, reader.ReadSingle());
        }
    }
}
