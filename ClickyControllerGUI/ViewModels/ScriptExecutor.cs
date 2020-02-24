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

        private void RemoveItemFromCommandList()
        {
            if (CommandList.Count > 0)
            {
                // Used to set the selected item index back to where it was after the item has been deleted
                int selectionIndex = SelectedCommandIndex;

                CommandList.RemoveAt(SelectedCommandIndex);

                if (selectionIndex > CommandList.Count)
                    SelectedCommandIndex = CommandList.Count - 1;
                else
                    SelectedCommandIndex = selectionIndex;
            }
        }


        public static string[] ScriptReader(string scriptFilepath)
        {
            try
            {
                var commandArray = File.ReadAllLines(scriptFilepath);
                return commandArray;
            }

            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

                return null;
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
            // TODO: Create logic for writing script files in a way that can be easily read by the ScriptReader method, this will be easier when the interface is designed
        }

            
    }
}
