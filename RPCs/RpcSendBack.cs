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

namespace TheOldUs.RPCs
{
    public class RpcSendBack : CustomRpc<(PlayerControl player, Console console, Vector2 position)>
    {
        public override void Write(MessageWriter writer, (PlayerControl player, Console console, Vector2 position) value)
        {
            writer.WriteVector2(value.position);
            writer.Write(value.player == null);
            if (value.player != null)
            {
                writer.WritePlayer(value.player);
                value.player.NetTransform.SnapTo(value.position);
                ShipStatusPatch.WaitingPlayers.Remove(value.player);
                return;
            }
            writer.WriteConsole(value.console);
            value.console.transform.position = value.position;
            ShipStatusPatch.WaitingConsoles.Remove(value.console);
        }
        public override void Handle(MessageReader reader)
        {
            Vector2 position = reader.ReadVector2();
            if (!reader.ReadBoolean())
            {
                PlayerControl player = reader.ReadPlayer();
                player.NetTransform.SnapTo(position);
                ShipStatusPatch.WaitingPlayers.Remove(player);
                return;
            }
            Console console = reader.ReadConsole();
            console.transform.position = position;
            ShipStatusPatch.WaitingConsoles.Remove(console);
        }
    }
}
