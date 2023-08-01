﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cryptocurrencies.Helpers;

public class NullToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is null
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return (value is Visibility visibility ? visibility : Visibility.Visible) == Visibility.Visible
            ? value
            : null;
    }
}