using SQLite;
using System;
using System.ComponentModel;

namespace A_Connect.Models
{
    public class STForm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }

        public string CourseCode { get; set; }
        public string OfferDay { get; set; } 
        public string OfferTime { get; set; }
        public string RequestDay { get; set; }
        public string RequestTime { get; set; }
        public DateTime Date { get; set; }
        public string PostedBy { get; set; }
        public string ContactInfo { get; set; }
        public string OfferDetails => $"{OfferDay}, {OfferTime}";

        public string RequestDetails => $"{RequestDay}, {RequestTime}";
        private bool _isAvailable;
        public bool IsAvailable
        {
            get => _isAvailable;
            set
            {
                if (_isAvailable != value)
                {
                    _isAvailable = value;
                    OnPropertyChanged(nameof(IsAvailable));
                    OnPropertyChanged(nameof(AvailabilityStatus)); // Notify UI to update
                }
            }
        }

        public string AvailabilityStatus => IsAvailable ? "Available" : "Unavailable";


        public bool ShowActionButtons
        {
            get
            {
                var currentUser = App.CurrentUser?.Username ?? "Unknown";
                return PostedBy == currentUser;
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
