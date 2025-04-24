using Microsoft.Maui.Controls;
using A_Connect.ViewModels;

namespace A_Connect.Views
{
    public partial class ProfsToPickNewsFeedPage : ContentPage
    {
        public ProfsToPickNewsFeedPage()
        {
            InitializeComponent();
            BindingContext = new ProfsToPickNewsFeedViewModel(App.ReviewDatabase);
        }

        private async void OnPostReviewClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfsToPickFormPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is ProfsToPickNewsFeedViewModel vm)
            {
                vm.LoadReviewsCommand.Execute(null);
            }
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the HomePage
            await Shell.Current.GoToAsync("//HomePage");
        }

        private async void OnNavIconClicked(object sender, EventArgs e)
{
    // Toggle visibility
    Menu_container.IsVisible = !Menu_container.IsVisible;
    await Menu_container.TranslateTo(0, 0, 250, Easing.CubicOut);
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


        private async void OnAccountClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AccountDetailsPage));
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

    }
}
