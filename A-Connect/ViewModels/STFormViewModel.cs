using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using A_Connect.Models;
using A_Connect.Services;
using System.Collections.ObjectModel;

namespace A_Connect.ViewModels
{
    public class STFormViewModel : BaseViewModel
    {
        private readonly STFormDatabase _db;
        public string CourseCode { get; set; }
        public string OfferDay { get; set; }
        public string OfferTime { get; set; }
        public string RequestDay { get; set; }
        public string RequestTime { get; set; }
        public string ContactInfo { get; set; }

        private string _selectedOfferDay;
        public string SelectedOfferDay
        {
            get => _selectedOfferDay;
            set => SetProperty(ref _selectedOfferDay, value);
        }

        private string _selectedOfferTime;
        public string SelectedOfferTime
        {
            get => _selectedOfferTime;
            set => SetProperty(ref _selectedOfferTime, value);
        }

        private string _selectedRequestDay;
        public string SelectedRequestDay
        {
            get => _selectedRequestDay;
            set => SetProperty(ref _selectedRequestDay, value);
        }

        private string _selectedRequestTime;
        public string SelectedRequestTime
        {
            get => _selectedRequestTime;
            set => SetProperty(ref _selectedRequestTime, value);
        }

        public ObservableCollection<string> DayOptions { get; } = new ObservableCollection<string>
    {
        "M-TH", "T-F","M", "T", "W", "TH", "F","S"
    };

        public ObservableCollection<string> TimeOptions { get; } = new ObservableCollection<string>
    {
        // 1-hour increments
        "7:00 - 8:00 AM", "8:00 - 9:00 AM", "9:00 - 10:00 AM", "10:00 - 11:00 AM", "11:00 - 12:00 PM",
        "12:00 - 1:00 PM", "1:00 - 2:00 PM", "2:00 - 3:00 PM", "3:00 - 4:00 PM", "4:00 - 5:00 PM",
        "5:00 - 6:00 PM", "6:00 - 7:00 PM", "7:00 - 8:00 PM", "8:00 - 9:00 PM",

        // 1hr 30min increments
        "8:00 - 9:30 AM", "9:30 - 11:00 AM", "11:00 - 12:30 PM", "12:30 - 2:00 PM", "2:00 - 3:30 PM",
        "3:30 - 5:00 PM", "5:00 - 6:30 PM", "6:30 - 8:00 PM", "8:00 - 9:30 PM",

        // 3-hour increments
        "8:00 - 11:00 AM", "11:00 - 2:00 PM", "2:00 - 5:00 PM", "5:00 - 8:00 PM"
    };
        public ICommand PostTradeCommand { get; }

        public STFormViewModel(STFormDatabase db)
        {
            PostTradeCommand = new Command(async () => await PostTrade());
            _db = db;
        }

        private async Task PostTrade()
        {
            CourseCode = string.IsNullOrWhiteSpace(CourseCode) ? string.Empty : CourseCode.Trim();
            // Validate input fields
            if (string.IsNullOrWhiteSpace(CourseCode) || string.IsNullOrWhiteSpace(SelectedOfferDay) ||
                string.IsNullOrWhiteSpace(SelectedOfferTime) || string.IsNullOrWhiteSpace(SelectedRequestTime) 
                || string.IsNullOrWhiteSpace(SelectedRequestTime) || string.IsNullOrWhiteSpace(ContactInfo))
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

            // Use App.CurrentUser to fill in the username
            var currentUsername = App.CurrentUser?.Username ?? "Unknown";

            // Create a new trade post
            var newPost = new STForm
            {
                PostedBy = currentUsername,
                CourseCode = CourseCode.ToUpper(),
                OfferDay = SelectedOfferDay,
                OfferTime = SelectedOfferTime,
                RequestDay = SelectedRequestDay,
                RequestTime = SelectedRequestTime,
                ContactInfo = ContactInfo,
                Date = DateTime.Now,
                IsAvailable = true
            };

            // Save the new post to the database
            await _db.SavePostAsync(newPost);

            MessagingCenter.Send(this, "NewTradePost", newPost);

            // Display success message
            await Application.Current.MainPage.DisplayAlert("Success", "Trade post submitted!", "OK");

            // Navigate back to the previous page (News Feed)
            await Shell.Current.GoToAsync("..");
        }
    }
}