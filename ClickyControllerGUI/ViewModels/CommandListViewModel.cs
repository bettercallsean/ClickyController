using ClickyControllerGUI.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Reflection;

namespace ClickyControllerGUI.ViewModels
{
    public class CommandListViewModel : BaseViewModel
    {
        private readonly ScriptViewModel _script = new ScriptViewModel();

        public CommandListViewModel()
        {
            CommandList = new ObservableCollection<CommandViewModel>();
            CommandListOptions = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(Resources.DisplayNameToMethod);
        }

        private Dictionary<string, Dictionary<string, string>> _commandListOptions;
        public Dictionary<string, Dictionary<string, string>> CommandListOptions
        {
            get => _commandListOptions;
            set { _commandListOptions = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CommandViewModel> _commandList;
        public  ObservableCollection<CommandViewModel> CommandList
        {
            get => _commandList;
            set { _commandList = value; OnPropertyChanged(); }
        }

        private int _selectedCommandIndex;
        public int SelectedCommandIndex
        {
            get => _selectedCommandIndex;
            set { _selectedCommandIndex = value; OnPropertyChanged(); }
        }

        public ICommand AddItemToListCommand => new RelayCommand(o => AddItemToCommandList(o));
        public ICommand RemoveItemFromCommandListCommand => new RelayCommand(o => RemoveItemFromCommandList(o));

        private void AddItemToCommandList(object commandType)
        {
            Type objectType = Type.GetType("ClickyControllerGUI.ViewModels." + commandType.ToString() + "ViewModel, ClickyControllerGUI");
            CommandViewModel command = (CommandViewModel)Activator.CreateInstance(objectType);
            command.Type = commandType.ToString();
            
            CommandList.Add(command);
            MessageBox.Show(command.Type);
        }

        private void RemoveItemFromCommandList(object command)
        {
            if (CommandList.Count <= 0) return;
            // Used to set the selected item index back to where it was after the item has been deleted
            int selectionIndex = SelectedCommandIndex;

            CommandList.Remove((CommandViewModel)command);
        
            // If the last item in the list is deleted and sets SelectedCommandIndex to the one below,
            // otherwise, it can be set to the same position again.
            // selectionIndex is used because SelectionCommandIndex is set to -1 after removing the element
            if (selectionIndex >= CommandList.Count && CommandList.Count != 0)
                SelectedCommandIndex = selectionIndex - 1;
            else
                SelectedCommandIndex = selectionIndex;
        }

        public ICommand RunScriptCommand => new RelayCommand(o => ScriptRunner());
        public ICommand ImportScriptCommand => new RelayCommand(o => ScriptReader());
        public ICommand SaveScriptCommand => new RelayCommand(o => ScriptWriter());

        private void ScriptReader()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                Title = "Import a Clicky Controller script"
            };

            if (openFileDialog.ShowDialog() != true) return;

            List<CommandViewModel> commandList = _script.ScriptReader(openFileDialog.FileName);

            if (commandList == null) return;

            CommandList = new ObservableCollection<CommandViewModel>(commandList);
        }

        private void ScriptRunner()
        {
            _script.Run(CommandList.ToList());
        }


        private void ScriptWriter()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "script",
                Filter = "JSON file (*.json)|*.json|All Files(*.*)|*.*",
                Title = "Save a script"
            };

            if (saveFileDialog.ShowDialog() == true && saveFileDialog.FileName != "")
            {
                _script.ScriptWriter(CommandList.ToList(), saveFileDialog.FileName);
            }
        }

    }
}
