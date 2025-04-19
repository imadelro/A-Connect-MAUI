

using SQLite;

namespace A_Connect.Models {
    public class Opportunity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string PostedBy { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }  // "Job" or "Internship"
        public string JobField { get; set; } 
        public string Position { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }
        public string PostURL { get; set; }
        public string Caption { get; set; }

    }
}
