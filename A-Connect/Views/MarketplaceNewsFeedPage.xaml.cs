using A_Connect.Services;
using A_Connect.ViewModels;
using A_Connect.Models;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Collections.Generic;

namespace A_Connect.Views
{
    public partial class MarketplaceNewsFeedPage : ContentPage
    {
        private MarketplaceNewsFeedViewModel viewModel;

        public MarketplaceNewsFeedPage()
        {
            InitializeComponent();
            var database = App.MarketplaceDB;
            viewModel = new MarketplaceNewsFeedViewModel(database);
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadItems();
        }

        private async void OnPostListingClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(MarketplaceFormPage));
        }

        async void OnListingTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is MarketplacePost post)
            {
                var navigationParameter = new Dictionary<string, object>
        {
            { "SelectedPost", post }
        };

                await Shell.Current.GoToAsync($"{nameof(MarketplaceListingDetailPage)}", navigationParameter);
            }
        }
        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            // The SearchText property is already bound with Mode=TwoWay
            // The ViewModel will handle filtering when the property changes
            // No additional code needed here
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the HomePage
            await Shell.Current.GoToAsync("//HomePage");
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