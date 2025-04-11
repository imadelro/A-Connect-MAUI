using System;
using System.Net;
using System.Windows.Input;
using A_Connect.Models;
using A_Connect.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace A_Connect.ViewModels
{
    public class InternNJobsFormViewModel : BaseViewModel
    {
        private string title;
        private string type;
        private string position;
        private string company;
        private string postURL;
        private string caption;

        private readonly OpportunityDatabase _database;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        public string Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }

        public string Company
        {
            get => company;
            set => SetProperty(ref company, value);
        }

        public string PostURL
        {
            get => postURL;
            set => SetProperty(ref postURL, value);
        }

        public string Caption
        {
            get => caption;
            set => SetProperty(ref caption, value);
        }

        public ICommand SubmitOpportunityCommand { get; }

        public InternNJobsFormViewModel()
        {
            _database = DependencyService.Get<OpportunityDatabase>();
            SubmitOpportunityCommand = new Command(async () => await SubmitOpportunity());
        }

        private async Task SubmitOpportunity()
        {
            if (new[] { Title, Type, Position, Company, PostURL, Caption }.Any(string.IsNullOrWhiteSpace))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill all fields.", "OK");
                return;
            }

            var newOpportunity = new Opportunity
            {
                AuthorID = App.CurrentUser.Username,
                Title = Title,
                Type = Type,
                Position = Position,
                Company = Company,
                PostURL = PostURL,
                Caption = Caption,
            };

            await _database.SaveOpportunityAsync(newOpportunity);
            await Application.Current.MainPage.DisplayAlert("Success", "Review submitted!", "OK");
            var newsfeedViewModel = DependencyService.Get<InternNJobsNewsfeedViewModel>();
            await newsfeedViewModel.LoadOpportunitiesAsync();
            await Shell.Current.GoToAsync("..");
        }
    }

}
