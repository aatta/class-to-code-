using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DiagramDesigner
{
    [ValueConversion(typeof(MultiplicityTypeEnum), typeof(string))]
    public class MultiplicityTypeConverter : MarkupExtension, IValueConverter
    {
        static MultiplicityTypeConverter()
        {
            Instance = new MultiplicityTypeConverter();
        }

        public static MultiplicityTypeConverter Instance
        {
            get;
            private set;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var dm = (MultiplicityTypeEnum)Enum.Parse(typeof(MultiplicityTypeEnum), value.ToString());

            switch (dm
)
            {
                case MultiplicityTypeEnum.Unspecified:
                    return string.Empty;
                case MultiplicityTypeEnum.Zero:
                    return "0";
                case MultiplicityTypeEnum.ZeroToOne:
                    return "0..1";
                case MultiplicityTypeEnum.ZeroToMany:
                    return "0..*";
                case MultiplicityTypeEnum.One:
                    return "1";
                case MultiplicityTypeEnum.OneToMany:
                    return "1..*";
                case MultiplicityTypeEnum.ManyToMany:
                    return "*";
            }

            return string.Empty;
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
