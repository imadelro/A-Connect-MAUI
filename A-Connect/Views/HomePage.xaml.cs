using Microsoft.Maui.Controls;

namespace A_Connect.Views
{
    public partial class Homepage : ContentPage
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private async void OnScheduleTradingClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ScheduleTradingNewsFeedPage");
        }

        private async void OnProfsToPickClicked(object sender, EventArgs e)
        {
            // Navigate to the Profs to Pick News Feed page
            // Make sure the route "ProfsToPickNewsFeedPage" is registered in AppShell.
            await Shell.Current.GoToAsync("ProfsToPickNewsFeedPage");
        }
        private async void OnTutorFinderClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("TutorFinderPage");

        } 
        private async void OnInternNJobsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("InternNJobsHomePage");
        }          

       

        private async void OnMarketplaceClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(MarketplaceNewsFeedPage));
        }

    }
}
