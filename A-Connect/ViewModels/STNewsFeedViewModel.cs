using A_Connect.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;

using A_Connect; 

namespace A_Connect.ViewModels
{
    public class STNewsFeedViewModel : BaseViewModel
    {
        // Commands
        public ICommand NavigateToHomeCommand { get; }
        public ICommand MarkAsUnavailableCommand { get; }
        public ICommand MarkAsAvailableCommand { get; }
        public ICommand ShowOtherPostsCommand { get; }
        public ICommand ShowOwnPostsCommand { get; }
        public ICommand PostTradeCommand { get; }
        public ICommand PostSelectedCommand { get; }
        public ICommand DeletePostCommand { get; }

        // Observable Collections for posts
        private ObservableCollection<STForm> _allPosts;
        public ObservableCollection<STForm> DisplayedPosts { get; set; }

        // Properties for filters
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
            set
            {
                if (SetProperty(ref _isOwnPostsSelected, value))
                {
                    // Notify all posts to update their ShowActionButtons property
                    foreach (var post in _allPosts)
                    {
                        post.OnPropertyChanged(nameof(post.ShowActionButtons));
                    }
                }
            }
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

        // Constructor
        public STNewsFeedViewModel()
        {
            // Initialize commands
            MarkAsUnavailableCommand = new Command<STForm>(async (post) => await MarkAsUnavailable(post));
            MarkAsAvailableCommand = new Command<STForm>(async (post) => await MarkAsAvailable(post));
            DeletePostCommand = new Command<STForm>(async (post) => await DeletePost(post));
            NavigateToHomeCommand = new Command(async () => await NavigateToHome());

            // Initialize collections
            _allPosts = new ObservableCollection<STForm>();
            DisplayedPosts = new ObservableCollection<STForm>();

            // Load posts from database asynchronously
            Task.Run(async () => await LoadPostsFromDatabaseAsync());

            // Default to "Posts by other users"
            allPostsSelected = true;
            IsOwnPostsSelected = false;
            FilterPosts();

            // Commands for toggling posts
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

            // Command to navigate to post form
            PostTradeCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("PostTradePage");
            });

            // Listen for new posts
            MessagingCenter.Unsubscribe<STFormViewModel, STForm>(this, "NewTradePost"); // Unsubscribe to avoid duplicates
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

        // Methods for post actions (Mark as Available, Mark as Unavailable, Delete)
        private async Task MarkAsUnavailable(STForm post)
        {
            if (post == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirm Action", "Are you sure you want to mark this post as unavailable?", "Yes", "No");

            if (confirm)
            {
                post.IsAvailable = false;
                await App.STDB.MarkAsUnavailableAsync(post); // Update in the database
                FilterPosts(); // Refresh displayed posts
            }
        }

        private async Task MarkAsAvailable(STForm post)
        {
            if (post == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirm Action", "Are you sure you want to mark this post as available?", "Yes", "No");

            if (confirm)
            {
                post.IsAvailable = true;
                await App.STDB.MarkAsAvailableAsync(post); // Update in the database
                FilterPosts(); // Refresh displayed posts
            }
        }

        private async Task DeletePost(STForm post)
        {
            if (post == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete", "Are you sure you want to delete this post?", "Yes", "No");

            if (confirm)
            {
                // Remove the post from the in-memory collection
                _allPosts.Remove(post);
                DisplayedPosts.Remove(post);

                // Delete from the database
                await App.STDB.DeletePostAsync(post);

                // Refresh displayed posts
                FilterPosts();
            }
        }

        // Navigate to the home page
        private async Task NavigateToHome()
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        // Load posts from the database asynchronously
        public async Task LoadPostsFromDatabaseAsync()
        {
            _allPosts.Clear(); // Clear current posts
            var dbPosts = await App.STDB.GetAllPostsAsync(); // Load posts from the database

            foreach (var post in dbPosts)
            {
                _allPosts.Add(post);
            }
        }

        // Filter posts based on tab selection and search text
        public async Task FilterPosts()
        {
            var currentUser = App.CurrentUser?.Username ?? "Unknown";

            var filtered = _allPosts.AsEnumerable();

            // Apply tab-based filter
            if (IsOwnPostsSelected)
            {
                filtered = filtered.Where(p => p.PostedBy == currentUser);
            }
            else
            {
                filtered = filtered.Where(p => p.PostedBy != currentUser);
            }

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(p => p.CourseCode.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            // Update the DisplayedPosts collection
            DisplayedPosts.Clear();
            foreach (var post in filtered)
            {
                // Dynamically fetch the display name each time
                post.DisplayName = await GetDisplayNameForUser(post.PostedBy); // Fetch display name here
                DisplayedPosts.Add(post);
            }

            OnPropertyChanged(nameof(DisplayedPosts));

        }

        private async Task<string> GetDisplayNameForUser(string username)
        {
            // Fetch the user display name from your database or service
            var user = await App.userDb.GetUserByUsernameAsync(username);
            return user?.DisplayName ?? "Unknown"; // Return display name or "Unknown" if not found
        }
    }
}
