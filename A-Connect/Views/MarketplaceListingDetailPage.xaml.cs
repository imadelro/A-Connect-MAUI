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
            BindingContext = selectedPost;
        }
    }

    public MarketplaceListingDetailPage()
    {
        InitializeComponent();
    }
}
