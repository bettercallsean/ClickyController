using ClickyController;
using ClickyControllerGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickyControllerGUI.ViewModels
{
    public class MouseViewModel : BaseViewModel
    {
        public int XPosition => Mouse.XPosition;
        public int YPosition => Mouse.YPosition;

        public static void MouseClick(string buttonType)
        {
            switch (buttonType)
            {
                case "L":
                    Mouse.LeftClick();
                    break;
                case "R":
                    Mouse.RightClick();
                    break;
                case "M":
                    Mouse.MiddleClick();
                    break;
                default:
                    break;
            } 
        }

        public static void MoveMouse(string coordinateString)
        {
            List<int> coordinates = coordinateString.Split(' ').Select(int.Parse).ToList();

            Mouse.MoveMouse(coordinates[0], coordinates[1], true);
        }

        public static void ScrollWheel(string direction)
        {
            switch(direction)
            {
                case "U":
                    Mouse.WheelUp();
                    break;
                case "D":
                    Mouse.WheelDown();
                    break;
                default:
                    break;
            }
        }

    }
}
