using FungleAPI.Networking;
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
    public class RpcUpdateGun : CustomRpc<(PlayerControl player, bool show)>
    {
        public override void Write(MessageWriter writer, (PlayerControl player, bool show) value)
        {
            writer.WritePlayer(value.player);
            writer.Write(value.show);
            value.player.GetComponent<RoleHelper>().ShowingGun = value.show;
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl player = reader.ReadPlayer();
            player.GetComponent<RoleHelper>().ShowingGun = reader.ReadBoolean();
        }
    }
}
