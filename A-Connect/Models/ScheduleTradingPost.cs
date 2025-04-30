using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace A_Connect.Models
{
    public class ScheduleTradingPost
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // The schedule the user WANTS
        public string WantedCourseCode { get; set; }
        public string WantedSection { get; set; }
        public string WantedSchedule { get; set; }

        // The schedule the user HAS
        public string HaveCourseCode { get; set; }
        public string HaveSection { get; set; }
        public string HaveSchedule { get; set; }

        // Contact details
        public string PosterName { get; set; }
        public string PosterFacebook { get; set; }
        public string PosterPhone { get; set; }
    }
}
