using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FungleAPI.Utilities.Assets;
using UnityEngine;

namespace TheOldUs.Assets
{
    public static class ButtonSprites
    {
        public static Sprite TemporaryButton;
        public static void LoadButtonSprites()
        {
            TemporaryButton = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Assets.Sprites.Buttons.funnyTemporaryButton", 100);
            SheriffKill = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Assets.Sprites.Buttons.SheriffKill", 130);
            JailerArrest = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Assets.Sprites.Buttons.JailerArrest", 300);
            JailerRelease = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Assets.Sprites.Buttons.JailerRelease", 250);
        }
        public static Sprite SheriffKill;
        public static Sprite JailerArrest;
        public static Sprite JailerRelease;
    }
}
