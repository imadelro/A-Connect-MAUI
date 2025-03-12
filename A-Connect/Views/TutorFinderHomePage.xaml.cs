using Microsoft.Maui.Controls;
using A_Connect.Services;

namespace A_Connect.Views
{
    public partial class TutorFinderHomePage : ContentPage
    {
        public TutorFinderHomePage()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(".."); // Navigates back
        }
    }
}
