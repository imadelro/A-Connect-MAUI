using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using A_Connect.Models;


namespace A_Connect.Services
{
    public class ScheduleTradingService
    {
        private readonly SQLiteAsyncConnection _db;

        public ScheduleTradingService(SQLiteAsyncConnection db)
        {
            _db = db;
            _db.CreateTableAsync<ScheduleTradingPost>().Wait();
        }

        // Get all posts
        public Task<List<ScheduleTradingPost>> GetAllPostsAsync()
        {
            return _db.Table<ScheduleTradingPost>().ToListAsync();
        }

        // Save a new post
        public Task<int> SavePostAsync(ScheduleTradingPost post)
        {
            return _db.InsertAsync(post);
        }

        // Search by course code in either "Wanted" or "Have"
        public Task<List<ScheduleTradingPost>> SearchPostsAsync(string text)
        {
            return _db.Table<ScheduleTradingPost>()
                .Where(p => p.WantedCourseCode.Contains(text) || p.HaveCourseCode.Contains(text))
                .ToListAsync();
        }
    }
}
