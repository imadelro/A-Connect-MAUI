using A_Connect.Services;
using A_Connect.Models;
using System.Diagnostics;

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

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = IDNum.Text?.Trim();
            string password = passwordEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both ID number and password.", "OK");
                return;
            }

            var user = await _userDatabase.GetUserAsync(username, password);
            if (user != null)
            {
                App.CurrentUser = user;
                Debug.Write(user.Username);
                Preferences.Set("Username", user.Username);
                Preferences.Set("IsLoggedIn", true);
                await DisplayAlert("Success", "Logged in successfully", "OK");
<<<<<<< HEAD
                await Shell.Current.GoToAsync("//HomePage");
=======
                //await Shell.Current.GoToAsync("//MainPage"); this is where we redirect but unavailable pa rn
                await Shell.Current.GoToAsync("//ScheduleTradingPage");

>>>>>>> 1fd85c8a5cd5e51b8cec819bf599391bd6871904
            }
            else
            {
                await DisplayAlert("Error", "Invalid credentials.", "OK");
            }
        }
    }
}
