using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ClickyController;
using ClickyControllerGUI.ViewModels;
using Newtonsoft.Json;

namespace ClickyControllerGUI.Models
{
    public class ScriptExecuter : BaseViewModel
    {

        public ScriptExecuter()
        {

        }

        public string[] ScriptReader(string scriptFilepath)
        {
            string[] commandArray;

            try
            {
                commandArray = File.ReadAllLines(scriptFilepath);
                return commandArray;
            }

            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

                return null;
            }

        }

        public void ScriptRunner(string[] commandArray)
        {
            Dictionary<string, Action<string>> commandDictionary = new Dictionary<string, Action<string>>();
        }




        public void ScriptWriter()
        {
            // TODO: Create logic for writing script files in a way that can be easily read by the ScriptReader method
        }

            
    }
}
