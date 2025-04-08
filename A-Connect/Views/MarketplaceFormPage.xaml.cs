using A_Connect.Services;
using A_Connect.ViewModels;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace A_Connect.Views
{
    public partial class MarketplaceFormPage : ContentPage
    {
        public MarketplaceFormPage(MarketplaceDatabase database)
        {
            InitializeComponent();
            BindingContext = new MarketplaceFormViewModel(database, App.CurrentUser.Username);
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as MarketplaceFormViewModel;
            if (viewModel != null)
            {
                // Call the correct method (SubmitListing instead of SubmitPostAsync)
                await viewModel.SubmitListing();

                Debug.WriteLine("Post created successfully.");
                await DisplayAlert("Success", "Your post has been created!", "OK");
                await Shell.Current.GoToAsync(".."); // Go back to the previous page (NewsFeed)
            }
            else
            {
                await DisplayAlert("Error", "There was an issue with your submission.", "OK");
                Debug.WriteLine("Failed to create post.");
            }
        }
    }
}
