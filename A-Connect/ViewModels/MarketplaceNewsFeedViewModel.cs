using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using A_Connect.Models;
using A_Connect.Services;
using Microsoft.Maui.Controls;

namespace A_Connect.ViewModels
{
    public class MarketplaceNewsFeedViewModel : BaseViewModel
    {
        private readonly MarketplaceDatabase _database;

        public ObservableCollection<MarketplacePost> Items { get; set; } = new ObservableCollection<MarketplacePost>();

        public ICommand DeleteCommand { get; }

        public MarketplaceNewsFeedViewModel(MarketplaceDatabase database)
        {
            _database = database;
            DeleteCommand = new Command<MarketplacePost>(async (post) => await DeletePost(post));
        }

        public async Task LoadItems()
        {
            Items.Clear();
            var items = await _database.GetPostsAsync();
            Debug.WriteLine($"Loading {items.Count} items into the feed...");
            foreach (var item in items)
            {
                Items.Add(item);
                Debug.WriteLine($"Loaded item: {item.ListingTitle}");
            }
            Debug.WriteLine("Done loading items.");
        }

        private async Task DeletePost(MarketplacePost post)
        {
            if (post == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you want to delete this post?", "Yes", "No");
            if (confirm)
            {
                await _database.DeletePostAsync(post);
                Items.Remove(post);
                Debug.WriteLine($"Deleted post: {post.ListingTitle}");
            }
        }
    }
}
