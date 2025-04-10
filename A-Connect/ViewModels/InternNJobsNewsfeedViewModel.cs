using System.Collections.ObjectModel;
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

        public bool AllPostsSelected
        {
            get => _allPostsSelected;
            set => SetProperty(ref _allPostsSelected, value);
        }

        public bool IsOwnPostsSelected
        {
            get => _isOwnPostsSelected;
            set => SetProperty(ref _isOwnPostsSelected, value);
        }

        public ICommand SwitchToAllPostsCommand { get; }
        public ICommand SwitchToOwnPostsCommand { get; }
        public ICommand SwitchToCreatePostCommand { get; }

        public InternNJobsNewsfeedViewModel()
        {
            _database = DependencyService.Get<OpportunityDatabase>();
            _allOpportunities = new ObservableCollection<Opportunity>();

            SwitchToAllPostsCommand = new Command(SwitchToAllPosts);
            SwitchToOwnPostsCommand = new Command(SwitchToOwnPosts);
            SwitchToCreatePostCommand = new Command(SwitchToCreatePost);
        }

        private void SwitchToAllPosts()
        {
            AllPostsSelected = true;
            IsOwnPostsSelected = false;
        }

        private void SwitchToOwnPosts()
        {
            AllPostsSelected = false;
            IsOwnPostsSelected = true;
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

            if (opportunities.Count > 0)
            {
                // Print each opportunity's details (e.g., Title, Position, Company, etc.)
                foreach (var opportunity in opportunities)
                {
                    Console.WriteLine($"Title: {opportunity.Title}, Position: {opportunity.Position}, Company: {opportunity.Company}, Caption: {opportunity.Caption}, Type: {opportunity.Type}, Post URL: {opportunity.PostURL}");
                }
            }
            else
            {
                Console.WriteLine("No opportunities found in the database.");
            }

            Opportunities = new ObservableCollection<Opportunity>(opportunities);


        }

    }
}
