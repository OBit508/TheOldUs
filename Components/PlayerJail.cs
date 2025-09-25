using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FungleAPI.Attributes;
using FungleAPI.Networking;
using TheOldUs.Roles.Jailer;

namespace TheOldUs.Components
{
    [RegisterTypeInIl2Cpp]
    public class PlayerJail : MonoBehaviour
    {
        public static Dictionary<PlayerControl, PlayerJail> Jails = new Dictionary<PlayerControl, PlayerJail>();
        public PlayerControl Player;
        public void Start()
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - 0.1f);
            Jails.Add(Player, this);
        }
        public void Update()
        {
            if (Player.transform.position != transform.position)
            {
                Player.NetTransform.SnapTo(transform.position);
                if (Player.AmOwner && Player.Data.IsDead)
                {
                    CustomRpcManager.Instance<ArrestRpc>().Send((Player, false), Player.NetId);
                }
            }
        }
    }
}
