using System;
using System.Runtime.InteropServices;

namespace ClickyController
{
    

    public class Controller
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern uint SendInput(uint numberOfInputs, INPUT[] input, int inputSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetMessageExtraInfo();


        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            internal uint type;
            internal InputUnion union;
            public static int Size
            {
                get { return Marshal.SizeOf(typeof(INPUT)); }
            }

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
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
        public struct KEYBDINPUT
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

        public static void KeyboardInput(ushort keyCode)
        {


            INPUT input = new INPUT
            {
                type = 1
            };

            input.union.keyboardInput = new KEYBDINPUT
            {
                virtualKeyCode = keyCode,
                time = 0,
                extraInfo = IntPtr.Zero,
                hardwareScanCode = 0,
                keystrokeFlags = 0
            };

            INPUT input2 = new INPUT
            {
                type = 1
            };

            input2.union.keyboardInput = new KEYBDINPUT
            {
                virtualKeyCode = keyCode,
                time = 0,
                extraInfo = IntPtr.Zero,
                hardwareScanCode = 0,
                keystrokeFlags = 2
            };

            INPUT[] inputs = new INPUT[] { input, input2 };

            

            if (SendInput(2, inputs, INPUT.Size) == 0)
            {
                int error = Marshal.GetLastWin32Error();
                Console.WriteLine(error);
            }
                
        }

        public static void LeftClick()
        {
            INPUT input = new INPUT
            {
                type = 0
            };

            input.union.mouseInput = new MOUSEINPUT
            {
                xPosition = 0,
                yPosition = 0,
                mouseButtonData = 0,
                mouseButtonAction = 0x0002,
                time = 0,
                extraInfo = IntPtr.Zero
            };

            INPUT input2 = new INPUT
            {
                type = 0
            };

            input2.union.mouseInput = new MOUSEINPUT
            {
                xPosition = 0,
                yPosition = 0,
                mouseButtonData = 0,
                mouseButtonAction = 0x0004,
                time = 0,
                extraInfo = IntPtr.Zero
            };



            INPUT[] inputs = new INPUT[] { input, input2 };

            SendInput(2, inputs, INPUT.Size);
        }
    }
}
