using FungleAPI.Attributes;
using FungleAPI.Base.Roles;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Player;
using FungleAPI.Role;
using FungleAPI.Teams;
using FungleAPI.Translation;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Roles.Neutrals
{
    internal class ArsonistRole : NeutralBase, ICustomRole
    {
        [TranslationHelper("arsonist_gasolineCooldown")]
        [ModdedNumberOption(null, 5, 50)]
        public static int GasolineCooldown => 20;
        public ModdedTeam Team { get; } = ModdedTeam.Neutrals;
        public StringNames RoleName => TouTranslation.ArsonistName;
        public StringNames RoleBlur => TouTranslation.ArsonistBlur;
        public StringNames RoleBlurMed => TouTranslation.ArsonistBlurMed;
        public Color RoleColor { get; } = TouPalette.ArsonistColor;
        public override bool ValidTarget(NetworkedPlayerInfo target)
        {
            return base.ValidTarget(target) && !target.Object.GetPlayerComponent<RoleHelper>().Soaked;
        }
        public RoleHintType HintType => RoleHintType.MiraAPI_RoleTab;
    }
}
