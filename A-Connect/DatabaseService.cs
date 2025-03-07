using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using A_Connect.Models;

namespace A_Connect
{
    public class DatabaseService : IDatabaseService
    {
        private string _filePath;
        private List<User> _users;

        public async Task InitializeAsync()
        {
            // Prevent re-initialization
            if (_users != null)
                return;

            // Place the file in the same folder as the running application
            // NOTE: On Android/iOS, this is typically read-only.
            // This approach is most likely to work on Windows or Mac Catalyst.
            string basePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
            _filePath = Path.Combine(basePath, "users.json");

            _users = new List<User>();
            System.Diagnostics.Debug.WriteLine($"[DEBUG] File path: {_filePath}");

            // Load existing data if the file already exists
            if (File.Exists(_filePath))
            {
                var json = await File.ReadAllTextAsync(_filePath);
                _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
        }

        private async Task SaveChangesAsync()
        {
            // Serialize the list of users to JSON and write to file
            var json = JsonSerializer.Serialize(_users);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<bool> RegisterUserAsync(string email, string password)
        {
            await InitializeAsync();

            // Check if email is already taken
            if (_users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                return false;

            // Add the new user
            _users.Add(new User { Email = email, Password = password });
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> LoginUserAsync(string email, string password)
        {
            await InitializeAsync();

            // Check if a user with matching credentials exists
            return _users.Any(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password
            );
        }
    }
}
