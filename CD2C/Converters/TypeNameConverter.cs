using CD2C.Common;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace CD2C.Converters
{
    /// <summary>
    [ValueConversion(typeof(TypeEnum), typeof(string))]
    public class TypeNameConverter : MarkupExtension, IValueConverter
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

        public override object ProvideValue(System.IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
