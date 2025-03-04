using Microsoft.Maui.Controls;
using System;

namespace A_Connect.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = emailIDnum.Text;
            string password = passwordEntry.Text;

            if (IsValidUser(email, password))
            {
                await Shell.Current.GoToAsync("//home");
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid email or password!", "OK");
            }
        }

        private bool IsValidUser(string email, string password)
        {
            return email == "user@example.com" && password == "password123";
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(".."); // Goes back to the previous page
        }
    }
}
