using System;
using System.Globalization;
using System.Windows.Data;

namespace SequenceGenerator.ValueConverters
{
    public abstract class ConverterBase : IValueConverter
    {
        public abstract object? Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public virtual object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"Converter '{GetType().Name}' does not support backward conversion");
        }
    }
}
