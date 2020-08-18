using Microsoft.EntityFrameworkCore;
using RestaurantApp.Helpers;
using RestaurantApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.Services
{
    public class RestaurantService : IContex
    {
        private readonly RestDBContext _context;


        public RestaurantService(RestDBContext context)
        {
            _context = context;
        }
        public void CreateObject(object restinfo)
        {
            if (restinfo == null)
                throw new ArgumentNullException(nameof(restinfo));
            _context.Add(restinfo);
        }

        public async Task<IEnumerable<object>> GetAllObjects() => await _context.Restaurant.ToListAsync();
        public async Task<object> GetObjectById(int id) => await _context.Restaurant.FirstOrDefaultAsync(x => x.UserId == id);

        public Task<object> RegisterObject(object user)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges() => (_context.SaveChanges() >= 0);
    }
}
