using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using A_Connect.Models;

namespace A_Connect.ViewModels
{
    public class STFormViewModel : BaseViewModel
    {
        private string _courseCode;
        public string CourseCode
        {
            get => _courseCode;
            set => SetProperty(ref _courseCode, value);
        }

        private string _tradeOffer;
        public string TradeOffer
        {
            get => _tradeOffer;
            set => SetProperty(ref _tradeOffer, value);
        }

        private string _tradeRequest;
        public string TradeRequest
        {
            get => _tradeRequest;
            set => SetProperty(ref _tradeRequest, value);
        }

        private string _contactInfo;
        public string ContactInfo
        {
            get => _contactInfo;
            set => SetProperty(ref _contactInfo, value);
        }

        public ICommand PostTradeCommand { get; }

        public STFormViewModel()
        {
            PostTradeCommand = new Command(async () =>
            {
                // Use App.CurrentUser to fill in the username.
                var currentUsername = App.CurrentUser?.Username ?? "Unknown";

                var newPost = new TradePost
                {
                    CourseCode = CourseCode,
                    TradeOffer = TradeOffer,
                    TradeRequest = TradeRequest,
                    ContactInfo = ContactInfo,
                    PostedBy = currentUsername,
                    Date = DateTime.Now
                };

                // Send the new post to OwnPostsViewModel via MessagingCenter.
                MessagingCenter.Send(this, "NewTradePost", newPost);

                // Navigate back after posting.
                await Shell.Current.GoToAsync("..");
            });
        }
    }
}
