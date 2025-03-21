using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using A_Connect.Models;

namespace A_Connect.Services
{
    public class ReviewDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public ReviewDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Review>().Wait(); 
        }

        // Add a new review
        public Task<int> SaveReviewAsync(Review review)
        {
            return _database.InsertAsync(review);
        }

        // Get all reviews
        public Task<List<Review>> GetAllReviewsAsync()
        {
            return _database.Table<Review>().ToListAsync();
        }

        // Get reviews for a specific professor
        public Task<List<Review>> GetReviewsByProfessorAsync(string professorName)
        {
            return _database.Table<Review>()
                .Where(r => r.ProfessorName == professorName)
                .ToListAsync();
        }

        // Delete a review
        public Task<int> DeleteReviewAsync(Review review)
        {
            return _database.DeleteAsync(review);
        }
    }
}
