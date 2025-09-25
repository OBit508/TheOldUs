using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace TheOldUs.Roles.Psychic
{
    internal class MovePlayerRpc : CustomRpc<(PlayerControl player, Vector2 position)>
    {
        public override void Write(MessageWriter writer, (PlayerControl player, Vector2 position) value)
        {
            writer.WritePlayer(value.player);
            writer.WriteVector2(value.position);
            value.player.NetTransform.SnapTo(value.position);
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl player = reader.ReadPlayer();
            player.NetTransform.SnapTo(reader.ReadVector2());
        }
    }
}
