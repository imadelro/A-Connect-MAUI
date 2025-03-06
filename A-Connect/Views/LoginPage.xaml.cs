using Microsoft.Maui.Controls;
using System;

namespace A_Connect.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly IDatabaseService _databaseService;

        // Using constructor injection
        public LoginPage(IDatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        // If NOT using dependency injection, comment out the constructor above
        // and uncomment below:
        /*
        public LoginPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }
        */

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = emailIDnum.Text?.Trim();
            string password = passwordEntry.Text;

            bool isValid = await _databaseService.LoginUserAsync(email, password);
            if (isValid)
            {
                await DisplayAlert("Login Successful", "You have successfully logged in!", "OK");
                //await Shell.Current.GoToAsync("//home");
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid email or password!", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string email = emailIDnum.Text?.Trim();
            string password = passwordEntry.Text;

            bool success = await _databaseService.RegisterUserAsync(email, password);
            if (success)
            {
                await DisplayAlert("Registration Successful", "Your account has been created!", "OK");
            }
            else
            {
                await DisplayAlert("Registration Failed", "Email is already taken!", "OK");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
