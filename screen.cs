using ComputerPlusPlus;
using GorillaNetworking;
using Oculus.Platform.Samples.VrHoops;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wall_Climb
{
    public class Wall_Clmb : IScreen
    {
        public string Title => "Wall Clmb";

        public string Description => "Press 1-9 to change the detection range, thats it \r\n Made By Abominathan96";

        string Button;

        // This is called every frame while the screen is active
        public string GetContent()
        {
            if (Button == "1")
            {
                Plugin.DetR = 0.1f;
            }
            else if (Button == "2")
            {
                Plugin.DetR = 0.2f;
            }
            else if (Button == "3")
            {
                Plugin.DetR = 0.3f;
            }
            else if(Button == "4")
            {
                Plugin.DetR = 0.4f;
            }
            Button = null;
            return ("Detection Radius is " + Plugin.DetR);
        }

        // This is called whenever a key is pressed while the screen is active
        public void OnKeyPressed(GorillaKeyboardButton button)
        {
            Button = button.characterString;

        }

        // This is called when the screen is registered
        public void Start() { }

    }
}
