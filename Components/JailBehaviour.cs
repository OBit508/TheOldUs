using FungleAPI.Attributes;
using FungleAPI.Components;
using FungleAPI.Networking;
using FungleAPI.PluginLoading;
using FungleAPI.Utilities;
using FungleAPI.Utilities.Prefabs;
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
    [RegisterTypeInIl2Cpp]
    public class JailBehaviour : MonoBehaviour
    {
        public static SystemConsole Prefab;
        public static SpriteRenderer Bars;
        public static List<PlayerControl> ArrestedPlayers = new List<PlayerControl>();
        public void Update()
        {
            ArrestedPlayers.RemoveAll(p => p == null || p.Data.IsDead || p.Data.Disconnected);
            foreach (PlayerControl player in ArrestedPlayers)
            {
                if (Vector2.Distance(player.transform.position, transform.position) > 3.2f)
                {
                    player.NetTransform.SnapTo(new Vector2(TouSettings.Ship.InvertX ? 12 : -12, TouSettings.Ship.InvertY ? -3 : 3));
                }
            }
        }
        public void Start()
        {
            ArrestedPlayers.Clear();
            if (Prefab == null)
            {
                Prefab = PrefabUtils.SkeldPrefab.GetComponentsInChildren<SystemConsole>().FirstOrDefault(s => s.name == "SurvConsole");
            }
            gameObject.layer = 9;
            GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Unlit/MaskShader"));
            Bars = transform.GetChild(0).GetComponent<SpriteRenderer>();
            Bars.material = new Material(Shader.Find("Sprites/Outline"));
            Bars.gameObject.layer = 9;
            ModdedConsole surv = Helpers.CreateConsole(2, new Predicate<PlayerControl>(p => ArrestedPlayers.Contains(p) || p.Data.IsDead), new Action(delegate
            {
                SurveillanceMinigame SurvMinigame = GameObject.Instantiate<Minigame>(Prefab.MinigamePrefab, Camera.main.transform).SafeCast<SurveillanceMinigame>();
                if (SurvMinigame != null)
                {
                    Minigame.Instance = SurvMinigame;
                    SurvMinigame.MyTask = null;
                    SurvMinigame.MyNormTask = null;
                    SurvMinigame.timeOpened = Time.realtimeSinceStartup;
                    if (PlayerControl.LocalPlayer)
                    {
                        if (MapBehaviour.Instance)
                        {
                            MapBehaviour.Instance.Close();
                        }
                        PlayerControl.LocalPlayer.MyPhysics.SetNormalizedVelocity(Vector2.zero);
                    }
                    SurvMinigame.logger.Info("Opening minigame " + SurvMinigame.GetType().Name, null);
                    SurvMinigame.StartCoroutine(SurvMinigame.CoAnimateOpen());
                    DestroyableSingleton<DebugAnalytics>.Instance.Analytics.MinigameOpened(PlayerControl.LocalPlayer.Data, SurvMinigame.TaskType);
                    DestroyableSingleton<HudManager>.Instance.PlayerCam.Locked = true;
                    SurvMinigame.FilteredRooms = Enumerable.ToArray<PlainShipRoom>(Enumerable.Where<PlainShipRoom>(ShipStatus.Instance.AllRooms, (PlainShipRoom i) => i.survCamera));
                    SurvMinigame.textures = new RenderTexture[1];
                    Camera camera = GameObject.Instantiate<Camera>(SurvMinigame.CameraPrefab);
                    camera.transform.SetParent(SurvMinigame.transform);
                    float timer = 5;
                    int index = 0;
                    PlayerControl currentPlayer = PlayerControl.AllPlayerControls[0];
                    camera.gameObject.AddComponent<Updater>().lateUpdate = new Action(delegate
                    {
                        camera.transform.position = currentPlayer.transform.position;
                        timer -= Time.deltaTime;
                        if (currentPlayer == null)
                        {
                            currentPlayer = PlayerControl.AllPlayerControls[0];
                            timer = 5;
                        }
                        else if (timer <= 0)
                        {
                            if (index + 1 == PlayerControl.AllPlayerControls.Count)
                            {
                                index = 0;
                            }
                            else
                            {
                                index++;
                            }
                            timer = 5;
                            currentPlayer = PlayerControl.AllPlayerControls[index];
                        }
                    });
                    camera.orthographicSize = 3;
                    RenderTexture temporary = RenderTexture.GetTemporary(256, 256, 16, RenderTextureFormat.ARGB32);
                    SurvMinigame.textures[0] = temporary;
                    camera.targetTexture = temporary;
                    foreach (MeshRenderer rend in SurvMinigame.ViewPorts)
                    {
                        rend.gameObject.SetActive(false);
                    }
                    MeshRenderer render = SurvMinigame.ViewPorts[0];
                    render.material.SetTexture("_MainTex", temporary);
                    render.transform.localScale = new Vector3(5.3f, 5.1f, 5.3f);
                    render.transform.localPosition = new Vector3(0, 0.015f, -5);
                    render.transform.gameObject.SetActive(true);
                    if (!PlayerControl.LocalPlayer.Data.IsDead)
                    {
                        ShipStatus.Instance.RpcUpdateSystem(SystemTypes.Security, 1);
                    }
                    SurvMinigame.SetupInput(true, false);
                }
            }), Prefab.Image.sprite);
            surv.transform.SetParent(transform);
            surv.OutlineColor = Color.white;
            surv.useIcon = ImageNames.CamsButton;
            surv.transform.localPosition = new Vector3(0, 1.57f, -0.1f);
        }
    }
}
