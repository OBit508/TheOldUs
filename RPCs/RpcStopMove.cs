using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Patches;
using UnityEngine;
using static Il2CppSystem.Linq.Expressions.Interpreter.CastInstruction.CastInstructionNoT;

namespace TheOldUs.RPCs
{
    internal class RpcStopMove : CustomRpc<PlayerControl>
    {
        public override void Write(MessageWriter writer, PlayerControl value)
        {
            writer.WritePlayer(value);
            if (ShipStatusPatch.MovingPlayers.ContainsKey(value))
            {
                ShipStatusPatch.MovingPlayers.Remove(value);
            }
            if (ShipStatusPatch.MovingConsoles.ContainsKey(value))
            {
                ShipStatusPatch.MovingConsoles.Remove(value);
            }
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl value = reader.ReadPlayer();
            if (ShipStatusPatch.MovingPlayers.ContainsKey(value))
            {
                ShipStatusPatch.MovingPlayers.Remove(value);
            }
            if (ShipStatusPatch.MovingConsoles.ContainsKey(value))
            {
                ShipStatusPatch.MovingConsoles.Remove(value);
            }
        }
    }
}
