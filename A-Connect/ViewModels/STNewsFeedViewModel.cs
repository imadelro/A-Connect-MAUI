using A_Connect.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace A_Connect.ViewModels
{
    public class STNewsFeedViewModel : BaseViewModel
    {
        private ObservableCollection<STForm> _allPosts;
        public ObservableCollection<STForm> DisplayedPosts { get; set; }

        private bool _isOtherPostsSelected = true;
        public bool IsOtherPostsSelected
        {
            get => _isOtherPostsSelected;
            set => SetProperty(ref _isOtherPostsSelected, value);
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

        public STNewsFeedViewModel()
        {
            // All posts
            // After (no sample data, just an empty list)
            _allPosts = new ObservableCollection<STForm>();


            DisplayedPosts = new ObservableCollection<STForm>();

            // Default to "Posts by other users"
            IsOtherPostsSelected = true;
            IsOwnPostsSelected = false;
            FilterPosts();

            ShowOtherPostsCommand = new Command(() =>
            {
                IsOtherPostsSelected = true;
                IsOwnPostsSelected = false;
                FilterPosts();
            });

            ShowOwnPostsCommand = new Command(() =>
            {
                IsOtherPostsSelected = false;
                IsOwnPostsSelected = true;
                FilterPosts();
            });

            // Navigate to post form
            PostTradeCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("PostTradePage");
            });

            // Tap on a post -> navigate to individual post
            PostSelectedCommand = new Command<STForm>(async (post) =>
            {
                var navParameter = new Dictionary<string, object> { { "SelectedPost", post } };
                await Shell.Current.GoToAsync("IndividualPostPage", navParameter);
            });

            // Listen for new posts from STFormViewModel
            MessagingCenter.Subscribe<STFormViewModel, STForm>(this, "NewTradePost", (sender, newPost) =>
            {
                _allPosts.Add(newPost);
                FilterPosts();
            });
        }

        private void FilterPosts()
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
                    p.CourseCode.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    p.TradeOffer.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    p.TradeRequest.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            // 4) Update DisplayedPosts
            foreach (var post in filtered)
            {
                DisplayedPosts.Add(post);
            }
        }
    }
}
