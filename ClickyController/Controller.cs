using System;
using System.Runtime.InteropServices;

namespace ClickyController
{
    
    public class Controller
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern uint SendInput(uint numberOfInputs, INPUT[] input, int inputSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetMessageExtraInfo();


        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            internal uint type;
            internal InputUnion union;
            public static int Size
            {
                get { return Marshal.SizeOf(typeof(INPUT)); }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
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
        private struct KEYBDINPUT
        {
            /* Stores data necessary for Windows to perform an action with the keyboard */
            internal ushort virtualKeyCode;
            internal ushort hardwareScanCode;
            internal uint keystrokeFlags;
            internal uint time;
            internal IntPtr extraInfo;
        }


        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)]
            internal MOUSEINPUT mouseInput;
            [FieldOffset(0)]
            internal KEYBDINPUT keyboardInput;
        } 

        public static void KeyboardInput(ushort keyCode)
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

            

            if (SendInput(2, inputs, INPUT.Size) == 0)
            {
                int error = Marshal.GetLastWin32Error();
                Console.WriteLine(error);
            }
                
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

        public static void RightClick()
        {
            MouseClick(0x0008, 0x0010);
        }

        public static void LeftClick()
        {
            MouseClick(0x0002, 0x0004);
        }
    }
}
