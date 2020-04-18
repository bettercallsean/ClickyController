using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClickyControllerGUI.Views.CommandViews
{
    /// <summary>
    /// Interaction logic for KeyboardCharacterInputView.xaml
    /// </summary>
    public partial class KeyboardCharacterInputView : Window
    {
        private readonly Dictionary<string, string> WindowsKeyToButtonDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(ClickyControllerGUI.Resources.WindowsKeysToButton);
        public KeyboardCharacterInputView()
        {
            InitializeComponent();
        }

        private void KeyPress_KeyDown(object sender, KeyEventArgs e)
        {

            string character = "";
            if (string.IsNullOrWhiteSpace(KeyPress.Text))
                character = KeyPress.Text;
            
            
            if (e.Key.ToString().Length > 1)
            {
                e.Handled = true;
                KeyPress.Text = WindowsKeyToButtonDictionary[e.Key.ToString()];
            }
            else
            {
                KeyPress.Text = character;
            }
        }
    }
}
