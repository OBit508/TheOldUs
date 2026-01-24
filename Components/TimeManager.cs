using FungleAPI.Networking;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Buttons;
using TheOldUs.Roles.Crewmates;
using TheOldUs.RPCs;
using TheOldUs.Utilities;
using UnityEngine;

namespace TheOldUs.Components
{
    [FungleAPI.Attributes.RegisterTypeInIl2Cpp]
    internal class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance;
        public List<TimePoint> TimePoints = new List<TimePoint>();
        public TimeManipulationType TimeManipulation;
        public SpriteRenderer Background;
        public int MaxPoints => (int)(530f / 10f * TimeMasterRole.RewindDuration);
        public void Update()
        {
            if (Background == null)
            {
                HudManager hudManager = HudManager.Instance;
                if (hudManager != null)
                {
                    Background = GameObject.Instantiate(hudManager.FullScreen, hudManager.FullScreen.transform.position, hudManager.FullScreen.transform.rotation, hudManager.FullScreen.transform.parent);
                    Background.gameObject.SetActive(true);
                }
            }
            if (TimeManipulation == TimeManipulationType.None)
            {
                TimePoint point = new TimePoint();
                foreach (PlayerControl playerControl in PlayerControl.AllPlayerControls)
                {
                    point.Points.Add(new PlayerPoint()
                    {
                        Position = playerControl.transform.position,
                        Velocity = playerControl.rigidbody2D.velocity,
                        IsDead = playerControl.Data.IsDead,
                        Owner = playerControl
                    });
                }
                TimePoints.Add(point);
                if (TimePoints.Count > MaxPoints)
                {
                    TimePoints.RemoveAt(0);
                }
            }
            else
            {
                if (TimePoints.Count <= 0)
                {
                    TimeManipulation = TimeManipulationType.None;
                    return;
                }
                TimePoint timePoint = TimePoints[TimePoints.Count - 1];
                foreach (PlayerPoint playerPoint in timePoint.Points)
                {
                    if (playerPoint.Owner != null)
                    {
                        playerPoint.Owner.transform.position = playerPoint.Position;
                        playerPoint.Owner.rigidbody2D.velocity = playerPoint.Velocity;
                        if (!playerPoint.IsDead && playerPoint.Owner.Data.IsDead && AmongUsClient.Instance.AmHost)
                        {
                            DeadBody deadBody = Helpers.GetBodyById(playerPoint.Owner.PlayerId);
                            if (deadBody != null)
                            {
                                Rpc<RpcRevive>.Instance.Send(deadBody, PlayerControl.LocalPlayer);
                            }
                        }
                    }
                }
                TimePoints.Remove(timePoint);
            }
            Background.color = TimeManipulation == TimeManipulationType.None ? Color.clear : new Color32(0, 124, 228, (byte)byte.MaxValue / 2);
        }
        public class TimePoint
        {
            public List<PlayerPoint> Points = new List<PlayerPoint>();
        }
        public class PlayerPoint
        {
            public Vector3 Position;
            public Vector3 Velocity;
            public bool IsDead;
            public PlayerControl Owner;
        }
    }
}
