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
    public class PlayerAnimationHelper : PlayerComponent
    {
        public static Material InvisibleMaterial = new Material(Shader.Find("Unlit/Transparent"));
        public Material OriginalPlayerMaterial = AmongUsClient.Instance.PlayerPrefab.cosmetics.bodySprites[0].BodySprite.material;
        public SpriteRenderer Animator;
        public GifFile RunAnim;
        public GifFile IdleAnim;
        public bool Transformed;
        public bool Invisible;
        private float time;
        public void Start()
        {
            Animator = new GameObject("Animator").AddComponent<SpriteRenderer>();
            Animator.transform.SetParent(transform);
            Animator.transform.localPosition = new Vector3(0, 0, -1);
            Animator.gameObject.AddComponent<Updater>().lateUpdate = new Action(delegate
            {
                Vector3 vec = Vector3.one * 1.3f;
                if (player.cosmetics.FlipX)
                {
                    vec.x *= -1;
                }
                Animator.transform.localScale = vec;
                Vector3 vec2 = Vector3.zero;
                vec2.z = player.transform.position.z - 0.0000000000000000000000000001f;
                Animator.transform.localPosition = vec2;
            });
        }
        public void Update()
        {
            Animator.enabled = Transformed;
            player.cosmetics.currentBodySprite.BodySprite.transform.localScale = Transformed ? Vector3.zero : Vector3.one * 0.5f;
            time += Time.deltaTime;
            player.cosmetics.nameText?.gameObject.SetActive(!Transformed);
            player.cosmetics.hat?.gameObject.SetActive(!Transformed);
            player.cosmetics.CurrentPet?.gameObject.SetActive(!Transformed);
            player.cosmetics.skin?.gameObject.SetActive(!Transformed);
            player.cosmetics.visor?.gameObject.SetActive(!Transformed);
            if (Transformed)
            {
                if (RunAnim != null && player.MyPhysics.Animations.IsPlayingRunAnimation())
                {
                    Animator.sprite = RunAnim.GetSprite(time);
                }
                else if (IdleAnim != null)
                {
                    Animator.sprite = IdleAnim.GetSprite(time);
                }
            }
            player.cosmetics.nameText.color = Color.clear;
            if (player.Visible)
            {
                Color hatColor = GetCosmeticColor(CosmeticType.hat);
                player.cosmetics.hat.FrontLayer.color = hatColor;
                player.cosmetics.hat.BackLayer.color = hatColor;
                if (!player.Data.IsDead || PlayerControl.LocalPlayer.Data.IsDead)
                {
                    player.cosmetics.nameText.color = hatColor;
                }
                player.cosmetics.skin.layer.color = GetCosmeticColor(CosmeticType.skin);
                player.cosmetics.visor.Image.color = GetCosmeticColor(CosmeticType.visor);
                if (player.cosmetics.CurrentPet != null)
                {
                    player.cosmetics.CurrentPet.renderers.ToArray().ToList().ForEach(new Action<SpriteRenderer>(delegate (SpriteRenderer rend)
                    {
                        rend.color = GetCosmeticColor(CosmeticType.pet);
                    }));
                }
                Animator.color = GetCosmeticColor(CosmeticType.body);
                player.cosmetics.currentBodySprite.BodySprite.color = Animator.color;
            }
        }
        public Color GetCosmeticColor(CosmeticType type)
        {
            switch (type)
            {
                case CosmeticType.body: return Invisible && !player.Data.IsDead ? new Color(1, 1, 1, 0.5f) : Color.white;
                case CosmeticType.hat: return Invisible || player.Data.IsDead ? new Color(1, 1, 1, 0.5f) : Color.white;
                case CosmeticType.pet: return Invisible && !player.Data.IsDead ? new Color(1, 1, 1, 0.5f) : Color.white;
                case CosmeticType.skin: return player.Data.IsDead ? Color.clear : (Invisible ? new Color(1, 1, 1, 0.5f) : Color.white);
                default: return Invisible || player.Data.IsDead ? new Color(1, 1, 1, 0.5f) : Color.white;
            }
        }
        public enum CosmeticType
        {
            body,
            hat,
            pet,
            skin,
            visor
        }
    }
}
