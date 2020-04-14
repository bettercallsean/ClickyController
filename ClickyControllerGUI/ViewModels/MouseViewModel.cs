using ClickyController;
using ClickyControllerGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClickyControllerGUI.ViewModels
{
    public class MouseViewModel : BaseViewModel
    {
        public int XPosition => Mouse.XPosition;
        public int YPosition => Mouse.YPosition;

    }
}
