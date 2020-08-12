using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ResturantApp.Helpers;
using ResturantApp.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ResturantApp.Services
{
    public class UserServices : IUserService<UserInfo>
    {
        private readonly Appsettings _appSettings;
        private readonly RestDBContext _context;


        public UserServices(IOptions<Appsettings> appSettings, RestDBContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task<UserInfo> Authenticate(string username, string password)
        {
            var user =  await _context.UserInfo.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
            if (user == null)
            {
                return null;
            }
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return await Task.FromResult(user);
        }
        public async Task<UserInfo> RegisterUser(UserInfo user)
        {
            var checkUserName = await CheckUserName(user.UserName);
            if(checkUserName == false)
            {
                user.Password = LogicHandler.Encrypt(user.Password);
                user.RegisterDate = DateTime.Now;

                _context.UserInfo.Add(user);
                SaveChanges(); ;
            }
            return await Task.FromResult(user);
        }
        public async Task<bool> CheckUserName(string userName) => await Task.FromResult(_context.UserInfo.Any(x => x.UserName == userName));
        public async Task<IEnumerable<UserInfo>> GetAllUsers() => await Task.FromResult(_context.UserInfo);
        public async Task<UserInfo> GetUserById(int id) => await Task.FromResult(_context.UserInfo.FirstOrDefaultAsync(x => x.UserId == id)).Result;
        public bool SaveChanges() => (_context.SaveChanges() >= 0);
    }
}
