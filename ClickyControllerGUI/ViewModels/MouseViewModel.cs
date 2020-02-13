using ClickyController;
using ClickyControllerGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickyControllerGUI.ViewModels
{
    class MouseViewModel : BaseViewModel
    {
        public int XPosition { get => Mouse.XPosition; }
        public int YPosition { get => Mouse.YPosition; }
        
    }
}
