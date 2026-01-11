using FungleAPI.Base.Rpc;
using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using FungleAPI.Player;
using Hazel;
using InnerNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;

namespace TheOldUs.RPCs
{
    public class RpcUpdateGun : AdvancedRpc<bool, PlayerControl>
    {
        public override void Write(PlayerControl playerControl, MessageWriter writer, bool value)
        {
            writer.Write(value);
            playerControl.GetPlayerComponent<RoleHelper>().ShowingGun = value;
        }
        public override void Handle(PlayerControl playerControl, MessageReader reader)
        {
            playerControl.GetPlayerComponent<RoleHelper>().ShowingGun = reader.ReadBoolean();
        }
    }
}
