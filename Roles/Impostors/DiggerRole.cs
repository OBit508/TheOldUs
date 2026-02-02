using FungleAPI.Base.Roles;
using FungleAPI.Configuration;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Hud;
using FungleAPI.Networking;
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
    internal class DiggerRole : ImpostorBase, ICustomRole
    {
        [ModdedNumberOption("Dig Vent Cooldown", 1, 60)]
        public static float CreateVentCooldown => 10;
        [ModdedNumberOption("Max Vents", 0, 120, 1, null, true, NumberSuffixes.None)]
        public static int MaxVents => 7;
        [ModdedNumberOption("Connect Distance", 0.5f, 10, 0.5f)]
        public static float ConnectDistance => 4;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName { get; } = TouTranslation.DiggerName;
        public StringNames RoleBlur { get; } = TouTranslation.DiggerBlur;
        public StringNames RoleBlurMed { get; } = TouTranslation.DiggerBlurMed;
        public Color RoleColor { get; } = TouPalette.DiggerColor;
        public RoleHintType HintType => RoleHintType.MiraAPI_RoleTab;
    }
}
