using ClickyControllerGUI.Models;
using ClickyControllerGUI.Utilities;
using Microsoft.Win32;
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

        public List<CommandViewModel> ScriptReader()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                    Title = "Import a Clicky Controller script"
                };

                if (openFileDialog.ShowDialog() != true) 
                    return null;

                // Implemented a custom converter because CommandViewModel is an abstract class and doesn't play nice with the Deserializer
                JsonConverter[] converters = { new CommandViewModelConverter() };

                List<CommandViewModel> commandList = JsonConvert.DeserializeObject<List<CommandViewModel>>(File.ReadAllText(openFileDialog.FileName), new JsonSerializerSettings
                {
                    Converters = converters
                });
                return commandList;
            }

            catch (JsonSerializationException)
            {
                MessageBox.Show("Error reading script!");
                return null;
            }

        }

        public void ScriptWriter(List<CommandViewModel> commandList)
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
                serializer.Serialize(file, commandList);
            }

            
            
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
