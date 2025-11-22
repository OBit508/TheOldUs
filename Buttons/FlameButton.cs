using FungleAPI.Base.Buttons;
using FungleAPI.GameOver;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.Impostors;
using TheOldUs.Roles.Neutrals;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class FlameButton : RoleButton<ArsonistRole>
    {
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override bool CanUse => ArsonistRole.SoakedPlayers[Player].Count(p => !p.AmOwner && !p.Data.IsDead && !JailBehaviour.ArrestedPlayers.Contains(p)) == PlayerControl.AllPlayerControls.FindAll(new Predicate<PlayerControl>(p => !p.AmOwner && !p.Data.IsDead && !JailBehaviour.ArrestedPlayers.Contains(p)).ToIl2CppPredicate()).Count;
        public override bool CanClick => CanUse;
        public override string OverrideText => "Flame";
        public override float Cooldown => 5;
        public override bool HaveUses => true;
        public override int NumUses => 1;
        public override Color32 TextOutlineColor { get; } = new Color32(173, 95, 5, byte.MaxValue);
        public override Sprite ButtonSprite => TOUAssets.Flame;
        public override void Click()
        {
            GameManager.Instance.RpcEndGame(new List<NetworkedPlayerInfo>() { Player.Data }, "Arsonist's victory", TextOutlineColor, TextOutlineColor);
        }
    }
}
