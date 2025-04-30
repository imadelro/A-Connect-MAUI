using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Connect.Services
{
    public class UserDatabaseService
    {
        public UserDatabase GetUserDatabase()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "User.db3");
            return new UserDatabase(dbPath);
        }
    }
}
