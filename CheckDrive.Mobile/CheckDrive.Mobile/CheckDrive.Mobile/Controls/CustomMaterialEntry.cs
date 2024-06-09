using Xamarin.Forms;

namespace CheckDrive.Mobile.Controls
{
    public class CustomMaterialEntry : Plugin.MaterialDesignControls.Material3.MaterialEntry
    {
        public static readonly BindableProperty TrailingIconWidthRequestProperty =
            BindableProperty.Create(nameof(TrailingIconWidthRequest), typeof(double), typeof(CustomMaterialEntry), 24.0);

        public static readonly BindableProperty TrailingIconHeightRequestProperty =
            BindableProperty.Create(nameof(TrailingIconHeightRequest), typeof(double), typeof(CustomMaterialEntry), 24.0);

        public double TrailingIconWidthRequest
        {
            get => (double)GetValue(TrailingIconWidthRequestProperty);
            set => SetValue(TrailingIconWidthRequestProperty, value);
        }

        public double TrailingIconHeightRequest
        {
            get => (double)GetValue(TrailingIconHeightRequestProperty);
            set => SetValue(TrailingIconHeightRequestProperty, value);
        }
    }
}
