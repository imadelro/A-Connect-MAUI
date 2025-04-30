using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace A_Connect.Models
{
    public class Review
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string AuthorID { get; set; }
        public string ProfessorName { get; set; }
        public string CourseCode { get; set; }
        public string SemesterTaken { get; set; }
        public string SchoolYear { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime DatePosted { get; set; }
        public string SemesterAndYear => $"{SemesterTaken} AY {SchoolYear}";

        public string Star1 => Rating >= 1 ? "star_filled.png" : "star_empty.png";
        public string Star2 => Rating >= 2 ? "star_filled.png" : "star_empty.png";
        public string Star3 => Rating >= 3 ? "star_filled.png" : "star_empty.png";
        public string Star4 => Rating >= 4 ? "star_filled.png" : "star_empty.png";
        public string Star5 => Rating >= 5 ? "star_filled.png" : "star_empty.png";
}

}
