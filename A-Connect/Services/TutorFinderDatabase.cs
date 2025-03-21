using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using A_Connect.Models;

namespace A_Connect.Services
{
    public class TutorFinderDatabase
    {
        private readonly SQLiteAsyncConnection _db;

        public TutorFinderDatabase(string dbPath)
        {
            // Initialize the DB connection
            _db = new SQLiteAsyncConnection(dbPath);
            // Create the table if it doesn't exist
            _db.CreateTableAsync<TutorPost>().Wait();
        }

        // Insert a new post
        public Task<int> SavePostAsync(TutorPost post)
        {
            return _db.InsertAsync(post);
        }

        // Retrieve all posts
        public Task<List<TutorPost>> GetAllPostsAsync()
        {
            return _db.Table<TutorPost>().ToListAsync();
        }

        // Search posts by course code or category
        public Task<List<TutorPost>> SearchPostsAsync(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return GetAllPostsAsync();
            }
            searchText = searchText.ToLower();

            return _db.Table<TutorPost>()
                .Where(p => p.CourseCode.ToLower().Contains(searchText)
                         || p.Category.ToLower().Contains(searchText))
                .ToListAsync();
        }

        // Get a single post by ID
        public Task<TutorPost> GetPostByIdAsync(int id)
        {
            return _db.Table<TutorPost>()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        // (Optional) Delete or update methods if needed
        public Task<int> DeletePostAsync(TutorPost post)
        {
            return _db.DeleteAsync(post);
        }
    }
}

