using A_Connect.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace A_Connect.Services
{
    public class STFormDatabase
    {
        private readonly SQLiteAsyncConnection _db;

        public STFormDatabase(string dbPath)
        {
            // Initialize the SQLite connection
            _db = new SQLiteAsyncConnection(dbPath);
            // Create the table if it doesn't exist
            _db.CreateTableAsync<STForm>().Wait();
        }

        // Insert a new post
        public async Task<int> SavePostAsync(STForm post)
        {
            // Check if the post already exists in the database
            var existingPost = await _db.Table<STForm>().FirstOrDefaultAsync(p => p.Id == post.Id);
            if (existingPost != null)
            {
                return 0; // Do not insert duplicate
            }

            return await _db.InsertAsync(post);
        }


        // Retrieve all posts
        public Task<List<STForm>> GetAllPostsAsync()
        {
            return _db.Table<STForm>().ToListAsync();
        }

        public async Task<int> DeletePostAsync(STForm post)
        {
            return await _db.DeleteAsync(post);
        }

    }
}
