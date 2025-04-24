using A_Connect.Models;
using A_Connect.Views;
using A_Connect.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace A_Connect.ViewModels
{
    public class InternNJobsIndivViewModel : BaseViewModel
    {
        private OpportunityDatabase _database;
        private Opportunity _selectedPost;

        private ObservableCollection<Opportunity> _opportunities;
        public ObservableCollection<Opportunity> Opportunities
        {
            get => _opportunities;
            set => SetProperty(ref _opportunities, value);
        }

        // Property to check if the current user is the owner of the post
        public bool IsCurrentUserPost
        {
            get => SelectedPost != null && SelectedPost.PostedBy == App.CurrentUser.Username;
        }

        public Opportunity SelectedPost
        {
            get => _selectedPost;
            set
            {
                SetProperty(ref _selectedPost, value);
                OnPropertyChanged(nameof(IsCurrentUserPost));  // Ensure IsCurrentUserPost is updated when SelectedPost changes
            }
        }

        public ICommand DeletePostCommand { get; }
        public ICommand OpenUrlCommand { get; }


        public InternNJobsIndivViewModel()
        {
            _database = DependencyService.Get<OpportunityDatabase>();
            Opportunities = new ObservableCollection<Opportunity>();
            DeletePostCommand = new Command<Opportunity>(async (opportunity) => await DeletePost(opportunity));
            OpenUrlCommand = new Command<string>(async (url) =>
            {
                if (!string.IsNullOrEmpty(url))
                {
                   await Launcher.Default.OpenAsync(url);
                }
            });
        }

        public async Task LoadPostAsync(int postId)
        {
            SelectedPost = await _database.GetOpportunityByIdAsync(postId);

        }


        private async Task DeletePost(Opportunity post)
        {
            if (post == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you want to delete this post?", "Yes", "No");
            if (confirm)
            {
                await _database.DeleteOpportunityAsync(post);
                Opportunities.Remove(post);
                await Shell.Current.GoToAsync("..");
            }
        }
    }

}