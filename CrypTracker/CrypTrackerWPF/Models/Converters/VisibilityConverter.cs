using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CrypTrackerWPF.Models.Converters;

public sealed class VisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isVisible)
        {
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => value;
}