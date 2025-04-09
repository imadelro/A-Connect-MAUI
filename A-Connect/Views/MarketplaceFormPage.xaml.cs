using A_Connect.Services;
using A_Connect.ViewModels;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace A_Connect.Views
{
    public partial class MarketplaceFormPage : ContentPage
    {
        private MarketplaceFormViewModel _viewModel;

        public MarketplaceFormPage(MarketplaceDatabase database)
        {
            InitializeComponent();
            _viewModel = new MarketplaceFormViewModel(database, App.CurrentUser.Username);
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Initialize the view model here if needed
        }
    }
}