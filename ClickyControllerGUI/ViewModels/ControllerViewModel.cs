using ClickyController;
using ClickyControllerGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ClickyControllerGUI.ViewModels
{
    internal class ControllerViewModel : BaseViewModel
    {
        public ControllerViewModel()
        {

        }

        public void LeftClick() => Mouse.LeftClick();

        public void RightClick() => Mouse.RightClick();

        public void MiddleClick() => Mouse.LeftClick();

        public void LeftDown() => Mouse.LeftPress();

        public void LeftUp() => Mouse.LeftRelease();

        public void RightDown() => Mouse.RightPress();

        public void RightUp() => Mouse.RightRelease();

        public void MiddleDown() => Mouse.MiddleDown();

        public void MiddleUp() => Mouse.MiddleRelease();

        public void MoveMouse(string coordinates)
        {
            try
            {
                int[] mouseCoordinates = coordinates.Split(' ').Select(int.Parse).ToArray();
                Mouse.MoveMouse(mouseCoordinates[0], mouseCoordinates[1]);
            }
            catch(FormatException)
            {

            }
        }

        public void Wait(string seconds)
        {
            try
            {
                Controller.Wait(int.Parse(seconds));
            }
            catch(FormatException)
            {

            }
            
        }

        public void EnterText(string text) => Keyboard.EnterText(text);

        public void PressKey(string key) => Keyboard.KeyPress(key);

        public void KeyDown(string key) => Keyboard.KeyDown(key);

        public void KeyUp(string key) => Keyboard.KeyRelease(key);

        private bool _validParamterFormat;
        public bool ValidParameterFormat
        {
            get => _validParamterFormat;
            set { _validParamterFormat = value; OnPropertyChanged(); }
        }

    }
}
