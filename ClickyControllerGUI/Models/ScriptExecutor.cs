using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ClickyControllerGUI.Models
{
    public class ScriptExecutor
    {
        public ScriptExecutor()
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

        public void ScriptRunner(List<Command> commandList)
        {
            foreach (Command command in commandList)
            {
                Type type = Type.GetType("ClickyControllerGUI.Models.Controller"); ;
                MethodInfo method = type.GetMethod(command.Method);
                object classObject = Activator.CreateInstance(type);

                if (method.GetParameters().Length > 0)
                    method.Invoke(classObject, new object[] { command.Parameters });
                else
                    method.Invoke(classObject, null);
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
