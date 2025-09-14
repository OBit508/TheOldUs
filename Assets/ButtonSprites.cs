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
        public static void LoadButtonSprites()
        {
            SheriffKill = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Assets.Sprites.Buttons.SheriffKill", 130);
        }
        public static Sprite SheriffKill;
    }
}
