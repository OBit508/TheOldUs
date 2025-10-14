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
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace TheOldUs.Buttons
{
    internal class ReleasePlayerButton : CustomAbilityButton
    {
        public PlayerControl Target;
        public override bool CanUse => Target != null;
        public override bool CanClick => CanUse;
        public override float Cooldown => JailerRole.ReleaseCooldown;
        public override string OverrideText => "Release";
        public override bool HaveUses => JailerRole.ReleaseUses > 0;
        public override int NumUses => JailerRole.ReleaseUses;
        public override Color32 TextOutlineColor { get; } = Color.blue;
        public override Sprite ButtonSprite => TOUAssets.JailerRelease;
        public override void Update()
        {
            base.Update();
            PlayerControl newTarget = PlayerControl.LocalPlayer.Data.Role.FindClosestTarget(new Predicate<PlayerControl>(player => !PlayerJail.Jails.ContainsKey(player)));
            if (newTarget != Target)
            {
                if (Target != null && !Target.IsNullOrDestroyed())
                {
                    Target?.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(Color.green));
                }
                newTarget?.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(Color.green));
                Target = newTarget;
            }
        }
        public override void Click()
        {
            try
            {
                if (Target != null)
                {
                    CustomRpcManager.Instance<RpcArrestPlayer>().Send((Target, false), PlayerControl.LocalPlayer.NetId);
                    Target = null;
                }
            }
            catch (Exception ex)
            {
                FungleAPIPlugin.Instance.Log.LogError(ex);
            }
        }
    }
}
