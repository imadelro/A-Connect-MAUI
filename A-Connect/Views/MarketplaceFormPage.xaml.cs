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
                bool success = await viewModel.SubmitPostAsync(
                    ListingTitleEntry.Text,
                    CategoryPicker.SelectedItem?.ToString(),
                    ConditionPicker.SelectedItem?.ToString(),
                    ContactDetailsEntry.Text,
                    AdditionalInfoEditor.Text
                );

                if (success)
                {
                    await DisplayAlert("Success", "Your post has been created!", "OK");
                    Debug.WriteLine("Post created successfully.");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await DisplayAlert("Error", "Please fill in all required fields.", "OK");
                    Debug.WriteLine("Failed to create post. Missing required fields.");
                }
            }
        }
    }
}
