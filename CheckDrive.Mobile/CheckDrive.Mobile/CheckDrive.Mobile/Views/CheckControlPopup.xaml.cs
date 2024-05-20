using CheckDrive.Mobile.ViewModels;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckDrive.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckControlPopup : ContentPage
	{
        public CheckControlPopup ()
        {
			InitializeComponent (); 
            var viewModel = new RoadMapViewModel();
            BindingContext = viewModel;
        }
        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation?.PopModalAsync();
        }
        private async void OkButton_Clicked(object sender, EventArgs e)
        {
            await Navigation?.PopModalAsync();
        }
    }
}