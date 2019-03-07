using System.Threading.Tasks;
using Valuelabs.API.Models;

namespace Valuelabs.API.Data
{
    public interface IAuthRepository
    {
         Task<User> RegisterAsync(User user,string password);
         Task<User> Login(string Username,string Password);
         Task<bool> UserExistsAsync(string username);
     }
}