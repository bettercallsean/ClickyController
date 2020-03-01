using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using ClickyControllerGUI.Models;

namespace ClickyControllerGUI.Utilities
{
    class ParameterToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Command)value).Parameters != null ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Command)value).Parameters == null ? Visibility.Hidden : (object)Visibility.Visible;
        }
    }
}
