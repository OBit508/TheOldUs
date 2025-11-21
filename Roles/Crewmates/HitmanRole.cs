using FungleAPI.Configuration.Attributes;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Player;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Buttons;
using TheOldUs.Components;
using TheOldUs.Patches;
using TheOldUs.Roles.BaseRole;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Roles.Crewmates
{
    internal class HitmanRole : CrewmateBase, ICustomRole
    {
        [ModdedNumberOption("Reload Cooldown", 5, 120)]
        public static float ReloadCooldown => 15;
        [ModdedNumberOption("Reload Uses", 0, 30, 1, null, true, NumberSuffixes.None)]
        public static int ReloadUses => 5;
        public static bool CanShoot;
        public static ControllerHelper myController = new ControllerHelper();
        public ModdedTeam Team { get; } = ModdedTeam.Crewmates;
        public StringNames RoleName { get; } = new Translator("Hitman").StringName;
        public StringNames RoleBlur { get; } = new Translator("Shoot the impostors.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("Use your gun to shoot the impostors.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Hitman can use a gun to shoot players.").StringName;
        public Color RoleColor { get; } = Palette.Orange;
        public List<CustomAbilityButton> Buttons { get; } = new List<CustomAbilityButton>() { CustomAbilityButton.Instance<EquipGunButton>(), CustomAbilityButton.Instance<UnequipGunButton>(), CustomAbilityButton.Instance<ReloadButton>() };
        public void Start()
        {
            if (Player != null && Player.AmOwner)
            {
                CanShoot = true;
                myController = new ControllerHelper();
            }
        }
        public void Update()
        {
            if (Player != null && Player.AmOwner)
            {
                myController.Update();
                ControllerHelper.TouchState touch;
                if (CanShoot && Player.GetComponent<RoleHelper>().ShowingGun)
                {
                    if (myController.AnyStarted(out touch))
                    {
                        foreach (Collider2D collider in Physics2D.OverlapPointAll(touch.Position))
                        {
                            PlayerControl player = collider.GetComponent<PlayerControl>();
                            if (player != null && !player.Data.IsDead && !player.AmOwner && !player.inVent && player.Visible && !player.Data.Disconnected)
                            {
                                CanShoot = false;
                                Player.RpcCustomMurderPlayer(player, MurderResultFlags.DecisionByHost, false, true, false);
                                break;
                            }
                        }
                    }
                    if (TargetBehaviour.targets.Count <= 0)
                    {
                        foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                        {
                            TOUAssets.Target.Instantiate(player.transform).GetComponent<TargetBehaviour>().Predicate = new Predicate<PlayerControl>(p => !p.Data.IsDead && !p.AmOwner && !p.inVent && p.Visible && !p.Data.Disconnected);
                        }
                    }
                }
                else if (TargetBehaviour.targets.Count > 0)
                {
                    TargetBehaviour.DestroyAll();
                }
            }
        }
    }
}
