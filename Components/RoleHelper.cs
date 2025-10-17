using AmongUs.GameOptions;
using FungleAPI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Crewmates;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Components
{
    public class RoleHelper : PlayerComponent
    {
        public SpriteRenderer Gun;
        public bool ShowingGun;
        public void Update()
        {
            if (player != null)
            {
                if (Gun == null)
                {
                    Gun = new GameObject("Gun").AddComponent<SpriteRenderer>();
                    Gun.sprite = TOUAssets.Gun;
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
            }
        }
    }
}
