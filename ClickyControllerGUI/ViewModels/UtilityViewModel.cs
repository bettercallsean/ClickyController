using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ClickyControllerGUI.ViewModels
{
    public class UtilityViewModel
    {
        public static void Wait(string seconds)
        {
            Thread.Sleep(int.Parse(seconds) * 1000);
        }
    }
}
