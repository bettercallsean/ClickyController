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

        public char Button { get; set; }

        public override void Execute()
        {
            switch(Button)
            {
                case 'L':
                    Mouse.LeftClick();
                    break;
                case 'M':
                    Mouse.MiddleClick();
                    break;
                case 'R':
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
        public char ButtonAction { get; set; }

        public string Character { get; set; }

        public override void Execute()
        {
            switch(ButtonAction)
            {
                case 'P':
                    Keyboard.KeyPress(Character);
                    break;
                case 'D':
                    Keyboard.KeyDown(Character);
                    break;
                case 'U':
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
