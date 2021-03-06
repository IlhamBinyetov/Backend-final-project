using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
        public bool IsAdmin { get; set; } 
        public List<Order> Orders { get; set; }

        public string ConnectionId { get; set; }
        public DateTime LastConnectedDate { get; set; }
    }
}
