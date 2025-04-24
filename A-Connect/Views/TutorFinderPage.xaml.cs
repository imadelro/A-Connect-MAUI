using Microsoft.Maui.Controls;
using A_Connect.Models;
using A_Connect.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace A_Connect.Views
{
    public partial class TutorFinderPage : ContentPage
    {
        public ICommand MarkAsUnavailableCommand { get; }
        public ICommand MarkAsAvailableCommand { get; }
        private readonly TutorFinderDatabase _database;

        // Keep all posts in memory
        private List<TutorPost> _allPosts = new List<TutorPost>();

        // Commands for navigation and tab switching
        public ICommand NavigateToHomeCommand { get; }
        public ICommand ShowOtherPostsCommand { get; }
        public ICommand ShowOwnPostsCommand { get; }
        public ICommand DeletePostCommand { get; }

        // Track which tab is selected
        private bool _allPostsSelected = true;
        public bool allPostsSelected
        {
            get => _allPostsSelected;
            set
            {
                if (_allPostsSelected != value)
                {
                    _allPostsSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isOwnPostsSelected;
        public bool IsOwnPostsSelected
        {
            get => _isOwnPostsSelected;
            set
            {
                if (_isOwnPostsSelected != value)
                {
                    _isOwnPostsSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<TutorPost> _displayedPosts;
        public List<TutorPost> DisplayedPosts
        {
            get => _displayedPosts;
            set
            {
                if (_displayedPosts != value)
                {
                    _displayedPosts = value;
                    OnPropertyChanged();
                }
            }
        }

        public TutorFinderPage(TutorFinderDatabase database)
        {
            InitializeComponent();
            _database = database;

            MarkAsUnavailableCommand = new Command<TutorPost>(async (post) => await MarkAsUnavailable(post));
            MarkAsAvailableCommand = new Command<TutorPost>(async (post) => await MarkAsAvailable(post));

            // Initialize commands
            NavigateToHomeCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("//HomePage");
            });

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

            DeletePostCommand = new Command<TutorPost>(async (post) => await DeletePost(post));

            BindingContext = this; // Set the BindingContext to the page itself
        }

        private async Task MarkAsUnavailable(TutorPost post)
        {
            if (post == null) return;

            post.IsAvailable = false;
            await _database.MarkAsUnavailableAsync(post);
            FilterPosts();
        }

        private async Task MarkAsAvailable(TutorPost post)
        {
            if (post == null) return;

            post.IsAvailable = true;
            await _database.MarkAsAvailableAsync(post);
            FilterPosts();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Load or refresh all posts
            _allPosts = await _database.GetAllPostsAsync();
            FilterPosts();
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            FilterPosts();
        }

        private async void OnCreatePostClicked(object sender, EventArgs e)
        {
            // Navigate to form page
            await Shell.Current.GoToAsync(nameof(TutorFinderFormPage));
        }

        private async void OnPostSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is TutorPost selectedPost)
            {
                // Build route with query parameter
                var route = $"{nameof(TutorFinderDetailsPage)}?PostId={selectedPost.Id}";
                await Shell.Current.GoToAsync(route);

                // Clear selection
                postsCollectionView.SelectedItem = null;
            }
        }

        private async Task DeletePost(TutorPost post)
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

                // Delete the post from the database
                await _database.DeletePostAsync(post);

                // Refresh the displayed posts
                FilterPosts();
            }
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is TutorPost post)
            {
                bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this post?", "Yes", "No");
                if (confirm)
                {
                    // Remove the post from the in-memory collection
                    _allPosts.Remove(post);

                    // Delete the post from the database
                    await _database.DeletePostAsync(post);

                    // Refresh the displayed posts
                    FilterPosts();
                }
            }
        }

        private void FilterPosts()
        {
            var currentUser = App.CurrentUser?.Username ?? "UnknownUser";

            // Start with all posts
            IEnumerable<TutorPost> filtered = _allPosts;

            // Tab-based filter
            if (IsOwnPostsSelected)
            {
                // Show only posts by the logged-in user
                filtered = filtered.Where(p => p.PosterName == currentUser);
            }
            else
            {
                // Show posts NOT by the logged-in user
                filtered = filtered.Where(p => p.PosterName != currentUser);
            }

            // Search filter
            var searchText = searchEntry.Text?.Trim().ToLower() ?? "";
            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(p =>
                    (p.CourseCode?.ToLower().Contains(searchText) ?? false)
                    || (p.Category?.ToLower().Contains(searchText) ?? false)
                    || (p.AdditionalInfo?.ToLower().Contains(searchText) ?? false));
            }

            foreach (var post in filtered)
            {
                post.IsOwnPost = post.PosterName == currentUser;
            }

            // Update the collection
            DisplayedPosts = filtered.ToList();
        }
    }
}
