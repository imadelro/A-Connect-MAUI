using System;
using System.Windows.Input;
using A_Connect.Models;
using A_Connect.Services;
using Microsoft.Maui.Controls;

namespace A_Connect.ViewModels
{
    public class ProfsToPickFormViewModel : BaseViewModel
    {
        private readonly ReviewDatabase _database;

        public string ProfessorName { get; set; }
        public string CourseCode { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public string SemesterTaken { get; set; }
        public string AuthorID { get; set; }

        public ICommand SubmitReviewCommand { get; }

        public ProfsToPickFormViewModel(ReviewDatabase database)
        {
            _database = database;
            SubmitReviewCommand = new Command(async() =>
            {
                await SubmitReview(); 
            });
        }
        private async Task SubmitReview()
        {
            if (string.IsNullOrWhiteSpace(ProfessorName) || string.IsNullOrWhiteSpace(CourseCode) || Rating <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill all fields.", "OK");
                return;
            }

            var newReview = new Review
            {
                AuthorID = AuthorID,
                ProfessorName = ProfessorName,
                CourseCode = CourseCode,
                ReviewText = ReviewText,
                Rating = Rating,
                SemesterTaken = SemesterTaken,
                DatePosted = DateTime.Now
            };

            await _database.SaveReviewAsync(newReview);
            await Application.Current.MainPage.DisplayAlert("Success", "Review submitted!", "OK");
        }
    }
}
