using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.Components
{
    [FungleAPI.Attributes.RegisterTypeInIl2Cpp]
    public class TargetBehaviour : MonoBehaviour
    {
        public static List<TargetBehaviour> targets = new List<TargetBehaviour>();
        public SpriteRenderer Rend;
        public object Parent;
        public Predicate<PlayerControl> Predicate;
        public static void DestroyAll()
        {
            foreach (TargetBehaviour target in targets)
            {
                if (target != null)
                {
                    GameObject.Destroy(target.gameObject);
                }
            }
            targets.Clear();
        }
        public void Awake()
        {
            Rend = GetComponent<SpriteRenderer>();
            if (transform.parent != null)
            {
                PlayerControl p = transform.parent.GetComponent<PlayerControl>();
                if (p != null)
                {
                    Parent = p;
                }
            }
            transform.localPosition = new Vector3(0, 0, -0.5f);
            transform.localScale = Vector3.one;
            targets.Add(this);
        }
        public void LateUpdate()
        {
            transform.Rotate(new Vector3(0, 0, 100) * Time.deltaTime);
            if (Parent != null)
            {
                if (Parent is PlayerControl player)
                {
                    Rend.enabled = Predicate(player);
                }
            }
        }
    }
}
