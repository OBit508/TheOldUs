using FungleAPI.Base.Rpc;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;

namespace TheOldUs.RPCs
{
    public class RpcUpdateDoor : AdvancedRpc<BetterDoorHelper>
    {
        public override void Write(MessageWriter writer, BetterDoorHelper value)
        {
            writer.Write(value.DoorId);
            value.SetDoorway();
        }
        public override void Handle(MessageReader reader)
        {
            int id = reader.ReadInt32();
            BetterDoorHelper.Doors.FirstOrDefault(d => d.DoorId == id).SetDoorway();
        }
    }
}
