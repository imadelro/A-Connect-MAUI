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
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
