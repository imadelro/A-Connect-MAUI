using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Connect.Models
{
    class Professor
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
        public double AverageRating { get; set; }
    }
}
