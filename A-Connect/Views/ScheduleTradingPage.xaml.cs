using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using A_Connect.Models;   // for ScheduleTradingPost
using SQLite;
using A_Connect.Services;
namespace A_Connect.Views
{
    public partial class ScheduleTradingPage : ContentPage
    {
        private readonly ScheduleTradingService _service;

        // Parameterless constructor required by Shell navigation
        public ScheduleTradingPage()
        {
            InitializeComponent();

            // For simplicity, create the SQLite connection and service here
            // (In a real app, you might use Dependency Injection instead)
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "scheduleTrading.db3");
            var connection = new SQLiteAsyncConnection(dbPath);
            _service = new ScheduleTradingService(connection);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadPostsAsync();
        }

        // Called whenever the user types in the SearchBar
        private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(searchText))
            {
                // No search text, show all posts
                await LoadPostsAsync();
            }
            else
            {
                // Search for posts by course code
                var results = await _service.SearchPostsAsync(searchText);
                postsCollectionView.ItemsSource = results;
            }
        }

        // Called when user taps the "Add Post" button
        private async void OnAddPostClicked(object sender, EventArgs e)
        {
            // Navigate to your form page (make sure it's registered in AppShell)
            await Shell.Current.GoToAsync(nameof(ScheduleTradingFormPage));
        }

        // Loads all posts and binds them to the CollectionView
        private async Task LoadPostsAsync()
        {
            var allPosts = await _service.GetAllPostsAsync();
            postsCollectionView.ItemsSource = allPosts;
        }

        internal async Task LoadPosts()
        {
            var allPosts = await _service.GetAllPostsAsync();
            postsCollectionView.ItemsSource = allPosts;
        }
    }
}

