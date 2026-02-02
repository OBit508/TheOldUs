using FungleAPI.Base.Buttons;
using FungleAPI.Hud;
using FungleAPI.Networking;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using TheOldUs.Roles.Crewmates;
using TheOldUs.Roles.Impostors;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using TheOldUs.Utilities;
using UnityEngine;

namespace TheOldUs.Buttons
{
    internal class RewindButton : RoleButton<TimeMasterRole>
    {
        public int Percent;
        public override ButtonLocation Location => ButtonLocation.BottomLeft;
        public override bool CanUse() => base.CanUse() && CanCooldown && Percent >= 100;
        public override float Cooldown => TimeMasterRole.RewindCooldown;
        public override bool CanCooldown => TimeManager.Instance != null && TimeManager.Instance.TimeManipulation == TimeManipulationType.None;
        public override string OverrideText => TouTranslation.Rewind.GetString();
        public override Color32 TextOutlineColor { get; } = TouPalette.TimeMasterColor;
        public override Sprite ButtonSprite => TouAssets.Rewind;
        public override void OnClick()
        {
            if (TimeManager.Instance != null)
            {
                Rpc<RpcChangeTimeManipulation>.Instance.Send(TimeManipulationType.TimeMaster, PlayerControl.LocalPlayer);
            }
        }
        public override void ClickHandler()
        {
            if (!CanClick())
            {
                return;
            }
            OnClick();
            Timer = Cooldown;
        }
        public override void CreateButton()
        {
            if (Button)
            {
                return;
            }
            Reset(ResetType.Create);
            Button = GameObject.Instantiate(HudManager.Instance.AbilityButton, Location == ButtonLocation.BottomRight ? HudHelper.BottomRight : HudHelper.BottomLeft);
            Button.name = OverrideText;
            Button.graphic.sprite = ButtonSprite;
            Button.OverrideText(OverrideText);
            Button.buttonLabelText.SetOutlineColor(TextOutlineColor);
            Button.usesRemainingSprite.gameObject.SetActive(true);
            Button.GetComponent<PassiveButton>().SetNewAction(ClickHandler);
        }
        protected override void UpdateUI()
        {
            base.UpdateUI();
            Percent = 100 - ((TimeManager.Instance.MaxPoints - TimeManager.Instance.TimePoints.Count) * 100 / TimeManager.Instance.MaxPoints);
            Button.usesRemainingText.text = Percent.ToString() + "%";
        }
    }
}
