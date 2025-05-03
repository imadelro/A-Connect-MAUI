using A_Connect.Models;
using A_Connect.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using System;

namespace A_Connect.ViewModels
{
    public class MarketplaceListingDetailViewModel : BaseViewModel
    {
        private MarketplaceDatabase _database;
        private MarketplacePost selectedPost;
        public ObservableCollection<MarketplacePost> Items { get; set; } = new ObservableCollection<MarketplacePost>();
        public ObservableCollection<MarketplacePost> FilteredItems { get; set; } = new ObservableCollection<MarketplacePost>();

        private string _postedByDisplayName;
        public string DisplayName
        {
            get => _postedByDisplayName;
            set => SetProperty(ref _postedByDisplayName, value); // Using SetProperty from BaseViewModel
        }

        public MarketplacePost SelectedPost
        {
            get => selectedPost;
            set
            {
                if (SetProperty(ref selectedPost, value)) // Using SetProperty here
                {
                    OnPropertyChanged(nameof(IsCurrentUserPost)); // Notify that IsCurrentUserPost property may have changed
                    LoadPostedByDisplayName(); // Load display name for the user who posted
                }
            }
        }

        public ICommand DeletePostCommand { get; }

        private readonly UserDatabase _userDatabase;

        public MarketplaceListingDetailViewModel(UserDatabase userDatabase)
        {
            _userDatabase = userDatabase;
            _database = App.MarketplaceDB;
            DeletePostCommand = new Command<MarketplacePost>(async (post) => await DeletePost(post));
        }

        public bool IsCurrentUserPost
        {
            get => SelectedPost != null && SelectedPost.PosterName == App.CurrentUser.Username;
        }

        private async void LoadPostedByDisplayName()
        {

            if (SelectedPost != null)
            {
                try
                {
                    string displayname = await _userDatabase.GetDisplayNameByUsernameAsync(SelectedPost.PosterName);
                    if (displayname != null)
                    {
                        DisplayName = displayname;
                    }
                    else
                    {
                        DisplayName = SelectedPost.PosterName; // Fallback if no display name found
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading display name for {SelectedPost.PosterName}: {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }
            }
           
        }


        private async Task DeletePost(MarketplacePost post)
        {
            if (post == null)
            {
                Console.WriteLine("Attempted to delete a null post.");
                return;
            }

            try
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you want to delete this post?", "Yes", "No");
                if (confirm)
                {
                    await _database.DeletePostAsync(post);
                    Items.Remove(post);
                    FilteredItems.Remove(post);
                    await Shell.Current.GoToAsync("..");
                    Console.WriteLine($"Post {post.Id} deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Post deletion canceled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting post with ID {post?.Id}: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

    

    }
}
