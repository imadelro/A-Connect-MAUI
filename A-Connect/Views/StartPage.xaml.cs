using Microsoft.Maui.Controls;
using System;

namespace A_Connect.Views
{
    public partial class StartPage : ContentPage  // Must be partial
    {
        public StartPage()
        {
             InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("LoginPage");
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("RegisterPage");
        }
        private async void OnScheduleTradingClicked(object sender, EventArgs e)
        {
            // Navigate to ScheduleTradingPage by route name
            await Shell.Current.GoToAsync(nameof(ScheduleTradingPage));
        }
    }
}
