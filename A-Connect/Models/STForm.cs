using SQLite;
using System;

namespace A_Connect.Models
{
    public class STForm
    {
        [PrimaryKey, AutoIncrement] // Added
        public int Id { get; set; }

        public string CourseCode { get; set; }
        public string TradeOffer { get; set; }
        public string TradeRequest { get; set; }
        public DateTime Date { get; set; }
        public string PostedBy { get; set; }
        public string ContactInfo { get; set; }
    }
}
