﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ActivityPlannerApp.Core.Converters
{
    public class ColorToSolidColorBrushConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return new SolidColorBrush(color);
            }

            return null;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush solidColorBrush)
            {
                return solidColorBrush.Color;
            }

            return null;
        }
    }
}
