using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        private bool _showOnlyInternships;
        private bool _showOnlyJobs;
        private bool _isAllSelected = true;
        private string _selectedJobField = "All";


        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    LoadOpportunitiesAsync();  // Update the opportunities based on the search text
                }
            }
        }
        public string SelectedJobField
        {
            get => _selectedJobField;
            set
            {
                if (SetProperty(ref _selectedJobField, value))
                {
                    LoadOpportunitiesAsync();  // Reload the opportunities based on the selected job field
                }
            }
        }
        public ObservableCollection<string> JobFields { get; set; }  // List of job fields (to bind to Picker)

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

            allPostsSelected = true;
            isOwnPostsSelected = false;

            JobFields = new ObservableCollection<string>
            {
                "All", "Administration", "Business Development / Strategy", "Creative / Design", "Customer Service",
                "Engineering / Technical", "Executive / Leadership", "Finance", "Human Resources (HR)", "Information Technology (IT)",
                "Legal / Compliance", "Marketing", "Operations / Logistics", "Product / Project Management", "Research & Development (R&D)", "Sales"
            };
        }

        private void SwitchToAllPosts()
        {
            allPostsSelected = true;
            isOwnPostsSelected = false;
            LoadOpportunitiesAsync();
        }

        private void SwitchToOwnPosts()
        {
            allPostsSelected = false;
            isOwnPostsSelected = true;
            LoadOpportunitiesAsync();
        }

        public bool ShowAllOpportunities
        {
            get => _isAllSelected;
            set
            {
                if (_isAllSelected != value)
                {
                    _isAllSelected = value;
                    OnPropertyChanged();
                    if (_isAllSelected)
                    {
                        ShowOnlyJobs = false;
                        ShowOnlyInternships = false;
                    }
                    LoadOpportunitiesAsync();  // Reload opportunities when this changes
                }
            }
        }

        public bool ShowOnlyJobs
        {
            get => _showOnlyJobs;
            set
            {
                if (SetProperty(ref _showOnlyJobs, value))
                {
                    LoadOpportunitiesAsync();  // Reload opportunities when this changes
                }
            }
        }

        public bool ShowOnlyInternships
        {
            get => _showOnlyInternships;
            set
            {
                if (SetProperty(ref _showOnlyInternships, value))
                {
                    LoadOpportunitiesAsync();  // Reload opportunities when this changes
                }
            }
        }

        private ObservableCollection<Opportunity> _displayedPosts;
        public ObservableCollection<Opportunity> DisplayedPosts
        {
            get => _displayedPosts;
            set => SetProperty(ref _displayedPosts, value);
        }

        private ObservableCollection<Opportunity> _opportunities;
        public ObservableCollection<Opportunity> Opportunities
        {
            get => _opportunities;
            set => SetProperty(ref _opportunities, value);
        }

        // Load the opportunities asynchronously
        public async Task LoadOpportunitiesAsync()
        {
            List<Opportunity> opportunities;

            // Get all opportunities initially
            opportunities = await _database.GetAllOpportunitiesAsync();

            // Apply user filters if selected
            if (isOwnPostsSelected)
            {
                opportunities = opportunities
                    .Where(o => o.PostedBy == App.CurrentUser.Username)
                    .ToList();
            }

            // Apply type filters if selected
            if (_showOnlyJobs)
            {
                opportunities = opportunities
                    .Where(o => o.Type == "Job")
                    .ToList();
            }
            else if (_showOnlyInternships)
            {
                opportunities = opportunities
                    .Where(o => o.Type == "Internship")
                    .ToList();
            }

            // Apply job field filter if selected (and not 'All')
            if (!string.IsNullOrEmpty(SelectedJobField) && SelectedJobField != "All")
            {
                opportunities = opportunities
                    .Where(o => o.JobField == SelectedJobField)
                    .ToList();
            }

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(_searchText))
            {
                opportunities = opportunities
                    .Where(o => o.Company.Contains(_searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                o.Location.Contains(_searchText, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Update the displayed posts
            Opportunities = new ObservableCollection<Opportunity>(opportunities);
            DisplayedPosts = Opportunities;
        }


        private async void SwitchToCreatePost()
        {
            await Shell.Current.GoToAsync(nameof(InternNJobsFormPage));
        }

        private async void OnPostTapped(Opportunity selectedPost)
        {
            if (selectedPost == null) return;

            // Navigate to the detail page for the selected post
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