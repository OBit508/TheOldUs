using FungleAPI.Base.Rpc;
using FungleAPI.Networking;
using FungleAPI.Player;
using FungleAPI.Utilities;
using Hazel;
using InnerNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.Neutrals;
using static UnityEngine.GraphicsBuffer;

namespace TheOldUs.RPCs
{
    internal class RpcSetTarget : SimpleRpc<PlayerControl>
    {
        public override void Write(PlayerControl innerNetObject, MessageWriter messageWriter)
        {
            Il2CppSystem.Collections.Generic.List<PlayerControl> validTargets = PlayerControl.AllPlayerControls.FindAll(new Predicate<PlayerControl>(p => p != innerNetObject).ToIl2CppPredicate());
            PlayerControl target = validTargets[UnityEngine.Random.RandomRangeInt(0, validTargets.Count - 1)];
            messageWriter.WritePlayer(target);
            innerNetObject.GetComponent<RoleHelper>().Target = target;
        }
        public override void Handle(PlayerControl innerNetObject, MessageReader messageReader)
        {
            innerNetObject.GetPlayerComponent<RoleHelper>().Target = messageReader.ReadPlayer();
        }
    }
}
