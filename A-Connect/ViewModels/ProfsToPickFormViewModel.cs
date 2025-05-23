﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Windows.Input;
using A_Connect.Models;
using A_Connect.Services;
using Microsoft.Maui.Controls;

namespace A_Connect.ViewModels
{
    public class ProfsToPickFormViewModel : BaseViewModel
    {
        private readonly ReviewDatabase _database;

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string CourseCode { get; set; }
        public string ReviewText { get; set; }
        private string _semesterTaken;
        public string SemesterTaken
        {
            get => _semesterTaken;
            set
            {
                if (_semesterTaken != value)
                {
                    _semesterTaken = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _schoolYear;
        public string SchoolYear
        {
            get => _schoolYear;
            set
            {
                if (_schoolYear != value)
                {
                    _schoolYear = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<string> SemesterOptions { get; } = new ObservableCollection<string> { "1st Sem", "2nd Sem", "Intersession" };
        public ObservableCollection<string> SchoolYearOptions { get; } = new ObservableCollection<string>(Enumerable.Range(2018, 7).Select(year => $"{year}-{year + 1}"));

        public ICommand SubmitReviewCommand { get; }

        private int _selectedRating;

        public int SelectedRating
        {
            get => _selectedRating;
            set
            {
                if (_selectedRating != value)
                {
                    _selectedRating = value;
                    OnPropertyChanged();
                    IsRatingSet = _selectedRating > 0; // Mark the rating as set when the slider is moved
                }
            }
        }

        private bool _isRatingSet;
        public bool IsRatingSet
        {
            get => _isRatingSet;
            set
            {
                if (_isRatingSet != value)
                {
                    _isRatingSet = value;
                    OnPropertyChanged();
                }
            }
        }

        public ProfsToPickFormViewModel(ReviewDatabase database)
        {
            _database = database;
            SubmitReviewCommand = new Command(async () => await SubmitReview());
            IsRatingSet = false; // Initially, the rating is not set
        }

        private async Task SubmitReview()
        {

            LastName = string.IsNullOrWhiteSpace(LastName) ? string.Empty : System.Text.RegularExpressions.Regex.Replace(LastName.Trim(), @"\s+", " ");
            FirstName = string.IsNullOrWhiteSpace(FirstName) ? string.Empty : System.Text.RegularExpressions.Regex.Replace(FirstName.Trim(), @"\s+", " ");

            CourseCode = string.IsNullOrWhiteSpace(CourseCode) ? string.Empty : CourseCode.Trim();

            if (string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(CourseCode) || string.IsNullOrWhiteSpace(SemesterTaken) ||
            string.IsNullOrWhiteSpace(SchoolYear) || SelectedRating <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill all fields.", "OK");
                return;
            }

            var courseCodePattern = @"^[A-Za-z]+ [0-9]+$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(CourseCode, courseCodePattern))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Course code must contain letters followed by a space and then digits (e.g., CSCI 41).", "OK");
                return;
            }

            var newReview = new Review
            {
                AuthorID = App.CurrentUser.Username,
                ProfessorName = $"{LastName.ToUpper()}, {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(FirstName.ToLower())}",
                CourseCode = CourseCode.ToUpper(),
                ReviewText = ReviewText,
                Rating = SelectedRating,
                SemesterTaken = SemesterTaken,
                SchoolYear = SchoolYear,
                DatePosted = DateTime.Now
            };

            await _database.SaveReviewAsync(newReview);
            await Application.Current.MainPage.DisplayAlert("Success", "Review submitted!", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}
