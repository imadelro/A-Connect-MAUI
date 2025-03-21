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
