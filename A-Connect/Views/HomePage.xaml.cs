using Microsoft.Maui.Controls;

namespace A_Connect.Views
{
    public partial class Homepage : ContentPage
    {
        public Homepage()
        {
            InitializeComponent();
            string username = App.CurrentUser?.Username ?? "Guest";
            WelcomeLabel.Text = $"Welcome, {username}!";
        }

        private async void OnScheduleTradingClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ScheduleTradingNewsFeedPage");
        }

        private async void OnProfsToPickClicked(object sender, EventArgs e)
        {
            // Navigate to the Profs to Pick News Feed page
            // Make sure the route "ProfsToPickNewsFeedPage" is registered in AppShell.
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
            await Shell.Current.GoToAsync(nameof(MarketplaceNewsFeedPage));
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");

            if (confirm)
            {
                // Clear user session data
                App.CurrentUser = null;

                // Remove any stored credentials or tokens
                Preferences.Remove("Username");
                Preferences.Remove("IsLoggedIn");

                // Navigate back to login page
                await Shell.Current.GoToAsync("StartPage");
            }
        }
    }
}
