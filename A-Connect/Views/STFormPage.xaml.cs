using Microsoft.Maui.Controls;
using A_Connect.ViewModels;

namespace A_Connect.Views
{
    public partial class PostTradePage : ContentPage
    {
        public PostTradePage()
        {
            InitializeComponent();
            BindingContext = new STFormViewModel(App.STDB);
        }
        private async void OnCreatePostClicked(object sender, EventArgs e)
        {
            // Navigate to form page
            await Shell.Current.GoToAsync(nameof(InternNJobsFormPage));
        }
    }
}
