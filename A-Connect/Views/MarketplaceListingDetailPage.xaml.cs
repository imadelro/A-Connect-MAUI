using A_Connect.Models;
namespace A_Connect.Views;


[QueryProperty(nameof(SelectedPost), "SelectedPost")]
public partial class MarketplaceListingDetailPage : ContentPage
{
    private MarketplacePost selectedPost;
    public MarketplacePost SelectedPost
    {
        get => selectedPost;
        set
        {
            selectedPost = value;
            OnPropertyChanged();
            // Set this as the BindingContext so the XAML bindings work
            BindingContext = selectedPost;
        }
    }

    public MarketplaceListingDetailPage()
    {
        InitializeComponent();
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

}
