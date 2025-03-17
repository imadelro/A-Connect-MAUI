using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using A_Connect.Models;
using A_Connect.Services;


namespace A_Connect.Views
{
    public partial class TutorFinderPage : ContentPage
    {
        private readonly TutorFinderDatabase _database;

        public TutorFinderPage(TutorFinderDatabase database)
        {
            InitializeComponent();
            _database = database;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Load or refresh the posts
            var posts = await _database.GetAllPostsAsync();
            postsCollectionView.ItemsSource = posts;
        }

        private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue;
            var filtered = await _database.SearchPostsAsync(searchText);
            postsCollectionView.ItemsSource = filtered;
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
    }
}


