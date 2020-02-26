﻿using ClickyController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickyControllerGUI.Models
{
    internal class Controller
    {
        public Controller()
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
            // This whole Model needs cleaning because its prone to errors 
            int[] mouseCoordinates = coordinates.Split(',').Select(int.Parse).ToArray();
            Mouse.MoveMouse(mouseCoordinates[0], mouseCoordinates[1]);
        }

        public void Wait(string seconds) => ClickyController.Controller.Wait(int.Parse(seconds));

        public void EnterText(string text) => Keyboard.EnterText(text);

        public void PressKey(string key) => Keyboard.KeyPress(key);

        public void KeyDown(string key) => Keyboard.KeyDown(key);

        public void KeyUp(string key) => Keyboard.KeyRelease(key);
    }
}
