using System;
using System.Globalization;
using Xamarin.Forms;

namespace CheckDrive.Mobile.Converter
{
    public class BoolToRedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolean && boolean)
            {
                return Color.Red;
            }
            return Color.Default; // Default color or specify another color
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
