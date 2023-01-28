using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Converter
{
    public class FalseToTrueConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return false; //Visibility.Collapsed;
            }
            else
            {
                return true;
            }
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return false; //Visibility.Collapsed;
            }
            else
            {
                return true;
            }
        }
    }
}
