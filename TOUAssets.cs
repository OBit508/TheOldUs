using FungleAPI.Utilities.Assets;
using FungleAPI.Utilities.Prefabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using UnityEngine;

namespace TheOldUs
{
    public static class TOUAssets
    {
        public static Sprite TemporaryButton;
        public static void LoadAssets()
        {
            TemporaryButton = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.funnyTemporaryButton", 100);
            SheriffKill = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.SheriffKill", 130);
            JailerArrest = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.JailerArrest", 300);
            JailerRelease = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.JailerRelease", 250);
            SpriteRenderer jail = new GameObject("Jail").AddComponent<SpriteRenderer>();
            jail.sprite = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.jail", 400);
            Jail = new Prefab<PlayerJail>(jail.gameObject.AddComponent<PlayerJail>());
        }
        public static Sprite SheriffKill;
        public static Sprite JailerArrest;
        public static Sprite JailerRelease;
        public static Prefab<PlayerJail> Jail;
    }
}
