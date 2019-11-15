using System.Collections.Generic;
using System.Threading.Tasks;
using API.Helpers;
using API.Models;

namespace API.Data
{
    public interface IBookingRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id, bool isCurrentUser);
    }
}