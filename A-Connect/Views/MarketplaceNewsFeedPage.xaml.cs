using A_Connect.Services;
using A_Connect.ViewModels;
using Microsoft.Maui.Controls;

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
    }
}

