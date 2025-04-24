using Microsoft.Maui.Controls;
using A_Connect.Services;

namespace A_Connect.Views
{
    public partial class AccountDetailsPage : ContentPage
    {
        private readonly UserDatabase _userDatabase;

        public AccountDetailsPage(UserDatabase userDatabase)
        {
            InitializeComponent();
            _userDatabase = userDatabase;

            emailLabel.Text = App.CurrentUser?.Email;
            idNumberLabel.Text = App.CurrentUser?.Username;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            string displayName = usernameEntry.Text;
            string email = emailLabel.Text;

            // Check if the display name already exists
            var existingUser = await _userDatabase.GetUserByDisplayNameAsync(displayName);

            if (existingUser != null)
            {
                await DisplayAlert("Error", "Display name is already taken", "OK");
                return;
            }

            // Check if the display name is valid (e.g., not empty)
            if (string.IsNullOrWhiteSpace(displayName))
            {
                await DisplayAlert("Error", "Display name cannot be empty", "OK");
                return;
            }

            // Proceed to update the display name in the database
            int rows = await _userDatabase.UpdateDisplayNameAsync(email, displayName);

            if (rows > 0)
            {
                await DisplayAlert("Success", "Display name updated successfully!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to update display name.", "OK");
            }
        }


    }
}
