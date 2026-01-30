using FungleAPI.GameOver;
using FungleAPI.Role;
using FungleAPI.Utilities;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Neutrals;
using UnityEngine;

namespace TheOldUs.GameOvers
{
    internal class TouNeutralGameOver : CustomGameOver
    {
        public static NeutralWin Win;
        public static byte WinnerId;
        public string text;
        public Color color;
        public override string WinText => text;
        public override Color BackgroundColor => color;
        public override GameOverReason Reason { get; } = GameOverManager.GetValidGameOver();
        public override void Serialize(MessageWriter messageWriter)
        {
            messageWriter.Write(WinnerId);
            messageWriter.Write((int)Win);
        }
        public override void Deserialize(MessageReader messageReader)
        {
            WinnerId = messageReader.ReadByte();
            Win = (NeutralWin)messageReader.ReadInt32();
        }
        public override void SetData()
        {
            Winners.Clear();
            NetworkedPlayerInfo networkedPlayerInfo = PlayerControl.AllPlayerControls.FirstOrDefault(p => p.PlayerId == WinnerId).Data;
            Winners.Add(new CachedPlayerData(networkedPlayerInfo));
            if (Win == NeutralWin.Jester)
            {
                text = "Jester's victory";
                color = new Color32(173, 54, 181, byte.MaxValue);
                return;
            }
            text = "Arsonist's victory";
            color = new Color32(173, 95, 5, byte.MaxValue);
        }
        public enum NeutralWin
        {
            Jester,
            Arsonist
        }
    }
}
