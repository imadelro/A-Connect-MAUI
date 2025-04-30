using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using A_Connect.Models;
using A_Connect.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace A_Connect.ViewModels
{
    public class MarketplaceFormViewModel : BaseViewModel
    {
        private readonly MarketplaceDatabase _database;
        private readonly string _currentUser;

        private string _selectedImagePath;
        public string SelectedImagePath
        {
            get => _selectedImagePath;
            set
            {
                _selectedImagePath = value;
                OnPropertyChanged(nameof(SelectedImagePath));
                OnPropertyChanged(nameof(HasSelectedImage));
            }
        }

        public bool HasSelectedImage => !string.IsNullOrEmpty(SelectedImagePath);

        public ICommand SubmitCommand { get; }
        public ICommand UploadImageCommand { get; }
        public ICommand TakePhotoCommand { get; }

        public MarketplaceFormViewModel(MarketplaceDatabase database, string currentUser)
        {
            _database = database;
            _currentUser = currentUser;
            SubmitCommand = new Command(async () => await SubmitListing());
            UploadImageCommand = new Command(async () => await UploadImage());
            TakePhotoCommand = new Command(async () => await TakePhoto());
        }

        public string ListingTitle { get; set; }
        public decimal Price{ get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public string ContactDetails { get; set; }
        public string Location { get; set; }

        public string Description { get; set; }

        private async Task UploadImage()
        {
            string imagePath = await ImageService.PickAndSaveImageAsync();
            if (!string.IsNullOrEmpty(imagePath))
            {
                SelectedImagePath = imagePath;
                Debug.WriteLine($"Image selected: {imagePath}");
            }
        }

        private async Task TakePhoto()
        {
            var photo = await ImageService.TakePhotoAsync();
            if (photo != null)
            {
                SelectedImagePath = photo.FullPath;
                Debug.WriteLine($"Photo taken: {photo.FullPath}");
            }
        }

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
                Price = Price,
                Category = Category.Trim(),
                Condition = Condition.Trim(),
                PosterName = _currentUser,
                PosterContact = ContactDetails.Trim(),
                Description = Description?.Trim(),
                Location = Location,
                DatePosted = DateTime.Now,
                ImagePath = SelectedImagePath  // Store the image path
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