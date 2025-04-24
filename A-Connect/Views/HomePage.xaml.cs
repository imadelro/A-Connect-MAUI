using System.Diagnostics;
using Microsoft.Maui.Controls;
using A_Connect.ViewModels;
using System.Windows.Input;

namespace A_Connect.Views
{
    public partial class Homepage : ContentPage
    {

        public Homepage()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            string username = App.CurrentUser?.Username ?? "Guest";

            WelcomeLabel.Text = $"Welcome, {username}!";
            BindingContext = new HomepageViewModel();

            string displayName = App.CurrentUser?.DisplayName;


            if (displayName == null)
            {
                WelcomeLabel.Text = $"Welcome, {username}!";
            } else
            {
                WelcomeLabel.Text = $"Welcome, {displayName}!";
            }

            
        }



        private async void OnScheduleTradingClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ScheduleTradingNewsFeedPage");
        }

        private async void OnProfsToPickClicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync("ProfsToPickNewsFeedPage");
        }

        private async void OnTutorFinderClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("TutorFinderPage");
        }

        private async void OnInternNJobsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("InternNJobsNewsfeedPage");
        }

        private async void OnMarketplaceClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("MarketplaceNewsFeedPage");
        }
        private async void OnNavIconClicked(object sender, EventArgs e)
{
    // Toggle visibility
    Menu_container.IsVisible = !Menu_container.IsVisible;
    await Menu_container.TranslateTo(0, 0, 250, Easing.CubicOut);
}

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");

            if (confirm)
            {

                // clear 
                App.CurrentUser = null;
                // Remove  stored credentials

                Preferences.Remove("Username");
                Preferences.Remove("IsLoggedIn");
                await Shell.Current.GoToAsync("StartPage");
            }
        }

        private async void OnAccountClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AccountDetailsPage));
        }
    }
}