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
          
        }
    }
}
