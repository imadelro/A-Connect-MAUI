using SQLite;
using System;

namespace A_Connect.Models
{
    public class MarketplacePost
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ListingTitle { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public string PosterName { get; set; }
        public string PosterContact { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime DatePosted { get; set; }
        public string ImagePath { get; set; } // Store the path to the saved image
        public bool IsOwnedByCurrentUser => PosterName == App.CurrentUser.Username;
    }
}