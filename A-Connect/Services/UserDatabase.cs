using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using A_Connect.Models;
using System.Threading.Tasks;
namespace A_Connect.Services
{
    public class UserDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public UserDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<User> GetUserAsync(string username, string password)
        {
            return _database.Table<User>()
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _database.Table<User>()
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }
        // register new users:
        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }
    }
}
