using FungleAPI.GameOver;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.GameOvers
{
    internal class NotSkeldGameOver : CustomGameOver
    {
        public override string WinText => TouTranslation.NotSkeld.GetString();
        public override Color BackgroundColor { get; } = TouPalette.NotSkeldColor;
        public override GameOverReason Reason { get; } = GameOverManager.GetValidGameOver();
        public override void SetData()
        {
            Winners = new List<CachedPlayerData>();
        }
    }
}
