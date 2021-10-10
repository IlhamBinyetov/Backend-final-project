using Microsoft.EntityFrameworkCore;
using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Data
{

    public class AppDbContext:DbContext
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
    }
}
