using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace WorkingHours.Converters
{
    public class ObjectPassConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
