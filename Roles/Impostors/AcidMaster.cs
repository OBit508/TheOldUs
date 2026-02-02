using AmongUs.GameOptions;
using FungleAPI.Base.Roles;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Role;
using FungleAPI.Teams;
using FungleAPI.Translation;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Roles.Impostors
{
    internal class AcidMaster : ImpostorBase, ICustomRole
    {
        [ModdedNumberOption("Tsunami Speed", 1, 15, 0.5f, null, true, NumberSuffixes.Multiplier)]
        public static float TsunamiSpeed => 5;
        [ModdedNumberOption("Acid Cooldown", 5, 60)]
        public static float AcidCooldown => 15;
        [ModdedNumberOption("Acid Uses", 0, 10, 1, null, true, NumberSuffixes.None)]
        public static int AcidUses => 3;
        [ModdedNumberOption("Extra kill cooldown", 0, 60)]
        public static float ExtraKillCooldown => 15;
        [ModdedNumberOption("Deadbody dissolve delay", 5, 30)]
        public static float DissolveDelay => 15;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName => TouTranslation.AcidMasterName;
        public StringNames RoleBlur => TouTranslation.AcidMasterBlur;
        public StringNames RoleBlurMed => TouTranslation.AcidMasterBlurMed;
        public Color RoleColor { get; } = TouPalette.AcidMasterColor;
        public DeadBodyType CreatedDeadBodyOnKill => DeadBodyType.Viper;
        public override void KillAnimSpecialSetup(DeadBody deadBody, PlayerControl killer, PlayerControl victim)
        {
            if (killer == Player)
            {
                ViperDeadBody viperDeadBody = deadBody.SafeCast<ViperDeadBody>();
                if (viperDeadBody != null)
                {
                    viperDeadBody.SetupViperInfo(DissolveDelay, Player, victim);
                }
            }
        }
        public KillButtonConfig CreateKillConfig()
        {
            KillButtonConfig killButtonConfig = new KillButtonConfig();
            killButtonConfig.Cooldown = () => GameOptionsManager.Instance.CurrentGameOptions.GetFloat(FloatOptionNames.KillCooldown) + ExtraKillCooldown;
            killButtonConfig.InitializeButton = delegate
            {
                killButtonConfig.Button.ChangeGraphic(RoleManager.Instance.GetRole(RoleTypes.Viper).SafeCast<ViperRole>().killSprite);
                killButtonConfig.Button.ChangeButtonText(StringNames.ViperAbility);
            };
            return killButtonConfig;
        }
        public RoleHintType HintType => RoleHintType.MiraAPI_RoleTab;
    }
}
