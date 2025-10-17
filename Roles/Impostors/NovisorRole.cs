using FungleAPI.Hud;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Buttons;
using TheOldUs.Components;
using TheOldUs.Roles.BaseRole;
using UnityEngine;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Utilities;
using FungleAPI.Networking;
using TheOldUs.RPCs;
using TheOldUs.TOU;

namespace TheOldUs.Roles.Impostors
{
    public class NovisorRole : ImpostorBase, ICustomRole
    {
        [ModdedNumberOption("Transform Cooldown", null, 5, 60)]
        public static float TransformCooldown => 20;
        [ModdedNumberOption("Transform Duration", null, 5, 60)]
        public static float TransformDuration => 20;
        [ModdedNumberOption("Split Cooldown", null, 5, 60)]
        public static float SplitCooldown => 20;
        [ModdedNumberOption("Invisiblity Cooldown", null, 5, 60)]
        public static float InvisibleCooldown => 20;
        [ModdedNumberOption("Invisiblity Duration", null, 5, 60)]
        public static float InvisibleDuration => 10;
        [ModdedNumberOption("Haunt Cooldown", null, 5, 60)]
        public static float HauntCooldown => 10;
        [ModdedNumberOption("Clone Speed", null, 1, 10, 0.5f, null, false, NumberSuffixes.Multiplier)]
        public static float CloneSpeed => 1;
        [ModdedNumberOption("Clone Lifetime", null, 1, 120)]
        public static float CloneLifetime => 10;
        [ModdedNumberOption("Haunt Speed", null, 3, 20, 1, null, false, NumberSuffixes.Multiplier)]
        public static float HauntSpeed => 7;
        public PlayerAnimationHelper AnimationHelper;
        public bool Transformed;
        public PlayerControl Target;
        public ModdedTeam Team { get; } = ModdedTeam.Impostors;
        public StringNames RoleName { get; } = new Translator("Novisor").StringName;
        public StringNames RoleBlur { get; } = new Translator("Haunt the crew.").StringName;
        public StringNames RoleBlurMed => RoleBlur;
        public StringNames RoleBlurLong => RoleBlur;
        public Color RoleColor { get; } = Color.gray;
        public List<CustomAbilityButton> Buttons { get; } = new List<CustomAbilityButton>() { CustomAbilityButton.Instance<NovisorTransformButton>(), CustomAbilityButton.Instance<SplitButton>(), CustomAbilityButton.Instance<NovisorInvisibleButton>(), CustomAbilityButton.Instance<HauntButton>() };
        public bool UseVanillaKillButton => true;
        public bool CanUseVent => !Transformed;
        public void Start()
        {
            if (Player != null)
            {
                AnimationHelper = Player.GetComponent<PlayerAnimationHelper>();
                AnimationHelper.IdleAnim = TOUAssets.NovisorIdle;
            }
        }
        public void Update()
        {
            if (Player != null)
            {
                if (AnimationHelper != null)
                {
                    AnimationHelper.Transformed = Transformed;
                    AnimationHelper.RunAnim = Target != null ? TOUAssets.NovisorAttack : TOUAssets.NovisorRun;
                }
                if (Target != null)
                {
                    if (Target.Data.IsDead || MeetingHud.Instance != null || !Transformed)
                    {
                        Target = null;
                    }
                    else if (Transformed)
                    {
                        Player.rigidbody2D.velocity = (Target.transform.position - Player.transform.position).normalized;
                        Player.transform.position = Vector3.MoveTowards(Player.transform.position, Target.transform.position, Time.deltaTime * HauntSpeed);
                        if (Player.AmOwner && Vector2.Distance(Player.transform.position, Target.transform.position) <= 0.3f)
                        {
                            Player.RpcCustomMurderPlayer(Target, MurderResultFlags.DecisionByHost, false);
                            CustomRpcManager.Instance<RpcHaunt>().Send((Player, null), Player.NetId);
                        }
                    }
                }
                Player.Collider.enabled = !Transformed;
            }
        }
        public override void OnMeetingStart()
        {
            Transformed = false;
        }
    }
}
