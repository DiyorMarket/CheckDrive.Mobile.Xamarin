using CheckDrive.Mobile.Models;
using System;
using Xamarin.Forms;

namespace CheckDrive.Mobile.Converter
{
    public class IconStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((Enum)value)
            {
                case Status.Completed : return "icon_check.png";
                case Status.Pending : return "icon_circle.png";
                case Status.Rejected : return "icon_faild.png";
                case Status.Unassigned : return "icon_circle.png";
                 default: return "icon_circle.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
