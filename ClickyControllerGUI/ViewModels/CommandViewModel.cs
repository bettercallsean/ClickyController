using ClickyControllerGUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickyControllerGUI.ViewModels
{
    public class CommandViewModel : BaseViewModel
    {
        private Command _command = new Command();

        public string GetCommandMethod
        {
            get => _command.Method;
            set { _command.Method = value;  OnPropertyChanged(); }
        }
        public string GetCommandNamespace
        {
            get => _command.Namespace;
            set { _command.Namespace = value; OnPropertyChanged(); }
        }
        public string GetCommandParameters
        {
            get => _command.Parameters;
            set { _command.Parameters = value; OnPropertyChanged(); }
        }
    }
}
