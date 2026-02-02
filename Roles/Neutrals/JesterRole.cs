using FungleAPI.Attributes;
using FungleAPI.Base.Roles;
using FungleAPI.Configuration;
using FungleAPI.Configuration.Attributes;
using FungleAPI.GameOver;
using FungleAPI.GameOver.Ends;
using FungleAPI.Networking;
using FungleAPI.Networking.RPCs;
using FungleAPI.Role;
using FungleAPI.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.GameOvers;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Roles.Neutrals
{
    internal class JesterRole : NeutralBase, ICustomRole
    {
        [TranslationHelper("jester_canVent")]
        [ModdedToggleOption(null)]
        public static bool Vent => true;
        public ModdedTeam Team { get; } = ModdedTeam.Neutrals;
        public StringNames RoleName => TouTranslation.JesterName;
        public StringNames RoleBlur => TouTranslation.JesterBlur;
        public StringNames RoleBlurMed => TouTranslation.JesterBlurMed;
        public Color RoleColor { get; } = TouPalette.JesterColor;
        public bool CanUseVent => Vent;
        public override void OnDeath(DeathReason reason)
        {
            if (reason == DeathReason.Exile && Player.AmOwner)
            {
                TouNeutralGameOver.WinnerId = Player.PlayerId;
                TouNeutralGameOver.Win = TouNeutralGameOver.NeutralWin.Jester;
                GameManager.Instance.RpcEndGame<TouNeutralGameOver>();
            }
        }
        public RoleHintType HintType => RoleHintType.MiraAPI_RoleTab;
    }
}
