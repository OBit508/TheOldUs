using FungleAPI.Components;
using FungleAPI.Role;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Roles.Impostors;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Components
{
    [FungleAPI.Attributes.RegisterTypeInIl2Cpp]
    public class FakeNovisorComp : MonoBehaviour
    {
        public Vector3 Direction;
        public GifAnimator Animator;
        public void Start()
        {
            Animator = GetComponent<GifAnimator>();
            Animator.spriteRenderer = GetComponent<SpriteRenderer>();
            Animator.Gif = TOUAssets.NovisorRun;
            Animator.Play();
            transform.localScale = new Vector3(Direction.x < 0 ? -0.9f : 0.9f, 0.9f, 0.9f);
            GameObject.Destroy(gameObject, NovisorRole.CloneLifetime);
        }
        public void Update()
        {
            transform.position += Direction * (NovisorRole.CloneSpeed * 0.016f);
            if (MeetingHud.Instance != null)
            {
                GameObject.Destroy(gameObject);
            }
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (MeetingHud.Instance == null)
            {
                PlayerControl component = other.GetComponent<PlayerControl>();
                if (component != null && component.Data != null && !component.Data.IsDead && component.Data.Role != null && component.Data.Role.GetTeam() != FungleAPI.Role.Teams.ModdedTeam.Impostors && (AmongUsClient.Instance.NetworkMode == NetworkModes.OnlineGame && component.AmOwner || AmongUsClient.Instance.NetworkMode == NetworkModes.FreePlay))
                {
                    component.RpcCustomMurderPlayer(component, MurderResultFlags.Succeeded, false, true, false, false);
                }
            }
        }
    }
}
