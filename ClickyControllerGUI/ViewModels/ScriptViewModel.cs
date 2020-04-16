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

        public List<Command> ScriptReader(string filepath)
        {
            try
            {
                List<Command> commandList = JsonConvert.DeserializeObject<List<Command>>(File.ReadAllText(filepath));
                return commandList;
            }

            catch (JsonSerializationException)
            {
                MessageBox.Show("Error reading script!");
                return null;
            }
            

        }

        public void ScriptWriter(List<Command> commandList, string filepath)
        {
            using StreamWriter file = new StreamWriter(filepath);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, commandList);
            
        }

        public void Run(List<Command> commandList)
        {
            foreach(Command command in commandList)
            {
                command.Execute();
            }

        }
    }
}
