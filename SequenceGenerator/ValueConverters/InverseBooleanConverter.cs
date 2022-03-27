using System.Windows;
using System.Windows.Data;

namespace SequenceGenerator.ValueConverters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed)
        {}
    }
}
