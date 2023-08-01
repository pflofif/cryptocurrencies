using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Cryptocurrencies.Helpers;

public class ChangePercentColorConverter : IValueConverter
{
    private const string PositiveColorBackground = "#2ebd85";
    private const string NegativeColorBackground = "#f6465d";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string changePercent)
        {
            return changePercent[0] is '+'
                ? PositiveColorBackground
                : NegativeColorBackground;
        }

        return Brushes.White;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string colorString)
        {
            return colorString is PositiveColorBackground ? "+" : "-";
        }
        return "+";
    }
}