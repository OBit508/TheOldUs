using AmongUs.Data;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using FungleAPI.Components;
using FungleAPI.Player;
using FungleAPI.Role;
using FungleAPI.Utilities;
using Sentry.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Patches;
using TheOldUs.Roles.Impostors;
using TheOldUs.Utilities;
using UnityEngine;

namespace TheOldUs.Components
{
    [FungleAPI.Attributes.RegisterTypeInIl2Cpp]
    public class AcidTsunami : MonoBehaviour
    {
        public static AcidTsunami Instance;
        public BoxCollider2D Collider;
        public ImportantTextTask TsunamiText;
        public Vector3 Target = new Vector3(29, -4.6f, -20);
        public void Start()
        {
            Instance = this;
            Collider = GetComponent<BoxCollider2D>();
            StartCoroutine(CoReactorFlash().WrapToIl2Cpp());
        }
        public void Update()
        {
            if (TsunamiText == null)
            {
                TsunamiText = new GameObject("Text").AddComponent<ImportantTextTask>();
                TsunamiText.Text = Color.red.ToTextColor() + "The Acid is comming hide!!!!!";
                TsunamiText.transform.SetParent(transform);
                PlayerControl.LocalPlayer.myTasks.Add(TsunamiText);
            }
            HudManager hud = HudManager.Instance;
            hud.StartCoroutine(hud.PlayerCam.CoShakeScreen(0.1f, 2));
            transform.position = Vector3.MoveTowards(transform.position, Target, Time.deltaTime * AcidMaster.TsunamiSpeed);
            if (Vector2.Distance(transform.position, Target) <= 0.5f)
            {
                foreach (Vent vent in ShipStatusPatch.AcidVents)
                {
                    foreach (PlayerControl player in vent.TryGetHelper().Players)
                    {
                        player.MyPhysics.BootFromVent(vent.Id);
                    }
                }
                HudManager.Instance.FullScreen.gameObject.SetActive(false);
                PlayerControl.LocalPlayer.myTasks.Remove(TsunamiText);
                GameObject.Destroy(gameObject);
            }
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (MeetingHud.Instance == null)
            {
                PlayerControl component = other.GetComponent<PlayerControl>();
                if (component != null && component.Data != null && !component.Data.IsDead && !component.inVent && component.Data.Role != null && component.Data.Role.GetTeam() != FungleAPI.Teams.ModdedTeam.Impostors && (AmongUsClient.Instance.NetworkMode == NetworkModes.OnlineGame && component.AmOwner || AmongUsClient.Instance.NetworkMode == NetworkModes.FreePlay))
                {
                    component.RpcDie(DeadBodyType.Viper, AcidMaster.DissolveDelay);
                }
            }
            foreach (DeadBody body in Helpers.AllDeadBodies)
            {
                if (body.SafeCast<ViperDeadBody>() == null && Collider.OverlapPoint(body.transform.position) && body.myCollider.enabled)
                {
                    body.myCollider.enabled = false;
                    foreach (SpriteRenderer rend in body.bodyRenderers)
                    {
                        rend.gameObject.SetActive(false);
                    }
                }
            }
        }
        public System.Collections.IEnumerator CoReactorFlash()
        {
            HudManager hud = HudManager.Instance;
            WaitForSeconds wait = new WaitForSeconds(1f);
            bool light = false;
            hud.FullScreen.color = new Color(1f, 0f, 0f, 0.37254903f);
            while (true)
            {
                hud.FullScreen.gameObject.SetActive(!hud.FullScreen.gameObject.activeSelf);
                SoundManager.Instance.PlaySound(ShipStatus.Instance.SabotageSound, false, 1f, null);
                if (hud.lightFlashHandle == null)
                {
                    hud.lightFlashHandle = DestroyableSingleton<DualshockLightManager>.Instance.AllocateLight();
                    hud.lightFlashHandle.color = new Color(1f, 0f, 0f, 1f);
                    hud.lightFlashHandle.intensity = 1f;
                }
                light = !light;
                Color color = hud.lightFlashHandle.color;
                color.a = (light ? 1f : 0f);
                hud.lightFlashHandle.color = color;
                yield return wait;
            }
        }
    }
}
