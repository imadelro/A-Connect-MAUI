using SQLite;
using System;

namespace A_Connect.Models
{
    public class STForm
    {
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

    }
}
