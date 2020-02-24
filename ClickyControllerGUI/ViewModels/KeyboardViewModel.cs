using ClickyController;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickyControllerGUI.ViewModels
{
    public class KeyboardViewModel : BaseViewModel
    {
        public static void EnterText(string text)
        {
            Keyboard.EnterText(text);
        }

        public static void KeyPress(string character)
        {
            Keyboard.KeyPress(character);
        }

        public static void KeyRelease(string character)
        {
            Keyboard.KeyRelease(character);
        }

        public static void KeyDown(string character)
        {
            Keyboard.KeyDown(character);
        }


    }
}
