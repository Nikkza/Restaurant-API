using System;
using System.Collections.Generic;

namespace RestaurantApp.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            Restaurant = new HashSet<Restaurant>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime? RegisterDate { get; set; }

        public virtual ICollection<Restaurant> Restaurant { get; set; }
    }
}
