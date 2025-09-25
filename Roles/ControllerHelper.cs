using LibCpp2IL.MachO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.Roles
{
    internal class ControllerHelper
    {
        public DragState State;
        public Controller myController;
        private bool LastCheck;
        public Vector3 HoverPosition => myController.HoverPosition;
        public ControllerHelper()
        {
            myController = new Controller();
        }
        public void Update()
        {
            myController.Update();
            State = DragState.NoTouch;
            if (myController.AnyTouch && LastCheck)
            {
                State = DragState.Holding;
            }
            if (myController.AnyTouch && !LastCheck)
            {
                State = DragState.TouchStart;
                LastCheck = true;
            }
            if (!myController.AnyTouch && LastCheck)
            {
                State = DragState.Released;
                LastCheck = false;
            }
        }
    }
}
