using ClickyControllerGUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ClickyControllerGUI.ViewModels
{
    public class CommandOptionsViewModel : BaseViewModel
    {
        public CommandOptionsViewModel()
        {
            CommandListOptions = JsonConvert.DeserializeObject<Dictionary<string, ObservableCollection<Command>>>(Resources.DisplayNameToMethod);
        }

        private Dictionary<string, ObservableCollection<Command>> _commandListOptions;
        public Dictionary<string, ObservableCollection<Command>> CommandListOptions
        {
            get => _commandListOptions;
            set { _commandListOptions = value; OnPropertyChanged(); }
        }
    }
}
