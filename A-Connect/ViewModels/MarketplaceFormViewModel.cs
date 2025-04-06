using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using A_Connect.Models;
using A_Connect.Services;
using System.Diagnostics;

namespace A_Connect.ViewModels
{
    public class MarketplaceFormViewModel : BaseViewModel
    {
        private readonly MarketplaceDatabase _database;
        private readonly string _currentUser;

        public ObservableCollection<string> UploadedImages { get; set; } = new ObservableCollection<string>();

        public ICommand SubmitCommand { get; }

        public MarketplaceFormViewModel(MarketplaceDatabase database, string currentUser)
        {
            _database = database;
            _currentUser = currentUser;
            SubmitCommand = new Command(async () => await SubmitListing());
        }

        public string ListingTitle { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public string ContactDetails { get; set; }
        public string Description { get; set; }

        private async Task SubmitListing()
        {
            var newPost = new MarketplacePost
            {
                ListingTitle = ListingTitle,
                Category = Category,
                Condition = Condition,
                PosterName = _currentUser,
                PosterContact = ContactDetails,
                Description = Description,
                //ImagePaths = string.Join(",", UploadedImages),
                DatePosted = DateTime.Now
            };

            await _database.SavePostAsync(newPost);
            await Shell.Current.GoToAsync(".."); // Go back to NewsFeed page
        }
        public async Task<bool> SubmitPostAsync(string title, string category, string condition, string contact, string description)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(condition) || string.IsNullOrEmpty(contact))
            {
                return false;
            }

            var newPost = new MarketplacePost
            {
                ListingTitle = title.Trim(),
                Category = category.Trim(),
                Condition = condition.Trim(),
                PosterName = App.CurrentUser.Username,
                PosterContact = contact.Trim(),
                Description = description?.Trim(),
                DatePosted = DateTime.Now
            };

            Debug.WriteLine("Saving new post to database...");
            await _database.SavePostAsync(newPost);
            Debug.WriteLine("Post saved successfully.");
            return true;
        }
    }
}

