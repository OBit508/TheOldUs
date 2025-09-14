using FungleAPI.Components;
using FungleAPI.Networking;
using FungleAPI.Role;
using FungleAPI.Utilities;
using FungleAPI.Utilities.Assets;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.Components
{
    public class TOUPlayerComponent : PlayerComponent
    {
        public static Material OriginalPlayerMaterial = AmongUsClient.Instance.PlayerPrefab.cosmetics.bodySprites[0].BodySprite.material;
        public static Material InvisibleMaterial = new Material(Shader.Find("Unlit/Transparent"));
        public SpriteRenderer Animator;
        public GifFile RunAnim;
        public GifFile IdleAnim;
        public bool Transformed;
        public bool Invisible;
        private float time;
        public void Awake()
        {
            Animator = new GameObject("Animator").AddComponent<SpriteRenderer>();
            Animator.transform.SetParent(transform);
            Animator.gameObject.AddComponent<Updater>().lateUpdate = new Action(delegate
            {
                Vector3 vec = Vector3.one;
                if (player.cosmetics.FlipX)
                {
                    vec.x *= -1;
                }
                Animator.transform.localScale = vec;
            });
        }
        public void Update()
        {
            time += Time.deltaTime;
            Animator.enabled = Transformed;
            if (player.cosmetics.currentBodySprite != null)
            {
                player.cosmetics.currentBodySprite.BodySprite.material = Transformed ? InvisibleMaterial : OriginalPlayerMaterial;
            }
            if (Transformed)
            {
                player.cosmetics.nameText?.gameObject.SetActive(false);
                player.cosmetics.hat?.gameObject.SetActive(false);
                player.cosmetics.CurrentPet?.gameObject.SetActive(false);
                player.cosmetics.skin?.gameObject.SetActive(false);
                player.cosmetics.visor?.gameObject.SetActive(false);
                if (RunAnim != null && player.MyPhysics.Animations.IsPlayingRunAnimation())
                {
                    Animator.sprite = RunAnim.GetSprite(time);
                }
                else if (IdleAnim != null)
                {
                    Animator.sprite = IdleAnim.GetSprite(time);
                }
            }
            if (!player.Data.IsDead)
            {
                Color color = player.Visible ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
                if (Invisible && player.Visible)
                {
                    if (!player.AmOwner)
                    {
                        color = new Color(1, 1, 1, 0);
                    }
                    if (player.Data.Role.GetTeam() == PlayerControl.LocalPlayer.Data.Role.GetTeam() || player.Data.IsDead)
                    {
                        color = new Color(1, 1, 1, 0.4f);
                    }
                }
                player.cosmetics.hat.FrontLayer.color = color;
                player.cosmetics.hat.BackLayer.color = color;
                player.cosmetics.skin.layer.color = color;
                player.cosmetics.visor.Image.color = color;
                if (player.cosmetics.CurrentPet != null)
                {
                    player.cosmetics.CurrentPet.renderers.ToArray().ToList().ForEach(new Action<SpriteRenderer>(delegate (SpriteRenderer rend)
                    {
                        rend.color = color;
                    }));
                }
                player.cosmetics.currentBodySprite.BodySprite.color = color;
            }
        }
    }
}
