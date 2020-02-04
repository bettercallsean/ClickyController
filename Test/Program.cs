using System;
using System.Runtime.InteropServices;
using System.Threading;
using ClickyController;

namespace Test
{
    class Program
    {
        static void Main()
        {
            Thread.Sleep(5000);
            Controller.LeftClick();
            Thread.Sleep(10);
            Controller.KeyboardInput(0x41);
        }
    
    }
}
