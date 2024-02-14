using SimulatorClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace SimulatorClient.ViewModel
{
    public class TeleParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var teleParameter = BuildTeleParameter(values);
            var comparisonMessage = teleParameter.Comparison == TeleComparison.BIGGER ? "bigger than" : "smaller than";
            return string.Format("{0} value: {1} {2}", teleParameter.Name, comparisonMessage, teleParameter.Value);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        private TeleParameter BuildTeleParameter(object[] values)
        {
            try
            {
                return new TeleParameter()
                {
                    Name = (string)values[0],
                    Value = (int)values[1],
                    Comparison = (TeleComparison)values[2]
                };
            }
            catch (InvalidCastException ) 
            {
                throw new InvalidCastException("Unable to cast binded values to TeleParameter");
            }
        }
    }
}
