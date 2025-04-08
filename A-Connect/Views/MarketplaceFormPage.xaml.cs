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
                // Bind the form inputs to the ViewModel properties
                viewModel.ListingTitle = ListingTitleEntry.Text;
                viewModel.Category = CategoryPicker.SelectedItem?.ToString();
                viewModel.Condition = ConditionPicker.SelectedItem?.ToString();
                viewModel.ContactDetails = ContactDetailsEntry.Text;
                viewModel.Description = AdditionalInfoEditor.Text;

                // Call the SubmitListing method
                await viewModel.SubmitListing();
            }
            else
            {
                await DisplayAlert("Error", "There was an issue with your submission.", "OK");
            }
        }

    }
}
