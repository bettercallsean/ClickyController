using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ClickyControllerGUI.Models
{
    public class ScriptReadWriter
    {
        public ScriptReadWriter()
        {

        }

        public List<Command> ScriptReader(string filepath)
        {
            try
            {
                List<Command> commandList = JsonConvert.DeserializeObject<List<Command>>(File.ReadAllText(filepath));
                return commandList;
            }

            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;

            }

        }

        public void ScriptWriter(List<Command> commandList, string filepath)
        {
            using StreamWriter file = new StreamWriter(filepath);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, commandList);
            
        }
    }
}
