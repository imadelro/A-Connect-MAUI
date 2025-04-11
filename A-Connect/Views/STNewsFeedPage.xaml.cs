using Microsoft.Maui.Controls;
using A_Connect.ViewModels;

namespace A_Connect.Views
{
    public partial class ScheduleTradingNewsFeedPage : ContentPage
    {
        public ScheduleTradingNewsFeedPage()
        {
            InitializeComponent();
            BindingContext = new STNewsFeedViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is STNewsFeedViewModel viewModel)
            {
                await viewModel.LoadPostsFromDatabaseAsync(); // Reload posts from the database
                viewModel.FilterPosts(); // Refresh the displayed posts
            }
        }

    }
}
