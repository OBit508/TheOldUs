using FungleAPI.Base.Rpc;
using FungleAPI.Networking;
using Hazel;
using InnerNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Utilities;
using static Il2CppSystem.Linq.Expressions.Interpreter.CastInstruction.CastInstructionNoT;

namespace TheOldUs.RPCs
{
    internal class RpcChangeTimeManipulation : AdvancedRpc<TimeManipulationType, PlayerControl>
    {
        public override void Write(PlayerControl innerNetObject, MessageWriter messageWriter, TimeManipulationType value)
        {
            messageWriter.Write((int)value);
            if (AmongUsClient.Instance.HostId == innerNetObject.Data.ClientId)
            {
                TimeManager.Instance.TimeManipulation = value;
            }
        }
        public override void Handle(PlayerControl innerNetObject, MessageReader messageReader)
        {
            TimeManipulationType value = (TimeManipulationType)messageReader.ReadInt32();
            if (AmongUsClient.Instance.HostId == innerNetObject.Data.ClientId)
            {
                TimeManager.Instance.TimeManipulation = value;
            }
            else if (AmongUsClient.Instance.AmHost && TimeManager.Instance.TimeManipulation == TimeManipulationType.None)
            {
                Rpc<RpcChangeTimeManipulation>.Instance.Send(value, PlayerControl.LocalPlayer);
            }
        }
    }
}
