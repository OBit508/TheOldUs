using AmongUs.GameOptions;
using FungleAPI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Crewmates;
using TheOldUs.Roles.Neutrals;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Components
{
    public class RoleHelper : PlayerComponent
    {
        public SpriteRenderer Gun;
        public bool ShowingGun;
        public bool Soaked;
        public Color DefaultVisorColor = new Color(0.5843f, 0.7922f, 0.8627f, 1);
        public PlayerControl Target;
        public void Update()
        {
            if (player != null)
            {
                if (Gun == null)
                {
                    Gun = new GameObject("Gun").AddComponent<SpriteRenderer>();
                    Gun.sprite = TouAssets.Gun;
                    Gun.transform.SetParent(player.transform);
                    Gun.transform.localPosition = new Vector3(0, -0.1f, -0.1f);
                }
                else
                {
                    Gun.gameObject.SetActive(ShowingGun);
                    if (Gun.gameObject.active)
                    {
                        Vector3 vec = Vector3.one * 0.7f;
                        if (player.cosmetics.FlipX)
                        {
                            vec.x *= -1;
                        }
                        Gun.transform.localScale = vec;
                    }
                }
                try
                {
                    player.cosmetics.currentBodySprite.BodySprite.material.SetColor("_VisorColor", Soaked ? Palette.Orange : DefaultVisorColor);
                }
                catch { }
                if (Target != null)
                {

                }
            }
        }
    }
}
