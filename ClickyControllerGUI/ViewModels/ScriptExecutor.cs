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
using ClickyControllerGUI.ViewModels;
using GongSolutions.Wpf.DragDrop;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace ClickyControllerGUI.ViewModels
{
    public class ScriptExecutor : BaseViewModel
    {
        private static readonly Dictionary<string, string[]> CommandToMethodDictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>> (Resources.CommandsToMethods);

        public ScriptExecutor()
        {
            CommandListItems = JsonConvert.DeserializeObject<Dictionary<string, string>>(Resources.CommandsToListDisplayName);

            CommandList = new ObservableCollection<string>();
        }

        private Dictionary<string, string> _commandListItems;
        public Dictionary<string, string> CommandListItems
        {
            get => _commandListItems;
            set { _commandListItems = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _commandList;
        public ObservableCollection<string> CommandList
        {
            get => _commandList;
            set { _commandList = value; OnPropertyChanged(); }
        }

        private string _selectedCommand;
        public string SelectedCommand
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
                    Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    Title = "Import a Clicky Controller script"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    List<string> commandList = File.ReadAllLines(openFileDialog.FileName).ToList();
                    CommandList = new ObservableCollection<string>(commandList);
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
            foreach (string line in CommandList)
            {
                /*
                 * inputType - This will either be 'mouse' or 'keybd' and is found at the start of each script line
                 * command - Contains the command key that will map to a method found in the corresponding ViewModel
                 * parameters - The rest of the string that is passed to the respective method, where the input is cleaned up for processing
                 */

                string command = line.Substring(0, 2);
                string parameters = line.Substring(3);
                string namespaceString = CommandToMethodDictionary[command][0];
                
                Type type = Type.GetType("ClickyControllerGUI.ViewModels." + namespaceString);
                MethodInfo method = type.GetMethod(CommandToMethodDictionary[command][1]);
                method.Invoke(null, new object[] { parameters });
            }
        }




        public void ScriptWriter()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "script",
                Filter = "Text file (*.txt)|*.txt|All Files(*.*)|*.*",
                Title = "Save a script"
            };

            if (saveFileDialog.ShowDialog() == true && saveFileDialog.FileName != "")
            {
                using StreamWriter file = new StreamWriter(saveFileDialog.FileName);
                foreach (string line in CommandList)
                    file.WriteLine(line);
            }
        }

            
    }
}
