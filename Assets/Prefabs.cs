using FungleAPI.Utilities.Assets;
using FungleAPI.Utilities.Prefabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using UnityEngine;

namespace TheOldUs.Assets
{
    public static class Prefabs
    {
        public static void LoadPrefabs()
        {
            SpriteRenderer jail = new GameObject("Jail").AddComponent<SpriteRenderer>();
            jail.sprite = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Assets.Sprites.jail", 400);
            Jail = new Prefab<PlayerJail>(jail.gameObject.AddComponent<PlayerJail>());
        }
        public static Prefab<PlayerJail> Jail;
    }
}
