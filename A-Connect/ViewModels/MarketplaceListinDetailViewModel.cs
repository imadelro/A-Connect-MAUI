using A_Connect.Models;
using A_Connect.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace A_Connect.ViewModels
{
    public class MarketplaceListingDetailViewModel : INotifyPropertyChanged
    {
        private MarketplaceDatabase _database;

        private MarketplacePost selectedPost;
        public ObservableCollection<MarketplacePost> Items { get; set; } = new ObservableCollection<MarketplacePost>();
        public ObservableCollection<MarketplacePost> FilteredItems { get; set; } = new ObservableCollection<MarketplacePost>();



        public MarketplacePost SelectedPost
        {
            get => selectedPost;
            set
            {
                if (selectedPost != value)
                {
                    selectedPost = value;
                    OnPropertyChanged(nameof(SelectedPost));
                    OnPropertyChanged(nameof(IsCurrentUserPost));
                }
            }
        }

        public ICommand DeletePostCommand { get; }

        public MarketplaceListingDetailViewModel()
        {
            _database = App.MarketplaceDB;

            DeletePostCommand = new Command<MarketplacePost>(async (post) => await DeletePost(post));

        }

        public bool IsCurrentUserPost
        {
            get => SelectedPost != null && SelectedPost.PosterName == App.CurrentUser.Username;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task DeletePost(MarketplacePost post)
        {
            if (post == null) return;

            Console.WriteLine(SelectedPost.Location);


            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you want to delete this post?", "Yes", "No");
            if (confirm)
            {
                await _database.DeletePostAsync(post);
                Items.Remove(post);
                FilteredItems.Remove(post);
                await Shell.Current.GoToAsync("..");

            }
        }
    }
}
