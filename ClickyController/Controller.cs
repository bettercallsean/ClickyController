using System;
using System.Runtime.InteropServices;

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
        public static void KeyPress(ushort keyCode)
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

    }

}
