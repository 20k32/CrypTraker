using System;
using System.Globalization;
using System.Windows.Data;
using CrypTrackerWPF.Models.LocalizationExtensions;

namespace CrypTrackerWPF.Models.Converters;

public sealed class CurrencyConverter : IValueConverter
{
    private string TrimZeroes(string value)
    {
        string result;
        int i;
        for (i = 0; i < value.Length; i++)
        {
            if (value[i] == '.')
            {
                break;
            }
        }

        result = value.Remove(i + 7);
        return result;
    }
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal price)
        {
            return TrimZeroes(price.ToString());
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => value;
}