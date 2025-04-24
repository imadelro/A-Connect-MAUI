using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using A_Connect.Models;
using A_Connect.Views;
using A_Connect.Services;
using Microsoft.Maui.Controls;

namespace A_Connect.ViewModels
{
    public class MarketplaceNewsFeedViewModel : BaseViewModel
    {
        private readonly MarketplaceDatabase _database;

        public ObservableCollection<MarketplacePost> Items { get; set; } = new ObservableCollection<MarketplacePost>();
        public ObservableCollection<MarketplacePost> FilteredItems { get; set; } = new ObservableCollection<MarketplacePost>();

        private bool _showOnlyBuyers;
        private bool _showOnlySellers;
        private bool _showAllItems = true;
        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (SetProperty(ref searchText, value))
                {
                    FilterItems();
                }
            }
        }

        private bool isAllListingsSelected = true;
        public bool IsAllListingsSelected
        {
            get => isAllListingsSelected;
            set => SetProperty(ref isAllListingsSelected, value);
        }

        private bool isMyListingsSelected = false;
        public bool IsMyListingsSelected
        {
            get => isMyListingsSelected;
            set => SetProperty(ref isMyListingsSelected, value);
        }

        public bool ShowAllItems
        {
            get => _showAllItems;
            set
            {
                if (SetProperty(ref _showAllItems, value))
                {
                    if (value)
                    {
                        _showOnlyBuyers = false;
                        _showOnlySellers = false;
                       
                    }
                    FilterItems();
                }
            }
        }

        public bool ShowOnlyBuyers
        {
            get => _showOnlyBuyers;
            set
            {
                if (SetProperty(ref _showOnlyBuyers, value))
                {
                    if (value)
                    {
                        _showAllItems = false;
                        _showOnlySellers = false;
                 
                    }
                    FilterItems();
                }
            }
        }

        public bool ShowOnlySellers
        {
            get => _showOnlySellers;
            set
            {
                if (SetProperty(ref _showOnlySellers, value))
                {
                    if (value)
                    {
                        _showAllItems = false;
                        _showOnlyBuyers = false;

                    }
                    FilterItems();
                }
            }
        }
        public ICommand DeleteCommand { get; }
        public ICommand AllListingsCommand { get; }
        public ICommand MyListingsCommand { get; }

        public MarketplaceNewsFeedViewModel(MarketplaceDatabase database)
        {
            _database = database;
            DeleteCommand = new Command<MarketplacePost>(async (post) => await DeletePost(post));
            AllListingsCommand = new Command(ShowAllListings);
            MyListingsCommand = new Command(ShowMyListings);
        }

        private void ShowAllListings()
        {
            IsAllListingsSelected = true;
            IsMyListingsSelected = false;
            LoadItems();
        }

        private void ShowMyListings()
        {
            IsAllListingsSelected = false;
            IsMyListingsSelected = true;
            LoadItems();
        }

        private void FilterItems()
        {
            FilteredItems.Clear();

            var searchQuery = SearchText?.ToLower() ?? string.Empty;

            var filtered = Items.Where(item =>
                // Tab filters
                (IsAllListingsSelected || (IsMyListingsSelected && item.IsOwnedByCurrentUser)) &&

                // Type filters (skip if ShowAllItems is true)
                (
                    _showAllItems ||
                    (_showOnlyBuyers && item.Category.Equals("Buyer", StringComparison.OrdinalIgnoreCase)) ||
                    (_showOnlySellers && item.Category.Equals("Seller", StringComparison.OrdinalIgnoreCase))
                ) &&

                // Search filter
                (string.IsNullOrEmpty(searchQuery) ||
                 (!string.IsNullOrEmpty(item.ListingTitle) && item.ListingTitle.ToLower().Contains(searchQuery)) ||
                 (!string.IsNullOrEmpty(item.Description) && item.Description.ToLower().Contains(searchQuery)))
            ).ToList();

            foreach (var item in filtered)
            {
                FilteredItems.Add(item);
            }
        }





        public async Task LoadItems()
        {
            Items.Clear();
            FilteredItems.Clear();
            var items = await _database.GetPostsAsync();
            Debug.WriteLine($"Loading {items.Count} items into the feed...");

            foreach (var item in items)
            {
                Items.Add(item);
                Debug.WriteLine($"Loaded item: {item.ListingTitle}");
            }

            FilterItems(); // Apply initial filtering
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
                FilteredItems.Remove(post);
            }
        }

        public ICommand ItemTappedCommand => new Command<MarketplacePost>(OnItemTapped);

        private async void OnItemTapped(MarketplacePost post)
        {
            if (post == null) return;

            await Shell.Current.GoToAsync(nameof(MarketplaceListingDetailPage), true, new Dictionary<string, object>
            {
                { "SelectedPost", post }
            });
        }
    }
}
