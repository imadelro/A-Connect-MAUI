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
            // Assuming you have Entry fields for Username, Password, and Email in your XAML
            string username = IDNum.Text;
            string password = passwordEntry.Text;
            string email = emailEntry.Text;

            // Check if the username already exists
            var existingUser = await _userDatabase.GetUserByUsernameAsync(username);
            if (await _userDatabase.GetUserByUsernameAsync(username) != null )
            {
                await DisplayAlert("Error", "ID Number already exists", "OK");
                return;
            }
            if (await _userDatabase.GetUserByEmailAsync(email) != null)
            {
                await DisplayAlert("Error", "Email already exists", "OK");
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
