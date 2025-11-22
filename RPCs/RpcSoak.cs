using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using FungleAPI.Utilities;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Neutrals;
using static Il2CppSystem.Linq.Expressions.Interpreter.CastInstruction.CastInstructionNoT;

namespace TheOldUs.RPCs
{
    internal class RpcSoak : CustomRpc<(PlayerControl arsonist, PlayerControl player)>
    {
        public override void Write(MessageWriter writer, (PlayerControl arsonist, PlayerControl player) value)
        {
            writer.WritePlayer(value.arsonist);
            writer.WritePlayer(value.player);
            ArsonistRole.SoakedPlayers[value.arsonist].Add(value.player);
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl arsonist = reader.ReadPlayer();
            PlayerControl player = reader.ReadPlayer();
            ArsonistRole.SoakedPlayers[arsonist].Add(player);
        }
    }
}
