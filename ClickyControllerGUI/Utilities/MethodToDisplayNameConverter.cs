﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ClickyControllerGUI.Utilities
{
    public class MethodToDisplayNameConverter : IValueConverter
    {
        private readonly static Dictionary<string, string> MethodToDisplayNameDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Resources.MethodToListDisplayName);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MethodToDisplayNameDictionary[value.ToString()];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
