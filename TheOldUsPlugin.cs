using BepInEx;
using BepInEx.Unity.IL2CPP;
using FungleAPI;
using HarmonyLib;
using System;
using System.Diagnostics;
using TheOldUs.Assets;
using TheOldUs.Roles;

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
            Plugin = ModPlugin.RegisterMod(this, ModVersion, new Action(delegate
            {
                ButtonSprites.LoadButtonSprites();
                ScreenshotSprites.LoadScreenshotSprites();
                GifAnimations.LoadAnimations();
                Prefabs.LoadPrefabs();
            }), ModName);
            Harmony.PatchAll();
        }
    }
}
