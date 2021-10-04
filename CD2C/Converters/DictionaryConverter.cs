using CD2C.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace CD2C.Converters
{
    /// <summary>
    [ValueConversion(typeof(Dictionary<string, TypeEnum>), typeof(string))]
    public class DictionaryConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Dictionary<string, TypeEnum>))
            {
                return string.Empty;
            }

            var dm = value as Dictionary<string, TypeEnum>;
            var rows = dm.Select(kv => string.Format("{0} {1}", kv.Key, kv.Value.ToString().Replace("Type", string.Empty))).ToList();

            return string.Join(Environment.NewLine, rows);
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
