using FungleAPI.Utilities.Assets;
using FungleAPI.Utilities.Prefabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Components;
using UnityEngine;
using System.Reflection;
using FungleAPI.Utilities;
using FungleAPI.Components;

namespace TheOldUs.TOU
{
    public static class TouAssets
    {
        public static Sprite TemporaryButton;
        public static void LoadAssets()
        {
            TemporaryButton = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.funnyTemporaryButton", 100);
            SheriffKill = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.SheriffKill", 130);
            JailerArrest = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.JailerArrest", 300);
            JailerRelease = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.JailerRelease", 250);
            CreateVent = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.CreateVent", 100);
            MedScan = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.MedScan", 100);
            Cuffs = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Varied.Cuffs", 225);
            EquipGun = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.EquipGun", 100);
            UnequipGun = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.UnequipGun", 100);
            Reload = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.Reload", 100);
            Gun = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Varied.Gun", 100);
            Gasoline = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.Gasoline", 140);
            Flame = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.Flame", 200);
            Acid = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.Acid", 80);
            AcidVent = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Ship.AcidVent", 100);
            Clean = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.Clean", 80);
            Grab = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.Grab", 100);
            Teleport = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.Teleport", 110);
            UnlockVents = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.UnlockVents", 110);
            Revive = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.Revive", 150);
            Rewind = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Buttons.Rewind", 120);
            NovisorIdle = ResourceHelper.LoadGif(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Animations.NovisorIdle", 100);
            NovisorRun = ResourceHelper.LoadGif(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Animations.NovisorRun", 100);
            NovisorAttack = ResourceHelper.LoadGif(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Animations.NovisorAttack", 100);
            LoadPrefabs();
        }
        public static void LoadPrefabs()
        {
            LoadJail();
            LoadTsunami();
            FakeNovisor = new Prefab<FakeNovisorComp>(new GameObject("FakeNovisor").AddComponent<GifAnimator>().gameObject.AddComponent<SpriteRenderer>().gameObject.AddComponent<FakeNovisorComp>());
            FakeNovisor.prefab.gameObject.AddComponent<BoxCollider2D>().size = new Vector2(0.8f, 1.8f);
            Target = new Prefab<GameObject>(new GameObject("Target"));
            Target.prefab.AddComponent<SpriteRenderer>().sprite = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Varied.Target", 100);
            Target.prefab.AddComponent<TargetBehaviour>();
        }
        public static void LoadTsunami()
        {
            Tsunami = new Prefab<AcidTsunami>(new GameObject("Tsunami").AddComponent<AcidTsunami>());
            Tsunami.prefab.gameObject.AddComponent<SpriteRenderer>().sprite = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Ship.AcidTsunami", 100);
            BoxCollider2D collider = Tsunami.prefab.gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
            Vector2 size = collider.size;
            size.x = 1;
            collider.size = size;
            collider.transform.localScale = Vector3.one * 3.1f;
        }
        public static void LoadJail()
        {
            Jail = new Prefab<JailBehaviour>(new GameObject("Jail").AddComponent<JailBehaviour>());
            Jail.prefab.gameObject.AddComponent<SpriteRenderer>().sprite = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Ship.Jail", 200);
            Jail.prefab.transform.localScale *= 0.9f;
            SpriteRenderer bars = new GameObject("Bars").AddComponent<SpriteRenderer>();
            bars.sprite = ResourceHelper.LoadSprite(TheOldUsPlugin.Plugin, "TheOldUs.Resources.Ship.Bars", 200);
            bars.transform.SetParent(Jail.prefab.transform);
            bars.transform.localScale = new Vector3(1, 0.9f, 1);
            bars.transform.localPosition = new Vector3(0f, -2f, -2.776f);
            BoxCollider2D c1 = new GameObject("Collider").AddComponent<BoxCollider2D>();
            c1.transform.SetParent(Jail.prefab.transform);
            c1.transform.localScale = new Vector3(1.6f, 0.8f, 1);
            c1.transform.localPosition = new Vector3(1.4f, -1.6f, 0);
            BoxCollider2D c2 = new GameObject("Collider").AddComponent<BoxCollider2D>();
            c2.transform.SetParent(Jail.prefab.transform);
            c2.transform.localScale = new Vector3(1.6f, 0.8f, 1);
            c2.transform.localPosition = new Vector3(-1.4f, -1.6f, 0);
            BoxCollider2D c3 = new GameObject("Collider").AddComponent<BoxCollider2D>();
            c3.transform.SetParent(Jail.prefab.transform);
            c3.transform.localScale = new Vector3(0.5f, 2.4f, 1);
            c3.transform.localPosition = new Vector3(1.4f, 0, 0);
            BoxCollider2D c4 = new GameObject("Collider").AddComponent<BoxCollider2D>();
            c4.transform.SetParent(Jail.prefab.transform);
            c4.transform.localScale = new Vector3(0.5f, 2.4f, 1);
            c4.transform.localPosition = new Vector3(-1.4f, 0, 0);
            CapsuleCollider2D c5 = new GameObject("Collider").AddComponent<CapsuleCollider2D>();
            c5.direction = CapsuleDirection2D.Horizontal;
            c5.size = new Vector2(1.2f, 0.7f);
            c5.transform.SetParent(Jail.prefab.transform);
            c5.transform.localScale = Vector3.one;
            c5.transform.localPosition = new Vector3(0, 0.1f, 0);
            BoxCollider2D c6 = new GameObject("Collider").AddComponent<BoxCollider2D>();
            c6.transform.SetParent(Jail.prefab.transform);
            c6.transform.localScale = new Vector3(4.1f, 0.1f, 1);
            c6.transform.localPosition = new Vector3(0, 1.3f, 0);
        }
        public static Sprite SheriffKill;
        public static Sprite JailerArrest;
        public static Sprite JailerRelease;
        public static Sprite CreateVent;
        public static Sprite MedScan;
        public static Sprite Cuffs;
        public static Sprite EquipGun;
        public static Sprite UnequipGun;
        public static Sprite Reload;
        public static Sprite Gun;
        public static Sprite Flame;
        public static Sprite Gasoline;
        public static Sprite Acid;
        public static Sprite AcidVent;
        public static Sprite Clean;
        public static Sprite Grab;
        public static Sprite Teleport;
        public static Sprite UnlockVents;
        public static Sprite Revive;
        public static Sprite Rewind;
        public static GifFile NovisorIdle;
        public static GifFile NovisorRun;
        public static GifFile NovisorAttack;
        public static Prefab<JailBehaviour> Jail;
        public static Prefab<FakeNovisorComp> FakeNovisor;
        public static Prefab<GameObject> Target;
        public static Prefab<AcidTsunami> Tsunami;
    }
}

