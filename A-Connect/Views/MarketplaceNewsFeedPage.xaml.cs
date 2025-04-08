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

        private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var selectedPost = e.CurrentSelection.FirstOrDefault() as MarketplacePost;

            if (selectedPost == null)
                return;

            Debug.WriteLine($"Selected item: {selectedPost.ListingTitle}");

            await Shell.Current.GoToAsync($"{nameof(MarketplaceListingDetailPage)}?ListingId={selectedPost.Id}");

            // Reset selection so same item can be tapped again
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
