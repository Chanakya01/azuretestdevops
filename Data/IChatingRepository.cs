using System.Collections.Generic;
using System.Threading.Tasks;
using Valuelabs.API.Models;

namespace Valuelabs.API.Data
{
    public interface IChatingRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);

    }
}