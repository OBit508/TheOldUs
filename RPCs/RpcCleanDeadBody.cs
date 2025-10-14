using BepInEx.Unity.IL2CPP.Utils.Collections;
using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.RPCs
{
    internal class RpcCleanDeadBody : CustomRpc<DeadBody>
    {
        public override void Write(MessageWriter writer, DeadBody value)
        {
            writer.WriteDeadBody(value);
            value.StartCoroutine(CoClean(value));
        }
        public override void Handle(MessageReader reader)
        {
            DeadBody body = reader.ReadBody();
            body.StartCoroutine(CoClean(body));
        }
        public static Il2CppSystem.Collections.IEnumerator CoClean(DeadBody body)
        {
            System.Collections.IEnumerator coClean()
            {
                Color color = Color.white;
                while (color.a > 0)
                {
                    color.a -= 0.05f;
                    foreach (SpriteRenderer rend in body.bodyRenderers)
                    {
                        rend.color = color;
                    }
                    yield return new WaitForSeconds(0.05f);
                }
                body.myCollider.enabled = false;
                foreach (SpriteRenderer rend in body.bodyRenderers)
                {
                    rend.color = Color.white;
                    rend.gameObject.SetActive(false);
                }
            }
            return coClean().WrapToIl2Cpp();
        }
    }
}
