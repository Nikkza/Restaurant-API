using System;
using System.Collections.Generic;

namespace RestaurantApp.Models
{
    public partial class Restaurant
    {
        public int RestId { get; set; }
        public string RestName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public int? UserId { get; set; }

        public virtual UserInfo User { get; set; }
    }
}
