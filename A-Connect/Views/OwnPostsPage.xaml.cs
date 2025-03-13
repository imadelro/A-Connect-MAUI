using Microsoft.Maui.Controls;
using A_Connect.ViewModels;

namespace A_Connect.Views
{
    public partial class OwnPostsPage : ContentPage
    {
        public OwnPostsPage()
        {
            InitializeComponent();
            BindingContext = new OwnPostsViewModel();
        }
    }
}
