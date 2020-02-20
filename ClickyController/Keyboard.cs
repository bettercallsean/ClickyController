using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickyController
{
    public class Keyboard : Controller
    {
        private static readonly Dictionary<string, ushort> KeyToVirtualKeyDictionary = JsonConvert.DeserializeObject<Dictionary<string, ushort>>(Properties.Resources.VirtualKeyCodes);
        private static readonly Dictionary<string, string> KeyToVirtualKeyShiftDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Properties.Resources.VirtualKeyCodesShift);
        private static readonly Dictionary<string, ushort> KeyToScanCodeDictionary = JsonConvert.DeserializeObject<Dictionary<string, ushort>>(Properties.Resources.ScanCodes);

        public static void KeyPress(string character)
        {
            bool holdShift = false;

            /* If the character entered requires the SHIFT key to be pressed, the keyToVirtualKeyShiftDictionary dictionary will contain the
             normal key that needs to be pressed 
             e.g. "!" is on the "1" key, "~" is on the "#" key 
             */

            if (!KeyToVirtualKeyDictionary.ContainsKey(character.ToString()))
            {
                try
                {
                    character = KeyToVirtualKeyShiftDictionary[character];
                    holdShift = true;
                }
                catch(KeyNotFoundException e)
                {
                    Console.WriteLine(e);
                }
            }

            INPUT keyPress = new INPUT
            {
                type = 1,
                union =
                {
                    keyboardInput = new KEYBDINPUT
                    {
                        virtualKeyCode = KeyToVirtualKeyDictionary[character],
                        time = 0,
                        extraInfo = GetMessageExtraInfo(),
                        hardwareScanCode = 0,
                        keystrokeFlags = 0
                    }
                }
            };


            INPUT keyRelease = new INPUT
            {
                type = 1,
                union =
                {
                    keyboardInput = new KEYBDINPUT
                    {
                        virtualKeyCode = KeyToVirtualKeyDictionary[character],
                        time = 0,
                        extraInfo = GetMessageExtraInfo(),
                        hardwareScanCode = 0,
                        keystrokeFlags = 2
                    }
                }
            };


            INPUT[] inputs = new INPUT[] { keyPress, keyRelease };

            if (holdShift)
            {
                KeyDown("SHIFT");
                SendInput(2, inputs, INPUT.Size);
                KeyRelease("SHIFT");
            }
            else
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
                        KeyPress(letter.ToString());

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
                type = 1,
                union =
                {
                    keyboardInput = new KEYBDINPUT
                    {
                        virtualKeyCode = KeyToVirtualKeyDictionary[character],
                        time = 0,
                        extraInfo = GetMessageExtraInfo(),
                        hardwareScanCode = 0,
                        keystrokeFlags = 0
                    }
                }
            };


            INPUT[] inputs = new INPUT[] { keyPress };

            SendInput(1, inputs, INPUT.Size);
        }

        public static void KeyRelease(string character)
        {
            INPUT keyPress = new INPUT
            {
                type = 1,
                union =
                {
                    keyboardInput = new KEYBDINPUT
                    {
                        virtualKeyCode = KeyToVirtualKeyDictionary[character],
                        time = 0,
                        extraInfo = GetMessageExtraInfo(),
                        hardwareScanCode = 0,
                        keystrokeFlags = 2
                    }
                }
            };


            INPUT[] inputs = new INPUT[] { keyPress };

            SendInput(1, inputs, INPUT.Size);
        }

        public static void KeyPressScanCode(string character)
        {
            ushort scanCode = KeyToScanCodeDictionary[character];

            INPUT keyDown = new INPUT
            {
                type = 1,
                union =
                {
                    keyboardInput = new KEYBDINPUT
                    {
                        virtualKeyCode = 0,
                        time = 0,
                        extraInfo = GetMessageExtraInfo(),
                        hardwareScanCode = scanCode,
                        keystrokeFlags = 0x0008
                    }
                }
            };


            INPUT keyRelease = new INPUT
            {
                type = 1,
                union =
                {
                    keyboardInput = new KEYBDINPUT
                    {
                        virtualKeyCode = 0,
                        time = 0,
                        extraInfo = GetMessageExtraInfo(),
                        hardwareScanCode = scanCode,
                        keystrokeFlags = 0x0008 | 0x0002
                    }
                }
            };


            INPUT[] inputs = new INPUT[] { keyDown, keyRelease };

            SendInput(2, inputs, INPUT.Size);

        }

        public static void KeyDownScanCode(string character)
        {
            INPUT keyPress = new INPUT
            {
                type = 1,
                union =
                {
                    keyboardInput = new KEYBDINPUT
                    {
                        virtualKeyCode = 0,
                        time = 0,
                        extraInfo = GetMessageExtraInfo(),
                        hardwareScanCode = KeyToScanCodeDictionary[character],
                        keystrokeFlags = 0x0008
                    }
                }
            };


            INPUT[] inputs = new INPUT[] { keyPress };

            SendInput(1, inputs, INPUT.Size);
        }

        public static void KeyReleaseScanCode(string character)
        {

            INPUT keyPress = new INPUT
            {
                type = 1,
                union =
                {
                    keyboardInput = new KEYBDINPUT
                    {
                        virtualKeyCode = 0,
                        time = 0,
                        extraInfo = GetMessageExtraInfo(),
                        hardwareScanCode = KeyToScanCodeDictionary[character],
                        keystrokeFlags = 0x0008 | 0x0002
                    }
                }
            };


            INPUT[] inputs = new INPUT[] { keyPress };

            SendInput(1, inputs, INPUT.Size);
        }

    }
}
