using System;

namespace A_Connect.Models
{
    public class TradePost
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string TradeOffer { get; set; }
        public string TradeRequest { get; set; }
        public DateTime Date { get; set; }
        public string PostedBy { get; set; } // This comes from App.CurrentUser.Username
        public string ContactInfo { get; set; }
    }
}
