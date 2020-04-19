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

        // Stores which mouse button is being pressed, e.g. "Left", "Middle", "Right"
        public string Button { get; set; }

        public string Action { get; set; }

        public override void Execute()
        {
            if(Action == "Down")
            {
                switch (Button)
                {
                    case "Left":
                        Mouse.LeftDown();
                        break;
                    case "Middle":
                        Mouse.MiddleDown();
                        break;
                    case "Right":
                        Mouse.RightDown();
                        break;
                }
            }
            else if (Action == "Press")
            {
                switch (Button)
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
            else if (Action == "Up")
            {
                switch (Button)
                {
                    case "Left":
                        Mouse.LeftUp();
                        break;
                    case "Middle":
                        Mouse.MiddleUp();
                        break;
                    case "Right":
                        Mouse.RightUp();
                        break;
                }
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

        // Stores whether the buttons is pressed normally ("Press"), down ("Down") or up ("Up")
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
                    Keyboard.KeyUp(Character);
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
