using FungleAPI.Freeplay;
using FungleAPI.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.RPCs;

namespace TheOldUs.TOU
{
    public class TOUFolder : ModFolderConfig
    {
        [Item("Get Arrested", null, null)]
        public static Action Arrest { get; } = new Action(delegate
        {
            CustomRpcManager.Instance<RpcArrestPlayer>().Send((PlayerControl.LocalPlayer, true), PlayerControl.LocalPlayer.NetId);
        });
    }
}
