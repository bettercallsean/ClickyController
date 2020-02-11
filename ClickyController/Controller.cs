using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ClickyController
{
    
    public class Controller
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern uint SendInput(uint numberOfInputs, INPUT[] input, int inputSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetMessageExtraInfo();

        [StructLayout(LayoutKind.Sequential)]
        internal struct INPUT
        {
            internal uint type;
            internal InputUnion union;
            public static int Size
            {
                get { return Marshal.SizeOf(typeof(INPUT)); }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            /* Stores data necessary for Windows to perform an action with the mouse */
            internal int xPosition;
            internal int yPosition;
            internal uint mouseButtonData;
            internal uint mouseButtonAction;
            internal uint time;
            internal IntPtr extraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            /* Stores data necessary for Windows to perform an action with the keyboard */
            internal ushort virtualKeyCode;
            internal ushort hardwareScanCode;
            internal uint keystrokeFlags;
            internal uint time;
            internal IntPtr extraInfo;
        }


        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion
        {
            [FieldOffset(0)]
            internal MOUSEINPUT mouseInput;
            [FieldOffset(0)]
            internal KEYBDINPUT keyboardInput;
        } 
    }

    public class Mouse : Controller
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool GetCursorPos(out POINT mousePosition);

        private struct POINT
        {
            long xPosition;
            long yPosition;
        }

        private static void MouseClick(uint buttonDownActionCode, uint buttonReleaseActionCode)
        {
            INPUT buttonDown = new INPUT
            {
                type = 0
            };

            buttonDown.union.mouseInput = new MOUSEINPUT
            {
                xPosition = 0,
                yPosition = 0,
                mouseButtonData = 0,
                mouseButtonAction = buttonDownActionCode,
                time = 0,
                extraInfo = GetMessageExtraInfo()
            };

            INPUT buttonRelease = new INPUT
            {
                type = 0
            };

            buttonRelease.union.mouseInput = new MOUSEINPUT
            {
                xPosition = 0,
                yPosition = 0,
                mouseButtonData = 0,
                mouseButtonAction = buttonReleaseActionCode,
                time = 0,
                extraInfo = GetMessageExtraInfo()
            };

            INPUT[] inputs = new INPUT[] { buttonDown, buttonRelease };

            SendInput(2, inputs, INPUT.Size);
        }

        private static void MouseAction(uint buttonActionCode)
        {
            INPUT buttonAction = new INPUT
            {
                type = 0
            };

            buttonAction.union.mouseInput = new MOUSEINPUT
            {
                xPosition = 0,
                yPosition = 0,
                mouseButtonData = 0,
                mouseButtonAction = buttonActionCode,
                time = 0,
                extraInfo = GetMessageExtraInfo()
            };

            INPUT[] inputs = new INPUT[] { buttonAction };

            SendInput(1, inputs, INPUT.Size);
        }


        public static void LeftClick()
        {
            MouseClick(0x0002, 0x0004);
        }

        public static void LeftPress()
        {
            MouseAction(0x0002);
        }

        public static void LeftRelease()
        {
            MouseAction(0x0004);
        }

        public static void RightClick()
        {
            MouseClick(0x0008, 0x0010);
        }

        public static void RightPress()
        {
            MouseAction(0x0008);
        }

        public static void RightRelease()
        {
            MouseAction(0x0010);
        }
    }

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
                        //TODO Differentiate bettwen upper and lowercase and reflect that in text entry
                        KeyDown();
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

        public static void KeyDown(ushort keyCode)
        {
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

            INPUT[] inputs = new INPUT[] { keyPress };

            SendInput(1, inputs, INPUT.Size);
        }

        public static void KeyRelease(ushort keyCode)
        {
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
                keystrokeFlags = 2
            };

            INPUT[] inputs = new INPUT[] { keyPress };

            SendInput(1, inputs, INPUT.Size);
        }

        public static void KeyDownScanCode(ushort scanCode)
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
                hardwareScanCode = scanCode,
                keystrokeFlags = 0x0008
            };

            INPUT[] inputs = new INPUT[] { keyPress };

            SendInput(1, inputs, INPUT.Size);
        }

        public static void KeyReleaseScanCode(ushort scanCode)
        {
            scanCode += 128;

            INPUT keyPress = new INPUT
            {
                type = 1
            };

            keyPress.union.keyboardInput = new KEYBDINPUT
            {
                virtualKeyCode = 0,
                time = 0,
                extraInfo = GetMessageExtraInfo(),
                hardwareScanCode = scanCode,
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
