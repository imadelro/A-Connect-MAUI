using System;
using A_Connect.Models;
using A_Connect.Services;
using Microsoft.Maui.Controls;

namespace A_Connect.Views
{
    public partial class TutorFinderFormPage : ContentPage
    {
        private readonly TutorFinderDatabase _database;

        public TutorFinderFormPage(TutorFinderDatabase database)
        {
            InitializeComponent();
            _database = database;
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrEmpty(courseCodeEntry.Text)
                || categoryPicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Course code and category are required.", "OK");
                return;
            }

            if (!courseCodeEntry.Text.Contains(" "))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Course code subject and number must be separated by a space.", "OK");
                return;
            }

            // PosterName is automatically the logged-in user's name
            var posterName = App.CurrentUser?.Username ?? "UnknownUser";

            var newPost = new TutorPost
            {
                CourseCode = courseCodeEntry.Text.Trim().ToUpper(),
                Category = categoryPicker.SelectedItem.ToString().Trim(),
                PosterName = posterName,
                PosterContact = contactEntry.Text?.Trim(),
                AdditionalInfo = additionalInfoEditor.Text?.Trim(),
                Date = DateTime.Now // store the submission date
            };

            // Save to the database
            await _database.SavePostAsync(newPost);

            await DisplayAlert("Success", "Your post has been created!", "OK");

            // Go back to the feed
            await Shell.Current.GoToAsync("..");
        }
    }
}
