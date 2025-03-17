using A_Connect.ViewModels;
using A_Connect.Services;
using Microsoft.Maui.Controls;

namespace A_Connect.Views
{
    public partial class ProfsToPickFormPage : ContentPage
    {
        public ProfsToPickFormPage()
        {
            InitializeComponent();
            BindingContext = new ProfsToPickFormViewModel(App.ReviewDatabase);
        }
    }
}
