using Microsoft.CodeAnalysis.CSharp.Syntax;
using RestaurantApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.Services
{
    public interface IUserService
    {
        Task<UserInfo> Authenticate(string username, string password);
        Task<bool> CheckUserName(string userName);
        
    }
}
