using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using A_Connect.Models;
using MessagingCenter = Microsoft.Maui.Controls.MessagingCenter;

namespace A_Connect.ViewModels
{
    public class STOwnPostViewModel : BaseViewModel
    {
        public ObservableCollection<STForm> OwnPosts { get; set; }
        public ICommand PostTradeCommand { get; }

        public STOwnPostViewModel()
        {
            OwnPosts = new ObservableCollection<STForm>();

            // Listen for new posts (sent via MessagingCenter).
            MessagingCenter.Subscribe<STFormViewModel, STForm>(this, "NewTradePost", (sender, newPost) =>
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
