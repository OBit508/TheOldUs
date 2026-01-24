using FungleAPI.Base.Rpc;
using FungleAPI.Hud;
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

namespace TheOldUs.RPCs
{
    public class RpcSplit : AdvancedRpc<Vector2>
    {
        public override void Write(MessageWriter writer, Vector2 value)
        {
            writer.WriteVector2(value);
            Split(value);
        }
        public override void Handle(MessageReader reader)
        {
            Split(reader.ReadVector2());
        }
        public void Split(Vector2 vec)
        {
            Vector3[] array = new Vector3[]
            {
                new Vector3(1f, 0f, 0f),
                new Vector3(1f, 1f, 0f),
                new Vector3(1f, -1f, 0f),
                new Vector3(0f, 1f, 0f),
                new Vector3(0f, -1f, 0f),
                new Vector3(-1f, 1f, 0f),
                new Vector3(-1f, 0f, 0f),
                new Vector3(-1f, -1f, 0f)
            };
            foreach (Vector2 v in array)
            {
                FakeNovisorComp fakeNovisor = TouAssets.FakeNovisor.Instantiate();
                fakeNovisor.transform.position = vec;
                fakeNovisor.Direction = v;
            }
        }
    }
}
