using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClickyController;
using ClickyControllerGUI.Models;
using ClickyControllerGUI.ViewModels;
using GongSolutions.Wpf.DragDrop;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace ClickyControllerGUI.ViewModels
{
    public class ScriptExecutor : BaseViewModel
    {
        //private static readonly Dictionary<string, string[]> CommandToMethodDictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>> (Resources.CommandsToMethods);

        public ScriptExecutor()
        {
            CommandListItems = JsonConvert.DeserializeObject<Dictionary<string, Command>>(Resources.MethodToCommand);
            CommandList = new ObservableCollection<Command>();
        }

        private Dictionary<string, Command> _commandListItems;
        public Dictionary<string, Command> CommandListItems
        {
            get => _commandListItems;
            set { _commandListItems = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Command> _commandList;
        public ObservableCollection<Command> CommandList
        {
            get => _commandList;
            set { _commandList = value; OnPropertyChanged(); }
        }

        private Command _selectedCommand;
        public Command SelectedCommand
        {
            get => _selectedCommand;
            set { _selectedCommand = value; OnPropertyChanged(); }
        }

        private int _selectedCommandIndex;
        public int SelectedCommandIndex
        {
            get => _selectedCommandIndex;
            set { _selectedCommandIndex = value; OnPropertyChanged(); }
        }

        public ICommand AddItemToCommandListCommand { get => new RelayCommand(o => CommandList.Add(SelectedCommand)); }
        public ICommand RemoveItemFromCommandListCommand { get => new RelayCommand(o => RemoveItemFromCommandList()); }
        public ICommand RunScriptCommand { get => new RelayCommand(o => ScriptRunner()); }
        public ICommand ImportScriptCommand { get => new RelayCommand(o => ScriptReader()); }
        public ICommand SaveScriptCommand { get => new RelayCommand(o => ScriptWriter()); }

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


        public void ScriptReader()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                    Title = "Import a Clicky Controller script"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    List<Command> commandList = JsonConvert.DeserializeObject<List<Command>>(File.ReadAllText(openFileDialog.FileName));
                    CommandList = new ObservableCollection<Command>(commandList);
                }
                    
            }

            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

            }

        }

        public void ScriptRunner()
        {
            foreach (Command command in CommandList)
            {
                Type type = Type.GetType("ClickyControllerGUI.ViewModels." + command.Namespace); ;
                MethodInfo method = type.GetMethod(command.Method);
                method.Invoke(null, new object[] { command.Parameters });
            }
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
                using StreamWriter file = new StreamWriter(saveFileDialog.FileName);
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, CommandList);
            }
        }

            
    }
}
