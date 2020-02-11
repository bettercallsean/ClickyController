using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ClickyController
{
    public struct POINT
    {
        internal int xPosition;
        internal int yPosition;
    }

    public class Mouse : Controller
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool GetCursorPos(out POINT mousePosition);

        private static POINT _mousePosition;

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

        public static POINT MousePosition
        {
            get
            {
                GetCursorPos(out _mousePosition);
                return _mousePosition;
            }

            private set => _mousePosition = value;
        }

        public static long XPosition => MousePosition.xPosition;
        public static long YPosition => MousePosition.yPosition;

    }
}
