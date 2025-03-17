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
    }
}
