using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.Roles.Psychic
{
    internal class MoveTaskRpc : CustomRpc<(Console console, Vector3 position)>
    {
        public override void Write(MessageWriter writer, (Console console, Vector3 position) value)
        {
            writer.Write(value.console.ConsoleId);
            writer.Write((int)value.console.Room);
            writer.Write(value.console.TaskTypes.Count);
            for (int i = 0; i < value.console.TaskTypes.Count; i++)
            {
                writer.Write((int)value.console.TaskTypes[i]);
            }
            writer.WriteVector3(value.position);
            value.console.transform.position = value.position;
        }
        public override void Handle(MessageReader reader)
        {
            List<TaskTypes> types = new List<TaskTypes>();
            int id = reader.ReadInt32();
            SystemTypes room = (SystemTypes)reader.ReadInt32();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                types.Add((TaskTypes)reader.ReadInt32());
            }
            Vector3 position = reader.ReadVector3();
            foreach (Console console in ShipStatus.Instance.AllConsoles)
            {
                if (console.ConsoleId == id && console.Room == room && console.TaskTypes.Count == count)
                {
                    bool flag = true;
                    for (int i = 0; i < console.TaskTypes.Count; i++)
                    {
                        if (console.TaskTypes[i] != types[i])
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        console.transform.position = position;
                        break;
                    }
                }
            }
        }
    }
}
