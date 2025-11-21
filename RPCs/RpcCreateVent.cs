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
using UnityEngine;

namespace TheOldUs.RPCs
{
    internal class RpcCreateVent : CustomRpc<Vector2>
    {
        public override void Write(MessageWriter writer, Vector2 value)
        {
            writer.WriteVector2(value);
            List<Vent> nearbyVents = new List<Vent>();
            foreach (Vent vent in ShipStatus.Instance.AllVents)
            {
                if (Vector2.Distance(vent.transform.position, value) <= VentCreatorRole.ConnectDistance)
                {
                    nearbyVents.Add(vent);
                }
            }
            Helpers.CreateVent(Helpers.VentType.Skeld, value, nearbyVents);
        }
        public override void Handle(MessageReader reader)
        {
            Vector2 value = reader.ReadVector2();
            List<Vent> nearbyVents = new List<Vent>();
            foreach (Vent vent in ShipStatus.Instance.AllVents)
            {
                if (Vector2.Distance(vent.transform.position, value) <= VentCreatorRole.ConnectDistance)
                {
                    nearbyVents.Add(vent);
                }
            }
            Helpers.CreateVent(Helpers.VentType.Skeld, value, nearbyVents);
        }
    }
}
