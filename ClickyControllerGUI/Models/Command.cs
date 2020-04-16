using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ClickyController;

namespace ClickyControllerGUI.Models
{
    public class Command
    {
        public Command()
        {

        }
        
        public bool TakesParameters { get; set; }

        public string Method { get; set; }

    }

    public class MouseClick : Command
    {
        public MouseClick()
        {
            TakesParameters = false;
        }

        public string Button { get; set; }
    }

    public class MouseMove : Command
    {
        public MouseMove()
        {
            TakesParameters = true;
        }

        public int XCoords { get; set; }
        public int YCoords { get; set; }
        public string Coordinates { get => string.Format("{0}, {1}", XCoords, YCoords); }
    }

    public class KeyboardCharacterInput : Command
    {
        public KeyboardCharacterInput()
        {
            TakesParameters = true;
        }

        // Stores whether the buttons is pressed normally, down or up
        public string ButtonAction { get; set; }

        public string Character { get; set; }
    }

    public class KeyboardTextInput : Command
    {
        public KeyboardTextInput()
        {
            TakesParameters = true;
        }

        public string Text { get; set; }
    }

    public class Wait : Command
    {
        public Wait()
        {
            TakesParameters = true;
        }

        public int Seconds { get; set; }
    }
}
