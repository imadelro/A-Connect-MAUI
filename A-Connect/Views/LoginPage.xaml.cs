using A_Connect.Services;
using A_Connect.Models;

namespace A_Connect.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly UserDatabase _userDatabase;

        
        public LoginPage(UserDatabase userDatabase)
        {
            InitializeComponent();
            _userDatabase = userDatabase;
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            // back buton
            await Shell.Current.GoToAsync("..");
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = IDNum.Text?.Trim();
            string password = passwordEntry.Text;

         
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both ID number and password.", "OK");
                return;
            }

            // check ur creds
            var user = await _userDatabase.GetUserAsync(username, password);
            if (user != null)
            {
                
                await DisplayAlert("Success", "Logged in successfully", "OK");
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                // invalid credentials
                await DisplayAlert("Error", "Invalid credentials.", "OK");
            }
        }

        
 
    }
}
