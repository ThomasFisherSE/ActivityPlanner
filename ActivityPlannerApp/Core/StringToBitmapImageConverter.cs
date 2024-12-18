using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ActivityPlannerApp.Core
{
    public class StringToBitmapImageConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string path && Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out Uri? imageUri)
                ? new BitmapImage(imageUri)
                : null;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
