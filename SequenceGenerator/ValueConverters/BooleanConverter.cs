using System;
using System.Collections.Generic;
using System.Globalization;

namespace SequenceGenerator.ValueConverters
{
    public class BooleanConverter<T> : ConverterBase
    {
        public T True { get; set; }
        public T False { get; set; } 

        public BooleanConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b && b ? True : False;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T t && EqualityComparer<T>.Default.Equals(t, True);
        }
    }
}
