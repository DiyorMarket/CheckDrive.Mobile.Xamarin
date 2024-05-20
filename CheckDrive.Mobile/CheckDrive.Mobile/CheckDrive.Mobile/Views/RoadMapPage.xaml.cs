using CheckDrive.Mobile.ViewModels;
using Syncfusion.XForms.PopupLayout;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckDrive.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoadMapPage : ContentPage
    {
        SfPopupLayout popupLayout;
        private RoadMapViewModel roadMapViewModel;

        public RoadMapPage()
        {
            InitializeComponent();
            BindingContext = roadMapViewModel = new RoadMapViewModel();
        }
        private async void ClickToShowPopup_Clicked(object sender, EventArgs e)
        {
            var popup = new CheckControlPopup()
            {
                BindingContext = BindingContext
            };

            await Navigation.PushModalAsync(popup);
        }
    }
}
