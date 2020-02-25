using ClickyControllerGUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickyControllerGUI.ViewModels
{
    public class CommandViewModel : BaseViewModel
    {
        private Command _command = new Command();

        public string CommandMethod
        {
            get => _command.Method;
            set { _command.Method = value;  OnPropertyChanged(); }
        }
        public string CommandNamespace
        {
            get => _command.Namespace;
            set { _command.Namespace = value; OnPropertyChanged(); }
        }
        public string CommandParameters
        {
            get => _command.Parameters;
            set { _command.Parameters = value; OnPropertyChanged(); }
        }
    }
}
