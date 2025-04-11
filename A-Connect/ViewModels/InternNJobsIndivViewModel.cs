using A_Connect.Models;
using A_Connect.Services;
using System.Threading.Tasks;

namespace A_Connect.ViewModels
{
    public class InternNJobsIndivViewModel : BaseViewModel
    {
        private OpportunityDatabase _database;
        private Opportunity _selectedPost;

        public Opportunity SelectedPost
        {
            get => _selectedPost;
            set => SetProperty(ref _selectedPost, value);
        }

        public InternNJobsIndivViewModel()
        {
            _database = DependencyService.Get<OpportunityDatabase>();
        }

        public async Task LoadPostAsync(int postId)
        {
            SelectedPost = await _database.GetOpportunityByIdAsync(postId);
        }
    }
}
