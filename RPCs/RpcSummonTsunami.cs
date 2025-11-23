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
    internal class RpcSummonTsunami : CustomRpc
    {
        public override void Write(MessageWriter writer)
        {
            TOUAssets.Tsunami.Instantiate().transform.position = new Vector3(-29, -4.6f, -20);
        }
        public override void Handle(MessageReader reader)
        {
            TOUAssets.Tsunami.Instantiate().transform.position = new Vector3(-29, -4.6f, -20);
        }
    }
}
