using System;
using System.Globalization;
using System.Windows.Data;

namespace CrypTrackerWPF.Models.Converters;

public sealed class TextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string strValue)
        {
            return strValue + ":";
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => value;
}