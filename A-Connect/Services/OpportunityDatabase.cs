using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using A_Connect.Models;

namespace A_Connect.Services
{
    public class OpportunityDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public OpportunityDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Opportunity>().Wait();
        }

        // Add a new opportunity (internship/job)
        public Task<int> SaveOpportunityAsync(Opportunity opportunity)
        {
            return _database.InsertAsync(opportunity);
        }

        // Get all opportunities (internships/jobs)
        public Task<List<Opportunity>> GetAllOpportunitiesAsync()
        {
            return _database.Table<Opportunity>().ToListAsync();
        }

        // Get opportunities by type (e.g., Internship or Job)
        public Task<List<Opportunity>> GetOpportunitiesByTypeAsync(string type)
        {
            return _database.Table<Opportunity>()
                .Where(o => o.Type == type)
                .ToListAsync();
        }

        // Get opportunities by position
        public Task<List<Opportunity>> GetOpportunitiesByPositionAsync(string position)
        {
            return _database.Table<Opportunity>()
                .Where(o => o.Position == position)
                .ToListAsync();
        }
        public async Task<Opportunity> GetOpportunityByIdAsync(int id)
        {
            return await _database.Table<Opportunity>().FirstOrDefaultAsync(o => o.Id == id);
        }

        // Delete an opportunity
        public Task<int> DeleteOpportunityAsync(Opportunity opportunity)
        {
            return _database.DeleteAsync(opportunity);
        }
    }
}
