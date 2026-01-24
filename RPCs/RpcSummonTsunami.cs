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

namespace TheOldUs.RPCs
{
    internal class RpcSummonTsunami : SimpleRpc
    {
        public override void Write(MessageWriter writer)
        {
            TouAssets.Tsunami.Instantiate().transform.position = new Vector3(-29, -4.6f, -20);
        }
        public override void Handle(MessageReader reader)
        {
            TouAssets.Tsunami.Instantiate().transform.position = new Vector3(-29, -4.6f, -20);
        }
    }
}
