using FungleAPI;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Role;
using FungleAPI.Utilities;
using Rewired.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.Crewmates;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace TheOldUs.Buttons
{
    internal class ReleasePlayerButton : CustomAbilityButton
    {
        public static ShapeshifterMinigame ShapPanelPrefab = RoleManager.Instance.AllRoles.ToSystemList().FirstOrDefault(role => role.Role == AmongUs.GameOptions.RoleTypes.Shapeshifter).SafeCast<ShapeshifterRole>().ShapeshifterMenu;
        public static ShapeshifterMinigame ShapMinigame;
        public override bool CanUse => Vector2.Distance(PlayerControl.LocalPlayer.transform.position, JailBehaviour.Bars.transform.position) <= 2 && ShapMinigame == null;
        public override bool CanClick => CanUse;
        public override float Cooldown => JailerRole.ReleaseCooldown;
        public override string OverrideText => "Release";
        public override Color32 TextOutlineColor { get; } = Color.blue;
        public override Sprite ButtonSprite => TOUAssets.JailerRelease;
        public override void Update()
        {
            base.Update();
            if (JailBehaviour.Bars != null)
            {
                JailBehaviour.Bars.material.SetFloat("_Outline", CanUse ? 1 : 0);
                JailBehaviour.Bars.material.SetColor("_OutlineColor", TextOutlineColor);
            }
        }
        public override void Click()
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
            ShapMinigame.potentialVictims = new Il2CppSystem.Collections.Generic.List<ShapeshifterPanel>();
            for (int i = 0; i < JailBehaviour.ArrestedPlayers.Count; i++)
            {
                PlayerControl player = JailBehaviour.ArrestedPlayers[i];
                int num = i % 3;
                int num2 = i / 3;
                bool flag = PlayerControl.LocalPlayer.Data.Role.NameColor == player.Data.Role.NameColor;
                ShapeshifterPanel shapeshifterPanel = GameObject.Instantiate<ShapeshifterPanel>(ShapMinigame.PanelPrefab, ShapMinigame.transform);
                shapeshifterPanel.transform.localPosition = new Vector3(ShapMinigame.XStart + (float)num * ShapMinigame.XOffset, ShapMinigame.YStart + (float)num2 * ShapMinigame.YOffset, -1f);
                shapeshifterPanel.SetPlayer(i, player.Data, new Action(delegate
                {
                    ShapMinigame.ForceClose();
                    CustomRpcManager.Instance<RpcArrestPlayer>().Send((player, false), PlayerControl.LocalPlayer.NetId);
                    SetCooldown(Cooldown);
                }));
                shapeshifterPanel.NameText.color = (flag ? player.Data.Role.NameColor : Color.white);
                ShapMinigame.potentialVictims.Add(shapeshifterPanel);
            }
        }
    }
}
