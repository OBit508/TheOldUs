using AmongUs.GameOptions;
using FungleAPI.Attributes;
using FungleAPI.Base.Roles;
using FungleAPI.Configuration;
using FungleAPI.Configuration.Attributes;
using FungleAPI.GameOver;
using FungleAPI.GameOver.Ends;
using FungleAPI.Hud;
using FungleAPI.Role;
using FungleAPI.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Buttons;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Roles.Impostors
{
    internal class JanitorRole : ImpostorBase, ICustomRole
    {
        [TranslationHelper("janitor_cleanCooldown")]
        [ModdedNumberOption(null, 5, 60)]
        public static float CleanCooldown => 15;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName => TouTranslation.JanitorName;
        public StringNames RoleBlur => TouTranslation.JanitorBlur;
        public StringNames RoleBlurMed => TouTranslation.JanitorBlurMed;
        public Color RoleColor { get; } = TouPalette.JanitorColor;
        public bool UseVanillaKillButton => true;
        public bool CanKill => PlayerControl.AllPlayerControls.FindAll(FungleAPI.Utilities.Il2CppUtils.ToIl2CppPredicate(new Predicate<PlayerControl>(p => p.Data.Role.GetTeam() == Team))).Count <= 1;
        public KillButtonConfig CreateKillConfig()
        {
            KillButtonConfig killButtonConfig = new KillButtonConfig();
            killButtonConfig.CanUse = () => CanKill && killButtonConfig.Button.isActiveAndEnabled && killButtonConfig.Button.currentTarget != null && !killButtonConfig.Button.isCoolingDown && !PlayerControl.LocalPlayer.Data.IsDead && PlayerControl.LocalPlayer.CanMove;
            return killButtonConfig;
        }
        public RoleHintType HintType => RoleHintType.MiraAPI_RoleTab;
    }
}
