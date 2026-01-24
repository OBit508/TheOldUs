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
        public override bool CanUse => TimeManager.Instance != null && TimeManager.Instance.TimeManipulation == TimeManipulationType.None && Percent >= 100;
        public override bool CanClick => CanUse;
        public override float Cooldown => TimeMasterRole.RewindCooldown;
        public override string OverrideText => "Rewind";
        public override Color32 TextOutlineColor { get; } = new Color32(0, 124, 228, byte.MaxValue);
        public override Sprite ButtonSprite => TouAssets.Rewind;
        public override void Click()
        {
            if (TimeManager.Instance != null)
            {
                TimeManager.Instance.TimeManipulation = TimeManipulationType.TimeMaster;
            }
        }
        public override void Update()
        {
            base.Update();
            Percent = 100 - ((TimeManager.Instance.MaxPoints - TimeManager.Instance.TimePoints.Count) * 100 / TimeManager.Instance.MaxPoints);
            Button.usesRemainingText.text = Percent.ToString() + "%";
        }
        public override AbilityButton CreateButton()
        {
            if (Button != null)
            {
                Destroy();
            }

            Reset(creating: true);
            Button = UnityEngine.Object.Instantiate(DestroyableSingleton<HudManager>.Instance.AbilityButton, (Location == ButtonLocation.BottomRight) ? HudHelper.BottomRight : HudHelper.BottomLeft);
            Button.name = GetType().Name;
            PassiveButton component = Button.GetComponent<PassiveButton>();
            Button.graphic.sprite = ButtonSprite;
            Button.graphic.color = new Color(1f, 1f, 1f, 1f);
            Button.gameObject.SetActive(value: true);
            Button.OverrideText(OverrideText);
            Button.buttonLabelText.SetOutlineColor(TextOutlineColor);
            Button.GetComponent<PassiveButton>().SetNewAction(delegate
            {
                if (CanClick && CanUse)
                {
                    if (Timer <= 0f)
                    {
                        Timer = Cooldown;
                        if (HaveUses)
                        {
                            CurrentNumUses--;
                            if (Button != null)
                            {
                                Button.SetUsesRemaining(CurrentNumUses);
                            }
                        }
                        Click();
                        if (TransformButton)
                        {
                            Transformed = true;
                        }
                    }
                }
            });
            Button.SetCoolDown(Timer, Cooldown);
            Button.usesRemainingSprite.gameObject.SetActive(true);
            return Button;
        }
    }
}
