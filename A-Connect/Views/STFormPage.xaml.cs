using Microsoft.Maui.Controls;
using A_Connect.ViewModels;

namespace A_Connect.Views
{
    public partial class PostTradePage : ContentPage
    {
        public PostTradePage()
        {
            InitializeComponent();
            BindingContext = new STFormViewModel(App.STDB);
        }
    }
}
