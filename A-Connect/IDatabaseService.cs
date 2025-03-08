using System.Threading.Tasks;

namespace A_Connect
{
    public interface IDatabaseService
    {
        Task InitializeAsync();
        Task<bool> RegisterUserAsync(string email, string password);
        Task<bool> LoginUserAsync(string email, string password);
    }
}
