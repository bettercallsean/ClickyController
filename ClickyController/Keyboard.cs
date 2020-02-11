﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickyController
{
    public class Keyboard : Controller
    {

        private static Dictionary<string, ushort> keyToVirtualKeyDictionary = JsonConvert.DeserializeObject<Dictionary<string, ushort>>(Properties.Resources.VirtualKeyCodes);
        private static Dictionary<string, ushort> keyToScanCodeDictionary = JsonConvert.DeserializeObject<Dictionary<string, ushort>>(Properties.Resources.ScanCodes);

        public static void KeyPress(string character)
        {
            ushort keyCode = keyToVirtualKeyDictionary[character];

            INPUT keyPress = new INPUT
            {
                type = 1
            };

            keyPress.union.keyboardInput = new KEYBDINPUT
            {
                virtualKeyCode = keyCode,
                time = 0,
                extraInfo = GetMessageExtraInfo(),
                hardwareScanCode = 0,
                keystrokeFlags = 0
            };

            INPUT keyRelease = new INPUT
            {
                type = 1
            };

            keyRelease.union.keyboardInput = new KEYBDINPUT
            {
                virtualKeyCode = keyCode,
                time = 0,
                extraInfo = GetMessageExtraInfo(),
                hardwareScanCode = 0,
                keystrokeFlags = 2
            };

            INPUT[] inputs = new INPUT[] { keyPress, keyRelease };

            SendInput(2, inputs, INPUT.Size);

        }

        public static void EnterText(string textEntry)
        {
            foreach (char letter in textEntry)
            {
                try
                {
                    if (char.IsUpper(letter))
                    {
                        // Simulates holding 'SHIFT' in order to create a capitalised version of a character 
                        KeyDown("SHIFT");
                        KeyPress(letter.ToString().ToLower());
                        KeyRelease("SHIFT");
                    }
                    else
                    {
                        KeyPress(letter.ToString());
                    }
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public static void KeyDown(string character)
        {
            INPUT keyPress = new INPUT
            {
                type = 1
            };

            keyPress.union.keyboardInput = new KEYBDINPUT
            {
                virtualKeyCode = keyToVirtualKeyDictionary[character],
                time = 0,
                extraInfo = GetMessageExtraInfo(),
                hardwareScanCode = 0,
                keystrokeFlags = 0
            };

            INPUT[] inputs = new INPUT[] { keyPress };

            SendInput(1, inputs, INPUT.Size);
        }

        public static void KeyRelease(string character)
        {
            INPUT keyPress = new INPUT
            {
                type = 1
            };

            keyPress.union.keyboardInput = new KEYBDINPUT
            {
                virtualKeyCode = keyToVirtualKeyDictionary[character],
                time = 0,
                extraInfo = GetMessageExtraInfo(),
                hardwareScanCode = 0,
                keystrokeFlags = 2
            };

            INPUT[] inputs = new INPUT[] { keyPress };

            SendInput(1, inputs, INPUT.Size);
        }

        public static void KeyDownScanCode(string character)
        {
            INPUT keyPress = new INPUT
            {
                type = 1
            };

            keyPress.union.keyboardInput = new KEYBDINPUT
            {
                virtualKeyCode = 0,
                time = 0,
                extraInfo = GetMessageExtraInfo(),
                hardwareScanCode = keyToScanCodeDictionary[character],
                keystrokeFlags = 0x0008
            };

            INPUT[] inputs = new INPUT[] { keyPress };

            SendInput(1, inputs, INPUT.Size);
        }

        public static void KeyReleaseScanCode(string character)
        {

            INPUT keyPress = new INPUT
            {
                type = 1
            };

            keyPress.union.keyboardInput = new KEYBDINPUT
            {
                virtualKeyCode = 0,
                time = 0,
                extraInfo = GetMessageExtraInfo(),
                hardwareScanCode = keyToScanCodeDictionary[character],
                keystrokeFlags = 0x0008 | 0x0002
            };

            INPUT[] inputs = new INPUT[] { keyPress };

            SendInput(1, inputs, INPUT.Size);
        }

        public static void KeyPressScanCode(string character)
        {
            ushort scanCode = keyToScanCodeDictionary[character];

            INPUT keyDown = new INPUT
            {
                type = 1
            };

            keyDown.union.keyboardInput = new KEYBDINPUT
            {
                virtualKeyCode = 0,
                time = 0,
                extraInfo = GetMessageExtraInfo(),
                hardwareScanCode = scanCode,
                keystrokeFlags = 0x0008
            };

            INPUT keyRelease = new INPUT
            {
                type = 1
            };

            keyRelease.union.keyboardInput = new KEYBDINPUT
            {
                virtualKeyCode = 0,
                time = 0,
                extraInfo = GetMessageExtraInfo(),
                hardwareScanCode = scanCode,
                keystrokeFlags = 0x0008 | 0x0002
            };

            INPUT[] inputs = new INPUT[] { keyDown, keyRelease };

            SendInput(2, inputs, INPUT.Size);

        }

    }
}
