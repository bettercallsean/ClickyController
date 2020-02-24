using System;
using System.Collections.Generic;
using System.Text;

namespace ClickyControllerGUI.Models
{
    public class Command
    {
        public Command()
        {

        }

        public string DisplayName { get; set; }
        public string Parameters { get; set; }
        public string Method { get; set; }
        public string Namespace { get; set; }
    }
}
