using A_Connect.ViewModels;
using A_Connect.Services;
using Microsoft.Maui.Controls;

namespace A_Connect.Views
{
    public partial class InternNJobsNewsfeedPage : ContentPage
    {
        private InternNJobsNewsfeedViewModel viewModel;

        public InternNJobsNewsfeedPage()
        {
            InitializeComponent();
            viewModel = new InternNJobsNewsfeedViewModel(); // Initialize viewModel here
            BindingContext = viewModel; // Set the BindingContext to the viewModel
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadOpportunitiesAsync(); // Call the method on the viewModel
        }
    }
}
