using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using A_Connect.Models;

namespace A_Connect.ViewModels
{
    public class IndividualPostViewModel : BaseViewModel
    {
        private TradePost _selectedPost;
        public TradePost SelectedPost
        {
            get => _selectedPost;
            set => SetProperty(ref _selectedPost, value);
        }

        public ICommand CloseCommand { get; }

        public IndividualPostViewModel()
        {
            CloseCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });
        }

        // Called when navigating via Shell query parameters.
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("SelectedPost"))
                SelectedPost = query["SelectedPost"] as TradePost;
        }
    }
}
