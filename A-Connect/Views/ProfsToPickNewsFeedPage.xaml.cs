using Microsoft.Maui.Controls;
using A_Connect.ViewModels;

namespace A_Connect.Views
{
    public partial class ProfsToPickNewsFeedPage : ContentPage
    {
        public ProfsToPickNewsFeedPage()
        {
            InitializeComponent();
            BindingContext = new ProfsToPickNewsFeedViewModel(App.ReviewDatabase);
        }

        private async void OnPostReviewClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfsToPickFormPage());
        }
    }
}
