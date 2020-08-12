using ResturantApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantApp.Services
{
    public interface IUserService<T>
    {
        Task<T> Authenticate(string username, string password);
        Task<IEnumerable<T>> GetAllUsers();
        Task<bool> CheckUserName(string userName);
        Task<T> RegisterUser(UserInfo user);
        Task<T> GetUserById(int id);
        bool SaveChanges();
    }
}
