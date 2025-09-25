using FungleAPI;
using FungleAPI.Networking;
using FungleAPI.Role;
using FungleAPI.Utilities;
using Rewired.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Assets;
using TheOldUs.Components;
using TheOldUs.Roles.Sheriff;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace TheOldUs.Roles.Jailer
{
    internal class JailerArrest : CustomAbilityButton
    {
        public PlayerControl Target;
        public override bool CanUse => Target != null;
        public override bool CanClick => CanUse;
        public override float Cooldown => JailerRole.ArrestCooldown;
        public override string OverrideText => "Arrest";
        public override bool HaveUses => JailerRole.ArrestUses > 0;
        public override int NumUses => (int)JailerRole.ArrestUses;
        public override Color32 TextOutlineColor { get; } = Color.blue;
        public override Sprite ButtonSprite => ButtonSprites.JailerArrest;
        public override void Update()
        {
            base.Update();
            PlayerControl newTarget = PlayerControl.LocalPlayer.Data.Role.FindClosestTarget(new Predicate<PlayerControl>(player => PlayerJail.Jails.ContainsKey(player)));
            if (newTarget != Target)
            {
                if (Target != null && !Target.IsNullOrDestroyed())
                {
                    Target?.cosmetics.SetOutline(false, new Il2CppSystem.Nullable<Color>(TextOutlineColor));
                }
                newTarget?.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(TextOutlineColor));
                Target = newTarget;
            }
        }
        public override void Click()
        {
            try
            {
                if (Target != null)
                {
                    CustomRpcManager.Instance<ArrestRpc>().Send((Target, true), PlayerControl.LocalPlayer.NetId);
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
