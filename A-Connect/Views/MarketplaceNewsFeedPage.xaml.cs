using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Connect.Views
{
    public partial class MarketplaceNewsFeedPage : ContentPage
    {
        public MarketplaceNewsFeedPage()
        {
            InitializeComponent();
        }

        private async void OnPostListingClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(MarketplaceFormPage));
        }
    }
}
