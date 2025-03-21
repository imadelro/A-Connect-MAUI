using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using A_Connect.Models;
using A_Connect.Services;
using Microsoft.Maui.Controls;
using System;

namespace A_Connect.Views
{
    [QueryProperty(nameof(PostId), "PostId")]
    public partial class TutorFinderDetailsPage : ContentPage
    {
        private readonly TutorFinderDatabase _database;
        private int _postId;

        public int PostId
        {
            get => _postId;
            set
            {
                _postId = value;
                LoadPostAsync(value);
            }
        }

        public TutorFinderDetailsPage(TutorFinderDatabase database)
        {
            InitializeComponent();
            _database = database;
        }

        private async void LoadPostAsync(int id)
        {
            TutorPost post = await _database.GetPostByIdAsync(id);
            if (post == null)
            {
                await DisplayAlert("Error", "Post not found.", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

            courseCodeLabel.Text = post.CourseCode;
            categoryLabel.Text = post.Category;
            nameLabel.Text = post.PosterName;
            contactLabel.Text = post.PosterContact;
            additionalInfoLabel.Text = post.AdditionalInfo;
            dateLabel.Text = post.Date.ToString("MMM d, yyyy");

        }
    }
}
