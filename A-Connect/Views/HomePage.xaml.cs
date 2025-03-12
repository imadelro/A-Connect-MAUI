using Microsoft.Maui.Controls;
using  A_Connect;

namespace A_Connect.Views
{
    public partial class Homepage : ContentPage
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private async void TutorFinderClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//TutorFinderHomePage");
        }
    }
}
