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
        private SQLiteAsyncConnection _connection;


        public UserDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<User>().Wait();
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

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _database.Table<User>()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }
        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public async Task<User> GetUserByDisplayNameAsync(string displayName)
        {
            return await _database.Table<User>()
                                  .FirstOrDefaultAsync(u => u.DisplayName == displayName);
        }

        public async Task<string> GetDisplayNameByUsernameAsync(string username)
        {
            var user = await _database.Table<User>()
                                      .Where(u => u.Username == username)
                                      .FirstOrDefaultAsync();

            return user?.DisplayName;
        }

        public async Task<int> UpdateDisplayNameAsync(string email, string newDisplayName)
        {
            var user = await _database.Table<User>().FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.DisplayName = newDisplayName;
                return await _database.UpdateAsync(user); // returns # rows affected
            }

            return 0;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                // Fetch all users from the User table
                var users = await _connection.Table<User>().ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<User>(); // Return an empty list in case of an error
            }
        }

    }
}
