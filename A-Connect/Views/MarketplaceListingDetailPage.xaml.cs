using A_Connect.Models;
using A_Connect.ViewModels;

namespace A_Connect.Views
{
    [QueryProperty(nameof(SelectedPost), "SelectedPost")]
    public partial class MarketplaceListingDetailPage : ContentPage
    {
        public MarketplaceListingDetailPage()
        {
            InitializeComponent();
            BindingContext = new MarketplaceListingDetailViewModel();
        }

        public MarketplacePost SelectedPost
        {
            set
            {
                if (BindingContext is MarketplaceListingDetailViewModel viewModel)
                {
                    viewModel.SelectedPost = value;
                }
            }
        }

        // Inside your MarketplaceNewsFeedPage.xaml.cs file
        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is MarketplacePost listing)
            {
                // Deselect the item
                ((ListView)sender).SelectedItem = null;

                // Navigate to the detail page with the selected listing
                var navigationParameter = new Dictionary<string, object>
                {
                    { "Listing", listing }
                };

                await Shell.Current.GoToAsync($"{nameof(MarketplaceListingDetailPage)}", navigationParameter);
            }
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the HomePage
            await Shell.Current.GoToAsync("//HomePage");
        }
    }
}
