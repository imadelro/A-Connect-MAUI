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

    }

}
