using FungleAPI.Components;
using FungleAPI.Networking;
using FungleAPI.Utilities.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.RPCs;
using TheOldUs.TOU;
using UnityEngine;

namespace TheOldUs.Components
{
    [FungleAPI.Attributes.RegisterTypeInIl2Cpp]
    public class BetterDoorHelper : SystemConsole
    {
        public AutoOpenDoor Door;
        public bool Open = true;
        public float timer;
        public void Awake()
        {
            Door = transform.parent.GetComponent<AutoOpenDoor>();
            Image = Door.GetComponent<SpriteRenderer>();
            Image.material = new Material(Shader.Find("Sprites/Outline"));
        }
        public new void Start()
        {
            SetDoorway();
        }
        public void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
        public void SetDoorway()
        {
            timer = 3;
            Open = !Open;
            Door.myCollider.isTrigger = Open;
            if (Door.shadowCollider != null)
            {
                Door.shadowCollider.enabled = !Open;
            }
            Door.animator.Play(Open ? Door.OpenDoorAnim : Door.CloseDoorAnim, 1f);
            Door.StopAllCoroutines();
            if (!Open)
            {
                Vector2 vector = Door.myCollider.size;
                Door.StartCoroutine(Door.CoCloseDoorway(vector.x > vector.y));
                if (Constants.ShouldPlaySfx())
                {
                    SoundManagerHelper.PlayDynamicSound(Door.name, Door.CloseSound, false, new FungleAPI.Utilities.Sound.DynamicSound.GetDynamicsFunction(Door.DoorDynamics), SoundManager.Instance.SfxChannel);
                }
                VibrationManager.Vibrate(2.5f, Door.transform.position, 3f, 0f, VibrationManager.VibrationFalloff.None, Door.CloseSound, false);
                return;
            }
            if (Constants.ShouldPlaySfx())
            {
                SoundManagerHelper.PlayDynamicSound(Door.name, Door.OpenSound, false, new FungleAPI.Utilities.Sound.DynamicSound.GetDynamicsFunction(Door.DoorDynamics), SoundManager.Instance.SfxChannel);
            }
            VibrationManager.Vibrate(2.5f, Door.transform.position, 3f, 0f, VibrationManager.VibrationFalloff.None, Door.OpenSound, false);
        }
        public override float CanUse(NetworkedPlayerInfo pc, out bool canUse, out bool couldUse)
        {
            PlayerControl @object = pc.Object;
            Vector3 center = @object.Collider.bounds.center;
            Vector3 position = transform.position;
            float num = Vector2.Distance(center, position);
            couldUse = TOUSettings.Ship.BetterDoors && !pc.IsDead && Door.Open;
            canUse = (num <= UsableDistance && !PhysicsHelpers.AnythingBetween(@object.Collider, center, position, Constants.ShipOnlyMask, false)) & couldUse;
            return num;
        }

        public override void Use()
        {
            CanUse(PlayerControl.LocalPlayer.Data, out var canUse, out var _);
            if (canUse)
            {
                CustomRpcManager.Instance<RpcUpdateDoor>().Send(this, ShipStatus.Instance);
            }
        }

        public override void SetOutline(bool on, bool mainTarget)
        {
            base.Image.material.SetFloat("_Outline", on ? 1 : 0);
            base.Image.material.SetColor("_OutlineColor", Color.white);
            if (mainTarget)
            {
                float r = Mathf.Clamp01(Color.white.r * 0.5f);
                float g = Mathf.Clamp01(Color.white.g * 0.5f);
                float b = Mathf.Clamp01(Color.white.b * 0.5f);
                base.Image.material.SetColor("_AddColor", new Color(r, g, b, 1f));
            }
            else
            {
                base.Image.material.SetColor("_AddColor", new Color(0f, 0f, 0f, 0f));
            }
        }
    }
}
