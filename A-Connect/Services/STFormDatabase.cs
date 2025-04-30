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
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<STForm>().Wait();
        }

        // Insert a new post
        public async Task<int> SavePostAsync(STForm post)
        {
            var existingPost = await _db.Table<STForm>().FirstOrDefaultAsync(p => p.Id == post.Id);
            if (existingPost != null)
            {
                return 0; 
            }

            return await _db.InsertAsync(post);
        }

        public Task<List<STForm>> GetAllPostsAsync()
        {
            return _db.Table<STForm>().ToListAsync();
        }

        public async Task<int> DeletePostAsync(STForm post)
        {
            return await _db.DeleteAsync(post);
        }

        public async Task MarkAsUnavailableAsync(STForm post)
        {
            post.IsAvailable = false;
            await UpdatePostAsync(post);
        }

        public async Task MarkAsAvailableAsync(STForm post)
        {
            post.IsAvailable = true;
            await UpdatePostAsync(post);
        }

        private async Task UpdatePostAsync(STForm post)
        {
            await _db.UpdateAsync(post); 
        }

    }
}
