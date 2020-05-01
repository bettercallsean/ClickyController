using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ClickyController
{
    /// <summary>
    /// Contains the methods and structs that are needed by the Mouse and Keyboard classes to send input to Windows.
    /// This class also contains any miscellaneous features that don't necessarily fit under the function of a 'mouse'
    /// or 'keyboard.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Part of the Win32 API and is used to send either mouse or keyboard commands to Windows.
        /// More information about the inputs can be found here https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput
        /// </summary>
        /// <param name="numberOfInputs">The number of items found in the INPUT[] array</param>
        /// <param name="input">Array containing INPUT items with data about the action a user wishes to perform</param>
        /// <param name="inputSize">Size of the input array in bytes</param>
        /// <returns>The methods found in the Mouse and Keyboard classes take are of formatting the data for use with this function</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern uint SendInput(uint numberOfInputs, INPUT[] input, int inputSize);

        /// <summary>
        /// Retrieves information about the current thread
        /// More information about this function can be found here https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmessageextrainfo
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetMessageExtraInfo();

        /// <summary>
        /// Contains the information needed by 'SendInput' function to create a simulted mouse/keyboard event
        /// More information can be found here https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-input
        /// </summary>
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

        /// <summary>
        /// Contains information about a simulated mouse event
        /// More information can be found here https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput
        /// </summary>
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

        /// <summary>
        /// Contains information about a simulated keyboard event
        /// More information can be found here https://docs.microsoft.com/en-gb/windows/win32/api/winuser/ns-winuser-keybdinput
        /// </summary>
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

        /// <summary>
        /// A workaround for the lack of a native 'Union' function in C#
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion
        {
            [FieldOffset(0)]
            internal MOUSEINPUT mouseInput;
            [FieldOffset(0)]
            internal KEYBDINPUT keyboardInput;
        }

        /// <summary>
        /// Waits the inputted amount of time in seconds
        /// </summary>
        /// <param name="seconds">Number of seconds to pause the program for</param>
        public static void Wait(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
    }
}
