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
using ClickyControllerGUI.Utilities;
using ClickyControllerGUI.Views.CommandViews;

namespace ClickyControllerGUI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ScriptViewModel _script = new ScriptViewModel();

        public MainViewModel()
        {
            CommandList = new ObservableCollection<CommandViewModel>();
            CommandListOptions = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(Resources.DisplayNameToViewModel);
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

        private bool _changesMadeToScript;
        public bool ChangesMadeToScript
        {
            get => _changesMadeToScript;
            set { _changesMadeToScript = value; OnPropertyChanged(); }
        }

        public ICommand AddItemToListCommand => new RelayCommand(o => AddItemToCommandList(o));
        

        private void AddItemToCommandList(object commandType)
        {
            Type objectType = Type.GetType("ClickyControllerGUI.ViewModels." + commandType.ToString() + ", ClickyControllerGUI");
            CommandViewModel command = (CommandViewModel)Activator.CreateInstance(objectType);
            command.ViewModel = commandType.ToString();

            CommandList.Add(command);
            ChangesMadeToScript = true;

            EditCommandInfo(command);
        }

        public ICommand RemoveItemFromCommandListCommand => new RelayCommand(o => RemoveItemFromCommandList(o));
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

            ChangesMadeToScript = true;
        }

        public ICommand NewCommandListCommand => new RelayCommand(o => NewCommandList());
        public void NewCommandList()
        {
            if(ChangesMadeToScript)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save changes to your script?", "Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                    SaveScript();

                else if (result == MessageBoxResult.Cancel)
                    return;
            }

            CommandList = new ObservableCollection<CommandViewModel>();
            ChangesMadeToScript = false;
        }


        // I absolutely know that the code I have done below is an absolute crime against MVVM,
        // but I've spent about 8 hours trying to get dialog boxes to open and everything I've
        // tried either doesn't work or requires code thats as long as the entire works of Shakespeare.
        // Please forgive me
        public ICommand EditCommandInfoCommand => new RelayCommand(o => EditCommandInfo(o));
        public void EditCommandInfo(object commandToEdit)
        { 
            CommandViewModel cvm = (CommandViewModel)commandToEdit;
            Type commandView = Type.GetType("ClickyControllerGUI.Views.CommandViews." + cvm.View + ", ClickyControllerGUI");

            Window view = (Window)Activator.CreateInstance(commandView);
            view.DataContext = cvm;

            view.ShowDialog();
        }

        public ICommand RunScriptCommand => new RelayCommand(o => _script.Run(CommandList.ToList()));
        public ICommand ImportScriptCommand => new RelayCommand(o => ScriptReader());
        public ICommand SaveScriptCommand => new RelayCommand(o => SaveScript());

        private void ScriptReader()
        {
            List<CommandViewModel> commandList = _script.ScriptReader();

            if (commandList == null) return;

            CommandList = new ObservableCollection<CommandViewModel>(commandList);
        }

        private void SaveScript()
        {
            _script.ScriptWriter(CommandList.ToList());
            ChangesMadeToScript = false;
        }


    }
}
