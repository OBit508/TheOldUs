using FungleAPI.GameOver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.GameOvers
{
    internal class NotSkeldGameOver : CustomGameOver
    {
        public override string WinText => "Only The Skeld map is supported";
        public override Color BackgroundColor { get; } = new Color32(41, 41, 41, byte.MaxValue);
        public override GameOverReason Reason => GameOverManager.GetValidGameOver();
        public override List<NetworkedPlayerInfo> GetWinners()
        {
            return new List<NetworkedPlayerInfo>();
        }
    }
}
