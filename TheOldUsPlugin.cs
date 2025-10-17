using BepInEx;
using BepInEx.Unity.IL2CPP;
using FungleAPI;
using FungleAPI.PluginLoading;
using FungleAPI.Utilities;
using HarmonyLib;
using System;
using System.Diagnostics;
using TheOldUs.Components;
using TheOldUs.Roles;
using TheOldUs.TOU;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheOldUs
{
    [BepInProcess("Among Us.exe")]
    [BepInPlugin(ModId, ModName, ModVersion)]
    [BepInDependency(FungleAPIPlugin.ModId)]
    public class TheOldUsPlugin : BasePlugin
    {
        public const string ModName = "TheOldUs";
        public const string Owner = "rafael";
        public const string ModDescription = "This is a ModTemplate for the API FungleAPI";
        public const string ModId = "com." + Owner + "." + ModName;
        public const string ModVersion = "1.0.0";
        public Harmony Harmony { get; } = new Harmony(ModId);
        public static ModPlugin Plugin;
        public override void Load()
        {
            SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>)delegate (Scene scene, LoadSceneMode _)
            {
                if (scene.name == "MainMenu" && ControllerHelper.myController == null)
                {
                    new GameObject("ControllerHelper").AddComponent<ControllerHelper>().DontDestroy();
                }
            });
            Plugin = ModPluginManager.RegisterMod(this, ModVersion, new Action(TOUAssets.LoadAssets), ModName);
            Plugin.UseShipReference = true;
            Harmony.PatchAll();
        }
    }
}
