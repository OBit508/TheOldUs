using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using FungleAPI.Utilities;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Patches;
using TheOldUs.Roles.Impostors;
using UnityEngine;

namespace TheOldUs.RPCs
{
    internal class RpcStartMove : CustomRpc<(PlayerControl psychic, PlayerControl player, Console console, Vector2 position)>
    {
        public override void Write(MessageWriter writer, (PlayerControl psychic, PlayerControl player, Console console, Vector2 position) value)
        {
            writer.WritePlayer(value.psychic);
            writer.WriteVector2(value.position);
            writer.Write(value.player == null);
            if (value.player != null)
            {
                writer.WritePlayer(value.player);
                value.player.NetTransform.SnapTo(value.position);
                ShipStatusPatch.WaitingPlayers.Add(value.player, (new ChangeableValue<float>(PsychicRole.ObjetTime), value.position));
                ShipStatusPatch.MovingPlayers.Add(value.psychic, value.player);
                return;
            }
            writer.WriteConsole(value.console);
            value.console.transform.position = value.position;
            ShipStatusPatch.WaitingConsoles.Add(value.console, (new ChangeableValue<float>(PsychicRole.ObjetTime), value.position));
            ShipStatusPatch.MovingConsoles.Add(value.psychic, value.console);
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl psychic = reader.ReadPlayer();
            Vector2 position = reader.ReadVector2();
            if (!reader.ReadBoolean())
            {
                PlayerControl player = reader.ReadPlayer();
                player.NetTransform.SnapTo(position);
                ShipStatusPatch.WaitingPlayers.Add(player, (new ChangeableValue<float>(PsychicRole.ObjetTime), position));
                ShipStatusPatch.MovingPlayers.Add(psychic, player);
                return;
            }
            Console console = reader.ReadConsole();
            console.transform.position = position;
            ShipStatusPatch.WaitingConsoles.Add(console, (new ChangeableValue<float>(PsychicRole.ObjetTime), position));
            ShipStatusPatch.MovingConsoles.Add(psychic, console);
        }
    }
}
