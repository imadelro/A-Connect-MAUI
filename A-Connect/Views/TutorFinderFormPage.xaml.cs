using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (string.IsNullOrEmpty(courseCodeEntry.Text) ||
                string.IsNullOrEmpty(categoryEntry.Text))
            {
                await DisplayAlert("Error", "Course code and category are required.", "OK");
                return;
            }

            var newPost = new TutorPost
            {
                CourseCode = courseCodeEntry.Text.Trim(),
                Category = categoryEntry.Text.Trim(),
                PosterName = nameEntry.Text?.Trim(),
                PosterContact = contactEntry.Text?.Trim(),
                AdditionalInfo = additionalInfoEditor.Text?.Trim()
            };

            // Save to the database
            await _database.SavePostAsync(newPost);

            await DisplayAlert("Success", "Your post has been created!", "OK");

            // Go back to the feed
            await Shell.Current.GoToAsync("..");
        }
    }
}
