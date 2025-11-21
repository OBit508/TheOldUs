using FungleAPI.Configuration;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using FungleAPI.Utilities;
using Il2CppSystem;
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
using UnityEngine;
using UnityEngine._Scripting.APIUpdating;

namespace TheOldUs.Roles.Impostors
{
    internal class PsychicRole : ImpostorBase, ICustomRole
    {
        [ModdedNumberOption("Move Console Cooldown", 5, 60)]
        public static float MoveConsoleCooldown => 15;
        [ModdedNumberOption("Move Player Cooldown", 5, 60)]
        public static float MovePlayerCooldown => 20;
        [ModdedNumberOption("Move Console Duration", 5, 60)]
        public static float MoveConsoleDuration => 20;
        [ModdedNumberOption("Move Player Duration", 5, 60)]
        public static float MovePlayerDuration => 20;
        [ModdedNumberOption("Time until the object returns", 5, 60)]
        public static float ObjetTime => 10;
        public static bool MovingConsole;
        public static bool MovingPlayer;
        public static Console movedConsole;
        public static PlayerControl movedPlayer;
        public static bool LastCheck;
        public static ControllerHelper.TouchState Touch;
        public static ControllerHelper myController;
        public static float timer;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName { get; } = new Translator("Psychic").StringName;
        public StringNames RoleBlur { get; } = new Translator("Use your mind to move things.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("You can use your mind to move things.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Psychic can move players and tasks and after a few seconds they return to their original position.").StringName;
        public Color RoleColor { get; } = new Color32(161, 121, 171, byte.MaxValue);
        public List<CustomAbilityButton> Buttons { get; } = new List<CustomAbilityButton>() { CustomAbilityButton.Instance<MoveTaskButton>(), CustomAbilityButton.Instance<MovePlayerbutton>() };
        public bool UseVanillaKillButton => false;
        public bool CanKill => PlayerControl.AllPlayerControls.FindAll(Il2CppUtils.ToIl2CppPredicate(new System.Predicate<PlayerControl>(p => p.Data.Role.GetTeam() == Team))).Count <= 1;
        public void Update()
        {
            if (Player != null && Player.AmOwner)
            {
                if (!LastCheck && (MovingConsole || MovingPlayer))
                {
                    Zoom(5);
                    LastCheck = true;
                }
                if (!MovingConsole && !MovingPlayer && LastCheck)
                {
                    Zoom(3);
                    LastCheck = false;
                }
                myController.Update();
                ControllerHelper.TouchState touch;
                if (Touch == null && (MovingConsole || MovingPlayer) && myController.AnyStarted(out touch))
                {
                    Touch = touch;
                    Collider2D[] hits = Physics2D.OverlapPointAll(Touch.Position);
                    if (MovingConsole)
                    {
                        foreach (Collider2D collider in hits)
                        {
                            Console console = collider.GetComponent<Console>();
                            if (console != null && (console.TaskTypes.Count > 0 || console.ValidTasks.Count > 0) && !ShipStatusPatch.WaitingConsoles.ContainsKey(console))
                            {
                                movedConsole = console;
                                CustomRpcManager.Instance<RpcStartMove>().Send((Player, null, console, console.transform.position), Player.NetId);
                                break;
                            }
                        }
                    }
                    if (MovingPlayer)
                    {
                        foreach (Collider2D collider in hits)
                        {
                            PlayerControl player = collider.GetComponent<PlayerControl>();
                            if (player != null && !player.Data.IsDead && !player.AmOwner && !player.inVent && player.Visible && !player.Data.Disconnected && !ShipStatusPatch.WaitingPlayers.ContainsKey(player))
                            {
                                movedPlayer = player;
                                CustomRpcManager.Instance<RpcStartMove>().Send((Player, player, null, player.transform.position), Player.NetId);
                                break;
                            }
                        }
                    }
                    if (movedConsole == null && movedPlayer == null)
                    {
                        Touch = null;
                    }
                }
                if (Touch != null)
                {
                    if (Touch.dragState == DragState.Released)
                    {
                        if (MovingConsole && movedConsole != null)
                        {
                            CustomAbilityButton.Instance<MoveTaskButton>().Destransform();
                        }
                        if (MovingPlayer && movedPlayer != null)
                        {
                            CustomAbilityButton.Instance<MovePlayerbutton>().Destransform();
                        }
                        Touch = null;
                    }
                    else if (Touch.dragState == DragState.Holding)
                    {
                        timer -= Time.deltaTime;
                        if (timer <= 0)
                        {
                            if (MovingConsole)
                            {
                                if (movedConsole != null)
                                {
                                    CustomRpcManager.Instance<RpcMove>().Send((null, movedConsole, Touch.Position), Player.NetId);
                                }
                                else
                                {
                                    MovingConsole = false;
                                    Touch = null;
                                }
                            }
                            else if (MovingPlayer)
                            {
                                if (movedPlayer != null)
                                {
                                    CustomRpcManager.Instance<RpcMove>().Send((movedPlayer, null, Touch.Position), Player.NetId);
                                }
                                else
                                {
                                    MovingPlayer = false;
                                    Touch = null;
                                }
                            }
                            timer = 0.02f;
                        }
                        if (movedConsole != null)
                        {
                            movedConsole.transform.position = Touch.Position;
                        }
                    }
                }
            }
        }
        public void Start()
        {
            if (Player != null && Player.AmOwner)
            {
                MovingConsole = false;
                MovingPlayer = false;
                movedConsole = null;
                movedPlayer = null;
                myController = new ControllerHelper();
            }
        }
        public void Zoom(float size)
        {
            Camera.main.orthographicSize = size;
            HudManager.Instance.UICamera.orthographicSize = size;
            HudManager.Instance.ShadowQuad.gameObject.SetActive(size == 3);
        }
    }
}
