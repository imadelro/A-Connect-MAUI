using A_Connect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Connect.Services
{
    class SearchService
    {
        private List<Professor> _professors = new();

        public List<Professor> SearchProfessors(string query)
        {
            return _professors.Where(p => p.FullName.Contains(query)).ToList();

        }
    }
}
