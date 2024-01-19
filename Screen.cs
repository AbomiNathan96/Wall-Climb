using ComputerPlusPlus;
using GorillaNetworking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wall_Climb
{
    public class Screen : IScreen
    {
        public string Title => "Test";

        public string Description => "To see if my code works lmao";

        public static bool Work = false;
        public static bool Left;
        public static bool Right;
        public static string Button = null;

        // This is called every frame while the screen is active
        public string GetContent()
        {
            return ("Grab Distence: " + Plugin.DetR + "\r\nWorking " + Work + "\r\nLeft " + Left + "\r\nRight " + Right);
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
