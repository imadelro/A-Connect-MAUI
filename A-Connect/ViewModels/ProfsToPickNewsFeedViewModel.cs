using A_Connect.Models;
using A_Connect.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace A_Connect.ViewModels
{
    public class ProfsToPickNewsFeedViewModel : BaseViewModel
    {
        private readonly ReviewDatabase _database;
        private string _searchText;
        private ObservableCollection<Review> _allReviews;

        public ObservableCollection<GroupedReview> GroupedReviews { get; set; }
            = new ObservableCollection<GroupedReview>();

        public ProfsToPickNewsFeedViewModel(ReviewDatabase database)
        {
            _database = database;
            _allReviews = new ObservableCollection<Review>();

            LoadReviewsCommand = new Command(async () => await LoadReviews());
            ToggleGroupCommand = new Command<GroupedReview>(ToggleGroup);
            SearchCommand = new Command(PerformSearch);

            LoadReviews();
        }

        public Command LoadReviewsCommand { get; }
        public Command SearchCommand { get; }
        public Command<GroupedReview> ToggleGroupCommand { get; } // Added for expand/collapse

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
            GroupReviews();  // ✅ FIX: Correct method call
        }

        private void GroupReviews()
        {
            GroupedReviews.Clear();

            var grouped = _allReviews
                .GroupBy(r => r.ProfessorName)
                .Select(g => new GroupedReview(g.Key, new ObservableCollection<Review>(g.ToList())))
                .ToList();

            foreach (var group in grouped)
            {
                GroupedReviews.Add(group);
            }
        }

        private void PerformSearch()
        {
            GroupedReviews.Clear();

            var filteredGroups = _allReviews
                .Where(r => string.IsNullOrWhiteSpace(SearchText) ||
                            r.ProfessorName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                            r.CourseCode.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase))
                .GroupBy(r => r.ProfessorName)
                .Select(g => new GroupedReview(g.Key, new ObservableCollection<Review>(g.ToList())))
                .ToList();

            foreach (var group in filteredGroups)
            {
                GroupedReviews.Add(group);
            }
        }

        private void ToggleGroup(GroupedReview group)
        {
            if (group == null) return;

            group.IsExpanded = !group.IsExpanded;
            OnPropertyChanged(nameof(GroupedReviews));

            System.Diagnostics.Debug.WriteLine($"Tapped: {group.ProfessorName}, Expanded: {group.IsExpanded}");
        }

    }
}
