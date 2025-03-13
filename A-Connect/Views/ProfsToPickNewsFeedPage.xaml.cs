using Microsoft.Maui.Controls;

namespace A_Connect.Views
{
    public partial class ProfsToPickNewsFeedPage : ContentPage
    {
        public ProfsToPickNewsFeedPage()
        {
            InitializeComponent();
            // No real logic yet. 
            // In the future, you might set BindingContext = new ProfsToPickViewModel();
        }

        // "Post a review" button click
        private async void OnPostReviewClicked(object sender, EventArgs e)
        {
            // Navigate to the form page
            await Shell.Current.GoToAsync("ProfsToPickFormPage");
        }
    }
}
