using ClickyControllerGUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace ClickyControllerGUI.ViewModels
{
    public class ScriptViewModel
    {
        public ScriptViewModel()
        {

        }

        public List<CommandViewModel> ScriptReader(string filepath)
        {
            try
            {
                List<CommandViewModel> commandList = JsonConvert.DeserializeObject<List<CommandViewModel>>(File.ReadAllText(filepath));
                return commandList;
            }

            catch (JsonSerializationException)
            {
                MessageBox.Show("Error reading script!");
                return null;
            }
            

        }

        public void ScriptWriter(List<CommandViewModel> commandList, string filepath)
        {
            using StreamWriter file = new StreamWriter(filepath);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, commandList);
            
        }

        public void Run(List<CommandViewModel> commandList)
        {
            foreach(CommandViewModel command in commandList)
            {
                command.Execute();
            }

        }
    }
}
