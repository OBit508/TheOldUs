using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Assets;
using TheOldUs.Components;
using UnityEngine;
using static Il2CppSystem.Linq.Expressions.Interpreter.CastInstruction.CastInstructionNoT;

namespace TheOldUs.Roles.Jailer
{
    internal class ArrestRpc : CustomRpc<(PlayerControl target, bool arrested)>
    {
        public override void Write(MessageWriter writer, (PlayerControl target, bool arrested) value)
        {
            writer.WritePlayer(value.target);
            writer.Write(value.arrested);
            if (value.arrested)
            {
                Prefabs.Jail.Instantiate().Player = value.target;
            }
            else
            {
                GameObject.Destroy(PlayerJail.Jails[value.target].gameObject);
                PlayerJail.Jails.Remove(value.target);
            }
        }
        public override void Handle(MessageReader reader)
        {
            PlayerControl target = reader.ReadPlayer();
            bool arrested = reader.ReadBoolean();
            if (arrested)
            {
                Prefabs.Jail.Instantiate().Player = target;
            }
            else
            {
                GameObject.Destroy(PlayerJail.Jails[target].gameObject);
                PlayerJail.Jails.Remove(target);
            }
        }
    }
}
