using System;
using System.Globalization;
using Xamarin.Forms;

namespace CheckDrive.Mobile.Converters
{
    public class BooleanToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return booleanValue ? "icon_check.png" : "icon_incorrect.png";
            }
            return "icon_incorrect.png"; // Default fallback
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
