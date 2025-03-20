using A_Connect.Models;
using A_Connect.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;

namespace A_Connect.ViewModels
{
    public class ProfsToPickNewsFeedViewModel : BaseViewModel
    {
        private readonly ReviewDatabase _database;
        private string _searchText;
        private ObservableCollection<Review> _allReviews;

        // Add: Booleans for tab selection
        private bool _isOtherPostsSelected = true; // default
        public bool IsOtherPostsSelected
        {
            get => _isOtherPostsSelected;
            set { SetProperty(ref _isOtherPostsSelected, value); }
        }

        private bool _isOwnPostsSelected;
        public bool IsOwnPostsSelected
        {
            get => _isOwnPostsSelected;
            set { SetProperty(ref _isOwnPostsSelected, value); }
        }

        // Existing
        public ObservableCollection<GroupedReview> GroupedReviews { get; set; }
            = new ObservableCollection<GroupedReview>();

        // Add: Commands for switching tabs
        public ICommand SwitchToOtherPostsCommand { get; }
        public ICommand SwitchToOwnPostsCommand { get; }

        public ProfsToPickNewsFeedViewModel(ReviewDatabase database)
        {
            _database = database;
            _allReviews = new ObservableCollection<Review>();

            LoadReviewsCommand = new Command(async () => await LoadReviews());
            ToggleGroupCommand = new Command<GroupedReview>(ToggleGroup);
            SearchCommand = new Command(PerformSearch);

            // Initialize tab commands
            SwitchToOtherPostsCommand = new Command(() => SwitchTab(isOther: true));
            SwitchToOwnPostsCommand = new Command(() => SwitchTab(isOther: false));

            // On startup, load everything
            LoadReviews();
        }

        public Command LoadReviewsCommand { get; }
        public Command SearchCommand { get; }
        public Command<GroupedReview> ToggleGroupCommand { get; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    PerformSearch();
                }
            }
        }

        private async Task LoadReviews()
        {
            var reviews = await _database.GetAllReviewsAsync();
            _allReviews = new ObservableCollection<Review>(reviews);
            // We apply the filter logic after loading
            FilterAndGroupReviews();
        }

        private void PerformSearch()
        {
            // Re-filter and group each time the search changes
            FilterAndGroupReviews();
        }

        private void FilterAndGroupReviews()
        {
            // 1) Start with all reviews
            var filtered = _allReviews.AsEnumerable();

            // 2) Filter by "own" vs "other" (placeholder logic)
            if (IsOwnPostsSelected)
            {
                // e.g., assume "own" means rating > 3? 
                // Or filter by user ID if you have it:
                var userId = App.CurrentUser.Username;
                filtered = filtered.Where(r => r.AuthorID == userId);
                //filtered = filtered.Where(r => r.Rating > 3);  // EXAMPLE ONLY
                Console.WriteLine($"UserID: {userId}");
                foreach (var review in _allReviews)
                {
                    Console.WriteLine($"AuthorID: {review.AuthorID}");
                }

            }
            else
            {
                // "Other" posts
                // e.g., filtered = filtered.Where(r => r.UserId != userId);
                //filtered = filtered.Where(r => r.Rating <= 3); // EXAMPLE ONLY
            }

            // 3) Filter by search text
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(r =>
                    r.ProfessorName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                    r.CourseCode.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));
            }

            // 4) Group them by professor
            var grouped = filtered
                .GroupBy(r => r.ProfessorName)
                .Select(g => new GroupedReview(g.Key, new ObservableCollection<Review>(g.ToList())))
                .ToList();

            // 5) Update the UI collection
            GroupedReviews.Clear();
            foreach (var group in grouped)
            {
                GroupedReviews.Add(group);
            }
        }

        private void ToggleGroup(GroupedReview group)
        {
            if (group == null) return;
            group.IsExpanded = !group.IsExpanded;
            OnPropertyChanged(nameof(GroupedReviews));
        }

        private void SwitchTab(bool isOther)
        {
            // Switch tab booleans
            IsOtherPostsSelected = isOther;
            IsOwnPostsSelected = !isOther;

            // Re-apply filters
            FilterAndGroupReviews();
        }
    }
}
