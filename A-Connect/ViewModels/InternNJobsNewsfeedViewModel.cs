﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using A_Connect.Models;
using A_Connect.Services;
using A_Connect.Views;
using Microsoft.Maui.Controls;

namespace A_Connect.ViewModels
{
    public class InternNJobsNewsfeedViewModel : BaseViewModel
    {

        private readonly OpportunityDatabase _database;
        private ObservableCollection<Opportunity> _allOpportunities;
        private string _searchText;
        private bool _allPostsSelected;
        private bool _isOwnPostsSelected;

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public bool allPostsSelected
        {
            get => _allPostsSelected;
            set => SetProperty(ref _allPostsSelected, value);
        }

        public bool isOwnPostsSelected
        {
            get => _isOwnPostsSelected;
            set => SetProperty(ref _isOwnPostsSelected, value);
        }

        public ICommand SwitchToAllPostsCommand { get; }
        public ICommand SwitchToOwnPostsCommand { get; }
        public ICommand SwitchToCreatePostCommand { get; }
        public ICommand PostTappedCommand { get; }
        public ICommand DeletePostCommand { get; }


        public InternNJobsNewsfeedViewModel()
        {
            _database = DependencyService.Get<OpportunityDatabase>();
            _allOpportunities = new ObservableCollection<Opportunity>();

            SwitchToAllPostsCommand = new Command(SwitchToAllPosts);
            SwitchToOwnPostsCommand = new Command(SwitchToOwnPosts);
            SwitchToCreatePostCommand = new Command(SwitchToCreatePost);
            PostTappedCommand = new Command<Opportunity>(OnPostTapped);
            DeletePostCommand = new Command<Opportunity>(async (opportunity) => await DeletePost(opportunity));

        }

        private void SwitchToAllPosts()
        {
            allPostsSelected = true;
            isOwnPostsSelected = false;
            UpdateDisplayedPosts();
        }

        private void SwitchToOwnPosts()
        {
            allPostsSelected = false;
            isOwnPostsSelected = true;
            UpdateDisplayedPosts();
        }

        private ObservableCollection<Opportunity> _displayedPosts;
        public ObservableCollection<Opportunity> DisplayedPosts
        {
            get => _displayedPosts;
            set => SetProperty(ref _displayedPosts, value);
        }

        private void UpdateDisplayedPosts()
        {
            var CurrentUser = App.CurrentUser.Username;

            if (_allPostsSelected)
            {
                DisplayedPosts = new ObservableCollection<Opportunity>(_opportunities.Where(o => o.PostedBy != CurrentUser));
            }
            else if (_isOwnPostsSelected)
            {
                DisplayedPosts = new ObservableCollection<Opportunity>(_opportunities.Where(o => o.PostedBy == CurrentUser));
            }
        }


        private async void SwitchToCreatePost()
        {
            await Shell.Current.GoToAsync(nameof(InternNJobsFormPage));
        }

        private ObservableCollection<Opportunity> _opportunities;
        public ObservableCollection<Opportunity> Opportunities
        {
            get => _opportunities;
            set => SetProperty(ref _opportunities, value);
        }

        public async Task LoadOpportunitiesAsync()
        {
            var opportunities = await _database.GetAllOpportunitiesAsync(); // Fetch the updated opportunities list

            Opportunities = new ObservableCollection<Opportunity>(opportunities);
            UpdateDisplayedPosts();

        }

        private async void OnPostTapped(Opportunity selectedPost)
        {
            if (selectedPost == null) return;

            // Navigate to a detail page (you'll need to have this page created)
            Console.WriteLine("PostTapped Working");
            await Shell.Current.GoToAsync($"{nameof(InternNJobsIndivPage)}?PostId={selectedPost.Id}");
        }

        private async Task DeletePost(Opportunity post)
        {
            if (post == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you want to delete this post?", "Yes", "No");
            if (confirm)
            {
                await _database.DeleteOpportunityAsync(post);
                Opportunities.Remove(post);
            }
        }

    }
}
