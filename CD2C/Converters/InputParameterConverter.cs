using CD2C.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace CD2C.Converters
{
    /// <summary>
    [ValueConversion(typeof(ObservableCollection<InputParameterModel>), typeof(string))]
    public class InputParameterConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ObservableCollection<InputParameterModel>))
            {
                return string.Empty;
            }

            var dm = value as ObservableCollection<InputParameterModel>;
            var rows = dm.Select(kv => string.Format("{0} {1}", kv.Name, kv.InputType.ToString().Replace("Type", string.Empty))).ToList();

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
