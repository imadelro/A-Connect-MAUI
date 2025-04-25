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
            string username = IDNum.Text;
            string password = passwordEntry.Text;
            string email = emailEntry.Text;

            // Check if the username already exists
            var existingUser = await _userDatabase.GetUserByUsernameAsync(username);
            if (await _userDatabase.GetUserByUsernameAsync(username) != null)
            {
                await DisplayAlert("Error", "ID Number already exists", "OK");
                return;
            }
            if (username.Length != 6)
            {
                await DisplayAlert("Error", "ID Number must be 6 characters", "OK");
                return;
            }
            if (await _userDatabase.GetUserByEmailAsync(email) != null)
            {
                await DisplayAlert("Error", "Email already exists", "OK");
                return;
            }

            // Email validation
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Error", "Email cannot be empty", "OK");
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                await DisplayAlert("Error", "Email must contain '@' and a domain (e.g., '.com')", "OK");
                return;
            }

            if (!email.EndsWith("@student.ateneo.edu"))
            {
                await DisplayAlert("Error", "Email must end with @student.ateneo.edu", "OK");
                return;
            }

            if (email.IndexOf("@") != email.LastIndexOf("@"))
            {
                await DisplayAlert("Error", "Email cannot contain multiple '@' symbols", "OK");
                return;
            }

            if (email.StartsWith("@") || email.StartsWith(".") || email.EndsWith("."))
            {
                await DisplayAlert("Error", "Email cannot start or end with '@' or '.'", "OK");
                return;
            }

            if (email.Contains(" "))
            {
                await DisplayAlert("Error", "Email cannot contain spaces", "OK");
                return;
            }

            // Simple password validation
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                await DisplayAlert("Error", "Password must be at least 8 characters long", "OK");
                return;
            }

            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasDigit = false;
            bool hasSpecial = false;
            string specialChars = "!@#$%^&*(),.?\":{}|<>";

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpperCase = true;
                else if (char.IsLower(c)) hasLowerCase = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if (specialChars.Contains(c)) hasSpecial = true;
            }

            if (!hasUpperCase)
            {
                await DisplayAlert("Error", "Password must contain at least one uppercase letter", "OK");
                return;
            }

            if (!hasLowerCase)
            {
                await DisplayAlert("Error", "Password must contain at least one lowercase letter", "OK");
                return;
            }

            if (!hasDigit)
            {
                await DisplayAlert("Error", "Password must contain at least one number", "OK");
                return;
            }

            if (!hasSpecial)
            {
                await DisplayAlert("Error", "Password must contain at least one special character", "OK");
                return;
            }

            // Create a new user
            var newUser = new User
            {
                Username = username,
                Password = password,
                Email = email
            };

            // Save the user to the database
            await _userDatabase.SaveUserAsync(newUser);

            await DisplayAlert("Success", "Registered successfully", "OK");
            await Shell.Current.GoToAsync("//StartPage");
        }
    }
}
