using System;
using Xamarin.Forms;

namespace CheckDrive.Mobile.Converter
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool isPressed && isPressed)
            {
                return Color.Gray; // Change to the color you want when the button is pressed
            }
            return Color.FromHex("#00A3FF"); // Default color
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

