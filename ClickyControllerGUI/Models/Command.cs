using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ClickyController;

namespace ClickyControllerGUI.Models
{
    public abstract class Command
    {
        public abstract void Execute();

        public string Type { get; set; }

    }

    public class MouseClick : Command
    {
        public MouseClick()
        {

        }

        public string Button { get; set; }

        public override void Execute()
        {
            switch(Button)
            {
                case "Left":
                    Mouse.LeftClick();
                    break;
                case "Middle":
                    Mouse.MiddleClick();
                    break;
                case "Right":
                    Mouse.RightClick();
                    break;
            }
        }
    }

    public class MouseMove : Command
    {
        public MouseMove()
        {

        }

        public int XCoords { get; set; }
        public int YCoords { get; set; }
        public bool Relative { get; set; }

        public override void Execute()
        {
            Mouse.MoveMouse(XCoords, YCoords, Relative);
        }

    }

    public class KeyboardCharacterInput : Command
    {
        public KeyboardCharacterInput()
        {

        }

        // Stores whether the buttons is pressed normally ('P'), down ('D') or up ('U')
        public string ButtonAction { get; set; }

        public string Character { get; set; }

        public override void Execute()
        {
            switch(ButtonAction)
            {
                case "Press":
                    Keyboard.KeyPress(Character);
                    break;
                case "Down":
                    Keyboard.KeyDown(Character);
                    break;
                case "Up":
                    Keyboard.KeyRelease(Character);
                    break;
            }
        }

    }

    public class KeyboardTextInput : Command
    {
        public KeyboardTextInput()
        {

        }

        public string Text { get; set; }

        public override void Execute()
        {
            Keyboard.EnterText(Text);
        }
    }

    public class Wait : Command
    {
        public Wait()
        {

        }

        public int Seconds { get; set; }

        public override void Execute()
        {
            Controller.Wait(Seconds);
        }
    }
}
