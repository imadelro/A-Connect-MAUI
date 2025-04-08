using System;
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

        // The SubmitListing method is the one that handles the actual posting
        public async Task SubmitListing()
        {
            // Validate the fields
            if (string.IsNullOrEmpty(ListingTitle) || string.IsNullOrEmpty(Category) ||
                string.IsNullOrEmpty(Condition) || string.IsNullOrEmpty(ContactDetails))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill all required fields.", "OK");
                return;
            }

            var newPost = new MarketplacePost
            {
                ListingTitle = ListingTitle.Trim(),
                Category = Category.Trim(),
                Condition = Condition.Trim(),
                PosterName = _currentUser,
                PosterContact = ContactDetails.Trim(),
                Description = Description?.Trim(),
                DatePosted = DateTime.Now
            };

            // Save the post to the database
            Debug.WriteLine("Saving new post to database...");
            await _database.SavePostAsync(newPost);
            Debug.WriteLine("Post saved successfully.");

            // Navigate back to the news feed page
            await Shell.Current.GoToAsync("..");
        }
    }
}
