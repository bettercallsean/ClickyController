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

            // If the character isn't " ", then all the whitespace gets removed
            // e.g. "Caps Lock " will turn to "CapsLock"

            // This is only useful when someone calls this method directly from their program to press a certain key 
            // Otherwise the EnterText method passes each character through one by one anyway, meaning there won't be any whitespace to remove
            if (!string.IsNullOrWhiteSpace(character))
                character = character.Replace(" ", string.Empty);

            // The dictionary keys are all stored in lowercase, so any
            character = character.ToLower();

            if (!VirtualCodeKeyExists(character))
            {
                // If the character entered requires 'Shift' to be held, this tries to find it in a dictionary containing those values
                // e.g "!" or "@"
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
                KeyUp("SHIFT");
            }
            else
                SendInput(2, inputs, INPUT.Size);
        }

        public static void EnterText(string textEntry)
        {

            foreach (char letter in textEntry)
            {
                if (char.IsUpper(letter))
                    {
                        // Simulates holding 'SHIFT' in order to create a capitalised version of a character 
                        KeyDown("SHIFT");
                        KeyPress(letter.ToString());
                        KeyUp("SHIFT");
                    }
                else
                    KeyPress(letter.ToString());
            }
        }

        public static void KeyDown(string character)
        {
            // Simulates holding a key down

            character = character.ToLower();

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

        public static void KeyUp(string character)
        {
            // Simulates releasing a key
            character = character.ToLower();

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

        public static void KeyboardShortcut(string character1, string character2, string character3 = "")
        {
            KeyDown(character1);
            KeyDown(character2);

            if (!string.IsNullOrWhiteSpace(character3))
                KeyPress(character3);
            

            KeyUp(character1);
            KeyUp(character2);
        }

        public static void KeyboardShortcutScanCode(string character1, string character2, string character3 = "")
        {
            KeyDownScanCode(character1);
            KeyDownScanCode(character2);

            if(!string.IsNullOrWhiteSpace(character3))
                KeyPressScanCode(character3);

            KeyUpScanCode(character1);
            KeyUpScanCode(character2);
        }

        // ScanCodes are the codes sent directly by your keyboard hardware and can be useful in apps/games that take their input 
        // directly from the keyboard directly.
        public static void KeyPressScanCode(string character)
        {
            character = character.ToLower();

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
            character = character.ToLower();

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

        public static void KeyUpScanCode(string character)
        {
            character = character.ToLower();

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

        public static bool VirtualCodeKeyExists(string character)
        {
            if (KeyToVirtualKeyDictionary.ContainsKey(character))
                return true;
            else
                return false;
        }

        public static bool ScanCodeKeyExists(string character)
        {
            if (KeyToScanCodeDictionary.ContainsKey(character))
                return true;
            else
                return false;
        }

    }
}
