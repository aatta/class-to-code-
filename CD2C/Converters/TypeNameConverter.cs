using CD2C.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace DemoApp.Converters
{
    /// <summary>
    [ValueConversion(typeof(TypeEnum), typeof(string))]
    public class TypeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var dm = (TypeEnum)Enum.Parse(typeof(TypeEnum), value.ToString());
            
            return dm.ToString().Replace("Type", string.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
