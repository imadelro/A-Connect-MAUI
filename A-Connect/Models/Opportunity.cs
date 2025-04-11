

namespace A_Connect.Models {
    public class Opportunity
    {
        public string AuthorID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }  // "Job" or "Internship"
        public string Position { get; set; }
        public string Company { get; set; }
        public string PostURL { get; set; }
        public string Caption { get; set; }

    }
}
