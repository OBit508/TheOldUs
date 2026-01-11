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
            writer.Write(value.Door.Id);
            writer.Write((int)value.Door.Room);
            value.SetDoorway();
        }
        public override void Handle(MessageReader reader)
        {
            int id = reader.ReadInt32();
            SystemTypes room = (SystemTypes)reader.ReadInt32();
            ShipStatus.Instance.AllDoors.FirstOrDefault(d => d.Id == id && d.Room == room).GetComponent<BetterDoorHelper>().SetDoorway();
        }
    }
}
