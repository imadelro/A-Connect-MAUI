using A_Connect.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

// ADDED
using A_Connect;                // for App.STDB
using System.Threading.Tasks;   // if needed for async

namespace A_Connect.ViewModels
{
    public class STNewsFeedViewModel : BaseViewModel
    {
        public ICommand NavigateToHomeCommand { get; }

        private ObservableCollection<STForm> _allPosts;
        public ObservableCollection<STForm> DisplayedPosts { get; set; }

        private bool _allPostsSelected = true;
        public bool allPostsSelected
        {
            get => _allPostsSelected;
            set => SetProperty(ref _allPostsSelected, value);
        }

        private bool _isOwnPostsSelected;
        public bool IsOwnPostsSelected
        {
            get => _isOwnPostsSelected;
            set => SetProperty(ref _isOwnPostsSelected, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    FilterPosts();
                }
            }
        }

        public ICommand ShowOtherPostsCommand { get; }
        public ICommand ShowOwnPostsCommand { get; }
        public ICommand PostTradeCommand { get; }
        public ICommand PostSelectedCommand { get; }

        public ICommand DeletePostCommand { get; } // Declare the DeletePostCommand property

        public STNewsFeedViewModel()
        {
            DeletePostCommand = new Command<STForm>(async (post) => await DeletePost(post));

            NavigateToHomeCommand = new Command(async () => await NavigateToHome());
            
            _allPosts = new ObservableCollection<STForm>();
            DisplayedPosts = new ObservableCollection<STForm>();

            Task.Run(async () => await LoadPostsFromDatabaseAsync());

            // Default to "Posts by other users"
            allPostsSelected = true;
            IsOwnPostsSelected = false;
            FilterPosts();

            ShowOtherPostsCommand = new Command(() =>
            {
                allPostsSelected = true;
                IsOwnPostsSelected = false;
                FilterPosts();
            });

            ShowOwnPostsCommand = new Command(() =>
            {
                allPostsSelected = false;
                IsOwnPostsSelected = true;
                FilterPosts();
            });

            // Navigate to post form
            PostTradeCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("PostTradePage");
            });

            // Listen for new posts from STFormViewModel
            MessagingCenter.Unsubscribe<STFormViewModel, STForm>(this, "NewTradePost"); // Unsubscribe first to avoid duplicates
            MessagingCenter.Subscribe<STFormViewModel, STForm>(this, "NewTradePost", async (sender, newPost) =>
            {
                if (!_allPosts.Any(p => p.Id == newPost.Id)) // Prevent duplicates
                {
                    _allPosts.Add(newPost);
                    FilterPosts();

                    // Save the new post to the database
                    await App.STDB.SavePostAsync(newPost);
                }
            });
        }

        private async Task DeletePost(STForm post)
        {
            if (post == null)
                return;

            // Confirm deletion
            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete",
                "Are you sure you want to delete this post?",
                "Yes",
                "No");

            if (confirm)
            {
                // Remove the post from the in-memory collection
                _allPosts.Remove(post);
                DisplayedPosts.Remove(post);

                // Delete the post from the database
                await App.STDB.DeletePostAsync(post);

                // Refresh the displayed posts
                FilterPosts();
            }
        }

        private async Task NavigateToHome()
        {
            // Navigate to the HomePage
            await Shell.Current.GoToAsync("//HomePage");
        }

        public async Task LoadPostsFromDatabaseAsync()
        {
            // Clear the current posts
            _allPosts.Clear();

            // Load posts from the database
            var dbPosts = await App.STDB.GetAllPostsAsync();

            // Add the posts to the in-memory collection
            foreach (var post in dbPosts)
            {
                _allPosts.Add(post);
            }
        }

        public void FilterPosts()
        {
            DisplayedPosts.Clear();
            var currentUser = App.CurrentUser?.Username ?? "Unknown";

            // 1) Start with all
            var filtered = _allPosts.AsEnumerable();

            // 2) Tab-based filter
            if (IsOwnPostsSelected)
            {
                filtered = filtered.Where(p => p.PostedBy == currentUser);
            }
            else
            {
                filtered = filtered.Where(p => p.PostedBy != currentUser);
            }

            // 3) Search filter
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(p =>
                    p.CourseCode.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            // 4) Update DisplayedPosts
            foreach (var post in filtered)
            {
                DisplayedPosts.Add(post);
            }
        }
    }
}
