using System.Collections.Generic;
using System.Threading.Tasks;
using A_Connect.Models;
using A_Connect.Services;

namespace A_Connect.Services
{
    public class ReviewService
    {
        private readonly ReviewDatabase _database;

        public ReviewService(ReviewDatabase database)
        {
            _database = database;
        }

        public async Task<bool> SubmitReviewAsync(Review newReview)
        {
            newReview.DatePosted = DateTime.Now;
            await _database.SaveReviewAsync(newReview);
            return true; // Successfully saved
        }

        public Task<List<Review>> GetReviewsAsync()
        {
            return _database.GetAllReviewsAsync();
        }
    }
}
