using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.Components
{
    [FungleAPI.Attributes.RegisterTypeInIl2Cpp]
    public class ControllerHelper : MonoBehaviour
    {
        public static Controller myController;
        public static Dictionary<Controller.TouchState, ChangeableValue<bool>> Touches = new Dictionary<Controller.TouchState, ChangeableValue<bool>>();
        public void Start()
        {
            myController = new Controller();
            foreach (Controller.TouchState touch in myController.Touches)
            {
                Touches.Add(touch, new ChangeableValue<bool>(false));
            }
        }
        public void Update()
        {
            myController.Update();
            foreach (KeyValuePair<Controller.TouchState, ChangeableValue<bool>> pair in Touches)
            {
                pair.Key.dragState = DragState.NoTouch;
                if (pair.Key.IsDown && pair.Value.Value)
                {
                    pair.Key.dragState = DragState.Holding;
                }
                if (pair.Key.IsDown && !pair.Value.Value)
                {
                    pair.Key.dragState = DragState.TouchStart;
                    pair.Value.Value = true;
                }
                if (!pair.Key.IsDown && pair.Value.Value)
                {
                    pair.Key.dragState = DragState.Released;
                    pair.Value.Value = false;
                }
            }
        }
        public static bool AnyStartedTouch(out Controller.TouchState touch)
        {
            foreach (KeyValuePair<Controller.TouchState, ChangeableValue<bool>> pair in Touches)
            {
                if (pair.Key.dragState == DragState.TouchStart)
                {
                    touch = pair.Key;
                    return true;
                }
            }
            touch = null;
            return false;
        }
    }
}
