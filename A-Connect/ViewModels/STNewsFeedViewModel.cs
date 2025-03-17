using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using A_Connect.Models;
using System.Collections.Generic;

namespace A_Connect.ViewModels
{
    public class STNewsFeedViewModel : BaseViewModel
    {
        public ObservableCollection<STForm> Posts { get; set; }
        public string SearchText { get; set; }

        public ICommand ShowOtherPostsCommand { get; }
        public ICommand ShowOwnPostsCommand { get; }
        public ICommand PostSelectedCommand { get; }

        public STNewsFeedViewModel()
        {
            // Sample posts for demonstration.
            Posts = new ObservableCollection<STForm>
            {
                new STForm
                {
                    CourseCode = "CS101",
                    TradeOffer = "Mon 9-11am",
                    TradeRequest = "Wed 2-4pm",
                    PostedBy = "UserA",
                    Date = DateTime.Now.AddDays(-1),
                    ContactInfo = "usera@example.com"
                },
                new STForm
                {
                    CourseCode = "MATH202",
                    TradeOffer = "Tue 1-3pm",
                    TradeRequest = "Thu 10-12am",
                    PostedBy = "UserB",
                    Date = DateTime.Now.AddDays(-2),
                    ContactInfo = "userb@example.com"
                }
            };

            ShowOtherPostsCommand = new Command(() =>
            {
                // Implementation for filtering posts by other users.
            });

            ShowOwnPostsCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("OwnPostsPage");
            });

            PostSelectedCommand = new Command<STForm>(async (post) =>
            {
                var navParameter = new Dictionary<string, object> { { "SelectedPost", post } };
                await Shell.Current.GoToAsync("IndividualPostPage", navParameter);
            });
        }
    }
}
