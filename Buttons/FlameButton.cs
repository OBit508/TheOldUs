using FungleAPI.Base.Buttons;
using FungleAPI.GameOver;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Player;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.GameOvers;
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
        public override bool CanUse() => base.CanUse() && PlayerControl.AllPlayerControls.FindAll(new Predicate<PlayerControl>(p => !p.AmOwner && !p.Data.IsDead && !p.GetPlayerComponent<RoleHelper>().Soaked).ToIl2CppPredicate()).Count <= 0;
        public override string OverrideText => "Flame";
        public override float Cooldown => 5;
        public override int MaxUses => 1;
        public override Color32 TextOutlineColor { get; } = new Color32(173, 95, 5, byte.MaxValue);
        public override Sprite ButtonSprite => TouAssets.Flame;
        public override void OnClick()
        {
            TouNeutralGameOver.WinnerId = Player.PlayerId;
            TouNeutralGameOver.Win = TouNeutralGameOver.NeutralWin.Arsonist;
            GameManager.Instance.RpcEndGame<TouNeutralGameOver>();
        }
    }
}
