using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using A_Connect.Models;
using MessagingCenter = Microsoft.Maui.Controls.MessagingCenter;

namespace A_Connect.ViewModels
{
    public class STOwnPostViewModel : BaseViewModel
    {
        public ObservableCollection<TradePost> OwnPosts { get; set; }
        public ICommand PostTradeCommand { get; }

        public STOwnPostViewModel()
        {
            OwnPosts = new ObservableCollection<TradePost>();

            // Listen for new posts (sent via MessagingCenter).
            MessagingCenter.Subscribe<STFormViewModel, TradePost>(this, "NewTradePost", (sender, newPost) =>
            {
                OwnPosts.Add(newPost);
            });

            PostTradeCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("PostTradePage");
            });
        }
    }
}
