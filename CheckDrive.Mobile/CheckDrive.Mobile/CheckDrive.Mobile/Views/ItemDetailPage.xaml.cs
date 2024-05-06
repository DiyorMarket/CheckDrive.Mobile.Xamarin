using CheckDrive.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CheckDrive.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}