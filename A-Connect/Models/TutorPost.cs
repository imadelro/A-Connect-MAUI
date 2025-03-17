using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace A_Connect.Models
{
    public class TutorPost
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // e.g., MATH 21, CS101
        public string CourseCode { get; set; }

        // e.g., Tutor or Tutee
        public string Category { get; set; }

        // Poster’s name and contact info
        public string PosterName { get; set; }
        public string PosterContact { get; set; }

        // Additional info (availability, location, etc.)
        public string AdditionalInfo { get; set; }
    }
}

