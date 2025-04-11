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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is ProfsToPickNewsFeedViewModel vm)
            {
                vm.LoadReviewsCommand.Execute(null);
            }
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the HomePage
            await Shell.Current.GoToAsync("//HomePage");
        }

    }
}
