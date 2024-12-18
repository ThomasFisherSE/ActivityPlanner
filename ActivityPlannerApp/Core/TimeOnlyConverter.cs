using System.Globalization;
using System.Windows.Data;

namespace ActivityPlannerApp.Core
{
    public class TimeOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is DateTime dateTime ? dateTime.ToString("HH:mm") : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.TryParseExact((string)value, "HH:mm", culture, DateTimeStyles.None, out DateTime dateTime) ? dateTime : value;
        }
    }
}
