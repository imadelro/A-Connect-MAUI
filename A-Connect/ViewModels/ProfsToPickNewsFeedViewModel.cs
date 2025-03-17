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

        public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();

        public ProfsToPickNewsFeedViewModel(ReviewDatabase database)
        {
            _database = database;
            _allReviews = new ObservableCollection<Review>();

            LoadReviewsCommand = new Command(async () => await LoadReviews());
            SearchCommand = new Command(PerformSearch); 

            LoadReviews();
        }

        public Command LoadReviewsCommand { get; }
        public Command SearchCommand { get; }

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
            PerformSearch(); 
        }

        private void PerformSearch() 
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                
                Reviews.Clear();
                foreach (var review in _allReviews)
                {
                    Reviews.Add(review);
                }
            }
            else
            {
               
                var filteredReviews = _allReviews
                    .Where(r => r.ProfessorName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase)
                             || r.CourseCode.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();

                Reviews.Clear();
                foreach (var review in filteredReviews)
                {
                    Reviews.Add(review);
                }
            }
        }
    }
}
