using RestaurantApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.Services
{
    public interface IContex
    {
        bool SaveChanges();
        void CreateObject(object ob);
        Task<IEnumerable<object>> GetAllObjects();
        Task<object> GetObjectById(int id);
        Task<object> RegisterObject(object user);
    }
}
