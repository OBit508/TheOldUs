using FungleAPI.Base.Rpc;
using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.TOU;
using UnityEngine;
using static Il2CppSystem.Linq.Expressions.Interpreter.CastInstruction.CastInstructionNoT;
using static UnityEngine.GraphicsBuffer;

namespace TheOldUs.RPCs
{
    internal class RpcArrestPlayer : AdvancedRpc<(PlayerControl target, bool arrested)>
    {
        public override void Write(MessageWriter writer, (PlayerControl target, bool arrested) value)
        {
            writer.WritePlayer(value.target);
            writer.Write(value.arrested);
            if (value.arrested)
            {
                JailBehaviour.ArrestedPlayers.Add(value.target);
                value.target.NetTransform.SnapTo(new Vector2(TouSettings.Ship.InvertX ? 12 : -12, TouSettings.Ship.InvertY ? -3 : 3));
            }
            else
            {
                JailBehaviour.ArrestedPlayers.Remove(value.target);
                value.target.NetTransform.SnapTo(new Vector2(TouSettings.Ship.InvertX ? 12 : -12, TouSettings.Ship.InvertY ? -1 : 1));
            }
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl target = reader.ReadPlayer();
            bool arrested = reader.ReadBoolean();
            if (arrested)
            {
                JailBehaviour.ArrestedPlayers.Add(target);
                target.NetTransform.SnapTo(new Vector2(TouSettings.Ship.InvertX ? 12 : -12, TouSettings.Ship.InvertY ? -3 : 3));
            }
            else
            {
                JailBehaviour.ArrestedPlayers.Remove(target);
                target.NetTransform.SnapTo(new Vector2(TouSettings.Ship.InvertX ? 12 : -12, TouSettings.Ship.InvertY ? -1 : 1));
            }
        }
    }
}
