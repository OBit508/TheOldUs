using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Impostors;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Buttons
{
    public class HauntButton : RoleButton<NovisorRole>
    {
        public static ShapeshifterMinigame ShapPanelPrefab = RoleManager.Instance.AllRoles.ToSystemList().FirstOrDefault(role => role.Role == AmongUs.GameOptions.RoleTypes.Shapeshifter).SafeCast<ShapeshifterRole>().ShapeshifterMenu;
        public static ShapeshifterMinigame ShapMinigame;
        public NovisorRole Novisor
        {
            get
            {
                if (PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.Data != null && PlayerControl.LocalPlayer.Data.Role != null && PlayerControl.LocalPlayer.Data.Role.SafeCast<NovisorRole>() != null)
                {
                    return PlayerControl.LocalPlayer.Data.Role.SafeCast<NovisorRole>();
                }
                return null;
            }
        }
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override bool CanClick => Novisor != null && Novisor.Transformed && Novisor.Target == null && ShapMinigame == null;
        public override bool CanUse => CanClick;
        public override float Cooldown => NovisorRole.HauntCooldown;
        public override string OverrideText => "Haunt";
        public override Sprite ButtonSprite => TouAssets.TemporaryButton;
        public override Color32 TextOutlineColor => Color.red;
        public override void Click()
        {
            if (Novisor != null)
            {
                SetCooldown(0);
                ShapMinigame = GameObject.Instantiate<ShapeshifterMinigame>(ShapPanelPrefab, Camera.main.transform);
                Minigame.Instance = ShapMinigame;
                ShapMinigame.timeOpened = Time.realtimeSinceStartup;
                if (PlayerControl.LocalPlayer)
                {
                    if (MapBehaviour.Instance)
                    {
                        MapBehaviour.Instance.Close();
                    }
                    PlayerControl.LocalPlayer.MyPhysics.SetNormalizedVelocity(Vector2.zero);
                }
                ShapMinigame.StartCoroutine(ShapMinigame.CoAnimateOpen());
                List<PlayerControl> list = new List<PlayerControl>();
                PlayerControl.AllPlayerControls.ForEach(new Action<PlayerControl>(delegate (PlayerControl player)
                {
                    if (player != PlayerControl.LocalPlayer && !player.Data.IsDead)
                    {
                        list.Add(player);
                    }
                }));
                ShapMinigame.potentialVictims = new Il2CppSystem.Collections.Generic.List<ShapeshifterPanel>();
                for (int i = 0; i < list.Count; i++)
                {
                    PlayerControl player = list[i];
                    int num = i % 3;
                    int num2 = i / 3;
                    ShapeshifterPanel shapeshifterPanel = GameObject.Instantiate<ShapeshifterPanel>(ShapMinigame.PanelPrefab, ShapMinigame.transform);
                    shapeshifterPanel.transform.localPosition = new Vector3(ShapMinigame.XStart + (float)num * ShapMinigame.XOffset, ShapMinigame.YStart + (float)num2 * ShapMinigame.YOffset, -1f);
                    shapeshifterPanel.SetPlayer(i, player.Data, new Action(delegate
                    {
                        SetCooldown(Cooldown);
                        ShapMinigame.ForceClose();
                        if (Novisor != null)
                        {
                            CustomRpcManager.Instance<RpcHaunt>().Send((PlayerControl.LocalPlayer, player), PlayerControl.LocalPlayer);
                        }
                    }));
                    shapeshifterPanel.NameText.color = Color.white;
                    ShapMinigame.potentialVictims.Add(shapeshifterPanel);
                }
            }
        }
    }
}
