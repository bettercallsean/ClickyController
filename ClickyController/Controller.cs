using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
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
            // Stores data necessary for Windows to perform an action with the mouse
            internal int xPosition;
            internal int yPosition;
            internal uint mouseData;
            internal uint mouseAction;
            internal uint time;
            internal IntPtr extraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            // Stores data necessary for Windows to perform an action with the keyboard
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

        public static void Wait(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
    }
}
