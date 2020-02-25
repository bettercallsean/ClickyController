﻿using ClickyControllerGUI.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ClickyControllerGUI.ViewModels
{
    public class CommandViewModel : BaseViewModel
    {
        private Command _command;
        private ScriptExecutor _scriptExecutor = new ScriptExecutor();

        public CommandViewModel()
        {
            CommandListOptions = JsonConvert.DeserializeObject<Dictionary<string, Command>>(Resources.MethodToCommandInfo);
            CommandList = new ObservableCollection<Command>();
        }

        private Dictionary<string, Command> _commandListOptions;
        public Dictionary<string, Command> CommandListOptions
        {
            get => _commandListOptions;
            set { _commandListOptions = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Command> _commandList;
        public  ObservableCollection<Command> CommandList
        {
            get => _commandList;
            set { _commandList = value; OnPropertyChanged(); }
        }

        private Command _commandStructure;
        public Command CommandStructure
        {
            get => _commandStructure;
            set { _commandStructure = value; OnPropertyChanged(); }
        }

        private int _selectedCommandIndex;
        public int SelectedCommandIndex
        {
            get => _selectedCommandIndex;
            set { _selectedCommandIndex = value; OnPropertyChanged(); }
        }

        public ICommand AddItemToListCommand { get => new RelayCommand(o => AddItemToCommandList()); }
        public ICommand RemoveItemFromCommandListCommand { get => new RelayCommand(o => RemoveItemFromCommandList()); }

        private void AddItemToCommandList()
        {
            _command = new Command
            {
                Method = CommandStructure.Method,
                Namespace = CommandStructure.Namespace
            };

            CommandList.Add(_command);
        }

        private void RemoveItemFromCommandList()
        {
            if (CommandList.Count > 0)
            {
                // Used to set the selected item index back to where it was after the item has been deleted
                int selectionIndex = SelectedCommandIndex;
        
                CommandList.RemoveAt(SelectedCommandIndex);
        
                // If the last item in the list is deleted and sets SelectedCommandIndex to the one below,
                // otherwise, it can be set to the same position again.
                // selectionIndex is used because SelectionCommandIndex is set to -1 after removing the element
                if (selectionIndex >= CommandList.Count && CommandList.Count != 0)
                    SelectedCommandIndex = selectionIndex - 1;
                else
                    SelectedCommandIndex = selectionIndex;
            }
        }

        public ICommand RunScriptCommand { get => new RelayCommand(o => ScriptRunner()); }
        public ICommand ImportScriptCommand { get => new RelayCommand(o => ScriptReader()); }
        public ICommand SaveScriptCommand { get => new RelayCommand(o => ScriptWriter()); }

        public void ScriptReader()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                Title = "Import a Clicky Controller script"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                List<Command> commandList = _scriptExecutor.ScriptReader(openFileDialog.FileName);
                CommandList = new ObservableCollection<Command>(commandList);
            }
        }

        public void ScriptRunner()
        {
            _scriptExecutor.ScriptRunner(CommandList.ToList());
        }


        public void ScriptWriter()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "script",
                Filter = "JSON file (*.json)|*.json|All Files(*.*)|*.*",
                Title = "Save a script"
            };

            if (saveFileDialog.ShowDialog() == true && saveFileDialog.FileName != "")
            {
                _scriptExecutor.ScriptWriter(CommandList.ToList(), saveFileDialog.FileName);
            }
        }

    }
}
