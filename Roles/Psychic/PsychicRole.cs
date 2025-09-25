using FungleAPI.Networking;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Utilities;
using UnityEngine._Scripting.APIUpdating;
using TheOldUs.Roles.BaseRole;

namespace TheOldUs.Roles.Psychic
{
    internal class PsychicRole : ImpostorBase, ICustomRole
    {
        [ModdedNumberOption("Move Console Cooldown", null, 5, 60)]
        public static float MoveConsoleCooldown => 15;
        [ModdedNumberOption("Move Player Cooldown", null, 5, 60)]
        public static float MovePlayerCooldown => 20;
        [ModdedNumberOption("Move Console Duration", null, 5, 60)]
        public static float MoveConsoleDuration => 20;
        [ModdedNumberOption("Move Player Duration", null, 5, 60)]
        public static float MovePlayerDuration => 20;
        [ModdedNumberOption("Time until the object returns", null, 5, 60)]
        public static float ObjetTime => 10;
        public static List<(Console console, Vector3 position, ChangeableValue<float> timer)> MovedConsoles = new List<(Console console, Vector3 position, ChangeableValue<float> timer)>();
        public static List<(PlayerControl player, Vector2 position, ChangeableValue<float> timer)> MovedPlayers = new List<(PlayerControl player, Vector2 position, ChangeableValue<float> timer)>();
        public static bool MovingConsole;
        public static bool MovingPlayer;
        public static ControllerHelper Controller = new ControllerHelper();
        public static Console console;
        public static PlayerControl player;
        public bool LastCheck;
        public float timer;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName { get; } = new Translator("Psychic").StringName;
        public StringNames RoleBlur { get; } = new Translator("Use your mind to move things.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("You can use your mind to move things.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Psychic can move players and tasks and after a few seconds they return to their original position.").StringName;
        public Color RoleColor { get; } = new Color32(161, 121, 171, byte.MaxValue);
        public RoleConfig Configuration => new RoleConfig(this)
        {
            Buttons = new CustomAbilityButton[] { CustomAbilityButton.Instance<MoveTask>(), CustomAbilityButton.Instance<MovePlayer>() },
            GhostRole = AmongUs.GameOptions.RoleTypes.ImpostorGhost
        };
        public void Update()
        {
            if (Player != null && Player.AmOwner)
            {
                if (!LastCheck && MovingConsole || MovingPlayer)
                {
                    Zoom(5);
                    LastCheck = true;
                }
                if (!MovingConsole && !MovingPlayer && LastCheck)
                {
                    Zoom(3);
                    LastCheck = false;
                }
                Controller.Update();
                if (Controller.State == DragState.TouchStart)
                {
                    Collider2D[] hits = Physics2D.OverlapPointAll(Controller.HoverPosition);
                    if (MovingConsole)
                    {
                        foreach (Collider2D collider in hits)
                        {
                            if (collider.GetComponent<Console>() != null)
                            {
                                console = collider.GetComponent<Console>();
                                MovedConsoles.Add((console, console.transform.position, new ChangeableValue<float>(ObjetTime)));
                                break;
                            }
                        }
                    }
                    if (MovingPlayer)
                    {
                        foreach (Collider2D collider in hits)
                        {
                            PlayerControl player = collider.GetComponent<PlayerControl>();
                            if (player != null && !player.Data.IsDead && !player.AmOwner)
                            {
                                PsychicRole.player = player;
                                MovedPlayers.Add((player, player.transform.position, new ChangeableValue<float>(ObjetTime)));
                                break;
                            }
                        }
                    }
                }
                else if (Controller.State == DragState.Released)
                {
                    if (MovingConsole && console != null)
                    {
                        CustomAbilityButton.Instance<MoveTask>().SetTransformDuration(0.001f);
                    }
                    if (MovingPlayer && player != null)
                    {
                        CustomAbilityButton.Instance<MovePlayer>().SetTransformDuration(0.001f);
                    }
                    console = null;
                    player = null;
                }
                if (Controller.State == DragState.Holding)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        if (MovingConsole && console != null)
                        {
                            CustomRpcManager.Instance<MoveTaskRpc>().Send((console, Controller.HoverPosition), Player.NetId);
                        }
                        else if (MovingPlayer && player != null)
                        {
                            CustomRpcManager.Instance<MovePlayerRpc>().Send((player, Controller.HoverPosition), Player.NetId);
                        }
                        timer = 0.02f;
                    }
                    if (console != null)
                    {
                        console.transform.position = Controller.HoverPosition;
                    }
                }
                for (int i = 0; i < MovedConsoles.Count; i++)
                {
                    (Console console, Vector3 position, ChangeableValue<float> timer) pair = MovedConsoles[i];
                    pair.timer.Value -= Time.deltaTime;
                    if (pair.timer.Value <= 0)
                    {
                        CustomRpcManager.Instance<MoveTaskRpc>().Send((pair.console, pair.position), Player.NetId);
                        MovedConsoles.Remove(pair);
                    }
                }
                for (int i = 0; i < MovedPlayers.Count; i++)
                {
                    (PlayerControl player, Vector3 position, ChangeableValue<float> timer) pair = MovedPlayers[i];
                    pair.timer.Value -= Time.deltaTime;
                    if (pair.timer.Value <= 0)
                    {
                        CustomRpcManager.Instance<MovePlayerRpc>().Send((pair.player, pair.position), Player.NetId);
                        MovedPlayers.Remove(pair);
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
                console = null;
                player = null;
                MovedConsoles.Clear();
                MovedPlayers.Clear();
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
