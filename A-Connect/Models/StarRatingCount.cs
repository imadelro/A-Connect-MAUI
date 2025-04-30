using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Connect.Models
{
    public class StarRatingCount
    {
        public string StarRating { get; set; }  // E.g., "5 Stars"
        public int ReviewCount { get; set; }    // Number of reviews for this rating
    }
}
