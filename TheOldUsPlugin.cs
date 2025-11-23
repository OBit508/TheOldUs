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
        public const string ModDescription = "This mod is inspired by older Among Us game mods";
        public const string ModId = "com." + Owner + "." + ModName;
        public const string ModVersion = "0.0.1";
        public static Harmony Harmony = new Harmony(ModId);
        public static ModPlugin Plugin;
        public override void Load()
        {
            Plugin = ModPluginManager.RegisterMod(this, ModVersion, new Action(TOUAssets.LoadAssets), ModName, "[TheOldUs v" + ModVersion + "] - Dev");
            Harmony.PatchAll();
            Utils.PatchFungleAPI();
        }
    }
}
