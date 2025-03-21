using Microsoft.Maui.Controls;
using A_Connect.Models;
using A_Connect.Services;
using System.Collections.Generic;
using System.Linq;
using System;

namespace A_Connect.Views
{
    public partial class TutorFinderPage : ContentPage
    {
        private readonly TutorFinderDatabase _database;

        // Keep all posts in memory
        private List<TutorPost> _allPosts = new List<TutorPost>();

        // Track which tab is selected
        private bool _showOwnPosts = false;

        public TutorFinderPage(TutorFinderDatabase database)
        {
            InitializeComponent();
            _database = database;
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

        private void OnOtherPostsClicked(object sender, EventArgs e)
        {
            _showOwnPosts = false;
            FilterPosts();
        }

        private void OnOwnPostsClicked(object sender, EventArgs e)
        {
            _showOwnPosts = true;
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

        private void FilterPosts()
        {
            var currentUser = App.CurrentUser?.Username ?? "UnknownUser";

            // Start with all posts
            IEnumerable<TutorPost> filtered = _allPosts;

            // 1) Tab-based filter
            if (_showOwnPosts)
            {
                // Show only posts by the logged-in user
                filtered = filtered.Where(p => p.PosterName == currentUser);
            }
            else
            {
                // Show posts NOT by the logged-in user
                filtered = filtered.Where(p => p.PosterName != currentUser);
            }

            // 2) Search filter
            var searchText = searchEntry.Text?.Trim().ToLower() ?? "";
            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(p =>
                    (p.CourseCode?.ToLower().Contains(searchText) ?? false)
                    || (p.Category?.ToLower().Contains(searchText) ?? false)
                    || (p.AdditionalInfo?.ToLower().Contains(searchText) ?? false));
            }

            // Update the collection
            postsCollectionView.ItemsSource = filtered.ToList();
        }
    }
}
