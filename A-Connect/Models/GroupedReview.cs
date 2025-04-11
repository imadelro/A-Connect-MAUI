using A_Connect.Services;
using A_Connect.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Connect.Models
{

    public class GroupedReview : BaseViewModel
    {
        public string ProfessorName { get; }
        public ObservableCollection<Review> Reviews { get; }
        public string CourseCodes => string.Join(", ", Reviews.Select(r => r.CourseCode).Distinct());
        public int AverageRating => (int)Math.Round(Reviews.Average(r => r.Rating), MidpointRounding.AwayFromZero);
        public string Star1 => AverageRating >= 1 ? "star_filled.png" : "star_empty.png";
        public string Star2 => AverageRating >= 2 ? "star_filled.png" : "star_empty.png";
        public string Star3 => AverageRating >= 3 ? "star_filled.png" : "star_empty.png";
        public string Star4 => AverageRating >= 4 ? "star_filled.png" : "star_empty.png";
        public string Star5 => AverageRating >= 5 ? "star_filled.png" : "star_empty.png";

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ReviewCount => Reviews?.Count ?? 0;

        public GroupedReview(string professorName, ObservableCollection<Review> reviews)
        {
            ProfessorName = professorName;
            Reviews = reviews;
            _isExpanded = false;
        }
    }


}
