using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ClickyController;
using ClickyControllerGUI.ViewModels;
using Newtonsoft.Json;

namespace ClickyControllerGUI.ViewModels
{
    public class ScriptExecutor : BaseViewModel
    {
        private static readonly Dictionary<string, string> CommandToNamespaceDictionary = new Dictionary<string, string> { { "mouse", "MouseViewModel" }, { "keybd", "KeyboardViewModel" } };
        private static readonly Dictionary<string, Dictionary<string, string>> CommandToMethodDictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>> (Resources.CommandsToMethods);

        public ScriptExecutor()
        {
            CommandList = new List<string>
            {
                "This",
                "is",
                "a",
                "test"
            };
        }

        private List<string> _commandList;
        public List<string> CommandList
        {
            get => _commandList;
            set { _commandList = value; OnPropertyChanged(); }
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

        public static void ScriptRunner(string scriptFilepath)
        {
            string[] commandArray = ScriptReader(scriptFilepath);

            foreach (string line in commandArray)
            {
                /*
                 * inputType - This will either be 'mouse' or 'keybd' and is found at the start of each script line
                 * command - Contains the command key that will map to a method found in the corresponding ViewModel
                 * parameters - The rest of the string that is passed to the respective method, where the input is cleaned up for processing
                 * 
                 */

                string inputType = line.Substring(0, 5);
                string command = line.Substring(6, 2);
                string parameters = line.Substring(9);
                string namespaceString = CommandToNamespaceDictionary[inputType];
                
                Type type = Type.GetType("ClickyControllerGUI.ViewModels." + namespaceString);
                MethodInfo method = type.GetMethod(CommandToMethodDictionary[inputType][command]);
                method.Invoke(null, new object[] { parameters });
            }
        }




        public void ScriptWriter()
        {
            // TODO: Create logic for writing script files in a way that can be easily read by the ScriptReader method, this will be easier when the interface is designed
        }

            
    }
}
