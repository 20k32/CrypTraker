using System;
using System.Globalization;
using System.Windows.Data;

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
        if (value is decimal priceM)
        {
            return TrimZeroes(priceM.ToString());
        }
        if (value is string priceStr)
        {
            return TrimZeroes(priceStr);
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => value;
}