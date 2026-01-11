using FungleAPI.Base.Rpc;
using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Utilities;
using UnityEngine;

namespace TheOldUs.RPCs
{
    internal class RpcMove : AdvancedRpc<(PlayerControl player, Console console, Vector2 position)>
    {
        public override void Write(MessageWriter writer, (PlayerControl player, Console console, Vector2 position) value)
        {
            writer.WriteVector2(value.position);
            writer.Write(value.player == null);
            if (value.player != null)
            {
                writer.WritePlayer(value.player);
                value.player.NetTransform.SnapTo(value.position);
                return;
            }
            writer.WriteConsole(value.console);
            value.console.transform.position = value.position;
        }
        public override void Handle(MessageReader reader)
        {
            Vector2 position = reader.ReadVector2();
            if (!reader.ReadBoolean())
            {
                reader.ReadPlayer().NetTransform.SnapTo(position);
                return;
            }
            reader.ReadConsole().transform.position = position;
        }
    }
}
