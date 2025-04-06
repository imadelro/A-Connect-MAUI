using SQLite;
using A_Connect.Models;
using System.Diagnostics;

namespace A_Connect.Services
{
    public class MarketplaceDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public MarketplaceDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<MarketplacePost>().Wait();
        }

        public async Task<List<MarketplacePost>> GetPostsAsync()
        {
            Debug.WriteLine("Retrieving posts from database...");
            var posts = await _database.Table<MarketplacePost>().OrderByDescending(p => p.DatePosted).ToListAsync();
            Debug.WriteLine($"Retrieved {posts.Count} posts.");
            return posts;
        }

        public async Task<int> SavePostAsync(MarketplacePost post)
        {
            if (post.Id != 0)
            {
                Debug.WriteLine("Updating existing post...");
                return await _database.UpdateAsync(post);
            }
            else
            {
                Debug.WriteLine("Inserting new post...");
                return await _database.InsertAsync(post);
            }
        }

        public Task<int> DeletePostAsync(MarketplacePost post)
        {
            return _database.DeleteAsync(post);
        }
    }
}
