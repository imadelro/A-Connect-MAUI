using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Connect.Services;
using A_Connect.Models;

namespace A_Connect.Views
{
    public partial class ScheduleTradingFormPage : ContentPage
    {
        private readonly ScheduleTradingService _service;

        public ScheduleTradingFormPage(ScheduleTradingService service)
        {
            InitializeComponent();
            _service = service;
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Build a new post from user input
            var post = new ScheduleTradingPost
            {
                WantedCourseCode = wantedCourseCodePicker.SelectedItem?.ToString(),
                WantedSection = wantedSectionEntry.Text,
                WantedSchedule = wantedScheduleEntry.Text,

                HaveCourseCode = haveCourseCodePicker.SelectedItem?.ToString(),
                HaveSection = haveSectionEntry.Text,
                HaveSchedule = haveScheduleEntry.Text,

                PosterName = posterNameEntry.Text,
                PosterFacebook = posterFacebookEntry.Text,
                PosterPhone = posterPhoneEntry.Text
            };

            // Save to DB
            await _service.SavePostAsync(post);
            await DisplayAlert("Success", "Post added!", "OK");

            // Navigate back to the feed
            await Shell.Current.GoToAsync(".."); // Navigate back to the feed

            // Force refresh the post list (if ScheduleTradingPage has a LoadPosts method)
            if (Shell.Current.CurrentPage is ScheduleTradingPage feedPage)
            {
                await feedPage.LoadPosts(); // Reloads the posts after returning
            }

        }
    }
}
