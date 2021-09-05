﻿using CD2C.Common;
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
    [ValueConversion(typeof(Dictionary<string, TypeEnum>), typeof(string))]
    public class DictionaryConverter : IValueConverter
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
    }
}