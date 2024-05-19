using SimulatorClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SimulatorClient.ViewModel.Converters
{
    internal class ComparisonConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("CONVER parameter: " + parameter + " value: " + value);
            return ((TeleComparison)parameter == (TeleComparison)value);
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("CONVERT BACK parameter: " + parameter + " value: " + value);
            return (bool)value ? parameter : Binding.DoNothing;
        }
    }
}
