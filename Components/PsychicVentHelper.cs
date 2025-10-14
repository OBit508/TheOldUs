using FungleAPI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOldUs.Patches;

namespace TheOldUs.Components
{
    public class PsychicVentHelper : VentComponent
    {
        public Console console;
        public void Start()
        {
            console = GetComponent<Console>();
        }
        public void Update()
        {
            if (Vent.currentVent == vent && PlayerControl.LocalPlayer.transform.position != vent.transform.position && ShipStatusPatch.MovingConsoles.ContainsValue(console))
            {
                PlayerControl.LocalPlayer.transform.position = vent.transform.position;
            }
        }
    }
}
