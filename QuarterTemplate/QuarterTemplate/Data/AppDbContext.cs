using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Data
{

    public class AppDbContext:IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Slider> Sliders { get; set; } 
        public DbSet<Team> Teams { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<AboutUs> AboutUses { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Aminity> Aminities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FavoriteItem> FavoriteItems { get; set; }
    }
}
