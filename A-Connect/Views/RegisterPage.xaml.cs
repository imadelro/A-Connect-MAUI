using A_Connect.Services;
using A_Connect.Models;

namespace A_Connect.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly UserDatabase _userDatabase;


        public RegisterPage(UserDatabase userDatabase)
        {
            InitializeComponent();
            _userDatabase = userDatabase;
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            // back buton
            await Shell.Current.GoToAsync("..");
        }


        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string username = emailIDnum.Text?.Trim();
            string password = passwordEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both email and password.", "OK");
                return;
            }

            // check if user already exists
            var existingUser = await _userDatabase.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                await DisplayAlert("Error", "User already exists. Please try a different email/ID.", "OK");
                return;
            }

            // create and save the new user
            var newUser = new User
            {
                Username = username,
                Password = password
            };
            await _userDatabase.SaveUserAsync(newUser);

            await DisplayAlert("Success", "Registration successful!", "OK");
            await Shell.Current.GoToAsync(""); this is where we redirect but unavailable pa rn
        }
    }
}
